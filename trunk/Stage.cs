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
        public float ScrollSpeed { get; set; }
        public Vector2 PreviousCameraPosition { get; set; }

        public CStage(CWorld world, CStageDefinition definition)
        {
            World = world;
            Definition = definition;
            ActiveElements = new List<CStageElement>();
        }

        public void Start()
        {
            PreviousCameraPosition = Vector2.Zero;
        }

        public void Finish()
        {
            ActiveElements.Clear();
        }

        public void Update()
        {
            ActivateElements();

            ActiveElements.ForEach(element => element.Update(World));
            ActiveElements.RemoveAll(element => element.IsExpired());

            PreviousCameraPosition = World.GameCamera.GetSpawnBorderLine();
        }

        private void ActivateElements()
        {
            Vector2 current = World.GameCamera.GetSpawnBorderLine();
            Vector2 previous = PreviousCameraPosition;

            foreach (CStageElement element in Definition.GetNewElements(previous, current))
            {
                ActiveElements.Add(element);
            }
        }
    }
}
