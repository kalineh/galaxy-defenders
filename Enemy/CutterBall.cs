//
// CutterBall.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Galaxy
{
    public class CCutterBall
        : CEnemy
    {
        public CCutter Owner { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 24.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/CutterBall");
        }

        public override void UpdateCollision()
        {
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        public override void TakeDamage(float damage, CShip source)
        {
            // NOTE: ball absorbs damage entirely
            //Owner.TakeDamage(damage);
        }
    }
}

