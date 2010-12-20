//
// StageElementMusicChanger.cs
//

using Microsoft.Xna.Framework;
using System.ComponentModel;
using System;
using System.Linq;

namespace Galaxy
{
    public class CStageElementMusicChanger
        : CStageElement
    {
        public string MusicName { get; set; }

        public override void Update(CWorld world)
        {
            CMusicChanger music_changer = new CMusicChanger();
            music_changer.Initialize(world);
            music_changer.MusicName = MusicName;
            world.EntityAdd(music_changer);
        }
    }
}
