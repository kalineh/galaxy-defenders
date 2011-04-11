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
            return Items.ContainsKey(current) ? Items[current] : Items["Rookie"];
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
                    Speed = 1.7f,
                    Friction = 0.90f,
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
                    Speed = 2.0f,
                    Friction = 0.95f,
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
            return Items.ContainsKey(current) ? Items[current] : Items["Standard Mk 1"];
        }

        public static Dictionary<string, CGeneratorPart> Items = new Dictionary<string, CGeneratorPart>()
        {
            // basic
            {
                "Standard Mk 1",
                new CGeneratorPart() {
                    Price = 1000,
                    Energy = 8.0f,
                    Regen = 0.05f,
                }
            },
            {
                "Standard Mk 2",
                new CGeneratorPart() {
                    Price = 5000,
                    Energy = 9.0f,
                    Regen = 0.07f,
                }
            },
            {
                "Standard Mk 3",
                new CGeneratorPart() {
                    Price = 18000,
                    Energy = 11.0f,
                    Regen = 0.10f,
                }
            },
            {
                "Standard Mk 4",
                new CGeneratorPart() {
                    Price = 35000,
                    Energy = 14.0f,
                    Regen = 0.15f,
                }
            },
            // impulse
            {
                "Impulse Mk 1",
                new CGeneratorPart() {
                    Price = 1500,
                    Energy = 3.0f,
                    Regen = 0.06f,
                }
            },
            {
                "Impulse Mk 2",
                new CGeneratorPart() {
                    Price = 7500,
                    Energy = 4.5f,
                    Regen = 0.09f,
                }
            },
            {
                "Impulse Mk 3",
                new CGeneratorPart() {
                    Price = 22000,
                    Energy = 7.0f,
                    Regen = 0.13f,
                }
            },
            {
                "Impulse Mk 4",
                new CGeneratorPart() {
                    Price = 38000,
                    Energy = 10.0f,
                    Regen = 0.18f,
                }
            },
            // capacitor
            {
                "Capacitor Mk 1",
                new CGeneratorPart() {
                    Price = 1500,
                    Energy = 14.0f,
                    Regen = 0.04f,
                }
            },
            {
                "Capacitor Mk 2",
                new CGeneratorPart() {
                    Price = 8000,
                    Energy = 16.0f,
                    Regen = 0.06f,
                }
            },
            {
                "Capacitor Mk 3",
                new CGeneratorPart() {
                    Price = 25000,
                    Energy = 21.0f,
                    Regen = 0.09f,
                }
            },
            {
                "Capacitor Mk 4",
                new CGeneratorPart() {
                    Price = 34000,
                    Energy = 38.0f,
                    Regen = 0.12f,
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
            return Items.ContainsKey(current) ? Items[current] : Items["Light Shield"];
        }

        public static Dictionary<string, CShieldPart> Items = new Dictionary<string, CShieldPart>()
        {
            {
                "Light Shield",
                new CShieldPart() {
                    Price = 1000,
                    Shield = 3.0f,
                    EnergyDrain = 0.030f,
                    Efficiency = 0.50f,
                }
            },
            {
                "Fiber Shield",
                new CShieldPart() {
                    Price = 2000,
                    Shield = 5.0f,
                    EnergyDrain = 0.035f,
                    Efficiency = 0.35f,
                }
            },
            {
                "Electric Shield",
                new CShieldPart() {
                    Price = 3500,
                    Shield = 4.0f,
                    EnergyDrain = 0.030f,
                    Efficiency = 0.65f,
                }
            },
            {
                "Advanced Shield",
                new CShieldPart() {
                    Price = 7500,
                    Shield = 5.0f,
                    EnergyDrain = 0.040f,
                    Efficiency = 0.75f,
                }
            },
            {
                "Micro Shield",
                new CShieldPart() {
                    Price = 9500,
                    Shield = 3.5f,
                    EnergyDrain = 0.065f,
                    Efficiency = 0.6f,
                }
            },
            {
                "Actuator Shield",
                new CShieldPart() {
                    Price = 11000,
                    Shield = 7.0f,
                    EnergyDrain = 0.045f,
                    Efficiency = 0.65f,
                }
            },
            {
                "Power Shield",
                new CShieldPart() {
                    Price = 13500,
                    Shield = 8.0f,
                    EnergyDrain = 0.075f,
                    Efficiency = 0.75f,
                }
            },
            {
                "Tank Shield",
                new CShieldPart() {
                    Price = 13000,
                    Shield = 15.0f,
                    EnergyDrain = 0.050f,
                    Efficiency = 0.50f,
                }
            },
            {
                "Ultimate Shield",
                new CShieldPart() {
                    Price = 15000,
                    Shield = 10.0f,
                    EnergyDrain = 0.075f,
                    Efficiency = 0.85f,
                }
            },
        };
    }
}
