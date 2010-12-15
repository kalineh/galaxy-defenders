﻿//
// Map.cs
//

/* 
 * Content Checklist
 * 
 * PRIMARY WEAPONS
 * + Front
 * + Spread
 * + Plasma
 * - Fire
 * - Shock
 * - Swirl
 * - Beam
 * - Lightning
 * 
 * SECONDARY WEAPONS
 * + Missile
 * + Seek Bomb
 * - ?
 * - ?
 * - ?
 * - ?
 * - ?
 * - ?
 * 
 * SIDEKICK WEAPONS
 * + Mini Shot
 * - Side Wave
 * - Shield Balls
 * - Bomblets
 * - Rear Spread Shot
 * - Blade
 * - Sonic Wave
 * - Rear Guard
 * 
 * ENEMIES
 * + Ball
 * + Shoot Ball
 * + Isosceles
 * + Down Turret
 * + Aim Turret
 * + Raid Turret
 * + Rotate Turret
 * + Missile Pod
 * + Beamer
 * + Triple Shot
 * + Fence Beam
 * + Cutter
 * + Airship
 * + Spike
 * + Pyramid
 * + Black Hole
 * + Glob
 * - Swirl
 * - Dango
 * - Cannon
 * - Splitter
 * - Teleporter
 * 
 * BOSSES
 * - FlyingOverCarrier
 * - ForgottenWaters
 * - DistantPlanet
 * - SpaceStationRX4
 * - StrikeInversion
 * - GraniteLocation
 * - TempestNebula
 * - MysteriousClouds
 * - RelentLess
 * - InvasiveAction
 * - ShadowOfTears
 * - LaminationX
 * 
 * DECORATIONS
 * - Square Platform
 * 
 * BUILDINGS
 * - Building 1
 * - Building 2
 * - Building 3
 * - Building 4
 * - Building 5
 * - Building 6
 * - Building 7
 * - Building 8
 * - Building 9
 * - Building 10
 * - Building 11
 * - Building 12
 * - Building 13
 * - Building 14
 * - Building 15
 * - Building 16
 * - Building 17
 * - Building 18
 * - Building 19
 * - Building 20
 * 
 * SCENERY
 * - FlyingOverCarrierBG
 * - FlyingOverCarrierFG
 * - ForgottenWatersBG
 * - ForgottenWatersFG
 * - DistantPlanetBG
 * - DistantPlanetFG
 * - SpaceStationRX4BG
 * - SpacestationRX4FG
 * - StrikeInversionBG
 * - StrikeInversionFG
 * - GraniteLocationBG
 * - GraniteLocationFG
 * - TempestNebulaBG
 * - TempestNebulaFG
 * - MysteriousCloudsBG
 * - MysteriousCloudsFG
 * - RelentLessBG
 * - RelentLessFG
 * - InvasiveActionBG
 * - InvasiveActionFG
 * - ShadowOfTearsBG
 * - ShadowOfTearsFG
 * - LaminationXBG
 * - LaminationXFG
 * 
 * SHOP ICONS
 * - Primary Weapons
 * - Secondary Weapons
 * - Sidekick Weapons
 * 
 * CHASSIS
 * - Rookie
 * - Eagle
 * - Crusher
 * - Interceptor
 * - Phoenix
 * - Lightning
 * - Dragon
 * - Demon
 * - Ace
 * - Ultima
 * 
 * GENERATORS
 * - Basic Mk 1
 * - Basic Mk 2
 * - Basic Mk 3
 * - Impulse Mk 1
 * - Impulse Mk 2
 * - Impulse Mk 3
 * - Capacitor Mk 1
 * - Capacitor Mk 2
 * - Capacitor Mk 3
 * - Kinetic // regen on hit
 * - Magnetic // regen on coin
 * - Fusion // low storage, fast regen
 * 
 * SHIELDS
 * - Light Shield
 * - Fiber Shield
 * - Magnetic Shield
 * - Electric Shield
 * - Advanced Shield
 * - Transport Shield
 * - Fusion Shield
 * - Micro Shield
 * - Disruptor Shield
 * - Power Shield
 * - Tank Shield
 * - Ultimate Shield
 * 
 * PORTRAITS
 * - Kazuki
 * - Rabbit
 * - Gunthor
 * - Mystery
 * 
 * 
 * Major Task Checklist
 * 
 * - Stage Layouts
 * - Secret Stages
 * + Remaining Enemies
 * - Shop System
 * - Main Menus
 * - Bosses
 * - Primary Weapons
 * - Secondary Weapons
 * - Sidekick Weapons
 * - Icons
 * - Chassis assets
 * - Building assets
 * - Scenery
 * - 360 profile/save data
 * - Optimization
 * - Secret Character
 * - Thread Effects
 * - Special ability fixing special cases
 * - Stats recording special cases (swirl-ball, etc)
 * - Polish!
 * 
 */

/*
 * Enemy Introduction
 * 
 * FLYING OVER CARRIER
 * + Ball
 * + DownTurret
 * + Turret
 * 
 * FORGOTTEN WATERS
 * + ShootBall
 * + Isosceles
 * 
 * DISTANT PLANET
 * + Spike
 * + RaidTurret
 * 
 * SPACE STATION RX4
 * + RotateTurret
 * + Splitter
 * 
 * STRIKE INVERSION
 * + TripleShot
 * + Beamer
 * 
 * GRANITE LOCATION
 * + Glob
 * + Swirl
 * 
 * TEMPEST NEBULA
 * + MissilePod
 * + BlackHole
 * 
 * MYSTERIOUS CLOUDS
 * - FenceBeam
 * - Airship
 * 
 * RELENT LESS
 * - Cutter
 * X Dango
 * 
 * INVASIVE ACTION
 * - Pyramid
 * - Teleporter
 * 
 * SHADOW OF TEARS
 * - Cannon
 * 
 * LAMINATION X
 * 
 */

/*
 * Theme
 * 
 * FLYING OVER CARRIER
 * - SKY: Light Blue
 * - BG: None
 * - FG: Circle Clouds
 * 
 * FORGOTTEN WATERS
 * - SKY: Dark Blue
 * - BG: Water Ripples
 * - FG: Sky Clouds
 * 
 * DISTANT PLANET
 * - SKY: GreenAqua
 * - BG: Drops
 * - FG: None
 * 
 * SPACE STATION RX4
 * - SKY: Black
 * - BG: Stars
 * - FG: Stars
 * 
 * STRIKE INVERSION
 * - SKY: Dark Blue
 * - BG: Diagonal Moving Blocks
 * - FG: None
 * 
 * GRANITE LOCATION
 * - SKY: Black
 * - BG: Vertical Lines
 * - FG: Stars
 * 
 * TEMPEST NEBULA
 * - SKY: Gradient: Blue, Green
 * - BG: Snow
 * - FG: Snow
 * 
 * MYSTERIOUS CLOUDS
 * - SKY: Light Blue
 * - BG: None
 * - FG: Sky Clouds
 * 
 * RELENT LESS
 * - SKY: Dark Grey
 * - BG: Vertical Lines
 * - FG: None
 * 
 * INVASIVE ACTION
 * - SKY: Light Blue
 * - BG: Water Ripples
 * - FG: Drops
 * 
 * SHADOW OF TEARS
 * - SKY: Dark Blue
 * - BG: Snow
 * - FG: Snow
 * 
 * LAMINATION X
 * - SKY: Black
 * - BG: Water Ripples
 * - FG: None
 * 
 */

/*
 * Decorations
 * 
 * FLYING OVER CARRIER
 * 
 * FORGOTTEN WATERS
 * 
 * DISTANT PLANET
 * 
 * SPACE STATION RX4
 * 
 * STRIKE INVERSION
 * 
 * GRANITE LOCATION
 * 
 * TEMPEST NEBULA
 * 
 * MYSTERIOUS CLOUDS
 * 
 * RELENT LESS
 * 
 * INVASIVE ACTION
 * 
 * SHADOW OF TEARS
 * 
 * LAMINATION X
 * 
 */


using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CMap
    {
        public static List<CMapNode> Nodes = new List<CMapNode>()
        {
            new CMapNode() {
                Current = "EditorStage",
                Next = new List<string>() {
                    "*",
                },
                AvailablePrimaryWeaponParts = new List<string>() {
                    "FrontLaser",
                    "SpreadLaser",
                    "Plasma",
                },
                AvailableSecondaryWeaponParts = new List<string>() {
                    "Missile",
                    "SeekBomb",
                },
                AvailableSidekickWeaponParts = new List<string>() {
                    "MiniShot",
                },
                AvailableChassisParts = new List<string>() {
                    "BasicShip",
                    "FighterShip",
                },
                AvailableGeneratorParts = new List<string>() {
                    "BasicGenerator",
                    "CoreGenerator",
                    "PowerGenerator",
                },
                AvailableShieldParts = new List<string>() {
                    "BasicShield",
                    "EnhancedShield",
                },
            },
            new CMapNode() {
                Current = "Start",
                Next = new List<string>() {
                    "Stage1",
                    "*",
                },
                AvailablePrimaryWeaponParts = new List<string>() {
                    "FrontLaser",
                    "SpreadLaser",
                    "Plasma",
                },
                AvailableSecondaryWeaponParts = new List<string>() {
                    "Missile",
                    "SeekBomb",
                },
                AvailableSidekickWeaponParts = new List<string>() {
                    "MiniShot",
                },
                AvailableChassisParts = new List<string>() {
                    "BasicShip",
                    "FighterShip",
                },
                AvailableGeneratorParts = new List<string>() {
                    "BasicGenerator",
                    "CoreGenerator",
                    "PowerGenerator",
                },
                AvailableShieldParts = new List<string>() {
                    "BasicShield",
                    "EnhancedShield",
                },
            },
            new CMapNode() {
                Current = "Stage1",
                Next = new List<string>() {
                    "Stage2",
                    "*",
                },
                AvailablePrimaryWeaponParts = new List<string>() {
                    "FrontLaser",
                    "Plasma",
                },
                AvailableSecondaryWeaponParts = new List<string>() {
                    "Missile",
                    "SeekBomb",
                },
                AvailableSidekickWeaponParts = new List<string>() {
                    "MiniShot",
                },
                AvailableChassisParts = new List<string>() {
                    "BasicShip",
                    "FighterShip",
                    "StrongShip",
                },
                AvailableGeneratorParts = new List<string>() {
                    "BasicGenerator",
                    "CoreGenerator",
                    "PowerGenerator",
                },
                AvailableShieldParts = new List<string>() {
                    "BasicShield",
                    "EnhancedShield",
                    "PowerShield",
                },
            },
            new CMapNode() {
                Current = "Stage2",
                Next = new List<string>() {
                    "Stage3",
                    "*",
                },
                AvailablePrimaryWeaponParts = new List<string>() {
                    "FrontLaser",
                    "Plasma",
                },
                AvailableSecondaryWeaponParts = new List<string>() {
                    "Missile",
                    "SeekBomb",
                },
                AvailableSidekickWeaponParts = new List<string>() {
                    "MiniShot",
                },
                AvailableChassisParts = new List<string>() {
                    "BasicShip",
                    "FighterShip",
                    "StrongShip",
                },
                AvailableGeneratorParts = new List<string>() {
                    "BasicGenerator",
                    "CoreGenerator",
                    "UltraGenerator",
                },
                AvailableShieldParts = new List<string>() {
                    "BasicShield",
                    "EnhancedShield",
                    "PowerShield",
                },
            },
            new CMapNode() {
                Current = "Stage3",
                Next = new List<string>() {
                    "Stage4",
                    "*",
                },
                AvailablePrimaryWeaponParts = new List<string>() {
                    "FrontLaser",
                    "Plasma",
                },
                AvailableSecondaryWeaponParts = new List<string>() {
                    "Missile",
                    "SeekBomb",
                },
                AvailableSidekickWeaponParts = new List<string>() {
                    "MiniShot",
                },
                AvailableChassisParts = new List<string>() {
                    "FighterShip",
                    "StrongShip",
                },
                AvailableGeneratorParts = new List<string>() {
                    "CoreGenerator",
                    "PowerGenerator",
                    "UltraGenerator",
                },
                AvailableShieldParts = new List<string>() {
                    "EnhancedShield",
                    "PowerShield",
                    "UltraShield",
                },
            },

            new CMapNode() {
                Current = "Stage4",
                Next = new List<string>() {
                    "Stage5",
                    "*",
                },
                AvailablePrimaryWeaponParts = new List<string>() {
                    "FrontLaser",
                    "SpreadLaser",
                    "Plasma",
                },
                AvailableSecondaryWeaponParts = new List<string>() {
                    "Missile",
                    "SeekBomb",
                },
                AvailableSidekickWeaponParts = new List<string>() {
                    "MiniShot",
                },
                AvailableChassisParts = new List<string>() {
                    "BasicShip",
                    "FighterShip",
                    "StrongShip",
                },
                AvailableGeneratorParts = new List<string>() {
                    "BasicGenerator",
                    "CoreGenerator",
                    "PowerGenerator",
                    "UltraGenerator",
                },
                AvailableShieldParts = new List<string>() {
                    "BasicShield",
                    "EnhancedShield",
                    "UltraShield",
                },
            },

            new CMapNode() {
                Current = "Stage5",
                Next = new List<string>() {
                    "Stage6",
                    "*",
                },
                AvailablePrimaryWeaponParts = new List<string>() {
                    "FrontLaser",
                    "SpreadLaser",
                    "Plasma",
                },
                AvailableSecondaryWeaponParts = new List<string>() {
                    "Missile",
                    "SeekBomb",
                },
                AvailableSidekickWeaponParts = new List<string>() {
                    "MiniShot",
                },
                AvailableChassisParts = new List<string>() {
                    "BasicShip",
                    "FighterShip",
                    "StrongShip",
                },
                AvailableGeneratorParts = new List<string>() {
                    "BasicGenerator",
                    "CoreGenerator",
                    "PowerGenerator",
                    "UltraGenerator",
                },
                AvailableShieldParts = new List<string>() {
                    "BasicShield",
                    "EnhancedShield",
                    "UltraShield",
                },
            },

            new CMapNode() {
                Current = "Stage6",
                Next = new List<string>() {
                    "Stage7",
                    "*",
                },
                AvailablePrimaryWeaponParts = new List<string>() {
                    "FrontLaser",
                    "SpreadLaser",
                    "Plasma",
                },
                AvailableSecondaryWeaponParts = new List<string>() {
                    "Missile",
                    "SeekBomb",
                },
                AvailableSidekickWeaponParts = new List<string>() {
                    "MiniShot",
                },
                AvailableChassisParts = new List<string>() {
                    "BasicShip",
                    "FighterShip",
                    "StrongShip",
                },
                AvailableGeneratorParts = new List<string>() {
                    "BasicGenerator",
                    "CoreGenerator",
                    "PowerGenerator",
                    "UltraGenerator",
                },
                AvailableShieldParts = new List<string>() {
                    "BasicShield",
                    "EnhancedShield",
                    "UltraShield",
                },
            },
            new CMapNode() {
                Current = "Stage7",
                Next = new List<string>() {
                    "*",
                },
                AvailablePrimaryWeaponParts = new List<string>() {
                    "FrontLaser",
                    "SpreadLaser",
                    "Plasma",
                },
                AvailableSecondaryWeaponParts = new List<string>() {
                    "Missile",
                    "SeekBomb",
                },
                AvailableSidekickWeaponParts = new List<string>() {
                    "MiniShot",
                },
                AvailableChassisParts = new List<string>() {
                    "BasicShip",
                    "FighterShip",
                    "StrongShip",
                },
                AvailableGeneratorParts = new List<string>() {
                    "BasicGenerator",
                    "CoreGenerator",
                    "PowerGenerator",
                    "UltraGenerator",
                },
                AvailableShieldParts = new List<string>() {
                    "BasicShield",
                    "EnhancedShield",
                    "UltraShield",
                },
            },
        };

        public static CMapNode GetMapNodeByStageName(string stage_name)
        {
            CMapNode node = Nodes.Find(n => n.Current == stage_name);
            return node;
        }
    }

    public class CMapNode
    {
        public CMapNode()
        {
            Current = "";
            Next = new List<string>();
            AvailablePrimaryWeaponParts = new List<string>();
            AvailableSecondaryWeaponParts = new List<string>();
            AvailableSidekickWeaponParts = new List<string>();
            AvailableChassisParts = new List<string>();
            AvailableGeneratorParts = new List<string>();
            AvailableShieldParts = new List<string>();
        }

        public string Current { get; set; }
        public List<string> Next { get; set; }
        public List<string> AvailablePrimaryWeaponParts { get; set; }
        public List<string> AvailableSecondaryWeaponParts { get; set; }
        public List<string> AvailableSidekickWeaponParts { get; set; }
        public List<string> AvailableChassisParts { get; set; }
        public List<string> AvailableGeneratorParts { get; set; }
        public List<string> AvailableShieldParts { get; set; }
    }
}