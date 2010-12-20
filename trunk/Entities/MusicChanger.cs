//
// MusicChanger.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CMusicChanger
        : CEntity
    {
        public string MusicName { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);
        }

        public override void Update()
        {
            CAudio.PlayMusic(MusicName);
            Delete();
        }
    }
}
