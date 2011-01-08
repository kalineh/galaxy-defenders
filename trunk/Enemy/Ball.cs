//
// Ball.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Galaxy
{
    public class CBall
        : CEnemy
    {
        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 32.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Ball");
            HealthMax = 0.5f;
            Coins = 1;
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }
    }
}

