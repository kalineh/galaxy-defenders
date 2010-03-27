//
// ShipDefinitions.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Galaxy
{
    public class CShipFactory
    {
        public static CShip GenerateShip(CWorld world, SProfile profile)
        {
            CChassisPart chassis = ChassisDefinitions.GetPart(profile.ChassisType);
            CGeneratorPart generator = GeneratorDefinitions.GetPart(profile.GeneratorType);
            CShieldPart shield = ShieldDefinitions.GetPart(profile.ShieldType);
            CWeaponPart primary = WeaponDefinitions.GetPart(profile.WeaponPrimaryType, profile.WeaponPrimaryLevel);
            CWeaponPart secondary = WeaponDefinitions.GetPart(profile.WeaponSecondaryType, profile.WeaponSecondaryLevel);
            CWeaponPart sidekick_left = WeaponDefinitions.GetPart(profile.WeaponSidekickLeftType, profile.WeaponSidekickLeftLevel);
            CWeaponPart sidekick_right = WeaponDefinitions.GetPart(profile.WeaponSidekickRightType, profile.WeaponSidekickRightLevel);
            CShip ship = new CShip(world, chassis, generator, shield, primary, secondary, sidekick_left, sidekick_right);
            return ship;
        }
    }

    public class ChassisDefinitions
    {
        public static string GetNextDefinition(string current)
        {
            int index = Indexed.FindIndex(s => s == current);
            int next = index + 1;
            return Indexed[next % Indexed.Count];
        }

        public static CChassisPart GetPart(string current)
        {
            return Items.ContainsKey(current) ? Items[current] : Items["BasicShip"];
        }

        public static List<string> Indexed = new List<string>
        {
            "BasicShip",
            "StrongShip",
        };

        public static Dictionary<string, CChassisPart> Items = new Dictionary<string, CChassisPart>()
        {
            {
                "BasicShip",
                new CChassisPart() {
                    Texture = "Textures/Player/BasicShip",
                    VisualScale = 0.25f,
                    Speed = 2.25f,
                    Friction = 0.8f,
                    Armor = 5.0f,
                }
            },

            {
                "StrongShip",
                new CChassisPart() {
                    Price = 2500,
                    Texture = "Textures/Player/StrongShip",
                    VisualScale = 0.25f,
                    Speed = 1.9f,
                    Friction = 0.85f,
                    Armor = 15.0f,
                }
            }
        };
    }

    public class GeneratorDefinitions
    {
        public static string GetNextDefinition(string current)
        {
            int index = Indexed.FindIndex(s => s == current);
            int next = index + 1;
            return Indexed[next % Indexed.Count];
        }

        public static CGeneratorPart GetPart(string current)
        {
            return Items.ContainsKey(current) ? Items[current] : Items["BasicGenerator"];
        }

        public static List<string> Indexed = new List<string>
        {
            "BasicGenerator",
            "UltraGenerator",
        };

        public static Dictionary<string, CGeneratorPart> Items = new Dictionary<string, CGeneratorPart>()
        {
            {
                "BasicGenerator",
                new CGeneratorPart() {
                    Energy = 10.0f,
                    Regen = 0.04f,
                }
            },
            {
                "UltraGenerator",
                new CGeneratorPart() {
                    Price = 5000,
                    Energy = 20.0f,
                    Regen = 0.15f,
                }
            }
        };
    }

    public class ShieldDefinitions
    {
        public static string GetNextDefinition(string current)
        {
            int index = Indexed.FindIndex(s => s == current);
            int next = index + 1;
            return Indexed[next % Indexed.Count];
        }

        public static CShieldPart GetPart(string current)
        {
            return Items.ContainsKey(current) ? Items[current] : Items["BasicShield"];
        }

        public static List<string> Indexed = new List<string>
        {
            "BasicShield",
            "UltraShield",
        };

        public static Dictionary<string, CShieldPart> Items = new Dictionary<string, CShieldPart>()
        {
            {
                "BasicShield",
                new CShieldPart() {
                    Shield = 5.0f,
                    Regen = 0.01f,
                }
            },
            {
                "UltraShield",
                new CShieldPart() {
                    Price = 5000,
                    Shield = 15.0f,
                    Regen = 0.15f,
                }
            }
        };
    }
}
