//
// Mover.cs
//

namespace Galaxy
{
    public abstract class CMover
    {
        public string Name { get; set; }
        public abstract void Move(CEntity entity);
    }
}

