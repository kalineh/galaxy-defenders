//
// BigBall.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Galaxy
{
    public class CBigBall
        : CEnemy
    {
        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 56.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/BigBall");
            HealthMax = 4.0f;
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        protected override void OnDie()
        {
            Vector2 anti_camera = Vector2.Zero;
            if (Mover != null)
                anti_camera = World.ScrollSpeed * -Vector2.UnitY * 0.5f;

            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyOrangeColor, 1.5f, anti_camera);
            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyGrayColor, 1.5f, anti_camera);

            base.OnDie();
        }
    }
}

