//
// Stage.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CStage
    {
        public CWorld World { get; private set; }
        public CStageDefinition Definition { get; private set; }
        public List<CStageElement> ActiveElements { get; private set; }
        private int Frame { get; set; }
        public float ScrollSpeed { get; set; }

        public CStage(CWorld world, CStageDefinition definition)
        {
            World = world;
            Definition = definition;
            ActiveElements = new List<CStageElement>();
            Frame = 0;
        }

        public void Start()
        {
            Frame = 0;
        }

        public void Finish()
        {
        }

        public void Update()
        {
            ActivateElements();

            ActiveElements.ForEach(element => element.Update(World));
            ActiveElements.RemoveAll(element => element.IsExpired());

            Frame += 1;
        }

        private void ActivateElements()
        {
            if (!Definition.HasElementsAtTime(Frame))
                return;

            foreach (CStageElement element in Definition.GetOrCreateElementsAtTime(Frame))
            {
                ActiveElements.Add(element);
            }
        }
    }
}
