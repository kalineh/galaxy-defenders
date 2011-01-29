//
// Stage.cs
//

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CStage
    {
        public CWorld World { get; private set; }
        public CStageDefinition Definition { get; private set; }
        public List<CStageElement> ActiveElements { get; private set; }
        public List<CStageElement> PreloadedElements { get; private set; }
        public Vector2 PreviousCameraPosition { get; set; }

        public CStage(CWorld world, CStageDefinition definition)
        {
            World = world;
            Definition = definition;
            definition.PreloadElements(world);
            ActiveElements = new List<CStageElement>(512);
        }

        public void Preload()
        {
        }

        public void Start()
        {
            PreviousCameraPosition = new Vector2(0.0f, 100000.0f);
        }

        public void Finish()
        {
            ActiveElements.Clear();
        }

        public void Update()
        {
            ActivateElements();

            for (int i = 0; i < ActiveElements.Count; ++i)
            {
                CStageElement element = ActiveElements[i];
                element.Update(World);

                if (element.IsExpired())
                {
                    ActiveElements[i] = ActiveElements[ActiveElements.Count - 1];
                    ActiveElements.RemoveAt(ActiveElements.Count - 1);
                    --i;
                }
            }

            //ActiveElements.ForEach(element => element.Update(World));
            //ActiveElements.RemoveAll(element => element.IsExpired());

            PreviousCameraPosition = World.GameCamera.GetSpawnBorderLine();
        }

        private void ActivateElements()
        {
            Vector2 current = World.GameCamera.GetSpawnBorderLine();
            Vector2 previous = PreviousCameraPosition;

            Definition.GetNewElements(ActiveElements, previous, current);
        }
    }
}
