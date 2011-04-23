//
// Boss1.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Galaxy
{
    public class CBoss1Chunk
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CEnemy Boss { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(120.0f, 120.0f));
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss1Chunk");
            HealthMax = 4.0f;
            Coins = 0;
            BaseScore = 0;

            FiberManager = new CFiberManager();
            FiberManager.Fork(this.UpdateWeapons);
        }

        public override void Update()
        {
            base.Update();
            FiberManager.Update();
        }

        public IEnumerable UpdateWeapons()
        {
            while (true)
            {
                RegularShot(Physics.Position + new Vector2(-16.0f, +24.0f), Vector2.UnitY);
                RegularShot(Physics.Position + new Vector2(+16.0f, +24.0f), Vector2.UnitY);
                RegularShot(Physics.Position + new Vector2(-16.0f, -24.0f), Vector2.UnitY);
                RegularShot(Physics.Position + new Vector2(+16.0f, -24.0f), Vector2.UnitY);

                if (Boss.Health < Boss.HealthMax * 0.3f)
                    yield return 30;
                else
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

    public class CBoss1
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss1Chunk Left { get; set; }
        public CBoss1Chunk Right { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 150.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss1");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            HealthMax = 14.0f;
            Coins = 0;
            BaseScore = 0;

            FiberManager = new CFiberManager();
            FiberManager.Fork(this.UpdateWeapons);
            FiberManager.Fork(this.UpdateMovement);
            FiberManager.Fork(this.UpdateMovementIdle);
        }

        public override void Update()
        {
            base.Update();
            FiberManager.Update();

            if (AliveTime == 1)
                MakeChildren();

            UpdateChildrenAttachment();
        }

        private void MakeChildren()
        {
            Left = new CBoss1Chunk() { Boss = this };
            Right = new CBoss1Chunk() { Boss = this };

            Left.Initialize(World);
            Right.Initialize(World);
            
            World.EntityAdd(Left);
            World.EntityAdd(Right);

            UpdateChildrenAttachment();
        }

        private void UpdateChildrenAttachment()
        {
            if (!Left.IsDead)
                Left.Physics.Position = Physics.Position + new Vector2(-150.0f, 90.0f);
            if (!Right.IsDead)
                Right.Physics.Position = Physics.Position + new Vector2(+150.0f, 90.0f);
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
        
        private IEnumerable UpdateWeapons()
        {
            yield return 60;

            while (true)
            {
                yield return 30;

                int attack_type = World.Random.Next() % 4;

                if (Left.IsDead && Right.IsDead)
                    attack_type = 2;

                switch (attack_type)
                {
                    case 0:
                        RegularShot(Physics.Position + new Vector2(+38.0f, +78.0f), Vector2.UnitY);
                        RegularShot(Physics.Position + new Vector2(-38.0f, +78.0f), Vector2.UnitY);
                        yield return 4;
                        RegularShot(Physics.Position + new Vector2(+38.0f, +78.0f), Vector2.UnitY);
                        RegularShot(Physics.Position + new Vector2(-38.0f, +78.0f), Vector2.UnitY);
                        yield return 4;
                        RegularShot(Physics.Position + new Vector2(+38.0f, +78.0f), Vector2.UnitY);
                        RegularShot(Physics.Position + new Vector2(-38.0f, +78.0f), Vector2.UnitY);
                        yield return 4;
                        break;

                    case 1:    
                        RegularShot(Physics.Position + new Vector2(+38.0f, +78.0f), GetDirToShip());
                        RegularShot(Physics.Position + new Vector2(-38.0f, +78.0f), GetDirToShip());
                        yield return 4;
                        RegularShot(Physics.Position + new Vector2(+38.0f, +78.0f), GetDirToShip());
                        RegularShot(Physics.Position + new Vector2(-38.0f, +78.0f), GetDirToShip());
                        yield return 4;
                        RegularShot(Physics.Position + new Vector2(+38.0f, +78.0f), GetDirToShip());
                        RegularShot(Physics.Position + new Vector2(-38.0f, +78.0f), GetDirToShip());
                        yield return 4;
                        break;

                    case 2:
                        Vector2 dir = GetDirToShip();
                        for (int i = 0; i < 8; ++i)
                        {
                            float angle = -0.25f + 0.5f / 8.0f * i;
                            RegularShot(Physics.Position + new Vector2(+38.0f, +78.0f), dir.Rotate(angle));
                            RegularShot(Physics.Position + new Vector2(-38.0f, +78.0f), dir.Rotate(angle));
                            yield return 2;
                        }
                        yield return 30;
                        break;
                }
            }
        }

        private IEnumerable UpdateMovement()
        {
            yield return 60;

            while (true)
            {
                if (Left.IsDead && Right.IsDead)
                    yield return 10 + World.Random.Next() % 30;
                else
                    yield return 30 + World.Random.Next() % 90;

                int move_type = 0;
                switch (move_type)
                {
                    case 0:
                        float src = Physics.Position.X;
                        float dst = World.Random.Next() % 2 == 0 ? -180.0f : 180.0f;
                        if (World.Random.Next() % 100 < 20)
                            dst = 0.0f;

                        for (int i = 0; i < 60; ++i)
                        {
                            float t = 1.0f / 60.0f * (float)i;
                            Physics.Position = new Vector2(
                                MathHelper.SmoothStep(src, dst, t),
                                Physics.Position.Y
                            );
                            yield return 0;
                        }
                        break;
                }
            }
        }

        private IEnumerable UpdateMovementIdle()
        {
            float base_ = Physics.Position.Y;
            float time = 0.0f;
            while (true)
            {
                float y = base_ + (float)Math.Sin(time) * 12.0f;
                Physics.Position = new Vector2(Physics.Position.X, y);
                time += 1.0f / 60.0f * 3.0f;
                yield return 0;
            }
        }

        private void RegularShot(Vector2 position, Vector2 direction)
        {
            float rotation = direction.ToAngle();
            CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, 8, 2.0f);
            CAudio.PlaySound("EnemyShoot");
        }
    }
}

