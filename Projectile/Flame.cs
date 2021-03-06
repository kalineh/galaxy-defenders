﻿//
// Flame.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public struct FlameCustomData
    {
        public int Lifetime;
        public float Friction;
        public float SprayAngle;
        public float StartRotation;
        public float FireRotationSpeed;
        public float AntiCameraSpeed;
        public float? ScaleOverride;
    };

    public class CFlame
        : CProjectile
    {
        public float BaseDamage;
        public float Lifetime;
        public float? ScaleOverride;

        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world, owner, damage);

            if (damage < 1.0f)
            {
                Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/Flame", owner.GameControllerIndex);
                Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
                Visual.UpdateColor();
                Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 16.0f);
            }
            else
            {
                Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/BigFlame", owner.GameControllerIndex);
                Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
                Visual.UpdateColor();
                Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 30.0f);
            }

            BaseDamage = damage;
        }

        public override void Update()
        {
            float t = 1.0f - (float)AliveTime / (float)Lifetime;
            Damage = BaseDamage * 0.5f + BaseDamage * t * 0.5f;

            base.Update();

            if (AliveTime > Lifetime)
                Delete();
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            if (Visual != null)
            {
                if (ScaleOverride == null)
                {
                    float t = (float)AliveTime / (float)Lifetime;
                    Visual.Draw(sprite_batch, Physics.Position, Physics.Rotation, 0.5f + t * 0.5f);
                }
                else
                {
                    Visual.Draw(sprite_batch, Physics.Position, Physics.Rotation, (float)ScaleOverride);
                }
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        protected override void OnDie()
        {
            // TODO: pfx override alpha param
            World.ParticleEffects.Spawn(EParticleType.WeaponFlameHit, Physics.Position, Visual.Color, null, -Physics.Velocity.Normal());
            base.OnDie();
        }
    }
}
