//
// ShaderSceneries.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CShaderScenery
        : CScenery
    {
        public float[] music_data;
        public float[] smooth_music_data;
        public Dictionary<VertexDeclaration, VertexBuffer> unit_quads;
      
        public CShaderScenery(CWorld world)
            : base(world)
        {
            music_data = new float[8];
            smooth_music_data = new float[8];
            unit_quads = new Dictionary<VertexDeclaration, VertexBuffer>();

            ReloadShaders();
        }

        public void UpdateMusicData()
        {
            for (int i = 0; i < 8; ++i)
            {
                music_data[i] = (float)CAudio.MusicBin.GetChannelData(i) / 255.0f;
                smooth_music_data[i] = MathHelper.Lerp(smooth_music_data[i], music_data[i], Math.Abs(music_data[i] - smooth_music_data[i]) * 0.05f);
            }
        }

        public float GetTotalMusicValue()
        {
            float result = 0.0f;
            for (int i = 0; i < 8; ++i)
                result += music_data[i];
            return result;
        }

        public override void Update()
        {
            base.Update();

            if (CInput.IsRawKeyPressed(Keys.R))
            {
                ReloadShaders();
            }

            UpdateMusicData();
        }

        public void SetShaderMusicData(Effect effect)
        {
            effect.Parameters["MusicData"].SetValue(music_data);
            effect.Parameters["SmoothMusicData"].SetValue(smooth_music_data);
        }

        public void DrawFullscreenQuad(VertexDeclaration declaration, Effect effect)
        {
            if (!unit_quads.ContainsKey(declaration))
            {
                if (declaration == VertexPositionColorTexture.VertexDeclaration)
                {
                    VertexBuffer vb = new VertexBuffer(World.Game.GraphicsDevice, VertexPositionColorTexture.VertexDeclaration, 6, BufferUsage.None);
                    vb.SetData(new VertexPositionColorTexture[6] {
                        new VertexPositionColorTexture() { Position = new Vector3(0.0f, 0.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f) },
                        new VertexPositionColorTexture() { Position = new Vector3(1.0f, 0.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f) },
                        new VertexPositionColorTexture() { Position = new Vector3(0.0f, 1.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f) },
                        new VertexPositionColorTexture() { Position = new Vector3(0.0f, 1.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f) },
                        new VertexPositionColorTexture() { Position = new Vector3(1.0f, 0.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f) },
                        new VertexPositionColorTexture() { Position = new Vector3(1.0f, 1.0f, 0.0f), Color = new Color(1.0f, 1.0f, 1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f) },
                    });

                    unit_quads[declaration] = vb;
                }
                else
                {
                    // todo
                    Console.WriteLine("CShaderScenery.DrawFullscreenQuad(): no support for declaration type: {0}", declaration.ToString());
                    return;
                }
            }

            VertexBuffer quad = unit_quads[declaration];

            GraphicsDevice gd = World.Game.GraphicsDevice;
            
            Viewport vp = gd.Viewport;
            Matrix world = Matrix.CreateScale(vp.Width, vp.Height, 1.0f);
            Matrix projection = Matrix.CreateOrthographicOffCenter(0.0f, vp.Width, vp.Height, 0.0f, 0.0f, 1.0f);

            effect.Parameters["World"].SetValue(world);
            effect.Parameters["Projection"].SetValue(projection);
            SetShaderMusicData(effect);
            effect.Techniques[0].Passes[0].Apply();

            gd.SetVertexBuffer(quad);
            gd.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
        }

        protected virtual void ReloadShaders()
        {
        }
    }
}

