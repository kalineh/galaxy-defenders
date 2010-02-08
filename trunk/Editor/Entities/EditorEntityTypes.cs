//
// EditorEntityTypes.cs
//

using System;
using System.Drawing.Design;
using System.Globalization;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
        public static class CEditorEntityTypes
        {
            public static List<Type> EntityTypeList = new List<Type>() {
                typeof(CAsteroid),
                typeof(CBeard),
                typeof(CPewPew),
                typeof(CSinBall),
                typeof(CTurret),
                typeof(CSpacePlatform),
                typeof(CBigSpacePlatform),
                typeof(CBuilding),
            };

            public static List<string> ToNames()
            {
                List<string> results = new List<string>();

                foreach (Type type in EntityTypeList)
                {
                    results.Add(type.Name);
                }

                return results;
            }
        }
    }
}
