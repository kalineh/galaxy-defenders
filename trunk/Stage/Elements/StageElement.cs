//
// StageElement.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public abstract class CStageElement
    {
        public Vector2 Position { get; set; }

        public abstract void Update(CWorld world);
        public abstract bool IsExpired();
    };
}


