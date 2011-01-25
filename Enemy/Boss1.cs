//
// Boss1.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Galaxy
{
    public class CBoss1
        : CEnemy
    {
        public List<CEnemy> Enemies { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = null;
            Visual = null;
            HealthMax = 10.0f;
            Coins = 0;
            BaseScore = 0;
        }

        public void CollectEnemies()
        {
            Enemies = new List<CEnemy>();
            foreach (CEntity entity in World.GetEntities())
            {
                if (entity.GetType() == typeof(CTurret) ||
                    entity.GetType() == typeof(CDownTurret))
                {
                    Vector2 delta = Physics.Position - entity.Physics.Position;
                    if (delta.Y > 0)
                        Enemies.Add(entity as CEnemy);    
                }
            }

            foreach (CEnemy enemy in Enemies)
            {
                enemy.BaseScore = 0;
                enemy.Coins = 0;
                enemy.HealthMax = 10000.0f;    
            }
        }

        public override void Update()
        {
            if (Enemies == null)
                CollectEnemies();

            float accumulated = 0.0f;
            foreach (CEnemy enemy in Enemies)
            {
                accumulated += 10000.0f - enemy.Health;
                enemy.Health = 10000.0f;
            }

            if (accumulated > 0.0f)
                TakeDamage(accumulated, null);

            base.Update();
        }

        protected override void OnDie()
        {
            foreach (CEnemy enemy in Enemies)
            {
                enemy.Die();    
            }

            Enemies.Clear();
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);

            sprite_batch.DrawString(World.Game.GameRegularFont, "health:" + Health, World.GameCamera.GetCenter().ToVector2(), Color.White);

            DrawHealthBar(sprite_batch);
            // draw health bar
        }

        public void DrawHealthBar(SpriteBatch sprite_batch)
        {
            Vector2 center = World.GameCamera.GetCenter().ToVector2();
            sprite_batch.Draw(World.Game.PixelTexture, new Rectangle((int)center.X - 350, (int)center.Y - 500, 600, 40), null, Color.DarkGray, 0.0f, Vector2.Zero, SpriteEffects.None, 0.9f);
            sprite_batch.Draw(World.Game.PixelTexture, new Rectangle((int)center.X - 340, (int)center.Y - 490, 580, 20), null, Color.LightGray, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
        }

        public override void UpdateCollision()
        {
        }
    }
}

