//
// Scenery.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CScenery
    {
        public CWorld World { get; set; }

        public CScenery(CWorld world)
        {
            World = world;
        }

        public virtual void Update()
        {
        }

        public virtual void Draw(SpriteBatch sprite_batch)
        {
        }
    }

    public class CSceneryChain
        : CScenery
    {
        public List<CScenery> Sceneries { get; set; }

        public CSceneryChain(CWorld world, params CScenery[] sceneries)
            : base(world)
        {
            Sceneries = new List<CScenery>(sceneries);
        }

        public override void Update()
        {
            foreach (CScenery scenery in Sceneries)
                scenery.Update();
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            foreach (CScenery scenery in Sceneries)
            {
                // TODO: hack for render ordering
                sprite_batch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, World.GameCamera.WorldMatrix);
                scenery.Draw(sprite_batch);
                sprite_batch.End();
            }
        }
    }
}

