//
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
        struct Settings
        {
            public float VisualScale;
            public float MovementSpeed;
            public float Friction;
            public float Shield;
            public float Armor;
        };
        static Settings SSettings;

        static CShip()
        {
            SSettings.VisualScale = 0.25f;
            SSettings.MovementSpeed = 2.25f;
            SSettings.Friction = 0.8f;
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
            if (World.Game.Input.IsKeyPressed(Keys.C))
                UpgradePrimaryWeapon();
            if (World.Game.Input.IsKeyPressed(Keys.V))
                UpgradeSecondaryWeapon();

            // TODO: bind to functions?
            if (buttons.B == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.S))
            {
                Fire(WeaponPrimary);
            }
            if (buttons.X == ButtonState.Pressed || World.Game.Input.IsKeyDown(Keys.D))
            {
                Fire(WeaponSecondary);
            }
        }

        public void UpdateWeapons()
        {
            WeaponPrimary.ForEach(weapon => weapon.Update());
            WeaponSecondary.ForEach(weapon => weapon.Update());
        }

        public void UpdateShields()
        {
            Shield += 0.01f;
            Shield = Math.Min(Shield, SSettings.Shield);
        }

        private void Fire(List<CWeapon> weapons)
        {
            bool fired = false;
            foreach (CWeapon weapon in weapons)
            {
                fired |= weapon.TryFire();
            }

            if (fired)
            {
                // TODO: weapon type fire
                SoundEffect sound = World.Game.Content.Load<SoundEffect>("SE/LaserShoot");
                sound.Play(0.1f, 0.0f, 0.0f);
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