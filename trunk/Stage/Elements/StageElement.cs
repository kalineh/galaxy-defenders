//
// StageElement.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public abstract class CStageElement
    {
        public Vector2 Position { get; set; }

        public abstract void Update(CWorld world);
        public abstract bool IsExpired();
    };
}


