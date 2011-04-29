//
// Boss4.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Galaxy
{
    public class CBoss4Chunk
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss4 Boss { get; set; }
        public Vector2 CollisionOffset { get; set; }
        public Vector2 FireOffset { get; set; }
        public delegate void OnDamageDelegate(float damage_tracker);
        public OnDamageDelegate OnDamage { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(70.0f, 70.0f));
            Visual = CVisual.MakeSpriteUncached(world.Game, "Textures/Enemy/Boss4Chunk");
            HealthMax = 1000.0f;
            Coins = 0;
            BaseScore = 0;
            FiberManager = new CFiberManager();
        }

        public override void Update()
        {
            base.Update();
            FiberManager.Update();
        }

        public override void TakeDamage(float damage, CShip source)
        {
            Boss.TakeDamageDirect(damage, source);
            OnDamage(damage);
        }

        public override float GetRadius()
        {
            // override being deleted due to offscreen
            return 512.0f;
        }

        public IEnumerable UpdateWeaponsPattern0()
        {
            while (true)
            {
                int Steps = 7;
                float Range = 0.3f;
                float Sign = World.Random.NextSign();
                Vector2 dir = Vector2.UnitY.Rotate(World.Random.NextSignedFloat() * 0.25f);
                for (int i = 0; i < Steps; ++i)
                {
                    RegularShot(Physics.Position + FireOffset, Vector2.UnitY.Rotate((Range * -0.5f + Range / (float)Steps * i) * Sign));
                    yield return 6;
                }

                yield return 90;
            }
        }

        public IEnumerable UpdateWeaponsPattern1()
        {
            while (true)
            {
                int Steps = 3;
                float Range = 0.3f;
                Vector2 dir = GetDirToShip();
                for (int i = 0; i < Steps; ++i)
                {
                    RegularShot(Physics.Position + FireOffset, dir.Rotate(Range * -0.5f + Range / (float)Steps * i));
                    yield return 3;
                }

                yield return 60;
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB collision = Collision as CollisionAABB;
            collision.Position = Physics.Position + CollisionOffset;
        }

        private void RegularShot(Vector2 position, Vector2 direction)
        {
            float rotation = direction.ToAngle();
            CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, 8.0f, 2.0f);
            CAudio.PlaySound("EnemyShoot");
        }
    }

    public class CBoss4
        : CEnemy
    {
        public CFiberManager FiberManager { get; set; }
        public CBoss4Chunk Center { get; set; }
        public CBoss4Chunk Left { get; set; }
        public CBoss4Chunk Right { get; set; }
        public float CenterOut { get; set; }
        public float LeftOut { get; set; }
        public float RightOut { get; set; }
        public float CenterDamage { get; set; }
        public float LeftDamage { get; set; }
        public float RightDamage { get; set; }
        public CVisual Cover { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(260.0f, 215.0f));
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Boss4");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            Cover = CVisual.MakeSpriteUncached(world.Game, "Textures/Enemy/Boss4Cover");
            Cover.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * 2.0f;
            HealthMax = 100.0f * CDifficulty.BossHealthScale[world.CachedDifficulty];
            Coins = 0;
            BaseScore = 0;
            CenterOut = 0.0f;
            LeftOut = 0.0f;
            RightOut = 0.0f;

            FiberManager = new CFiberManager();
            //FiberManager.Fork(this.UpdateMovement);
            //FiberManager.Fork(this.UpdateWeapons);
            FiberManager.Fork(this.UpdateCenter);
            FiberManager.Fork(this.UpdateLeft);
            FiberManager.Fork(this.UpdateRight);
        }

        public override void Update()
        {
            base.Update();
            FiberManager.Update();

            if (AliveTime == 1)
                MakeChildren();

            UpdateChildrenAttachment();

            IsSeekerTarget = true;
        }

        private void MakeChildren()
        {
            Center = new CBoss4Chunk() { Boss = this };
            Left = new CBoss4Chunk() { Boss = this };
            Right = new CBoss4Chunk() { Boss = this };

            Center.Initialize(World);
            Left.Initialize(World);
            Right.Initialize(World);

            Center.Physics.Rotation = MathHelper.PiOver2;
            Right.Physics.Rotation = MathHelper.Pi;

            Center.CollisionOffset = new Vector2(-36.0f, +140.0f);
            Left.CollisionOffset = new Vector2(+140.0f, -32.0f);
            Right.CollisionOffset = new Vector2(-214.0f, -32.0f);

            Center.FireOffset = new Vector2(-1.0f, 180.0f);
            Left.FireOffset = new Vector2(+182.0f, -6.0f);
            Right.FireOffset = new Vector2(-182.0f, -6.0f);

            Center.Visual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            Left.Visual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            Right.Visual.NormalizedOrigin = new Vector2(0.0f, 0.5f);

            Center.Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -2.0f;
            Left.Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -2.0f;
            Right.Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -2.0f;

            Center.OnDamage = (damage => this.CenterDamage += damage);
            Left.OnDamage = (damage => this.LeftDamage += damage);
            Right.OnDamage = (damage => this.RightDamage += damage);
            
            World.EntityAdd(Center);
            World.EntityAdd(Left);
            World.EntityAdd(Right);

            UpdateChildrenAttachment();
        }

        private void UpdateChildrenAttachment()
        {
            Center.Physics.Position = Physics.Position + Vector2.UnitY * -140.0f + Vector2.UnitY * -CenterOut;
            Left.Physics.Position = Physics.Position + Vector2.UnitX * -140.0f + Vector2.UnitX * -LeftOut;
            Right.Physics.Position = Physics.Position + Vector2.UnitX * +140.0f + Vector2.UnitX * +RightOut;
        }

        protected override void OnDie()
        {
            FiberManager.KillAll();
            World.DestroyAllProjectiles();
            World.DestroyAllEnemies();

            for (int i = 0; i < 14; ++i)
            {
                World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position + World.Random.NextVector2() * World.Random.NextSignedFloat() * 240.0f, CEnemy.EnemyOrangeColor, 1.5f, null);
                World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position + World.Random.NextVector2() * World.Random.NextSignedFloat() * 240.0f, CEnemy.EnemyGrayColor, 1.5f, null);
            }
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);

            if (Health > HealthMax * 0.6f)
                Cover.Draw(sprite_batch, Physics.Position, 0.0f);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB collision = Collision as CollisionAABB;
            collision.Position = Physics.Position + new Vector2(-130.0f, -120.0f);
        }

        public override void TakeDamage(float damage, CShip source)
        {
            // suppress damage
            //base.TakeDamage(damage, source);
            //Vector2 position = projectile position?
            //World.ParticleEffects.Spawn(EParticleType.WeaponIneffective, position);

            if (Health > HealthMax * 0.6f)
                return;

            base.TakeDamage(damage, source);
        }

        // TODO: maybe need to OnCollide with everything here
        
        public void TakeDamageDirect(float damage, CShip source)
        {
            if (Health > HealthMax * 0.6f && (Health - damage) < HealthMax * 0.6f)
                BreakCover();

            base.TakeDamage(damage, source);
        }

        public void BreakCover()
        {
            for (int i = 0; i < 4; ++i)
            {
                World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position + World.Random.NextVector2() * World.Random.NextSignedFloat() * 100.0f, CEnemy.EnemyOrangeColor, 1.2f, null);
                World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position + World.Random.NextVector2() * World.Random.NextSignedFloat() * 100.0f, CEnemy.EnemyGrayColor, 1.2f, null);
            }

            string death_sound = EnemyDeathSoundStrings[World.Random.Next() % 3];
            CAudio.PlaySound(death_sound);

            FiberManager.Fork(this.UpdateWeapons);
        }
        
        private IEnumerable UpdateWeapons()
        {
            yield return 60;

            Vector2 center_cannon = new Vector2(+2.0f, -58.0f);
            Vector2 left_cannon = new Vector2(-75.0f, -58.0f);
            Vector2 right_cannon = new Vector2(+75.0f, -58.0f);

            while (true)
            {
                yield return 30;

                int attack_type = World.Random.Next() % 3;

                switch (attack_type)
                {
                    case 0:
                        RegularShot(Physics.Position + left_cannon, GetDirToShip());
                        RegularShot(Physics.Position + right_cannon, GetDirToShip());
                        yield return 4;
                        RegularShot(Physics.Position + left_cannon, GetDirToShip());
                        RegularShot(Physics.Position + right_cannon, GetDirToShip());
                        yield return 4;
                        RegularShot(Physics.Position + left_cannon, GetDirToShip());
                        RegularShot(Physics.Position + right_cannon, GetDirToShip());
                        yield return 4;
                        break;

                    case 1:    
                        for (int i = 0; i < 8; ++i)
                        {
                            PelletShot(Physics.Position + center_cannon, GetDirToShip());
                            yield return 6;
                        }
                        break;

                    case 2:
                        for (int i = 0; i < 8; ++i)
                        {
                            PelletShot(Physics.Position + center_cannon, Vector2.UnitY.Rotate(World.Random.NextSignedFloat() * 0.1f * World.Random.NextFloat()));
                            yield return 6;
                        }
                        break;
                }
            }
        }

        private IEnumerable UpdateCenter()
        {
            while (true)
            {
                yield return 30 + World.Random.Next() % 120;

                for (int i = 0; i < 60; ++i)
                {
                    CenterOut = Interpolation.MoveToValue(CenterOut, -100.0f, 0.05f);
                    yield return 0;
                }

                switch (World.Random.Next() % 2)
                {
                    case 0: Center.FiberManager.Fork(Center.UpdateWeaponsPattern0); break;
                    case 1: Center.FiberManager.Fork(Center.UpdateWeaponsPattern1); break;
                }

                while (CenterDamage < 5.0f || Health < HealthMax * 0.30f)
                {
                    yield return 30;
                }

                CenterDamage = 0.0f;
                Center.FiberManager.Kill(Center.UpdateWeaponsPattern0);
                Center.FiberManager.Kill(Center.UpdateWeaponsPattern1);
                string death_sound = EnemyDeathSoundStrings[World.Random.Next() % 3];
                CAudio.PlaySound(death_sound);

                World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Center.Physics.Position + Center.FireOffset, CEnemy.EnemyOrangeColor, null, null);
                World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Center.Physics.Position + Center.FireOffset, CEnemy.EnemyGrayColor, null, null);

                for (int i = 0; i < 30; ++i)
                {
                    CenterOut = Interpolation.MoveToValue(CenterOut, 0.0f, 0.10f);
                    yield return 0;
                }

                yield return 120;
            }
        }

        private IEnumerable UpdateLeft()
        {
            yield return 120;

            while (true)
            {
                yield return 30 + World.Random.Next() % 120;

                for (int i = 0; i < 60; ++i)
                {
                    LeftOut = Interpolation.MoveToValue(LeftOut, -140.0f, 0.05f);
                    yield return 0;
                }

                switch (World.Random.Next() % 2)
                {
                    case 0: Left.FiberManager.Fork(Left.UpdateWeaponsPattern0); break;
                    case 1: Left.FiberManager.Fork(Left.UpdateWeaponsPattern1); break;
                }

                while (LeftDamage < 5.0f || Health < HealthMax * 0.25f)
                {
                    yield return 30;
                }

                LeftDamage = 0.0f;
                Left.FiberManager.Kill(Left.UpdateWeaponsPattern0);
                Left.FiberManager.Kill(Left.UpdateWeaponsPattern1);
                string death_sound = EnemyDeathSoundStrings[World.Random.Next() % 3];
                CAudio.PlaySound(death_sound);

                World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Left.Physics.Position + Left.FireOffset, CEnemy.EnemyOrangeColor, null, null);
                World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Left.Physics.Position + Left.FireOffset, CEnemy.EnemyGrayColor, null, null);

                for (int i = 0; i < 30; ++i)
                {
                    LeftOut = Interpolation.MoveToValue(LeftOut, 0.0f, 0.10f);
                    yield return 0;
                }

                yield return 120;
            }
        }

        private IEnumerable UpdateRight()
        {
            yield return 120;

            while (true)
            {
                yield return 30 + World.Random.Next() % 120;

                for (int i = 0; i < 60; ++i)
                {
                    RightOut = Interpolation.MoveToValue(RightOut, -140.0f, 0.05f);
                    yield return 0;
                }

                switch (World.Random.Next() % 2)
                {
                    case 0: Right.FiberManager.Fork(Right.UpdateWeaponsPattern0); break;
                    case 1: Right.FiberManager.Fork(Right.UpdateWeaponsPattern1); break;
                }

                while (RightDamage < 5.0f || Health < HealthMax * 0.25f)
                {
                    yield return 30;
                }

                RightDamage = 0.0f;
                Right.FiberManager.Kill(Right.UpdateWeaponsPattern0);
                Right.FiberManager.Kill(Right.UpdateWeaponsPattern1);
                string death_sound = EnemyDeathSoundStrings[World.Random.Next() % 3];
                CAudio.PlaySound(death_sound);

                World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Right.Physics.Position + Right.FireOffset, CEnemy.EnemyOrangeColor, null, null);
                World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Right.Physics.Position + Right.FireOffset, CEnemy.EnemyGrayColor, null, null);

                for (int i = 0; i < 30; ++i)
                {
                    RightOut = Interpolation.MoveToValue(RightOut, 0.0f, 0.10f);
                    yield return 0;
                }

                yield return 120;
            }
        }

        private IEnumerable UpdateMovement()
        {
            yield return 120;

            Vector2 base_ = Physics.Position;
            while (true)
            {
                Vector2 target = new Vector2(
                    (float)Math.Sin(AliveTime * 0.01f) * 40.0f,
                    (float)-Math.Sin(AliveTime * 0.02f) * 20.0f
                );

                Physics.Position = Vector2.Lerp(Physics.Position, base_ + target, 0.1f);
                yield return 0;
            }
        }

        private void RegularShot(Vector2 position, Vector2 direction)
        {
            float rotation = direction.ToAngle();
            CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, 8.0f, 2.0f);
            CAudio.PlaySound("EnemyShoot");
        }

        private void PelletShot(Vector2 position, Vector2 direction)
        {
            float rotation = direction.ToAngle();
            CEnemyPellet pellet = CEnemyPellet.Spawn(World, position, rotation, 5.0f, 2.0f);
            CAudio.PlaySound("EnemyShoot");
        }
    }
}

