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
            result.Physics.Position = position;
            owner.World.EntityAdd(result);
            return result;
        }

        public void Initialize(CShip owner)
        {
            base.Initialize(owner.World);

            Owner = owner;
            World = owner.World;
            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 48.0f);
        }

        public override void Update()
        {
            if (AliveTime <= 0)
                World.ParticleEffects.Spawn(EParticleType.SkillDetonationExplosion, Physics.Position, Owner.PlayerColor, null, null);

            base.Update();

            if (AliveTime > 3)
                Die();
        }

        public override void UpdateCollision()
        {
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
            circle.Radius = 48.0f;
        }
    }
}