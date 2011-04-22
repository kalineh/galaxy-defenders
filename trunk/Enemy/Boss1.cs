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
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(60.0f, 80.0f));
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss1Chunk");
            HealthMax = 1.0f;
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
            yield return 4;
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB collision = Collision as CollisionAABB;
            collision.Position = Physics.Position;
        }

        protected override void OnDie()
        {
            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyOrangeColor, 1.5f, null);
            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyGrayColor, 1.5f, null);
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
            HealthMax = 20.0f;
            Coins = 0;
            BaseScore = 0;

            FiberManager = new CFiberManager();
            FiberManager.Fork(this.UpdateWeapons);

            Left = new CBoss1Chunk() { Boss = this };
            Right = new CBoss1Chunk() { Boss = this };

            Left.Initialize(world);
            Right.Initialize(world);
            
            world.EntityAdd(Left);
            world.EntityAdd(Right);

            UpdateChildrenAttachment();
        }

        public override void Update()
        {
            base.Update();
            FiberManager.Update();
            UpdateChildrenAttachment();
        }

        public void UpdateChildrenAttachment()
        {
            if (!Left.IsDead)
                Left.Physics.Position = Physics.Position + new Vector2(-140.0f, 50.0f);
            if (!Right.IsDead)
                Right.Physics.Position = Physics.Position + new Vector2(+140.0f, 50.0f);
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
            while (true)
            {
                yield return 30;

                int attack_type = World.Random.Next() % 4;
                switch (attack_type)
                {
                    case 0:
                        RegularShot(Physics.Position, Vector2.UnitY);
                        yield return 4;
                        RegularShot(Physics.Position, Vector2.UnitY);
                        yield return 4;
                        RegularShot(Physics.Position, Vector2.UnitY);
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

                    case 2:    
                        break;

                    case 3:
                        break;
                }


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

