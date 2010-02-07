//
// StageDefinition.cs
//

using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CStageDefinition
    {
        public string Name { get; private set; }
        public float ScrollSpeed { get; set; }
        public Dictionary<int, List<CStageElement>> Elements { get; private set; }
        public List<CStageElement> ActiveElements { get; private set; }

        public CStageDefinition(string name)
        {
            Name = name;
            Elements = new Dictionary<int, List<CStageElement>>();
            ScrollSpeed = 1.0f;
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

        public void AddElement(int time, CStageElement element, object ignored_parameter_for_editor)
        {
            AddElement(time, element);
        }

        public void AddElement(int time, CStageElement element)
        {
            List<CStageElement> elements = GetOrCreateElementsAtTime(time);
            elements.Add(element);
        }

        public bool HasElementsAtTime(int time)
        {
            return Elements.ContainsKey(time);
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


