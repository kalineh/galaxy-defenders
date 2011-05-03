//
// WeaponDefinitions.cs
//

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponFactory
    {
        public static List<CWeapon> GenerateWeapon(CEntity owner, CWeaponPart weapon_part)
        {
            string typename = weapon_part.Type;
            int level = weapon_part.Level;

            if (!WeaponDefinitions.Items.ContainsKey(typename))
            {
                return new List<CWeapon>();
            }

            List<WeaponDefinitions.SWeaponData> weapon_data = WeaponDefinitions.Items[typename].Data[level];
            List<CWeapon> weapons = new List<CWeapon>();

            foreach (WeaponDefinitions.SWeaponData data in weapon_data)
            {
                Type type = Type.GetType("Galaxy.CWeapon" + typename);
                CWeapon weapon = Activator.CreateInstance(type) as CWeapon;
                weapon.Initialize(owner as CShip);
                weapon.ApplyWeaponData(data);

                weapon.Damage *= 1.0f + (owner as CShip).Pilot.BonusDamage;

                // TODO: cleanup
                weapon.Sound = WeaponDefinitions.Items[typename].Sound;

                weapons.Add(weapon);
            }

            return weapons;
        }

        public static int GetMaxLevel(string typename)
        {
            if (!WeaponDefinitions.Items.ContainsKey(typename))
                return 0;

            return WeaponDefinitions.Items[typename].Data.Count - 1;
        }

        public static bool CanUpgrade(string typename, int level, int steps)
        {
            if (!WeaponDefinitions.Items.ContainsKey(typename))
                return false;

            return WeaponDefinitions.Items[typename].Data.Count > level + steps;
        }

        public static bool CanUpgrade(CWeaponPart weapon_part, int steps)
        {
            return CanUpgrade(weapon_part.Type, weapon_part.Level, steps);
        }

        public static bool CanDowngrade(string typename, int level, int steps)
        {
            return level >= steps;
        }

        public static bool CanDowngrade(CWeaponPart weapon_part, int steps)
        {
            return CanDowngrade(weapon_part.Type, weapon_part.Level, steps);
        }

        public static string GetNextWeaponInCycle(string current, List<string> types)
        {
            if (current == "")
                return types[0];

            int index = types.FindIndex(s => s == current);
            if (index == types.Count)
                return "";

            int next = index + 1;
            return types[next % types.Count];
        }

        public static int GetPriceForLevel(string typename, int level)
        {
            // TODO: not this badness
            if (typename == "")
                return 0;

            // TODO: is this price calculation ok?
            int calculable_level = level;
            int base_ = WeaponDefinitions.Items[typename].BasePrice;
            int total = base_ + (int)((float)(base_ * calculable_level * calculable_level) * 0.75f);
            int rounded = total - total % 100;
            return rounded;
        }

        public static int GetTotalPriceForLevel(string typename, int level)
        {
            int total = 0;
            foreach (int index in Enumerable.Range(0, level + 1))
            {
                total += GetPriceForLevel(typename, index);
            }
            return total;
        }
    }

    public class WeaponDefinitions
    {
        public struct SWeaponData
        {
            public float ReloadTime { get; set; }
            public float Speed { get; set; }
            public float Damage { get; set; }
            public float KickbackForce { get; set; }
            public Vector2 Offset { get; set; }
            public float Rotation { get; set; }
            public float Energy { get; set; }
            public float ChargeSpeed { get; set; }
            public int AutoDischarge { get; set; }
            public object CustomData { get; set; }
            public bool ToggleWeapon { get; set; }
            public float ToggleEnergyDrain { get; set; }
            public float RandomRotation { get; set; }
            public float RandomReloadTime { get; set; }
            public bool ShowSidekick { get; set; }
        }

        public struct SWeaponDefinition
        {
            public int BasePrice;
            public string Sound;
            public string DisplayName;
            public List<List<SWeaponData>> Data;
        }

        public static CWeaponPart GetPart(string typename, int level)
        {
            return new CWeaponPart()
            {
                Type = typename,
                Level = level,
            };
        }

        public static SWeaponData MakeFrontLaserData(float offset)
        {
            return new SWeaponData()
            {
                ReloadTime = 0.16f,
                Speed = 17.0f,
                Damage = 0.1f,
                KickbackForce = 0.0f,
                Offset = Vector2.UnitY * offset,
                Rotation = 0.0f,
                Energy = 0.1f,
            };
        }

        public static SWeaponData MakeFrontLaserFocusData(float offset, float reload)
        {
            return new SWeaponData()
            {
                ReloadTime = reload,
                Speed = 19.0f,
                Damage = 0.15f,
                KickbackForce = 0.0f,
                Offset = Vector2.UnitY * offset,
                Rotation = 0.0f,
                Energy = 0.25f,
            };
        }

        public static SWeaponData MakeBigFrontLaserData(float offset)
        {
            return new SWeaponData()
            {
                ReloadTime = 0.16f,
                Speed = 17.0f,
                Damage = 0.25f,
                KickbackForce = 0.0f,
                Offset = Vector2.UnitY * offset,
                Rotation = 0.0f,
                Energy = 0.15f,
            };
        }

        public static SWeaponData MakeSpreadLaserData(float rotation)
        {
            return new SWeaponData()
            {
                ReloadTime = 0.18f,
                Speed = 15.0f,
                Damage = 0.1f,
                KickbackForce = 0.0f,
                Offset = new Vector2(0.0f, 0.0f),
                Rotation = MathHelper.ToRadians(rotation),
                Energy = 0.1f,
            };
        }

        public static SWeaponData MakeSpreadLaserFocusData(float rotation, float reload)
        {
            return new SWeaponData()
            {
                ReloadTime = reload,
                Speed = 15.0f,
                Damage = 0.125f,
                KickbackForce = 0.0f,
                Offset = new Vector2(0.0f, 0.0f),
                Rotation = MathHelper.ToRadians(rotation),
                Energy = 0.15f,
                RandomRotation = MathHelper.ToRadians(5.0f),
            };
        }

        public static SWeaponData MakeBigSpreadLaserData(float rotation)
        {
            return new SWeaponData()
            {
                ReloadTime = 0.18f,
                Speed = 15.0f,
                Damage = 0.25f,
                KickbackForce = 0.0f,
                Offset = new Vector2(0.0f, 0.0f),
                Rotation = MathHelper.ToRadians(rotation),
                Energy = 0.15f,
            };
        }

        public static SWeaponData MakeLaserShotData(float offset)
        {
            return new SWeaponData()
            {
                ReloadTime = 0.16f,
                Speed = 17.0f,
                Damage = 0.1f,
                KickbackForce = 0.0f,
                Offset = Vector2.UnitY * offset,
                Rotation = 0.0f,
                Energy = 0.1f,
                ChargeSpeed = 1.0f,
                AutoDischarge = 1,
            };
        }

        public static SWeaponData MakePlasmaData(Vector2 offset, float rotation)
        {
            return new SWeaponData()
            {
                ReloadTime = 0.25f,
                Speed = 12.0f,
                Damage = 0.15f,
                KickbackForce = 0.0f,
                Offset = offset,
                Rotation = MathHelper.ToRadians(rotation),
                Energy = 0.175f,
            };
        }

        public static SWeaponData MakePlasmaFocusData(Vector2 offset, float rotation)
        {
            return new SWeaponData()
            {
                ReloadTime = 0.25f,
                Speed = 12.0f,
                Damage = 0.75f,
                KickbackForce = 0.0f,
                Offset = offset,
                Rotation = MathHelper.ToRadians(rotation),
                Energy = 1.0f,
            };
        }

        public static SWeaponData MakeFlameData(float reload, float offset)
        {
            return new SWeaponData()
            {
                ReloadTime = reload,
                Speed = 15.0f,
                Damage = 0.35f,
                KickbackForce = 0.0f,
                Offset = Vector2.UnitY * offset,
                Rotation = 0.0f,
                Energy = 0.15f,
                RandomReloadTime = 0.1f,
                CustomData = new FlameCustomData() { Lifetime = 20, Friction = 0.99f, SprayAngle = 0.1f, StartRotation = MathHelper.ToRadians(-90.0f), }
            };
        }

        public static SWeaponData MakeBigFlameData(float reload, float offset)
        {
            return new SWeaponData()
            {
                ReloadTime = reload,
                Speed = 15.0f,
                Damage = 1.0f,
                KickbackForce = 0.0f,
                Offset = Vector2.UnitY * offset,
                Rotation = 0.0f,
                Energy = 0.25f,
                CustomData = new FlameCustomData() { Lifetime = 20, Friction = 0.99f, SprayAngle = 0.05f, StartRotation = MathHelper.ToRadians(-90.0f), },
            };
        }

        public static SWeaponData MakeFlameFocusData(float reload, float rotation)
        {
            return new SWeaponData()
            {
                ReloadTime = reload,
                Speed = 15.0f,
                Damage = 0.35f,
                KickbackForce = 0.0f,
                Offset = Vector2.UnitY * -2.0f,
                Rotation = 0.0f,
                Energy = 0.15f,
                //RandomReloadTime = 0.1f,
                CustomData = new FlameCustomData() { Lifetime = 20, Friction = 0.99f, SprayAngle = 0.0f, StartRotation = MathHelper.ToRadians(rotation), FireRotationSpeed = 0.070f },
            };
        }


        public static SWeaponData MakeVulcanData(float reload, float spray)
        {
            return new SWeaponData()
            {
                ReloadTime = reload,
                Speed = 17.0f,
                Damage = 0.075f,
                KickbackForce = 0.0f,
                Offset = Vector2.Zero,
                Rotation = 0.0f,
                Energy = 0.09f,
                CustomData = new VulcanCustomData() { SprayAngle = spray },
            };
        }

        public static SWeaponData MakeBombletData(float reload)
        {
            return new SWeaponData()
            {
                ReloadTime = reload,
                Speed = 1.25f,
                Damage = 1.25f,
                KickbackForce = 0.0f,
                Offset = Vector2.Zero,
                Rotation = 0.0f,
                Energy = 0.30f,
                CustomData = null,
            };
        }

        public static SWeaponData MakeLightningData(float offset, int bounces)
        {
            return new SWeaponData()
            {
                ReloadTime = 0.2f,
                Speed = 16.0f,
                Damage = 0.1f,
                KickbackForce = 0.0f,
                Offset = Vector2.UnitY * offset,
                Rotation = 0.0f,
                Energy = 0.20f,
                CustomData = new LightningCustomData() { Bounces = bounces },
            };
        }

        public static SWeaponData MakeLightningFocusData(float offset, int bounces, float reload, float angle)
        {
            return new SWeaponData()
            {
                ReloadTime = reload,
                Speed = 16.0f,
                Damage = 0.15f,
                KickbackForce = 0.0f,
                Offset = Vector2.UnitY * offset,
                Rotation = 0.0f,
                Energy = 0.275f,
                RandomRotation = MathHelper.ToRadians(angle),
                CustomData = new LightningCustomData() { Bounces = bounces },
            };
        }

        public static SWeaponData MakeBeamData(float damage, float drain, float width)
        {
            return new SWeaponData()
            {
                ReloadTime = 0.0f,
                Speed = 0.0f,
                Damage = damage,
                KickbackForce = 0.0f,
                Offset = Vector2.UnitY * 1.0f,
                Rotation = 0.0f,
                Energy = 0.0f,
                CustomData = new BeamCustomData() { Width = width, },
                ToggleWeapon = true,
                ToggleEnergyDrain = drain,
            };
        }

        public static SWeaponData MakeBeamFocusData(float damage, float drain, float radius)
        {
            return new SWeaponData()
            {
                ReloadTime = 0.0f,
                Speed = 0.0f,
                Damage = damage,
                KickbackForce = 0.0f,
                Offset = Vector2.UnitY * 1.0f,
                Rotation = 0.0f,
                Energy = 0.0f,
                CustomData = new BeamFocusCustomData() { Radius = radius, },
                ToggleWeapon = true,
                ToggleEnergyDrain = drain,
            };
        }

        public static SWeaponData MakeMissileData(Vector2 offset)
        {
            return new SWeaponData()
            {
                ReloadTime = 0.5f,
                Speed = 17.0f,
                Damage = 0.2f,
                KickbackForce = 0.0f,
                Offset = offset,
                Rotation = MathHelper.ToRadians(180.0f),
                Energy = 0.2f,
            };
        }

        public static SWeaponData MakeMissileLauncherData(Vector2 offset)
        {
            return new SWeaponData()
            {
                ReloadTime = 0.5f,
                Speed = 17.0f,
                Damage = 0.2f,
                KickbackForce = 0.0f,
                Offset = offset,
                Rotation = MathHelper.ToRadians(180.0f),
                Energy = 0.2f,
                ChargeSpeed = 1.0f,
                AutoDischarge = 1,
            };
        }

        public static SWeaponData MakeDrunkMissileData(Vector2 offset, float reload)
        {
            return new SWeaponData()
            {
                ReloadTime = reload,
                Speed = 10.0f,
                Damage = 0.15f,
                KickbackForce = 0.0f,
                Offset = offset,
                Rotation = MathHelper.ToRadians(180.0f),
                Energy = 0.2f,
            };
        }

        public static SWeaponData MakeMissileVolleyData(Vector2 offset)
        {
            return new SWeaponData()
            {
                ReloadTime = 1.0f,
                Speed = 10.0f,
                Damage = 0.20f,
                KickbackForce = 0.5f,
                Offset = offset,
                Rotation = MathHelper.ToRadians(180.0f),
                Energy = 0.10f,
                ChargeSpeed = Time.ToSeconds(2),
                AutoDischarge = 1,
            };
        }

        public static SWeaponData MakeSeekBombData(Vector2 offset, float angle)
        {
            return new SWeaponData()
            {
                ReloadTime = 0.7f,
                Speed = 14.0f,
                Damage = 0.1f,
                KickbackForce = 0.0f,
                Offset = new Vector2(0.0f, -10.0f),
                Rotation = MathHelper.ToRadians(angle),
                Energy = 0.1f,
            };
        }

        public static SWeaponData MakeBoomerangData(Vector2 offset, float angle, float reload)
        {
            return new SWeaponData()
            {
                ReloadTime = reload,
                Speed = 11.0f,
                Damage = 0.020f,
                KickbackForce = 0.0f,
                Offset = offset,
                Rotation = MathHelper.ToRadians(angle),
                Energy = 0.30f,
                CustomData = new BoomerangCustomData() { SprayAngle = 0.0f },
            };
        }


        public static Dictionary<string, SWeaponDefinition> Items = new Dictionary<string, SWeaponDefinition>()
        {
            { "FrontLaser",
                new SWeaponDefinition()
                {
                    BasePrice = 650,
                    DisplayName = "Front\nLaser",
                    Sound = "WeaponShootLaser",
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakeFrontLaserData(0.0f),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakeFrontLaserData(-10.0f),
                            MakeFrontLaserData(+10.0f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeFrontLaserData(-15.0f),
                            MakeFrontLaserData(-5.0f),
                            MakeFrontLaserData(+5.0f),
                            MakeFrontLaserData(+15.0f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeFrontLaserData(-20.0f),
                            MakeFrontLaserData(-10.0f),
                            MakeBigFrontLaserData(+0.0f),
                            MakeFrontLaserData(+10.0f),
                            MakeFrontLaserData(+20.0f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeFrontLaserData(-25.0f),
                            MakeFrontLaserData(-15.0f),
                            MakeBigFrontLaserData(-5.0f),
                            MakeBigFrontLaserData(+5.0f),
                            MakeFrontLaserData(+15.0f),
                            MakeFrontLaserData(+25.0f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeFrontLaserData(-25.0f),
                            MakeBigFrontLaserData(-15.0f),
                            MakeBigFrontLaserData(-5.0f),
                            MakeBigFrontLaserData(+5.0f),
                            MakeBigFrontLaserData(+15.0f),
                            MakeFrontLaserData(+25.0f),
                        },
                        // level 7
                        new List<SWeaponData>() {
                            MakeBigFrontLaserData(-25.0f),
                            MakeBigFrontLaserData(-15.0f),
                            MakeBigFrontLaserData(-5.0f),
                            MakeBigFrontLaserData(+5.0f),
                            MakeBigFrontLaserData(+15.0f),
                            MakeBigFrontLaserData(+25.0f),
                        },
                        // level 8
                        new List<SWeaponData>() {
                            MakeBigFrontLaserData(-35.0f),
                            MakeBigFrontLaserData(-25.0f),
                            MakeBigFrontLaserData(-15.0f),
                            MakeBigFrontLaserData(-5.0f),
                            MakeBigFrontLaserData(+5.0f),
                            MakeBigFrontLaserData(+15.0f),
                            MakeBigFrontLaserData(+25.0f),
                            MakeBigFrontLaserData(+35.0f),
                        },
                    },
                }
            },

            { "FrontLaserFocus",
                new SWeaponDefinition()
                {
                    Sound = "WeaponShootLaser",
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakeFrontLaserFocusData(0.0f, 0.06f),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakeFrontLaserFocusData(0.0f, 0.05f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeFrontLaserFocusData(0.0f, 0.04f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeFrontLaserFocusData(-3.0f, 0.05f),
                            MakeFrontLaserFocusData(+3.0f, 0.05f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeFrontLaserFocusData(-3.0f, 0.04f),
                            MakeFrontLaserFocusData(+3.0f, 0.04f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeFrontLaserFocusData(-3.0f, 0.03f),
                            MakeFrontLaserFocusData(+3.0f, 0.03f),
                        },
                        // level 7
                        new List<SWeaponData>() {
                            MakeFrontLaserFocusData(-4.0f, 0.04f),
                            MakeFrontLaserFocusData(-0.0f, 0.04f),
                            MakeFrontLaserFocusData(+4.0f, 0.04f),
                        },
                        // level 8
                        new List<SWeaponData>() {
                            MakeFrontLaserFocusData(-4.0f, 0.03f),
                            MakeFrontLaserFocusData(-0.0f, 0.03f),
                            MakeFrontLaserFocusData(+4.0f, 0.03f),
                        },
                    },
                }
            },

            { "SpreadLaser",
                new SWeaponDefinition()
                {
                    BasePrice = 700,
                    DisplayName = "Spread\nLaser",
                    Sound = "WeaponShootLaser",
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakeSpreadLaserData(0.0f),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakeSpreadLaserData(-2.0f),
                            MakeSpreadLaserData(+2.0f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeSpreadLaserData(-6.0f),
                            MakeSpreadLaserData(-2.0f),
                            MakeSpreadLaserData(2.0f),
                            MakeSpreadLaserData(6.0f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeSpreadLaserData(-8.0f),
                            MakeSpreadLaserData(-4.0f),
                            MakeSpreadLaserData(0.0f),
                            MakeSpreadLaserData(4.0f),
                            MakeSpreadLaserData(8.0f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeSpreadLaserData(-8.0f),
                            MakeSpreadLaserData(-4.0f),
                            MakeBigSpreadLaserData(0.0f),
                            MakeSpreadLaserData(4.0f),
                            MakeSpreadLaserData(8.0f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeSpreadLaserData(-12.0f),
                            MakeSpreadLaserData(-7.0f),
                            MakeBigSpreadLaserData(-2.0f),
                            MakeBigSpreadLaserData(+2.0f),
                            MakeSpreadLaserData(+7.0f),
                            MakeSpreadLaserData(+12.0f),
                        },
                        // level 7
                        new List<SWeaponData>() {
                            MakeSpreadLaserData(-11.0f),
                            MakeBigSpreadLaserData(-7.0f),
                            MakeBigSpreadLaserData(-3.0f),
                            MakeBigSpreadLaserData(-0.0f),
                            MakeBigSpreadLaserData(+3.0f),
                            MakeBigSpreadLaserData(+7.0f),
                            MakeSpreadLaserData(+11.0f),
                        },
                        // level 8
                        new List<SWeaponData>() {
                            MakeBigSpreadLaserData(-11.0f),
                            MakeBigSpreadLaserData(-7.0f),
                            MakeBigSpreadLaserData(-4.0f),
                            MakeBigSpreadLaserData(-1.0f),
                            MakeBigSpreadLaserData(+1.0f),
                            MakeBigSpreadLaserData(+4.0f),
                            MakeBigSpreadLaserData(+7.0f),
                            MakeBigSpreadLaserData(+11.0f),
                        },
                    },
                }
            },

            { "SpreadLaserFocus",
                new SWeaponDefinition()
                {
                    Sound = "WeaponShootLaser",
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakeSpreadLaserFocusData(-80.0f, 0.08f),
                            MakeSpreadLaserFocusData(-80.0f, 0.08f),
                            MakeSpreadLaserFocusData(+80.0f, 0.08f),
                            MakeSpreadLaserFocusData(+80.0f, 0.08f),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakeSpreadLaserFocusData(-80.0f, 0.07f),
                            MakeSpreadLaserFocusData(-80.0f, 0.07f),
                            MakeSpreadLaserFocusData(+80.0f, 0.07f),
                            MakeSpreadLaserFocusData(+80.0f, 0.07f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeSpreadLaserFocusData(-80.0f, 0.06f),
                            MakeSpreadLaserFocusData(-80.0f, 0.06f),
                            MakeSpreadLaserFocusData(+80.0f, 0.06f),
                            MakeSpreadLaserFocusData(+80.0f, 0.06f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeSpreadLaserFocusData(-80.0f, 0.05f),
                            MakeSpreadLaserFocusData(-80.0f, 0.05f),
                            MakeSpreadLaserFocusData(+80.0f, 0.05f),
                            MakeSpreadLaserFocusData(+80.0f, 0.05f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeSpreadLaserFocusData(-80.0f, 0.04f),
                            MakeSpreadLaserFocusData(-80.0f, 0.04f),
                            MakeSpreadLaserFocusData(+80.0f, 0.04f),
                            MakeSpreadLaserFocusData(+80.0f, 0.04f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeSpreadLaserFocusData(-80.0f, 0.03f),
                            MakeSpreadLaserFocusData(-80.0f, 0.03f),
                            MakeSpreadLaserFocusData(+80.0f, 0.03f),
                            MakeSpreadLaserFocusData(+80.0f, 0.03f),
                        },
                        // level 7
                        new List<SWeaponData>() {
                            MakeSpreadLaserFocusData(-80.0f, 0.02f),
                            MakeSpreadLaserFocusData(-80.0f, 0.02f),
                            MakeSpreadLaserFocusData(+80.0f, 0.02f),
                            MakeSpreadLaserFocusData(+80.0f, 0.02f),
                        },
                        // level 8
                        new List<SWeaponData>() {
                            MakeSpreadLaserFocusData(-80.0f, 0.01f),
                            MakeSpreadLaserFocusData(-80.0f, 0.01f),
                            MakeSpreadLaserFocusData(+80.0f, 0.01f),
                            MakeSpreadLaserFocusData(+80.0f, 0.01f),
                        },
                    },
                }
            },

            { "Plasma", 
                new SWeaponDefinition() {
                    Sound = "WeaponShootPlasma",
                    DisplayName = "Plasma\nShot",
                    BasePrice = 1250,
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakePlasmaData(new Vector2(+0.0f, +0.0f), +0.0f),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakePlasmaData(new Vector2(+0.0f, -10.0f), -1.0f),
                            MakePlasmaData(new Vector2(+0.0f, +10.0f), +1.0f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakePlasmaData(new Vector2(-1.0f, -10.0f), -2.0f),
                            MakePlasmaData(new Vector2(-0.0f, +0.0f), +0.0f),
                            MakePlasmaData(new Vector2(+1.0f, +10.0f), +2.0f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakePlasmaData(new Vector2(-2.0f, -13.0f), -2.0f),
                            MakePlasmaData(new Vector2(-5.0f, +0.0f), +0.0f),
                            MakePlasmaData(new Vector2(-5.0f, +0.0f), +0.0f),
                            MakePlasmaData(new Vector2(-2.0f, +13.0f), +2.0f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakePlasmaData(new Vector2(-5.0f, -19.0f), -4.0f),
                            MakePlasmaData(new Vector2(-3.0f, -10.0f), -2.0f),
                            MakePlasmaData(new Vector2(+0.0f, +0.0f), -0.0f),
                            MakePlasmaData(new Vector2(-3.0f, +10.0f), +2.0f),
                            MakePlasmaData(new Vector2(-5.0f, +19.0f), +4.0f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakePlasmaData(new Vector2(-8.0f, -19.0f), -3.5f),
                            MakePlasmaData(new Vector2(-5.0f, -10.0f), -2.0f),
                            MakePlasmaData(new Vector2(+3.0f, -5.0f), -1.0f),
                            MakePlasmaData(new Vector2(+0.0f, +0.0f), -0.0f),
                            MakePlasmaData(new Vector2(+3.0f, +5.0f), +1.0f),
                            MakePlasmaData(new Vector2(-5.0f, +10.0f), +2.0f),
                            MakePlasmaData(new Vector2(-8.0f, +19.0f), +3.5f),
                        },
                        // level 7
                        new List<SWeaponData>() {
                            MakePlasmaData(new Vector2(-8.0f, -19.0f), -3.5f),
                            MakePlasmaData(new Vector2(-5.0f, -10.0f), -2.5f),
                            MakePlasmaData(new Vector2(+3.0f, -5.0f), -1.5f),
                            MakePlasmaData(new Vector2(+0.0f, -0.0f), -0.5f),
                            MakePlasmaData(new Vector2(+0.0f, +0.0f), +0.5f),
                            MakePlasmaData(new Vector2(+3.0f, +5.0f), +1.0f),
                            MakePlasmaData(new Vector2(-5.0f, +10.0f), +3.0f),
                            MakePlasmaData(new Vector2(-8.0f, +19.0f), +5.0f),
                        },
                    },
                }
            },

            { "PlasmaFocus", 
                new SWeaponDefinition() {
                    Sound = "WeaponShootPlasma",
                    DisplayName = "Plasma\nShot",
                    BasePrice = 1250,
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakePlasmaFocusData(new Vector2(+0.0f, +0.0f), +0.0f),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakePlasmaFocusData(new Vector2(+0.0f, -10.0f), -1.0f),
                            MakePlasmaFocusData(new Vector2(+0.0f, +10.0f), +1.0f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakePlasmaFocusData(new Vector2(-1.0f, -10.0f), -2.0f),
                            MakePlasmaFocusData(new Vector2(-0.0f, +0.0f), +0.0f),
                            MakePlasmaFocusData(new Vector2(+1.0f, +10.0f), +2.0f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakePlasmaFocusData(new Vector2(-2.0f, -13.0f), -2.0f),
                            MakePlasmaFocusData(new Vector2(-5.0f, +0.0f), +0.0f),
                            MakePlasmaFocusData(new Vector2(-5.0f, +0.0f), +0.0f),
                            MakePlasmaFocusData(new Vector2(-2.0f, +13.0f), +2.0f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakePlasmaFocusData(new Vector2(-5.0f, -19.0f), -4.0f),
                            MakePlasmaFocusData(new Vector2(-3.0f, -10.0f), -2.0f),
                            MakePlasmaFocusData(new Vector2(+0.0f, +0.0f), -0.0f),
                            MakePlasmaFocusData(new Vector2(-3.0f, +10.0f), +2.0f),
                            MakePlasmaFocusData(new Vector2(-5.0f, +19.0f), +4.0f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakePlasmaFocusData(new Vector2(-8.0f, -19.0f), -3.5f),
                            MakePlasmaFocusData(new Vector2(-5.0f, -10.0f), -2.0f),
                            MakePlasmaFocusData(new Vector2(+3.0f, -5.0f), -1.0f),
                            MakePlasmaFocusData(new Vector2(+0.0f, +0.0f), -0.0f),
                            MakePlasmaFocusData(new Vector2(+3.0f, +5.0f), +1.0f),
                            MakePlasmaFocusData(new Vector2(-5.0f, +10.0f), +2.0f),
                            MakePlasmaFocusData(new Vector2(-8.0f, +19.0f), +3.5f),
                        },
                        // level 7
                        new List<SWeaponData>() {
                            MakePlasmaFocusData(new Vector2(-8.0f, -19.0f), -3.5f),
                            MakePlasmaFocusData(new Vector2(-5.0f, -10.0f), -2.5f),
                            MakePlasmaFocusData(new Vector2(+3.0f, -5.0f), -1.5f),
                            MakePlasmaFocusData(new Vector2(+0.0f, -0.0f), -0.5f),
                            MakePlasmaFocusData(new Vector2(+0.0f, +0.0f), +0.5f),
                            MakePlasmaFocusData(new Vector2(+3.0f, +5.0f), +1.0f),
                            MakePlasmaFocusData(new Vector2(-5.0f, +10.0f), +3.0f),
                            MakePlasmaFocusData(new Vector2(-8.0f, +19.0f), +5.0f),
                        },
                    },
                }
            },

            { "Flame",
                new SWeaponDefinition()
                {
                    BasePrice = 800,
                    DisplayName = "Flame\nThrower",
                    Sound = "WeaponShootFlame",
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakeFlameData(0.12f, -2.0f),
                            MakeFlameData(0.12f, +2.0f),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakeFlameData(0.10f, -2.0f),
                            MakeFlameData(0.10f, +2.0f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeFlameData(0.11f, -4.0f),
                            MakeFlameData(0.11f, +0.0f),
                            MakeFlameData(0.11f, +4.0f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeFlameData(0.10f, -4.0f),
                            MakeFlameData(0.10f, +0.0f),
                            MakeFlameData(0.10f, +4.0f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeFlameData(0.08f, -4.0f),
                            MakeFlameData(0.08f, +0.0f),
                            MakeFlameData(0.08f, +4.0f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeFlameData(0.075f, -4.0f),
                            MakeFlameData(0.075f, -4.0f),
                            MakeBigFlameData(0.125f, 0.0f),
                        },
                        // level 7
                        new List<SWeaponData>() {
                            MakeFlameData(0.075f, -4.0f),
                            MakeFlameData(0.075f, -4.0f),
                            MakeBigFlameData(0.10f, 0.0f),
                        },
                        // level 8
                        new List<SWeaponData>() {
                            MakeBigFlameData(0.10f, -4.0f),
                            MakeBigFlameData(0.10f, -4.0f),
                            MakeBigFlameData(0.10f, 0.0f),
                        },
                    },
                }
            },

            { "FlameFocus",
                new SWeaponDefinition()
                {
                    BasePrice = 800,
                    DisplayName = "Flame\nThrower",
                    Sound = "WeaponShootFlame",
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakeFlameFocusData(0.05f, 0.0f),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakeFlameFocusData(0.04f, 0.0f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeFlameFocusData(0.03f, 0.0f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeFlameFocusData(0.04f, 0.0f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeFlameFocusData(0.03f, -90.0f),
                            MakeFlameFocusData(0.03f, +90.0f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeFlameFocusData(0.02f, -90.0f),
                            MakeFlameFocusData(0.02f, +90.0f),
                        },
                        // level 7
                        new List<SWeaponData>() {
                            MakeFlameFocusData(0.01f, -90.0f),
                            MakeFlameFocusData(0.01f, +90.0f),
                        },
                        // level 8
                        new List<SWeaponData>() {
                            MakeFlameFocusData(0.01f, -120.0f),
                            MakeFlameFocusData(0.01f, 0.0f),
                            MakeFlameFocusData(0.01f, +120.0f),
                        },
                    },
                }
            },

            { "Vulcan",
                new SWeaponDefinition()
                {
                    BasePrice = 800,
                    DisplayName = "Vulcan Cannon",
                    Sound = "WeaponShootMiniShot",
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakeVulcanData(0.15f, 0.0f),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakeVulcanData(0.14f, 0.0f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeVulcanData(0.13f, 0.0f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeVulcanData(0.1400f, 0.0f),
                            MakeVulcanData(0.1401f, 0.0f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeVulcanData(0.1300f, 0.0f),
                            MakeVulcanData(0.1301f, 0.0f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeVulcanData(0.1200f, 0.0f),
                            MakeVulcanData(0.1201f, 0.0f),
                        },
                        // level 7
                        new List<SWeaponData>() {
                            MakeVulcanData(0.1400f, 0.0f),
                            MakeVulcanData(0.1401f, 0.0f),
                            MakeVulcanData(0.1402f, 0.0f),
                        },
                        // level 8
                        new List<SWeaponData>() {
                            MakeVulcanData(0.1200f, 0.0f),
                            MakeVulcanData(0.1201f, 0.0f),
                            MakeVulcanData(0.1202f, 0.0f),
                        },
                    },
                }
            },

            { "Bomblet",
                new SWeaponDefinition()
                {
                    BasePrice = 900,
                    DisplayName = "Bomblet",
                    Sound = "WeaponShootSeekBomb",
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakeBombletData(1.00f),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakeBombletData(0.95f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeBombletData(0.90f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeBombletData(0.85f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeBombletData(0.75f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeBombletData(0.65f),
                        },
                        // level 7
                        new List<SWeaponData>() {
                            MakeBombletData(0.55f),
                        },
                        // level 8
                        new List<SWeaponData>() {
                            MakeBombletData(0.40f),
                        },
                    },
                }
            },


            { "Lightning",
                new SWeaponDefinition()
                {
                    BasePrice = 800,
                    DisplayName = "Lightning\nGun",
                    Sound = "WeaponShootLaser", // TODO: SFX
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakeLightningData(+0.0f, 2),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakeLightningData(-4.0f, 2),
                            MakeLightningData(+4.0f, 2),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeLightningData(-8.0f, 2),
                            MakeLightningData(+0.0f, 2),
                            MakeLightningData(+8.0f, 2),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeLightningData(-8.0f, 2),
                            MakeLightningData(+0.0f, 3),
                            MakeLightningData(+8.0f, 2),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeLightningData(-12.0f, 2),
                            MakeLightningData(-4.0f, 3),
                            MakeLightningData(+4.0f, 3),
                            MakeLightningData(+12.0f, 2),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeLightningData(-14.0f, 3),
                            MakeLightningData(-6.0f, 3),
                            MakeLightningData(-0.0f, 3),
                            MakeLightningData(+6.0f, 3),
                            MakeLightningData(+14.0f, 3),
                        },
                        // level 7
                        new List<SWeaponData>() {
                            MakeLightningData(-16.0f, 3),
                            MakeLightningData(-8.0f, 4),
                            MakeLightningData(-2.0f, 4),
                            MakeLightningData(+2.0f, 4),
                            MakeLightningData(+8.0f, 4),
                            MakeLightningData(+16.0f, 3),
                        },
                        // level 8
                        new List<SWeaponData>() {
                            MakeLightningData(-20.0f, 4),
                            MakeLightningData(-14.0f, 4),
                            MakeLightningData(-8.0f, 4),
                            MakeLightningData(-2.0f, 4),
                            MakeLightningData(+2.0f, 4),
                            MakeLightningData(+8.0f, 4),
                            MakeLightningData(+14.0f, 4),
                            MakeLightningData(+20.0f, 4),
                        },
                    },
                }
            },

            { "LightningFocus",
                new SWeaponDefinition()
                {
                    BasePrice = 800,
                    DisplayName = "Lightning\nGun",
                    Sound = "WeaponShootLaser", // TODO: SFX
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakeLightningFocusData(+0.0f, 2, 0.07f, 20.0f),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakeLightningFocusData(+0.0f, 2, 0.04f, 20.0f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeLightningFocusData(+0.0f, 2, 0.07f, 20.0f),
                            MakeLightningFocusData(+0.0f, 2, 0.07f, 35.0f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeLightningFocusData(+0.0f, 2, 0.04f, 20.0f),
                            MakeLightningFocusData(+0.0f, 2, 0.04f, 35.0f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeLightningFocusData(+0.0f, 2, 0.06f, 20.0f),
                            MakeLightningFocusData(+0.0f, 2, 0.06f, 35.0f),
                            MakeLightningFocusData(+0.0f, 2, 0.06f, 45.0f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeLightningFocusData(+0.0f, 2, 0.04f, 20.0f),
                            MakeLightningFocusData(+0.0f, 2, 0.04f, 35.0f),
                            MakeLightningFocusData(+0.0f, 2, 0.04f, 45.0f),
                        },
                        // level 7
                        new List<SWeaponData>() {
                            MakeLightningFocusData(+0.0f, 2, 0.06f, 20.0f),
                            MakeLightningFocusData(+0.0f, 2, 0.06f, 35.0f),
                            MakeLightningFocusData(+0.0f, 2, 0.06f, 45.0f),
                            MakeLightningFocusData(+0.0f, 2, 0.06f, 55.0f),
                        },
                        // level 8
                        new List<SWeaponData>() {
                            MakeLightningFocusData(+0.0f, 2, 0.04f, 20.0f),
                            MakeLightningFocusData(+0.0f, 2, 0.04f, 35.0f),
                            MakeLightningFocusData(+0.0f, 2, 0.04f, 45.0f),
                            MakeLightningFocusData(+0.0f, 2, 0.04f, 55.0f),
                        },
                    },
                }
            },

            { "Beam",
                new SWeaponDefinition()
                {
                    BasePrice = 2500,
                    DisplayName = "Beam\nWeapon",
                    // TODO: too long, doesnt stop quickly
                    //Sound = "WeaponShootBeam",
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakeBeamData(0.01f, 0.02f, 1.0f),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakeBeamData(0.02f, 0.025f, 2.0f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeBeamData(0.03f, 0.030f, 3.0f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeBeamData(0.04f, 0.035f, 4.0f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeBeamData(0.05f, 0.040f, 5.0f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeBeamData(0.06f, 0.045f, 6.0f),
                        },
                        // level 7
                        new List<SWeaponData>() {
                            MakeBeamData(0.07f, 0.050f, 7.0f),
                        },
                        // level 8
                        new List<SWeaponData>() {
                            MakeBeamData(0.08f, 0.055f, 8.0f),
                        },
                    },
                }
            },

            { "BeamFocus",
                new SWeaponDefinition()
                {
                    BasePrice = 2500,
                    DisplayName = "Beam\nWeapon",
                    // TODO: too long, doesnt stop quickly
                    //Sound = "WeaponShootBeam",
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakeBeamFocusData(0.05f, 0.05f, 170.0f),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakeBeamFocusData(0.75f, 0.07f, 200.0f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeBeamFocusData(0.10f, 0.09f, 230.0f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeBeamFocusData(0.125f, 0.11f, 260.0f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeBeamFocusData(0.15f, 0.13f, 290.0f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeBeamFocusData(0.175f, 0.15f, 320.0f),
                        },
                        // level 7
                        new List<SWeaponData>() {
                            MakeBeamFocusData(0.20f, 0.18f, 350.0f),
                        },
                        // level 8
                        new List<SWeaponData>() {
                            MakeBeamFocusData(0.225f, 0.20f, 380.0f),
                        },
                    },
                }
            },


            { "Missile", 
                new SWeaponDefinition() {
                    Sound = "WeaponShootMissile",
                    DisplayName = "Missile\nRack",
                    BasePrice = 1000,
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakeMissileData(new Vector2(+0.0f, +0.0f)),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakeMissileData(new Vector2(+0.0f, +10.0f)),
                            MakeMissileData(new Vector2(+0.0f, -10.0f)),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeMissileData(new Vector2(+0.0f, +16.0f)),
                            MakeMissileData(new Vector2(+0.0f, +0.0f)),
                            MakeMissileData(new Vector2(+0.0f, -16.0f)),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeMissileData(new Vector2(+0.0f, +18.0f)),
                            MakeMissileData(new Vector2(+12.0f, +6.0f)),
                            MakeMissileData(new Vector2(+12.0f, -6.0f)),
                            MakeMissileData(new Vector2(+0.0f, -18.0f)),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeMissileData(new Vector2(+0.0f, +22.0f)),
                            MakeMissileData(new Vector2(+8.0f, +12.0f)),
                            MakeMissileData(new Vector2(+14.0f, +0.0f)),
                            MakeMissileData(new Vector2(+8.0f, -12.0f)),
                            MakeMissileData(new Vector2(+0.0f, -22.0f)),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeMissileData(new Vector2(+0.0f, +22.0f)),
                            MakeMissileData(new Vector2(+8.0f, +13.0f)),
                            MakeMissileData(new Vector2(+14.0f, +6.0f)),
                            MakeMissileData(new Vector2(+14.0f, -6.0f)),
                            MakeMissileData(new Vector2(+8.0f, -13.0f)),
                            MakeMissileData(new Vector2(+0.0f, -22.0f)),
                        },
                    },
                }
            },

            { "SeekBomb", 
                new SWeaponDefinition() {
                    Sound = "WeaponShootSeekBomb",
                    DisplayName = "Seek\nBomb",
                    BasePrice = 500,
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakeSeekBombData(new Vector2(0.0f, 0.0f), 180.0f),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakeSeekBombData(new Vector2(0.0f, -10.0f), 195.0f),
                            MakeSeekBombData(new Vector2(0.0f, +10.0f), 165.0f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeSeekBombData(new Vector2(0.0f, -10.0f), 195.0f),
                            MakeSeekBombData(new Vector2(0.0f, +0.0f), 180.0f),
                            MakeSeekBombData(new Vector2(0.0f, +10.0f), 165.0f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeSeekBombData(new Vector2(0.0f, -10.0f), 195.0f),
                            MakeSeekBombData(new Vector2(0.0f, -5.0f), 185.0f),
                            MakeSeekBombData(new Vector2(0.0f, +5.0f), 175.0f),
                            MakeSeekBombData(new Vector2(0.0f, +10.0f), 165.0f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeSeekBombData(new Vector2(0.0f, -12.0f), 200.0f),
                            MakeSeekBombData(new Vector2(0.0f, -6.0f), 190.0f),
                            MakeSeekBombData(new Vector2(0.0f, +0.0f), 180.0f),
                            MakeSeekBombData(new Vector2(0.0f, +6.0f), 170.0f),
                            MakeSeekBombData(new Vector2(0.0f, +12.0f), 160.0f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeSeekBombData(new Vector2(0.0f, -14.0f), 205.0f),
                            MakeSeekBombData(new Vector2(4.0f, -8.0f), 195.0f),
                            MakeSeekBombData(new Vector2(10.0f, -3.0f), 185.0f),
                            MakeSeekBombData(new Vector2(10.0f, +3.0f), 175.0f),
                            MakeSeekBombData(new Vector2(4.0f, +8.0f), 165.0f),
                            MakeSeekBombData(new Vector2(0.0f, +14.0f), 155.0f),
                        },
                    },
                }
            },

            { "Boomerang", 
                new SWeaponDefinition() {
                    Sound = "WeaponShootSeekBomb",
                    DisplayName = "Boomerang",
                    BasePrice = 700,
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakeBoomerangData(new Vector2(0.0f, 0.0f), 0.0f, 0.4f),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakeBoomerangData(new Vector2(0.0f, 0.0f), 0.0f, 0.3f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeBoomerangData(new Vector2(0.0f, -10.0f), -3.0f, 0.3f),
                            MakeBoomerangData(new Vector2(0.0f, +10.0f), 3.0f, 0.3f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeBoomerangData(new Vector2(0.0f, -10.0f), -3.0f, 0.25f),
                            MakeBoomerangData(new Vector2(0.0f, +10.0f), 3.0f, 0.25f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeBoomerangData(new Vector2(0.0f, -10.0f), -3.0f, 0.25f),
                            MakeBoomerangData(new Vector2(0.0f, -10.0f), 0.0f, 0.25f),
                            MakeBoomerangData(new Vector2(0.0f, +10.0f), 3.0f, 0.25f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeBoomerangData(new Vector2(0.0f, -10.0f), -5.0f, 0.25f),
                            MakeBoomerangData(new Vector2(0.0f, -10.0f), -2.0f, 0.25f),
                            MakeBoomerangData(new Vector2(0.0f, -10.0f), 2.0f, 0.25f),
                            MakeBoomerangData(new Vector2(0.0f, +10.0f), 5.0f, 0.25f),
                        },
                    },
                }
            },

            { "DrunkMissile", 
                new SWeaponDefinition() {
                    Sound = "WeaponShootMissile",
                    DisplayName = "Drunk\nMissile",
                    BasePrice = 750,
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            MakeDrunkMissileData(new Vector2(0.0f, 0.0f), 0.4f),
                        },
                        // level 2
                        new List<SWeaponData>() {
                            MakeDrunkMissileData(new Vector2(0.0f, 0.0f), 0.3f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeDrunkMissileData(new Vector2(0.0f, +0.0f), 0.2f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeDrunkMissileData(new Vector2(0.0f, -2.0f), 0.2f),
                            MakeDrunkMissileData(new Vector2(0.0f, +2.0f), 0.2f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeDrunkMissileData(new Vector2(0.0f, -2.0f), 0.15f),
                            MakeDrunkMissileData(new Vector2(0.0f, +2.0f), 0.15f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeDrunkMissileData(new Vector2(0.0f, -4.0f), 0.15f),
                            MakeDrunkMissileData(new Vector2(4.0f, +0.0f), 0.15f),
                            MakeDrunkMissileData(new Vector2(0.0f, +4.0f), 0.15f),
                        },
                    },
                }
            },

            //
            // Sidekicks
            //
            { "MiniShot", 
                new SWeaponDefinition() {
                    Sound = "WeaponShootMiniShot",
                    DisplayName = "Mini\nShot",
                    BasePrice = 2500,
                    Data = new List<List<SWeaponData>>() {
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.075f,
                                Speed = 18.0f,
                                Damage = 0.025f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = MathHelper.ToRadians(0.0f),
                                Energy = 0.015f,
                                ChargeSpeed = 1.0f,
                                AutoDischarge = 1,
                            },
                        },
                    },
                }
            },
            { "ChargeShot", 
                new SWeaponDefinition() {
                    Sound = "WeaponShootLaser",
                    DisplayName = "Charge\nShot",
                    BasePrice = 12500,
                    Data = new List<List<SWeaponData>>() {
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.15f,
                                Speed = 15.0f,
                                Damage = 2.0f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = MathHelper.ToRadians(0.0f),
                                Energy = 0.015f,
                                ChargeSpeed = Time.ToSeconds(3),
                            },
                        },
                    },
                }
            },
            { "Blade", 
                new SWeaponDefinition() {
                    Sound = "WeaponShootMiniShot",
                    DisplayName = "Blade",
                    BasePrice = 10000,
                    Data = new List<List<SWeaponData>>() {
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.40f,
                                Speed = 0.0f,
                                Damage = 0.40f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = MathHelper.ToRadians(0.0f),
                                Energy = 4.0f,
                                ChargeSpeed = 1.0f,
                                AutoDischarge = 1,
                            },
                        },
                    },
                }
            },

            { "MissileVolley", 
                new SWeaponDefinition() {
                    Sound = "WeaponShootMissile",
                    DisplayName = "MissileVolley",
                    BasePrice = 12000,
                    Data = new List<List<SWeaponData>>() {
                        new List<SWeaponData>() {
                            MakeMissileVolleyData(new Vector2(-4.0f, -12.0f)),
                            MakeMissileVolleyData(new Vector2(0.0f, -8.0f)),
                            MakeMissileVolleyData(new Vector2(8.0f, -4.0f)),
                            MakeMissileVolleyData(new Vector2(12.0f, -1.0f)),
                            MakeMissileVolleyData(new Vector2(12.0f, 1.0f)),
                            MakeMissileVolleyData(new Vector2(8.0f, 4.0f)),
                            MakeMissileVolleyData(new Vector2(0.0f, 8.0f)),
                            MakeMissileVolleyData(new Vector2(-4.0f, 12.0f)),
                        },
                    },
                }
            },

            { "MissileLauncher", 
                new SWeaponDefinition() {
                    Sound = "WeaponShootMissile",
                    DisplayName = "MissileLauncher",
                    BasePrice = 6500,
                    Data = new List<List<SWeaponData>>() {
                        new List<SWeaponData>() {
                            MakeMissileLauncherData(new Vector2(-8.0f, -0.0f)),
                        },
                    },
                }
            },

            { "LaserShot", 
                new SWeaponDefinition() {
                    Sound = "WeaponShootLaser",
                    DisplayName = "LaserShot",
                    BasePrice = 3000,
                    Data = new List<List<SWeaponData>>() {
                        new List<SWeaponData>() {
                            MakeLaserShotData(0.0f),
                        },
                    },
                }
            },
        };
    }
}
