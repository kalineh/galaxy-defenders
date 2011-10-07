//
// FunkyClouds.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CFunkyClouds
        : CShaderScenery
    {
        Effect effect;
        Texture2D cloud_texture;
        VertexBuffer quad;
        VertexDeclaration quad_declaration;

        public struct Cloud
        {
            public Vector2 position;
            public Vector2 velocity;
        };

        List<Cloud> Clouds;

        public CFunkyClouds(CWorld world)
            : base(world)
        {
            cloud_texture = World.Game.Content.Load<Texture2D>("Textures/Background/Cloud");
            quad = new VertexBuffer(World.Game.GraphicsDevice, VertexPositionColorTexture.VertexDeclaration, 6, BufferUsage.None);
            quad.SetData(new VertexPositionColorTexture[6] {
                new VertexPositionColorTexture() { Position = new Vector3(0.0f, 0.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f) },
                new VertexPositionColorTexture() { Position = new Vector3(1.0f, 0.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f) },
                new VertexPositionColorTexture() { Position = new Vector3(0.0f, 1.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f) },
                new VertexPositionColorTexture() { Position = new Vector3(0.0f, 1.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f) },
                new VertexPositionColorTexture() { Position = new Vector3(1.0f, 0.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f) },
                new VertexPositionColorTexture() { Position = new Vector3(1.0f, 1.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f) },
            });

            Clouds = new List<Cloud>();
            for (int i = 0; i < 12; ++i)
                Clouds.Add(new Cloud());
        }

        public override void Update()
        {
            base.Update();
        }

        public void UpdateClouds()
        {
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);

            GraphicsDevice gd = World.Game.GraphicsDevice;

            gd.Clear(new Color(32, 45, 129));

            Viewport vp = gd.Viewport;
            Matrix world = Matrix.CreateScale(500.0f);
            Matrix projection = Matrix.CreateOrthographicOffCenter(0.0f, vp.Width, vp.Height, 0.0f, 0.0f, 1.0f);

            effect.Parameters["View"] .SetValue(Matrix.Identity);
            effect.Parameters["Projection"].SetValue(projection);
            effect.Parameters["CloudTexture"].SetValue(cloud_texture);
            SetShaderMusicData(effect);
            effect.Techniques[0].Passes[0].Apply();

            for (int i = 0; i < Clouds.Count; ++i)
                DrawCloud(Clouds[i]);
        }

        public void DrawCloud(Cloud cloud)
        {
            Matrix world = Matrix.CreateScale(120.0f) * Matrix.CreateTranslation(cloud.position.X, cloud.position.Y, 0.0f);
            effect.Parameters["World"].SetValue(world);
            GraphicsDevice gd = World.Game.GraphicsDevice;
            gd.SetVertexBuffer(quad);
            gd.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
        }

        protected override void ReloadShaders()
        {
            base.ReloadShaders();
            //effect = World.Game.Content.Load<Effect>("Effects/FunkyClouds");
            effect = CEffectLoader.Load(World.Game.GraphicsDevice, "FunkyClouds");
        }
    }
}

