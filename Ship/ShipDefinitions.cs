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
            CChassisPart chassis = ChassisDefinitions.Items[profile.ChassisType];
            CGeneratorPart generator = GeneratorDefinitions.Items[profile.GeneratorType];
            CShieldPart shield = ShieldDefinitions.Items[profile.ShieldType];
            CWeaponPart primary = WeaponDefinitions.GetPart(profile.WeaponPrimaryType, profile.WeaponPrimaryLevel);
            CWeaponPart secondary = WeaponDefinitions.GetPart(profile.WeaponSecondaryType, profile.WeaponSecondaryLevel);
            CWeaponPart sidekick_left = WeaponDefinitions.GetPart(profile.WeaponPrimaryType, profile.WeaponPrimaryLevel);
            CWeaponPart sidekick_right = WeaponDefinitions.GetPart(profile.WeaponSecondaryType, profile.WeaponSecondaryLevel);
            CShip ship = new CShip(world, chassis, generator, shield, primary, secondary, sidekick_left, sidekick_right);
            return ship;
        }
    }

    public class ChassisDefinitions
    {
        public static Dictionary<string, CChassisPart> Items = new Dictionary<string, CChassisPart>()
        {
            {
                "BasicShip",
                new CChassisPart() {
                    Texture = "Textures/Player/Ship",
                    VisualScale = 0.25f,
                    Speed = 2.25f,
                    Friction = 0.8f,
                    Armor = 10.0f,
                }
            },

            {
                "BasicShip2",
                new CChassisPart() {
                    Texture = "Textures/Player/Ship",
                    VisualScale = 0.25f,
                    Speed = 2.25f,
                    Friction = 0.8f,
                    Armor = 10.0f,
                }
            }
        };
    }

    public class GeneratorDefinitions
    {
        public static Dictionary<string, CGeneratorPart> Items = new Dictionary<string, CGeneratorPart>()
        {
            {
                "BasicGenerator",
                new CGeneratorPart() {
                    Energy = 10.0f,
                    Regen = 0.02f,
                }
            }
        };
    }

    public class ShieldDefinitions
    {
        public static Dictionary<string, CShieldPart> Items = new Dictionary<string, CShieldPart>()
        {
            {
                "BasicShield",
                new CShieldPart() {
                    Shield = 5.0f,
                    Regen = 0.01f,
                }
            }
        };
    }
}
