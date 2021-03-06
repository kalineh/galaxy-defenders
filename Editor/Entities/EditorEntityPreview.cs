﻿//
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
        {
            base.Initialize(world);

            Parent = parent;
            Physics = new CPhysics();
            BasePosition = parent.Physics.Position;
            Physics.Position = BasePosition;
            Visual = parent.Visual;
        }

        public override void Update()
        {
            base.Update();

            if (AliveTime > Time.ToFrames(10.0f))
            {
                Delete();
            }
        }
    }
}
