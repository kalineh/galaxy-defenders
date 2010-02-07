//
// EditorUnknownEntity.cs
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
            [CategoryAttribute("Internal")]
            [TypeConverter(typeof(ExpandableObjectConverter))]
            public CStageElement StageElement { get; set; }

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
    }
}
