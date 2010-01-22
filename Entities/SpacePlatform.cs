//
// SpacePlatform.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CSpacePlatform
        : CEntity
    {
        public CSpacePlatform(CWorld world, Vector2 position)
            : base(world, "SpacePlatform")
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Visual = new CVisual(world.Game.Content.Load<Texture2D>("SpacePlatform"), Color.White);
        }

        public override void Update()
        {
            base.Update();
            if (IsOffScreenBottom())
                Delete();
        }
    }
}
