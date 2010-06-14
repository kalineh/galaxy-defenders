using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CDetonation
        : CEntity
    {
        public static CDetonation MakeDetonation(CWorld world, Vector2 position)
        {
            CDetonation result = new CDetonation(world, position);
            world.EntityAdd(result);
            return result;
        }

        private CDetonation(CWorld world, Vector2 position)
            : base(world)
        {
            World = world;
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = CCollision.GetCacheCircle(this, position, 128.0f);
        }

        public override void Update()
        {
            if (AliveTime <= 0)
                CEffect.DetonationEffect(World, Physics.PositionPhysics.Position);

            base.Update();

            if (AliveTime > 3)
                Die();
        }
    }
}