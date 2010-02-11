//
// EditorPreviewEntity.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    namespace Editor
    {

        /// <summary>
        /// Default entity for unknown stage element type.
        /// </summary>
        public class CEditorPreviewEntity
            : CEntity
        {
            private Vector2 BasePosition { get; set; }

            public CEditorPreviewEntity(CWorld world, CEntity parent, CMover mover)
                : base(world, "EditorPreviewEntity")
            {
                Physics = new CPhysics();
                BasePosition = parent.Physics.PositionPhysics.Position;
                Physics.PositionPhysics.Position = BasePosition;
                Visual = parent.Visual;
                Mover = mover;
            }

            public override void Update()
            {
                if (Mover == null)
                {
                    base.Update();
                    return;
                }

                // update 5x for faster editor preview
                //foreach (int i in Enumerable.Range(0, 5))
                //{
                    base.Update();
                //}

                // temporary bunger
                if (AliveTime > 10.0f)
                {
                    Delete();
                }
            }
        }
    }
}
