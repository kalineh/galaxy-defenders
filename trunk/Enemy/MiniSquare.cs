//
// CMiniSquare.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Galaxy
{
    public class CMiniSquare
        : CEnemy
    {
        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 24.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/MiniSquare");
            HealthMax = 0.15f;
            Coins = world.Random.Next() > 0.5f ? 1 : 0;
        }

        public override void UpdateAI()
        {
            Physics.Rotation += 0.01f;
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        protected override void OnDie()
        {
            string death_sound = EnemyDeathSoundStrings[World.Random.Next() % 3];
            CAudio.PlaySound(death_sound);

            Vector2 anti_camera = Vector2.Zero;
            if (Mover != null)
                anti_camera = World.ScrollSpeed * -Vector2.UnitY * 0.5f;

            anti_camera += Vector2.UnitX.Rotate(World.Random.NextAngle()) * 1.5f;

            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyOrangeColor, 0.5f, anti_camera);
            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyGrayColor, 0.5f, anti_camera);

            DropCoins();
        }
    }
}

