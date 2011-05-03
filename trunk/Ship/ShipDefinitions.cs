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
        public static CShip GenerateShip(CWorld world, SProfilePilotState profile, GameControllerIndex index)
        {
            CPilot pilot = CPilot.MakePilot(profile);
            CChassisPart chassis = ChassisDefinitions.GetPart(profile.ChassisType);
            CGeneratorPart generator = GeneratorDefinitions.GetPart(profile.GeneratorType);
            CShieldPart shield = ShieldDefinitions.GetPart(profile.ShieldType);
            CWeaponPart primary = WeaponDefinitions.GetPart(profile.WeaponPrimaryType, profile.WeaponPrimaryLevel);
            CWeaponPart primary_focus = WeaponDefinitions.GetPart(profile.WeaponPrimaryType + "Focus", profile.WeaponPrimaryLevel);
            CWeaponPart secondary = WeaponDefinitions.GetPart(profile.WeaponSecondaryType, profile.WeaponSecondaryLevel);
            CWeaponPart sidekick = WeaponDefinitions.GetPart(profile.WeaponSidekickType, profile.WeaponSidekickLevel);
            CShip ship = new CShip();
            ship.Initialize(world, index, pilot, chassis, generator, shield, primary, primary_focus, secondary, sidekick);
            return ship;
        }
    }

    public class ChassisDefinitions
    {
        public static CChassisPart GetPart(string current)
        {
            return Items[current];
        }

        public static Dictionary<string, CChassisPart> Items = new Dictionary<string, CChassisPart>()
        {
            {
                "Rookie",
                new CChassisPart() {
                    Price = 1000,
                    Texture = "Textures/Player/Rookie",
                    VisualScale = 0.25f,
                    Speed = 1.75f,
                    Friction = 0.85f,
                    Armor = 20.0f,
                    CollisionResistance = 0.25f,
                    Description = "Rookie Ship",
                }
            },
            {
                "Eagle",
                new CChassisPart() {
                    Price = 1250,
                    Texture = "Textures/Player/Eagle",
                    VisualScale = 0.25f,
                    Speed = 2.0f,
                    Friction = 0.825f,
                    Armor = 15.0f,
                    CollisionResistance = 0.15f,
                    Description = "Highly Maneuverable",
                }
            },
            {
                "Crusher",
                new CChassisPart() {
                    Price = 2500,
                    Texture = "Textures/Player/Crusher",
                    VisualScale = 0.25f,
                    Speed = 0.7f,
                    Friction = 0.92f,
                    Armor = 30.0f,
                    CollisionResistance = 0.60f,
                    Description = "Solid and Durable",
                }
            },
            {
                "Interceptor",
                new CChassisPart() {
                    Price = 7000,
                    Texture = "Textures/Player/Interceptor",
                    VisualScale = 0.25f,
                    Speed = 2.25f,
                    Friction = 0.80f,
                    Armor = 25.0f,
                    CollisionResistance = 0.35f,
                    Description = "Experimental Fighter",
                }
            },
            {
                "Phoenix",
                new CChassisPart() {
                    Price = 9500,
                    Texture = "Textures/Player/Phoenix",
                    VisualScale = 0.25f,
                    Speed = 2.0f,
                    Friction = 0.85f,
                    Armor = 30.0f,
                    CollisionResistance = 0.50f,
                    Description = "Strong All-Round",
                }
            },
            {
                "Lightning",
                new CChassisPart() {
                    Price = 14000,
                    Texture = "Textures/Player/Lightning",
                    VisualScale = 0.25f,
                    Speed = 3.5f,
                    Friction = 0.775f,
                    Armor = 25.0f,
                    CollisionResistance = 0.35f,
                    Description = "Unmatched Speed",
                }
            },
            {
                "Dragon",
                new CChassisPart() {
                    Price = 22500,
                    Texture = "Textures/Player/Dragon",
                    VisualScale = 0.25f,
                    Speed = 2.25f,
                    Friction = 0.825f,
                    Armor = 35.0f,
                    CollisionResistance = 0.60f,
                    Description = "Powerful Fighter Ship",
                }
            },
            {
                "Demon",
                new CChassisPart() {
                    Price = 22500,
                    Texture = "Textures/Player/Demon",
                    VisualScale = 0.25f,
                    Speed = 0.75f,
                    Friction = 0.92f,
                    Armor = 50.0f,
                    Description = "Exceptional Strength",
                    CollisionResistance = 0.85f,
                }
            },
            {
                "Ace",
                new CChassisPart() {
                    Price = 25000,
                    Texture = "Textures/Player/Ace",
                    VisualScale = 0.25f,
                    Speed = 2.75f,
                    Friction = 0.75f,
                    Armor = 40.0f,
                    CollisionResistance = 0.75f,
                    Description = "Top Quality",
                }
            },
        };
    }

    public class GeneratorDefinitions
    {
        public static CGeneratorPart GetPart(string current)
        {
            return Items[current];
        }

        public static Dictionary<string, CGeneratorPart> Items = new Dictionary<string, CGeneratorPart>()
        {
            // basic
            {
                "1",
                new CGeneratorPart() {
                    Price = 1000,
                    Energy = 3.0f,
                    Regen = 0.05f,
                }
            },
            {
                "2",
                new CGeneratorPart() {
                    Price = 2500,
                    Energy = 4.0f,
                    Regen = 0.07f,
                }
            },
            {
                "3",
                new CGeneratorPart() {
                    Price = 3500,
                    Energy = 5.0f,
                    Regen = 0.08f,
                }
            },
            {
                "4",
                new CGeneratorPart() {
                    Price = 7500,
                    Energy = 6.0f,
                    Regen = 0.09f,
                }
            },
            // impulse
            {
                "5",
                new CGeneratorPart() {
                    Price = 12000,
                    Energy = 7.0f,
                    Regen = 0.10f,
                }
            },
            {
                "6",
                new CGeneratorPart() {
                    Price = 14500,
                    Energy = 8.0f,
                    Regen = 0.11f,
                }
            },
            {
                "7",
                new CGeneratorPart() {
                    Price = 18000,
                    Energy = 9.0f,
                    Regen = 0.12f,
                }
            },
            {
                "8",
                new CGeneratorPart() {
                    Price = 23500,
                    Energy = 10.0f,
                    Regen = 0.13f,
                }
            },
            // capacitor
            {
                "9",
                new CGeneratorPart() {
                    Price = 26500,
                    Energy = 11.0f,
                    Regen = 0.14f,
                }
            },
            {
                "10",
                new CGeneratorPart() {
                    Price = 31000,
                    Energy = 12.0f,
                    Regen = 0.15f,
                }
            },
            {
                "11",
                new CGeneratorPart() {
                    Price = 34000,
                    Energy = 13.0f,
                    Regen = 0.16f,
                }
            },
            {
                "12",
                new CGeneratorPart() {
                    Price = 38000,
                    Energy = 14.0f,
                    Regen = 0.17f,
                }
            },
            {
                "Kinetic",
                new CGeneratorPart() {
                    Price = 12000,
                    Energy = 10.0f,
                    Regen = 0.07f,
                    RegenOnCollide = 1.5f,
                    Description = "Collisions provide extra Energy",
                }
            },
            {
                "Magnetic",
                new CGeneratorPart() {
                    Price = 27000,
                    Energy = 16.0f,
                    Regen = 0.10f,
                    RegenOnCoin = 0.025f,
                    Description = "Coins increase Energy",
                }
            },
            {
                "Fusion",
                new CGeneratorPart() {
                    Price = 45000,
                    Energy = 7.5f,
                    Regen = 0.22f,
                }
            },
        };
    }

    public class ShieldDefinitions
    {
        public static CShieldPart GetPart(string current)
        {
            return Items[current];
        }

        public static Dictionary<string, CShieldPart> Items = new Dictionary<string, CShieldPart>()
        {
            {
                "Light Shield",
                new CShieldPart() {
                    Price = 1000,
                    Shield = 6.0f,
                    EnergyDrain = 0.030f,
                    Efficiency = 0.50f,
                }
            },
            {
                "Fiber Shield",
                new CShieldPart() {
                    Price = 2000,
                    Shield = 8.0f,
                    EnergyDrain = 0.035f,
                    Efficiency = 0.35f,
                }
            },
            {
                "Electric Shield",
                new CShieldPart() {
                    Price = 3500,
                    Shield = 7.0f,
                    EnergyDrain = 0.030f,
                    Efficiency = 0.65f,
                }
            },
            {
                "Advanced Shield",
                new CShieldPart() {
                    Price = 7500,
                    Shield = 9.0f,
                    EnergyDrain = 0.040f,
                    Efficiency = 0.75f,
                }
            },
            {
                "Micro Shield",
                new CShieldPart() {
                    Price = 9500,
                    Shield = 7.5f,
                    EnergyDrain = 0.065f,
                    Efficiency = 0.6f,
                }
            },
            {
                "Actuator Shield",
                new CShieldPart() {
                    Price = 11000,
                    Shield = 11.0f,
                    EnergyDrain = 0.045f,
                    Efficiency = 0.65f,
                }
            },
            {
                "Power Shield",
                new CShieldPart() {
                    Price = 13500,
                    Shield = 14.0f,
                    EnergyDrain = 0.075f,
                    Efficiency = 0.75f,
                }
            },
            {
                "Tank Shield",
                new CShieldPart() {
                    Price = 13000,
                    Shield = 18.0f,
                    EnergyDrain = 0.050f,
                    Efficiency = 0.50f,
                }
            },
            {
                "Ultimate Shield",
                new CShieldPart() {
                    Price = 15000,
                    Shield = 15.0f,
                    EnergyDrain = 0.075f,
                    Efficiency = 0.85f,
                }
            },
        };
    }
}
