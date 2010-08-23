using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CDetonation
        : CEntity
    {
        public static CDetonation MakeDetonation(CWorld world, Vector2 position)
        {
            CDetonation result = new CDetonation();
            result.Initialize(world);
            result.Physics.PositionPhysics.Position = position;
            world.EntityAdd(result);
            return result;
        }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);
            
            World = world;
            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 128.0f);
        }

        public override void Update()
        {
            if (AliveTime <= 0)
                CEffect.DetonationEffect(World, Physics.PositionPhysics.Position);

            base.Update();

            if (AliveTime > 3)
                Die();
        }

        public override void UpdateCollision()
        {
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
            circle.Radius = 128.0f;
        }
    }
}