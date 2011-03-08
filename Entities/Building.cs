//
// Building.cs
//

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CBuilding
        : CEntity
    {
        public struct SBuildingDefinition
        {
            public float HealthMax;
            public EParticleType ExplosionType;
        };

        public Dictionary<string, SBuildingDefinition> BuildingDefinitions = new Dictionary<string, SBuildingDefinition>()
        {
            {   "Building1",
                new SBuildingDefinition()
                {
                    HealthMax = 2.5f,
                    ExplosionType = EParticleType.BuildingDestroyedBig,
                }
            },
            {   "Building2",
                new SBuildingDefinition()
                {
                    HealthMax = 2.5f,
                    ExplosionType = EParticleType.BuildingDestroyedBig,
                }
            },
            {   "Building3",
                new SBuildingDefinition()
                {
                    HealthMax = 3.0f,
                    ExplosionType = EParticleType.BuildingDestroyedBig,
                }
            },
            {   "Building4",
                new SBuildingDefinition()
                {
                    HealthMax = 5.0f,
                    ExplosionType = EParticleType.BuildingDestroyedBig,
                }
            },
            {   "Building5",
                new SBuildingDefinition()
                {
                    HealthMax = 5.0f,
                    ExplosionType = EParticleType.BuildingDestroyedBig,
                }
            },
            {   "Building6",
                new SBuildingDefinition()
                {
                    HealthMax = 6.0f,
                    ExplosionType = EParticleType.BuildingDestroyedBig,
                }
            },
            {   "Building7",
                new SBuildingDefinition()
                {
                    HealthMax = 7.0f,
                    ExplosionType = EParticleType.BuildingDestroyedBig,
                }
            },
            {   "Building8",
                new SBuildingDefinition()
                {
                    HealthMax = 8.0f,
                    ExplosionType = EParticleType.BuildingDestroyedBig,
                }
            },
            {   "Building9",
                new SBuildingDefinition()
                {
                    HealthMax = 10.0f,
                    ExplosionType = EParticleType.BuildingDestroyedBig,
                }
            },
            {   "SmallBuilding1",
                new SBuildingDefinition()
                {
                    HealthMax = 1.0f,
                    ExplosionType = EParticleType.BuildingDestroyedSmall,
                }
            },
            {   "SmallBuilding2",
                new SBuildingDefinition()
                {
                    HealthMax = 1.5f,
                    ExplosionType = EParticleType.BuildingDestroyedSmall,
                }
            },
            {   "SmallBuilding3",
                new SBuildingDefinition()
                {
                    HealthMax = 1.5f,
                    ExplosionType = EParticleType.BuildingDestroyedSmall,
                }
            },
            {   "SmallBuilding4",
                new SBuildingDefinition()
                {
                    HealthMax = 2.0f,
                    ExplosionType = EParticleType.BuildingDestroyedSmall,
                }
            },
            {   "SmallBuilding5",
                new SBuildingDefinition()
                {
                    HealthMax = 2.5f,
                    ExplosionType = EParticleType.BuildingDestroyedSmall,
                }
            },
            {   "SmallBuilding6",
                new SBuildingDefinition()
                {
                    HealthMax = 5.0f,
                    ExplosionType = EParticleType.BuildingDestroyedSmall,
                }
            },
            {   "SmallBuilding7",
                new SBuildingDefinition()
                {
                    HealthMax = 6.0f,
                    ExplosionType = EParticleType.BuildingDestroyedSmall,
                }
            },
        };

        public float HealthMax { get; set; }
        public float Health { get; private set; }
        public int Coins { get; set; }
        public bool Powerup { get; set; }
        public string TextureName { get; set; }

        public CVisual VisualNormal { get; private set; }
        public CVisual VisualDestroyed { get; private set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, Vector2.Zero);
        }

        public override void Update()
        {
            base.Update();
            if (IsOffScreenBottom())
                Delete();
        }

        public void UpdateDefinition()
        {
            VisualNormal = CVisual.MakeSpriteCached1(World.Game, "Textures/Static/" + TextureName);
            VisualDestroyed = CVisual.MakeSpriteCached1(World.Game, "Textures/Static/Destroyed/" + TextureName);
            VisualNormal = VisualNormal ?? CVisual.MakeLabel(World.Game, TextureName);
            VisualDestroyed = VisualDestroyed ?? CVisual.MakeLabel(World.Game, "Destroyed/" + TextureName);
            Visual = VisualNormal;

            Vector2 texture = new Vector2(Visual.Texture.Width, Visual.Texture.Height);
            CollisionAABB aabb = Collision as CollisionAABB;
            aabb.Size = texture * Visual.Scale * 0.80f;

            SBuildingDefinition definition = BuildingDefinitions[TextureName];
            Health = definition.HealthMax;
            HealthMax = definition.HealthMax;
        }

        // TODO: CWeapon OnCollide?
        public void OnCollide(CLaser laser)
        {
            World.Stats.ShotDamageDealt += laser.Damage;
            TakeDamage(laser.Damage, laser.Owner);
            laser.Die();
        }

        public void OnCollide(CMissile missile)
        {
            World.Stats.ShotDamageDealt += missile.Damage;
            TakeDamage(missile.Damage, missile.Owner);
            missile.Die();
        }

        public void OnCollide(CPlasma plasma)
        {
            World.Stats.ShotDamageDealt += plasma.Damage;
            TakeDamage(plasma.Damage, plasma.Owner);
            plasma.Die();
        }

        public void OnCollide(CMiniShot minishot)
        {
            World.Stats.ShotDamageDealt += minishot.Damage;
            TakeDamage(minishot.Damage, minishot.Owner);
            minishot.Die();
        }

        public void OnCollide(CChargeShot chargeshot)
        {
            World.Stats.ShotDamageDealt += chargeshot.Damage;
            TakeDamage(chargeshot.Damage, chargeshot.Owner);
            chargeshot.Die();
        }

        public void OnCollide(CFlame flame)
        {
            World.Stats.ShotDamageDealt += flame.Damage;
            TakeDamage(flame.Damage, flame.Owner);
            flame.Die();
        }

        public void OnCollideSimulation(CLightning lightning)
        {
            World.Stats.ShotDamageDealt += lightning.Damage;
            TakeDamage(lightning.Damage, lightning.Owner);
            lightning.Die();
        }

        public void OnCollide(CDetonation detonation)
        {
            TakeDamage(5.0f, detonation.Owner);
        }

        public void OnCollide(CEnemyShot shot)
        {
            if (!shot.IsReflected)
                return;

            TakeDamage(shot.Damage, shot.WhoReflected);
            shot.Die();
        }

        public void OnCollide(CEnemyLaser laser)
        {
            if (!laser.IsReflected)
                return;

            TakeDamage(laser.Damage, laser.WhoReflected);
            laser.Die();
        }

        private int CalculateScoreFromHealth()
        {
            float s = HealthMax * 15.0f;
            float d = s * CDifficulty.MoneyScale[CSaveData.GetCurrentGameData(World.Game).Difficulty];
            int score = (int)d;
            return score - score % 10;
        }

        protected override void OnDie()
        {
            Vector2 center = Physics.Position;
            Vector2 directional = Vector2.UnitX.Rotate(World.Random.NextAngle()) * 0.75f;

            SBuildingDefinition definition = BuildingDefinitions[TextureName];
            EParticleType type = definition.ExplosionType;
            World.ParticleEffects.Spawn(type, center, new Color(102, 102, 102), null, directional);
            World.ParticleEffects.Spawn(type, center, new Color(229, 214, 214), null, directional);
            Visual = VisualDestroyed;
            Collision = null;

            CAudio.PlaySound("BuildingExplode", 1.0f);

            for (int i = 0; i < Coins; i++)
            {
                CBonus bonus = new CBonus();
                bonus.Initialize(World);
                bonus.Physics.Position = Physics.Position;
                World.EntityAdd(bonus);
            }

            if (Powerup)
            {
                CPowerup powerup = new CPowerup();
                powerup.Initialize(World);
                powerup.Physics.Position = Physics.Position;
                World.EntityAdd(powerup);
            }

            // TODO: cache
            CDecoration corpse = new CDecoration();
            corpse.Initialize(World);
            corpse.Physics.Position = Physics.Position;
            corpse.Visual = VisualDestroyed;
            World.EntityAdd(corpse);

            base.OnDie();
        }

        public void TakeDamage(float damage, CShip source)
        {
            Health -= damage;
            if (Health < 0.0f)
            {
                World.Stats.BuildingKills += 1;
                source.Score += CalculateScoreFromHealth();
                Die();
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB aabb = Collision as CollisionAABB;
            aabb.Position = Physics.Position - aabb.Size * 0.5f;
        }
    }
}
