﻿//
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

        public void InitializeTexture()
        {
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

        public override void Update()
        {
            base.Update();
            if (IsOffScreenBottom())
                Delete();
        }
    }
}
