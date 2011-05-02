//
// State.cs
//


namespace Galaxy
{
    public abstract class CState
    {
        public virtual void OnEnter() {}
        public virtual void OnExit() {}
        public abstract void Update();
        public abstract void Draw();
    }
}
