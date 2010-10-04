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

        public static Dictionary<string, SWeaponDefinition> Items = new Dictionary<string, SWeaponDefinition>()
        {
            { "FrontLaser",
                new SWeaponDefinition()
                {
                    BasePrice = 750,
                    DisplayName = "Front\nLaser",
                    Sound = "WeaponShootLaser",
                    Data = new List<List<SWeaponData>>() {
                        // level 1
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
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
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                        },
                        // level 3
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -12.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -4.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 4.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 12.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                        },
                        // level 4
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -18.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = 0.0f,
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 18.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                        },
                        // level 5
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -20.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -13.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -5.0f),
                                Rotation = 0.0f,
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 5.0f),
                                Rotation = 0.0f,
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 13.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 20.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                        },
                        // level 6
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -23.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -15.0f),
                                Rotation = 0.0f,
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -5.0f),
                                Rotation = 0.0f,
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 5.0f),
                                Rotation = 0.0f,
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 15.0f),
                                Rotation = 0.0f,
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.1f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 23.0f),
                                Rotation = 0.0f,
                                Energy = 0.1f,
                            },
                        },
                        // level 7
                        new List<SWeaponData>() {
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -25.0f),
                                Rotation = 0.0f,
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -15.0f),
                                Rotation = 0.0f,
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -5.0f),
                                Rotation = 0.0f,
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 5.0f),
                                Rotation = 0.0f,
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 15.0f),
                                Rotation = 0.0f,
                                Energy = 0.15f,
                            },
                            new SWeaponData() {
                                ReloadTime = 0.14f,
                                Speed = 17.0f,
                                Damage = 0.3f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 25.0f),
                                Rotation = 0.0f,
                                Energy = 0.15f,
                            },
                        },
                    },
                }
            },

            { "SpreadLaser",
                new SWeaponDefinition()
                {
                    BasePrice = 750,
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
                            },
                        },
                    },
                }
            },
        };
    }
}
