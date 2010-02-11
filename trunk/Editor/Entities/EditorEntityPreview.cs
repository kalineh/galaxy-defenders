//
// EditorEntityPreview.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    /// <summary>
    /// Default entity for unknown stage element type.
    /// </summary>
    public class CEditorEntityPreview
        : CEntity
    {
        public CEntity Parent { get; set; }
        private Vector2 BasePosition { get; set; }

        public CEditorEntityPreview(CWorld world, CEntity parent)
            : base(world, "EditorEntityPreview")
        {
            Parent = parent;
            Physics = new CPhysics();
            BasePosition = parent.Physics.PositionPhysics.Position;
            Physics.PositionPhysics.Position = BasePosition;
            Visual = parent.Visual;
        }

        public override void Update()
        {
            base.Update();

            if (AliveTime > 10.0f)
            {
                Delete();
            }
        }
    }
}
