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
        public abstract void Update(CWorld world);
        public abstract bool IsExpired();
    };
}


