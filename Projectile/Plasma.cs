//
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

            if (damage < 0.5f)
            {
                Physics = new CPhysics();
                Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/SmallPlasma", owner.GameControllerIndex);
                Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
                Visual.Update();
                Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 2.0f);
            }
            else
            {
                Physics = new CPhysics();
                Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/BigPlasma", owner.GameControllerIndex);
                Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
                Visual.Update();
                Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 38.0f);
                Splash = CPlasmaSplash.GetCached(world);
                Splash.Initialize(world);
                Splash.SetRadius(210.0f);
                Splash.Owner = owner;
                Splash.Damage = Damage * 0.5f;
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle box = Collision as CollisionCircle;
            box.Position = Physics.Position;
        }

        public override void Update()
        {
            base.Update();

            if (Splash != null)
            {
                World.ParticleEffects.Spawn(EParticleType.WeaponPlasmaTrail, Physics.Position, Visual.Color, null, -Physics.Velocity.Normal());
            }
        }

        protected override void OnDie()
        {
            if (Splash != null)
            {
                World.EntityAdd(Splash);
                Splash.Physics.Position = Physics.Position;
                Splash.Collision.Enabled = true;
                World.ParticleEffects.Spawn(EParticleType.WeaponPlasmaSplash, Physics.Position, Visual.Color, null, Physics.Velocity.Normal() * 1.0f);
                World.ParticleEffects.Spawn(EParticleType.WeaponPlasmaHit, Physics.Position, Visual.Color, 2.0f, -Physics.Velocity.Normal());
            }
            else
            {
                World.ParticleEffects.Spawn(EParticleType.WeaponPlasmaHit, Physics.Position, Visual.Color, null, -Physics.Velocity.Normal());
            }

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
            {
                Die();
                return;
            }

            if (AliveTime > 2)
            {
                Die();
                return;
            }
        }

        public void SetRadius(float radius)
        {
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Radius = radius;
        }
    }
}
