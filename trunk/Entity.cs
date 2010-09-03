//
// Entity.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CEntity
    {
        public CWorld World { get; set; }
        public CPhysics Physics { get; set; }
        public CVisual Visual { get; set; }
        public CCollision Collision { get; set; }
        public CMover Mover { get; set; }
        public int AliveTime { get; protected set; }
        public bool IsDead { get; set; }

        public virtual void Initialize(CWorld world)
        {
            World = world;
            Physics = null;
            Visual = null;
            Collision = null;
            Mover = null;
            AliveTime = 0;
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
            }

            if (Collision != null)
            {
                UpdateCollision();
            }

            if (Visual != null)
            {
                Visual.Update();
            }

            AliveTime += 1;
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

#if DEBUG
            if (CInput.IsRawKeyDown(Keys.L))
            {
                if (Visual != null)
                {
                    Visual.DebugText = GetType().ToString().Replace("Galaxy.", "");
                    Visual.DebugTextClampToScreen = true;
                }
            }
#endif
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
            // TODO: is this a good result?
            if (Physics == null)
                return false;

            // some enemies travel up after appearing!
            if (World.GameCamera.IsAboveActiveRegionForDeath(Physics.PositionPhysics.Position))
                return true;

            if (World.GameCamera.IsAboveActiveRegion(Physics.PositionPhysics.Position))
                return false;

            // TODO: bottom check stricter than 150 side check
            if (IsOffScreenBottom())
                return true;

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
            if (Collision != null)
            {
                CollisionCircle circle = Collision as CollisionCircle;
                if (circle != null)
                    return circle.Radius;

                CollisionAABB aabb = Collision as CollisionAABB;
                if (aabb != null)
                    return Math.Max(aabb.Size.X, aabb.Size.Y) * 0.5f;
            }

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
            IsDead = true;
            OnDie();
            World.EntityDelete(this);

            // TODO: making these null will make things crash
            // TODO: maybe make an interface for this that handles death state?
            // TODO: maybe the collision system and tree lookup should explicitly handle deadness
            // TODO: will it be ok to leave just physics on?
            //Physics = null;
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
