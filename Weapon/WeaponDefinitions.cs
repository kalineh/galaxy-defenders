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
            int total = base_ + base_ * calculable_level * calculable_level;
            int rounded = total + total % 50;
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

        public static SWeaponData MakeBigFrontLaserData(float offset)
        {
            return new SWeaponData()
            {
                ReloadTime = 0.16f,
                Speed = 17.0f,
                Damage = 0.3f,
                KickbackForce = 0.0f,
                Offset = Vector2.UnitY * offset,
                Rotation = 0.0f,
                Energy = 0.15f,
            };
        }

        public static SWeaponData MakeFlameData(float reload, float offset)
        {
            return new SWeaponData()
            {
                ReloadTime = reload,
                Speed = 15.0f,
                Damage = 0.25f,
                KickbackForce = 0.0f,
                Offset = Vector2.UnitY * offset,
                Rotation = 0.0f,
                Energy = 0.15f,
                CustomData = new FlameCustomData() { Lifetime = 20, Friction = 0.99f, SprayAngle = 0.1f },
            };
        }

        public static SWeaponData MakeBigFlameData(float reload, float offset)
        {
            return new SWeaponData()
            {
                ReloadTime = reload,
                Speed = 15.0f,
                Damage = 0.65f,
                KickbackForce = 0.0f,
                Offset = Vector2.UnitY * offset,
                Rotation = 0.0f,
                Energy = 0.15f,
                CustomData = new FlameCustomData() { Lifetime = 20, Friction = 0.99f, SprayAngle = 0.05f },
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
                            MakeFrontLaserData(-12.0f),
                            MakeFrontLaserData(-4.0f),
                            MakeFrontLaserData(+4.0f),
                            MakeFrontLaserData(+12.0f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeFrontLaserData(-18.0f),
                            MakeFrontLaserData(-10.0f),
                            MakeBigFrontLaserData(+0.0f),
                            MakeFrontLaserData(+10.0f),
                            MakeFrontLaserData(+18.0f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeFrontLaserData(-20.0f),
                            MakeFrontLaserData(-13.0f),
                            MakeBigFrontLaserData(-5.0f),
                            MakeBigFrontLaserData(+5.0f),
                            MakeFrontLaserData(+13.0f),
                            MakeFrontLaserData(+20.0f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeFrontLaserData(-23.0f),
                            MakeBigFrontLaserData(-15.0f),
                            MakeBigFrontLaserData(-5.0f),
                            MakeBigFrontLaserData(+5.0f),
                            MakeBigFrontLaserData(+15.0f),
                            MakeFrontLaserData(+23.0f),
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

            { "SpreadLaser",
                new SWeaponDefinition()
                {
                    BasePrice = 700,
                    DisplayName = "Spread\nLaser",
                    Sound = "WeaponShootLaser",
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                        },
                        // level 2
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-2.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(2.0f),
                                Energy = 0.1f,
                            },
                        },
                        // level 3
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-6.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-2.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(2.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(6.0f),
                                Energy = 0.1f,
                            },
                        },
                        // level 4
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-8.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-4.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(0.0f),
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(4.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(8.0f),
                                Energy = 0.1f,
                            },
                        },
                        // level 5
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-10.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-6.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-2.0f),
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(2.0f),
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(6.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(10.0f),
                                Energy = 0.1f,
                            },
                        },
                        // level 6
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-12.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-7.0f),
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-2.0f),
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(2.0f),
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(7.0f),
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(12.0f),
                                Energy = 0.1f,
                            },
                        },
                        // level 7
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-13.0f),
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-7.0f),
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-2.0f),
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(2.0f),
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(7.0f),
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.18f,
                                Speed = 15.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(13.0f),
                                Energy = 0.15f,
                            },
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
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 0.25f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = 0.0f,
                                Energy = 0.2f,
                            },
                        },
                        // level 2
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 0.25f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = MathHelper.ToRadians(-2.0f),
                                Energy = 0.2f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 0.25f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = MathHelper.ToRadians(2.0f),
                                Energy = 0.2f,
                            },
                        },
                        // level 3
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 0.25f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(-1.0f, -10.0f),
                                Rotation = MathHelper.ToRadians(-2.0f),
                                Energy = 0.2f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 0.25f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = 0.0f,
                                Energy = 0.2f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 0.25f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(-1.0f, 10.0f),
                                Rotation = MathHelper.ToRadians(2.0f),
                                Energy = 0.2f,
                            },
                        },
                        // level 4
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 0.25f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(-2.0f, -13.0f),
                                Rotation = MathHelper.ToRadians(-2.0f),
                                Energy = 0.2f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = 0.0f,
                                Energy = 0.7f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 0.25f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(-2.0f, 13.0f),
                                Rotation = MathHelper.ToRadians(2.0f),
                                Energy = 0.2f,
                            },
                        },
                        // level 5
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 0.25f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(-5.0f, -19.0f),
                                Rotation = MathHelper.ToRadians(-2.0f),
                                Energy = 0.2f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = 0.0f,
                                Energy = 0.7f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = 0.0f,
                                Energy = 0.7f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 0.25f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(-5.0f, 19.0f),
                                Rotation = MathHelper.ToRadians(2.0f),
                                Energy = 0.2f,
                            },
                        },
                        // level 6
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(-3.0f, -22.0f),
                                Rotation = MathHelper.ToRadians(-1.0f),
                                Energy = 0.7f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = 0.0f,
                                Energy = 0.7f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = 0.0f,
                                Energy = 0.7f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(-3.0f, 22.0f),
                                Rotation = MathHelper.ToRadians(1.0f),
                                Energy = 0.7f,
                            },
                        },
                        // level 7
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 0.25f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(-6.0f, -32.0f),
                                Rotation = MathHelper.ToRadians(-2.0f),
                                Energy = 0.2f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(-2.0f, -22.0f),
                                Rotation = MathHelper.ToRadians(-1.0f),
                                Energy = 0.7f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = 0.0f,
                                Energy = 0.7f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = 0.0f,
                                Energy = 0.7f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(-2.0f, 22.0f),
                                Rotation = MathHelper.ToRadians(1.0f),
                                Energy = 0.7f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 12.0f,
                                Damage = 0.25f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(-6.0f, 32.0f),
                                Rotation = MathHelper.ToRadians(2.0f),
                                Energy = 0.2f,
                            },
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
                            MakeLightningData(-12.0f, 3),
                            MakeLightningData(-4.0f, 4),
                            MakeLightningData(+4.0f, 4),
                            MakeLightningData(+12.0f, 3),
                        },
                        // level 7
                        new List<SWeaponData>() {
                            MakeLightningData(-16.0f, 3),
                            MakeLightningData(-8.0f, 4),
                            MakeLightningData(+0.0f, 4),
                            MakeLightningData(+8.0f, 4),
                            MakeLightningData(+16.0f, 3),
                        },
                        // level 8
                        new List<SWeaponData>() {
                            MakeLightningData(-20.0f, 4),
                            MakeLightningData(-12.0f, 4),
                            MakeLightningData(-4.0f, 4),
                            MakeLightningData(+4.0f, 4),
                            MakeLightningData(+12.0f, 4),
                            MakeLightningData(+20.0f, 4),
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
                            MakeBeamData(0.02f, 0.03f, 2.0f),
                        },
                        // level 3
                        new List<SWeaponData>() {
                            MakeBeamData(0.03f, 0.05f, 3.0f),
                        },
                        // level 4
                        new List<SWeaponData>() {
                            MakeBeamData(0.05f, 0.07f, 4.0f),
                        },
                        // level 5
                        new List<SWeaponData>() {
                            MakeBeamData(0.07f, 0.09f, 5.0f),
                        },
                        // level 6
                        new List<SWeaponData>() {
                            MakeBeamData(0.09f, 0.11f, 6.0f),
                        },
                        // level 7
                        new List<SWeaponData>() {
                            MakeBeamData(0.11f, 0.14f, 7.0f),
                        },
                        // level 8
                        new List<SWeaponData>() {
                            MakeBeamData(0.15f, 0.18f, 8.0f),
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
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                        },
                        // level 2
                        new List<SWeaponData>() {
                            new SWeaponData() { ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                        },
                        // level 3
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 16.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(12.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -16.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                        },
                        // level 4
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 18.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(12.0f, 6.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(12.0f, -6.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -18.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                        },
                        // level 5
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 22.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(8.0f, 12.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(14.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(8.0f, -12.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -22.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
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
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                        },
                        // level 2
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = MathHelper.ToRadians(195.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = MathHelper.ToRadians(165.0f),
                            },
                        },
                        // level 3
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = MathHelper.ToRadians(195.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = MathHelper.ToRadians(165.0f),
                            },
                        },
                        // level 4
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = MathHelper.ToRadians(195.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -5.0f),
                                Rotation = MathHelper.ToRadians(185.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 5.0f),
                                Rotation = MathHelper.ToRadians(175.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = MathHelper.ToRadians(165.0f),
                            },
                        },
                        // level 5
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -14.0f),
                                Rotation = MathHelper.ToRadians(205.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -8.0f),
                                Rotation = MathHelper.ToRadians(195.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -3.0f),
                                Rotation = MathHelper.ToRadians(185.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 3.0f),
                                Rotation = MathHelper.ToRadians(175.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 8.0f),
                                Rotation = MathHelper.ToRadians(165.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 14.0f),
                                Rotation = MathHelper.ToRadians(155.0f),
                            },
                        },
                        // level 6
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -18.0f),
                                Rotation = MathHelper.ToRadians(215.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -14.0f),
                                Rotation = MathHelper.ToRadians(205.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -8.0f),
                                Rotation = MathHelper.ToRadians(195.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -3.0f),
                                Rotation = MathHelper.ToRadians(185.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 3.0f),
                                Rotation = MathHelper.ToRadians(175.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 8.0f),
                                Rotation = MathHelper.ToRadians(165.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 14.0f),
                                Rotation = MathHelper.ToRadians(155.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 18.0f),
                                Rotation = MathHelper.ToRadians(145.0f),
                            },
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
                    BasePrice = 5000,
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
                    BasePrice = 7500,
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
        };
    }
}
