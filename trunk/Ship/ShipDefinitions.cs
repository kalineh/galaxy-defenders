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
        public static CShip GenerateShip(CWorld world, SProfilePilotState profile, PlayerIndex index)
        {
            CPilot pilot = CPilot.MakePilot(profile);
            CChassisPart chassis = ChassisDefinitions.GetPart(profile.ChassisType);
            CGeneratorPart generator = GeneratorDefinitions.GetPart(profile.GeneratorType);
            CShieldPart shield = ShieldDefinitions.GetPart(profile.ShieldType);
            CWeaponPart primary = WeaponDefinitions.GetPart(profile.WeaponPrimaryType, profile.WeaponPrimaryLevel);
            CWeaponPart secondary = WeaponDefinitions.GetPart(profile.WeaponSecondaryType, profile.WeaponSecondaryLevel);
            CWeaponPart sidekick_left = WeaponDefinitions.GetPart(profile.WeaponSidekickLeftType, profile.WeaponSidekickLeftLevel);
            CWeaponPart sidekick_right = WeaponDefinitions.GetPart(profile.WeaponSidekickRightType, profile.WeaponSidekickRightLevel);
            CShip ship = new CShip();
            ship.Initialize(world, index, pilot, chassis, generator, shield, primary, secondary, sidekick_left, sidekick_right);
            return ship;
        }
    }

    public class ChassisDefinitions
    {
        public static CChassisPart GetPart(string current)
        {
            return Items.ContainsKey(current) ? Items[current] : Items["BasicShip"];
        }

        public static Dictionary<string, CChassisPart> Items = new Dictionary<string, CChassisPart>()
        {
            {
                "BasicShip",
                new CChassisPart() {
                    Texture = "Textures/Player/BasicShip",
                    VisualScale = 0.25f,
                    Speed = 2.0f,
                    Friction = 0.8f,
                    Armor = 20.0f,
                }
            },
            {
                "FighterShip",
                new CChassisPart() {
                    Price = 2500,
                    Texture = "Textures/Player/FighterShip",
                    VisualScale = 0.25f,
                    Speed = 2.25f,
                    Friction = 0.8f,
                    Armor = 28.0f,
                }
            },
            {
                "StrongShip",
                new CChassisPart() {
                    Price = 6500,
                    Texture = "Textures/Player/StrongShip",
                    VisualScale = 0.25f,
                    Speed = 1.9f,
                    Friction = 0.85f,
                    Armor = 40.0f,
                }
            }
        };
    }

    public class GeneratorDefinitions
    {
        public static CGeneratorPart GetPart(string current)
        {
            return Items.ContainsKey(current) ? Items[current] : Items["BasicGenerator"];
        }

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
                "CoreGenerator",
                new CGeneratorPart() {
                    Price = 2000,
                    Energy = 12.0f,
                    Regen = 0.06f,
                }
            },
            {
                "PowerGenerator",
                new CGeneratorPart() {
                    Price = 4000,
                    Energy = 14.0f,
                    Regen = 0.09f,
                }
            },
            {
                "UltraGenerator",
                new CGeneratorPart() {
                    Price = 12000,
                    Energy = 20.0f,
                    Regen = 0.15f,
                }
            }
        };
    }

    public class ShieldDefinitions
    {
        public static CShieldPart GetPart(string current)
        {
            return Items.ContainsKey(current) ? Items[current] : Items["BasicShield"];
        }

        public static Dictionary<string, CShieldPart> Items = new Dictionary<string, CShieldPart>()
        {
            {
                "BasicShield",
                new CShieldPart() {
                    Shield = 20.0f,
                    Regen = 0.05f,
                }
            },
            {
                "EnhancedShield",
                new CShieldPart() {
                    Price = 2500,
                    Shield = 26.0f,
                    Regen = 0.08f,
                }
            },
            {
                "PowerShield",
                new CShieldPart() {
                    Price = 9000,
                    Shield = 40.0f,
                    Regen = 0.13f,
                }
            },
            {
                "UltraShield",
                new CShieldPart() {
                    Price = 14000,
                    Shield = 75.0f,
                    Regen = 0.25f,
                }
            }
        };
    }
}
