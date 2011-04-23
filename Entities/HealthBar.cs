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
                else
                    continue;
                break;
            }
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            if (Boss == null)
                return;

            float health = Boss.Health / Boss.HealthMax;
            int length = Math.Max((int)(580.0f * health), 0);

            Vector2 center = World.GameCamera.GetCenter().ToVector2();
            sprite_batch.Draw(World.Game.PixelTexture, new Rectangle((int)center.X - 350, (int)center.Y - 500, 600, 40), null, Color.DarkGray, 0.0f, Vector2.Zero, SpriteEffects.None, 0.9f);
            sprite_batch.Draw(World.Game.PixelTexture, new Rectangle((int)center.X - 340, (int)center.Y - 490, length, 20), null, Color.LightGray, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
        }
    }
}
