//
// Boss11.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Galaxy
{
    public class CBoss11Hand
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss11 Boss { get; set; }
        public CBoss11Arm Arm { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = new CollisionCircle(Vector2.Zero, 51.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss11Hand");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            HealthMax = 1000.0f;
            Coins = 0;
            BaseScore = 0;
            FiberManager = new CFiberManager();
        }

        public override void Update()
        {
            base.Update();
            FiberManager.Update();
        }

        public override void TakeDamage(float damage, CShip source)
        {
            Boss.TakeDamage(damage, source);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle collision = Collision as CollisionCircle;
            collision.Position = Physics.Position;
        }

        public void Activate()
        {
            FiberManager.Fork(this.UpdateSpin);
            FiberManager.Fork(this.UpdateWeapons);
        }

        public IEnumerable UpdateSpin()
        {
            yield return 30;

            while (true)
            {
                for (int i = 0; i < 120; ++i)
                {
                    Physics.AngularVelocity += 0.004f;
                    yield return 1;
                }

                for (int i = 0; i < 120; ++i)
                {
                    Physics.AngularVelocity -= 0.004f;
                    yield return 1;
                }

                Physics.AngularVelocity = 0.0f;

                yield return 30 + World.Random.Next() % 3 * 15;
            }
        }

        public IEnumerable UpdateWeapons()
        {
            Vector2[] cannons =
            {
                new Vector2(14.0f, 0.0f),
                new Vector2(-14.0f, 0.0f),
                new Vector2(0.0f, 14.0f),
                new Vector2(0.0f, -14.0f),
            };

            while (true)
            {
                while (Physics.AngularVelocity < 0.4f)
                    yield return 1;

                switch (World.Random.Next() % 1)
                {
                    case 0:
                        for (int i = 0; i < 15; ++i)
                        {
                            AimedShot(cannons[i % 4].Rotate(Physics.Rotation));
                            yield return 4;
                        }
                        break;
                }
            }
        }

        public void AimedShot(Vector2 offset)
        {
            Vector2 position = Physics.Position + offset;
            float rotation = GetDirToShip().ToAngle();
            CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, 8, 2.0f);
            CAudio.PlaySound("EnemyShoot");
        }
    }

    public class CBoss11Arm
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss11 Boss { get; set; }
        public CBoss11Hand Hand { get; set; }
        public float Out { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = new CollisionAABB(Vector2.Zero, new Vector2(100.0f, 32.0f));
            Visual = CVisual.MakeSpriteUncached(world.Game, "Textures/Enemy/Boss11Arm");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -2.0f;
            HealthMax = 1000.0f;
            Coins = 0;
            BaseScore = 0;
            FiberManager = new CFiberManager();
        }

        public override void Update()
        {
            base.Update();
            FiberManager.Update();

            if (AliveTime == 1)
                MakeChildren();

            Vector2 dir = Physics.GetDir();
            Hand.Physics.Position = Physics.Position + dir * 44.0f;
            Physics.Position = Boss.Physics.Position + dir * Out;
        }

        public void MakeChildren()
        {
            Hand = new CBoss11Hand() { Boss = Boss, Arm = this };
            Hand.Initialize(World);
            World.EntityAdd(Hand);

            Hand.Physics.Position = Physics.Position;
            Hand.UpdateCollision();
        }

        public override void TakeDamage(float damage, CShip source)
        {
            Boss.TakeDamage(damage, source);
        }

        public void Extend()
        {
            FiberManager.Fork(this.ExtendArm);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            //CollisionCircle collision = Collision as CollisionCircle;
            //collision.Position = Physics.Position;
            CollisionAABB collision = Collision as CollisionAABB;
            collision.Position = Physics.Position + new Vector2(-50.0f, -16.0f);
        }

        public IEnumerable ExtendArm()
        {
            float target = 192.0f;
            for (int i = 0; i < 120; ++i)
            {
                Out = target * 1.0f / 120.0f * i;
                yield return 1;
            }

            Hand.Activate();
        }
    }

    public class CBoss11
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss11Arm ArmLeft { get; set; }
        public CBoss11Arm ArmRight { get; set; }
        public int Phase { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(280.0f, 144.0f));
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss11");
            HealthMax = 180.0f * CDifficulty.BossHealthScale[world.CachedDifficulty];
            Coins = 0;
            BaseScore = 0;
            Phase = 0;

            FiberManager = new CFiberManager();
            FiberManager.Fork(this.OpeningSequence);
        }

        public override void Update()
        {
            base.Update();
            FiberManager.Update();

            if (AliveTime == 1)
                MakeChildren();

            if (Phase == 0 && Health < HealthMax * 0.80f)
            {
                Phase = 1;
                ArmLeft.Extend();
                ArmRight.Extend();
            }

            if (Phase == 1 && Health < HealthMax * 0.45f)
            {
                Phase = 2;
                FiberManager.Fork(this.UpdateMissiles);
            }
        }

        private void MakeChildren()
        {
            ArmLeft = new CBoss11Arm() { Boss = this };
            ArmRight = new CBoss11Arm() { Boss = this };

            ArmLeft.Initialize(World);
            ArmRight.Initialize(World);

            World.EntityAdd(ArmLeft);
            World.EntityAdd(ArmRight);

            ArmLeft.Physics.Rotation = MathHelper.Pi;

            ArmLeft.Physics.Position = Physics.Position;
            ArmRight.Physics.Position = Physics.Position;

            ArmLeft.UpdateCollision();
            ArmRight.UpdateCollision();
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
            collision.Position = Physics.Position - new Vector2(140.0f, 72.0f);
        }

        private IEnumerable OpeningSequence()
        {
            yield return 30;

            FiberManager.Fork(this.UpdateMovement);
            FiberManager.Fork(this.UpdateCannons);
        }

        private IEnumerable UpdateMovement()
        {
            yield return 60;

            const float Movement = 220.0f;

            float src = Physics.Position.X;
            float dst = Physics.Position.X - Movement / 2.0f;

            for (int i = 0; i < 90; ++i)
            {
                float t = 1.0f / 90.0f * (float)i;
                Physics.Position = new Vector2(
                    MathHelper.SmoothStep(src, dst, t),
                    Physics.Position.Y
                );
                yield return 0;
            }

            while (true)
            {
                int move_type = 0;
                switch (move_type)
                {
                    case 0:
                        src = Physics.Position.X;
                        dst = Physics.Position.X + Movement;

                        int time = (Phase <= 1) ? 180 : 90;
                        for (int i = 0; i < time; ++i)
                        {
                            float t = 1.0f / (float)time * (float)i;
                            Physics.Position = new Vector2(
                                MathHelper.SmoothStep(src, dst, t),
                                Physics.Position.Y
                            );
                            yield return 0;
                        }

                        if (Phase <= 0)
                            yield return 30;

                        src = Physics.Position.X;
                        dst = Physics.Position.X - Movement;

                        for (int i = 0; i < time; ++i)
                        {
                            float t = 1.0f / (float)time * (float)i;
                            Physics.Position = new Vector2(
                                MathHelper.SmoothStep(src, dst, t),
                                Physics.Position.Y
                            );
                            yield return 0;
                        }

                        if (Phase <= 0)
                            yield return 30 + World.Random.Next() % 60;

                        break;
                }
            }
        }

        public IEnumerable UpdateCannons()
        {
            Vector2[] cannons = 
            {
                new Vector2(-54.0f, 91.0f),
                new Vector2(+54.0f, 91.0f),
            };

            while (true)
            {
                int rounds = World.Random.Next() % 5 + 1;
                for (int i = 0; i < rounds; ++i)
                {
                    CannonShot(Physics.Position + cannons[0]);
                    CannonShot(Physics.Position + cannons[1]);
                    yield return 12;
                }

                if (Phase >= 2)
                    yield return 30;
                else if (Phase >= 1)
                    yield return 45;
                else
                    yield return 60;
            }
        }

        public IEnumerable UpdateMissiles()
        {
            Vector2[][] cannons = 
            {
                new Vector2[]
                {
                    new Vector2(-106.0f, -32.0f),
                    new Vector2(-91.0f, -32.0f),
                    new Vector2(-76, -32.0f),
                },
                new Vector2[]
                {
                    new Vector2(-15.0f, -32.0f),
                    new Vector2(+0.0f, -32.0f),
                    new Vector2(+15.0f, -32.0f),
                },
                new Vector2[]
                {
                    new Vector2(+79.0f, -32.0f),
                    new Vector2(+94.0f, -32.0f),
                    new Vector2(+109.0f, -32.0f),
                },
            };

            while (true)
            {
                switch (World.Random.Next() % 3)
                {
                    case 0:
                        for (int j = 0; j < 3; ++j)
                        {
                            MissileShot(Physics.Position + cannons[j][0], Vector2.UnitY);
                            MissileShot(Physics.Position + cannons[j][1], Vector2.UnitY);
                            MissileShot(Physics.Position + cannons[j][2], Vector2.UnitY);
                            yield return 10;
                        }
                        break;

                    case 1:
                        for (int j = 0; j < 9; ++j)
                        {
                            MissileShot(Physics.Position + cannons[j % 3][0], Vector2.UnitY);
                            yield return 6;
                            MissileShot(Physics.Position + cannons[j % 3][1], Vector2.UnitY);
                            yield return 6;
                            MissileShot(Physics.Position + cannons[j % 3][2], Vector2.UnitY);
                            yield return 6;
                        }
                        break;

                    case 2:
                        MissileShot(Physics.Position + cannons[0][0], Vector2.UnitY);
                        MissileShot(Physics.Position + cannons[2][2], Vector2.UnitY);
                        yield return 8;

                        MissileShot(Physics.Position + cannons[0][1], Vector2.UnitY);
                        MissileShot(Physics.Position + cannons[2][1], Vector2.UnitY);
                        yield return 8;

                        MissileShot(Physics.Position + cannons[0][2], Vector2.UnitY);
                        MissileShot(Physics.Position + cannons[2][0], Vector2.UnitY);
                        yield return 8;

                        MissileShot(Physics.Position + cannons[1][0], Vector2.UnitY);
                        MissileShot(Physics.Position + cannons[1][2], Vector2.UnitY);
                        yield return 8;

                        MissileShot(Physics.Position + cannons[1][1], Vector2.UnitY);
                        yield return 8;
                        break;
                }

                yield return Phase <= 1 ? 90 : 60;
            }
        }

        private void MissileShot(Vector2 position, Vector2 direction)
        {
            float rotation = direction.ToAngle();
            CEnemyMissile missile = CEnemyMissile.Spawn(World, position, rotation, 3.5f, 4.0f);
            CAudio.PlaySound("EnemyShoot");
        }

        private void CannonShot(Vector2 position)
        {
            CEnemyLaser.Spawn(World, position, MathHelper.PiOver2, 8.0f, 2.0f);
            CAudio.PlaySound("EnemyCannonShoot");
        }
    }
}

