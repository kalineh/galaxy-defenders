//
// Collision.cs
//

using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

// TODO: type-type mapping for collision
// TODO: collision results retrieval
// TODO: !!! cache bounding objects

namespace Galaxy
{
    public class CCollision
    {
        private static Dictionary<object, CollisionCircle> CollisionCircleCache { get; set; }
        private static Dictionary<object, CollisionAABB> CollisionAABBCache { get; set; }

        static CCollision()
        {
            ClearCache();
        }

        public static void ClearCache()
        {
            CollisionCircleCache = new Dictionary<object, CollisionCircle>(2048);
            CollisionAABBCache = new Dictionary<object, CollisionAABB>(2048);
        }

        public static CollisionCircle GetCacheCircle(object key, Vector2 position, float radius)
        {
            CollisionCircle result;
            bool had = CollisionCircleCache.TryGetValue(key, out result);
            if (!had)
            {
                result = new CollisionCircle(position, radius);
                CollisionCircleCache[key] = result;
                return result;
            }

            result.Position = position;
            result.Radius = radius;

            return result;
        }

        public static CollisionAABB GetCacheAABB(object key, Vector2 position, Vector2 size)
        {
            CollisionAABB result;
            bool had = CollisionAABBCache.TryGetValue(key, out result);
            if (!had)
            {
                result = new CollisionAABB(position, size);
                CollisionAABBCache[key] = result;
                return result;
            }

            result.Position = position;
            result.Size = size;

            return result;
        }


        public bool Enabled { get; set; }
        public bool IgnoreSelfType { get; set; }

        private BoundingSphere BoundingSphereCache { get; set; }
        private BoundingBox BoundingBoxCache { get; set; }

        public CCollision()
        {
            Enabled = true;
            IgnoreSelfType = true;
        }

        public virtual bool Intersects(CCollision vs)
        {
            // TODO: not this!
            if (GetType() == typeof(CollisionCircle))
            {
                if (vs.GetType() == typeof(CollisionCircle))
                {
                    return CircleCircle((CollisionCircle)this, (CollisionCircle)vs);
                }
                if (vs.GetType() == typeof(CollisionAABB))
                {
                    return CircleBox((CollisionCircle)this, (CollisionAABB)vs);
                }
            }
            if (GetType() == typeof(CollisionAABB))
            {
                if (vs.GetType() == typeof(CollisionCircle))
                {
                    return BoxCircle((CollisionAABB)this, (CollisionCircle)vs);
                }
                if (vs.GetType() == typeof(CollisionAABB))
                {
                    return BoxBox((CollisionAABB)this, (CollisionAABB)vs);
                }
            }

            throw new Exception("Collision.Test(): unhandled collision pair: " + GetType().ToString() + " vs " + vs.GetType().ToString());
        }

        private static bool CircleCircle(CollisionCircle a, CollisionCircle b)
        {
            float range = a.Radius * a.Radius + b.Radius * b.Radius;
            float lensqr = (a.Position - b.Position).LengthSquared();
            return lensqr < range;
        }

        private static bool BoxBox(CollisionAABB a, CollisionAABB b)
        {
            if (a.Position.X > b.Position.X + b.Size.X)
                return false;
            if (a.Position.Y > b.Position.Y + b.Size.Y)
                return false;
            if (a.Position.X + a.Size.X < b.Position.X)
                return false;
            if (a.Position.Y + a.Size.Y < b.Position.Y)
                return false;

            return true;
        }

        private static bool CircleBox(CollisionCircle a, CollisionAABB b)
        {
            if (a.Position.X + a.Radius < b.Position.X)
                return false;
            if (a.Position.Y + a.Radius < b.Position.Y)
                return false;
            if (a.Position.X - a.Radius > b.Position.X + b.Size.X)
                return false;
            if (a.Position.Y - a.Radius > b.Position.Y + b.Size.Y)
                return false;

            return true;
        }

        // reverse
        private static bool BoxCircle(CollisionAABB a, CollisionCircle b)
        {
            return CircleBox(b, a);
        }
    }

    public class CollisionCircle
        : CCollision
    {
        public Vector2 Position { get; set; }
        public float Radius { get; set; }

        public CollisionCircle(Vector2 position, float radius)
        {
            Position = position;
            Radius = radius;
        }
    }

    public class CollisionAABB
        : CCollision
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public CollisionAABB(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        public bool Contains(Vector2 point)
        {
            BoundingBox box = new BoundingBox(Position.xyz(-10000.0f), Position.xyz(-10000.0f) + Size.xyz(20000.0f));
            ContainmentType result = box.Contains(point.xy0());
            return result == ContainmentType.Contains;
        }

        public bool Contains(Vector2 point, float radius)
        {
            BoundingBox box = new BoundingBox(Position.xyz(-10000.0f), Position.xyz(-10000.0f) + Size.xyz(20000.0f));
            BoundingSphere sphere = new BoundingSphere(point.xy0(), radius);
            ContainmentType result = box.Contains(sphere);
            return result == ContainmentType.Contains;
        }
    }
}
