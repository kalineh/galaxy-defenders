//
// Weapon.cs
//

using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public abstract class CWeapon
    {
        public CEntity Owner { get; private set; }
        public string Type { get; set; }
        public float ReloadTime { get; set; }
        public float Speed { get; set; }
        public float Damage { get; set; }
        public float KickbackForce { get; set; }
        public Vector2 Offset { get; private set; }
        public float Rotation { get; private set; }
        private float Cooldown { get; set; }

        public CWeapon(CEntity owner, string type)
        {
            Owner = owner;
            Type = type;
            Offset = Vector2.Zero;
            Cooldown = 0.0f;
            Rotation = 0.0f;
        }

        public void Update()
        {
            Cooldown -= Time.SingleFrame;
            Cooldown = Math.Max(Cooldown, 0.0f);
        }

        public void ApplyWeaponData(CWeaponFactory.WeaponData data)
        {
            ReloadTime = data.ReloadTime;
            Speed = data.Speed;
            Damage = data.Damage;
            KickbackForce = data.KickbackForce;
            Offset = data.Offset;
            Rotation = data.Rotation;

            Cooldown = Math.Min(Cooldown, ReloadTime);
        }

        public bool CanFire()
        {
            return Cooldown <= 0.0f;
        }

        public bool TryFire()
        {
            if (CanFire())
            {
                Fire();
                return true;
            }

            return false;
        }

        private Vector2 Kickback(float rotation)
        {
            return Vector2.UnitX.Rotate(rotation) * KickbackForce * -1.0f;
        }

        private void Fire()
        {
            // TODO: this math is weird and broken
            // TODO: replace with matrices
            Vector2 position = Owner.Physics.PositionPhysics.Position;
            float base_rotation = Owner.Physics.AnglePhysics.Rotation;
            float rotation = base_rotation + Rotation;
            Vector2 dir = Owner.Physics.AnglePhysics.GetDir();
            Vector2 fire_offset = dir * Offset.X + dir.Perp() * Offset.Y;
            Vector2 fire_position = position + fire_offset;

            Instantiate(Owner.World, fire_position, rotation, Speed, Damage);

            Owner.Physics.PositionPhysics.Velocity += Kickback(Owner.Physics.AnglePhysics.Rotation);

            Cooldown = ReloadTime;
        }

        protected abstract void Instantiate(CWorld world, Vector2 position, float rotation, float speed, float damage);
    };

}
