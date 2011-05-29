//
// Boss7.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Galaxy
{
    public class CBoss7CannonBig
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss7 Boss { get; set; }
        public float RotateTarget { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = null;
            Visual = CVisual.MakeSpriteUncached(world.Game, "Textures/Enemy/Boss7CannonBig");
            Visual.NormalizedOrigin = new Vector2(0.5f, 0.0f);
            Visual.Update();
            HealthMax = 1000.0f;
            Coins = 0;
            BaseScore = 0;
            FiberManager = new CFiberManager();
            FiberManager.Fork(this.UpdateWeapons0);
        }

        public override void Update()
        {
            base.Update();
            FiberManager.Update();
            Physics.Rotation = Interpolation.MoveToAngle(Physics.Rotation, RotateTarget - MathHelper.PiOver2, 0.025f);
        }

        public override void TakeDamage(float damage, CShip source)
        {
            Boss.TakeDamage(damage, source);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            //CollisionCircle collision = Collision as CollisionCircle;
            //collision.Position = Physics.Position;
        }

        public IEnumerable UpdateWeapons0()
        {
            while (true)
            {
                yield return 30;

                switch (World.Random.Next() % 1)
                {
                    case 0:
                        Vector2 dir = GetDirToShip();
                        RotateTarget = dir.ToAngle();
                        RegularShot();
                        yield return 4;
                        RegularShot();
                        yield return 4;
                        RegularShot();
                        yield return 4;
                        RegularShot();
                        break;
                }
            }
        }

        public void RegularShot()
        {
            Vector2 position = Physics.Position + Vector2.UnitY.Rotate(Physics.Rotation) * 94.0f;
            CEnemyShot shot = CEnemyShot.Spawn(World, position, Physics.Rotation + MathHelper.PiOver2, 8, 2.0f);
            CAudio.PlaySound("EnemyShoot");
        }
    }

    public class CBoss7CannonSmall
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss7 Boss { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = null;
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss7CannonSmall");
            Visual.NormalizedOrigin = new Vector2(0.5f, 0.0f);
            Visual.Update();
            HealthMax = 1000.0f;
            Coins = 0;
            BaseScore = 0;
            FiberManager = new CFiberManager();
            FiberManager.Fork(this.UpdateWeapons0);
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
            //CollisionCircle collision = Collision as CollisionCircle;
            //collision.Position = Physics.Position;
        }

        public IEnumerable UpdateWeapons0()
        {
            while (true)
            {
                yield return 30;

                switch (World.Random.Next() % 1)
                {
                    case 0:
                        Vector2 dir = GetDirToShip();
                        float target = dir.ToAngle() - MathHelper.PiOver2;
                        for (int i = 0; i < 15; ++i)
                        {
                            Physics.Rotation = Interpolation.MoveToAngle(Physics.Rotation, target, 0.025f);
                            yield return 1;
                        }
                        RegularShot();
                        break;
                }
            }
        }

        public void AimedShot()
        {
            Vector2 position = Physics.Position + Vector2.UnitX.Rotate(Physics.Rotation + MathHelper.PiOver2) * 50.0f;
            float rotation = GetDirToShip().ToAngle();
            CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, 8, 2.0f);
            CAudio.PlaySound("EnemyShoot");
        }
        
        public void RegularShot()
        {
            Vector2 position = Physics.Position + Vector2.UnitX.Rotate(Physics.Rotation + MathHelper.PiOver2) * 50.0f;
            CEnemyShot shot = CEnemyShot.Spawn(World, position, Physics.Rotation + MathHelper.PiOver2, 8, 2.0f);
            CAudio.PlaySound("EnemyShoot");
        }
    }

    public class CBoss7
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss7CannonBig CannonBig { get; set; }
        public CBoss7CannonSmall CannonSmallLeft { get; set; }
        public CBoss7CannonSmall CannonSmallRight { get; set; }
        public int Phase { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(409.0f, 103.0f));
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss7");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -2.0f;
            HealthMax = 140.0f * CDifficulty.BossHealthScale[world.CachedDifficulty];
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

            if (Phase == 0 && Health < HealthMax * 0.75f)
            {
                Phase = 1;
                FiberManager.Fork(this.UpdateMissiles);
            }

            if (Phase == 1 && Health < HealthMax * 0.45f)
            {
                Phase = 2;
            }

            UpdateChildrenAttachment();
        }

        private void MakeChildren()
        {
            CannonBig = new CBoss7CannonBig() { Boss = this, };
            CannonSmallLeft = new CBoss7CannonSmall() { Boss = this, };
            CannonSmallRight = new CBoss7CannonSmall() { Boss = this, };

            CannonBig.Initialize(World);
            CannonSmallLeft.Initialize(World);
            CannonSmallRight.Initialize(World);

            World.EntityAdd(CannonBig);
            World.EntityAdd(CannonSmallLeft);
            World.EntityAdd(CannonSmallRight);

            UpdateChildrenAttachment();
        }

        private void UpdateChildrenAttachment()
        {
            CannonBig.Physics.Position = Physics.Position + new Vector2(-1.0f, -57.0f);
            CannonSmallLeft.Physics.Position = Physics.Position + new Vector2(-128.0f, +0.0f);
            CannonSmallRight.Physics.Position = Physics.Position + new Vector2(+124.0f, +0.0f);
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
            collision.Position = Physics.Position - new Vector2(205.0f, 64.0f);
        }

        private IEnumerable OpeningSequence()
        {
            yield return 90;

            FiberManager.Fork(this.UpdateMovement);
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

        public IEnumerable UpdateMissiles()
        {
            Vector2[] cannons =
            {
                new Vector2(-183.0f, -38.0f),
                new Vector2(+178.0f, -38.0f),
            };

            while (true)
            {
                MissileShot(Physics.Position + cannons[0], Vector2.UnitY);
                MissileShot(Physics.Position + cannons[1], Vector2.UnitY);

                yield return Phase <= 1 ? 90 : 60;
            }
        }

        private void MissileShot(Vector2 position, Vector2 direction)
        {
            float rotation = direction.ToAngle();
            CEnemyMissile missile = CEnemyMissile.Spawn(World, position, rotation, 2.5f, 4.0f);
            CAudio.PlaySound("EnemyShoot");
        }
    }
}

