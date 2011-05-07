//
// Boss6.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Galaxy
{
    public class CBoss6Sphere
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss6 Boss { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 38.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss6Sphere");
            HealthMax = 30.0f;
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

        protected override void OnDie()
        {
            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyOrangeColor, 1.5f, null);
            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyGrayColor, 1.5f, null);
        }

        public void AimedShot()
        {
            float rotation = GetDirToShip().ToAngle();
            CEnemyShot shot = CEnemyShot.Spawn(World, Physics.Position, rotation, 8, 2.0f);
            CAudio.PlaySound("EnemyShoot");
        }
        
        public void RegularShot(Vector2 direction)
        {
            float rotation = direction.ToAngle();
            CEnemyShot shot = CEnemyShot.Spawn(World, Physics.Position, rotation, 8, 2.0f);
            CAudio.PlaySound("EnemyShoot");
        }
    }

    public class CBoss6
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public List<CBoss6Sphere> Spheres { get; set; }
        public int Phase { get; set; }
        public float ChildrenOut { get; set; }
        public float ChildrenDesiredOut { get; set; }
        public float ChildrenSpeed { get; set; }
        public float ChildrenDesiredSpeed { get; set; }
        public float ChildrenRotation { get; set; }
        public float CenterDesiredSpeed { get; set; }
        public float CenterSpeed { get; set; }
        public float CenterRotation { get; set; }
        public CVisual CenterPiece;

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 110.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss6");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -2.0f;
            CenterPiece = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss6Center");
            CenterPiece.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            HealthMax = 100.0f * CDifficulty.BossHealthScale[world.CachedDifficulty];
            Coins = 0;
            BaseScore = 0;
            Phase = 0;
            ChildrenOut = 0.0f;
            ChildrenDesiredOut = 120.0f;
            ChildrenSpeed = 0.0f;
            ChildrenDesiredSpeed = 0.0f;
            ChildrenRotation = 0.0f;
            CenterRotation = 0.0f;
            CenterDesiredSpeed = 0.0f;
            CenterSpeed = 0.0f;

            FiberManager = new CFiberManager();
            FiberManager.Fork(this.OpeningSequence);
        }

        public override void Update()
        {
            base.Update();
            FiberManager.Update();

            if (AliveTime == 1)
                MakeChildren();

            if (Phase == 0 && Health < HealthMax * 0.75f)
            {
                Phase = 1;
                FiberManager.KillAll();
                FiberManager.Fork(this.UpdateMovementRandom);
                FiberManager.Fork(this.UpdateWeapons1);

                CenterDesiredSpeed = -0.02f;
                ChildrenDesiredSpeed = 0.05f;
            }

            if (Phase == 1 && Health < HealthMax * 0.45f)
            {
                Phase = 2;
                FiberManager.KillAll();
                FiberManager.Fork(this.UpdateMovementRandomFast);
                FiberManager.Fork(this.UpdateWeapons2);

                CenterDesiredSpeed = -0.05f;
                ChildrenDesiredSpeed = 0.1f;
            }

            UpdateChildrenAttachment();
            UpdateLerping();
        }

        private void MakeChildren()
        {
            Spheres = new List<CBoss6Sphere>();
            for (int i = 0; i < 6; ++i)
            {
                CBoss6Sphere sphere = new CBoss6Sphere() { Boss = this };
                sphere.Initialize(World);
                World.EntityAdd(sphere);
                Spheres.Add(sphere);
            }

            UpdateChildrenAttachment();
            UpdateLerping();
        }

        private void UpdateChildrenAttachment()
        {
            float step = MathHelper.TwoPi / (float)Spheres.Count;
            for (int i = 0; i < Spheres.Count; ++i)
            {
                if (!Spheres[i].IsDead && Health < (2.5f + 0.5f * i))
                {
                    Spheres[i].Die();
                    continue;
                }

                float angle = MathHelper.WrapAngle(ChildrenRotation + step * (float)i);
                Vector2 out_ = Vector2.UnitX.Rotate(angle) * ChildrenOut;
                Spheres[i].Physics.Position = Physics.Position + out_;
            }
        }

        private void UpdateLerping()
        {
            ChildrenOut = Interpolation.MoveToValue(ChildrenOut, ChildrenDesiredOut, 0.020f);
            ChildrenSpeed = Interpolation.MoveToValue(ChildrenSpeed, ChildrenDesiredSpeed, 0.025f);
            ChildrenRotation = ChildrenRotation + ChildrenSpeed;
            CenterSpeed = Interpolation.MoveToValue(CenterSpeed, CenterDesiredSpeed, 0.01f);
            CenterRotation = CenterRotation + CenterSpeed;
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
            CenterPiece.Draw(sprite_batch, Physics.Position, CenterRotation);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        private IEnumerable OpeningSequence()
        {
            yield return 90;

            ChildrenDesiredOut = 145.0f;
            ChildrenDesiredSpeed = 0.02f;
            CenterDesiredSpeed = -0.01f;

            FiberManager.Fork(this.UpdateMovementIdle);
            FiberManager.Fork(this.UpdateWeapons0);
        }

        private IEnumerable UpdateMovementIdle()
        {
            Vector2 base_ = Physics.Position;
            while (true)
            {
                Vector2 target = new Vector2(
                    (float)Math.Sin(AliveTime * 0.01f) * 200.0f,
                    (float)-Math.Sin(AliveTime * 0.02f) * 80.0f
                );

                Physics.Position = Vector2.Lerp(Physics.Position, base_ + target, 0.01f);
                yield return 0;
            }
        }

        private IEnumerable UpdateMovementRandom()
        {
            while (true)
            {
                Vector2 center = World.GameCamera.GetCenter().ToVector2() + Vector2.UnitY * -120.0f;
                Vector2 target = center + Vector2.UnitX.Rotate(World.Random.NextAngle()) * 300.0f * World.Random.NextSign();

                for (int i = 0; i < 180; ++i)
                {
                    Physics.Position = Vector2.Lerp(Physics.Position, target, 0.010f);
                    yield return 0;
                }
                yield return 0;
            }
        }

        private IEnumerable UpdateMovementRandomFast()
        {
            while (true)
            {
                Vector2 center = World.GameCamera.GetCenter().ToVector2() + Vector2.UnitY * -120.0f;
                Vector2 target = center + Vector2.UnitX.Rotate(World.Random.NextAngle()) * 300.0f * World.Random.NextSign();

                for (int i = 0; i < 90; ++i)
                {
                    Physics.Position = Vector2.Lerp(Physics.Position, target, 0.020f);
                    yield return 0;
                }
                yield return 0;
            }
        }

        public IEnumerable UpdateWeapons0()
        {
            while (true)
            {
                yield return 60 + World.Random.Next() % 30;

                switch (World.Random.Next() % 3)
                {
                    case 0:
                        for (int c = 0; c < 4; ++c)
                        {
                            Vector2 dir = Vector2.UnitY;
                            float sign = c % 2 == 0 ? 1.0f : -1.0f;
                            for (int i = 0; i < 6; ++i)
                            {
                                RegularShot(c, dir.Rotate(0.4f / 8.0f * i * sign));
                                yield return 2;
                            }
                        }
                        break;

                    case 1:
                    {
                        for (int c = 0; c < 4; ++c)
                        {
                            Vector2 dir = GetDirToShip();
                            float sign = c % 2 == 0 ? 1.0f : -1.0f;
                            for (int i = 0; i < 5; ++i)
                            {
                                RegularShot(c, dir.Rotate(0.4f / 8.0f * i * sign));
                                yield return 3;
                            }
                        }
                        break;
                    }

                    case 2:
                        float old = ChildrenDesiredSpeed;
                        ChildrenDesiredSpeed = 0.2f;
                        yield return 60;

                        for (int i = 0; i < 5; ++i)
                        {
                            for (int j = 0; j < 6; ++j)
                            {
                                Spheres[j].AimedShot();
                                yield return 3;
                            }
                        }

                        ChildrenDesiredSpeed = old;
                        break;
                }
            }
        }

        public IEnumerable UpdateWeapons1()
        {
            while (true)
            {
                yield return 60 + World.Random.Next() % 30;

                switch (World.Random.Next() % 3)
                {
                    case 0:
                        for (int c = 0; c < 6; ++c)
                        {
                            Vector2 dir = Vector2.UnitX.Rotate(ChildrenRotation + MathHelper.TwoPi / 6.0f * c);
                            Vector2 dir2 = Vector2.UnitX.Rotate(ChildrenRotation + MathHelper.TwoPi / 6.0f * (c + 3) % 6);
                            for (int i = 0; i < 8; ++i)
                            {
                                Spheres[c].RegularShot(dir);
                                Spheres[(c + 3) % 6].RegularShot(dir2);
                                yield return 2;
                            }
                        }
                        break;

                    case 1:
                    {
                        for (int c = 0; c < 4; ++c)
                        {
                            Vector2 dir = GetDirToShip();
                            float sign = c % 2 == 0 ? 1.0f : -1.0f;
                            for (int i = 0; i < 14; ++i)
                            {
                                RegularShot(c, dir.Rotate(0.4f / 8.0f * i * sign));
                                yield return 3;
                            }
                        }
                        break;
                    }

                    case 2:
                        float old = ChildrenDesiredSpeed;
                        ChildrenDesiredSpeed = 0.2f;
                        yield return 60;

                        for (int i = 0; i < 5; ++i)
                        {
                            for (int j = 0; j < 6; ++j)
                            {
                                Spheres[j].AimedShot();
                                yield return 3;
                            }
                        }

                        ChildrenDesiredSpeed = old;
                        break;
                }
            }
        }

        public IEnumerable UpdateWeapons2()
        {
            while (true)
            {
                yield return 60 + World.Random.Next() % 30;

                switch (World.Random.Next() % 3)
                {
                    case 0:
                        for (int c = 0; c < 6; ++c)
                        {
                            for (int i = 0; i < 4; ++i)
                            {
                                Spheres[(c + 0) % 6].RegularShot(Vector2.UnitX.Rotate(ChildrenRotation + MathHelper.TwoPi / 6.0f * (c + 0) % 6));
                                Spheres[(c + 1) % 6].RegularShot(Vector2.UnitX.Rotate(ChildrenRotation + MathHelper.TwoPi / 6.0f * (c + 1) % 6));
                                Spheres[(c + 2) % 6].RegularShot(Vector2.UnitX.Rotate(ChildrenRotation + MathHelper.TwoPi / 6.0f * (c + 2) % 6));
                                Spheres[(c + 3) % 6].RegularShot(Vector2.UnitX.Rotate(ChildrenRotation + MathHelper.TwoPi / 6.0f * (c + 3) % 6));
                                Spheres[(c + 4) % 6].RegularShot(Vector2.UnitX.Rotate(ChildrenRotation + MathHelper.TwoPi / 6.0f * (c + 4) % 6));
                                Spheres[(c + 5) % 6].RegularShot(Vector2.UnitX.Rotate(ChildrenRotation + MathHelper.TwoPi / 6.0f * (c + 5) % 6));
                                yield return 2;
                            }
                        }
                        break;

                    case 1:
                    {
                        for (int c = 0; c < 4; ++c)
                        {
                            Vector2 dir = GetDirToShip();
                            float sign = c % 2 == 0 ? 1.0f : -1.0f;
                            for (int i = 0; i < 10; ++i)
                            {
                                RegularShot(c, dir.Rotate(0.4f / 8.0f * i * sign));
                                yield return 3;
                            }
                        }
                        break;
                    }

                    case 2:
                    {
                        Vector2 dir = GetDirToShipWithLead(32.0f);
                        for (int i = 0; i < 32; ++i)
                        {
                            RegularShot(i % 4, dir);
                            yield return 4;
                        }

                        break;
                    }
                }
            }
        }


        private void RegularShot(int cannon, Vector2 direction)
        {
            Vector2[] offsets = {
                new Vector2(-40.0f, 0.0f),
                new Vector2(0.0f, 40.0f),
                new Vector2(40.0f, 0.0f),
                new Vector2(0.0f, -40.0f),
            };

            Vector2 position = Physics.Position + offsets[cannon].Rotate(CenterRotation);
            
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

