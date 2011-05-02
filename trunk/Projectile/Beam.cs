//
// Beam.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Galaxy
{
    public struct BeamCustomData
    {
        public float Width;
    };

    public class CBeam
        : CProjectile
    {
        public Vector2 OwnerOffset;
        public CWeapon OwnerWeapon;
        public float Width;
        public Cue SoundEffect;

        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world, owner, damage);

            Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/Beam", owner.GameControllerIndex);
            Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
            Visual.UpdateColor();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, Vector2.Zero);
        }

        public override void Update()
        {
            // TODO: figure out why Die() instead of Delete() tries to draw one extra frame :(
            if (Owner.IsDead)
            {
                OwnerWeapon.IsToggled = false;
                Delete();
                return;
            }

            if (OwnerWeapon.IsToggled == false)
            {
                Delete();
                return;
            }

            Physics.Position = Owner.Physics.Position + OwnerOffset + new Vector2(0.0f, 42.0f);
            base.Update();
        }

        public void OnCollide(CEnemy enemy)
        {
            Vector2 pos = new Vector2(MathHelper.Clamp(enemy.Physics.Position.X, Physics.Position.X - Width, Physics.Position.X + Width), enemy.Physics.Position.Y);
            World.ParticleEffects.Spawn(EParticleType.WeaponBeamHit, pos + World.Random.NextVector2Variable() * 16.0f * World.Random.NextSign(), Visual.Color, null, null);
            CAudio.PlaySound("WeaponHitLaser");
        }

        public void OnCollide(CBuilding building)
        {
            Vector2 pos = new Vector2(MathHelper.Clamp(building.Physics.Position.X, Physics.Position.X - Width, Physics.Position.X + Width), building.Physics.Position.Y);
            World.ParticleEffects.Spawn(EParticleType.WeaponBeamHit, pos + World.Random.NextVector2Variable() * 16.0f * World.Random.NextSign(), Visual.Color, null, null);
            CAudio.PlaySound("WeaponHitLaser");
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            if (Visual == null)
                return;

            const float scale = 17.0f;
            Vector2 offset = new Vector2(-0.5f, -1080.0f / 2.0f);
            Visual.Draw(sprite_batch, Physics.Position + offset, Physics.Rotation, new Vector2(scale, Width));
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB aabb = Collision as CollisionAABB;
            aabb.Size = new Vector2(8.0f * Width, 1080.0f);
            aabb.Position = Physics.Position + new Vector2(-4.0f * Width, -1080.0f);
        }
    }

    public struct BeamFocusCustomData
    {
        public float Radius;
    };

    public class CBeamFocus
        : CProjectile
    {
        public Vector2 OwnerOffset;
        public CWeapon OwnerWeapon;
        public float TargetRadius;
        public float Radius;
        public Cue SoundEffect;

        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world, owner, damage);

            Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/BeamFocus", owner.GameControllerIndex);
            Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
            Visual.UpdateColor();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 0.0f);
        }

        public override void Update()
        {
            // TODO: figure out why Die() instead of Delete() tries to draw one extra frame :(
            if (Owner.IsDead)
            {
                OwnerWeapon.IsToggled = false;
                Delete();
                return;
            }

            if (OwnerWeapon.IsToggled == false)
            {
                Delete();
                return;
            }

            Radius = Math.Min(Radius + 20.0f, TargetRadius);

            Physics.Position = Owner.Physics.Position;
            base.Update();
        }

        public void OnCollide(CEnemy enemy)
        {
            Vector2 pos = Physics.Position + (enemy.Physics.Position - Physics.Position) * 0.8f;
            World.ParticleEffects.Spawn(EParticleType.WeaponBeamHit, pos + World.Random.NextVector2Variable() * 16.0f * World.Random.NextSign(), Visual.Color, null, null);
            CAudio.PlaySound("WeaponHitLaser");
        }

        public void OnCollide(CBuilding building)
        {
            Vector2 pos = Physics.Position + (building.Physics.Position - Physics.Position) * 0.8f;
            World.ParticleEffects.Spawn(EParticleType.WeaponBeamHit, pos + World.Random.NextVector2Variable() * 16.0f * World.Random.NextSign(), Visual.Color, null, null);
            CAudio.PlaySound("WeaponHitLaser");
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            if (Visual == null)
                return;

            Visual.Draw(sprite_batch, Physics.Position, (float)AliveTime * 0.25f, Radius / 256.0f);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle collision = Collision as CollisionCircle;
            collision.Position = Physics.Position;
            collision.Radius = Radius * 0.46f;
        }
    }
}
