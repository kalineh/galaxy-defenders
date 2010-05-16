//
// Mover.cs
//

namespace Galaxy
{
    public abstract class CMover
    {
        public float SpeedMultiplier { get; set; }
        public bool Paused { get; set; }
        public string Name { get; set; }
        public abstract void Move(CEntity entity);

        public CMover()
        {
            SpeedMultiplier = 1.0f;
        }
    }
}

