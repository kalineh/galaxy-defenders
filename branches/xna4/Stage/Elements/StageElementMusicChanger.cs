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
        private CMusicChanger Preloaded { get; set; }

        public override void Initialize(CWorld world)
        {
            CMusicChanger music_changer = new CMusicChanger();
            music_changer.Initialize(world);
            music_changer.MusicName = MusicName;
            Preloaded = music_changer;
        }

        public override void Update(CWorld world)
        {
            world.EntityAdd(Preloaded);
        }
    }
}
