//
// Ship.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        };
        static Settings SSettings;

        static CShip()
        {
            SSettings.VisualScale = 0.25f;
            SSettings.MovementSpeed = 1.0f;
            SSettings.Friction = 0.8f;
        }

        // TODO: spawn point (Window.ClientBounds)
        // TODO: game object sprite def?

        public List<CWeapon> Weapons { get; private set; }
        public List<CWeapon> WeaponsAlternate { get; private set; }

        public CShip(CWorld world, Vector2 position)
            : base(world, "Ship")
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Physics.PositionPhysics.Friction = SSettings.Friction;
            Physics.AnglePhysics.Rotation = new Vector2(0.0f, -1.0f).ToAngle();
            Collision = new CollisionCircle(Vector2.Zero, 12.0f);
            Visual = new CVisual(world.Game.Content.Load<Texture2D>("Ship"), Color.White);
            Visual.Scale = new Vector2(SSettings.VisualScale);

            Weapons = new List<CWeapon>(2);

            Weapons.Add(new CWeaponLaser(this, new Vector2(0.0f, -10.0f)) {
                Damage = 1.0f,
                ReloadTime = 0.2f,
                Speed = 8.0f,
                KickbackForce = 0.0f,
                });

            Weapons.Add(new CWeaponLaser(this, new Vector2(0.0f, 10.0f)) {
                Damage = 1.0f,
                ReloadTime = 0.2f,
                Speed = 8.0f,
                KickbackForce = 0.0f,
                });

            WeaponsAlternate = new List<CWeapon>(1);

            WeaponsAlternate.Add(new CWeaponMissile(this, new Vector2(0.0f, 0.0f)) {
                Damage = 4.0f,
                ReloadTime = 2.0f,
                Speed = 6.0f,
                KickbackForce = 9.0f,
                });
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
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);
        }

        protected override void OnDie()
        {
            CExplosion.Spawn(World, Physics.PositionPhysics.Position, 1.0f);
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

            Physics.PositionPhysics.Velocity += force;

            if (kb.IsKeyDown(Keys.Z)) { Physics.AnglePhysics.Rotation -= 0.1f; }
            if (kb.IsKeyDown(Keys.X)) { Physics.AnglePhysics.Rotation += 0.1f; }

            // TODO: bind to functions?
            if (buttons.B == ButtonState.Pressed || kb.IsKeyDown(Keys.S))
            {
                Fire(Weapons);
            }
            if (buttons.X == ButtonState.Pressed || kb.IsKeyDown(Keys.D))
            {
                Fire(WeaponsAlternate);
            }
        }

        private void UpdateWeapons()
        {
            Weapons.ForEach(weapon => weapon.Update());
            WeaponsAlternate.ForEach(weapon => weapon.Update());
        }

        private void Fire(List<CWeapon> weapons)
        {
            weapons.ForEach(weapon => weapon.TryFire());
        }
    };
}