﻿//
// Plasma.cs
//

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CPlasma
        : CProjectile
    {
        public CPlasmaSplash Splash { get; set; }

        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world, owner, damage);

            if (damage < 1.0f)
            {
                Physics = new CPhysics();
                Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/SmallPlasma", owner.GameControllerIndex);
                Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
                Visual.Update();
                Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 2.0f);
                Splash = CPlasmaSplash.GetCached(world);
                Splash.Initialize(world);
                Splash.SetRadius(32.0f);
                Splash.Owner = owner;
                Splash.Damage = 0.2f;
            }
            else
            {
                Physics = new CPhysics();
                Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/BigPlasma", owner.GameControllerIndex);
                Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
                Visual.Update();
                Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 6.0f);
                Splash = CPlasmaSplash.GetCached(world);
                Splash.Initialize(world);
                Splash.SetRadius(64.0f);
                Splash.Owner = owner;
                Splash.Damage = 0.6f;
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle box = Collision as CollisionCircle;
            box.Position = Physics.Position;
        }

        protected override void OnDie()
        {
            World.EntityAdd(Splash);
            Splash.Physics.Position = Physics.Position;
            Splash.Collision.Enabled = true;

            World.ParticleEffects.Spawn(EParticleType.WeaponPlasmaHit, Physics.Position, Visual.Color, null, -Physics.Velocity.Normal());
            base.OnDie();
        }
    }

    public class CPlasmaSplash
        : CEntity
    {
        public CShip Owner { get; set; }
        public float Damage { get; set; }
        public bool WasHit { get; set; }

        public static CPlasmaSplash GetCached(CWorld world)
        {
            List<CPlasmaSplash> cache = world.PlasmaSplashCache;
            if (cache == null)
            {
                cache = new List<CPlasmaSplash>();
                for (int i = 0; i < 32; ++i)
                    cache.Add(new CPlasmaSplash());
            }

            if (cache.Count <= 0)
                cache.Add(new CPlasmaSplash());

            CPlasmaSplash result = cache[cache.Count - 1];
            cache.RemoveAt(cache.Count - 1);
            return result;
        }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 0.0f);
            Collision.Enabled = false;
            Physics = new CPhysics();
        }

        public override void Update()
        {
            base.Update();

            if (WasHit)
                Die();

            if (AliveTime > 2)
                Die();
        }

        public void SetRadius(float radius)
        {
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Radius = radius;
        }
    }
}
