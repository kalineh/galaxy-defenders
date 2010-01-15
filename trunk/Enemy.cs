//
// Enemy.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using galaxy;

namespace galaxy
{
    public class CEnemy
        : CEntity
    {
        public static CEnemy Spawn(CWorld world, Vector2 position)
        {
            Texture2D texture = world.Game.Content.Load<Texture2D>("Enemy");
            CEnemy enemy = new CEnemy(world, "Enemy", texture);

            enemy.Physics.PositionPhysics.Position = position;

            world.EntityAdd(enemy);

            return enemy;
        }

        public CEnemy(CWorld world, String name, Texture2D texture)
            : base(world, name, texture)
        {
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

