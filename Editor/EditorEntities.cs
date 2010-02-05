//
// EditorEntities.cs
//

using System;
using System.Drawing.Design;
using System.Globalization;
using System.ComponentModel;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    namespace Editor
    {
        using XnaColor = Microsoft.Xna.Framework.Graphics.Color;

        /// <summary>
        /// Default entity for unknown stage element type.
        /// </summary>
        [TypeConverter(typeof(Editor.CEntityConverter))]
        public class CUnknown
            : CEntity
        {
            public CUnknown(CWorld world, CStageElement stage_element)
                : base(world, "EditorEntity")
            {
                Physics = new CPhysics();
            }

            public override float GetRadius()
            {
                return 15.0f;
            }
        }

        /// <summary>
        /// Editor entity for CSpawnerEntity.
        /// </summary>
        [TypeConverter(typeof(CSpawnerEntityConverter))]
        public class CSpawnerEntity
            : CEntity
        {
            [TypeConverter(typeof(ExpandableObjectConverter))]
            public CStageElement StageElement { get; set; }
            public int StartTime { get; set; }

            public CSpawnerEntity(CWorld world, Galaxy.CSpawnerEntity element)
                : base(world, "EditorEntitySpawnEntity")
            {
                StageElement = element;
                Physics = new CPhysics();
                Visual = new CVisual(CContent.LoadTexture2D(world.Game, "Textures/Enemy/Turret"), XnaColor.White);
            }

            public override float GetRadius()
            {
                return 15.0f;
            }
        }
    }
}
