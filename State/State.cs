//
// State.cs
//

using System;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public abstract class CState
    {
        public abstract void Update();
        public abstract void Draw();
    }
}
