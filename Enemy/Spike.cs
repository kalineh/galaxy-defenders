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
            Physics.Friction = 0.98f;
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Spike");
            Visual.TileX = 2;
            Visual.AnimationSpeed = 0.05f;
            HealthMax = 2.0f;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 28.0f);
            ScreenEdgeIgnoreTime = 60;
            Coins = 0;
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

            if (Physics.AngularVelocity > 0.0f)
                return; 

            Target = World.GetNearestShip(Physics.Position, 900.0f);
        }

        public void UpdateSeek()
        {
            Physics.AngularVelocity = MathHelper.Min(0.275f, Physics.AngularVelocity + 0.0075f);

            if (Physics.AngularVelocity > 0.25f)
            {
                Vector2 offset = Target.Physics.Position - Physics.Position;
                float length = offset.Length();
                const float limit = 900.0f;
                if (offset.Length() < limit)
                {
                    float t = 0.2f;
                    Vector2 force = offset.Normal() * t;
                    Physics.Velocity += force;
                }
            }
        }

        public void UpdateStop()
        {
            Physics.AngularVelocity = MathHelper.Max(0.0f, Physics.AngularVelocity - 0.01f);
            Physics.Position += CMoverPresets.AntiCamera * 0.9f;
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

            if (!World.GameCamera.IsInside(Physics.Position, 0.0f))
            {
                Vector2 to_center = World.GameCamera.GetCenter().ToVector2() - Physics.Position;
                Physics.Velocity = to_center.Normal() * Physics.Velocity.Length();
                Physics.Position -= CMoverPresets.AntiCamera;
                Physics.Position = World.GameCamera.ClampInside(Physics.Position, 0.0f);
                ScreenEdgeHitCount += 1;
                ScreenEdgeIgnoreTime = 6;
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        public new void OnCollide(CShip ship)
        {
            base.OnCollide(ship);

            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyOrangeColor, 1.25f, Physics.Velocity * 0.25f);
            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyGrayColor, 1.25f, Physics.Velocity * 0.25f);

            ship.TakeDamage(Physics.Position, Physics.Velocity, 7.5f);

            Die();
        }
    }
}
