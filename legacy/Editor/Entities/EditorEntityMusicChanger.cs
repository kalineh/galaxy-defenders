//
// EditorEntityMusicChanger.cs
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    /// <summary>
    /// Editor entity for CMusicChanger.
    /// </summary>
    [TypeConverter(typeof(CEditorConverterGenerated))]
    public class CEditorEntityMusicChanger
        : CEditorEntityBase
    {
        [CategoryAttribute("Music")]
        public string MusicName { get; set; }

        public CEditorEntityMusicChanger(CWorld world, Type type, Vector2 position)
            : base(world, position)
        {
            Visual = CVisual.MakeLabel(world.Game, "Music Changer", Color.Blue);
        }

        public CEditorEntityMusicChanger(CWorld world, Vector2 position)
            : this(world, typeof(CMusicChanger), position)
        {
        }

        public CEditorEntityMusicChanger(CWorld world, CStageElement element)
            : this(world, typeof(CMusicChanger), element.Position)
        {
            MusicName = ((CStageElementMusicChanger)element).MusicName;
        }

        public override CStageElement GenerateStageElement()
        {
            CStageElementMusicChanger result = new CStageElementMusicChanger()
            {
                Position = Position,
                MusicName = MusicName,
            };

            return result;
        }
    }
}
