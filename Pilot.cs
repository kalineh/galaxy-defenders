//
// Pilot.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;

namespace Galaxy
{
    public abstract class CAbility
    {
        public static Dictionary<string, List<string>> PilotAbilityNames = new Dictionary<string, List<string>>()
        {
            { "Kazuki", new List<string>() { "DashBurst", "Shimmer", "AbsorbEnergy" } },
            { "Rabbit", new List<string>() { "BulletReflect", "BulletDetonate", "BulletAlchemy" } },
            { "Gunthor", new List<string>() { "GroundSmash", "SuctionCrusher", "ArmorRepair" } },
            { "Mystery", new List<string>() { "", "", "" } },
        };

        public static string GetAbilityName(string pilot, int index)
        {
            return PilotAbilityNames[pilot][index];
        }

        public CPilot Pilot { get; set; }
        public bool HasPurchased { get; set; }
        public bool Enabled { get; set; }
        public float ActiveTime { get; set; }
        public float Active { get; set; }
        public float CooldownTime { get; set; }
        public float Cooldown { get; set; }

        public CAbility(CPilot pilot, bool has_purchased)
        {
            Pilot = pilot;
            HasPurchased = has_purchased;
        }

        public virtual void Enable()
        {
            Enabled = true;
            Active = ActiveTime;
            Cooldown = CooldownTime;
        }

        public virtual void Disable()
        {
            Enabled = false;
        }

        public virtual void Update()
        {
            if (Enabled)
            {
                Cooldown = CooldownTime;
                Active -= Time.SingleFrame;
                if (Active <= 0.0f)
                    Disable();
            }

            Cooldown -= Time.SingleFrame;
            Cooldown = Math.Max(0.0f, Cooldown);
        }

        public void TryEnable()
        {
            if (CanEnable())
                Enable();
        }

        public bool CanEnable()
        {
            if (!HasPurchased)
                return false;

            if (Cooldown > 0.0f)
                return false;

            return true;
        }
    }

    public abstract class CPilot
    {
        public CShip Ship { get; set; }
        public CAbility Ability0 { get; set; }
        public CAbility Ability1 { get; set; }
        public CAbility Ability2 { get; set; }

        public CPilot()
        {
        }

        public virtual void Update()
        {
            Ability0.Update();
            Ability1.Update();
            Ability2.Update();
        }

        public static CPilot MakePilot(SProfilePilotState profile)
        {
            if (profile.Pilot == "" || profile.Pilot == null)
                return new Pilots.Kazuki();

            Type type = Type.GetType("Galaxy.Pilots." + profile.Pilot);
            CPilot result = Activator.CreateInstance(type) as CPilot;
            result.Ability0.HasPurchased = profile.AbilityUnlocked0;
            result.Ability1.HasPurchased = profile.AbilityUnlocked1;
            result.Ability2.HasPurchased = profile.AbilityUnlocked2;
            return result;
        }
    }

    namespace Abilities
    {
        // dash burst
        // shimmer
        // absorb energy

        // bullet reflect
        // bullet detonate
        // bullet alchemy

        // ground smash
        // armor repair
        // suction crusher

        // ???
        // ???
        // ???

        public class DashBurst
            : CAbility
        {
            public DashBurst(CPilot pilot, bool ability0)
                : base(pilot, ability0)
            {
                ActiveTime = 3.0f;
                CooldownTime = 15.0f;
            }

            public override void Enable()
            {
                Pilot.Ship.IsInvincible += 1;
                Pilot.Ship.SpeedEnhancement += 0.15f;
                base.Enable();
            }

            public override void Disable()
            {
                Pilot.Ship.IsInvincible -= 1;
                Pilot.Ship.SpeedEnhancement -= 0.15f;
                base.Disable();
            }

            public override void Update()
            {
                if (Enabled)
                {
                    Pilot.Ship.World.ParticleEffects.Spawn(EParticleType.SkillDashBurst, Pilot.Ship.Physics.Position, Pilot.Ship.Visual.Color, null, null);
                }

                base.Update();
            }
        }

        public class Shimmer
            : CAbility
        {
            public Shimmer(CPilot pilot, bool ability1)
                : base(pilot, ability1)
            {
                ActiveTime = 3.0f;
                CooldownTime = 15.0f;
            }

            public override void Enable()
            {
                Pilot.Ship.IsIgnoreBullets += 1;
                base.Enable();
            }

            public override void Disable()
            {
                Pilot.Ship.IsIgnoreBullets -= 1;
                Pilot.Ship.Visual.Alpha = 1.0f;
                base.Disable();
            }

            public override void Update()
            {
                if (Enabled)
                {
                    Pilot.Ship.Visual.Alpha = Pilot.Ship.World.Random.NextFloat();
                }
                base.Update();
            }
        }

        public class AbsorbEnergy
            : CAbility
        {
            public AbsorbEnergy(CPilot pilot, bool ability2)
                : base(pilot, ability2)
            {
                ActiveTime = 3.0f;
                CooldownTime = 15.0f;
            }

            public override void Enable()
            {
                Pilot.Ship.IsAbsorbBullets += 1;
                base.Enable();
            }

            public override void Disable()
            {
                Pilot.Ship.IsAbsorbBullets -= 1;
                base.Disable();
            }

            public override void Update()
            {
                if (Enabled)
                {
                    Pilot.Ship.World.ParticleEffects.Spawn(EParticleType.SkillAbsorbBullet, Pilot.Ship.Physics.Position, Pilot.Ship.Visual.Color, null, null);
                }
                base.Update();
            }
        }

        public class BulletReflect
            : CAbility
        {
            public BulletReflect(CPilot pilot, bool ability0)
                : base(pilot, ability0)
            {
                ActiveTime = 6.0f;
                CooldownTime = 15.0f;
            }

            public override void Enable()
            {
                Pilot.Ship.IsReflectBullets += 1;
                base.Enable();
            }

            public override void Disable()
            {
                Pilot.Ship.IsReflectBullets -= 1;
                base.Disable();
            }

            public override void Update()
            {
                if (Enabled)
                {
                    Pilot.Ship.World.ParticleEffects.Spawn(EParticleType.SkillReflectBullet, Pilot.Ship.Physics.Position, Pilot.Ship.Visual.Color, null, null);
                }
                base.Update();
            }
        }

        public class BulletDetonate
            : CAbility
        {
            public BulletDetonate(CPilot pilot, bool ability1)
                : base(pilot, ability1)
            {
                ActiveTime = 0.0f;
                CooldownTime = 15.0f;
            }

            public override void Update()
            {
                if (Enabled)
                {
                    foreach (CEntity entity in Pilot.Ship.World.GetEntities())
                    {
                        CEnemyShot shot = entity as CEnemyShot;
                        if (shot != null)
                        {
                            CDetonation.MakeDetonation(Pilot.Ship, shot.Physics.Position);
                            shot.Die();
                        }

                        CEnemyLaser laser = entity as CEnemyLaser;
                        if (laser != null)
                        {
                            CDetonation.MakeDetonation(Pilot.Ship, laser.Physics.Position);
                            laser.Die();
                        }

                        CEnemyCannonShot cannon_shot = entity as CEnemyCannonShot;
                        if (cannon_shot != null)
                        {
                            CDetonation.MakeDetonation(Pilot.Ship, cannon_shot.Physics.Position);
                            cannon_shot.Die();
                        }

                        CEnemyMissile missile = entity as CEnemyMissile;
                        if (missile != null)
                        {
                            CDetonation.MakeDetonation(Pilot.Ship, missile.Physics.Position);
                            missile.Die();
                        }

                        CEnemyPellet pellet = entity as CEnemyPellet;
                        if (pellet != null)
                        {
                            CDetonation.MakeDetonation(Pilot.Ship, pellet.Physics.Position);
                            pellet.Die();
                        }
                    }
                }
                base.Update();
            }
        }

        public class BulletAlchemy
            : CAbility
        {
            public BulletAlchemy(CPilot pilot, bool ability2)
                : base(pilot, ability2)
            {
                ActiveTime = 0.0f;
                CooldownTime = 15.0f;
            }

            public override void Update()
            {
                if (Enabled)
                {
                    foreach (CEntity entity in Pilot.Ship.World.GetEntities())
                    {
                        CEnemyShot shot = entity as CEnemyShot;
                        if (shot != null)
                        {
                            CBonus bonus = new CBonus();
                            bonus.Initialize(Pilot.Ship.World);
                            bonus.Physics.Position = shot.Physics.Position;
                            Pilot.Ship.World.EntityAdd(bonus);
                            shot.Die();
                        }

                        CEnemyLaser laser = entity as CEnemyLaser;
                        if (laser != null)
                        {
                            CBonus bonus = new CBonus();
                            bonus.Initialize(Pilot.Ship.World);
                            bonus.Physics.Position = laser.Physics.Position;
                            Pilot.Ship.World.EntityAdd(bonus);
                            laser.Die();
                        }
                    }
                }
                base.Update();
            }
        }

        // ground smash
        // armor repair
        // suction crusher


        public class GroundSmash
            : CAbility
        {
            public GroundSmash(CPilot pilot, bool ability0)
                : base(pilot, ability0)
            {
                ActiveTime = 0.0f;
                CooldownTime = 15.0f;
            }

            public override void Update()
            {
                if (Enabled)
                {
                    Vector2 hit_location = Pilot.Ship.Physics.Position + Vector2.UnitY * -100.0f;
                    Pilot.Ship.World.ParticleEffects.Spawn(EParticleType.SkillGroundSmash, hit_location);

                    const float Range = 150.0f;
                    foreach (CEntity entity in Pilot.Ship.World.GetEntities())
                    {
                        CBuilding building = entity as CBuilding;
                        if (building != null)
                        {
                            Vector2 offset = hit_location - building.Physics.Position;
                            float length = offset.Length();
                            if (length > Range)
                                continue;

                            building.TakeDamage(1000.0f, Pilot.Ship);
                        }

                        // TODO: not a duplicate of above
                        CEnemy enemy = entity as CEnemy;
                        if (enemy is CTurret || enemy is CDownTurret)
                        {
                            Vector2 offset = hit_location - enemy.Physics.Position;
                            float length = offset.Length();
                            if (length > Range)
                                continue;

                            enemy.TakeDamage(1000.0f, Pilot.Ship);
                        }
                    }
                }
                base.Update();
            }
        }

        public class SuctionCrusher
            : CAbility
        {
            private List<CEntity> SuctionList { get; set; }

            public SuctionCrusher(CPilot pilot, bool ability1)
                : base(pilot, ability1)
            {
                ActiveTime = 1.0f;
                CooldownTime = 15.0f;
                SuctionList = new List<CEntity>();
            }

            public override void Enable()
            {
                Pilot.Ship.IsInvincible += 1;

                Vector2 position = Pilot.Ship.Physics.Position;
                foreach (CEntity entity in Pilot.Ship.World.GetEntities())
                {
                    CEnemy enemy = entity as CEnemy;
                    if (enemy == null)
                        continue;

                    if (enemy is CTurret || enemy is CDownTurret)
                        continue;

                    Vector2 offset = position - enemy.Physics.Position;
                    float length = offset.Length();
                    if (length > 400.0f)
                        continue;

                    SuctionList.Add(entity);
                }

                base.Enable();
            }

            public override void Disable()
            {
                Pilot.Ship.IsInvincible -= 1;
                SuctionList.Clear();
                base.Disable();
            }

            public override void Update()
            {
                if (Enabled)
                {
                    Vector2 position = Pilot.Ship.Physics.Position;
                    foreach (CEntity entity in SuctionList)
                    {
                        CEnemy enemy = entity as CEnemy;
                        if (enemy == null)
                            continue;

                        Vector2 offset = position - enemy.Physics.Position;
                        enemy.Physics.Position += offset * 0.3f;
                    }
                }
                base.Update();
            }
        }

        public class ArmorRepair
            : CAbility
        {
            public ArmorRepair(CPilot pilot, bool ability2)
                : base(pilot, ability2)
            {
                ActiveTime = 1.5f;
                CooldownTime = 15.0f;
            }

            public override void Enable()
            {
                Pilot.Ship.IsInvincible += 1;
                base.Enable();
            }

            public override void Disable()
            {
                Pilot.Ship.IsInvincible -= 1;
                base.Disable();
            }

            public override void Update()
            {
                if (Enabled)
                {
                    Pilot.Ship.World.ParticleEffects.Spawn(EParticleType.SkillArmorRepair, Pilot.Ship.Physics.Position);
                    Pilot.Ship.CurrentArmor += 0.05f;
                    Pilot.Ship.CurrentArmor = Math.Min(Pilot.Ship.CurrentArmor, Pilot.Ship.Chassis.Armor);
                }
                base.Update();
            }
        }
    }

    namespace Pilots
    {
        public class Kazuki
            : CPilot
        {
            public Kazuki()
            {
                Ability0 = new Abilities.DashBurst(this, false);
                Ability1 = new Abilities.Shimmer(this, false);
                Ability2 = new Abilities.AbsorbEnergy(this, false);
            }
        }

        public class Rabbit
            : CPilot
        {
            public Rabbit()
            {
                Ability0 = new Abilities.BulletReflect(this, false);
                Ability1 = new Abilities.BulletDetonate(this, false);
                Ability2 = new Abilities.BulletAlchemy(this, false);
            }
        }

        public class Gunthor
            : CPilot
        {
            public Gunthor()
            {
                Ability0 = new Abilities.GroundSmash(this, false);
                Ability1 = new Abilities.SuctionCrusher(this, false);
                Ability2 = new Abilities.ArmorRepair(this, false);
            }
        }
    }
}