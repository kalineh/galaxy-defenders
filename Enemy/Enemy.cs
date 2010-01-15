//
// Enemy.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace galaxy
{
    public class CEnemy
        : CEntity
    {
        public CEnemy(CWorld world, String name)
            : base(world, name)
        {
            Physics = new CPhysics();
        }

        public virtual void UpdateAI()
        {
            float t = World.Game.GameFrame * 0.05f;
            float x = (float)Math.Cos(t) * 4.0f;
            float y = 2.0f;
            Physics.PositionPhysics.Velocity = new Vector2(x, y);
        }

        public override void Update()
        {
            UpdateAI();

            base.Update();

            if (IsOffScreenBottom())
                Die();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
            circle.Radius = Visual.GetScaledTextureSize().Length() * 0.2f;
        }

        public void OnCollide(CShip ship)
        {
            ship.Die();
        }
    }
}

