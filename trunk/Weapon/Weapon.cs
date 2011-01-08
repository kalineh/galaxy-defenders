﻿//
// Weapon.cs
//

using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public abstract class CWeapon
    {
        public CShip Owner { get; private set; }
        public string Sound { get; set; }
        public float ReloadTime { get; set; }
        public float Speed { get; set; }
        public float Damage { get; set; }
        public float KickbackForce { get; set; }
        public Vector2 Offset { get; set; }
        public float Rotation { get; set; }
        public float Energy { get; private set; }
        public bool IsCharge { get; set; }
        public int AutoDischarge { get; set; }
        public float ChargeSpeed { get; set; }
        protected float Cooldown { get; set; }
        protected int CurrentCharge { get; set; }

        public virtual void Initialize(CShip owner)
        {
            Owner = owner;
            Offset = Vector2.Zero;
            Cooldown = 0.0f;
            Rotation = 0.0f;
            IsCharge = false;
            AutoDischarge = 0;
            CurrentCharge = 0;
        }

        public void Update()
        {
            Cooldown -= Time.SingleFrame;
            Cooldown = Math.Max(Cooldown, 0.0f);
        }

        public void ApplyWeaponData(WeaponDefinitions.SWeaponData data)
        {
            ReloadTime = data.ReloadTime;
            Speed = data.Speed;
            Damage = data.Damage;
            KickbackForce = data.KickbackForce;
            Offset = data.Offset;
            Rotation = data.Rotation;
            Energy = data.Energy;
            Cooldown = Math.Min(Cooldown, ReloadTime);
            IsCharge = data.ChargeSpeed > 0.0f;
            ChargeSpeed = data.ChargeSpeed;
            AutoDischarge = data.AutoDischarge;
        }

        public bool CanFire()
        {
            if (IsCharge)
            {
                if (CurrentCharge == 0)
                    return false;

                if (AutoDischarge > 0)
                    return CurrentCharge >= AutoDischarge;
            }

            return Cooldown <= 0.0f;
        }

        public bool CanCharge()
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

        public void Charge()
        {
            if (!IsCharge)
                TryFire();

            CurrentCharge += 1;

            // NOTE: 0 means not-auto-discharge, and will never trigger here
            if (AutoDischarge == CurrentCharge)
            {
                TryFire();
                return;
            }

            float charge = Math.Min(1.0f, Time.ToSeconds(CurrentCharge));
            Owner.World.ParticleEffects.Spawn(EParticleType.WeaponCharge, Owner.Physics.Position + Offset.Rotate(-MathHelper.PiOver2), Owner.PlayerColor, charge, null);
        }

        private Vector2 Kickback(float rotation)
        {
            return Vector2.UnitX.Rotate(rotation) * KickbackForce * -1.0f;
        }

        private void Fire()
        {
            // TODO: this math is weird and broken
            // TODO: replace with matrices
            Vector2 position = Owner.Physics.Position;
            float base_rotation = Owner.Physics.Rotation;
            float rotation = base_rotation + Rotation;
            Vector2 dir = Owner.Physics.GetDir();
            Vector2 fire_offset = dir * Offset.X + dir.Perp() * Offset.Y;
            Vector2 fire_position = position + fire_offset;

            float normalized_charge = Math.Min(1.0f, Time.ToSeconds(CurrentCharge));

            Instantiate(Owner, fire_position, rotation, Speed, Damage, normalized_charge);

            Owner.Physics.Velocity += Kickback(Owner.Physics.Rotation);

            Cooldown = ReloadTime;
            CurrentCharge = 0;

            if (Sound != null)
            {
                CAudio.PlaySound(Sound);
            }
        }

        protected abstract void Instantiate(CShip owner, Vector2 position, float rotation, float speed, float damage, float charge);
    };

}
