//
// RotateTurret.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CRotateTurret
        : CEnemy
    {
        public float FireDelay { get; private set; }
        private int FireCooldown { get; set; }
        public float FireDamage { get; private set; }
        public float FireSpeed { get; private set; }
        public float PauseCounter { get; private set; }
        public float TripleShotDelay { get; set; }
        public int TripleShotCounter { get; set; }
        private CMover OriginalMover { get; set; }
        private CMover IgnoreCameraMover { get; set; }
        private CVisual TurretVisual { get; set; }
        private float TurretRotation { get; set; }
        private float NextTurretRotation { get; set; }
        private float TurretShotOffset { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(56.0f, 56.0f));
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/RotateTurretBase");
            TurretVisual = CVisual.MakeSpriteUncached(world.Game, "Textures/Enemy/RotateTurret");
            TurretVisual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * 1.0f;
            TurretVisual.Recache();
            HealthMax = 2.25f;

            FireDelay = 0.75f;
            FireCooldown = (int)(Time.ToFrames(FireDelay) * world.Random.NextFloat());
            FireDamage = 3.0f;
            FireSpeed = CEnemy.TurretShotSpeed;
            TripleShotDelay = 0.15f;
        }

        public override void UpdateAI()
        {
            base.UpdateAI();
            UpdateFire();
            UpdateRotation();
        }

        private void UpdateFire()
        {
            FireCooldown -= 1;
            if (FireCooldown <= 0)
            {
                Fire();
            }
        }

        private void UpdateRotation()
        {
            if (TripleShotCounter == 0)
            {
                TurretRotation = Math.Min(NextTurretRotation, TurretRotation + 0.03f);
            }
        }

        private void Fire()
        {
            if (TripleShotCounter < 3)
            {
                Vector2 position = Physics.Position;
                Vector2 dir = Vector2.UnitX.Rotate(TurretRotation + TurretShotOffset);
                float rotation = dir.ToAngle();

                CEnemyShot shot1 = CEnemyShot.Spawn(World, position, rotation, FireSpeed, FireDamage);
                CEnemyShot shot2 = CEnemyShot.Spawn(World, position, rotation + MathHelper.Pi, FireSpeed, FireDamage);
                CAudio.PlaySound("EnemyShoot");

                OriginalMover = OriginalMover ?? Mover;

                if (Mover != null)
                {
                    IgnoreCameraMover = IgnoreCameraMover ?? new CMoverIgnoreCamera();
                }

                Mover = IgnoreCameraMover;
                FireCooldown = Time.ToFrames(TripleShotDelay);
                TripleShotCounter += 1;
            }
            else
            {
                FireCooldown = Time.ToFrames(FireDelay);
                Mover = OriginalMover;
                OriginalMover = null;
                TripleShotCounter = 0;
                NextTurretRotation += MathHelper.TwoPi / 8.0f;
                TurretShotOffset += MathHelper.TwoPi / 4.0f;
            }
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);
            TurretVisual.Draw(sprite_batch, Physics.Position, TurretRotation);
        }

        public override void UpdateCollision()
        {
            Vector2 offset = new Vector2(28.0f, 28.0f);
            CollisionAABB aabb = Collision as CollisionAABB;
            aabb.Position = Physics.Position - offset;
        }

        protected override bool DoesGenerateCorpse()
        {
            return true;
        }
    }
}

