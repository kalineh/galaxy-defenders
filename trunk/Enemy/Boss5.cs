//
// Boss5.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Galaxy
{
    public class CBoss5Chunk
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss5 Boss { get; set; }
        public Vector2 CollisionOffset { get; set; }
        public Vector2 FireOffset { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 36.0f);
            Visual = CVisual.MakeSpriteUncached(world.Game, "Textures/Enemy/Boss5Chunk");
            HealthMax = 40.0f;
            Coins = 0;
            BaseScore = 0;
            FiberManager = new CFiberManager();
        }

        public override void Update()
        {
            base.Update();
            FiberManager.Update();
        }

        public IEnumerable UpdateWeapons2()
        {
            while (true)
            {
                int Steps = 7;
                float Range = 0.3f;
                float Sign = World.Random.NextSign();
                Vector2 dir = Vector2.UnitY.Rotate(World.Random.NextSignedFloat() * 0.25f);
                for (int i = 0; i < Steps; ++i)
                {
                    RegularShot(Physics.Position + FireOffset, Vector2.UnitY.Rotate((Range * -0.5f + Range / (float)Steps * i) * Sign));
                    yield return 6;
                }

                yield return 90;
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle collision = Collision as CollisionCircle;
            collision.Position = Physics.Position;
        }

        public void Activate()
        {
            FiberManager.Fork(this.UpdateMovement);
            FiberManager.Fork(this.UpdateWeapons);
        }

        public IEnumerable UpdateMovement()
        {
            // move out
            // random move up, down * random sign
            // 

            yield return 30;

            float dir = Physics.Position.X < Boss.Physics.Position.X ? -1.0f : 1.0f;
            Vector2 src = Physics.Position;
            Vector2 dst = src + Vector2.UnitX * 300.0f * dir;
            Vector2 move = dst - src;
            for (int i = 0; i < 120; ++i)
            {
                Physics.Position = src + move * MathHelper.Lerp(0.0f, 1.0f, 1.0f / 120.0f * i);
                yield return 0;
            }

            yield return 60;

            while (true)
            {
                float sign = World.Random.NextSign();
                src = Physics.Position;
                dst = src + Vector2.UnitY * 200.0f * sign;
                move = dst - src;
                for (int i = 0; i < 120; ++i)
                {
                    Physics.Position = src + move * MathHelper.Lerp(0.0f, 1.0f, 1.0f / 120.0f * i);
                    yield return 0;
                }

                yield return 60 + World.Random.Next() % 30;

                src = Physics.Position;
                dst = src + Vector2.UnitY * 200.0f * -sign;
                move = dst - src;
                for (int i = 0; i < 120; ++i)
                {
                    Physics.Position = src + move * MathHelper.Lerp(0.0f, 1.0f, 1.0f / 120.0f * i);
                    yield return 0;
                }

                yield return 60;
            }
        }

        public IEnumerable UpdateWeapons()
        {
            yield return 180;

            while (true)
            {
                yield return 60 + World.Random.Next() % 30;

                switch (World.Random.Next() % 2)
                {
                    case 0:
                        Vector2 dir = GetDirToShip();
                        RegularShot(Physics.Position, dir);
                        yield return 4;
                        RegularShot(Physics.Position, dir.Rotate(-0.04f));
                        RegularShot(Physics.Position, dir.Rotate(+0.04f));
                        yield return 4;
                        RegularShot(Physics.Position, dir.Rotate(-0.06f));
                        RegularShot(Physics.Position, dir);
                        RegularShot(Physics.Position, dir.Rotate(+0.06f));
                        yield return 4;
                        break;

                    case 1:
                        RegularShot(Physics.Position, GetDirToShip());
                        yield return 4;
                        RegularShot(Physics.Position, GetDirToShip());
                        yield return 4;
                        RegularShot(Physics.Position, GetDirToShip());
                        yield return 4;
                        break;
                }
            }
        }

        private void RegularShot(Vector2 position, Vector2 direction)
        {
            float rotation = direction.ToAngle();
            CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, 8.0f, 2.0f);
            CAudio.PlaySound("EnemyShoot");
        }

    }

    public class CBoss5
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss5Chunk Left { get; set; }
        public CBoss5Chunk Right { get; set; }
        public float LeftDamage { get; set; }
        public float RightDamage { get; set; }
        public CVisual Cover { get; set; }
        public float CoverRotation { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(170.0f, 170.0f));
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss5");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            Cover = CVisual.MakeSpriteUncached(world.Game, "Textures/Enemy/Boss5Cover");
            Cover.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * 2.0f;
            HealthMax = 120.0f * CDifficulty.BossHealthScale[world.CachedDifficulty];
            Coins = 0;
            BaseScore = 0;
            CoverRotation = MathHelper.PiOver2;

            FiberManager = new CFiberManager();
            FiberManager.Fork(this.UpdateWeapons4);
        }

        public override void Update()
        {
            base.Update();
            FiberManager.Update();

            if (AliveTime == 1)
                MakeChildren();

            IsSeekerTarget = true;
        }

        private void MakeChildren()
        {
            Left = new CBoss5Chunk() { Boss = this };
            Right = new CBoss5Chunk() { Boss = this };

            Left.Initialize(World);
            Right.Initialize(World);

            Left.Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -2.0f;
            Right.Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -2.0f;

            Left.Physics.Position = Physics.Position + Vector2.UnitX * -80.0f;
            Right.Physics.Position = Physics.Position + Vector2.UnitX * +80.0f;

            World.EntityAdd(Left);
            World.EntityAdd(Right);
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
            Cover.Draw(sprite_batch, Physics.Position, CoverRotation);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB collision = Collision as CollisionAABB;
            collision.Position = Physics.Position + new Vector2(-85.0f, -85.0f);
        }

        public override void TakeDamage(float damage, CShip source)
        {
            if (HealthReducedBelow(0.8f, damage))
            {
                Left.Activate();
                Right.Activate();
            }
            else if (HealthReducedBelow(0.5f, damage))
            {
                FiberManager.Fork(this.RotateCover);    
                FiberManager.Kill(this.UpdateWeapons4);    
                FiberManager.Fork(this.UpdateWeapons6);    
            }

            base.TakeDamage(damage, source);
        }

        private bool HealthReducedBelow(float threshold, float damage)
        {
            float Threshold = HealthMax * threshold;
            float before = Health;
            float after = Health - damage;
            return before >= Threshold && after < Threshold;
        }

        private IEnumerable UpdateWeapons4()
        {
            yield return 60;

            Vector2[] cannons = {
                new Vector2(-54.0f, +50.0f),
                new Vector2(+49.0f, +50.0f),
                new Vector2(-54.0f, -53.0f),
                new Vector2(+49.0f, -53.0f),
            };

            while (true)
            {
                yield return 60;

                switch (World.Random.Next() % 2)
                {
                    case 0:
                        RegularShot(Physics.Position + cannons[0], GetDirToShip());
                        RegularShot(Physics.Position + cannons[1], GetDirToShip());
                        RegularShot(Physics.Position + cannons[2], GetDirToShip());
                        RegularShot(Physics.Position + cannons[3], GetDirToShip());
                        break;

                    case 1:
                        Vector2 dir = GetDirToShip();
                        for (int i = 0; i < 18; ++i)
                        {
                            RegularShot(Physics.Position + cannons[i % 4], GetDirToShip());
                            yield return 3;
                        }

                        break;
                }
            }
        }

        private IEnumerable UpdateWeapons6()
        {
            yield return 120;

            Vector2[] cannons = {
                new Vector2(-54.0f, +50.0f),
                new Vector2(-3.0f, +50.0f),
                new Vector2(+49.0f, +50.0f),
                new Vector2(-54.0f, -53.0f),
                new Vector2(-3.0f, -53.0f),
                new Vector2(+49.0f, -53.0f),
            };

            while (true)
            {
                yield return 30;

                switch (World.Random.Next() % 2)
                {
                    case 0:
                        for (int i = 0; i < 4 + World.Random.Next() % 4; ++i)
                        {
                            RegularShot(Physics.Position + cannons[0], GetDirToShip());
                            RegularShot(Physics.Position + cannons[1], GetDirToShip());
                            RegularShot(Physics.Position + cannons[2], GetDirToShip());
                            RegularShot(Physics.Position + cannons[3], GetDirToShip());
                            RegularShot(Physics.Position + cannons[4], GetDirToShip());
                            RegularShot(Physics.Position + cannons[5], GetDirToShip());

                            yield return 30;
                        }
                        break;

                    case 1:
                        RegularShot(Physics.Position + cannons[0], GetDirToShip());
                        RegularShot(Physics.Position + cannons[1], GetDirToShip());
                        RegularShot(Physics.Position + cannons[2], GetDirToShip());
                        RegularShot(Physics.Position + cannons[3], GetDirToShip());
                        RegularShot(Physics.Position + cannons[4], GetDirToShip());
                        RegularShot(Physics.Position + cannons[5], GetDirToShip());
                        break;
                }
            }
        }

        private IEnumerable RotateCover()
        {
            for (int i = 0; i < 120; ++i)
            {
                CoverRotation += MathHelper.PiOver2 / 120.0f;
                CoverRotation = MathHelper.WrapAngle(CoverRotation);
                yield return 0;
            }
        }

        private void RegularShot(Vector2 position, Vector2 direction)
        {
            float rotation = direction.ToAngle();
            CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, 8.0f, 2.0f);
            CAudio.PlaySound("EnemyShoot");
        }

        private void PelletShot(Vector2 position, Vector2 direction)
        {
            float rotation = direction.ToAngle();
            CEnemyPellet pellet = CEnemyPellet.Spawn(World, position, rotation, 5.0f, 2.0f);
            CAudio.PlaySound("EnemyShoot");
        }
    }
}

