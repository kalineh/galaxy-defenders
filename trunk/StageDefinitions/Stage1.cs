//
// Stage1.cs
//
namespace Galaxy {
namespace Stages {
public class Stage1 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("Stage1");
stage.DisplayName = "FLYING OVER CARRIER";
stage.ScrollSpeed = 2.5f;
stage.BackgroundSceneryName = "FlyingOverCarrierBG";
stage.ForegroundSceneryName = "FlyingOverCarrierFG";
stage.MusicName = "The_Voyage";
stage.AddElement(0, 
new Galaxy.CStageElementCameraStop() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -18768.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementStageFinish() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -21320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -656.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 336.0f,
Y = -656.0f,
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
Y = -17984.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -17984.0f,
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
Y = -17992.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -17992.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -17984.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -17992.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -17992.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -17984.0f,
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
Y = -18152.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -18152.0f,
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
Y = -18160.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -18160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -18152.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -18160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -18160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -18152.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -18376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -18376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -18376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -18512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -18512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -18512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -18376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -18376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -18376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -18512.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -18512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -18512.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -18512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -18512.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -18512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 528.0f,
Y = -17992.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 528.0f,
Y = -18160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -488.0f,
Y = -18152.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -488.0f,
Y = -17984.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -656.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementMusicChanger() {
MusicName = "Boss 9",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -18144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -1584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -1456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -1328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue8",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -1208.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -1088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -1088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -1088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 496.0f,
Y = -1088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -1016.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -656.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -1512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -1376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -1512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -1376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -1512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -1384.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -1512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -1344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -1088.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -1088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -2360.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -2360.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -2232.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -2288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -2368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -2368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -2232.0f,
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
Y = -2232.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -2232.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -2288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -2264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -2136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue3",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -2264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -2392.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -400.0f,
Y = -2288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -400.0f,
Y = -2088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -2520.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -2648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -400.0f,
Y = -2288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -2264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -400.0f,
Y = -2088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -2136.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -2136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -2240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -2776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -2776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -2904.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -3032.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -3032.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -3160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -3288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -3288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -3288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 496.0f,
Y = -3288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -2776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -2776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -3032.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -3032.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -3032.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -3288.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -3288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -3288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -3288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -3288.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -3288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -5480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -5480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -5672.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -5544.0f,
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
Y = -5480.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -5480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -5480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 520.0f,
Y = -5480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -464.0f,
Y = -7192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -464.0f,
Y = -7192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -7192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -7192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -7448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -7192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -7192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -7192.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -7192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -6216.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -472.0f,
Y = -6216.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -6216.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -6216.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -6216.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 480.0f,
Y = -6216.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -6216.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -6216.0f,
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
Y = -6640.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -6640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -6680.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -6680.0f,
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
Y = -6680.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -6680.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue8",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -80.0f,
Y = -7192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -7448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -7448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -7192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 304.0f,
Y = -7192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 560.0f,
Y = -7192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 432.0f,
Y = -7192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -7448.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -7448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -8712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -8712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8200.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8200.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -8712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -8712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -8712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue7",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -8712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 408.0f,
Y = -8712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8712.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8712.0f,
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
Y = -8712.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -8712.0f,
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
Y = -8712.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -8712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -8712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 536.0f,
Y = -8712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 408.0f,
Y = -8456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 536.0f,
Y = -8456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -8240.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -8240.0f,
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
Y = -8184.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -8184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -328.0f,
Y = -8296.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -328.0f,
Y = -8296.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue8",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -9752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue8",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -10008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -9624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -9624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -9368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -9880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -9880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue8",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -9752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -9624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -9624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -9880.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -9880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -9368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -11320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -11064.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -11192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -11192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -11448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -10936.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue8",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = -11192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue8",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = -11448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -11448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -11320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -11192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -11192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -10936.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -10936.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -11448.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -11448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -11448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -13664.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -13536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -13408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -13408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -13280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue7",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -13280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -472.0f,
Y = -13280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -13280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -13152.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -13024.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -13024.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -13280.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -13280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 520.0f,
Y = -14896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -14896.0f,
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
Y = -14896.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -14896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue7",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -15472.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -15344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -15600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -15344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -15600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -15600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -15600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -15344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -15344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -15344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -15344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -15344.0f,
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
Y = -15600.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -15600.0f,
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
Y = -15600.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -15600.0f,
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
Y = -15600.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -15600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -15600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -17008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -16496.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -16880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -16880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -17264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -17136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -17136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -17008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -16880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -16880.0f,
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
Y = -17264.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -17264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -9880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -9496.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -9624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -9880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 296.0f,
Y = -9880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -10136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -9880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -10136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -9880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -9624.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -9624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -9880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -10936.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -11448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -11448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -11448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -11448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -11576.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -11704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -11832.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -11448.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -11448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -10936.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -11960.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -11576.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -11704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -11832.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -11960.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -12088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -12088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -12088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -13024.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -13024.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -13280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -13024.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -14768.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -14640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -14640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -16352.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -16352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -16392.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -16392.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -15344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -15728.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -15856.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -15984.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -16112.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -16240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -16184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -16184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -16184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -16176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -16176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -16256.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -16368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -16368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -16360.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -16360.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -16368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -16432.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -16432.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -16472.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -16472.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -16624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -16752.0f,
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
Y = -17144.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -17144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -14048.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -14056.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -14056.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -13920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -13792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -15344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -16368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -16184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -17136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue2",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -17136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -17264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -17264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -16880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -17248.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -17248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -17328.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -17328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -17288.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -17288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -1392.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -1584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 408.0f,
Y = -656.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -1464.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -5672.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -10488.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 4.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -10488.0f,
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
Y = -10488.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 4.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -10488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -17368.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -17368.0f,
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
Y = -14616.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -14616.0f,
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
Y = -14656.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -14656.0f,
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
Y = -14696.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -14696.0f,
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
Y = -14736.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -14736.0f,
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
Y = -4944.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4944.0f,
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
Y = -4984.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4984.0f,
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
Y = -5024.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -5024.0f,
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
Y = -5064.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -5064.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -4592.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -4592.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -4632.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -4632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -4672.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -4672.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -4712.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -4712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -4192.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -4192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -4232.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
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
X = -280.0f,
Y = -4272.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -4272.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -4312.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -4312.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -3808.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -3808.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -3848.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -3848.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -3888.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -3888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -3928.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -3928.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -12096.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -12096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue3",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -2120.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue8",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -80.0f,
Y = -7448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue8",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -7320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue8",
DepthOffset = -0.01f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -7320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -6640.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -6640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.0f,
Y = -13488.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.0f,
Y = -13488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.0f,
Y = -13528.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.0f,
Y = -13528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.0f,
Y = -13568.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.0f,
Y = -13568.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.0f,
Y = -13608.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.0f,
Y = -13608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 3,
Powerup = false,
Type = typeof(Galaxy.CBigBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -12744.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 4.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -12744.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 3,
Powerup = false,
Type = typeof(Galaxy.CBigBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -12744.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 4.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -12744.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 3,
Powerup = false,
Type = typeof(Galaxy.CBigBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -10488.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -10488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 3,
Powerup = false,
Type = typeof(Galaxy.CBigBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -6632.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 4.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -6632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 3,
Powerup = false,
Type = typeof(Galaxy.CBigBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -16304.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -16304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -3968.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -3968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -4352.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -4352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -4752.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -4752.0f,
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
Y = -5104.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -5104.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 3,
Powerup = false,
Type = typeof(Galaxy.CBigBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -17200.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -17200.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -18376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -18512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -18376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -18512.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -18512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 3,
Powerup = false,
Type = typeof(Galaxy.CBigBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -14728.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 4.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -14728.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
