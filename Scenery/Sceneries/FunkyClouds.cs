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
        Texture2D cloud;
        VertexBuffer quad;
        VertexDeclaration quad_declaration;
        float[] music_data;

        public CFunkyClouds(CWorld world)
            : base(world)
        {
            cloud = World.Game.Content.Load<Texture2D>("Textures/Background/Cloud");
            quad = new VertexBuffer(World.Game.GraphicsDevice, VertexPositionColor.VertexDeclaration, 6, BufferUsage.None);
            quad.SetData(new VertexPositionColor[6] {
                new VertexPositionColor() { Position = new Vector3(0.0f, 0.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), },
                new VertexPositionColor() { Position = new Vector3(1.0f, 0.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), },
                new VertexPositionColor() { Position = new Vector3(0.0f, 1.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), },
                new VertexPositionColor() { Position = new Vector3(0.0f, 1.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), },
                new VertexPositionColor() { Position = new Vector3(1.0f, 0.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), },
                new VertexPositionColor() { Position = new Vector3(1.0f, 1.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), },
            });
            music_data = new float[8];
        }

        public override void Update()
        {
            base.Update();

            music_data[0] = (float)CAudio.MusicBin.GetChannelData(0) / 255.0f;
            music_data[1] = (float)CAudio.MusicBin.GetChannelData(1) / 255.0f;
            music_data[2] = (float)CAudio.MusicBin.GetChannelData(2) / 255.0f;
            music_data[3] = (float)CAudio.MusicBin.GetChannelData(3) / 255.0f;
            music_data[4] = (float)CAudio.MusicBin.GetChannelData(4) / 255.0f;
            music_data[5] = (float)CAudio.MusicBin.GetChannelData(5) / 255.0f;
            music_data[6] = (float)CAudio.MusicBin.GetChannelData(6) / 255.0f;
            music_data[7] = (float)CAudio.MusicBin.GetChannelData(7) / 255.0f;
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);

            GraphicsDevice gd = World.Game.GraphicsDevice;

            //gd.Clear(Color.Gray);

            Viewport vp = gd.Viewport;
            Matrix world = Matrix.CreateScale(500.2f);
            Matrix projection = Matrix.CreateOrthographicOffCenter(0.0f, vp.Width, vp.Height, 0.0f, 0.0f, 1.0f);

            effect.Parameters["World"].SetValue(world);
            effect.Parameters["View"] .SetValue(Matrix.Identity);
            effect.Parameters["Projection"].SetValue(projection);
            effect.Parameters["MusicData"].SetValue(music_data);
            effect.Techniques[0].Passes[0].Apply();

            gd.SetVertexBuffer(quad);
            gd.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
        }

        protected override void ReloadShaders()
        {
            base.ReloadShaders();
            effect = World.Game.Content.Load<Effect>("Effects/FunkyClouds");
        }
    }
}

