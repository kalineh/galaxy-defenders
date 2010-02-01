//
// StageDefinition.cs
//

using System;
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

        public void AddElement(int time, CStageElement element)
        {
            List<CStageElement> elements = GetElementsAtTime(time);
            elements.Add(element);
        }

        public List<CStageElement> GetElementsAtTime(int time)
        {
            if (!Elements.ContainsKey(time))
            {
                Elements.Add(time, new List<CStageElement>());
            }

            return Elements[time];
        }

    }
}


