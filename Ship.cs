//
// ship.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace galaxy
{
    public class CWeapon
    {
        struct Settings
        {
            public float[] ReloadTime;
            public float[] Speed;
            public float[] Damage;
            public float KickbackForce;
        };

        static Settings SSettings;

        static CWeapon()
        {
            SSettings.ReloadTime = new float[3]{ 0.2f, 0.17f, 0.13f };
            SSettings.Speed = new float[3]{ 8.0f, 9.0f, 10.0f };
            SSettings.Damage = new float[3] { 1.0f, 1.5f, 2.0f };
            SSettings.KickbackForce = 3.0f;
        }

        public CEntity Owner { get; private set; }
        public Vector2 Offset { get; private set; }
        public int Level { get; set; }
        public float ReloadTime { get { return SSettings.ReloadTime[Level]; } }
        public float Speed { get { return SSettings.Speed[Level]; } }
        public float Damage { get { return SSettings.Damage[Level]; } }
        private float Cooldown { get; set; }

        public CWeapon(CEntity owner, Vector2 offset)
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

            CLaser laser = CLaser.Spawn(Owner.World, fire_position, rotation, Speed, Damage);
            Cooldown = ReloadTime;
        }

        public void Update()
        {
            Cooldown -= Time.SingleFrame;
            Cooldown = Math.Max(Cooldown, 0.0f);
        }
    };

    public class CShip
        : CEntity
    {
        struct Settings
        {
            public float VisualScale;
            public float MovementSpeed;
            public float Friction;
        };
        static Settings SSettings;

        static CShip()
        {
            SSettings.VisualScale = 0.2f;
            SSettings.MovementSpeed = 2.0f;
            SSettings.Friction = 0.8f;
        }

        // TODO: spawn point (Window.ClientBounds)
        // TODO: game object sprite def?

        public List<CWeapon> Weapons { get; private set; }

        public CShip(CWorld world, string name, Texture2D texture)
            : base(world, name, texture)
        {
            Visual.Scale = new Vector2(SSettings.VisualScale);
            Physics.PositionPhysics.Friction = SSettings.Friction;
            Physics.AnglePhysics.Rotation = new Vector2(0.0f, -1.0f).ToAngle();

            Weapons = new List<CWeapon>(2);
            Vector2 texture_size = Visual.GetScaledTextureSize() * 0.5f;
            //Weapons.Add(new CWeapon(this, new Vector2(-texture_size.X, texture_size.Y)));
            //Weapons.Add(new CWeapon(this, new Vector2(texture_size.X, texture_size.Y)));
            Weapons.Add(new CWeapon(this, new Vector2(0.0f, 10.0f)));
            Weapons.Add(new CWeapon(this, new Vector2(0.0f, -10.0f)));
        }

        public override void Update()
        {
            UpdateInput();

            UpdateWeapons();

            base.Update();

            // post-physics update
            ClampPositionToScreen();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
            circle.Radius = Visual.GetScaledTextureSize().Length() * 0.05f;
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);
        }

        protected override void OnDie()
        {
            CExplosion.Spawn(World, Physics.PositionPhysics.Position);
        }

        private void UpdateInput()
        {
            // TODO: entity/physics input controller?
            GamePadState state = GamePad.GetState(PlayerIndex.One);
            GamePadButtons buttons = state.Buttons;
            GamePadDPad dpad = state.DPad;
            KeyboardState kb = Keyboard.GetState();

            float Speed = SSettings.MovementSpeed;
            Vector2 force = new Vector2(0.0f, 0.0f);
 
            if (dpad.Up == ButtonState.Pressed || kb.IsKeyDown(Keys.Up)) { force.Y -= Speed; }
            if (dpad.Down == ButtonState.Pressed || kb.IsKeyDown(Keys.Down) ) { force.Y += Speed; }
            if (dpad.Left == ButtonState.Pressed || kb.IsKeyDown(Keys.Left) ) { force.X -= Speed; }
            if (dpad.Right == ButtonState.Pressed || kb.IsKeyDown(Keys.Right) ) { force.X += Speed; }

            // directional
            //Physics.PositionPhysics.Velocity += force.Rotate(MathHelper.PiOver2).Rotate(Physics.AnglePhysics.Rotation);
            // direct
            Physics.PositionPhysics.Velocity += force;

            if (kb.IsKeyDown(Keys.A)) { Physics.AnglePhysics.Rotation -= 0.1f; }
            if (kb.IsKeyDown(Keys.D)) { Physics.AnglePhysics.Rotation += 0.1f; }

            // TODO: bind to functions?
            if (buttons.B == ButtonState.Pressed || kb.IsKeyDown(Keys.S))
            {
                Fire();
            }
        }

        private void UpdateWeapons()
        {
            foreach (CWeapon weapon in Weapons)
            {
                weapon.Update();
            }
        }

        private void Fire()
        {
            foreach (CWeapon weapon in Weapons)
            {
                bool fired = weapon.TryFire();
                if (fired)
                {
                    Physics.PositionPhysics.Velocity += weapon.Kickback(Physics.AnglePhysics.Rotation);
                }
            }
        }
    };
}