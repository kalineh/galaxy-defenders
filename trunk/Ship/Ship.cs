//
// Ship.cs
//

using System;
using System.Collections;
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
        public CWeaponPart FocusWeapon { get; set; }
        public CWeaponPart SecondaryWeapon { get; set; }
        public CWeaponPart Sidekick { get; set; }

        public float CurrentArmor { get; set; }
        public float CurrentShield { get; set; }
        public float CurrentEnergy { get; set; }
        private float SingleShotEnergyUsage { get; set; }

        public List<CWeapon> WeaponPrimary { get; set; }
        public List<CWeapon> WeaponFocus { get; set; }
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

        public bool IsFocusMode { get; set; }

        public int Score { get; set; }

        public CShipAI AI { get; set; }

        public void Initialize(
            CWorld world,
            GameControllerIndex index,
            CPilot pilot,
            CChassisPart chassis,
            CGeneratorPart generator,
            CShieldPart shield,
            CWeaponPart primary,
            CWeaponPart focus,
            CWeaponPart secondary,
            CWeaponPart sidekick
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
            FocusWeapon = focus;
            SecondaryWeapon = secondary;
            Sidekick = sidekick;

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
            // xna4: premultiplied alpha?
            ShieldHitVisual.Color = new Color(PlayerColor.R, PlayerColor.G, PlayerColor.B, 128);
            ShieldHitVisual.Scale = new Vector2(1.25f, 1.25f);
            ShieldHitVisual.Update();

            InitializeEquipment();
            Mover = new CMoverIgnoreCamera();

            SpeedEnhancement = 1.0f;
        }

        public void InitializeEquipment()
        {
            WeaponPrimary = CWeaponFactory.GenerateWeapon(this, PrimaryWeapon);
            WeaponFocus = CWeaponFactory.GenerateWeapon(this, FocusWeapon);
            WeaponSecondary = CWeaponFactory.GenerateWeapon(this, SecondaryWeapon);
            WeaponSidekickLeft = CWeaponFactory.GenerateWeapon(this, Sidekick);
            WeaponSidekickRight = CWeaponFactory.GenerateWeapon(this, Sidekick);

            CurrentArmor = Chassis.Armor;
            CurrentShield = Shield.Shield;
            CurrentEnergy = Generator.Energy;
            SingleShotEnergyUsage = CalculateSingleShotEnergy();

            foreach (CWeapon weapon in WeaponPrimary)
                weapon.Offset = weapon.Offset + Vector2.UnitX * 20.0f;
            foreach (CWeapon weapon in WeaponSidekickLeft)
                weapon.Offset = weapon.Offset + Vector2.UnitY * -40.0f;
            foreach (CWeapon weapon in WeaponSidekickRight)
                weapon.Offset = weapon.Offset + Vector2.UnitY * 40.0f;
        }

        public override void Update()
        {
            if (AI != null)
                AI.Update();

            UpdateInput();
            UpdateGenerator();
            UpdateWeapons();
            UpdateShields();
            UpdateArmor();

#if SOAK_TEST
            CurrentShield = Shield.Shield;
            CurrentArmor = Chassis.Armor;
#endif

            if (IsFocusMode)
            {
                Physics.Friction = Interpolation.MoveToValue(Physics.Friction, Chassis.Friction * 0.6f, 0.2f);
            }
            else
            {
                Physics.Friction = Interpolation.MoveToValue(Physics.Friction, Chassis.Friction, 0.4f);
            }

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

            //if (AI != null)
                //AI.Draw(sprite_batch);

            if (WeaponSidekickLeft.Count > 0)
                SidekickVisual.Draw(sprite_batch, Physics.Position + Physics.GetDir().Perp() * -40.0f, Physics.GetDir().Perp().ToAngle());
            if (WeaponSidekickRight.Count > 0)
                SidekickVisual.Draw(sprite_batch, Physics.Position + Physics.GetDir().Perp() * 40.0f, Physics.GetDir().Perp().ToAngle());

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
            GamePadButtonsMutable mutable = World.Game.Input.GetCurrentFrameGamePadButtonsState(GameControllerIndex);

            float Speed = Chassis.Speed * SpeedEnhancement + Chassis.Speed * Pilot.BonusSpeed;
            Vector2 force = Vector2.Zero;

            if (mutable.DPadUp == ButtonState.Pressed || World.Game.Input.IsKeyDownGame(GameControllerIndex, Keys.Up)) { force.Y -= Speed; }
            if (mutable.DPadDown == ButtonState.Pressed || World.Game.Input.IsKeyDownGame(GameControllerIndex, Keys.Down)) { force.Y += Speed; }
            if (mutable.DPadLeft == ButtonState.Pressed || World.Game.Input.IsKeyDownGame(GameControllerIndex, Keys.Left)) { force.X -= Speed; }
            if (mutable.DPadRight == ButtonState.Pressed || World.Game.Input.IsKeyDownGame(GameControllerIndex, Keys.Right)) { force.X += Speed; }

            //Console.WriteLine("thumb: {0}, speed: {1}, force: {2}", state.ThumbSticks.Left, Speed, force);
            force += mutable.ThumbstickLeft * new Vector2(1.0f, -1.0f) * Speed;

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
            GamePadButtonsMutable buttons = World.Game.Input.GetCurrentFrameGamePadButtonsState(GameControllerIndex);

            Vector2 force = GetInputVector();
            Physics.Velocity += force;

            // TEST: weapon upgrade
#if DEBUG
            if (CInput.IsRawKeyPressed(Keys.NumPad1))
            {
                WeaponPrimary = DowngradeWeapon(PrimaryWeapon);
                WeaponFocus = DowngradeWeapon(FocusWeapon);
                SingleShotEnergyUsage = CalculateSingleShotEnergy();
            }
            if (CInput.IsRawKeyPressed(Keys.NumPad2))
            {
                WeaponSecondary = DowngradeWeapon(SecondaryWeapon);
                SingleShotEnergyUsage = CalculateSingleShotEnergy();
            }
            if (CInput.IsRawKeyPressed(Keys.NumPad4))
            {
                WeaponPrimary = UpgradeWeapon(PrimaryWeapon);
                WeaponFocus = UpgradeWeapon(FocusWeapon);
                SingleShotEnergyUsage = CalculateSingleShotEnergy();
            }
            if (CInput.IsRawKeyPressed(Keys.NumPad5))
            {
                WeaponSecondary = UpgradeWeapon(SecondaryWeapon);
                SingleShotEnergyUsage = CalculateSingleShotEnergy();
            }
#endif

            bool is_focus_fire = buttons.A == ButtonState.Pressed || buttons.B == ButtonState.Pressed || buttons.X == ButtonState.Pressed || buttons.Y == ButtonState.Pressed;

            // need to handle keyboard special here because confirm/cancel are mapped to A/B with Enter/Escape
            if (World.Game.Input.IsKeyboardController(GameControllerIndex))
            {
                is_focus_fire = buttons.X == ButtonState.Pressed;
            }

            if (is_focus_fire)
            {
                FireFocusWeapons();
                World.ParticleEffects.Spawn(EParticleType.PlayerFocusMode, Physics.Position, Visual.Color, null, Physics.Velocity + World.ScrollSpeed * -Vector2.UnitY);
                IsFocusMode = true;
            }
            else
            {
                IsFocusMode = false;

                if (buttons.RightShoulder == ButtonState.Pressed || World.Game.Input.IsR2Down(GameControllerIndex))
                {
                    FirePrimarySecondaryWeapons();
                }
                else
                {
                    DidntFirePrimarySecondaryWeapons();
                }
            }

            if (!IsFocusMode)
            {
                if (buttons.LeftShoulder == ButtonState.Pressed || World.Game.Input.IsL2Down(GameControllerIndex))
                {
                    ChargeSidekickLeft();
                    ChargeSidekickRight();
                }
                else
                {
                    FireSidekickLeft();
                    FireSidekickRight();
                }
            }

//#if DEBUG
            if (GameControllerIndex == Galaxy.GameControllerIndex.One && World.Game.Input.IsKeyPressed(Keys.F1))
            {
                if (AI == null)
                    EnableAI(true);
                else
                    DisableAI();
            }

            if (GameControllerIndex == Galaxy.GameControllerIndex.Two && World.Game.Input.IsKeyPressed(Keys.F2))
            {
                if (AI == null)
                    EnableAI(true);
                else
                    DisableAI();
            }

#if SOAK_TEST
            if (AI == null)
                EnableAI(true);
#endif

//#endif
        }

        public void UpdateGenerator()
        {
            CurrentEnergy = Math.Min(CurrentEnergy + Generator.Regen, Generator.Energy);
        }

        public void UpdateWeapons()
        {
            foreach (CWeapon weapon in WeaponPrimary)
                weapon.Update();
            foreach (CWeapon weapon in WeaponFocus)
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

            float used_energy = Math.Min(CurrentEnergy, Shield.EnergyDrain);
            used_energy = Math.Min(used_energy, Shield.Shield - CurrentShield);
            float regen_energy = used_energy * Shield.Efficiency;
            CurrentShield += regen_energy;
            CurrentEnergy -= used_energy;
        }

        public void UpdateArmor()
        {
            if (IsLowArmor())
                CAudio.PlaySound("PlayerArmorWarning");
        }

        private void Fire(List<CWeapon> weapons)
        {
            // TODO: cache me
            float required_energy = 0.0f;
            foreach (CWeapon weapon in weapons)
            {
                if (weapon.CanFire())
                    required_energy += weapon.Energy + weapon.ToggleEnergyDrain;
            }

            if (CurrentEnergy < required_energy)
            {
                DidntFire(weapons);

                foreach (CWeapon weapon in weapons)
                    weapon.Overheat();

                return;
            }

            CurrentEnergy -= required_energy;

            foreach (CWeapon weapon in weapons)
            {
                weapon.TryFire();
            }
        }

        private void DidntFire(List<CWeapon> weapons)
        {
            foreach (CWeapon weapon in weapons)
                weapon.DidntFire();
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

        public void ChargeAndFireFullSidekick(List<CWeapon> weapons)
        {
            foreach (CWeapon weapon in weapons)
            {
                if (weapon.CanCharge())
                    weapon.Charge();
            }

            foreach (CWeapon weapon in weapons)
            {
                if (weapon.CurrentCharge < CWeapon.MaximumChargeFrames)
                    return;
            }

            foreach (CWeapon weapon in weapons)
            {
                if (weapon.AutoDischarge == 0)
                    weapon.TryFire();
            }
        }

        public void FirePrimarySecondaryWeapons()
        {
            Fire(WeaponPrimary);
            DidntFire(WeaponFocus);
            Fire(WeaponSecondary);
        }

        public void FireFocusWeapons()
        {
            Fire(WeaponFocus);
            DidntFire(WeaponPrimary);
        }

        public void DidntFirePrimarySecondaryWeapons()
        {
            DidntFire(WeaponPrimary);
            DidntFire(WeaponFocus);
            DidntFire(WeaponSecondary);
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
            // NOTE: this is doubled up in the collision resist calcation
            //       but we want to factor it in a little for regular damage too
            float resistance = Chassis.CollisionResistance * 0.25f;
            float inverse_resistance = 1.0f - resistance;
            float adjusted = damage * inverse_resistance;

            ApplyDamage(adjusted);

            Vector2 position = Physics.Position + (source - Physics.Position) * 0.35f;
            Vector2 force = velocity.Normal() * -2.0f;
            if (CurrentShield > 0.0f)
            {
                //CAnimationEffects.PlayerTakeShieldDamage(this, Physics.Position, 1.5f, PlayerColor);
                CAudio.PlaySound("PlayerShieldHit", 1.0f);
                ShieldHitDisplayFrames = 4;
                World.ParticleEffects.Spawn(EParticleType.PlayerShipShieldDamage, position, PlayerColor, null, null);
                World.GameCamera.Shake(0.05f, adjusted * 0.5f);
            }
            else
            {
                CAudio.PlaySound("PlayerArmorHit", 1.0f);
                World.ParticleEffects.Spawn(EParticleType.PlayerShipArmorDamage, position, PlayerColor, null, force);
                Physics.Velocity *= 0.15f;
                Physics.Velocity += velocity * 0.20f * adjusted;
                World.GameCamera.Shake(0.10f, adjusted * 1.0f);
            }

        }

        public void TakeCollideDamage(Vector2 source, Vector2 velocity, float damage)
        {
            CurrentEnergy += damage * Generator.RegenOnCollide;

            float inverse_resistance = 1.0f - Chassis.CollisionResistance;
            float adjusted = damage * inverse_resistance;

            Vector2 offset = Physics.Position - source;
            if (!offset.IsEffectivelyZero() && IsInvincible <= 0)
            {
                Vector2 dir = offset.Normal();
                Physics.Velocity *= 0.8f;
                Physics.Velocity += dir * 3.5f * adjusted;
            }

            TakeDamage(source, Physics.Velocity, adjusted);

            World.GameCamera.Shake(0.25f, adjusted * 0.3f);

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

        public void AbsorbBullet(CProjectile shot)
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

            // NOTE: sidekicks can be high energy use, we dont want to factor them in or shields will not regen!
            //foreach (CWeapon weapon in WeaponSidekickLeft)
                //usage += weapon.Energy;
            //foreach (CWeapon weapon in WeaponSidekickRight)
                //usage += weapon.Energy;

            return usage;
        }

        public bool IsLowArmor()
        {
            float threshold = Chassis.Armor * 0.2f;
            if (CurrentArmor > 0.0f && CurrentArmor < threshold)
                return true;

            return false;
        }

        public void GetCoin()
        {
            CurrentEnergy += 1.0f * Generator.RegenOnCoin;
        }

        public int CalculateEquipmentValue()
        {
            int value = 0;

            value += CWeaponFactory.GetTotalPriceForLevel(PrimaryWeapon.Type, PrimaryWeapon.Level);
            value += CWeaponFactory.GetTotalPriceForLevel(SecondaryWeapon.Type, SecondaryWeapon.Level);
            value += CWeaponFactory.GetTotalPriceForLevel(Sidekick.Type, Sidekick.Level);
            value += Chassis.Price;
            value += Generator.Price;
            value += Shield.Price;
            value += CSaveData.GetCurrentGameData(World.Game).Pilots[(int)GameControllerIndex].Money;

            return value;
        }

        public void EnableAI(bool generate_equipment)
        {
            AI = new CShipAI(this);
            if (generate_equipment)
                AI.GenerateEquipment(this.CalculateEquipmentValue());
        }

        public void DisableAI()
        {
            AI = null;
        }
    }

    public class CShipAI
    {
        public CShip Ship { get; set; }
        public CFiberManager FiberManager { get; set; }

        public Vector2 MoveTarget { get; set; }
        public Vector2 LastSlidingMoveTarget { get; set; }
        public bool Shooting { get; set; }
        public bool FocusShooting { get; set; }

        public List<string> Status { get; set; }

        public GamePadThumbSticks InputThumbSticks { get; set; }
        public GamePadTriggers InputTriggers { get; set; }
        public GamePadButtons InputButtons { get; set; }
        public GamePadDPad InputDPad { get; set; }

        public CShipAI(CShip ship)
        {
            Ship = ship;
            FiberManager = new CFiberManager();

            FiberManager.Fork(this.UpdateMovement);
            FiberManager.Fork(this.UpdateMovementReturnToSafeY);
            FiberManager.Fork(this.UpdateShooting);
            FiberManager.Fork(this.UpdateThinkShooting);
            FiberManager.Fork(this.UpdateThinkDodging);
            FiberManager.Fork(this.UpdateThinkCoinSearch);
            FiberManager.Fork(this.UpdateThinkEnergyConserve);

            Status = new List<string>();
            for (int i = 0; i < 4; ++i)
                Status.Add("");
        }

        public void Update()
        {
            if (Ship.IsDead)
                return;

            FiberManager.Update();

            UpdateInput();
        }

        public void UpdateInput()
        {
            GamePadState state = new GamePadState(
                InputThumbSticks,
                InputTriggers,
                InputButtons,
                InputDPad
            );

            Ship.World.Game.Input.SetCurrentFrameGamePadState(Ship.GameControllerIndex, state);
        }
    

        public IEnumerable UpdateMovement()
        {
            Random r = Ship.World.Random;
            CInput input = Ship.World.Game.Input;

            while (true)
            {
                for (int i = 0; i < 60; ++i)
                {
                    yield return 0;

                    Vector2 target_offset = new Vector2(
                        (float)Math.Cos(Ship.AliveTime * 0.01f),
                        (float)Math.Sin(Ship.AliveTime * 0.02f) * 0.25f
                    ) * (float)Math.Sin(Ship.AliveTime * 0.07f) * 80.0f;

                    Vector2 sliding_target = MoveTarget + target_offset;

                    //sliding_target = new Vector2(
                        //sliding_target.X,
                        //Vector2.Lerp(sliding_target, Ship.Physics.Position, 0.8f).Y
                    //);

                    LastSlidingMoveTarget = sliding_target;
                    Vector2 offset = sliding_target - Ship.Physics.Position;
                    float offset_distance = 0.0f;
                    if (offset.LengthSquared() > 0.1f)
                    {
                        offset_distance = offset.Length();
                        offset.Normalize();
                    }

                    Vector2 desired = offset;
                    desired *= new Vector2(1.0f, -0.7f);
                    if (FocusShooting)
                        desired *= 0.1f;

                    if (offset_distance > 30.0f)
                    {
                        InputThumbSticks = new GamePadThumbSticks(
                            Interpolation.MoveToVector(InputThumbSticks.Left, desired, 0.01f),
                            //Vector2.Lerp(InputThumbSticks.Left, desired, offset_distance * offset_distance * 0.00002f),
                            InputThumbSticks.Right
                        );

                        Ship.Physics.Velocity += offset * 0.65f;
                    }
                    else
                    {
                        InputThumbSticks = new GamePadThumbSticks(
                            Interpolation.MoveToVector(InputThumbSticks.Left, -desired, 0.02f),
                            InputThumbSticks.Right
                        );

                        Ship.Physics.Velocity *= 0.97f;
                    }
                }

                yield return r.Next() % 20;
            }
        }

        public IEnumerable UpdateMovementReturnToSafeY()
        {
            CInput input = Ship.World.Game.Input;
            CCamera camera = Ship.World.GameCamera;

            while (true)
            {
                MoveTarget = Interpolation.MoveToVector(MoveTarget, new Vector2(MoveTarget.X, camera.GetCenter().Y + 100.0f), 0.01f);
                //MoveTarget = new Vector2(MoveTarget.X, camera.GetCenter().Y + 120.0f);
                yield return 0;
            }
        }

        public IEnumerable UpdateShooting()
        {
            Random r = Ship.World.Random;
            CInput input = Ship.World.Game.Input;

            while (true)
            {
                yield return 0;

                if (!Shooting)
                {
                    InputButtons = new GamePadButtons();
                    yield return 8;
                    continue;
                }

                if (FocusShooting)
                {
                    int frames = r.Next() % 12 + 20;
                    for (int i = 0; i < frames; ++i)
                    {
                        InputButtons = new GamePadButtons(Buttons.B);
                        if (Ship.CurrentEnergy < Ship.Generator.Energy * 0.1f)
                            break;

                        yield return 0;
                    }
                }
                else
                {
                    InputButtons = new GamePadButtons(Buttons.LeftShoulder | Buttons.RightShoulder);
                }
            }
        }


        public IEnumerable FocusShoot()
        {
            Shooting = true;
            FocusShooting = true;
            yield return Ship.World.Random.Next() % 20 + 60;
            Shooting = false;
            FocusShooting = false;
        }

        public IEnumerable Shoot()
        {
            Shooting = true;
            FocusShooting = false;
            yield return Ship.World.Random.Next() % 20 + 60;
            Shooting = false;
            FocusShooting = false;
        }

        public IEnumerable UpdateThinkShooting()
        {
            const int EnemySearchPerFrame = 8;
            Random random = Ship.World.Random;
            List<CEntity> entities = Ship.World.Entities;

            while (true)
            {
                yield return 0;

                for (int i = 0; i < EnemySearchPerFrame; ++i)
                {
                    CEntity candidate = entities[random.Next() % entities.Count];
                    if (candidate.IsDead)
                        continue;

                    if (candidate as CEnemy == null &&
                        candidate as CBuilding == null)
                        continue;

                    float safety_range = 1.0f;

                    if (Ship.PrimaryWeapon.Type == "Flame")
                        safety_range = 0.1f;

                    MoveTarget = Vector2.Lerp(
                        new Vector2(
                            candidate.Physics.Position.X,
                            Ship.Physics.Position.Y + random.NextSignedFloat() * 30.0f + 15.0f
                        ),
                        new Vector2(
                            candidate.Physics.Position.X + random.NextSignedFloat() * 30.0f,
                            (candidate.Physics.Position.Y + random.NextFloat() * 100.0f + 200.0f) * safety_range
                        ),
                        random.NextFloat() * 0.2f + 0.8f
                    );

                    yield return 8;

                    while (true)
                    {
                        if (random.Next() % 100 < 10)
                            FiberManager.Fork(this.FocusShoot);
                        else
                            FiberManager.Fork(this.Shoot);

                        yield return 80;
                        if (candidate.IsDead == true || random.Next() % 100 < 30)
                            break;

                        if (candidate.Physics.Position.Y > Ship.Physics.Position.Y)
                            break;

                        if (candidate.Physics.Position.Y > Ship.World.GameCamera.GetBottomRight().Y)
                            break;
                    }

                    break;
                }

                yield return random.Next() % 30 + 30;
            }
        }

        public IEnumerable UpdateThinkDodging()
        {
            const int ProjectileSearchPerFrame = 4;
            Random random = Ship.World.Random;
            List<CEntity> entities = Ship.World.Entities;

            while (true)
            {
                yield return 0;

                for (int i = 0; i < ProjectileSearchPerFrame; ++i)
                {
                    CEntity candidate = entities[random.Next() % entities.Count];
                    if (candidate.IsDead)
                        continue;

                    if (candidate as CProjectile == null)
                        continue;

                    if (candidate as CEnemyCannonShot == null &&
                        candidate as CEnemyLaser == null &&
                        candidate as CEnemyMiniShot == null &&
                        candidate as CEnemyMissile == null &&
                        candidate as CEnemyPellet == null &&
                        candidate as CEnemyShot == null)
                    {
                        continue;
                    }

                    MoveTarget += new Vector2(
                        random.NextSignedFloat() * 20.0f,
                        random.NextSignedFloat() * 10.0f
                    );
                }

                yield return random.Next() % 30 + 30;
            }
        }

        public IEnumerable UpdateThinkCoinSearch()
        {
            const int CoinSearchPerFrame = 4;
            Random random = Ship.World.Random;
            List<CEntity> entities = Ship.World.Entities;

            while (true)
            {
                yield return 0;

                for (int i = 0; i < CoinSearchPerFrame; ++i)
                {
                    CEntity candidate = entities[random.Next() % entities.Count];
                    if (candidate.IsDead)
                        continue;

                    if (candidate as CBonus == null)
                        continue;

                    MoveTarget = candidate.Physics.Position;
                }

                yield return random.Next() % 90 + 90;
            }
        }

        public IEnumerable UpdateThinkEnergyConserve()
        {
            Random random = Ship.World.Random;

            while (true)
            {
                yield return 0;

                if (Ship.CurrentEnergy < Ship.Generator.Energy * 0.2f)
                {
                    int frames = random.Next() % 8 + 8;
                    for (int i = 0; i < frames; ++i)
                    {
                        FiberManager.Kill(this.Shoot);
                        Shooting = false;
                        FocusShooting = false;
                        yield return 0;
                    }
                }

                yield return random.Next() % 60 + 60;
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            CDebugRender.Circle(Ship.World.GameCamera.WorldMatrix, MoveTarget, 5.0f, 2.0f, Color.Red);
            CDebugRender.Circle(Ship.World.GameCamera.WorldMatrix, LastSlidingMoveTarget, 5.0f, 2.0f, Color.Blue);
        }

        public void GenerateEquipment(int monetary_value)
        {
            const float primary_ratio = 0.2f;
            const float secondary_ratio = 0.2f;
            const float sidekick_ratio = 0.1f;
            const float chassis_ratio = 0.15f;
            const float generator_ratio = 0.2f;
            const float shield_ratio = 0.15f;

            int remaining = (int)(monetary_value * 2.0f);

            List<string> primaries = CMap.AllPrimaryWeapons();
            List<string> secondaries = CMap.AllSecondaryWeapons();
            List<string> sidekicks = CMap.AllSidekickWeapons();
            List<string> chassises = CMap.AllChassisParts();
            List<string> generators = CMap.AllGeneratorParts();
            List<string> shields = CMap.AllShieldParts();

            Random random = Ship.World.Random;

            Ship.PrimaryWeapon.Type = primaries[random.Next() % 3];
            Ship.SecondaryWeapon.Type = "";
            Ship.Sidekick.Type = "";
            Ship.PrimaryWeapon.Level = 0;
            Ship.SecondaryWeapon.Level = 0;
            Ship.Sidekick.Level = 0;

            string primary_candidate = primaries[random.Next() % primaries.Count];
            for (int i = 0; i < 8; ++i)
            {
                if (monetary_value * primary_ratio < CWeaponFactory.GetTotalPriceForLevel(primary_candidate, i))
                    break;

                Ship.PrimaryWeapon.Type = primary_candidate;
                Ship.PrimaryWeapon.Level = i;
                Ship.FocusWeapon = WeaponDefinitions.GetPart(Ship.PrimaryWeapon.Type + "Focus", Ship.PrimaryWeapon.Level);
            }

            string secondary_candidate = secondaries[random.Next() % secondaries.Count];
            for (int i = 0; i < 8; ++i)
            {
                if (monetary_value * secondary_ratio < CWeaponFactory.GetTotalPriceForLevel(secondary_candidate, i))
                    break;

                Ship.SecondaryWeapon.Type = secondary_candidate;
                Ship.SecondaryWeapon.Level = i;
            }

            string sidekick_candidate = sidekicks[random.Next() % sidekicks.Count];
            for (int i = 0; i < 8; ++i)
            {
                if (monetary_value * sidekick_ratio < CWeaponFactory.GetTotalPriceForLevel(sidekick_candidate, i))
                    break;

                Ship.Sidekick.Type = sidekick_candidate;
                Ship.Sidekick.Level = 0;
            }

            List<KeyValuePair<int, string>> chassis_candidates = new List<KeyValuePair<int, string>>();
            for (int i = 0; i < chassises.Count; ++i)
            {
                string candidate = chassises[i];
                int price = ChassisDefinitions.GetPart(candidate).Price;
                if (monetary_value * chassis_ratio < ChassisDefinitions.GetPart(candidate).Price)
                    chassis_candidates.Add(new KeyValuePair<int, string>(price, candidate));
            }

            chassis_candidates = chassis_candidates.OrderByDescending(x => x.Key).ToList();
            if (chassis_candidates.Count > 0)
            {
                string chassis_selection = chassis_candidates[random.Next() % Math.Min(3, chassis_candidates.Count)].Value;
                Ship.Chassis = ChassisDefinitions.GetPart(chassis_selection);
            }
            else
            {
                Ship.Chassis = ChassisDefinitions.GetPart(CMap.AllChassisParts()[0]);
            }

            List<KeyValuePair<int, string>> generator_candidates = new List<KeyValuePair<int, string>>();
            for (int i = 0; i < generators.Count; ++i)
            {
                string candidate = generators[i];
                int price = GeneratorDefinitions.GetPart(candidate).Price;
                if (monetary_value * generator_ratio < GeneratorDefinitions.GetPart(candidate).Price)
                    generator_candidates.Add(new KeyValuePair<int, string>(price, candidate));
            }

            generator_candidates = generator_candidates.OrderByDescending(x => x.Key).ToList(); ;
            if (generator_candidates.Count > 0)
            {
                string generator_selection = generator_candidates[random.Next() % Math.Min(3, generator_candidates.Count)].Value;
                Ship.Generator = GeneratorDefinitions.GetPart(generator_selection);
            }
            else
            {
                Ship.Generator = GeneratorDefinitions.GetPart(CMap.AllGeneratorParts()[0]);
            }

            List<KeyValuePair<int, string>> shield_candidates = new List<KeyValuePair<int, string>>();
            for (int i = 0; i < shields.Count; ++i)
            {
                string candidate = shields[i];
                int price = ShieldDefinitions.GetPart(candidate).Price;
                if (monetary_value * shield_ratio < ShieldDefinitions.GetPart(candidate).Price)
                    shield_candidates.Add(new KeyValuePair<int, string>(price, candidate));
            }

            shield_candidates = shield_candidates.OrderByDescending(x => x.Key).ToList();
            if (shield_candidates.Count > 0)
            {
                string shield_selection = shield_candidates[random.Next() % Math.Min(3, shield_candidates.Count)].Value;
                Ship.Shield = ShieldDefinitions.GetPart(shield_selection);
            }
            else
            {
                Ship.Shield = ShieldDefinitions.GetPart(CMap.AllShieldParts()[0]);
            }

            // clamp to max levels
            Ship.PrimaryWeapon.Level = Math.Min(Ship.PrimaryWeapon.Level, CWeaponFactory.GetMaxLevel(Ship.PrimaryWeapon.Type) - 1);
            Ship.SecondaryWeapon.Level = Math.Min(Ship.SecondaryWeapon.Level, CWeaponFactory.GetMaxLevel(Ship.SecondaryWeapon.Type) - 1);
            Ship.FocusWeapon.Level = Math.Min(Ship.FocusWeapon.Level, CWeaponFactory.GetMaxLevel(Ship.FocusWeapon.Type) - 1);
            Ship.Sidekick.Level = Math.Min(Ship.Sidekick.Level, CWeaponFactory.GetMaxLevel(Ship.Sidekick.Type) - 1);
            Ship.PrimaryWeapon.Level = Math.Max(Ship.PrimaryWeapon.Level, 0);
            Ship.SecondaryWeapon.Level = Math.Max(Ship.SecondaryWeapon.Level, 0);
            Ship.FocusWeapon.Level = Math.Max(Ship.FocusWeapon.Level, 0);
            Ship.Sidekick.Level = Math.Max(Ship.Sidekick.Level, 0);

            Ship.InitializeEquipment();
        }
    }
}