//
// Boss4.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Galaxy
{
    public class CBoss4Chunk
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss4 Boss { get; set; }
        public Vector2 CollisionOffset { get; set; }
        public Vector2 FireOffset { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(70.0f, 70.0f));
            Visual = CVisual.MakeSpriteUncached(world.Game, "Textures/Enemy/Boss4Chunk");
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
            Boss.TakeDamageDirect(damage, source);
        }

        public IEnumerable UpdateWeaponsPattern0()
        {
            while (true)
            {
                RegularShot(Physics.Position + FireOffset, Vector2.UnitY);
                yield return 30;
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB collision = Collision as CollisionAABB;
            collision.Position = Physics.Position + CollisionOffset;
        }

        private void RegularShot(Vector2 position, Vector2 direction)
        {
            float rotation = direction.ToAngle();
            CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, 8.0f, 2.0f);
            CAudio.PlaySound("EnemyShoot");
        }
    }

    public class CBoss4
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss4Chunk Center { get; set; }
        public CBoss4Chunk Left { get; set; }
        public CBoss4Chunk Right { get; set; }
        public float CenterOut { get; set; }
        public float LeftOut { get; set; }
        public float RightOut { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(260.0f, 215.0f));
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss4");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            HealthMax = 30.0f;
            Coins = 0;
            BaseScore = 0;
            CenterOut = 0.0f;
            LeftOut = 0.0f;
            RightOut = 0.0f;

            FiberManager = new CFiberManager();
            FiberManager.Fork(this.UpdateMovement);
            FiberManager.Fork(this.UpdateWeapons);
            FiberManager.Fork(this.UpdateCenter);
            FiberManager.Fork(this.UpdateLeft);
            FiberManager.Fork(this.UpdateRight);
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
            Center = new CBoss4Chunk() { Boss = this };
            Left = new CBoss4Chunk() { Boss = this };
            Right = new CBoss4Chunk() { Boss = this };

            Center.Initialize(World);
            Left.Initialize(World);
            Right.Initialize(World);

            Center.Physics.Rotation = MathHelper.PiOver2;
            Right.Physics.Rotation = MathHelper.Pi;

            Center.CollisionOffset = new Vector2(-36.0f, +140.0f);
            Left.CollisionOffset = new Vector2(+140.0f, -32.0f);
            Right.CollisionOffset = new Vector2(-214.0f, -32.0f);

            Center.FireOffset = new Vector2(0.0f, -94.0f);
            Left.FireOffset = new Vector2(-178.0f, -34.0f);
            Right.FireOffset = new Vector2(+249.0f, -34.0f);

            Center.Visual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            Left.Visual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            Right.Visual.NormalizedOrigin = new Vector2(0.0f, 0.5f);

            Center.Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -2.0f;
            Left.Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -2.0f;
            Right.Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -2.0f;
            
            World.EntityAdd(Center);
            World.EntityAdd(Left);
            World.EntityAdd(Right);

            UpdateChildrenAttachment();
        }

        private void UpdateChildrenAttachment()
        {
            Center.Physics.Position = Physics.Position + Vector2.UnitY * -140.0f + Vector2.UnitY * -CenterOut;
            Left.Physics.Position = Physics.Position + Vector2.UnitX * -140.0f + Vector2.UnitX * -LeftOut;
            Right.Physics.Position = Physics.Position + Vector2.UnitX * +140.0f + Vector2.UnitX * +RightOut;
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
            CollisionAABB collision = Collision as CollisionAABB;
            collision.Position = Physics.Position + new Vector2(-130.0f, -120.0f);
        }

        public override void TakeDamage(float damage, CShip source)
        {
            // suppress damage
            //base.TakeDamage(damage, source);
            //Vector2 position = projectile position?
            //World.ParticleEffects.Spawn(EParticleType.WeaponIneffective, position);
        }

        // TODO: maybe need to OnCollide with everything here
        
        public void TakeDamageDirect(float damage, CShip source)
        {
            base.TakeDamage(damage, source);
        }
        
        private IEnumerable UpdateWeapons()
        {
            yield return 60;
            yield break;

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
            }
        }

        private IEnumerable UpdateCenter()
        {
            while (true)
            {
                yield return 60;    

                for (int i = 0; i < 60; ++i)
                {
                    CenterOut = Interpolation.MoveToValue(CenterOut, -100.0f, 0.05f);
                    yield return 0;
                }

                Center.FiberManager.Fork(Center.UpdateWeaponsPattern0);
                yield return 240;
                Center.FiberManager.Kill(Center.UpdateWeaponsPattern0);

                for (int i = 0; i < 60; ++i)
                {
                    CenterOut = Interpolation.MoveToValue(CenterOut, 0.0f, 0.05f);
                    yield return 0;
                }
            }
        }

        private IEnumerable UpdateLeft()
        {
            while (true)
            {
                yield return 60;    

                for (int i = 0; i < 60; ++i)
                {
                    LeftOut = Interpolation.MoveToValue(LeftOut, -140.0f, 0.05f);
                    yield return 0;
                }

                Center.FiberManager.Fork(Center.UpdateWeaponsPattern0);
                yield return 240;
                Center.FiberManager.Kill(Center.UpdateWeaponsPattern0);

                for (int i = 0; i < 60; ++i)
                {
                    LeftOut = Interpolation.MoveToValue(LeftOut, 0.0f, 0.05f);
                    yield return 0;
                }
            }
        }

        private IEnumerable UpdateRight()
        {
            while (true)
            {
                yield return 60;    

                for (int i = 0; i < 60; ++i)
                {
                    RightOut = Interpolation.MoveToValue(RightOut, -140.0f, 0.05f);
                    yield return 0;
                }

                Center.FiberManager.Fork(Center.UpdateWeaponsPattern0);
                yield return 240;
                Center.FiberManager.Kill(Center.UpdateWeaponsPattern0);

                for (int i = 0; i < 60; ++i)
                {
                    RightOut = Interpolation.MoveToValue(RightOut, 0.0f, 0.05f);
                    yield return 0;
                }
            }
        }

        private IEnumerable UpdateMovement()
        {
            Vector2 base_ = Physics.Position;
            while (true)
            {
                Vector2 target = new Vector2(
                    (float)Math.Sin(AliveTime * 0.01f) * 40.0f,
                    (float)-Math.Sin(AliveTime * 0.02f) * 20.0f
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

