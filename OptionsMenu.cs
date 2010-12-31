//
// OptionsMenu.cs
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Galaxy
{
    public class COptionsMenu
        : CMenu
    {
        public CMenu Menu { get; set; }

        public delegate void OnBackDelegate();
        public OnBackDelegate OnBack { get; set; }

        public COptionsMenu(CGalaxy game)
            : base(game)
        {
            MenuOptions = new List<CMenu.CMenuOption>()
            {
                new CMenu.CMenuOption() { Text = "SFX Volume", Axis = SFXVolumeAxis, AxisValidate = SFXVolumeAxisValidate, CustomRender = SFXVolumeAxisRender },
                new CMenu.CMenuOption() { Text = "Music Volume", Axis = MusicVolumeAxis, AxisValidate = MusicVolumeAxisValidate, CustomRender = MusicVolumeAxisRender },
                //new CMenu.CMenuOption() { Text = "Language", Axis = LanguageAxis },
                new CMenu.CMenuOption() { Text = "Back", Select = Back, CancelOption = true, PanelType = CMenu.PanelType.Small },
            };

            OnBack = () => { };
        }

        public void SFXVolumeAxis(object tag, int axis)
        {
            float volume = (float)axis / 10.0f;
            CAudio.SetSFXVolume(volume);
        }

        public bool SFXVolumeAxisValidate(object tag, int axis)
        {
            return axis >= 0 && axis <= 10;
        }

        public void SFXVolumeAxisRender(object tag, SpriteBatch sprite_batch, Vector2 position)
        {
            int sfx_volume = (int)Math.Round(CAudio.GetSFXVolume() * 10.0f);
            DrawVolumeAxis(sprite_batch, position, sfx_volume);
        }

        public void MusicVolumeAxis(object tag, int axis)
        {
            float volume = (float)axis / 10.0f;
            CAudio.SetMusicVolume(volume);
        }

        public bool MusicVolumeAxisValidate(object tag, int axis)
        {
            return axis >= 0 && axis <= 10;
        }

        public void MusicVolumeAxisRender(object tag, SpriteBatch sprite_batch, Vector2 position)
        {
            int music_volume = (int)Math.Round(CAudio.GetMusicVolume() * 10.0f);
            DrawVolumeAxis(sprite_batch, position, music_volume);
        }

        public void DrawVolumeAxis(SpriteBatch sprite_batch, Vector2 position, int volume)
        {
            Vector2 base_ = position + new Vector2(30.0f, 48.0f);
            Vector2 step = new Vector2(20.0f, 0.0f);
            for (int i = 0; i < 10; ++i)
            {
                Vector2 p = base_ + step * i;
                Color c = volume > i ? Color.LightGray : Color.Gray;
                sprite_batch.Draw(Game.PixelTexture, new Rectangle((int)p.X, (int)p.Y, 16, 8), c);
            }
        }

        public void Back(object tag)
        {
            SProfile profile = CSaveData.GetCurrentProfile();
            profile.Options.SFXVolume = CAudio.GetSFXVolume();
            profile.Options.MusicVolume = CAudio.GetMusicVolume();
            CSaveData.SetCurrentProfileData(profile);
            CSaveData.SaveRequest();

            OnBack();
        }

        public void RefreshVolumes()
        {
            int sfx_volume = (int)Math.Round(CAudio.GetSFXVolume() * 10.0f);
            int music_volume = (int)Math.Round(CAudio.GetMusicVolume() * 10.0f);
            MenuOptions[0].AxisValue = sfx_volume;
            MenuOptions[1].AxisValue = music_volume;
        }
    }
}
