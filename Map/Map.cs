//
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
 * + Swirl
 * - Dango
 * + Cannon
 * + Splitter
 * + Teleporter
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
 * MUSIC
 * 
 * 
 * stage 1 - the voyage
 * stage 2 - alkali earth
 * stage 3 - luminosity
 * stage 4 - sirius
 * stage 5 - aerial assault
 * stage 6 - afraid of darkness
 * stage 7 - insurrection
 * stage 8 - troubled dreams
 * stage 9 - turbo
 * stage 10 - fighting for control
 * stage 11 - math party
 * stage 12 - eye of the predator
 * 
 * boss: boss 9
 * stage clear: ?? :(
 * shop: - title?
 * title - konamis moon base
 * secret stage: the hidden answer
 * puase: 1-X-0
 * 
 * - Stage 1 - the voyage
 * - Stage 2
 * - Stage 3
 * - Stage 4
 * - Stage 5
 * - Stage 6
 * - Stage 7
 * - Stage 8
 * - Stage 9
 * - Stage 10
 * - Stage 11
 * - Stage 12 - eye of the predator
 * - Boss
 * - Title - konami's moon base
 * - Shop - 
 * - Game Over
 * - Stage Clear
 * - Secret Stage
 * - Pause: 1-X-0
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
        public static List<string> AllPrimaryWeapons()
        {
            return new List<string>() {
                "FrontLaser",
                "SpreadLaser",
                "Plasma",
                "Flame",
                "Lightning",
                "Beam",
            };
        }

        public static List<string> AllSecondaryWeapons()
        {
            return new List<string>() {
                "Missile",
                "DrunkMissile",
                "SeekBomb",
                "Boomerang",
                "Vulcan",
            };
        }

        public static List<string> AllSidekickWeapons()
        {
            return new List<string>() {
                "MiniShot",
                "ChargeShot",
                "Blade",
                "MissileVolley",
            };
        }

        public static List<string> AllChassisParts()
        {
            return new List<string>() {
                "Rookie",
                "Eagle",
                "Crusher",
                "Interceptor",
                "Phoenix",
                "Lightning",
                "Dragon",
                "Demon",
                "Ace",
            };
        }

        public static List<string> AllGeneratorParts()
        {
            return new List<string>() {
                "Basic Mk 1",
                "Basic Mk 2",
                "Basic Mk 3",
                "Impulse Mk 1",
                "Impulse Mk 2",
                "Impulse Mk 3",
                "Capacitor Mk 1",
                "Capacitor Mk 2",
                "Capacitor Mk 3",
                "Kinetic",
                "Magnetic",
                "Fusion",
            };
        }

        public static List<string> AllShieldParts()
        {
            return new List<string>() {
                "Light Shield",
                "Fiber Shield",
                "Electric Shield",
                "Advanced Shield",
                "Micro Shield",
                "Actuator Shield",
                "Power Shield",
                "Tank Shield",
                "Ultimate Shield",
            };
        }

        public static List<CMapNode> Nodes = new List<CMapNode>()
        {
            new CMapNode() {
                SaveIndex = 0,
                StageName = "Editor Stage",
                Current = "EditorStage",
                Next = new List<string>() {
                    "*",
                },
                AvailablePrimaryWeaponParts = AllPrimaryWeapons(),
                AvailableSecondaryWeaponParts = AllSecondaryWeapons(),
                AvailableSidekickWeaponParts = AllSidekickWeapons(),
                AvailableChassisParts = AllChassisParts(),
                AvailableGeneratorParts = AllGeneratorParts(),
                AvailableShieldParts = AllShieldParts(),
            },
            new CMapNode() {
                SaveIndex = 0,
                StageName = "Start Stage",
                Current = "Start",
                Next = new List<string>() {
                    "Stage1",
                    "*",
                },
#if DEBUG
                AvailablePrimaryWeaponParts = AllPrimaryWeapons(),
                AvailableSecondaryWeaponParts = AllSecondaryWeapons(),
                AvailableSidekickWeaponParts = AllSidekickWeapons(),
                AvailableChassisParts = AllChassisParts(),
                AvailableGeneratorParts = AllGeneratorParts(),
                AvailableShieldParts = AllShieldParts(),
#else
                AvailablePrimaryWeaponParts = AllPrimaryWeapons(),
                AvailableSecondaryWeaponParts = AllSecondaryWeapons(),
                AvailableSidekickWeaponParts = AllSidekickWeapons(),
                AvailableChassisParts = new List<string>()
                {
                    "Rookie",    
                    "Eagle",    
                    "Crusher",    
                },
                AvailableGeneratorParts = new List<string>()
                {
                    "Basic Mk 1",    
                    "Impulse Mk 1",    
                },
                AvailableShieldParts = new List<string>()
                {
                    "Light Shield",    
                    "Fiber Shield",    
                },
#endif
            },
            new CMapNode() {
                SaveIndex = 0,
                StageName = "Flying Over Carrier",
                Current = "Stage1",
                Next = new List<string>() {
                    "Stage2",
                    "*",
                },
                AvailablePrimaryWeaponParts = AllPrimaryWeapons(),
                AvailableSecondaryWeaponParts = AllSecondaryWeapons(),
                AvailableSidekickWeaponParts = AllSidekickWeapons(),
                AvailableChassisParts = new List<string>()
                {
                    "Rookie",    
                    "Eagle",    
                    "Crusher",    
                    "Interceptor",    
                },
                AvailableGeneratorParts = new List<string>()
                {
                    "Basic Mk 1",    
                    "Basic Mk 2",    
                    "Impulse Mk 1",    
                    "Capacitor Mk 1",    
                },
                AvailableShieldParts = new List<string>()
                {
                    "Light Shield",    
                    "Fiber Shield",    
                    "Electric Shield",    
                },
            },
            new CMapNode() {
                SaveIndex = 1,
                StageName = "Forgotten Waters",
                Current = "Stage2",
                Next = new List<string>() {
                    "Stage3",
                    "*",
                },
                AvailablePrimaryWeaponParts = AllPrimaryWeapons(),
                AvailableSecondaryWeaponParts = AllSecondaryWeapons(),
                AvailableSidekickWeaponParts = AllSidekickWeapons(),
                AvailableChassisParts = new List<string>()
                {
                    "Rookie",    
                    "Crusher",    
                    "Interceptor",    
                },
                AvailableGeneratorParts = new List<string>()
                {
                    "Basic Mk 1",    
                    "Impulse Mk 1",    
                    "Capacitor Mk 1",    
                    "Capacitor Mk 2",    
                },
                AvailableShieldParts = new List<string>()
                {
                    "Light Shield",    
                    "Fiber Shield",    
                    "Electric Shield",    
                },
            },
            new CMapNode() {
                SaveIndex = 2,
                StageName = "Distant Planet",
                Current = "Stage3",
                Next = new List<string>() {
                    "Stage4",
                    "*",
                },
                AvailablePrimaryWeaponParts = AllPrimaryWeapons(),
                AvailableSecondaryWeaponParts = AllSecondaryWeapons(),
                AvailableSidekickWeaponParts = AllSidekickWeapons(),
                AvailableChassisParts = new List<string>()
                {
                    "Eagle",    
                    "Crusher",    
                    "Interceptor",    
                },
                AvailableGeneratorParts = new List<string>()
                {
                    "Basic Mk 2",    
                    "Impulse Mk 1",    
                    "Impulse Mk 2",    
                    "Capacitor Mk 1",    
                },
                AvailableShieldParts = new List<string>()
                {
                    "Light Shield",    
                    "Fiber Shield",    
                    "Micro Shield",    
                },
            },

            new CMapNode() {
                SaveIndex = 3,
                StageName = "Space Station RX-4",
                Current = "Stage4",
                Next = new List<string>() {
                    "Stage5",
                    "*",
                },
                AvailablePrimaryWeaponParts = AllPrimaryWeapons(),
                AvailableSecondaryWeaponParts = AllSecondaryWeapons(),
                AvailableSidekickWeaponParts = AllSidekickWeapons(),
                AvailableChassisParts = new List<string>()
                {
                    "Rookie",    
                    "Eagle",    
                    "Interceptor",    
                },
                AvailableGeneratorParts = new List<string>()
                {
                    "Basic Mk 2",    
                    "Impulse Mk 2",    
                    "Capacitor Mk 1",    
                    "Capacitor Mk 2",    
                    "Magnetic",    
                },
                AvailableShieldParts = new List<string>()
                {
                    "Light Shield",    
                    "Advanced Shield",    
                    "Micro Shield",    
                },

            },

            new CMapNode() {
                SaveIndex = 4,
                StageName = "Strike Inversion",
                Current = "Stage5",
                Next = new List<string>() {
                    "Stage6",
                    "*",
                },
                AvailablePrimaryWeaponParts = AllPrimaryWeapons(),
                AvailableSecondaryWeaponParts = AllSecondaryWeapons(),
                AvailableSidekickWeaponParts = AllSidekickWeapons(),
                AvailableChassisParts = new List<string>()
                {
                    "Rookie",    
                    "Eagle",    
                    "Crusher",    
                    "Interceptor",    
                },
                AvailableGeneratorParts = new List<string>()
                {
                    "Basic Mk 2",    
                    "Capacitor Mk 2",    
                    "Kinetic",    
                },
                AvailableShieldParts = new List<string>()
                {
                    "Electric Shield",    
                    "Micro Shield",    
                    "Actuator Shield",    
                },

            },

            new CMapNode() {
                SaveIndex = 5,
                StageName = "Granite Location",
                Current = "Stage6",
                Next = new List<string>() {
                    "Stage7",
                    "*",
                },
                AvailablePrimaryWeaponParts = AllPrimaryWeapons(),
                AvailableSecondaryWeaponParts = AllSecondaryWeapons(),
                AvailableSidekickWeaponParts = AllSidekickWeapons(),
                AvailableChassisParts = new List<string>()
                {
                    "Eagle",    
                    "Interceptor",    
                    "Phoenix",    
                },
                AvailableGeneratorParts = new List<string>()
                {
                    "Basic Mk 2",    
                    "Impulse Mk 2",    
                    "Capacitor Mk 3",    
                    "Magnetic",    
                },
                AvailableShieldParts = new List<string>()
                {
                    "Micro Shield",    
                    "Advanced Shield",    
                    "Actuator Shield",    
                },

            },
            new CMapNode() {
                SaveIndex = 6,
                StageName = "Tempest Nebula",
                Current = "Stage7",
                Next = new List<string>() {
                    "Stage8",
                    "*",
                },
                AvailablePrimaryWeaponParts = AllPrimaryWeapons(),
                AvailableSecondaryWeaponParts = AllSecondaryWeapons(),
                AvailableSidekickWeaponParts = AllSidekickWeapons(),
                AvailableChassisParts = new List<string>()
                {
                    "Crusher",    
                    "Eagle",    
                    "Phoenix",    
                },
                AvailableGeneratorParts = new List<string>()
                {
                    "Basic Mk 3",    
                    "Impulse Mk 2",    
                    "Impulse Mk 3",    
                    "Fusion",    
                },
                AvailableShieldParts = new List<string>()
                {
                    "Advanced Shield",    
                    "Micro Shield",    
                    "Power Shield",    
                },

            },
            new CMapNode() {
                SaveIndex = 7,
                StageName = "Mysterious Clouds",
                Current = "Stage8",
                Next = new List<string>() {
                    "Stage9",
                },
                AvailablePrimaryWeaponParts = AllPrimaryWeapons(),
                AvailableSecondaryWeaponParts = AllSecondaryWeapons(),
                AvailableSidekickWeaponParts = AllSidekickWeapons(),
                AvailableChassisParts = new List<string>()
                {
                    "Eagle",    
                    "Interceptor",    
                    "Lightning",    
                },
                AvailableGeneratorParts = new List<string>()
                {
                    "Basic Mk 2",    
                    "Basic Mk 3",    
                    "Capacitor Mk 2",    
                    "Capacitor Mk 3",    
                    "Kinetic",    
                },
                AvailableShieldParts = new List<string>()
                {
                    "Advanced Shield",    
                    "Actuator Shield",    
                    "Power Shield",    
                },

            },
            new CMapNode() {
                SaveIndex = 8,
                StageName = "Relent Less",
                Current = "Stage9",
                Next = new List<string>() {
                    "Stage10",
                },
                AvailablePrimaryWeaponParts = AllPrimaryWeapons(),
                AvailableSecondaryWeaponParts = AllSecondaryWeapons(),
                AvailableSidekickWeaponParts = AllSidekickWeapons(),
                AvailableChassisParts = new List<string>()
                {
                    "Crusher",    
                    "Interceptor",    
                    "Lightning",    
                    "Dragon",    
                },
                AvailableGeneratorParts = new List<string>()
                {
                    "Basic Mk 3",    
                    "Impulse Mk 3",    
                    "Fusion",    
                },
                AvailableShieldParts = new List<string>()
                {
                    "Micro Shield",    
                    "Power Shield",    
                    "Tank Shield",    
                },
            },
            new CMapNode() {
                SaveIndex = 9,
                StageName = "Invasive Action",
                Current = "Stage10",
                Next = new List<string>() {
                    "Stage11",
                },
                AvailablePrimaryWeaponParts = AllPrimaryWeapons(),
                AvailableSecondaryWeaponParts = AllSecondaryWeapons(),
                AvailableSidekickWeaponParts = AllSidekickWeapons(),
                AvailableChassisParts = new List<string>()
                {
                    "Interceptor",    
                    "Dragon",    
                    "Phoenix",    
                    "Demon",    
                },
                AvailableGeneratorParts = new List<string>()
                {
                    "Impulse Mk 3",    
                    "Capacitor Mk 3",    
                    "Kinetic",    
                },
                AvailableShieldParts = new List<string>()
                {
                    "Advanced Shield",    
                    "Actuator Shield",    
                    "Power Shield",    
                },
            },
            new CMapNode() {
                SaveIndex = 10,
                StageName = "Shadow of Tears",
                Current = "Stage11",
                Next = new List<string>() {
                    "Stage12",
                },
                AvailablePrimaryWeaponParts = AllPrimaryWeapons(),
                AvailableSecondaryWeaponParts = AllSecondaryWeapons(),
                AvailableSidekickWeaponParts = AllSidekickWeapons(),
                AvailableChassisParts = new List<string>()
                {
                    "Interceptor",    
                    "Lightning",    
                    "Ace",    
                    "Dragon",    
                },
                AvailableGeneratorParts = new List<string>()
                {
                    "Basic Mk 3",    
                    "Impulse Mk 3",    
                    "Capacitor Mk 3",    
                },
                AvailableShieldParts = new List<string>()
                {
                    "Actuator Shield",    
                    "Power Shield",    
                    "Tank Shield",    
                },
            },
            new CMapNode() {
                SaveIndex = 11,
                StageName = "Lamination X",
                Current = "Stage12",
                AvailablePrimaryWeaponParts = AllPrimaryWeapons(),
                AvailableSecondaryWeaponParts = AllSecondaryWeapons(),
                AvailableSidekickWeaponParts = AllSidekickWeapons(),
                AvailableChassisParts = new List<string>()
                {
                    "Lightning",    
                    "Demon",    
                    "Dragon",    
                    "Ace",    
                },
                AvailableGeneratorParts = new List<string>()
                {
                    "Impulse Mk 3",    
                    "Kinetic",    
                    "Magnetic",    
                    "Fusion",    
                },
                AvailableShieldParts = new List<string>()
                {
                    "Advanced Shield",    
                    "Tank Shield",    
                    "Ultimate Shield",    
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
            SaveIndex = 0;
            Current = "";
            StageName = "";
            Next = new List<string>();
            AvailablePrimaryWeaponParts = new List<string>();
            AvailableSecondaryWeaponParts = new List<string>();
            AvailableSidekickWeaponParts = new List<string>();
            AvailableChassisParts = new List<string>();
            AvailableGeneratorParts = new List<string>();
            AvailableShieldParts = new List<string>();
        }

        public int SaveIndex { get; set; }
        public string StageName { get; set; }
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
