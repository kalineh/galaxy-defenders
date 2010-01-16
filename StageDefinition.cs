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
        public Dictionary<int, List<CStageElement>> Elements { get; private set; }
        public List<CStageElement> ActiveElements { get; private set; }

        public CStageDefinition(string name)
        {
            Name = name;
            Elements = new Dictionary<int, List<CStageElement>>();
        }

        public void AddElement(float time, CStageElement element)
        {
            int frame = Time.ToFrames(time);
            List<CStageElement> elements = GetElementsAtTime(frame);
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


