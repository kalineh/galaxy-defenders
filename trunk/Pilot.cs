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
            if (!HasPurchased)
                return;

            if (Cooldown > 0.0f)
                return;

            Enable();
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

        public static CPilot MakePilot(string pilot)
        {
            if (pilot == "" || pilot == null)
                return new Pilots.Kazuki();

            Type type = Type.GetType("Galaxy.Pilots." + pilot);
#if XBOX360
            CPilot result = Activator.CreateInstance(type) as CPilot;
#else
            CPilot result = Activator.CreateInstance(type, null) as CPilot;
#endif
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
                    CEffect.DashBurstEffect(Pilot.Ship.World,
                                         Pilot.Ship.Physics.PositionPhysics.Position + Pilot.Ship.World.Random.NextVector2Variable() * 16.0f,
                                         0.5f,
                                         Pilot.Ship.Visual.Color);
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
                    CEffect.AbsorbBulletEffect(Pilot.Ship, 
                                         Pilot.Ship.Physics.PositionPhysics.Position,
                                         3.0f,
                                         Pilot.Ship.Visual.Color);
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
                    CEffect.ReflectBulletEffect(Pilot.Ship, 
                                         Pilot.Ship.Physics.PositionPhysics.Position,
                                         3.0f,
                                         Pilot.Ship.Visual.Color);
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
                            CDetonation.MakeDetonation(Pilot.Ship.World, shot.Physics.PositionPhysics.Position);
                            shot.Die();
                        }

                        CEnemyLaser laser = entity as CEnemyLaser;
                        if (laser != null)
                        {
                            CDetonation.MakeDetonation(Pilot.Ship.World, laser.Physics.PositionPhysics.Position);
                            laser.Die();
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
                            Pilot.Ship.World.EntityAdd(new CBonus(Pilot.Ship.World, shot.Physics.PositionPhysics.Position));
                            shot.Die();
                        }

                        CEnemyLaser laser = entity as CEnemyLaser;
                        if (laser != null)
                        {
                            Pilot.Ship.World.EntityAdd(new CBonus(Pilot.Ship.World, laser.Physics.PositionPhysics.Position));
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
                    Vector2 hit_location = Pilot.Ship.Physics.PositionPhysics.Position + Vector2.UnitY * -100.0f;
                    CEffect.Explosion(Pilot.Ship.World, hit_location, 4.0f);

                    const float Range = 150.0f;
                    foreach (CEntity entity in Pilot.Ship.World.GetEntities())
                    {
                        CBuilding building = entity as CBuilding;
                        if (building != null)
                        {
                            Vector2 offset = hit_location - building.Physics.PositionPhysics.Position;
                            float length = offset.Length();
                            if (length > Range)
                                continue;

                            CEffect.BuildingExplosion(Pilot.Ship.World, building.Physics.PositionPhysics.Position, 4.0f);
                            building.TakeDamage(1000.0f);
                        }

                        // TODO: not a duplicate of above
                        CEnemy enemy = entity as CEnemy;
                        if ((enemy as CTurret) != null || (enemy as CDownTurret) != null)
                        {
                            Vector2 offset = hit_location - enemy.Physics.PositionPhysics.Position;
                            float length = offset.Length();
                            if (length > Range)
                                continue;

                            CEffect.BuildingExplosion(Pilot.Ship.World, enemy.Physics.PositionPhysics.Position, 4.0f);
                            enemy.TakeDamage(1000.0f);
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

                Vector2 position = Pilot.Ship.Physics.PositionPhysics.Position;
                foreach (CEntity entity in Pilot.Ship.World.GetEntities())
                {
                    CEnemy enemy = entity as CEnemy;
                    if (enemy == null)
                        continue;

                    if ((enemy as CTurret) != null || (enemy as CDownTurret) != null)
                        continue;

                    Vector2 offset = position - enemy.Physics.PositionPhysics.Position;
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
                    Vector2 position = Pilot.Ship.Physics.PositionPhysics.Position;
                    foreach (CEntity entity in SuctionList)
                    {
                        CEnemy enemy = entity as CEnemy;
                        if (enemy == null)
                            continue;

                        Vector2 offset = position - enemy.Physics.PositionPhysics.Position;
                        enemy.Physics.PositionPhysics.Position += offset * 0.3f;
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
                    CEffect.ArmorRepairEffect(Pilot.Ship.World, 
                                         Pilot.Ship.Physics.PositionPhysics.Position,
                                         1.5f,
                                         Pilot.Ship.Visual.Color);

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
                Ability0 = new Abilities.DashBurst(this, CSaveData.GetCurrentProfile().Ability0);
                Ability1 = new Abilities.Shimmer(this, CSaveData.GetCurrentProfile().Ability1);
                Ability2 = new Abilities.AbsorbEnergy(this, CSaveData.GetCurrentProfile().Ability2);
            }
        }

        public class Rabbit
            : CPilot
        {
            public Rabbit()
            {
                Ability0 = new Abilities.BulletReflect(this, CSaveData.GetCurrentProfile().Ability0);
                Ability1 = new Abilities.BulletDetonate(this, CSaveData.GetCurrentProfile().Ability1);
                Ability2 = new Abilities.BulletAlchemy(this, CSaveData.GetCurrentProfile().Ability2);
            }
        }

        public class Gunthor
            : CPilot
        {
            public Gunthor()
            {
                Ability0 = new Abilities.GroundSmash(this, CSaveData.GetCurrentProfile().Ability0);
                Ability1 = new Abilities.SuctionCrusher(this, CSaveData.GetCurrentProfile().Ability1);
                Ability2 = new Abilities.ArmorRepair(this, CSaveData.GetCurrentProfile().Ability2);
                ;
            }
        }
    }
}