//
// Boss2.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Galaxy
{
    public class CBoss2
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(420.0f, 160.0f));
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss2");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            HealthMax = 70.0f * CDifficulty.BossHealthScale[world.CachedDifficulty];
            Coins = 0;
            BaseScore = 0;

            FiberManager = new CFiberManager();
            FiberManager.Fork(this.UpdateWeapons);
            FiberManager.Fork(this.UpdateMovement);
        }

        public override void Update()
        {
            base.Update();
            FiberManager.Update();
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
            collision.Position = Physics.Position + new Vector2(-210.0f, -80.0f);
        }
        
        private IEnumerable UpdateWeapons()
        {
            yield return 60;

            while (true)
            {
                yield return 60;

                int attack_type = World.Random.Next() % 4;

                switch (attack_type)
                {
                    case 0:
                        FireMainCannon();
                        yield return 3;
                        FireMainCannon();
                        yield return 3;
                        FireMainCannon();
                        yield return 3;
                        FireMainCannon();
                        yield return 3;
                        FireMainCannon();
                        yield return 3;
                        FireMainCannon();
                        yield return 3;
                        FireMainCannon();
                        yield return 3;
                        FireMainCannon();
                        yield return 3;
                        FireMainCannon();
                        yield return 8;
                        FireLeftBall();
                        FireRightBall();
                        yield return 8;
                        break;

                    case 1:    
                        FireLeftCannon();
                        FireRightCannon();
                        yield return 4;
                        FireLeftCannon();
                        FireRightCannon();
                        yield return 4;
                        FireLeftCannon();
                        FireRightCannon();
                        yield return 4;
                        FireLeftCannon();
                        FireRightCannon();
                        yield return 4;
                        FireLeftCannon();
                        FireRightCannon();
                        yield return 4;
                        break;

                    case 2:
                        FireLeftBall();
                        FireRightBall();
                        FireLeftCannon();
                        FireRightCannon();
                        yield return 4;
                        FireLeftCannon();
                        FireRightCannon();
                        yield return 4;
                        FireLeftCannon();
                        FireRightCannon();
                        yield return 4;
                        break;

                    case 3:
                        FireLeftBall();
                        FireRightBall();
                        yield return 16;
                        FireLeftBall();
                        FireRightBall();
                        yield return 16;
                        FireLeftBall();
                        FireRightBall();
                        yield return 16;
                        break;
                }
            }
        }

        private IEnumerable UpdateMovement()
        {
            yield return 60;

            const float Movement = 250.0f;

            float src = Physics.Position.X;
            float dst = Physics.Position.X - Movement / 2.0f;

            for (int i = 0; i < 30; ++i)
            {
                float t = 1.0f / 30.0f * (float)i;
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

                        for (int i = 0; i < 60; ++i)
                        {
                            float t = 1.0f / 60.0f * (float)i;
                            Physics.Position = new Vector2(
                                MathHelper.SmoothStep(src, dst, t),
                                Physics.Position.Y
                            );
                            yield return 0;
                        }

                        yield return 30;

                        src = Physics.Position.X;
                        dst = Physics.Position.X - Movement;

                        for (int i = 0; i < 60; ++i)
                        {
                            float t = 1.0f / 60.0f * (float)i;
                            Physics.Position = new Vector2(
                                MathHelper.SmoothStep(src, dst, t),
                                Physics.Position.Y
                            );
                            yield return 0;
                        }

                        yield return 30 + World.Random.Next() % 60;

                        break;
                }
            }
        }

        private void FireLeftBall()
        {
            Vector2 position = Physics.Position + new Vector2(-146.0f, 32.0f);
            for (int i = 0; i < 16; ++i)
            {
                float t = MathHelper.TwoPi / 16.0f * i;
                CEnemyShot shot = CEnemyShot.Spawn(World, position, t, 8, 3.0f);
                CAudio.PlaySound("EnemyShoot");
            }
        }

        private void FireRightBall()
        {
            Vector2 position = Physics.Position + new Vector2(+146.0f, 32.0f);
            for (int i = 0; i < 16; ++i)
            {
                float t = MathHelper.TwoPi / 16.0f * i;
                CEnemyShot shot = CEnemyShot.Spawn(World, position, t, 8, 3.0f);
                CAudio.PlaySound("EnemyShoot");
            }
        }

        private void FireLeftCannon()
        {
            float rotation = MathHelper.PiOver2;
            Vector2 position = Physics.Position + new Vector2(-64.0f, 110.0f);
            CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, 8, 3.0f);
            CAudio.PlaySound("EnemyShoot");
        }

        private void FireRightCannon()
        {
            float rotation = MathHelper.PiOver2;
            Vector2 position = Physics.Position + new Vector2(+64.0f, 110.0f);
            CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, 8, 3.0f);
            CAudio.PlaySound("EnemyShoot");
        }

        private void FireMainCannon()
        {
            float rotation = MathHelper.PiOver2;
            Vector2 position = Physics.Position + new Vector2(0.0f, 132.0f);
            CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, 8, 3.0f);
            CAudio.PlaySound("EnemyShoot");
        }
    }
}

