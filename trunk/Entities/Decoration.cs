//
// Decoration.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CDecoration
        : CEntity
    {
        public string TextureName { get; set; }
        public int DepthOffset { get; set; }

        public CDecoration(CWorld world, Vector2 position)
            : base(world)
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
        }

        public override void Update()
        {
            // TODO: this is kind of crappy
            if (Visual == null && TextureName != null)
            {
                Visual = new CVisual(World, CContent.LoadTexture2D(World.Game, "Textures/Decoration/" + TextureName), Color.White);
            }

            base.Update();
            if (IsOffScreenBottom())
                Delete();
        }
    }
}
