//
// Boss3.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Galaxy
{
    public class CBoss3Chunk
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss3 Boss { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(120.0f, 120.0f));
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss3Chunk");
            HealthMax = 100.0f;
            Coins = 0;
            BaseScore = 0;
            FiberManager = new CFiberManager();
            FiberManager.Fork(this.UpdateWeaponsPhase0);
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

        public IEnumerable UpdateWeaponsPhase0()
        {
            while (true)
            {
                yield return 30;
                if (Boss.Phase > 0)
                {
                    FiberManager.Kill(this.UpdateWeaponsPhase0);
                    FiberManager.Fork(this.UpdateWeaponsPhase1);
                    yield break;
                }
            }
        }

        public IEnumerable UpdateWeaponsPhase1()
        {
            while (true)
            {
                RegularShot(Physics.Position + new Vector2(-34.0f, +44.0f), Vector2.UnitY);
                RegularShot(Physics.Position + new Vector2(+34.0f, +44.0f), Vector2.UnitY);
                RegularShot(Physics.Position + new Vector2(-34.0f, -44.0f), Vector2.UnitY);
                RegularShot(Physics.Position + new Vector2(+34.0f, -44.0f), Vector2.UnitY);
                yield return 90;

                if (Boss.Phase > 1)
                {
                    FiberManager.Kill(this.UpdateWeaponsPhase1);
                    FiberManager.Fork(this.UpdateWeaponsPhase2);
                    yield break;
                }
            }
        }

        public IEnumerable UpdateWeaponsPhase2()
        {
            while (true)
            {
                RegularShot(Physics.Position + new Vector2(-34.0f, +44.0f), Vector2.UnitY);
                RegularShot(Physics.Position + new Vector2(+34.0f, +44.0f), Vector2.UnitY);
                RegularShot(Physics.Position + new Vector2(-34.0f, -44.0f), Vector2.UnitY);
                RegularShot(Physics.Position + new Vector2(+34.0f, -44.0f), Vector2.UnitY);
                yield return 60;
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB collision = Collision as CollisionAABB;
            collision.Position = Physics.Position + new Vector2(-60.0f, -60.0f);
        }

        protected override void OnDie()
        {
            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyOrangeColor, 1.5f, null);
            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyGrayColor, 1.5f, null);
        }

        private void RegularShot(Vector2 position, Vector2 direction)
        {
            float rotation = direction.ToAngle();
            CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, 8, 2.0f);
            CAudio.PlaySound("EnemyShoot");
        }
    }

    public class CBoss3
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss3Chunk Left { get; set; }
        public CBoss3Chunk Right { get; set; }
        public int Phase { get; set; }
        public float ChildrenOut { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 60.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss3");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            HealthMax = 100.0f * CDifficulty.BossHealthScale[world.CachedDifficulty];
            Coins = 0;
            BaseScore = 0;
            Phase = 0;
            ChildrenOut = 80.0f;

            FiberManager = new CFiberManager();
            FiberManager.Fork(this.UpdateWeaponsPhase0);
            FiberManager.Fork(this.UpdateMovementIdle);
        }

        public override void Update()
        {
            base.Update();
            FiberManager.Update();

            if (Health < HealthMax * 0.45f)
                Phase = 2;
            else if (Health < HealthMax * 0.75f)
                Phase = 1;

            if (AliveTime == 1)
                MakeChildren();

            if (Phase == 1)
                Physics.AngularVelocity = Interpolation.MoveToValue(Physics.AngularVelocity, 0.025f, 0.015f);
            if (Phase == 2)
                Physics.AngularVelocity = Interpolation.MoveToValue(Physics.AngularVelocity, 0.050f, 0.005f);

            UpdateChildrenAttachment();
        }

        private void MakeChildren()
        {
            Left = new CBoss3Chunk() { Boss = this };
            Right = new CBoss3Chunk() { Boss = this };

            Left.Initialize(World);
            Right.Initialize(World);
            
            World.EntityAdd(Left);
            World.EntityAdd(Right);

            UpdateChildrenAttachment();
        }

        private void UpdateChildrenAttachment()
        {
            if (Phase > 1)
            {
                ChildrenOut = Interpolation.MoveToValue(ChildrenOut, 160.0f, 0.01f);
                Left.Physics.AngularVelocity = Interpolation.MoveToValue(Left.Physics.AngularVelocity, -0.01f, 0.01f);
                Right.Physics.AngularVelocity = Interpolation.MoveToValue(Right.Physics.AngularVelocity, -0.01f, 0.01f);
            }
            else if (Phase > 0)
            {
                ChildrenOut = Interpolation.MoveToValue(ChildrenOut, 100.0f, 0.01f);
            }

            Vector2 normal = Vector2.UnitX.Rotate(MathHelper.WrapAngle(Physics.Rotation));
            Left.Physics.Position = Physics.Position + normal * -ChildrenOut;
            Right.Physics.Position = Physics.Position + normal * +ChildrenOut;
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

            //sprite_batch.DrawString(World.Game.GameRegularFont, "health:" + Health, World.GameCamera.GetCenter().ToVector2(), Color.White);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }
        
        private IEnumerable UpdateWeaponsPhase0()
        {
            yield return 60;

            while (true)
            {
                yield return 30;

                int attack_type = World.Random.Next() % 2;

                switch (attack_type)
                {
                    case 0:
                        RegularShot(Physics.Position, GetDirToShip());
                        yield return 4;
                        RegularShot(Physics.Position, GetDirToShip());
                        yield return 4;
                        RegularShot(Physics.Position, GetDirToShip());
                        yield return 4;
                        break;

                    case 1:    
                        for (int i = 0; i < 8; ++i)
                        {
                            PelletShot(Physics.Position, GetDirToShip());
                            yield return 6;
                        }
                        break;
                }

                if (Phase > 0)
                {
                    FiberManager.Kill(this.UpdateWeaponsPhase0);
                    FiberManager.Fork(this.UpdateWeaponsPhase1);
                    yield break;
                }
            }
        }

        private IEnumerable UpdateWeaponsPhase1()
        {
            yield return 60;

            while (true)
            {
                yield return 30;

                int attack_type = World.Random.Next() % 2;

                switch (attack_type)
                {
                    case 0:
                        for (int i = 0; i < 12; ++i)
                        {
                            RegularShot(Physics.Position, GetDirToShip());
                            yield return 4;
                        }
                        yield return 4;
                        break;

                    case 1:    
                        for (int i = 0; i < 5; ++i)
                        {
                            Vector2 dir = GetDirToShip();
                            PelletShot(Physics.Position, GetDirToShip());
                            PelletShot(Physics.Position, GetDirToShip().Rotate(-0.3f));
                            PelletShot(Physics.Position, GetDirToShip().Rotate(+0.3f));
                            yield return 6;
                        }
                        break;
                }

                if (Phase > 1)
                {
                    FiberManager.Kill(this.UpdateWeaponsPhase1);
                    FiberManager.Fork(this.UpdateWeaponsPhase2);
                    yield break;
                }
            }
        }

        private IEnumerable UpdateWeaponsPhase2()
        {
            yield return 60;

            while (true)
            {
                yield return 30;

                int attack_type = World.Random.Next() % 2;

                switch (attack_type)
                {
                    case 0:
                        for (int i = 0; i < 12; ++i)
                        {
                            Vector2 dir = Vector2.UnitY.Rotate(-0.5f + 1.0f / 12.0f * i);
                            RegularShot(Physics.Position, dir);
                        }
                        yield return 8;

                        for (int i = 0; i < 12; ++i)
                        {
                            Vector2 dir = Vector2.UnitY.Rotate(-0.5f + 1.0f / 12.0f * i);
                            RegularShot(Physics.Position, dir);
                        }
                        yield return 8;
                        break;

                    case 1:    
                        for (int i = 0; i < 20; ++i)
                        {
                            PelletShot(Physics.Position, GetDirToShip());
                            yield return 6;
                        }
                        break;
                }
            }
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

                Physics.Position = Vector2.Lerp(Physics.Position, base_ + target, 0.1f);
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

