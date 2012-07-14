//
// HealthBar.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Galaxy
{
    public class CHealthBar
        : CEntity
    {
        public CEnemy Boss { get; set; }
        public float Health { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = null;
            Visual = null;
            Mover = null;
        }

        public override void Update()
        {
            base.Update();

            FindBoss();

            if (Boss == null)
                return;

#if SOAK_TEST
            Boss.TakeDamage(1.0f, null);
#endif
        }

        public void FindBoss()
        {
            if (Boss != null)
                return;

            foreach (CEntity entity in World.GetEntities())
            {
                if (entity.GetType() == typeof(CBoss1))
                    Boss = entity as CEnemy;
                else if (entity.GetType() == typeof(CBoss2))
                    Boss = entity as CEnemy;
                else if (entity.GetType() == typeof(CBoss3))
                    Boss = entity as CEnemy;
                else if (entity.GetType() == typeof(CBoss4))
                    Boss = entity as CEnemy;
                else if (entity.GetType() == typeof(CBoss5))
                    Boss = entity as CEnemy;
                else if (entity.GetType() == typeof(CBoss6))
                    Boss = entity as CEnemy;
                else if (entity.GetType() == typeof(CBoss7))
                    Boss = entity as CEnemy;
                else if (entity.GetType() == typeof(CBoss8))
                    Boss = entity as CEnemy;
                else if (entity.GetType() == typeof(CBoss9))
                    Boss = entity as CEnemy;
                else if (entity.GetType() == typeof(CBoss10))
                    Boss = entity as CEnemy;
                else if (entity.GetType() == typeof(CBoss11))
                    Boss = entity as CEnemy;
                else if (entity.GetType() == typeof(CBoss12))
                    Boss = entity as CEnemy;
                else
                    continue;
                break;
            }
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            if (Boss == null)
                return;

            if (Boss.IsDead)
                return;

            float health = Boss.Health / Boss.HealthMax;
            int length = Math.Max((int)(580.0f * health), 0);

            Vector2 center = World.GameCamera.GetCenter().ToVector2();
            sprite_batch.Draw(World.Game.PixelTexture, new Rectangle((int)center.X - 350, (int)center.Y - 550, 600, 40), null, Color.DimGray, 0.0f, Vector2.Zero, SpriteEffects.None, 0.9f);
            sprite_batch.Draw(World.Game.PixelTexture, new Rectangle((int)center.X - 340, (int)center.Y - 540, 580, 20), null, Color.DarkGray, 0.0f, Vector2.Zero, SpriteEffects.None, 0.95f);
            sprite_batch.Draw(World.Game.PixelTexture, new Rectangle((int)center.X - 340, (int)center.Y - 540, length, 20), null, CEnemy.EnemyOrangeColor, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
        }
    }
}
