//
// Blackhole.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Galaxy
{
    public class CBlackHole
        : CEnemy
    {
        public CParticleGroupSpawner CenterEffect { get; set; }
        public CParticleGroupSpawner PullEffect { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 300.0f);
            Visual = CVisual.MakeSpriteCached1(world, "Textures/Enemy/Blackhole");
            Visual.Depth = CLayers.Weapons + CLayers.SubLayerIncrement * 1.0f;
            Visual.Recache();
            HealthMax = 0.0f;

            CenterEffect = CParticleGroupSpawner.MakeBlackHoleCenter(Vector2.Zero, CEnemy.EnemyOrangeColor);
            PullEffect = CParticleGroupSpawner.MakeBlackHolePull(Vector2.Zero, Color.Black);
        }

        public override void UpdateAI()
        {
            base.UpdateAI();
            CenterEffect.Position = Physics.PositionPhysics.Position;
            CenterEffect.Spawn(World.ParticleEffects);

            PullEffect.Position = Physics.PositionPhysics.Position - Vector2.UnitX.Rotate(World.Random.NextAngle()) * 54.0f;
            PullEffect.Spawn(World.ParticleEffects);
        }

        private void PullEntity(CEntity entity, float scale)
        {
            Vector2 offset = Physics.PositionPhysics.Position - entity.Physics.PositionPhysics.Position;
            float length = offset.Length();
            float inverse = Math.Max( 0.0f, 300.0f - length );
            float square_inverse = inverse * inverse;
            Vector2 pull = offset * square_inverse * 0.0000015f * scale;
            entity.Physics.PositionPhysics.Velocity += pull;
            PullEffect.Position = Physics.PositionPhysics.Position - offset.Normal() * 58.0f;
            PullEffect.Spawn(World.ParticleEffects);

            if (length < 42.0f)
            {
                // TODO: is this ok? no OnDie() is called!
                entity.Delete();
            }
        }

        public new void OnCollide(CShip ship)
        {
            PullEntity(ship, 0.2f);
        }

        // TODO: need to handle IsSubClassOf in the collision system
        // TODO: replace with generic CWeapon collider
        public new void OnCollide(CLaser laser)
        {
            PullEntity(laser, 1.0f);
        }

        public new void OnCollide(CMissile missile)
        {
            PullEntity(missile, 1.0f);
        }

        public new void OnCollide(CSeekBomb seek_bomb)
        {
            PullEntity(seek_bomb, 1.0f);
        }

        public new void OnCollide(CPlasma plasma)
        {
            PullEntity(plasma, 1.0f);
        }

        public new void OnCollide(CMiniShot minishot)
        {
            PullEntity(minishot, 1.0f);
        }

        public new void OnCollide(CDetonation detonation)
        {
        }

        public new void OnCollide(CEnemyShot shot)
        {
            PullEntity(shot, 1.0f);
        }

        public new void OnCollide(CEnemyLaser laser)
        {
            PullEntity(laser, 1.0f);
        }

        public new void OnCollide(CEnemyPellet pellet)
        {
            PullEntity(pellet, 1.0f);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }
    }
}

