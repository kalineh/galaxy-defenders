using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CDetonation
        : CEntity
    {
        public CShip Owner { get; set; }

        public static CDetonation MakeDetonation(CShip owner, Vector2 position)
        {
            CDetonation result = new CDetonation();
            result.Initialize(owner);
            result.Physics.PositionPhysics.Position = position;
            owner.World.EntityAdd(result);
            return result;
        }

        public void Initialize(CShip owner)
        {
            base.Initialize(owner.World);

            Owner = owner;
            World = owner.World;
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