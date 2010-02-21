//
// Entity.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CEntity
    {
        public CWorld World { get; set; }
        public CPhysics Physics { get; set; }
        public CVisual Visual { get; set; }
        public Collision Collision { get; set; }
        public CMover Mover { get; set; }
        public float AliveTime { get; protected set; }
        public bool IgnoreCameraScroll { get; set; }

        public CEntity(CWorld world)
        {
            World = world;
            Physics = null;
            Visual = null;
            Collision = null;
            Mover = null;
            AliveTime = 0.0f;
        }

        public virtual void Update()
        {
            if (Mover != null)
            {
                Mover.Move(this);
            }

            if (Physics != null)
            {
                Physics.PositionPhysics.Solve();
                Physics.AnglePhysics.Solve();

                if (IgnoreCameraScroll)
                {
                    if (World.Game.State.GetType() == typeof(CStateGame))
                    {
                        Physics.PositionPhysics.Position += World.Game.StageDefinition.ScrollSpeed * -Vector2.UnitY;
                    }
                }
            }

            if (Collision != null)
            {
                UpdateCollision();
            }

            if (Visual != null)
            {
                Visual.Update();
            }

            AliveTime += Time.SingleFrame;
        }

        public virtual void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
            circle.Radius = GetRadius() * 0.75f;
        }

        public virtual void Draw(SpriteBatch sprite_batch)
        {
            if (Visual != null)
            {
                Visual.Draw(sprite_batch, Physics.PositionPhysics.Position, Physics.AnglePhysics.Rotation);
            }
        }

        public void ClampInsideScreen()
        {
            Physics.PositionPhysics.Position = World.GameCamera.ClampInside(Physics.PositionPhysics.Position, GetRadius());
        }

        public bool IsInScreen(float buffer)
        {
            return World.GameCamera.IsInside(Physics.PositionPhysics.Position, GetRadius() + buffer);
        }

        public bool IsInScreen()
        {
            return IsInScreen(0.0f);
        }

        public bool IsInDieRegion()
        {
            if (Physics.PositionPhysics.Position.Y < 0.0f)
            {
                return false;
            }

            // TODO: proper region top check
            // TODO: proper edge buffer system
            return !IsInScreen(150.0f);
        }

        public bool IsOffScreenBottom()
        {
            return World.GameCamera.IsOffBottom(Physics.PositionPhysics.Position, GetRadius());
        }

        public Vector2 GetDirToShip()
        {
            Vector2 position = Physics.PositionPhysics.Position;
            CShip ship = World.GetNearestShip(position);
            if (ship == null)
            {
                return Vector2.UnitY;
            }

            Vector2 offset = ship.Physics.PositionPhysics.Position - position;
            Vector2 dir = offset.Normal();

            return dir;
        }


        public virtual float GetRadius()
        {
            if (Visual == null)
                return 0.0f;
            float texture = Math.Max(Visual.Texture.Width, Visual.Texture.Height);
            float scale = Math.Max(Visual.Scale.X, Visual.Scale.Y);
            return texture * scale * 0.5f;
        }

        public virtual void Delete()
        {
            World.EntityDelete(this);
        }

        public virtual void Die()
        {
            OnDie();
            World.EntityDelete(this);

            Physics = null;
            Visual = null;
            Collision = null;
            Mover = null;
        }

        protected virtual void OnDie()
        {
            // nothing
        }
    }
}
