//
// Ship.cs
//

using System;
using System.Collections.Generic;
using System.Linq;
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
        private static Color[] PlayerColors = {
            new Color(247, 208, 74, 255),
            new Color(195, 150, 255, 255),
        };

        public static Color GetPlayerColor(GameControllerIndex index)
        {
            return PlayerColors[(int)index];
        }

        public GameControllerIndex GameControllerIndex { get; set; }
        public Color PlayerColor { get; set; }

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
        private float SingleShotEnergyUsage { get; set; }

        public List<CWeapon> WeaponPrimary { get; set; }
        public List<CWeapon> WeaponSecondary { get; set; }
        public List<CWeapon> WeaponSidekickLeft { get; set; }
        public List<CWeapon> WeaponSidekickRight { get; set; }

        public CVisual SidekickVisual { get; set; }
        public CVisual ShieldHitVisual { get; set; }
        public int ShieldHitDisplayFrames { get; set; }

        public float Vibrate { get; set; }

        public CPilot Pilot { get; set; }

        public int IsIgnoreBullets { get; set; }
        public int IsReflectBullets { get; set; }
        public int IsInvincible { get; set; }
        public int IsAbsorbBullets { get; set; }
        public float SpeedEnhancement { get; set; }

        public int Score { get; set; }

        public void Initialize(
            CWorld world,
            GameControllerIndex index,
            CPilot pilot,
            CChassisPart chassis,
            CGeneratorPart generator,
            CShieldPart shield,
            CWeaponPart primary,
            CWeaponPart secondary,
            CWeaponPart sidekick_left,
            CWeaponPart sidekick_right
        )
        {
            base.Initialize(world);

            GameControllerIndex = index;
            PlayerColor = GetPlayerColor(GameControllerIndex);

            Pilot = pilot;
            Pilot.Ship = this;

            Chassis = chassis;
            Generator = generator;
            Shield = shield;
            PrimaryWeapon = primary;
            SecondaryWeapon = secondary;
            SidekickLeft = sidekick_left;
            SidekickRight = sidekick_right;

            Physics = new CPhysics();
            Physics.Friction = chassis.Friction;
            Physics.Rotation = new Vector2(0.0f, -1.0f).ToAngle();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 24.0f); // NOTE: radius updated per-frame below
            Visual = CVisual.MakeSpriteUncached(world.Game, chassis.Texture);
            Visual.Scale = new Vector2(chassis.VisualScale);
            Visual.Color = PlayerColor;
            Visual.Update();
            SidekickVisual = CVisual.MakeSpriteUncached(world.Game, "Textures/Player/Sidekick");
            SidekickVisual.Color = PlayerColor;
            SidekickVisual.Update();

            ShieldHitVisual = CVisual.MakeSpriteUncached(world.Game, "Textures/Effects/PlayerShieldHit");
            ShieldHitVisual.Color = new Color(PlayerColor, 128);
            ShieldHitVisual.Scale = new Vector2(1.25f, 1.25f);
            ShieldHitVisual.Update();

            InitializeEquipment();
            Mover = new CMoverIgnoreCamera();

            SpeedEnhancement = 1.0f;
        }

        public void InitializeEquipment()
        {
            WeaponPrimary = CWeaponFactory.GenerateWeapon(this, PrimaryWeapon);
            WeaponSecondary = CWeaponFactory.GenerateWeapon(this, SecondaryWeapon);
            WeaponSidekickLeft = CWeaponFactory.GenerateWeapon(this, SidekickLeft);
            WeaponSidekickRight = CWeaponFactory.GenerateWeapon(this, SidekickRight);

            CurrentArmor = Chassis.Armor;
            CurrentShield = Shield.Shield;
            CurrentEnergy = Generator.Energy;
            SingleShotEnergyUsage = CalculateSingleShotEnergy();

            foreach (CWeapon weapon in WeaponPrimary)
                weapon.Offset = weapon.Offset + Vector2.UnitX * 16.0f;
            foreach (CWeapon weapon in WeaponSidekickLeft)
                weapon.Offset = weapon.Offset + Vector2.UnitY * -32.0f;
            foreach (CWeapon weapon in WeaponSidekickRight)
                weapon.Offset = weapon.Offset + Vector2.UnitY * 32.0f;
        }

        public override void Update()
        {
            UpdateInput();
            UpdateGenerator();
            UpdateWeapons();
            UpdateShields();

            base.Update();

            Pilot.Update();

            // post-physics update
            // TODO: camera scroll management (scenery system?)
            if (World.Game.State.GetType() == typeof(CStateGame))
            {
                if (World.StageEnd)
                    return;

                ClampInsideScreen();

                float ship_x = Physics.Position.X;
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

            // TODO: player index/controller index; move to Input.cs
            //GamePad.SetVibration(GameControllerIndex, Vibrate, Vibrate);
            //Vibrate = Math.Max(0.0f, Vibrate - 0.1f);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
            circle.Radius = CurrentShield > 0.0f ? 24.0f : 16.0f;
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);

            if (WeaponSidekickLeft.Count > 0)
                SidekickVisual.Draw(sprite_batch, Physics.Position + Physics.GetDir().Perp() * -32.0f, Physics.GetDir().Perp().ToAngle());
            if (WeaponSidekickRight.Count > 0)
                SidekickVisual.Draw(sprite_batch, Physics.Position + Physics.GetDir().Perp() * 32.0f, Physics.GetDir().Perp().ToAngle());

            if (ShieldHitDisplayFrames > 0)
                ShieldHitVisual.Draw(sprite_batch, Physics.Position, Physics.Rotation);
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
            World.ParticleEffects.Spawn(EParticleType.PlayerShipDestroyed, Physics.Position, PlayerColor, null, null);
            World.ShipEntitiesCache.Remove(this);
        }

        public Vector2 GetInputVector()
        {
            GamePadState state = World.Game.Input.GetCurrentFrameGamePadState(GameControllerIndex);
            GamePadButtons buttons = state.Buttons;
            GamePadDPad dpad = state.DPad;

            float Speed = Chassis.Speed * SpeedEnhancement;
            Vector2 force = Vector2.Zero;

            if (dpad.Up == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.Up)) { force.Y -= Speed; }
            if (dpad.Down == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.Down)) { force.Y += Speed; }
            if (dpad.Left == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.Left)) { force.X -= Speed; }
            if (dpad.Right == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.Right)) { force.X += Speed; }

            force += state.ThumbSticks.Left * new Vector2(1.0f, -1.0f) * Speed;

            if (force.LengthSquared() > 0.0f)
                force = force.Normal() * Math.Min(force.Length(), Speed);

            return force;
        }

        private void UpdateInput()
        {
            // no input at stage end
            if (World.StageEnd)
                return;

            // TODO: entity/physics input controller?
            GamePadState state = World.Game.Input.GetCurrentFrameGamePadState(GameControllerIndex);
            GamePadButtons buttons = state.Buttons;
            GamePadDPad dpad = state.DPad;

            Vector2 force = GetInputVector();
            Physics.Velocity += force;

            // TEST: weapon upgrade
            bool lctrl_down = CInput.IsRawKeyDown(Keys.LeftControl);
            if (lctrl_down)
            {
                if (CInput.IsRawKeyPressed(Keys.C))
                {
                    WeaponPrimary = DowngradeWeapon(PrimaryWeapon);
                    SingleShotEnergyUsage = CalculateSingleShotEnergy();
                }
                if (CInput.IsRawKeyPressed(Keys.V))
                {
                    WeaponSecondary = DowngradeWeapon(SecondaryWeapon);
                    SingleShotEnergyUsage = CalculateSingleShotEnergy();
                }
            }
            else
            {
                if (CInput.IsRawKeyPressed(Keys.C))
                {
                    WeaponPrimary = UpgradeWeapon(PrimaryWeapon);
                    SingleShotEnergyUsage = CalculateSingleShotEnergy();
                }
                if (CInput.IsRawKeyPressed(Keys.V))
                {
                    WeaponSecondary = UpgradeWeapon(SecondaryWeapon);
                    SingleShotEnergyUsage = CalculateSingleShotEnergy();
                }
            }


            // TODO: bind to functions?
            if (buttons.Y == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.A))
            {
                Pilot.Ability0.TryEnable();
            }
            // TODO: this key isnt working?
            if (buttons.B == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.S))
            {
                Pilot.Ability1.TryEnable();
            }
            if (buttons.A == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.D))
            {
                Pilot.Ability2.TryEnable();
            }
            if (buttons.X == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.LeftShift))
            {
                FirePrimarySecondaryWeapons();
            }

            if (buttons.LeftShoulder == ButtonState.Pressed || World.Game.Input.IsL2Down(GameControllerIndex) || World.Game.Input.IsKeyDown(Keys.Z))
            {
                ChargeSidekickLeft();
            }
            else
            {
                FireSidekickLeft();
            }

            if (buttons.RightShoulder == ButtonState.Pressed || World.Game.Input.IsR2Down(GameControllerIndex) || World.Game.Input.IsKeyDown(Keys.X))
            {
                ChargeSidekickRight();
            }
            else
            {
                FireSidekickRight();
            }
        }

        public void UpdateGenerator()
        {
            CurrentEnergy = Math.Min(CurrentEnergy + Generator.Regen, Generator.Energy);
        }

        public void UpdateWeapons()
        {
            foreach (CWeapon weapon in WeaponPrimary)
                weapon.Update();
            foreach (CWeapon weapon in WeaponSecondary)
                weapon.Update();
            foreach (CWeapon weapon in WeaponSidekickLeft)
                weapon.Update();
            foreach (CWeapon weapon in WeaponSidekickRight)
                weapon.Update();
        }

        public void UpdateShields()
        {
            ShieldHitDisplayFrames = Math.Max(0, ShieldHitDisplayFrames - 1);

            // shields never use the energy of the primary weapon
            // this forces the player to balance weapon usage with shields
            if (CurrentEnergy < SingleShotEnergyUsage * 2.0f)
                return;

            float used_energy = Math.Min(CurrentEnergy, Shield.Regen);
            used_energy = Math.Min(used_energy, Shield.Shield - CurrentShield);
            CurrentShield += used_energy;
            CurrentEnergy -= used_energy;
        }

        private void Fire(List<CWeapon> weapons)
        {
            // TODO: cache me
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

        private void ChargeSidekick(List<CWeapon> weapons)
        {
            float cost = 0.0f;

            foreach (CWeapon weapon in weapons)
            {
                if (!weapon.CanCharge())
                    return;
            }

            foreach (CWeapon weapon in weapons)
            {
                if (weapon.CurrentCharge < CWeapon.MaximumChargeFrames)
                    cost += weapon.Energy;

                weapon.Charge();
            }

            CurrentEnergy = Math.Max(0.0f, CurrentEnergy - cost);
        }

        private void FireSidekick(List<CWeapon> weapons)
        {
            foreach (CWeapon weapon in weapons)
            {
                weapon.TryFire();
            }
        }

        public void FirePrimarySecondaryWeapons()
        {
            Fire(WeaponPrimary);
            Fire(WeaponSecondary);
        }

        public void ChargeSidekickLeft()
        {
            ChargeSidekick(WeaponSidekickLeft);
        }

        public void ChargeSidekickRight()
        {
            ChargeSidekick(WeaponSidekickRight);
        }

        public void FireSidekickLeft()
        {
            FireSidekick(WeaponSidekickLeft);
        }

        public void FireSidekickRight()
        {
            FireSidekick(WeaponSidekickRight);
        }

        public void TakeDamage(Vector2 source, Vector2 velocity, float damage)
        {
            ApplyDamage(damage);

            Vector2 position = Physics.Position + (source - Physics.Position) * 0.35f;
            Vector2 force = velocity.Normal() * -2.0f;
            if (CurrentShield > 0.0f)
            {
                //CAnimationEffects.PlayerTakeShieldDamage(this, Physics.Position, 1.5f, PlayerColor);
                CAudio.PlaySound("PlayerShieldHit", 1.0f);
                ShieldHitDisplayFrames = 4;
                World.ParticleEffects.Spawn(EParticleType.PlayerShipShieldDamage, position, PlayerColor, null, null);
            }
            else
            {
                World.ParticleEffects.Spawn(EParticleType.PlayerShipArmorDamage, position, PlayerColor, null, force);
                Physics.Velocity *= 0.15f;
                Physics.Velocity += velocity * 0.20f * damage;
            }

        }

        public void TakeCollideDamage(Vector2 source, Vector2 velocity, float damage)
        {
            Vector2 offset = Physics.Position - source;
            if (!offset.IsEffectivelyZero() && IsInvincible <= 0)
            {
                Vector2 dir = offset.Normal();
                Physics.Velocity *= 0.8f;
                Physics.Velocity += dir * 7.0f * damage;
            }

            TakeDamage(source, Physics.Velocity, damage);

            // TODO: find a way to be sure we can disable this in all cases so it doesnt get left on (particularly after program exit)
            //Vibrate = 0.4f;
        }

        public void TakeDirectDamage(float damage)
        {
            ApplyDamage(damage);
        }

        public void TakeDirectArmorDamage(float damage)
        {
            float shield = CurrentShield;
            CurrentShield = 0.0f;
            ApplyDamage(damage);
            CurrentShield = shield;
        }

        private void ApplyDamage(float damage)
        {
            if (IsInvincible > 0)
                return;

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
        }

        public void AbsorbBullet(CEnemyShot shot)
        {
            float value = shot.Damage;
            float max_to_energy = Generator.Energy - CurrentEnergy;
            float max_to_shield = Shield.Shield - CurrentShield;

            float to_energy = Math.Min(value, max_to_energy);
            value -= to_energy;
            float to_shield = Math.Min(value, max_to_shield);

            const float Scale = 0.025f;
            CurrentEnergy += to_energy * Scale;
            CurrentShield += to_shield * Scale;

            CurrentEnergy = Math.Min(CurrentEnergy, Generator.Energy);
            CurrentShield = Math.Min(CurrentShield, Shield.Shield);
        }

        private float CalculateSingleShotEnergy()
        {
            float usage = 0.0f;

            foreach (CWeapon weapon in WeaponPrimary)
                usage += weapon.Energy;
            foreach (CWeapon weapon in WeaponSecondary)
                usage += weapon.Energy;
            foreach (CWeapon weapon in WeaponSidekickLeft)
                usage += weapon.Energy;
            foreach (CWeapon weapon in WeaponSidekickRight)
                usage += weapon.Energy;

            return usage;
        }

    };
}