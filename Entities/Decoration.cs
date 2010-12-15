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
        public float DepthOffset { get; set; }

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
                //Visual = new CVisual(World, CContent.LoadTexture2D(World.Game, "Textures/Decoration/" + TextureName), Color.White);
                //Visual = CVisual.MakeSpriteCached1(World, "Textures/Decoration" + TextureName);
                if (DepthOffset != 0.0f)
                {
                    Visual = CVisual.MakeSpriteUncached(World.Game, "Textures/Decoration/" + TextureName);
                }
                else
                {
                    Visual = CVisual.MakeSpriteCached1(World.Game, "Textures/Decoration/" + TextureName);
                }

                Visual.Depth += DepthOffset;
            }

            base.Update();
            if (IsOffScreenBottom())
                Delete();
        }
    }
}
