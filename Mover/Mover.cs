//
// Mover.cs
//

namespace Galaxy
{
    public abstract class CMover
    {
        public bool Paused { get; set; }
        public string Name { get; set; }
        public abstract void Move(CEntity entity);
    }
}

