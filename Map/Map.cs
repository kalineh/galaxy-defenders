//
// Map.cs
//

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CMap
    {
        public static List<CMapNode> Nodes = new List<CMapNode>()
        {
            new CMapNode() {
                Current = "Start",
                Next = new List<string>() {
                    "Stage1",
                    "*",
                },
                AvailablePrimaryWeaponParts = new List<string>() {
                    "FrontLaser",
                    "SpreadLaser",
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
                    "CoreGeneratorMark2",
                },
                AvailableShieldParts = new List<string>() {
                    "BasicShield",
                    "EnhancedShield",
                    "UltraShield",
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
                    "UltraGenerator",
                },
                AvailableShieldParts = new List<string>() {
                    "BasicShield",
                    "EnhancedShield",
                    "UltraShield",
                },
            },
            new CMapNode() {
                Current = "Stage2",
                Next = new List<string>() {
                    "Stage3",
                    "*",
                }
            }
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
