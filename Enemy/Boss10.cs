//
// Boss10.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Galaxy
{
    public class CBoss10Ball
        : CEnemy
    {
        public int BlockHitCountdown { get; set; }
        public int Powered { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 31.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss10Ball");
            Visual.Depth = CLayers.Entity;
            HealthMax = 1000.0f;
            Coins = 0;
            BaseScore = 0;

            CanSeekerTarget = false;
        }

        protected override void OnDie()
        {
            base.OnDie();
        }

        public override void Update()
        {
            Vector2 old = Physics.Position;
            base.Update();
            BlockHitCountdown = Math.Max(BlockHitCountdown - 1, 0);
            Powered = Math.Max(Powered - 1, 0);

            const float buffer = 31.0f;
            Vector2 position = Physics.Position;
            Vector2 velocity = Physics.Velocity;
            Vector2 tl = World.GameCamera.GetTopLeft();
            Vector2 br = World.GameCamera.GetBottomRight();

            if (position.X <= tl.X + buffer)
            {
                position.X = Math.Max(tl.X + buffer, position.X);
                velocity = Vector2.Reflect(velocity, Vector2.UnitX);
                position += velocity;
            }

            if (position.X >= br.X - buffer)
            {
                position.X = Math.Min(br.X - buffer, position.X);
                velocity = Vector2.Reflect(velocity, -Vector2.UnitX);
                position += velocity;
            }

            if (position.Y <= tl.Y + buffer)
            {
                position.Y = Math.Max(tl.Y + buffer, position.Y);
                velocity = Vector2.Reflect(velocity, Vector2.UnitY);
                position += velocity;
            }

            if (position.Y >= br.Y - buffer)
            {
                position.Y = Math.Min(br.Y - buffer, position.Y);
                velocity = Vector2.Reflect(velocity, -Vector2.UnitY);
                position += velocity;
            }

            Physics.Position = position;
            Physics.Velocity = velocity;

            if (Powered > 0)
            {
                World.ParticleEffects.Spawn(EParticleType.Boss10BallTrail, Physics.Position, CEnemy.EnemyGrayColor, 1.0f, null);
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle collision = Collision as CollisionCircle;
            collision.Position = Physics.Position;
        }

        public new void OnCollide(CShip ship)
        {
            // avoid near multiple collision
            if (Powered > 6000 - 60)
                return;

            Vector2 offset = Physics.Position - ship.Physics.Position;
            Physics.Velocity = Vector2.Reflect(Physics.Velocity, Vector2.UnitY);

            if (offset.X < 0.0f)
                Physics.Velocity = new Vector2(Math.Abs(Physics.Velocity.X) * -1.0f, -Math.Abs(Physics.Velocity.Y));
            else
                Physics.Velocity = new Vector2(Math.Abs(Physics.Velocity.X) * +1.0f, -Math.Abs(Physics.Velocity.Y));

            Physics.Velocity = Physics.Velocity.Normal() * 8.0f;
            Physics.Position += Physics.Velocity;
            Powered = 6000; // infinite~ is ok

            ship.Physics.Velocity += offset * -0.5f;
        }

        public void OnCollide(CBoss10Block block)
        {
            if (BlockHitCountdown > 0)
                return;

            if (!block.Collision.Enabled)
                return;

            Vector2 offset = Physics.Position - block.Physics.Position;
            bool is_x = Math.Abs(offset.X) > Math.Abs(offset.Y);
            Vector2 normal = new Vector2(
                (is_x ? 1.0f : 0.0f) * Math.Sign(offset.X),
                (is_x ? 0.0f : 1.0f) * Math.Sign(offset.Y)
            );

            float dot = Vector2.Dot(Physics.Velocity, normal);
            if (dot > 0.0f)
                return;

            Physics.Velocity = Vector2.Reflect(Physics.Velocity, normal);
            Physics.Position += Physics.Velocity;
            Physics.Velocity = Physics.Velocity.Normal() * 6.0f;

            if (Powered <= 0)
                return;

            Powered = 0;
            block.FadeOut();
            BlockHitCountdown = 0;
        }
    }

    public class CBoss10Block
        : CEnemy
    {
        public bool Dying { get; set; }
        public int Spawning { get; set; }
        public int RespawnTimer { get; set; }
        public int FadeTimer { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            HealthMax = 100000.0f;
            Coins = 0;
            BaseScore = 0;

            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(110.0f, 89.0f));
            Collision.Enabled = false;
            Visual = CVisual.MakeSpriteUncached(world.Game, "Textures/Enemy/Boss10Block");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -2.0f;
            Visual.Scale = Vector2.Zero;

            FadeTimer = 60;
        }

        public override void Update()
        {
            base.Update();

            if (FadeTimer > 0)
            {
                FadeTimer -= 1;
                Visual.Scale += new Vector2(1.0f / 60.0f);

                if (FadeTimer == 0)
                    Collision.Enabled = true;
            }
            else if (FadeTimer < 0)
            {
                FadeTimer += 1;

                if (FadeTimer > -60)
                {
                    Visual.Scale += new Vector2(1.0f / 60.0f);
                }
                else
                {
                    Visual.Scale -= new Vector2(1.0f / 60.0f);
                    Visual.Scale = Vector2.Max(Visual.Scale, Vector2.Zero);
                }

                if (FadeTimer == 0)
                {
                    Collision.Enabled = true;
                    Visual.Scale = Vector2.One;
                }
            }
        }

        public void FadeOut()
        {
            Collision.Enabled = false;
            FadeTimer = 60 * -25;
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB collision = Collision as CollisionAABB;
            collision.Position = Physics.Position - new Vector2(56.0f, 43.0f);
        }

    }

    public class CBoss10
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss9Bar Bar { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 60.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss10");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -0.0f;
            HealthMax = 60.0f * CDifficulty.BossHealthScale[world.CachedDifficulty];
            Coins = 0;
            BaseScore = 0;

            FiberManager = new CFiberManager();
            FiberManager.Fork(this.UpdateWeapons0);
            FiberManager.Fork(this.UpdateWeapons1);
            FiberManager.Fork(this.UpdateMovement);
        }

        public override void Update()
        {
            if (AliveTime == 0)
                MakeChildren();

            if (AliveTime == 60)
            {
                for (int i = 0; i < 2; ++i)
                {
                    CBoss10Ball ball = new CBoss10Ball();
                    ball.Initialize(World);
                    ball.Physics.Position = new Vector2(0.0f, Physics.Position.Y + 400.0f);
                    ball.Physics.Velocity = new Vector2(i % 2 == 0 ? -8.0f : 8.0f, 8.0f);
                    ball.Physics.Velocity.Normalize();
                    World.EntityAdd(ball);
                }
            }

            base.Update();
            FiberManager.Update();
        }

        public void MakeChildren()
        {
            for (int j = 0; j < 3; ++j)
            {
                for (int i = 0; i < 7; ++i)
                {
                    CBoss10Block block = new CBoss10Block();
                    block.Initialize(World);
                    World.EntityAdd(block);

                    block.Physics.Position =
                        new Vector2(-360.0f, Physics.Position.Y + 110.0f) +
                        new Vector2(i * 112.0f, j * 92.0f);
                }
            }
        }

        protected override void OnDie()
        {
            FiberManager.KillAll();
            World.DestroyAllProjectiles();
            World.DestroyAllEnemies();

            for (int i = 0; i < 14; ++i)
            {
                World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position + World.Random.NextVector2() * World.Random.NextSignedFloat() * 240.0f, CEnemy.EnemyOrangeColor, 1.5f, null);
                World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position + World.Random.NextVector2() * World.Random.NextSignedFloat() * 240.0f, CEnemy.EnemyGrayColor, 1.5f, null);
            }
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle collision = Collision as CollisionCircle;
            collision.Position = Physics.Position;
        }

        private IEnumerable UpdateWeapons0()
        {
            yield return 60;

            while (true)
            {
                yield return 90;

                int attack_type = World.Random.Next() % 3;

                switch (attack_type)
                {
                    case 0:
                        for (int i = 0; i < 14; ++i)
                        {
                            int index = i % 2;
                            Fire(0, -0.4f + 0.1f * i + MathHelper.PiOver2);
                            Fire(1, +0.4f - 0.1f * i + MathHelper.PiOver2);
                            yield return 3;
                        }
                        
                        break;

                    case 1:
                    {
                        float angle = GetDirToShip().ToAngle();
                        for (int i = 0; i < 8; ++i)
                        {
                            Fire(0, angle - 0.2f);
                            Fire(1, angle + 0.2f);
                            yield return 4;
                        }
                        break;
                    }
                }
            }
        }

        private IEnumerable UpdateWeapons1()
        {
            yield return 180;

            while (true)
            {
                yield return 120 + World.Random.Next() % 30;

                int attack_type = World.Random.Next() % 3;

                switch (attack_type)
                {
                    case 0:
                        for (int i = 0; i < 3; ++i)
                        {
                            FireLaser();
                            yield return 10;
                        }
                        break;

                    case 1:
                        for (int i = 0; i < 6; ++i)
                        {
                            FireLaser();
                            yield return 16;
                        }
                        break;
                }
            }
        }

        private IEnumerable UpdateMovement()
        {
            Vector2 base_ = Physics.Position - new Vector2(Physics.Position.X % 112.0f, 0.0f);
            Physics.Position = base_ - new Vector2(22.0f, 0.0f);

            while (true)
            {
                float x = (float)Math.Sin(AliveTime * 0.01f) * 200.0f;
                Vector2 target = new Vector2(x - x % 112.0f - 22.0f, 0.0f);

                for (int i = 0; i < 60; ++i)
                {
                    Physics.Position = Vector2.Lerp(Physics.Position, base_ + target, 0.15f);
                    yield return 0;
                }

                yield return 30 + World.Random.Next() % 30;
            }
        }

        static Vector2[] Cannons = new Vector2[]
        {
            new Vector2(-16.0f, 0.0f),   
            new Vector2(+16.0f, 0.0f),   
        };

        private void Fire(int index, float angle)
        {
            Vector2 position = Physics.Position + Cannons[index];
            CEnemyShot shot = CEnemyShot.Spawn(World, position, angle, 8, 3.0f);
            CAudio.PlaySound("EnemyShoot");
        }
        
        private void FireLaser()
        {
            Vector2 position = Physics.Position + new Vector2(0.0f, 64.0f);
            CEnemyLaser.Spawn(World, position, MathHelper.PiOver2, 8.0f, 2.0f);
            CAudio.PlaySound("EnemyCannonShoot");
        }
        
    }
}
            