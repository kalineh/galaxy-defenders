﻿//
// Ship.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Galaxy
{
    public class CShip
        : CEntity
    {
        public struct Settings
        {
            public float VisualScale;
            public float MovementSpeed;
            public float Friction;
            public float Energy;
            public float Shield;
            public float Armor;
        };
        public static Settings SSettings;

        static CShip()
        {
            SSettings.VisualScale = 0.25f;
            SSettings.MovementSpeed = 2.25f;
            SSettings.Friction = 0.8f;
            SSettings.Energy = 10.0f;
            SSettings.Shield = 5.0f;
            SSettings.Armor = 10.0f;
        }

        // TODO: spawn point (Window.ClientBounds)
        // TODO: game object sprite def?

        public List<CWeapon> WeaponPrimary { get; private set; }
        public List<CWeapon> WeaponSecondary { get; private set; }

        public string WeaponPrimaryType { get; set; }
        public int WeaponPrimaryLevel { get; set; }
        public string WeaponSecondaryType { get; set; }
        public int WeaponSecondaryLevel { get; set; }
        public float Energy { get; private set; }
        public float Shield { get; private set; }
        public float Armor { get; private set; }

        public CShip(CWorld world, SProfile profile, Vector2 position)
            : base(world)
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Physics.PositionPhysics.Friction = SSettings.Friction;
            Physics.AnglePhysics.Rotation = new Vector2(0.0f, -1.0f).ToAngle();
            Collision = new CollisionCircle(Vector2.Zero, 12.0f);
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Player/Ship"), Color.White);
            Visual.Scale = new Vector2(SSettings.VisualScale);

            WeaponPrimaryType = profile.WeaponPrimaryType;
            WeaponPrimaryLevel = profile.WeaponPrimaryLevel;
            WeaponPrimary = CWeaponFactory.GeneratePrimaryWeapon(this, WeaponPrimaryType, WeaponPrimaryLevel);

            WeaponSecondaryType = profile.WeaponSecondaryType;
            WeaponSecondaryLevel = profile.WeaponSecondaryLevel;
            WeaponSecondary = CWeaponFactory.GenerateSecondaryWeapon(this, WeaponSecondaryType, WeaponSecondaryLevel);

            Shield = SSettings.Shield;
            Armor = SSettings.Armor;

            IgnoreCameraScroll = true;
        }

        public override void Update()
        {
            UpdateInput();
            UpdateGenerator();
            UpdateWeapons();
            UpdateShields();

            base.Update();

            // post-physics update
            // TODO: camera scroll management (scenery system?)
            if (World.Game.State.GetType() == typeof(CStateGame))
            {
                ClampInsideScreen();

                float ship_x = Physics.PositionPhysics.Position.X;
                float pan = World.GameCamera.PanLimit / 2.0f;
                float width = World.GameCamera.GetVisibleWidth() / 2.0f;
                float x = ship_x / width * pan;


                // camera X position is bound to player
                World.GameCamera.Position = new Vector3(
                    x,
                    World.GameCamera.Position.Y,
                    World.GameCamera.Position.Z
                );
            }

        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);
        }

        public void UpgradePrimaryWeapon()
        {
            if (CWeaponFactory.CanUpgrade(WeaponPrimaryType, WeaponPrimaryLevel))
            {
                WeaponPrimaryLevel += 1;
                WeaponPrimary = CWeaponFactory.GeneratePrimaryWeapon(this, WeaponPrimaryType, WeaponPrimaryLevel);
            }
        }

        public void UpgradeSecondaryWeapon()
        {
            if (CWeaponFactory.CanUpgrade(WeaponSecondaryType, WeaponSecondaryLevel))
            {
                WeaponSecondaryLevel += 1;
                WeaponSecondary = CWeaponFactory.GenerateSecondaryWeapon(this, WeaponSecondaryType, WeaponSecondaryLevel);
            }
        }

        public void DowngradePrimaryWeapon()
        {
            if (CWeaponFactory.CanDowngrade(WeaponPrimaryType, WeaponPrimaryLevel))
            {
                WeaponPrimaryLevel -= 1;
                WeaponPrimary = CWeaponFactory.GeneratePrimaryWeapon(this, WeaponPrimaryType, WeaponPrimaryLevel);
            }
        }

        public void DowngradeSecondaryWeapon()
        {
            if (CWeaponFactory.CanDowngrade(WeaponSecondaryType, WeaponSecondaryLevel))
            {
                WeaponSecondaryLevel -= 1;
                WeaponSecondary = CWeaponFactory.GenerateSecondaryWeapon(this, WeaponSecondaryType, WeaponSecondaryLevel);
            }
        }

        protected override void OnDie()
        {
            CEffect.Explosion(World, Physics.PositionPhysics.Position, 1.0f);
        }

        private void UpdateInput()
        {
            // TODO: entity/physics input controller?
            GamePadState state = GamePad.GetState(PlayerIndex.One);
            GamePadButtons buttons = state.Buttons;
            GamePadDPad dpad = state.DPad;

            float Speed = SSettings.MovementSpeed;
            Vector2 force = new Vector2(0.0f, 0.0f);
 
            if (dpad.Up == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.Up)) { force.Y -= Speed; }
            if (dpad.Down == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.Down) ) { force.Y += Speed; }
            if (dpad.Left == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.Left) ) { force.X -= Speed; }
            if (dpad.Right == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.Right) ) { force.X += Speed; }

            force += GamePad.GetState(PlayerIndex.One).ThumbSticks.Left * new Vector2(1.0f, -1.0f) * Speed;

            if (force.Length() > 0.0f)
                force = force.Normal() * Math.Min(force.Length(), Speed);

            Physics.PositionPhysics.Velocity += force;

            if (World.Game.Input.IsKeyDown(Keys.Z)) { Physics.AnglePhysics.Rotation -= 0.1f; }
            if (World.Game.Input.IsKeyDown(Keys.X)) { Physics.AnglePhysics.Rotation += 0.1f; }

            // TEST: weapon upgrade
            bool lshift_down = CInput.IsRawKeyDown(Keys.LeftShift);
            if (lshift_down)
            {
                if (CInput.IsRawKeyPressed(Keys.C))
                    DowngradePrimaryWeapon();
                if (CInput.IsRawKeyPressed(Keys.V))
                    DowngradeSecondaryWeapon();
            }
            else
            {
                if (CInput.IsRawKeyPressed(Keys.C))
                    UpgradePrimaryWeapon();
                if (CInput.IsRawKeyPressed(Keys.V))
                    UpgradeSecondaryWeapon();
            }

            // TODO: bind to functions?
            if (buttons.B == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.S))
            {
                Fire(WeaponPrimary);
            }
            if (buttons.Y == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.D))
            {
                Fire(WeaponSecondary);
            }
        }

        public void UpdateGenerator()
        {
            Energy = Math.Min(Energy + 0.05f, SSettings.Energy);
        }

        public void UpdateWeapons()
        {
            WeaponPrimary.ForEach(weapon => weapon.Update());
            WeaponSecondary.ForEach(weapon => weapon.Update());
        }

        public void UpdateShields()
        {
            float used_energy = Math.Min(Energy, 0.01f);
            used_energy = Math.Min(used_energy, SSettings.Shield - Shield);
            Shield += used_energy;
            Energy -= used_energy;
        }

        private void Fire(List<CWeapon> weapons)
        {
            float required_energy = 0.0f;
            foreach (CWeapon weapon in weapons)
            {
                if (weapon.CanFire())
                    required_energy += weapon.Energy;
            }

            if (Energy < required_energy)
                return;

            Energy -= required_energy;

            // TODO: logic mismatch with above
            bool fired = false;
            foreach (CWeapon weapon in weapons)
            {
                fired |= weapon.TryFire();
            }

            if (fired)
            {
                // TODO: weapon type fire
                World.Sound.Play("LaserShoot", 0.1f);
            }
        }

        public void FireAllWeapons()
        {
            Fire(WeaponPrimary);
            Fire(WeaponSecondary);
        }

        public void TakeDamage(float damage)
        {
            ApplyDamage(damage);
        }

        public void TakeCollideDamage(Vector2 source, float damage)
        {
            Vector2 offset = Physics.PositionPhysics.Position - source;
            Vector2 dir = offset.Normal();
            Physics.PositionPhysics.Velocity += dir * 7.0f;
            ApplyDamage(damage);
        }

        private void ApplyDamage(float damage)
        {
            if (Shield > 0.0f)
            {
                Shield -= damage;
                if (Shield > 0.0f)
                    return;

                damage = Math.Abs(Shield);
                Shield = 0.0f;
            }

            Armor -= damage;
            if (Armor <= 0.0f)
            {
                Die();
                return;
            }

            if (Shield <= 0.0f)
            {
                CEffect.PlayerTakeDamage(this, Physics.PositionPhysics.Position, 0.5f);
            }
            else
            {
                CEffect.PlayerTakeShieldDamage(this, Physics.PositionPhysics.Position, 0.5f);
            }
        }
    };
}