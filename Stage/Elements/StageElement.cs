﻿//
// StageElement.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public abstract class CStageElement
    {
        public Vector2 Position { get; set; }

        public abstract void Initialize(CWorld world);
        public abstract void Update(CWorld world);

        public virtual bool IsExpired()
        {
            return true;
        }
    };
}


