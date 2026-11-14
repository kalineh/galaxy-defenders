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

    public class CSceneryCustomEffectTest
        : CScenery
    {
        Effect effect;

        public CSceneryCustomEffectTest(CWorld world)
            : base(world)
        {
            effect = World.Game.Content.Load<Effect>("Effects/SpriteBatchCustomTest");
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);

            Viewport vp = World.Game.GraphicsDevice.Viewport;
            Matrix projection = Matrix.CreateOrthographicOffCenter(0.0f, vp.Width, vp.Height, 0.0f, 0.0f, 1.0f);
            Matrix half_pixel_offset = Matrix.CreateTranslation(-0.5f, -0.5f, 0.0f);

            effect.Parameters["World"].SetValue(Matrix.Identity);
            effect.Parameters["View"] .SetValue(Matrix.Identity);
            effect.Parameters["Projection"].SetValue(half_pixel_offset * projection);

            sprite_batch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, effect, World.Game.RenderScaleMatrix);
            //sprite_batch.Begin(0, null, null, null, null, effect);
            sprite_batch.Draw(World.Game.PixelTexture, new Rectangle(0, 0, 1920, 1080), null, Color.Blue, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
            sprite_batch.DrawString(World.Game.DebugFont, "asdasdfas", new Vector2(1000.0f, 200.0f), Color.White);
            sprite_batch.End();
        }
    }
}

