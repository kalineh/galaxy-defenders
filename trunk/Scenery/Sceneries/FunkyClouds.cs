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
        Effect background_effect;
        Effect cloud_effect;
        Texture2D cloud_texture;
        Texture2D noise_texture;
        VertexBuffer quad;

        public class Cloud
        {
            public Vector2 position;
            public Vector2 velocity;

            public void Update()
            {
                position += velocity;
            }
        };

        List<Cloud> Clouds;

        public CFunkyClouds(CWorld world)
            : base(world)
        {
            cloud_texture = World.Game.Content.Load<Texture2D>("Textures/Background/Cloud");
            noise_texture = World.Game.Content.Load<Texture2D>("Textures/Background/PerlinNoise");
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
            for (int i = 0; i < 16; ++i)
            {
                Clouds.Add(new Cloud());
                Clouds[i].position = RandomScreenPosition();
                Clouds[i].velocity = new Vector2(0.1f * World.Random.NextSignedFloat(), 6.0f + World.Random.NextFloat() * 4.0f);
            }
        }

        public override void Update()
        {
            base.Update();

            UpdateClouds();
        }

        public void UpdateClouds()
        {
            for (int i = 0; i < Clouds.Count; ++i)
            {
                Clouds[i].Update();
                if (!World.GameCamera.IsInside(Clouds[i].position, 64.0f))
                    Clouds[i].position = RandomRespawnPosition();
            }
        }

        private Vector2 RandomScreenPosition()
        {
            Vector2 tl = World.GameCamera.GetTopLeft();
            Vector2 br = World.GameCamera.GetBottomRight();
            Vector2 range = br - tl;
            return tl + new Vector2(World.Random.NextFloat() * range.X, World.Random.NextFloat() * range.Y);
        }

        private Vector2 RandomRespawnPosition()
        {
            Vector2 tl = World.GameCamera.GetTopLeft();
            Vector2 br = World.GameCamera.GetBottomRight();
            Vector2 range = br - tl;
            return tl + new Vector2(World.Random.NextFloat() * range.X, -32.0f + World.Random.NextFloat() * -16.0f);
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);

            GraphicsDevice gd = World.Game.GraphicsDevice;

            //gd.Clear(new Color(32, 45, 129));

            background_effect.Parameters["Time"].SetValue((float)CAudio.MusicBin.FrameCount);
            background_effect.Parameters["NoiseTexture"].SetValue(noise_texture);
            DrawFullscreenQuad(VertexPositionColorTexture.VertexDeclaration, background_effect);

            Viewport vp = gd.Viewport;
            Matrix projection = Matrix.CreateOrthographicOffCenter(0.0f, vp.Width, vp.Height, 0.0f, 0.0f, 1.0f);
            cloud_effect.Parameters["Time"].SetValue((float)CAudio.MusicBin.FrameCount);
            cloud_effect.Parameters["Projection"].SetValue(projection);
            cloud_effect.Parameters["CloudTexture"].SetValue(cloud_texture);
            cloud_effect.Parameters["NoiseTexture"].SetValue(noise_texture);
            SetShaderMusicData(cloud_effect);
            cloud_effect.Techniques[0].Passes[0].Apply();

            for (int i = 0; i < Clouds.Count; ++i)
                DrawCloud(Clouds[i]);
        }

        public void DrawCloud(Cloud cloud)
        {
            Vector3 scale = new Vector3(64.0f, 32.0f, 1.0f);
            Vector2 position = cloud.position;
            Matrix world = Matrix.CreateScale(scale) * Matrix.CreateTranslation(position.X, position.Y, 0.0f);
            cloud_effect.Parameters["World"].SetValue(world * World.GameCamera.WorldMatrix);
            cloud_effect.Techniques[0].Passes[0].Apply();
            GraphicsDevice gd = World.Game.GraphicsDevice;
            gd.SetVertexBuffer(quad);
            gd.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
        }

        protected override void ReloadShaders()
        {
            base.ReloadShaders();
            //effect = World.Game.Content.Load<Effect>("Effects/FunkyClouds");
            background_effect = CEffectLoader.Load(World.Game.GraphicsDevice, "FunkyCloudsBackground") ?? background_effect;
            cloud_effect = CEffectLoader.Load(World.Game.GraphicsDevice, "FunkyClouds") ?? cloud_effect;
        }
    }
}

