//
// Lightning.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public struct LightningCustomData
    {
        public int Bounces;
    };

    public class CLightning
        : CProjectile
    {
        public float BaseDamage;
        public int BouncesRemaining;
        public CEntity IgnoreEntity;
        public int NoCollideTime;
        
        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world, owner, damage);

            Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/Lightning", owner.GameControllerIndex);
            Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
            Visual.UpdateColor();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 16.0f);

            BaseDamage = damage;
        }

        public override void Update()
        {
            NoCollideTime = Math.Max(0, NoCollideTime - 1);
            base.Update();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
            circle.Enabled = NoCollideTime == 0;
        }

        public override void Die()
        {
            if (BouncesRemaining > 0)
                return;

            base.Die();
        }

        protected override void OnDie()
        {
            // TODO: pfx override alpha param
            World.ParticleEffects.Spawn(EParticleType.WeaponLightningHit, Physics.Position, Visual.Color, null, -Physics.Velocity.Normal());
            base.OnDie();
        }

        public void OnCollide(CEnemy enemy)
        {
            if (enemy == IgnoreEntity)
                return;

            IgnoreEntity = enemy;
            enemy.OnCollideSimulation(this);
            DoBounce();
        }

        public void OnCollide(CBuilding building)
        {
            if (building == IgnoreEntity)
                return;

            IgnoreEntity = building;
            building.OnCollideSimulation(this);
            DoBounce();
        }

        public void DoBounce()
        {
            BouncesRemaining -= 1;
            if (BouncesRemaining < 0)
            {
                Die();
                return;
            }

            CEntity candidate = null;
            float nearest = float.MaxValue;
            foreach (CEntity entity in World.GetEntities())
            {
                if (entity.GetType().IsSubclassOf(typeof(CEnemy)) == false && entity.GetType() != typeof(CBuilding))
                    continue;

                if (entity == IgnoreEntity)
                    continue;

                if (entity.IsInScreen() == false)
                    continue;

                Vector2 target_position = entity.Physics.Position;
                Vector2 offset = target_position - Physics.Position;
                float length = offset.LengthSquared();

                if (length > nearest)
                    continue;

                float dot = Vector2.Dot(offset, Physics.Velocity);
                if (dot < -0.0f)
                    continue;

                nearest = length;
                candidate = entity;
            }

            if (candidate == null)
            {
                BouncesRemaining = 0;
                Die();
                return;
            }

            Vector2 offset2 = candidate.Physics.Position - Physics.Position;
            offset2 = offset2.Rotate(World.Random.NextFloat() * 0.3f * World.Random.NextSign());
            Physics.Position += Physics.Velocity * 1.0f;
            Physics.Rotation = offset2.ToAngle();
            Physics.Velocity = offset2.Normal() * Physics.Velocity.Length();
            Physics.Position += Physics.Velocity * 1.0f;

            World.ParticleEffects.Spawn(EParticleType.WeaponLightningHit, Physics.Position, Visual.Color, null, -Physics.Velocity.Normal());

            CAudio.PlaySound("WeaponHitLaser", 1.0f);

            NoCollideTime = 0;
        }
    }
}
