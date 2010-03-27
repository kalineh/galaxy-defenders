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
        // TODO: spawn point (Window.ClientBounds)
        // TODO: game object sprite def?

        public CChassisPart Chassis { get; set; }
        public CGeneratorPart Generator { get; set; }
        public CShieldPart Shield { get; set; }
        public CWeaponPart PrimaryWeapon { get; set; }
        public CWeaponPart SecondaryWeapon { get; set; }
        public CWeaponPart SidekickLeft { get; set; }
        public CWeaponPart SidekickRight { get; set; }

        public float CurrentArmor { get; set; }
        public float CurrentShield { get; set; }
        public float CurrentEnergy { get; set; }

        public List<CWeapon> WeaponPrimary { get; set; }
        public List<CWeapon> WeaponSecondary { get; set; }
        public List<CWeapon> WeaponSidekickLeft { get; set; }
        public List<CWeapon> WeaponSidekickRight { get; set; }

        public CVisual SidekickVisual { get; set; }

        public float Vibrate { get; set; }

        public CShip(
            CWorld world,
            CChassisPart chassis,
            CGeneratorPart generator,
            CShieldPart shield,
            CWeaponPart primary,
            CWeaponPart secondary,
            CWeaponPart sidekick_left,
            CWeaponPart sidekick_right)
            : base(world)
        {
            Chassis = chassis;
            Generator = generator;
            Shield = shield;
            PrimaryWeapon = primary;
            SecondaryWeapon = secondary;
            SidekickLeft = sidekick_left;
            SidekickRight = sidekick_right;

            Physics = new CPhysics();
            Physics.PositionPhysics.Friction = chassis.Friction;
            Physics.AnglePhysics.Rotation = new Vector2(0.0f, -1.0f).ToAngle();
            Collision = new CollisionCircle(Vector2.Zero, 12.0f);
            Visual = CVisual.MakeSprite(world, chassis.Texture);
            Visual.Scale = new Vector2(chassis.VisualScale);
            SidekickVisual = CVisual.MakeSprite(world, "Textures/Player/Sidekick");

            WeaponPrimary = CWeaponFactory.GenerateWeapon(this, PrimaryWeapon);
            WeaponSecondary = CWeaponFactory.GenerateWeapon(this, SecondaryWeapon);
            WeaponSidekickLeft = CWeaponFactory.GenerateWeapon(this, SidekickLeft);
            WeaponSidekickRight = CWeaponFactory.GenerateWeapon(this, SidekickRight);

            CurrentArmor = chassis.Armor;
            CurrentShield = shield.Shield;
            CurrentEnergy = generator.Energy;

            foreach (CWeapon weapon in WeaponPrimary)
                weapon.Offset = weapon.Offset + Vector2.UnitX * 16.0f;
            foreach (CWeapon weapon in WeaponSidekickLeft)
                weapon.Offset = weapon.Offset + Vector2.UnitY * -32.0f;
            foreach (CWeapon weapon in WeaponSidekickRight)
                weapon.Offset = weapon.Offset + Vector2.UnitY * 32.0f;

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

            GamePad.SetVibration(PlayerIndex.One, Vibrate, Vibrate);
            Vibrate = Math.Max(0.0f, Vibrate - 0.1f);
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

            if (WeaponSidekickLeft.Count > 0)
                SidekickVisual.Draw(sprite_batch, Physics.PositionPhysics.Position + Physics.AnglePhysics.GetDir().Perp() * -32.0f, Physics.AnglePhysics.GetDir().Perp().ToAngle());
            if (WeaponSidekickRight.Count > 0)
                SidekickVisual.Draw(sprite_batch, Physics.PositionPhysics.Position + Physics.AnglePhysics.GetDir().Perp() * 32.0f, Physics.AnglePhysics.GetDir().Perp().ToAngle());
        }

        public List<CWeapon> UpgradeWeapon(CWeaponPart weapon_part)
        {
            if (!CWeaponFactory.CanUpgrade(weapon_part, 1))
                return CWeaponFactory.GenerateWeapon(this, weapon_part);

            weapon_part.Level += 1;
            List<CWeapon> weapon = CWeaponFactory.GenerateWeapon(this, weapon_part);
            return weapon;
        }

        public List<CWeapon> DowngradeWeapon(CWeaponPart weapon_part)
        {
            if (!CWeaponFactory.CanDowngrade(weapon_part, 1))
                return CWeaponFactory.GenerateWeapon(this, weapon_part);

            weapon_part.Level -= 1;
            List<CWeapon> weapon = CWeaponFactory.GenerateWeapon(this, weapon_part);
            return weapon;
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

            float Speed = Chassis.Speed;
            Vector2 force = new Vector2(0.0f, 0.0f);
 
            if (dpad.Up == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.Up)) { force.Y -= Speed; }
            if (dpad.Down == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.Down) ) { force.Y += Speed; }
            if (dpad.Left == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.Left) ) { force.X -= Speed; }
            if (dpad.Right == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.Right) ) { force.X += Speed; }

            force += GamePad.GetState(PlayerIndex.One).ThumbSticks.Left * new Vector2(1.0f, -1.0f) * Speed;

            if (force.Length() > 0.0f)
                force = force.Normal() * Math.Min(force.Length(), Speed);

            Physics.PositionPhysics.Velocity += force;

            // TODO: remove eventually
            if (World.Game.Input.IsKeyDown(Keys.Z)) { Physics.AnglePhysics.Rotation -= 0.1f; }
            if (World.Game.Input.IsKeyDown(Keys.X)) { Physics.AnglePhysics.Rotation += 0.1f; }

            // TEST: weapon upgrade
            bool lshift_down = CInput.IsRawKeyDown(Keys.LeftShift);
            if (lshift_down)
            {
                if (CInput.IsRawKeyPressed(Keys.C))
                    WeaponPrimary = DowngradeWeapon(PrimaryWeapon);
                if (CInput.IsRawKeyPressed(Keys.V))
                    WeaponSecondary = DowngradeWeapon(SecondaryWeapon);
            }
            else
            {
                if (CInput.IsRawKeyPressed(Keys.C))
                    WeaponPrimary = UpgradeWeapon(PrimaryWeapon);
                if (CInput.IsRawKeyPressed(Keys.V))
                    WeaponSecondary = UpgradeWeapon(SecondaryWeapon);
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
            if (buttons.LeftShoulder == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.A))
            {
                Fire(WeaponSidekickLeft);
            }
            if (buttons.RightShoulder == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.F))
            {
                Fire(WeaponSidekickRight);
            }
        }

        public void UpdateGenerator()
        {
            CurrentEnergy = Math.Min(CurrentEnergy + Generator.Regen, Generator.Energy);
        }

        public void UpdateWeapons()
        {
            WeaponPrimary.ForEach(weapon => weapon.Update());
            WeaponSecondary.ForEach(weapon => weapon.Update());
            WeaponSidekickLeft.ForEach(weapon => weapon.Update());
            WeaponSidekickRight.ForEach(weapon => weapon.Update());
        }

        public void UpdateShields()
        {
            float used_energy = Math.Min(CurrentEnergy, Shield.Regen);
            used_energy = Math.Min(used_energy, Shield.Shield - CurrentShield);
            CurrentShield += used_energy;
            CurrentEnergy -= used_energy;
        }

        private void Fire(List<CWeapon> weapons)
        {
            float required_energy = 0.0f;
            foreach (CWeapon weapon in weapons)
            {
                if (weapon.CanFire())
                    required_energy += weapon.Energy;
            }

            if (CurrentEnergy < required_energy)
                return;

            CurrentEnergy -= required_energy;

            foreach (CWeapon weapon in weapons)
            {
                weapon.TryFire();
            }
        }

        public void FireAllWeapons()
        {
            Fire(WeaponPrimary);
            Fire(WeaponSecondary);
            Fire(WeaponSidekickLeft);
            Fire(WeaponSidekickRight);
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

            // TODO: find a way to be sure we can disable this in all cases so it doesnt get left on (particularly after program exit)
            //Vibrate = 0.4f;
        }

        private void ApplyDamage(float damage)
        {
            if (CurrentShield > 0.0f)
            {
                CurrentShield -= damage;
                if (CurrentShield > 0.0f)
                    return;

                damage = Math.Abs(CurrentShield);
                CurrentShield = 0.0f;
            }

            CurrentArmor -= damage;
            if (CurrentArmor <= 0.0f)
            {
                Die();
                return;
            }

            if (CurrentShield <= 0.0f)
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