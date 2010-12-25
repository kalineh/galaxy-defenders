//
// Stage1.cs
//
namespace Galaxy {
namespace Stages {
public class Stage1 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("Stage1");
stage.DisplayName = "FLYING OVER CARRIER";
stage.ScrollSpeed = 3.0f;
stage.BackgroundSceneryName = "FlyingOverCarrierBG";
stage.ForegroundSceneryName = "FlyingOverCarrierFG";
stage.MusicName = "The_Voyage";
stage.AddElement(0, 
new Galaxy.CStageElementCameraStop() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -24392.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementStageFinish() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -25768.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = -968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -23480.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -23480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -23488.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -23488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -368.0f,
Y = -23472.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -23488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -23488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -23480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -23776.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -23776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -23784.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -23784.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -368.0f,
Y = -23768.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -23784.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -23784.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -23776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -24000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -24000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -24000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -24136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -24136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -24136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -24000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -24000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -24000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -24136.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -24136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -24136.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -24136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -24136.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -24136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 528.0f,
Y = -23488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 528.0f,
Y = -23784.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -496.0f,
Y = -23768.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -496.0f,
Y = -23472.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementMusicChanger() {
MusicName = "Boss 9",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -23768.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -1904.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -1712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -1776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -1648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -1528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -1408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -1408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -1408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -1408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -1336.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -1208.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -1088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -1832.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -1696.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -1832.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -1696.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -1832.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -1704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -1832.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -1664.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -1408.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -1408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -2680.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -2680.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -2552.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -2608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -2688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 480.0f,
Y = -2688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -2552.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -2552.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -2552.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -2608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -2584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -2456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -2440.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -2584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -2712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -2608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -2408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -2840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -2968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -2608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -2584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -2408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -2456.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -2456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -2560.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -3096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -4664.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -3096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -3224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -3352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -3352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -3480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -3608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -3608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -3608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -3608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -3096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -3096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -3352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4296.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4296.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4232.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4232.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4168.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -4408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -3352.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -3352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -3608.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -3608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -3608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -3608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -3608.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -3608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -4536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -4536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -4280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -4280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -4536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -4536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -4536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -4792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -4536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -4792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -4792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -4664.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -4536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -4536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -4792.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -4792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -4536.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -4536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -4536.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -4536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -4536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -5944.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -5944.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -5624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -5624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -5816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -5688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -5624.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -5624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.0f,
Y = -5624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 512.0f,
Y = -5624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -5816.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -5816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 528.0f,
Y = -5912.0f,
},
},
MoverPresetName = "SinHorizontal",
MoverSpeedMultiplier = -4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 528.0f,
Y = -5912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -6104.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -6104.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -7744.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -456.0f,
Y = -7616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -6632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -456.0f,
Y = -7616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -480.0f,
Y = -6632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -7616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -328.0f,
Y = -7616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -7872.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -7872.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -7744.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -7616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -7616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -7616.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -7616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -6632.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -6632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 512.0f,
Y = -5176.0f,
},
},
MoverPresetName = "SinHorizontal",
MoverSpeedMultiplier = -4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 512.0f,
Y = -5176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -528.0f,
Y = -5256.0f,
},
},
MoverPresetName = "SinHorizontal",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -528.0f,
Y = -5256.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -536.0f,
Y = -4992.0f,
},
},
MoverPresetName = "SinHorizontal",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -536.0f,
Y = -4992.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -6792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -480.0f,
Y = -6792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -6792.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -6792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -6632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -6632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -6632.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -6632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -6792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -6792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -6792.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -6792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -7040.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -7040.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = -7088.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = -7088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -7088.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -7088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -7616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -7872.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -7872.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.0f,
Y = -7616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -7616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 568.0f,
Y = -7616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 440.0f,
Y = -7616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -7872.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -7872.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 512.0f,
Y = -5368.0f,
},
},
MoverPresetName = "SinHorizontal",
MoverSpeedMultiplier = -4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 512.0f,
Y = -5368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8760.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -9144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -9144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -9144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -9144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -9144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -9272.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -9144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 408.0f,
Y = -9144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -9144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -9016.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -9144.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -9144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -9144.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -9144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -9144.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -9144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -9144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 536.0f,
Y = -9144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 408.0f,
Y = -8888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 536.0f,
Y = -8888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -9272.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -8424.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -8424.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -8480.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -8480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -8368.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -8368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -10408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 408.0f,
Y = -10408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -10280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -10280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -10024.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -10536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -10536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -10408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -10280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -10280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -10536.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -10536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -10024.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -11968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -11712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -11840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -11840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -12096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -11584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -11840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -12096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -12096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -11968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -11840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -11840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -11584.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -11584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -12096.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -12096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -12096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -13784.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -13784.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -368.0f,
Y = -13912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -368.0f,
Y = -14040.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -13784.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -240.0f,
Y = -14040.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -14040.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -13912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -13784.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -13784.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -368.0f,
Y = -14040.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -368.0f,
Y = -14040.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -368.0f,
Y = -13912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -15840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -15712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -15584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -15584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -15456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -15456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -480.0f,
Y = -15456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -15456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -15328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -15200.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -15200.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -15456.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -15456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -16912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -464.0f,
Y = -16912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 504.0f,
Y = -16904.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -16904.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -17040.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -17168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -17168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -16904.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -16904.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -18480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -18352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -18608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -18352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -18608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -18608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -18608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -18352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -18352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -18352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -18352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -18352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -18608.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -18608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -18608.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -18608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -18608.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -18608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -18608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -20016.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -19504.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -19888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -19888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -20520.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -20144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -20144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -20016.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -19888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -19888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -20520.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -20520.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -10536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -10152.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -10280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -10536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -10536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 408.0f,
Y = -10536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -10280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 408.0f,
Y = -10536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -10280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -10280.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -10280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -10536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -11384.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -11384.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -11384.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -11456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -11584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -12096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -192.0f,
Y = -12096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -312.0f,
Y = -12096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -432.0f,
Y = -12096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -432.0f,
Y = -12224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -432.0f,
Y = -12352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -432.0f,
Y = -12480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDoubleTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -432.0f,
Y = -12096.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -432.0f,
Y = -12096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -11584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -432.0f,
Y = -12608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -12224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -12352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -12480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -12608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -12736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -424.0f,
Y = -12736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -12736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -424.0f,
Y = -12736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -12992.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -12992.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -13024.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -13024.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.0f,
Y = -13056.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.0f,
Y = -13056.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -13088.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -13088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -13120.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -13120.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -13152.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -13152.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -14040.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -14040.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -632.0f,
Y = -14432.0f,
},
},
MoverPresetName = "Right",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -632.0f,
Y = -14432.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -576.0f,
Y = -14464.0f,
},
},
MoverPresetName = "Right",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -576.0f,
Y = -14464.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -520.0f,
Y = -14496.0f,
},
},
MoverPresetName = "Right",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -520.0f,
Y = -14496.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -15200.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -15200.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -15456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -15200.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -552.0f,
Y = -14960.0f,
},
},
MoverPresetName = "Right",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -552.0f,
Y = -14960.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 512.0f,
Y = -15088.0f,
},
},
MoverPresetName = "Left",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 512.0f,
Y = -15088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -15888.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -15888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -16000.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -16000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -16088.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -16088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -16912.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -16912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -17032.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -17160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -17160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -17624.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -17624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -17840.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -17840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -17528.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -17528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -17776.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -17776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -18848.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -18848.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -18912.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -18912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -18352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -18736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -18864.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -18992.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -19120.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -19248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -19192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -19192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -19192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -19184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -19184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -19256.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -19376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -19376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -19368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -19368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -19376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -19360.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -19360.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -19424.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -19424.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -19632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -19760.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -20152.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -20152.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -392.0f,
Y = -20480.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -392.0f,
Y = -20480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -20600.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -20600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -20416.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -20416.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -16224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -16224.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -16224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -16096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -15968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -18352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -19376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -19192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -20144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -20144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -20272.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -20272.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -20264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -20392.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -20264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -20264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -20264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -19888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -21240.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -21240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -21360.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -21360.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -21176.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -21176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -21920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -21928.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -21928.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -21792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -21792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 304.0f,
Y = -21912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 304.0f,
Y = -21920.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 304.0f,
Y = -21920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 304.0f,
Y = -22040.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 304.0f,
Y = -22040.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -21984.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -21928.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -21928.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -80.0f,
Y = -21928.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 304.0f,
Y = -22168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 432.0f,
Y = -22168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 560.0f,
Y = -22168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 304.0f,
Y = -22168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -22112.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -22112.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -22768.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -22768.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -22816.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -22816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -22864.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -22864.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -1712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -1904.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -1784.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
