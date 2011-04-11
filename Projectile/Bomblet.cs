//
// Bomblet.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CBomblet
        : CProjectile
    {
        public static CBomblet Spawn(CShip owner, Vector2 position, float rotation, float speed, float damage)
        {
            CBomblet bomblet = new CBomblet();
            bomblet.Initialize(owner.World, owner, damage);

            //bomblet.Speed = speed;

            bomblet.Physics.Rotation = owner.World.Random.NextAngle();
            bomblet.Physics.Position = position;
            bomblet.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            owner.World.EntityAdd(bomblet);

            return bomblet;
        }

        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world, owner, damage);

            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/Bomblet", owner.GameControllerIndex);
            Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
            Visual.Update();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 4.0f);
            Damage = damage;
        }

        public override void Update()
        {
            Physics.AngularVelocity += 0.0001f;

            if (AliveTime > 180)
                Die();

            base.Update();
        }

        protected override void OnDie()
        {
            World.ParticleEffects.Spawn(EParticleType.WeaponBombletHit, Physics.Position, Visual.Color, null, null);
            CAudio.PlaySound("WeaponHitSeekBomb", 1.0f);
            base.OnDie();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }
    }
}
