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
                CWeapon weapon = Activator.CreateInstance(type, new object[] { owner }) as CWeapon;
                weapon.ApplyWeaponData(data);
                weapons.Add(weapon);
            }

            return weapons;
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

        public static List<string> PrimaryWeaponTypes = new List<string>
        {
            "FrontLaser",
            "SpreadLaser",
            "Plasma",
        };

        public static List<string> SecondaryWeaponTypes = new List<string>
        {
            "",
            "SeekBomb",
            "Missile",
        };

        public static List<string> SidekickWeaponTypes = new List<string>
        {
            "",
            "MiniShot",
        };

        public static string GetNextWeaponInCycle(string current, List<string> types)
        {
            int index = types.FindIndex(s => s == current);
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
        }

        public struct SWeaponDefinition
        {
            public int BasePrice;
            public string Sound;
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

        public static Dictionary<string, SWeaponDefinition> Items = new Dictionary<string, SWeaponDefinition>()
        {
            { "FrontLaser",
                new SWeaponDefinition()
                {
                    BasePrice = 750,
                    Sound = "SE/LaserShoot",
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                        },
                        // level 2
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                        },
                        // level 3
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -12.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -4.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 4.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 12.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                        },
                        // level 4
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -20.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -12.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -4.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 4.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 12.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 20.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                        },
                        // level 5
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -28.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -20.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -12.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -4.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 4.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 12.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 20.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 28.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                        },
                    },
                }
            },

            { "SpreadLaser",
                new SWeaponDefinition()
                {
                    BasePrice = 750,
                    Sound = "SE/LaserShoot",
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                        },
                        // level 2
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-1.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(1.0f),
                                Energy = 0.1f,
                            },
                        },
                        // level 3
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-3.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-1.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(1.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(3.0f),
                                Energy = 0.1f,
                            },
                        },
                        // level 4
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-5.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-3.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-1.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(1.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(3.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(5.0f),
                                Energy = 0.1f,
                            },
                        },
                        // level 5
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-7.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-5.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-3.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-1.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(1.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(3.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(5.0f),
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.1f,
                                Speed = 20.0f,
                                Damage = 0.2f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(7.0f),
                                Energy = 0.1f,
                            },
                        },
                    },
                }
            },

            { "Plasma", 
                new SWeaponDefinition() {
                    BasePrice = 1750,
                    Sound = null,
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 16.0f,
                                Damage = 0.75f,
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
                                Speed = 16.0f,
                                Damage = 0.75f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = MathHelper.ToRadians(-2.0f),
                                Energy = 0.2f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 16.0f,
                                Damage = 0.75f,
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
                                Speed = 16.0f,
                                Damage = 0.75f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = 0.0f,
                                Energy = 0.2f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 16.0f,
                                Damage = 0.75f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = MathHelper.ToRadians(-2.0f),
                                Energy = 0.2f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 16.0f,
                                Damage = 0.75f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = MathHelper.ToRadians(2.0f),
                                Energy = 0.2f,
                            },
                        },
                        // level 4
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 16.0f,
                                Damage = 1.5f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = 0.0f,
                                Energy = 0.5f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 16.0f,
                                Damage = 0.75f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = MathHelper.ToRadians(-2.0f),
                                Energy = 0.2f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.25f,
                                Speed = 16.0f,
                                Damage = 0.75f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = MathHelper.ToRadians(2.0f),
                                Energy = 0.2f,
                            },
                        },
                    },
                }
            },

            { "Missile", 
                new SWeaponDefinition() {
                    BasePrice = 1000,
                    Sound = null,
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 2.0f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                        },
                        // level 2
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 2.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 2.0f,
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
                                Damage = 2.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 16.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 2.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.5f,
                                Speed = 17.0f,
                                Damage = 2.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -16.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                        },
                    },
                }
            },

            { "SeekBomb", 
                new SWeaponDefinition() {
                    BasePrice = 1750,
                    Sound = null,
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.8f,
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
                                Damage = 0.8f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = MathHelper.ToRadians(195.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.8f,
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
                                Damage = 0.8f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = MathHelper.ToRadians(195.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.8f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                            new SWeaponData() {
                                ReloadTime = 0.7f,
                                Speed = 14.0f,
                                Damage = 0.8f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = MathHelper.ToRadians(165.0f),
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
                    BasePrice = 1500,
                    Sound = null,
                    Data = new List<List<SWeaponData>>() {
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.075f,
                                Speed = 18.0f,
                                Damage = 0.01f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = MathHelper.ToRadians(0.0f),
                            },
                        },
                    },
                }
            },
        };
    }
}
