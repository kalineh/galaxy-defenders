//
// Glob.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Galaxy
{
    public class CGlob
        : CEnemy
    {
        private CShip ChaseTarget { get; set; }
        private CShip AttachTarget { get; set; }
        private int SelfDestructTimer { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);
            
            Physics = new CPhysics();
            Physics.AnglePhysics.Rotation = world.Random.NextAngle();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 32.0f);
            Visual = CVisual.MakeSpriteCached1(world, "Textures/Enemy/Glob");
            Visual.Depth = CLayers.Player + CLayers.SubLayerIncrement * 1.0f;
            HealthMax = 4.5f;
            SelfDestructTimer = 90;
        }

        public override void UpdateAI()
        {
            float scale_speed = AttachTarget == null ? 0.1f : 0.3f;
            float scale_size = AttachTarget == null ? 0.04f : 0.08f;
            Visual.Scale = Vector2.One + Vector2.One * (float)Math.Sin(World.Game.GameFrame * scale_speed) * scale_size;
            // TODO: not full recache!
            Visual.Update();

            if (ChaseTarget != null)
            {
                Mover = null;

                // TODO: this can become null suddenly -> this will be bugs
                if (ChaseTarget.IsDead)
                {
                    ChaseTarget = null;
                    AttachTarget = null;
                    Die();
                    return;
                }

                Vector2 target = ChaseTarget.Physics.PositionPhysics.Position;
                Vector2 offset = target - Physics.PositionPhysics.Position;
                Vector2 velocity = Physics.PositionPhysics.Velocity;
                float speed = Math.Max(velocity.Length(), 5.0f);
                Vector2 new_velocity = velocity + offset * 0.005f;
                Physics.PositionPhysics.Velocity = new_velocity.Normal() * speed;
                Physics.PositionPhysics.Position += Vector2.UnitY * -World.Game.StageDefinition.ScrollSpeed;
            }
            else if (AttachTarget != null)
            {
                Collision = null;

                // TODO: less bugs
                if (AttachTarget.IsDead)
                {
                    AttachTarget = null;
                    Die();
                    return;
                }

                Physics.PositionPhysics.Position = AttachTarget.Physics.PositionPhysics.Position;
                World.Stats.CollisionDamageReceived += 0.01f;
                AttachTarget.TakeDirectArmorDamage(0.01f);
                AttachTarget.Physics.PositionPhysics.Velocity *= 0.60f;
                Health += 0.5f;
                SelfDestructTimer -= 1;
                if (SelfDestructTimer <= 0)
                {
                    Die();
                    return;
                }
            }
            else if (AttachTarget == null && ChaseTarget == null)
            {
                CShip nearest = World.GetNearestShip(Physics.PositionPhysics.Position, 2000.0f);
                ChaseTarget = nearest;
            }
        }

        public new void OnCollide(CShip ship)
        {
            AttachTarget = ship;
            ChaseTarget = null;
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }
    }
}

