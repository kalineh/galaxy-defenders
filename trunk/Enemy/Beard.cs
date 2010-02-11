//
// Beard.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CBeard
        : CEnemy
    {
        private Texture2D TextureBase { get; set; }
        private Texture2D TextureExposed { get; set; }

        public CBeard(CWorld world, Vector2 position)
            : base(world, "Beard")
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = new CollisionCircle(Vector2.Zero, 32.0f);
            TextureBase = CContent.LoadTexture2D(world.Game, "Textures/Enemy/Beard");
            TextureExposed = CContent.LoadTexture2D(world.Game, "Textures/Enemy/BeardExposed");
            Visual = new CVisual(TextureBase, Color.White);
            Health = 8.0f;
        }

        public override void UpdateAI()
        {
        }

        public override void Update()
        {
            UpdateAI();
            base.Update();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }

        public override void TakeDamage(float damage)
        {
            if (Visual.Texture == TextureBase)
            {
                Visual.Texture = TextureExposed;
                Mover = new CMoverFixedVelocity() { Velocity = Vector2.UnitY * 10.0f, };
                CExplosion.Spawn(World, Physics.PositionPhysics.Position, 1.0f);
            }
            base.TakeDamage(damage);
        }

    }
}

