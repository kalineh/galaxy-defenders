//
// DrunkMissile.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CDrunkMissile
        : CProjectile
    {
        public float Speed { get; set; }
        public float CurveOffset { get; set; }
        public float BaseRotation { get; set; }
        public float CurveSpeed { get; set; }
        public float CurveMagnitude { get; set; }

        public static CDrunkMissile Spawn(CShip owner, Vector2 position, float rotation, float speed, float damage)
        {
            CDrunkMissile missile = new CDrunkMissile();
            missile.Initialize(owner.World, owner, damage);

            missile.Speed = speed;
            missile.CurveSpeed = 0.03f + owner.World.Random.NextFloat() * 0.03f;
            missile.CurveMagnitude = 0.3f * owner.World.Random.NextSign() + owner.World.Random.NextFloat() * 0.2f * owner.World.Random.NextSign();
            missile.CurveOffset = owner.World.Random.NextSign();

            missile.BaseRotation = rotation - MathHelper.Pi + owner.World.Random.NextFloat() * 0.25f * owner.World.Random.NextSign();
            missile.Physics.Rotation = missile.BaseRotation;

            missile.Physics.Position = position;
            missile.Physics.Velocity = Vector2.UnitX.Rotate(missile.Physics.Rotation) * missile.Speed;

            owner.World.EntityAdd(missile);


            return missile;
        }

        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world, owner, damage);

            Owner = owner;
            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/DrunkMissile", owner.GameControllerIndex);
            Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
            Visual.Update();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));
        }

        public override void Update()
        {
            float curve = (float)Math.Sin(CurveOffset + AliveTime * CurveSpeed) * CurveMagnitude;
            Physics.Rotation = MathHelper.Lerp(BaseRotation, curve, curve);
            Physics.Velocity = Vector2.UnitX.Rotate(Physics.Rotation) * Speed;
            //Physics.Velocity = Vector2.Lerp(Physics.Velocity, Vector2.UnitX.Rotate(Physics.Rotation) * Speed, 0.05f);
            //Physics.Velocity = Vector2.Lerp(Physics.Velocity, -FireVector * Speed, 0.15f);
            Vector2 dir = Physics.Velocity.Normal();
            if (dir.Y < 0.0f)
                World.ParticleEffects.Spawn(EParticleType.WeaponDrunkMissileTrail, Physics.Position + dir * -4.0f, Visual.Color, null, -dir);

            base.Update();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB box = Collision as CollisionAABB;
            box.Position = Physics.Position;
        }

        protected override void OnDie()
        {
            World.ParticleEffects.Spawn(EParticleType.WeaponMissileHit, Physics.Position, Visual.Color, null, null);
            base.OnDie();
        }
    }
}
