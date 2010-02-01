//
// StageSerializer.cs
//

using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;

namespace Galaxy
{
    public class CStageSerializer
    {
        public static void Export(string filename, CStageDefinition stage)
        {
            string fullpath = Path.Combine(StorageContainer.TitleLocation, filename);
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate, FileAccess.Write);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(CStageDefinition));
                serializer.Serialize(stream, stage);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine(exception.InnerException.ToString());
            }
            finally
            {
                stream.Close();
            }
        }
    }
}
