//
// StageDefinition.cs
//

using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using System.ComponentModel;

namespace Galaxy
{
    public class CStageDefinition
    {
        [Browsable(false)]
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public float ScrollSpeed { get; set; }
        public string BackgroundSceneryName { get; set; }
        public string ForegroundSceneryName { get; set; }
        public string MusicName { get; set; }
        [Browsable(false)]
        public Dictionary<int, List<CStageElement>> Elements { get; private set; }
        [Browsable(false)]
        public List<CStageElement> ActiveElements { get; private set; }

        public static CStageDefinition GetStageDefinitionByName(string name)
        {
#if XBOX360
            Type type = Type.GetType("Galaxy.Stages." + name);
            MethodInfo generate_method = type.GetMethod("GenerateDefinition");
            CStageDefinition result = generate_method.Invoke(null, null) as CStageDefinition;
            return result;
#else
            Assembly assembly = Assembly.GetAssembly(typeof(CEntity));
            Type type = assembly.GetType("Galaxy.Stages." + name);
            MethodInfo generate_method = type.GetMethod("GenerateDefinition");
            CStageDefinition result = generate_method.Invoke(null, null) as CStageDefinition;
            return result;
#endif
        }

        public CStageDefinition(string name)
        {
            Name = name;
            Elements = new Dictionary<int, List<CStageElement>>();
            ScrollSpeed = 3.0f;
            BackgroundSceneryName = "Black";
            ForegroundSceneryName = "Empty";
            MusicName = "fighting_for_control";
        }

        public string ToFilename()
        {
            string cwd = Directory.GetCurrentDirectory();
            string base_ = cwd.Substring(0, cwd.LastIndexOf("\\bin\\"));
            if (cwd.Contains("StageEditor"))
                base_ = cwd.Substring(0, cwd.LastIndexOf("\\StageEditor\\bin\\"));
            string stage_filename = base_ + "\\StageDefinitions\\" + Name + ".cs";
            return stage_filename;
        }

        //[Obsolete("replace time spawning with positional")]
        public void AddElement(int time, CStageElement element, object ignored_parameter_for_editor)
        {
            AddElement(time, element);
        }

        //[Obsolete("replace time spawning with positional")]
        public void AddElement(int time, CStageElement element)
        {
            List<CStageElement> elements = GetOrCreateElementsAtTime(time);
            elements.Add(element);
        }

        //[Obsolete("replace time spawning with positional")]
        public bool HasElementsAtTime(int time)
        {
            return Elements.ContainsKey(time);
        }

        private bool ShouldActivateElement(Vector2 previous, Vector2 current, CStageElement element)
        {
            return element.Position.Y <= previous.Y && element.Position.Y > current.Y;
        }

        public void GetNewElements(List<CStageElement> results, Vector2 previous, Vector2 current)
        {
            // TODO: sort elements based on y?
            // TODO: not create a list every frame
            foreach (KeyValuePair<int, List<CStageElement>> element in Elements)
            {
                foreach (CStageElement item in element.Value)
                {
                    if (ShouldActivateElement(previous, current, item))
                        results.Add(item);
                }
            }
        }

        public List<CStageElement> GetOrCreateElementsAtTime(int time)
        {
            if (!Elements.ContainsKey(time))
            {
                Elements.Add(time, new List<CStageElement>());
            }

            return Elements[time];
        }
    }
}


