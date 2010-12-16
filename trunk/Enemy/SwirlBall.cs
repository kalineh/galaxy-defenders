//
// SwirlBall.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Galaxy
{
    public class CSwirlBall
        : CEnemy
    {
        public int Index { get; set; }
        public CSwirl Owner { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 16.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/SwirlBall");
            HealthMax = 1.0f;
        }

        public override void UpdateCollision()
        {
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }

        public override void TakeDamage(float damage, CShip source)
        {
            if (!Owner.IsDead)
            {
                Owner.TakeDamage(damage, source);
                return;
            }

            base.TakeDamage(damage, source);
        }
    }
}

