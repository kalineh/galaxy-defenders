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
    public class CBoss1
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 170.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss1");
            HealthMax = 30.0f;
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

        protected override void OnDie()
        {
            FiberManager.KillAll();
            World.DestroyAllProjectiles();
            World.DestroyAllEnemies();

            for (int i = 0; i < 30; ++i)
            {
                World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position + World.Random.NextVector2() * World.Random.NextSignedFloat() * 240.0f, CEnemy.EnemyOrangeColor, 1.25f, null);
                World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position + World.Random.NextVector2() * World.Random.NextSignedFloat() * 240.0f, CEnemy.EnemyGrayColor, 1.25f, null);
            }
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);

            sprite_batch.DrawString(World.Game.GameRegularFont, "health:" + Health, World.GameCamera.GetCenter().ToVector2(), Color.White);
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
                yield return 60;
                Console.WriteLine("Tick");
            }
        }
    }
}

