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
      
        public CShaderScenery(CWorld world)
            : base(world)
        {
            music_data = new float[8];

            ReloadShaders();
        }

        public void UpdateMusicData()
        {
            music_data[0] = (float)CAudio.MusicBin.GetChannelData(0) / 255.0f;
            music_data[1] = (float)CAudio.MusicBin.GetChannelData(1) / 255.0f;
            music_data[2] = (float)CAudio.MusicBin.GetChannelData(2) / 255.0f;
            music_data[3] = (float)CAudio.MusicBin.GetChannelData(3) / 255.0f;
            music_data[4] = (float)CAudio.MusicBin.GetChannelData(4) / 255.0f;
            music_data[5] = (float)CAudio.MusicBin.GetChannelData(5) / 255.0f;
            music_data[6] = (float)CAudio.MusicBin.GetChannelData(6) / 255.0f;
            music_data[7] = (float)CAudio.MusicBin.GetChannelData(7) / 255.0f;
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
        }

        protected virtual void ReloadShaders()
        {
        }
    }
}

