//
// Boss12.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Galaxy
{
    public class CBoss12Eye
        : CEnemy
    {
        public CBoss12 Boss { get; set; }
        public CFiberManager FiberManager { get; set; }
        public Vector2 BaseOffset { get; set; }
        public Vector2 LookOffset { get; set; }
        public Vector2 LookTarget { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = null;
            Visual = CVisual.MakeSpriteUncached(world.Game, "Textures/Enemy/Boss12Eye");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * +1.0f;
            Visual.NormalizedOrigin = new Vector2(1.0f, 0.5f);
            Visual.Update();
            HealthMax = 1000.0f;
            Coins = 0;
            BaseScore = 0;
            FiberManager = new CFiberManager();
            FiberManager.Fork(UpdateEyes);
        }

        public override void Update()
        {
            base.Update();

            Physics.Position = Boss.Physics.Position + BaseOffset + LookOffset;

            FiberManager.Update();
        }
        
        public void LookAtPlayer(CShip ship)
        {
            LookTarget = ship.Physics.Position;
        }

        public IEnumerable UpdateEyes()
        {
            while (true)
            {
                float desired = (LookTarget - Physics.Position).ToAngle();
                float current = LookOffset.ToAngle();
                float angle = Interpolation.MoveToAngle(current, desired, 0.02f);
                LookOffset = Vector2.UnitX.Rotate(angle) * 10.0f;
                yield return 0;
            }
        }
    }

    public class CBoss12Arm
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss12 Boss { get; set; }
        public Vector2 BaseOffset { get; set; }
        public float BaseRotation { get; set; }
        public float WaveStrength { get; set; }
        public float WaveOffset { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = null;
            Visual = CVisual.MakeSpriteUncached(world.Game, "Textures/Enemy/Boss12Arm");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -2.0f;
            HealthMax = 1000.0f;
            Coins = 0;
            BaseScore = 0;
        }

        public override void Update()
        {
            base.Update();
            Physics.Position = Boss.Physics.Position + BaseOffset;
            Physics.Rotation = BaseRotation + (float)Math.Sin(AliveTime * 0.03f) * WaveStrength + WaveOffset;
        }

        public override void TakeDamage(float damage, CShip source)
        {
            Boss.TakeDamage(damage, source);
        }

        public override void UpdateCollision()
        {
        }
    }

    public class CBoss12
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss12Arm[] ArmsLeft { get; set; }
        public CBoss12Arm[] ArmsRight { get; set; }
        public CBoss12Eye[] Eyes { get; set; }
        public int Phase { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(400.0f, 144.0f));
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss12");
            HealthMax = 250.0f * CDifficulty.BossHealthScale[world.CachedDifficulty];
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
            }

            if (Phase == 1 && Health < HealthMax * 0.45f)
            {
                Phase = 2;
            }
        }

        private void MakeChildren()
        {
            ArmsLeft = new CBoss12Arm[]
            {
                new CBoss12Arm() { Boss = this, BaseOffset = new Vector2(+110.0f, -60.0f), BaseRotation = -MathHelper.Pi, WaveStrength = 0.06f, WaveOffset = -0.07f },
                new CBoss12Arm() { Boss = this, BaseOffset = new Vector2(+110.0f, -10.0f), BaseRotation = -MathHelper.Pi, WaveStrength = 0.06f, WaveOffset = -0.07f },
                new CBoss12Arm() { Boss = this, BaseOffset = new Vector2(+110.0f, +40.0f), BaseRotation = -MathHelper.Pi, WaveStrength = 0.06f, WaveOffset = -0.07f },
            };

            ArmsRight = new CBoss12Arm[]
            {
                new CBoss12Arm() { Boss = this, BaseOffset = new Vector2(-110.0f, -60.0f), BaseRotation = 0.0f, WaveStrength = -0.06f, WaveOffset = 0.07f, },
                new CBoss12Arm() { Boss = this, BaseOffset = new Vector2(-110.0f, -10.0f), BaseRotation = 0.0f, WaveStrength = -0.06f, WaveOffset = 0.07f, },
                new CBoss12Arm() { Boss = this, BaseOffset = new Vector2(-110.0f, +40.0f), BaseRotation = 0.0f, WaveStrength = -0.06f, WaveOffset = 0.07f, },
            };

            for (int i = 0; i < 3; ++i)
            {
                ArmsLeft[i].Initialize(World);
                ArmsRight[i].Initialize(World);

                // avoid despawn being out of screen
                ArmsLeft[i].Physics.Position = Physics.Position;
                ArmsRight[i].Physics.Position = Physics.Position;

                World.EntityAdd(ArmsLeft[i]);
                World.EntityAdd(ArmsRight[i]);
            }

            Eyes = new CBoss12Eye[]
            {
                new CBoss12Eye() { Boss = this, BaseOffset = new Vector2(-38.0f, +42.0f) },
                new CBoss12Eye() { Boss = this, BaseOffset = new Vector2(+52.0f, +42.0f) },
            };

            Eyes[0].Initialize(World);
            Eyes[1].Initialize(World);

            // avoid despawn being out of screen
            Eyes[0].Physics.Position = Physics.Position;
            Eyes[1].Physics.Position = Physics.Position;

            World.EntityAdd(Eyes[0]);
            World.EntityAdd(Eyes[1]);
        }

        protected override void OnDie()
        {
            FiberManager.KillAll();
            World.DestroyAllProjectiles();
            World.DestroyAllEnemies();

            for (int i = 0; i < 30; ++i)
            {
                World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position + World.Random.NextVector2() * World.Random.NextSignedFloat() * 440.0f, CEnemy.EnemyOrangeColor, 1.75f, null);
                World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position + World.Random.NextVector2() * World.Random.NextSignedFloat() * 440.0f, CEnemy.EnemyGrayColor, 1.75f, null);
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
            collision.Position = Physics.Position - new Vector2(200.0f, 80.0f);
        }

        private IEnumerable OpeningSequence()
        {
            yield return 30;

            FiberManager.Fork(this.UpdateMovement);
            FiberManager.Fork(this.UpdateCannons0);
            FiberManager.Fork(this.EyeLook);
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

        public IEnumerable EyeLook()
        {
            while (true)
            {
                CShip ship = World.GetNearestShip(Physics.Position);
                if (ship != null)
                {
                    Eyes[0].LookAtPlayer(ship);
                    Eyes[1].LookAtPlayer(ship);
                }

                yield return 8;
            }
        }

        static Vector2[][] CannonPositions = 
        {
            new Vector2[] {
                new Vector2(-70.0f, -54.0f),
                new Vector2(-70.0f, -42.0f),
                new Vector2(-70.0f, -30.0f),
                new Vector2(-70.0f, -18.0f),
            },
            new Vector2[] {
                new Vector2(-23.0f, -54.0f),
                new Vector2(-23.0f, -42.0f),
                new Vector2(-23.0f, -30.0f),
                new Vector2(-23.0f, -18.0f),
            },
            new Vector2[] {
                new Vector2(+23.0f, -54.0f),
                new Vector2(+23.0f, -42.0f),
                new Vector2(+23.0f, -30.0f),
                new Vector2(+23.0f, -18.0f),
            },
            new Vector2[] {
                new Vector2(+70.0f, -54.0f),
                new Vector2(+70.0f, -42.0f),
                new Vector2(+70.0f, -30.0f),
                new Vector2(+70.0f, -18.0f),
            },
        };

        public IEnumerable UpdateCannons0()
        {
            while (true)
            {
                switch (World.Random.Next() % 4)
                {
                    case 0:
                        {
                            for (int i = 0; i < 8; ++i)
                            {
                                for (int j = 0; j < 4; ++j)
                                {
                                    for (int k = 0; k < 4; ++k)
                                    {
                                        CannonShot(Physics.Position + CannonPositions[k][j]);
                                    }
                                }
                                yield return 8;
                            }
                            break;
                        }

                    case 1:
                        {
                            for (int i = 0; i < 8; ++i)
                            {
                                Vector2 dir = GetDirToShip();

                                for (int j = 0; j < 4; ++j)
                                {
                                    CannonShotDirectional(Physics.Position + CannonPositions[j][3], dir);
                                }

                                yield return 6;
                            }
                            break;
                        }

                    case 2:
                        {
                            for (int i = 0; i < 14; ++i)
                            {
                                Vector2 dirl = GetDirToShipWithLead(4.0f).Rotate(-0.2f);
                                Vector2 dirr = GetDirToShipWithLead(4.0f).Rotate(+0.2f);

                                CannonShotDirectional(Physics.Position + CannonPositions[0][3], dirl);
                                CannonShotDirectional(Physics.Position + CannonPositions[3][3], dirr);
                                CannonShotDirectional(Physics.Position + CannonPositions[1][3], dirr);
                                CannonShotDirectional(Physics.Position + CannonPositions[2][3], dirl);
                                yield return 1;

                                CannonShotDirectional(Physics.Position + CannonPositions[0][3], dirl);
                                CannonShotDirectional(Physics.Position + CannonPositions[3][3], dirr);
                                CannonShotDirectional(Physics.Position + CannonPositions[1][3], dirr);
                                CannonShotDirectional(Physics.Position + CannonPositions[2][3], dirl);
                                yield return 3;
                            }
                            break;
                        }
                }

                yield return 20;

                if (Phase > 0)
                {
                    FiberManager.Kill(this.UpdateCannons0);
                    FiberManager.Fork(this.UpdateCannons1);
                }
            }
        }

        public IEnumerable UpdateCannons1()
        {
            while (true)
            {
                switch (World.Random.Next() % 2)
                {
                    case 0:
                    {
                        for (int i = 0; i < 9; ++i)
                        {
                            for (int j = 0; j < 4; ++j)
                            {
                                for (int k = 0; k < 2; ++k)
                                {
                                    CannonShot(Physics.Position + CannonPositions[k][j]);
                                }
                            }
                            yield return 6;
                        }
                        break;
                    }

                    case 1:
                    {
                        for (int i = 0; i < 12; ++i)
                        {
                            Vector2 dir = GetDirToShip();

                            for (int j = 0; j < 2; ++j)
                            {
                                CannonShotDirectional(Physics.Position + CannonPositions[j][3], dir);
                            }

                            yield return 3;
                        }
                        break;
                    }
                }

                yield return 15;

                if (Phase > 1)
                {
                    FiberManager.Kill(this.UpdateCannons1);
                    FiberManager.Fork(this.UpdateCannons2);
                }
            }
        }

        public IEnumerable UpdateCannons2()
        {
            while (true)
            {
                switch (World.Random.Next() % 3)
                {
                    case 0:
                    {
                        for (int i = 0; i < 3; ++i)
                        {
                            for (int j = 0; j < 4; ++j)
                            {
                                for (int k = 0; k < 4; ++k)
                                {
                                    CannonShot(Physics.Position + CannonPositions[k][j]);
                                }
                            }
                            yield return 6;
                        }
                        break;
                    }

                    case 1:
                    {
                        for (int i = 0; i < 20; ++i)
                        {
                            Vector2 dir = GetDirToShip();

                            for (int j = 0; j < 4; ++j)
                            {
                                CannonShotDirectional(Physics.Position + CannonPositions[j][3], dir);
                            }

                            yield return 10;
                        }
                        break;
                    }

                    case 2:
                    {
                        for (int i = 0; i < 6; ++i)
                        {
                            Vector2 dirl = GetDirToShipWithLead(4.0f).Rotate(-0.2f);
                            Vector2 dirr = GetDirToShipWithLead(4.0f).Rotate(+0.2f);

                            CannonShotDirectional(Physics.Position + CannonPositions[0][3], dirl);
                            CannonShotDirectional(Physics.Position + CannonPositions[3][3], dirr);
                            CannonShotDirectional(Physics.Position + CannonPositions[1][3], dirr);
                            CannonShotDirectional(Physics.Position + CannonPositions[2][3], dirl);
                            yield return 3;

                            CannonShotDirectional(Physics.Position + CannonPositions[0][3], dirl);
                            CannonShotDirectional(Physics.Position + CannonPositions[3][3], dirr);
                            CannonShotDirectional(Physics.Position + CannonPositions[1][3], dirr);
                            CannonShotDirectional(Physics.Position + CannonPositions[2][3], dirl);
                            yield return 3;
                        }
                        break;
                    }
                }

                if (Health > HealthMax * 0.05f)
                {
                    yield return 10;
                }
            }
        }

        static float[] CannonSpeeds =
        {
            8.0f, 11.0f, 14.0f 
        };

        private void CannonShot(Vector2 position)
        {
            CEnemyMiniShot.Spawn(World, position, MathHelper.PiOver2, CannonSpeeds[Phase], 1.25f);
            CAudio.PlaySound("EnemyCannonShoot");
        }

        private void CannonShotDirectional(Vector2 position, Vector2 dir)
        {
            float angle = dir.ToAngle();
            CEnemyMiniShot.Spawn(World, position, angle, CannonSpeeds[Phase], 1.25f);
            CAudio.PlaySound("EnemyCannonShoot");
        }
    }
}

