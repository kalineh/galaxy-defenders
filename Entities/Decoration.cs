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

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
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
