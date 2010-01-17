//
// WeaponDefinitions.cs
//

using System;
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

        public static List<CWeapon> GenerateWeapon(CEntity owner, Type type, int level)
        {
            List<WeaponData> weapon_data = WeaponDefinitions[type][level];
            List<CWeapon> weapons = new List<CWeapon>();

            foreach (WeaponData data in weapon_data)
            {
                CWeapon weapon = Activator.CreateInstance(type, new object[] { owner }) as CWeapon;
                weapon.ApplyWeaponData(data);
                weapons.Add(weapon);
            }

            return weapons;
        }

        public static bool CanUpgrade(Type type, int level)
        {
            return WeaponDefinitions[type].Count > level + 1;
        }

        public static Dictionary<Type, List<List<WeaponData>>> WeaponDefinitions = new Dictionary<Type, List<List<WeaponData>>>()
        {
            { typeof(CWeaponLaser), new List<List<WeaponData>>() {
                    // level 1
                    new List<WeaponData>() {
                        new WeaponData() {
                            ReloadTime = 0.2f,
                            Speed = 8.0f,
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
                            Speed = 8.0f,
                            Damage = 1.0f,
                            KickbackForce = 0.0f,
                            Offset = new Vector2(0.0f, -10.0f),
                            Rotation = 0.0f,
                        },
                        new WeaponData() {
                            ReloadTime = 0.2f,
                            Speed = 8.0f,
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
                            Speed = 8.0f,
                            Damage = 1.0f,
                            KickbackForce = 0.0f,
                            Offset = new Vector2(0.0f, -12.0f),
                            Rotation = MathHelper.ToRadians(-15.0f),
                        },
                        new WeaponData() {
                            ReloadTime = 0.2f,
                            Speed = 8.0f,
                            Damage = 1.0f,
                            KickbackForce = 0.0f,
                            Offset = new Vector2(0.0f, -8.0f),
                            Rotation = 0.0f,
                        },
                        new WeaponData() {
                            ReloadTime = 0.2f,
                            Speed = 8.0f,
                            Damage = 1.0f,
                            KickbackForce = 0.0f,
                            Offset = new Vector2(0.0f, 8.0f),
                            Rotation = 0.0f,
                        },
                        new WeaponData() {
                            ReloadTime = 0.2f,
                            Speed = 8.0f,
                            Damage = 1.0f,
                            KickbackForce = 0.0f,
                            Offset = new Vector2(0.0f, 12.0f),
                            Rotation = MathHelper.ToRadians(15.0f),
                        },
                    },
                }
            },

            { typeof(CWeaponMissile), new List<List<WeaponData>>() {
                    // level 1
                    new List<WeaponData>() {
                        new WeaponData() {
                            ReloadTime = 3.0f,
                            Speed = 8.0f,
                            Damage = 6.0f,
                            KickbackForce = 0.0f,
                            Offset = Vector2.Zero,
                            Rotation = 0.0f,
                        },
                    },
                    // level 2
                    new List<WeaponData>() {
                        new WeaponData() {
                            ReloadTime = 3.0f,
                            Speed = 8.0f,
                            Damage = 6.0f,
                            KickbackForce = 0.0f,
                            Offset = new Vector2(0.0f, -10.0f),
                            Rotation = 0.0f,
                        },
                        new WeaponData() {
                            ReloadTime = 3.0f,
                            Speed = 8.0f,
                            Damage = 6.0f,
                            KickbackForce = 0.0f,
                            Offset = new Vector2(0.0f, 10.0f),
                            Rotation = 0.0f,
                        },
                    },
                }
            },
        };
    }
}
