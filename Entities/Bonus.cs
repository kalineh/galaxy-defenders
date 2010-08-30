//
// Bonus.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Galaxy
{
    public class CBonus
        : CEntity
    {
        private bool GotoPlayer { get; set; }
        private float GotoForce { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Physics.PositionPhysics.Velocity = world.Random.NextVector2(2.0f);
            Physics.PositionPhysics.Friction = 0.95f + world.Random.NextFloat() * 0.03f;
            Physics.AnglePhysics.AngularVelocity = 0.1f;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 16.0f);
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Entity/Bonus"), Color.White);
            Mover = new CMoverIgnoreCamera();

            world.Stats.CoinsTotal += 1;
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
            CShip ship = World.GetNearestShip(Physics.PositionPhysics.Position);
            if (ship == null)
            {
                GotoPlayer = false;
                return;
            }

            Vector2 target = ship.Physics.PositionPhysics.Position;
            Vector2 offset = target - Physics.PositionPhysics.Position;
            Vector2 dir = offset.Normal();
            float length = offset.Length();

            const float MaxLength = 120.0f;
            if (length < MaxLength)
            {
                GotoPlayer = true;
            }

            if (GotoPlayer)
            {
                GotoForce += 2.5f;
                float power = Math.Max(GotoForce, MaxLength - length);
                float power_multiplier = 0.02f;
                Physics.PositionPhysics.Velocity += dir * power * power_multiplier;
            }
        }

        public void OnCollide(CShip ship)
        {
            World.Stats.CoinsCollected += 1;
            World.Score += 50;
            CAudio.PlaySound("BonusGet", 1.0f);
            Die();
        }
    }

    public class CBigBonus
        : CBonus
    {
        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Entity/BigBonus"), Color.White);
            Visual.Depth = CLayers.Entity + CLayers.SubLayerIncrement * 1;
        }

        public new void OnCollide(CShip ship)
        {
            World.Stats.CoinsCollected += 10;
            World.Score += 500;
            CAudio.PlaySound("BonusGet", 1.0f);
            Die();
        }
    }
}
