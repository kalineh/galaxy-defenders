//
// StageSerializer.cs
//

using System;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Storage;

namespace Galaxy
{
    public class CStageSerializer
    {
        public static void Export(string filename, CStageDefinition stage)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(CStageElement));
            Type[] serializable_types = assembly.GetTypes().Where(
                t => t.IsSubclassOf(typeof(CStageElement)) ||
                     t.IsSubclassOf(typeof(CEntity))
            ).ToArray();

            CStageDefinitionSerializable serializable = new CStageDefinitionSerializable();
            serializable.ConvertFromDefinition(stage);

            string fullpath = Path.Combine(StorageContainer.TitleLocation, filename);
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate, FileAccess.Write);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(CStageDefinitionSerializable), serializable_types);
                serializer.Serialize(stream, serializable);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                if (exception.InnerException != null)
                    Console.WriteLine(exception.InnerException.ToString());
            }
            finally
            {
                stream.Close();
            }
        }
    }
}
