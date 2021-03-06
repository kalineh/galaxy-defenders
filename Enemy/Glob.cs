﻿//
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
        private float BurstOffset { get; set; }
        private bool IsGripped { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);
            
            Physics = new CPhysics();
            Physics.Rotation = world.Random.NextAngle();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 32.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Glob");
            Visual.Depth = CLayers.Player + CLayers.SubLayerIncrement * 1.0f;
            HealthMax = 3.5f;
            SelfDestructTimer = 90;
            Coins = 0;
            BurstOffset = World.Random.NextAngle();
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

                Vector2 target = ChaseTarget.Physics.Position;
                Vector2 offset = target - Physics.Position;
                Vector2 velocity = Physics.Velocity;
                float burst = (float)Math.Sin(World.Game.GameFrame * 0.075f + BurstOffset);
                float speed = 3.5f + burst * 3.0f;
                Vector2 new_velocity = velocity + offset * 0.0075f;
                Physics.Velocity = new_velocity.Normal() * speed;
                Physics.Position += Vector2.UnitY * -World.ScrollSpeed;
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

                Vector2 offset = Physics.Position - AttachTarget.Physics.Position;
                if (offset.LengthSquared() < 12.0f * 12.0f)
                    IsGripped = true;

                if (IsGripped)
                    Physics.Position = AttachTarget.Physics.Position;
                else
                    Physics.Position = Vector2.Lerp(Physics.Position, AttachTarget.Physics.Position, 0.35f);

                World.Stats.CollisionDamageReceived += 0.05f;
                AttachTarget.TakeDirectArmorDamage(0.05f);
                AttachTarget.Physics.Velocity *= 0.60f;
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
                CShip nearest = World.GetNearestShip(Physics.Position, 2000.0f);
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
            circle.Position = Physics.Position;
        }
    }
}

