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
                    Speed = 1.5f,
                    Friction = 0.85f,
                    Armor = 20.0f,
                    CollisionResistance = 0.25f,
                    Description = "Rookie Ship",
                    IconName = "Textures/UI/Shop/IconChassisRookie",
                }
            },
            {
                "Eagle",
                new CChassisPart() {
                    Price = 1250,
                    Texture = "Textures/Player/Eagle",
                    VisualScale = 0.25f,
                    Speed = 1.75f,
                    Friction = 0.825f,
                    Armor = 15.0f,
                    CollisionResistance = 0.15f,
                    Description = "Highly Maneuverable",
                    IconName = "Textures/UI/Shop/IconChassisEagle",
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
                    IconName = "Textures/UI/Shop/IconChassisCrusher",
                }
            },
            {
                "Interceptor",
                new CChassisPart() {
                    Price = 7000,
                    Texture = "Textures/Player/Interceptor",
                    VisualScale = 0.25f,
                    Speed = 2.0f,
                    Friction = 0.80f,
                    Armor = 25.0f,
                    CollisionResistance = 0.35f,
                    Description = "Experimental Fighter",
                    IconName = "Textures/UI/Shop/IconChassisInterceptor",
                }
            },
            {
                "Phoenix",
                new CChassisPart() {
                    Price = 9500,
                    Texture = "Textures/Player/Phoenix",
                    VisualScale = 0.25f,
                    Speed = 1.8f,
                    Friction = 0.85f,
                    Armor = 30.0f,
                    CollisionResistance = 0.50f,
                    Description = "Strong All-Round",
                    IconName = "Textures/UI/Shop/IconChassisPhoenix",
                }
            },
            {
                "Lightning",
                new CChassisPart() {
                    Price = 14000,
                    Texture = "Textures/Player/Lightning",
                    VisualScale = 0.25f,
                    Speed = 3.0f,
                    Friction = 0.775f,
                    Armor = 25.0f,
                    CollisionResistance = 0.35f,
                    Description = "Unmatched Speed",
                    IconName = "Textures/UI/Shop/IconChassisLightning",
                }
            },
            {
                "Dragon",
                new CChassisPart() {
                    Price = 22500,
                    Texture = "Textures/Player/Dragon",
                    VisualScale = 0.25f,
                    Speed = 2.15f,
                    Friction = 0.825f,
                    Armor = 35.0f,
                    CollisionResistance = 0.60f,
                    Description = "Powerful Fighter Ship",
                    IconName = "Textures/UI/Shop/IconChassisDragon",
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
                    CollisionResistance = 0.85f,
                    Description = "Exceptional Strength",
                    IconName = "Textures/UI/Shop/IconChassisDemon",
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
                    IconName = "Textures/UI/Shop/IconChassisAce",
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
            {
                "Standard Generator",
                new CGeneratorPart() {
                    Price = 1000,
                    Energy = 3.0f,
                    Regen = 0.05f,
                    IconName = "Textures/UI/Shop/IconGeneratorStandard",
                }
            },
            {
                "Conduction Generator",
                new CGeneratorPart() {
                    Price = 2500,
                    Energy = 4.0f,
                    Regen = 0.07f,
                    IconName = "Textures/UI/Shop/IconGeneratorConduction",
                }
            },
            {
                "Battery Generator",
                new CGeneratorPart() {
                    Price = 3500,
                    Energy = 7.0f,
                    Regen = 0.06f,
                    IconName = "Textures/UI/Shop/IconGeneratorBattery",
                }
            },
            {
                "Booster Generator",
                new CGeneratorPart() {
                    Price = 7500,
                    Energy = 5.0f,
                    Regen = 0.09f,
                    IconName = "Textures/UI/Shop/IconGeneratorBooster",
                }
            },
            {
                "Impulse Generator",
                new CGeneratorPart() {
                    Price = 12000,
                    Energy = 7.0f,
                    Regen = 0.10f,
                    IconName = "Textures/UI/Shop/IconGeneratorImpulse",
                }
            },
            {
                "Microwave Generator",
                new CGeneratorPart() {
                    Price = 14500,
                    Energy = 8.0f,
                    Regen = 0.11f,
                    IconName = "Textures/UI/Shop/IconGeneratorMicrowave",
                }
            },
            {
                "Vortex Generator",
                new CGeneratorPart() {
                    Price = 18000,
                    Energy = 9.0f,
                    Regen = 0.12f,
                    IconName = "Textures/UI/Shop/IconGeneratorVortex",
                }
            },
            {
                "Capacitor Generator",
                new CGeneratorPart() {
                    Price = 23500,
                    Energy = 14.0f,
                    Regen = 0.11f,
                    IconName = "Textures/UI/Shop/IconGeneratorCapacitor",
                }
            },
            {
                "Electron Generator",
                new CGeneratorPart() {
                    Price = 26500,
                    Energy = 12.0f,
                    Regen = 0.14f,
                    IconName = "Textures/UI/Shop/IconGeneratorElectron",
                }
            },
            {
                "Kinetic Generator",
                new CGeneratorPart() {
                    Price = 12000,
                    Energy = 10.0f,
                    Regen = 0.07f,
                    RegenOnCollide = 1.5f,
                    Description = "Collisions provide Energy",
                    IconName = "Textures/UI/Shop/IconGeneratorKinetic",
                }
            },
            {
                "Magnetic Generator",
                new CGeneratorPart() {
                    Price = 27000,
                    Energy = 16.0f,
                    Regen = 0.10f,
                    RegenOnCoin = 0.025f,
                    Description = "Coins increase Energy",
                    IconName = "Textures/UI/Shop/IconGeneratorMagnetic",
                }
            },
            {
                "Fusion Generator",
                new CGeneratorPart() {
                    Price = 45000,
                    Energy = 7.5f,
                    Regen = 0.22f,
                    IconName = "Textures/UI/Shop/IconGeneratorFusion",
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
                    IconName = "Textures/UI/Shop/IconShieldLight",
                }
            },
            {
                "Fiber Shield",
                new CShieldPart() {
                    Price = 2000,
                    Shield = 8.0f,
                    EnergyDrain = 0.035f,
                    Efficiency = 0.35f,
                    IconName = "Textures/UI/Shop/IconShieldFiber",
                }
            },
            {
                "Electric Shield",
                new CShieldPart() {
                    Price = 3500,
                    Shield = 7.0f,
                    EnergyDrain = 0.030f,
                    Efficiency = 0.65f,
                    IconName = "Textures/UI/Shop/IconShieldElectric",
                }
            },
            {
                "Advanced Shield",
                new CShieldPart() {
                    Price = 7500,
                    Shield = 9.0f,
                    EnergyDrain = 0.040f,
                    Efficiency = 0.75f,
                    IconName = "Textures/UI/Shop/IconShieldAdvanced",
                }
            },
            {
                "Micro Shield",
                new CShieldPart() {
                    Price = 9500,
                    Shield = 7.5f,
                    EnergyDrain = 0.065f,
                    Efficiency = 0.6f,
                    IconName = "Textures/UI/Shop/IconShieldMicro",
                }
            },
            {
                "Actuator Shield",
                new CShieldPart() {
                    Price = 11000,
                    Shield = 11.0f,
                    EnergyDrain = 0.045f,
                    Efficiency = 0.65f,
                    IconName = "Textures/UI/Shop/IconShieldActuator",
                }
            },
            {
                "Power Shield",
                new CShieldPart() {
                    Price = 13500,
                    Shield = 14.0f,
                    EnergyDrain = 0.075f,
                    Efficiency = 0.75f,
                    IconName = "Textures/UI/Shop/IconShieldPower",
                }
            },
            {
                "Tank Shield",
                new CShieldPart() {
                    Price = 13000,
                    Shield = 18.0f,
                    EnergyDrain = 0.050f,
                    Efficiency = 0.50f,
                    IconName = "Textures/UI/Shop/IconShieldTank",
                }
            },
            {
                "Ultimate Shield",
                new CShieldPart() {
                    Price = 15000,
                    Shield = 15.0f,
                    EnergyDrain = 0.075f,
                    Efficiency = 0.85f,
                    IconName = "Textures/UI/Shop/IconShieldUltimate",
                }
            },
        };
    }
}
