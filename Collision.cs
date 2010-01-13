//
// Collision.cs
//

using System;
using Microsoft.Xna.Framework;

// TODO: type-type mapping for collision
// TODO: collision results retrieval
// TODO: !!! cache bounding objects

namespace galaxy
{
    public class Collision
    {
        public bool Enabled { get; set; }
        public bool IgnoreSelfType { get; set; }

        public Collision()
        {
            Enabled = true;
            IgnoreSelfType = true;
        }

        public virtual bool Intersects(Collision vs)
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
                    return CircleCircle((CollisionCircle)this, (CollisionCircle)vs);
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
            BoundingSphere sphere_a = new BoundingSphere(a.Position.xy0(), a.Radius);
            BoundingSphere sphere_b = new BoundingSphere(b.Position.xy0(), b.Radius);
            return sphere_a.Intersects(sphere_b);
        }

        private static bool BoxBox(CollisionAABB a, CollisionAABB b)
        {
            BoundingBox box_a = new BoundingBox(a.Position.xy0(), a.Position.xy0() + a.Size.xy0());
            BoundingBox box_b = new BoundingBox(b.Position.xy0(), b.Position.xy0() + b.Size.xy0());
            return box_a.Intersects(box_b);
        }

        private static bool CircleBox(CollisionCircle a, CollisionAABB b)
        {
            BoundingSphere sphere_a = new BoundingSphere(a.Position.xy0(), a.Radius);
            BoundingBox box_b = new BoundingBox(b.Position.xy0(), b.Position.xy0() + b.Size.xy0());
            return sphere_a.Intersects(box_b);
        }

        // reverse
        private static bool BoxCircle(CollisionAABB a, CollisionCircle b)
        {
            return CircleBox(b, a);
        }
    }

    public class CollisionCircle
        : Collision
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
        : Collision
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
            BoundingBox box = new BoundingBox(Position.xy0(), Position.xy0() + Size.xy0());
            return box.Contains(point.xy0()) == ContainmentType.Contains;
        }
    }
}
