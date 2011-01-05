//
// Bonus.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Galaxy
{
    public class CBonus
        : CEntity
    {
        static public int BonusValue = 20;

        private bool GotoPlayer { get; set; }
        private float GotoForce { get; set; }

        public static List<CBonus> BonusCache { get; set; }
        static CBonus()
        {
            BonusCache = new List<CBonus>(128);
        }

        public static CBonus GetCachedBonus(CWorld world)
        {
            if (BonusCache.Count == 0)
                BonusCache.Add(new CBonus());

            CBonus result = BonusCache[BonusCache.Count - 1];
            BonusCache.RemoveAt(BonusCache.Count - 1);
            result.Initialize(world);
            return result;
        }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Physics.Velocity = world.Random.NextVector2(2.0f);
            Physics.Friction = 0.95f + world.Random.NextFloat() * 0.03f;
            Physics.AngularVelocity = 0.1f;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 16.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Entity/Bonus");
            Mover = new CMoverIgnoreCamera();

            world.Stats.CoinsTotal += 1;
        }

        public override void Update()
        {
            base.Update();
            
            LerpGravity();
            LerpToPlayers();

            if (!IsInScreen() && Physics.Position.Y > 0.0f)
                Delete();
        }

        private void LerpGravity()
        {
            Physics.Velocity = Vector2.Lerp(Physics.Velocity, Vector2.UnitY * 2.0f, 0.03f);
        }

        private void LerpToPlayers()
        {
            CShip ship = World.GetNearestShip(Physics.Position);
            if (ship == null)
            {
                GotoPlayer = false;
                return;
            }

            Vector2 target = ship.Physics.Position;
            Vector2 offset = target - Physics.Position;
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
                Physics.Velocity += dir * power * power_multiplier;
            }
        }

        public void OnCollide(CShip ship)
        {
            World.Stats.CoinsCollected += 1;
            ship.Score += BonusValue;
            CAudio.PlaySound("BonusGet", 1.0f);
            Die();
        }

        protected override void OnDie()
        {
            base.OnDie();
            BonusCache.Add(this);
        }
    }

    public class CBigBonus
        : CBonus
    {
        public new static int BonusValue = 250;
        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Entity/BigBonus");
            Visual.Depth = CLayers.Entity + CLayers.SubLayerIncrement * 1;
        }

        public new void OnCollide(CShip ship)
        {
            World.Stats.CoinsCollected += 10;
            ship.Score += BonusValue;
            CAudio.PlaySound("BonusGet", 1.0f);
            Die();
        }
    }
}
