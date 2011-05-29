//
// Boss8.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Galaxy
{
    public class CBoss8Collision
        : CEnemy
    {
        public CBoss8 Boss { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 75.0f);
            HealthMax = 1000.0f;
            Coins = 0;
            BaseScore = 0;
        }

        public override void TakeDamage(float damage, CShip source)
        {
            Boss.TakeDamage(damage, source);
        }

        public override void UpdateCollision()
        {
            // handled in boss
        }
    }

    public class CBoss8Pusher
        : CEnemy
    {
        public bool Active { get; set; }
        public CBoss8 Boss { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);
        }

        public override void UpdateCollision()
        {
            // handled in boss
        }

        public override void Update()
        {
            base.Update();

            if (!Active)
                return;

            for (int i = 0; i < World.Game.PlayersInGame; ++i)
            {
                CShip ship = World.Ships[i];
                if (ship == null || ship.IsDead)
                    continue;

                Vector2 dir = (ship.Physics.Position - Boss.Physics.Position).Normal();
                ship.Physics.Velocity += dir * 1.0f;

                Vector2 posl = Boss.Physics.Position + new Vector2(-116.0f, 12.0f);
                Vector2 posr = Boss.Physics.Position + new Vector2(+116.0f, 12.0f);
                World.ParticleEffects.Spawn(EParticleType.Boss8Wind, posl, CEnemy.EnemyOrangeColor, 1.0f, dir.Rotate(World.Random.NextSignedFloat() * 0.2f) * 18.0f);
                World.ParticleEffects.Spawn(EParticleType.Boss8Wind, posr, CEnemy.EnemyOrangeColor, 1.0f, dir.Rotate(World.Random.NextSignedFloat() * 0.2f) * 18.0f);
            }
        }
    }

    public class CBoss8
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public List<CBoss8Collision> Collisions { get; set; }
        public CBoss8Pusher Pusher { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(360.0f, 170.0f));
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss8");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            HealthMax = 180.0f * CDifficulty.BossHealthScale[world.CachedDifficulty];
            Coins = 0;
            BaseScore = 0;

            FiberManager = new CFiberManager();
            FiberManager.Fork(this.UpdateWeapons0);
            FiberManager.Fork(this.UpdateMovement);
        }

        public override void Update()
        {
            if (AliveTime == 0)
                MakeChildren();

            base.Update();
            FiberManager.Update();
        }

        public void MakeChildren()
        {
            //Collisions = new List<CBoss8Collision>()
            //{
                //new CBoss8Collision(),    
                //new CBoss8Collision(),    
                //new CBoss8Collision(),    
                //new CBoss8Collision(),    
                //new CBoss8Collision(),    
            //};

            //foreach (CBoss8Collision c in Collisions)
            //{
                //c.Initialize(World);
                //c.Boss = this;
                //World.EntityAdd(c);
            //}

            ////UpdateCollision();

            Pusher = new CBoss8Pusher();
            Pusher.Initialize(World);
            Pusher.Boss = this;
            World.EntityAdd(Pusher);
        }

        public override void TakeDamage(float damage, CShip source)
        {
            if (Health > HealthMax * 0.5f && (Health - damage) <= HealthMax * 0.5f)
            {
                FiberManager.Kill(this.UpdateWeapons0);
                FiberManager.Fork(this.UpdateWeapons1);
                FiberManager.Fork(this.PhaseRotate);
                FiberManager.Fork(this.Blowing);
            }
            base.TakeDamage(damage, source);
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
            CollisionAABB collision = Collision as CollisionAABB;
            collision.Position = Physics.Position + new Vector2(-180.0f, -80.0f);

            //(Collisions[0].Collision as CollisionCircle).Position = Physics.Position + new Vector2(0.0f, 0.0f);
            //(Collisions[1].Collision as CollisionCircle).Position = Physics.Position + new Vector2(0.0f, 0.0f);
            //(Collisions[2].Collision as CollisionCircle).Position = Physics.Position + new Vector2(0.0f, 0.0f);
            //(Collisions[3].Collision as CollisionCircle).Position = Physics.Position + new Vector2(0.0f, 0.0f);
            //(Collisions[4].Collision as CollisionCircle).Position = Physics.Position + new Vector2(0.0f, 0.0f);
        }

        private IEnumerable UpdateWeapons0()
        {
            yield return 60;

            while (true)
            {
                yield return 60;

                int attack_type = World.Random.Next() % 3;

                switch (attack_type)
                {
                    case 0:
                        for (int i = 0; i < 4; ++i)
                        {
                            FireLowerCannon(0, Vector2.UnitY);
                            FireLowerCannon(1, Vector2.UnitY);
                            FireLowerCannon(2, Vector2.UnitY);
                            yield return 8;
                            FireLowerCannon(3, Vector2.UnitY);
                            FireLowerCannon(4, Vector2.UnitY);
                            FireLowerCannon(5, Vector2.UnitY);
                            yield return 8;
                        }
                        break;

                    case 1:
                        for (int j = 0; j < 3; ++j)
                        {
                            Vector2 dir = GetDirToShip();
                            for (int i = 0; i < 6; ++i)
                            {
                                FireLowerCannon(i % 3, dir);
                                yield return 3;
                            }

                            for (int i = 0; i < 6; ++i)
                            {
                                FireLowerCannon(i % 3 + 3, dir);
                                yield return 3;
                            }
                        }
                        break;

                    case 2:
                    {
                        Vector2 dir = GetDirToShip();
                        for (int i = 0; i < 18; ++i)
                        {
                            FireLowerCannon(i % 3, dir.Rotate(-0.3f + 0.6f / 12.0f * (float)i));
                            FireLowerCannon(5 - i % 3, dir.Rotate(0.3f - 0.6f / 12.0f * (float)i));
                            yield return 1;
                        }
                        break;
                    }
                }
            }
        }

        private IEnumerable UpdateWeapons1()
        {
            // extra wait for rotate to finish
            yield return 140;

            while (true)
            {
                yield return 30;

                int attack_type = World.Random.Next() % 4;

                switch (attack_type)
                {
                    case 0:
                        for (int i = 0; i < 6; ++i)
                        {
                            FireUpperCannon(0, Vector2.UnitY);
                            FireUpperCannon(1, Vector2.UnitY);
                            FireUpperCannon(2, Vector2.UnitY);
                            yield return 2;
                            FireUpperCannon(3, Vector2.UnitY);
                            FireUpperCannon(4, Vector2.UnitY);
                            FireUpperCannon(5, Vector2.UnitY);
                            yield return 2;
                        }
                        break;

                    case 1:
                        for (int j = 0; j < 8; ++j)
                        {
                            Vector2 dir = GetDirToShip();
                            for (int i = 0; i < 6; ++i)
                            {
                                FireUpperCannon(i % 3, dir);
                                yield return 5;
                            }

                            for (int i = 0; i < 6; ++i)
                            {
                                FireUpperCannon(i % 3 + 3, dir);
                                yield return 5;
                            }
                        }
                        break;

                    case 2:
                    {
                        Vector2 dir = GetDirToShip();
                        for (int i = 0; i < 24; ++i)
                        {
                            FireUpperCannon(i % 3, dir.Rotate(-0.3f + 0.6f / 12.0f * (float)i));
                            FireUpperCannon(5 - i % 3, dir.Rotate(0.3f - 0.6f / 12.0f * (float)i));
                            yield return 1;
                        }
                        break;
                    }

                    case 3:
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            Vector2 dir = GetDirToShip();
                            for (int i = 0; i < 18; ++i)
                            {
                                FireUpperCannon(i % 3, dir.Rotate(-0.3f + 0.6f / 12.0f * (float)i));
                                FireUpperCannon(5 - i % 3, dir.Rotate(0.3f - 0.6f / 12.0f * (float)i));
                                yield return 1;
                            }
                        }
                        break;
                    }
                }
            }
        }

        private IEnumerable UpdateMovement()
        {
            Vector2 base_ = Physics.Position;
            while (true)
            {
                float time = Health < HealthMax * 0.5f ? 1.25f : 1.0f;
                Vector2 target = new Vector2(
                    (float)Math.Sin(AliveTime * 0.01f * time) * 200.0f,
                    (float)-Math.Sin(AliveTime * 0.02f * time) * 80.0f
                );

                Physics.Position = Vector2.Lerp(Physics.Position, base_ + target, 0.1f);
                yield return 0;
            }
        }


        private IEnumerable PhaseRotate()
        {
            for (int i = 0; i < 150; ++i)
            {
                Physics.Rotation += MathHelper.Pi / 50.0f;
                yield return 0;
            }
        }

        private IEnumerable Blowing()
        {
            while (true)
            {
                yield return 200 + World.Random.Next() % 90;
                Pusher.Active = true;
                yield return 120 + World.Random.Next() % 90;
                Pusher.Active = false;
            }
        }


        private void FireLowerCannon(int index, Vector2 direction)
        {
            Vector2[] cannons = {
                new Vector2(-128.0f, +82.0f),
                new Vector2(-108.0f, +92.0f),
                new Vector2(-83.0f, +82.0f),
                new Vector2(+85.0f, +84.0f),
                new Vector2(+109.0f, +92.0f),
                new Vector2(+133.0f, +80.0f),
            };
            float rotation = direction.ToAngle();
            Vector2 position = Physics.Position + cannons[index];
            CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, 8, 3.0f);
            CAudio.PlaySound("EnemyShoot");
        }

        private void FireUpperCannon(int index, Vector2 direction)
        {
            Vector2[] cannons = {
                new Vector2(-128.0f, +82.0f),
                new Vector2(-108.0f, +92.0f),
                new Vector2(-83.0f, +82.0f),
                new Vector2(+85.0f, +84.0f),
                new Vector2(+109.0f, +92.0f),
                new Vector2(+133.0f, +80.0f),
            };

            float rotation = direction.ToAngle();
            Vector2 position = Physics.Position + cannons[index];
            CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, 8, 3.0f);
        }
    }
}
            