﻿//
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
#if DEBUG
        public static int NextID { get; set; }
        public int ID { get; set; }
#endif
        public CWorld World { get; set; }
        public CPhysics Physics { get; set; }
        public CVisual Visual { get; set; }
        public CCollision Collision { get; set; }
        public CMover Mover { get; set; }
        public int AliveTime { get; protected set; }
        public bool IsDead { get; set; }

        public virtual void Initialize(CWorld world)
        {
            //Console.WriteLine("Entity.Initialize(): ID: {0}, Old ID: {1}, Type: {2}", NextID + 1, ID, GetType().ToString());

#if DEBUG
            ID = NextID++;
#endif

            World = world;
            Physics = null;
            Visual = null;
            Collision = null;
            Mover = null;
            AliveTime = 0;
            IsDead = false;
        }

        public virtual void Update()
        {
            if (IsDead)
                System.Console.WriteLine("WARNING: updating dead object: {0}", this.GetType().ToString());

            if (Mover != null)
            {
                Mover.Move(this);
            }

            if (Physics != null)
            {
                Physics.Solve();
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
            circle.Position = Physics.Position;
            circle.Radius = GetRadius() * 0.75f;
        }

        public virtual void Draw(SpriteBatch sprite_batch)
        {
            if (Visual != null)
            {
                Visual.Draw(sprite_batch, Physics.Position, Physics.Rotation);
            }
        }

        public void DebugDraw(SpriteBatch sprite_batch)
        {
#if DEBUG
            if (CInput.IsRawKeyDown(Keys.L))
            {
                if (Visual != null)
                {
                    if (CInput.IsRawKeyDown(Keys.LeftShift))
                    {
                        Visual.DebugText = null;
                        Visual.DebugTextClampToScreen = false;
                    }
                    else
                    {
                        Visual.DebugText = GetType().ToString().Replace("Galaxy.", "");
                        Visual.DebugTextClampToScreen = true;
                    }
                }
            }
#endif
        }

        public void ClampInsideScreen()
        {
            Physics.Position = World.GameCamera.ClampInside(Physics.Position, GetRadius());
        }

        public bool IsInScreen(float buffer)
        {
            return World.GameCamera.IsInside(Physics.Position, GetRadius() + buffer);
        }

        public bool IsInScreen()
        {
            return IsInScreen(GetRadius());
        }

        public bool IsInDieRegion()
        {
            // TODO: is this a good result?
            if (Physics == null)
                return false;

            // some enemies travel up after appearing!
            if (World.GameCamera.IsAboveActiveRegionForDeath(Physics.Position))
                return true;

            if (World.GameCamera.IsAboveActiveRegion(Physics.Position))
                return false;

            // TODO: bottom check stricter than 150 side check
            if (IsOffScreenBottom())
                return true;

            // TODO: proper region top check
            // TODO: proper edge buffer system
            return !IsInScreen(150.0f);
        }

        public bool IsOffScreenBottom(float buffer)
        {
            return World.GameCamera.IsOffBottom(Physics.Position, GetRadius() + buffer);
        }

        public bool IsOffScreenBottom()
        {
            return World.GameCamera.IsOffBottom(Physics.Position, GetRadius());
        }

        public Vector2 GetDirToShip()
        {
            Vector2 position = Physics.Position;
            CShip ship = World.GetNearestShip(position);
            if (ship == null)
            {
                return Vector2.UnitY;
            }

            Vector2 offset = ship.Physics.Position - position;
            Vector2 dir = offset.Normal();

            return dir;
        }

        public Vector2 GetDirToShipWithLead(float lead)
        {
            Vector2 position = Physics.Position;
            CShip ship = World.GetNearestShip(position);
            if (ship == null)
            {
                return Vector2.UnitY;
            }

            Vector2 ship_position = ship.Physics.Position + ship.Physics.Velocity * lead;
            Vector2 offset = ship_position - position;
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
            //Console.WriteLine("Entity.Delete(): ID: {0}, IsDead: {1}, Type: {2}", ID, IsDead, GetType().ToString());

            IsDead = true;
            World.EntityDelete(this);
        }

        public virtual void Die()
        {
            //Console.WriteLine("Entity.Die(): ID: {0}, IsDead: {1}, Type: {2}", ID, IsDead, GetType().ToString());

            if (IsDead)
                return;

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
