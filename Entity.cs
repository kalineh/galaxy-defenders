//
// Entity.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace galaxy
{
    public class CEntity
    {
        public CWorld World { get; set; }
        public String Name { get; set; }
        public CPhysics Physics { get; set; }
        public CVisual Visual { get; set; }
        public Collision Collision { get; set; }

        public CEntity(CWorld world, String name)
        {
            World = world;
            Name = name;
            Physics = null;
            Visual = null;
            Collision = null;
        }

        public virtual void Update()
        {
            if (Physics != null)
            {
                Physics.PositionPhysics.Solve();
                Physics.AnglePhysics.Solve();
            }

            if (Collision != null)
            {
                UpdateCollision();
            }

            if (Visual != null)
            {
                Visual.Update();
            }
        }

        public virtual void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
            circle.Radius = Visual.GetScaledTextureSize().Length() * 0.2f;
        }

        public virtual void Draw(SpriteBatch sprite_batch)
        {
            Visual.Draw(sprite_batch, Physics.PositionPhysics.Position, Physics.AnglePhysics.Rotation);
        }

        public virtual void Delete()
        {
            World.EntityDelete(this);
        }

        public virtual void Die()
        {
            OnDie();
            World.EntityDelete(this);
        }

        protected virtual void OnDie()
        {
            // nothing
        }

        protected void ClampPositionToScreen()
        {
            Vector2 size = Visual.GetScaledTextureSize() / 2.0f;
            Viewport viewport = World.Game.GraphicsDevice.Viewport;
            Vector2 clamped = Physics.PositionPhysics.Position;

            clamped.X = Math.Max(viewport.X + size.X, clamped.X);
            clamped.Y = Math.Max(viewport.Y + size.Y, clamped.Y);
            clamped.X = Math.Min(viewport.X + viewport.Width - size.X, clamped.X);
            clamped.Y = Math.Min(viewport.Y + viewport.Height - size.Y, clamped.Y);

            Physics.PositionPhysics.Position = clamped;
        }

        protected bool IsInScreen()
        {
            Viewport viewport = World.Game.GraphicsDevice.Viewport;
            CollisionAABB box = new CollisionAABB(
                new Vector2(viewport.X, viewport.Y),
                new Vector2(viewport.Width, viewport.Height)
            );

            return Collision.Intersects(box);
        }

        protected bool IsOffScreenBottom()
        {
            if (Physics.PositionPhysics.Position.Y > World.Game.GraphicsDevice.Viewport.Height + 200)
            {
                return true;
            }
            return false;
        }
    }
}
