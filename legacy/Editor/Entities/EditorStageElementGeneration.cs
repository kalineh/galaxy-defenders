//
// EditorStageElementGeneration.cs
//

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    /// <summary>
    /// Generate CStageElements at runtime to support adding items from editor.
    /// </summary>
    public class CEditorStageElementGeneration
    {
        public virtual CStageElement GenerateStageElement()
        {
            throw new NotImplementedException();
        }
    }
}
