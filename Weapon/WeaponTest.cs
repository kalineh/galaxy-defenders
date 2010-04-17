//
// WeaponTest.cs
//

using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    // TODO: delete me
    public class CWeaponTest
    {
        struct Settings
        {
            public float[] ReloadTime;
            public float[] Speed;
            public float[] Damage;
            public float KickbackForce;
        };

        static Settings SSettings;

        static CWeaponTest()
        {
            SSettings.ReloadTime = new float[3]{ 0.2f, 0.17f, 0.13f };
            SSettings.Speed = new float[3]{ 8.0f, 9.0f, 10.0f };
            SSettings.Damage = new float[3] { 1.0f, 1.5f, 2.0f };
            SSettings.KickbackForce = 3.0f;
        }

        public CShip Owner { get; private set; }
        public Vector2 Offset { get; private set; }
        public int Level { get; set; }
        public float ReloadTime { get { return SSettings.ReloadTime[Level]; } }
        public float Speed { get { return SSettings.Speed[Level]; } }
        public float Damage { get { return SSettings.Damage[Level]; } }
        private float Cooldown { get; set; }

        public CWeaponTest(CShip owner, Vector2 offset)
        {
            Owner = owner;
            Offset = offset;
            Cooldown = 0.0f;
            Level = 0;
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

        public Vector2 Kickback(float rotation)
        {
            return Vector2.UnitX.Rotate(rotation) * SSettings.KickbackForce * -1.0f;
        }

        private void Fire()
        {
            // TODO: this math is weird and broken
            // TODO: replace with matrices
            Vector2 position = Owner.Physics.PositionPhysics.Position;
            float rotation = Owner.Physics.AnglePhysics.Rotation;
            Vector2 dir = Owner.Physics.AnglePhysics.GetDir();
            Vector2 fire_offset = dir * Offset.X + dir.Perp() * Offset.Y;
            Vector2 fire_position = position + fire_offset;

            CLaser laser = CLaser.Spawn(Owner.World, fire_position, rotation, Speed, Damage, Owner.PlayerIndex);
            Cooldown = ReloadTime;
        }

        public void Update()
        {
            Cooldown -= Time.SingleFrame;
            Cooldown = Math.Max(Cooldown, 0.0f);
        }
    };

}
