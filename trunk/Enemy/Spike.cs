//
// Spike.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CSpike
        : CEnemy
    {
        private CShip Target { get; set; }
        private int ScreenEdgeHitCount { get; set; }
        private int ScreenEdgeIgnoreTime { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Physics.PositionPhysics.Friction = 0.98f;
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Spike");
            Visual.TileX = 2;
            Visual.AnimationSpeed = 0.05f;
            HealthMax = 3.5f;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 28.0f);
        }

        public override void UpdateAI()
        {
            Mover = null;
            UpdateTarget();

            if (Target == null)
                UpdateStop();
            else
                UpdateSeek();

            UpdateScreenEdgeHit();
        }

        public void UpdateTarget()
        {
            if (Target != null)
            {
                if (Target.IsDead)
                    Target = null;
            }

            if (Target != null)
                return;

            if (Physics.AnglePhysics.AngularVelocity > 0.0f)
                return; 

            Target = World.GetNearestShip(Physics.PositionPhysics.Position, 900.0f);
        }

        public void UpdateSeek()
        {
            Physics.AnglePhysics.AngularVelocity = MathHelper.Min(0.275f, Physics.AnglePhysics.AngularVelocity + 0.0075f);

            if (Physics.AnglePhysics.AngularVelocity > 0.25f)
            {
                Vector2 offset = Target.Physics.PositionPhysics.Position - Physics.PositionPhysics.Position;
                float length = offset.Length();
                const float limit = 900.0f;
                if (offset.Length() < limit)
                {
                    float t = 0.2f;
                    Vector2 force = offset.Normal() * t;
                    Physics.PositionPhysics.Velocity += force;
                }
            }
        }

        public void UpdateStop()
        {
            Physics.AnglePhysics.AngularVelocity = MathHelper.Max(0.0f, Physics.AnglePhysics.AngularVelocity - 0.01f);
            Physics.PositionPhysics.Position += CMoverPresets.AntiCamera * 0.9f;
        }

        public void UpdateScreenEdgeHit()
        {
            if (ScreenEdgeHitCount > 5)
                return;

            if (ScreenEdgeIgnoreTime > 0)
            {
                ScreenEdgeIgnoreTime -= 1;
                return;
            }

            if (!World.GameCamera.IsInside(Physics.PositionPhysics.Position, 0.0f))
            {
                Vector2 to_center = World.GameCamera.GetCenter().ToVector2() - Physics.PositionPhysics.Position;
                Physics.PositionPhysics.Velocity = to_center.Normal() * Physics.PositionPhysics.Velocity.Length();
                Physics.PositionPhysics.Position -= CMoverPresets.AntiCamera;
                Physics.PositionPhysics.Position = World.GameCamera.ClampInside(Physics.PositionPhysics.Position, 0.0f);
                ScreenEdgeHitCount += 1;
                ScreenEdgeIgnoreTime = 6;
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }

        public new void OnCollide(CShip ship)
        {
            base.OnCollide(ship);
            Physics.PositionPhysics.Velocity = -Physics.PositionPhysics.Velocity;
            Physics.PositionPhysics.Velocity *= 0.5f;
            Target = null;
        }

    }
}
