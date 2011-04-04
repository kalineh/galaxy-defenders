//
// Boomerang.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public struct BoomerangCustomData
    {
        public float SprayAngle;
    };

    public class CBoomerang
        : CProjectile
    {
        public static CBoomerang Spawn(CShip owner, Vector2 position, float rotation, float speed, float damage, object custom_data)
        {
            CBoomerang boomerang = new CBoomerang();
            boomerang.Initialize(owner.World, owner, damage);

            boomerang.Physics.Rotation = owner.World.Random.NextAngle();
            boomerang.Physics.AngularVelocity = 0.175f;
            boomerang.Physics.Position = position;
            boomerang.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            BoomerangCustomData custom = (BoomerangCustomData)custom_data;
            boomerang.Physics.Velocity = boomerang.Physics.Velocity.Rotate(owner.World.Random.NextFloat() * custom.SprayAngle * owner.World.Random.NextSign());

            owner.World.EntityAdd(boomerang);

            return boomerang;
        }

        public float Scale { get; set; }

        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world, owner, damage);

            Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/Boomerang", owner.GameControllerIndex);
            Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
            Visual.Update();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 16.0f);
            Damage = damage;
            Scale = 1.0f;
        }

        public void OnCollide(CEnemy enemy)
        {
            GetHit(enemy);
        }

        public void OnCollide(CBuilding building)
        {
            GetHit(building);
        }

        public void GetHit(CEntity entity)
        {
            //Vector2 mid = Physics.Position + (Physics.Position - entity.Physics.Position) * 0.5f;
            //Vector2 rand = World.Random.NextVector2Variable() * 8.0f;
            Vector2 mid = Physics.Position;
            Vector2 rand = Physics.Velocity * World.Random.NextFloat() * -3.0f;
            World.ParticleEffects.Spawn(EParticleType.WeaponBoomerangHit, mid + rand, Visual.Color, null, -Physics.Velocity.Normal());

            Scale -= 0.05f;
        }

        public override void Update()
        {
            base.Update();

            //Scale -= 0.005f;

            if (Scale < 0.40f)
            {
                World.ParticleEffects.Spawn(EParticleType.WeaponBoomerangHit, Physics.Position, Visual.Color, null, -Physics.Velocity.Normal());
                World.ParticleEffects.Spawn(EParticleType.WeaponBoomerangHit, Physics.Position, Visual.Color, null, -Physics.Velocity.Normal());
                Die();
            }
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            if (Visual != null)
            {
                Visual.Draw(sprite_batch, Physics.Position, Physics.Rotation, Scale);
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
            circle.Radius = 16.0f * Scale;
        }
    }
}
