//
// Pilot.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;

namespace Galaxy
{
    public abstract class CAbility
    {
        public CPilot Pilot { get; set; }

        public bool Enabled { get; set; }
        public float ActiveTime { get; set; }
        public float Active { get; set; }
        public float CooldownTime { get; set; }
        public float Cooldown { get; set; }

        public CAbility(CPilot pilot)
        {
            Pilot = pilot;
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
            public DashBurst(CPilot pilot)
                : base(pilot)
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
            public Shimmer(CPilot pilot)
                : base(pilot)
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
            public AbsorbEnergy(CPilot pilot)
                : base(pilot)
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
            public BulletReflect(CPilot pilot)
                : base(pilot)
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
            public BulletDetonate(CPilot pilot)
                : base(pilot)
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
            public BulletAlchemy(CPilot pilot)
                : base(pilot)
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
    }

    namespace Pilots
    {
        public class Kazuki
            : CPilot
        {
            public Kazuki()
            {
                Ability0 = new Abilities.DashBurst(this);
                Ability1 = new Abilities.Shimmer(this);
                Ability2 = new Abilities.AbsorbEnergy(this);
            }
        }

        public class Rabbit
            : CPilot
        {
            public Rabbit()
            {
                Ability0 = new Abilities.BulletReflect(this);
                Ability1 = new Abilities.BulletDetonate(this);
                Ability2 = new Abilities.BulletAlchemy(this);
            }
        }
    }
}