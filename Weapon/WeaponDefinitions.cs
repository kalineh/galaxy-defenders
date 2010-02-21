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
        public struct WeaponData
        {
            public float ReloadTime { get; set; }
            public float Speed { get; set; }
            public float Damage { get; set; }
            public float KickbackForce { get; set; }
            public Vector2 Offset { get; set; }
            public float Rotation { get; set; }
        }

        public static List<CWeapon> GeneratePrimaryWeapon(CEntity owner, string typename, int level)
        {
            return GenerateWeapon(owner, typename, level, PrimaryWeaponDefinitions);
        }

        public static List<CWeapon> GenerateSecondaryWeapon(CEntity owner, string typename, int level)
        {
            return GenerateWeapon(owner, typename, level, SecondaryWeaponDefinitions);
        }

        public static List<CWeapon> GenerateWeapon(CEntity owner, string typename, int level, Dictionary<string, SWeaponDefinition> definitions)
        {
            if (!definitions.ContainsKey(typename))
            {
                return new List<CWeapon>();
            }

            List<WeaponData> weapon_data = definitions[typename].Data[level];
            List<CWeapon> weapons = new List<CWeapon>();

            foreach (WeaponData data in weapon_data)
            {
                Type type = Type.GetType("Galaxy.CWeapon" + typename);
                CWeapon weapon = Activator.CreateInstance(type, new object[] { owner }) as CWeapon;
                weapon.ApplyWeaponData(data);
                weapons.Add(weapon);
            }

            return weapons;
        }

        public static bool CanUpgrade(string typename, int level)
        {
            if (PrimaryWeaponDefinitions.ContainsKey(typename))
                return PrimaryWeaponDefinitions[typename].Data.Count > level + 1;
            if (SecondaryWeaponDefinitions.ContainsKey(typename))
                return SecondaryWeaponDefinitions[typename].Data.Count > level + 1;
            return false;
        }

        public static bool CanDowngrade(string typename, int level)
        {
            return level > 0;
        }

        public static string GetNextWeaponInCycle(string current)
        {
            // TODO: not this badness
            Dictionary<string, SWeaponDefinition> definitions =
                PrimaryWeaponDefinitions.ContainsKey(current) ? PrimaryWeaponDefinitions : SecondaryWeaponDefinitions;

            // TODO: can we do this cleaner?
            bool is_next = false;
            foreach (KeyValuePair<string, SWeaponDefinition> kv in definitions)
            {
                if (kv.Key == current)
                {
                    is_next = true;
                    continue;
                }

                if (is_next)
                {
                    return kv.Key;
                }
            }
            
            // TODO: find cleaner way to do this too!
            foreach (KeyValuePair<string, SWeaponDefinition> kv in definitions)
            {
                return kv.Key;
            }

            return current;
        }

        public static int GetPriceForLevel(string typename, int level)
        {
            // TODO: not this badness
            Dictionary<string, SWeaponDefinition> definitions =
                PrimaryWeaponDefinitions.ContainsKey(typename) ? PrimaryWeaponDefinitions : SecondaryWeaponDefinitions;

            if (typename == "")
                return 0;

            // TODO: where can we put a per-weapon price? (not per-level)
            int calculable_level = level;
            int base_ = definitions[typename].BasePrice;
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

        public struct SWeaponDefinition
        {
            public int BasePrice;
            public string Sound;
            public List<List<WeaponData>> Data;
        }


        public static Dictionary<string, SWeaponDefinition> PrimaryWeaponDefinitions = new Dictionary<string, SWeaponDefinition>()
        {
            { "FrontLaser", new SWeaponDefinition()
                {
                    BasePrice = 750,
                    Sound = "SE/LaserShoot",
                    Data = new List<List<WeaponData>>() {
                        // level 1
                        new List<WeaponData>() {
                            new WeaponData() {
                                ReloadTime = 0.2f,
                                Speed = 15.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = 0.0f,
                            },
                        },
                        // level 2
                        new List<WeaponData>() {
                            new WeaponData() {
                                ReloadTime = 0.2f,
                                Speed = 15.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = 0.0f,
                            },
                            new WeaponData() {
                                ReloadTime = 0.2f,
                                Speed = 15.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = 0.0f,
                            },
                        },
                        // level 3
                        new List<WeaponData>() {
                            new WeaponData() {
                                ReloadTime = 0.2f,
                                Speed = 15.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -16.0f),
                                Rotation = 0.0f,
                            },
                            new WeaponData() {
                                ReloadTime = 0.2f,
                                Speed = 15.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -8.0f),
                                Rotation = 0.0f,
                            },
                            new WeaponData() {
                                ReloadTime = 0.2f,
                                Speed = 15.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 8.0f),
                                Rotation = 0.0f,
                            },
                            new WeaponData() {
                                ReloadTime = 0.2f,
                                Speed = 15.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 16.0f),
                                Rotation = 0.0f,
                            },
                        },
                    },
                }
            },

            { "SpreadLaser", new SWeaponDefinition()
                {
                    BasePrice = 750,
                    Sound = "SE/LaserShoot",
                    Data = new List<List<WeaponData>>() {
                        // level 1
                        new List<WeaponData>() {
                            new WeaponData() {
                                ReloadTime = 0.2f,
                                Speed = 15.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = 0.0f,
                            },
                        },
                        // level 2
                        new List<WeaponData>() {
                            new WeaponData() {
                                ReloadTime = 0.2f,
                                Speed = 15.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-3.0f),
                            },
                            new WeaponData() {
                                ReloadTime = 0.2f,
                                Speed = 15.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(3.0f),
                            },
                        },
                        // level 3
                        new List<WeaponData>() {
                            new WeaponData() {
                                ReloadTime = 0.2f,
                                Speed = 15.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-7.0f),
                            },
                            new WeaponData() {
                                ReloadTime = 0.2f,
                                Speed = 15.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(-3.0f),
                            },
                            new WeaponData() {
                                ReloadTime = 0.2f,
                                Speed = 15.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(3.0f),
                            },
                            new WeaponData() {
                                ReloadTime = 0.2f,
                                Speed = 15.0f,
                                Damage = 1.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 0.0f),
                                Rotation = MathHelper.ToRadians(7.0f),
                            },
                        },
                    },
                }
            },
        };

        public static Dictionary<string, SWeaponDefinition> SecondaryWeaponDefinitions = new Dictionary<string, SWeaponDefinition>()
        {
            { "Missile", 
                new SWeaponDefinition() {
                    BasePrice = 1000,
                    Sound = null,
                    Data = new List<List<WeaponData>>() {
                        // level 1
                        new List<WeaponData>() {
                            new WeaponData() {
                                ReloadTime = 1.0f,
                                Speed = 11.0f,
                                Damage = 2.0f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = MathHelper.ToRadians(180.0f),
                            },
                        },
                        // level 2
                        new List<WeaponData>() {
                            new WeaponData() {
                                ReloadTime = 1.0f,
                                Speed = 11.0f,
                                Damage = 2.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = MathHelper.ToRadians(195.0f),
                            },
                            new WeaponData() {
                                ReloadTime = 1.0f,
                                Speed = 11.0f,
                                Damage = 2.0f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = MathHelper.ToRadians(165.0f),
                            },
                        },
                    },
                }
            },

            { "SeekBomb", 
                new SWeaponDefinition() {
                    BasePrice = 1750,
                    Sound = null,
                    Data = new List<List<WeaponData>>() {
                        // level 1
                        new List<WeaponData>() {
                            new WeaponData() {
                                ReloadTime = 1.0f,
                                Speed = 5.0f,
                                Damage = 0.5f,
                                KickbackForce = 0.0f,
                                Offset = Vector2.Zero,
                                Rotation = MathHelper.ToRadians(0.0f),
                            },
                        },
                        // level 2
                        new List<WeaponData>() {
                            new WeaponData() {
                                ReloadTime = 1.0f,
                                Speed = 5.0f,
                                Damage = 0.5f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, -10.0f),
                                Rotation = MathHelper.ToRadians(-15.0f),
                            },
                            new WeaponData() {
                                ReloadTime = 1.0f,
                                Speed = 5.0f,
                                Damage = 0.5f,
                                KickbackForce = 0.0f,
                                Offset = new Vector2(0.0f, 10.0f),
                                Rotation = MathHelper.ToRadians(15.0f),
                            },
                        },
                    },
                }
            },
        };
    }
}
