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

            bomblet.Physics.AngularVelocity = 0.02f;
            bomblet.Physics.Rotation = owner.World.Random.NextAngle();
            bomblet.Physics.Position = position;
            bomblet.Physics.Velocity = Vector2.UnitX.Rotate(bomblet.Physics.Rotation) * speed + owner.World.ScrollSpeed * -Vector2.UnitY * 3.5f;
            bomblet.Physics.Friction = 0.98f;

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
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 12.0f);
            Damage = damage;
        }

        public override void Update()
        {
            Physics.AngularVelocity += 0.0001f;
            Physics.Position += Owner.World.ScrollSpeed * -Vector2.UnitY;
            Physics.Velocity += Vector2.UnitY * -0.005f;

            if (AliveTime > 480)
                Die();

            base.Update();
        }

        protected override void OnDie()
        {
            World.ParticleEffects.Spawn(EParticleType.WeaponBombletHit, Physics.Position, Visual.Color, null, Owner.World.ScrollSpeed * -Vector2.UnitY);
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
