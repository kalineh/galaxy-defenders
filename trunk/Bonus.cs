﻿//
// Bonus.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Galaxy
{
    public class CBonus
        : CEntity
    {
        public CBonus(CWorld world, Vector2 position)
            : base(world, "Bonus")
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Physics.PositionPhysics.Velocity = world.Random.NextVector2(2.0f);
            Physics.PositionPhysics.Friction = 0.95f + world.Random.NextFloat() * 0.03f;
            Physics.AnglePhysics.AngularVelocity = 0.1f;
            Collision = new CollisionCircle(Vector2.Zero, 16.0f);
            Visual = new CVisual(CContent.LoadTexture2D(world.Game, "Textures/Entity/Bonus"), Color.White);
        }

        public override void Update()
        {
            base.Update();

            LerpGravity();
            LerpToPlayers();

            if (!IsInScreen() && Physics.PositionPhysics.Position.Y > 0.0f)
                Delete();
        }

        private void LerpGravity()
        {
            Physics.PositionPhysics.Velocity = Vector2.Lerp(Physics.PositionPhysics.Velocity, Vector2.UnitY * 2.0f, 0.03f);
        }

        private void LerpToPlayers()
        {
            foreach (CEntity entity in World.GetEntitiesOfType(typeof(CShip)))
            {
                Vector2 target = entity.Physics.PositionPhysics.Position + entity.Physics.PositionPhysics.Velocity * 4.0f;
                Vector2 offset = target - Physics.PositionPhysics.Position;
                Vector2 dir = offset.Normal();
                float length = offset.Length();

                const float MaxLength = 60.0f;
                if (length < MaxLength)
                {
                    float power = MaxLength - length;
                    float power_multiplier = 0.02f;
                    Physics.PositionPhysics.Velocity += dir * power * power_multiplier;
                }
            }
        }

        public void OnCollide(CShip ship)
        {
            World.Score += 100;
            SoundEffect sound = World.Game.Content.Load<SoundEffect>("SE/BonusGet");
            sound.Play(0.1f, 0.0f, 0.0f);
            Die();
        }
    }
}