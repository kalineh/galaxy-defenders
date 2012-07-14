//
// Boss9.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Galaxy
{
    public class CBoss9Ball
        : CEnemy
    {
        public CBoss9 Boss { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 32.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss9Ball");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            HealthMax = 1000.0f;
            Coins = 0;
            BaseScore = 0;
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle collision = Collision as CollisionCircle;
            collision.Position = Physics.Position;
        }
    }

    public class CBoss9Bar
        : CEnemy
    {
        public CBoss9 Boss { get; set; }
        public CFiberManager FiberManager { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss9Bar");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -2.0f;

            FiberManager = new CFiberManager();
            FiberManager.Fork(this.UpdateBallLeft);
            FiberManager.Fork(this.UpdateBallRight);

            Coins = 0;
        }

        public override void Update()
        {
            base.Update();
            FiberManager.Update();
        }

        public IEnumerable UpdateBallLeft()
        {
            return UpdateBall(-1.0f);
        }

        public IEnumerable UpdateBallRight()
        {
            return UpdateBall(+1.0f);
        }

        public IEnumerable UpdateBall(float direction)
        {
            const float distance = 120.0f;

            CBoss9Ball ball = new CBoss9Ball() { Boss = Boss };
            ball.Initialize(World);
            World.EntityAdd(ball);

            float move = 1.0f;
            float fire = World.Random.NextFloat() * -3.0f;

            while (true)
            {
                while (ball.IsDead == false)
                {
                    yield return 0;

                    Vector2 normal = Physics.GetDir() * direction;
                    Vector2 offset = normal * distance * move;
                    ball.Physics.Position = Physics.Position + offset;
                    move = MathHelper.Min(move + 1.0f / 60.0f * 2.0f, 1.0f);
                    fire += 1.0f / 60.0f;

                    // check fire
                    if (fire >= 1.0f)
                    {
                        Vector2 perp = normal.Perp();
                        CShip nearest = World.GetNearestShip(ball.Physics.Position);
                        if (nearest == null)
                            continue;

                        Vector2 to = nearest.Physics.Position - ball.Physics.Position;
                        Vector2 dir = to.Normal();
                        bool front = Vector2.Dot(dir, perp) > 0.0f;
                        if (!front)
                            continue;

                        float dot = Vector2.Dot(dir, normal);
                        if (Math.Abs(dot) > 0.05f)
                            continue;

                        float speed = 1.0f - Boss.Health / Boss.HealthMax;
                        ball.Physics.Velocity = normal.Perp() * (4.0f + 6.0f * speed);
                        ball = null;
                        break;
                    }


                    // DEBUG
                    //if (CInput.IsRawKeyPressed(Microsoft.Xna.Framework.Input.Keys.A))
                        //ball.Die();    
                }

                yield return 60;

                ball = new CBoss9Ball() { Boss = Boss };
                ball.Initialize(World);
                World.EntityAdd(ball);
                move = 0.0f;
                fire = World.Random.NextFloat() * -2.0f * Boss.Health / Boss.HealthMax;
            }
        }
    }

    public class CBoss9
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss9Bar Bar { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 75.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss9");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -0.0f;
            HealthMax = 210.0f * CDifficulty.BossHealthScale[world.CachedDifficulty];
            Coins = 0;
            BaseScore = 0;

            FiberManager = new CFiberManager();
            FiberManager.Fork(this.UpdateWeapons0);
            FiberManager.Fork(this.UpdateMovement);
            FiberManager.Fork(this.UpdateChildren);
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
            Bar = new CBoss9Bar() { Boss = this };
            Bar.Initialize(World);
            World.EntityAdd(Bar);
        }

        public IEnumerable UpdateChildren()
        {
            while (true)
            {
                float d = 1.0f - Health / HealthMax;
                float speed = 0.020f + 0.075f * d;

                Bar.Physics.Position = Physics.Position;
                Bar.Physics.Rotation = MathHelper.WrapAngle(Bar.Physics.Rotation + speed);

                yield return 0;
            }
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
            CollisionCircle collision = Collision as CollisionCircle;
            collision.Position = Physics.Position;
        }

        private IEnumerable UpdateWeapons0()
        {
            yield return 60;

            while (true)
            {
                yield return 60;

                int attack_type = World.Random.Next() % 4;

                switch (attack_type)
                {
                    case 0:
                        for (int k = 0; k < 4; ++k)
                        {
                            float skew = -0.4f + k * 0.2f;
                            for (int j = 0; j < 6; ++j)
                            {
                                for (int i = 0; i < 4; ++i)
                                {
                                    int index = i * 2 + j % 2;
                                    Vector2 dir = -Vector2.UnitY.Rotate(MathHelper.TwoPi / 8.0f * index + skew);
                                    Fire(index, dir);
                                }

                                yield return 2;
                            }

                            yield return 6;
                        }
                        break;

                    case 1:
                    {
                        for (int j = 0; j < 4; ++j)
                        {
                            Vector2 dir = GetDirToShipWithLead(10.0f);
                            for (int i = 0; i < 8; ++i)
                            {
                                Fire(i, dir);
                            }

                            yield return 15;
                        }
                        break;
                    }

                    case 2:
                    {
                        for (int j = 0; j < 6; ++j)
                        {
                            for (int i = 0; i < 8; ++i)
                            {
                                Vector2 dir = Vector2.UnitX.Rotate(MathHelper.TwoPi / 8.0f * i);
                                Fire(i, dir);
                            }

                            yield return 6;
                        }
                        break;
                    }

                    case 3:
                    {
                        for (int i = 0; i < 32; ++i)
                        {
                            int index = i % 8;
                            Vector2 dir = Vector2.UnitX.Rotate(MathHelper.TwoPi / 8.0f * index);
                            Fire(index, dir);
                            yield return 2;
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
                Vector2 target = new Vector2(
                    (float)Math.Sin(AliveTime * 0.01f) * 200.0f,
                    (float)-Math.Sin(AliveTime * 0.02f) * 80.0f
                );

                Physics.Position = Vector2.Lerp(Physics.Position, base_ + target, 0.1f);
                yield return 0;
            }
        }

        private void Fire(int index, Vector2 direction)
        {
            float rotation = MathHelper.TwoPi / 8.0f * index;
            Vector2 cannon = -Vector2.UnitY.Rotate(rotation) * 40.0f;
            Vector2 position = Physics.Position + cannon;
            CEnemyShot shot = CEnemyShot.Spawn(World, position, direction.ToAngle(), 8, 3.0f);
            CAudio.PlaySound("EnemyShoot");
        }
    }
}
            