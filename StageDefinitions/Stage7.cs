//
// Stage7.cs
//
namespace Galaxy {
namespace Stages {
public class Stage7 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("Stage7");
stage.DisplayName = "TEMPEST NEBULA";
stage.ScrollSpeed = 2.5f;
stage.BackgroundSceneryName = "TempestNebulaBG";
stage.ForegroundSceneryName = "TempestNebulaFG";
stage.MusicName = "Insurrection";
stage.AddElement(0, 
new Galaxy.CStageElementCameraStop() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -17792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementStageFinish() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -18896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -1240.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -1240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 319.0f,
Y = -715.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -1000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building6",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -720.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -1240.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -1240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -1120.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -1240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissileTriple),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -2400.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -2656.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissileTriple),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -2400.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -2528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -2656.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 528.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -3832.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -3688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -3688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -135.29f,
Y = -4468.45f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -3528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -3832.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -3688.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -3688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -3688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -3832.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -3688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -3528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -3528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -456.0f,
Y = -4520.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -135.35f,
Y = -4610.78f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -456.0f,
Y = -4520.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -456.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -456.0f,
Y = -4776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -40.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building6",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 88.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 344.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 472.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -4624.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -4624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -4672.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -4672.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 72.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -6384.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.0f,
Y = -6256.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -5968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -6096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -6256.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -6256.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -6256.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -6096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -5840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -5840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7432.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7560.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7432.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7304.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7560.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7560.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7816.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.0f,
Y = -9824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -9632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -9504.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.0f,
Y = -9424.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.0f,
Y = -9824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissileTriple),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.0f,
Y = -9632.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.0f,
Y = -9632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.0f,
Y = -9664.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -9624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -9632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.0f,
Y = -9424.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -9368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -9368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 296.0f,
Y = -9368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -9368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 552.0f,
Y = -9368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissileTriple),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -9632.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -9632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -9624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -9632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -9632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -536.0f,
Y = -9632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -11504.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -11504.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -11808.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -11632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -11576.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -11504.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -11808.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -11760.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -11760.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -11632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -11760.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -11632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -11456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -11456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CBlackHole),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -10768.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -10768.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -10816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -472.0f,
Y = -10816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building6",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -10824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -12592.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 528.0f,
Y = -12592.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -12592.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -13888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = -13728.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -13640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -13888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CRaidTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -13728.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -13728.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.49f,
Y = -13764.99f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -13728.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -13728.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -13512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -13512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 93.22f,
Y = -13605.01f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -13568.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.0f,
Y = -13568.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissileStorm),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -13728.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -13728.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -13568.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSpike),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -472.0f,
Y = -1888.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -472.0f,
Y = -1888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -3488.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 4.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -3488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -3304.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -3304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -5624.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -5624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -5440.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -5440.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -456.0f,
Y = -7304.0f,
},
},
MoverPresetName = "Right",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -456.0f,
Y = -7304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 448.0f,
Y = -7384.0f,
},
},
MoverPresetName = "Left",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 448.0f,
Y = -7384.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 416.0f,
Y = -10920.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 3.5f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 416.0f,
Y = -10920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -12824.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -12824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -12776.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -12776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -12872.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -12872.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -12728.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -12728.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -8496.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -8496.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -8496.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -8496.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -14616.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.5f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -14616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -14840.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.5f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -14840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -3688.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -3688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -3832.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -3528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -1240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -1240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = -0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -1240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -1240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -1240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CRotateTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -1000.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -1000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementMusicChanger() {
MusicName = "Boss 9",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 680.0f,
Y = -17320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSpike),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -2248.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -2248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSpike),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 616.0f,
Y = -1936.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 616.0f,
Y = -1936.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4648.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4560.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4560.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4480.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CRotateTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -3688.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -3688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -456.0f,
Y = -4776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissileStorm),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -6256.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -6256.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -7688.0f,
},
},
MoverPresetName = "Right",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -7688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -7040.0f,
},
},
MoverPresetName = "Left",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -7040.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CBigBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -8688.0f,
},
},
MoverPresetName = "DownWaitDown",
MoverSpeedMultiplier = 6.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -8688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CBigBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -8832.0f,
},
},
MoverPresetName = "DownWaitDown",
MoverSpeedMultiplier = 6.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -8832.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CRotateTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -6384.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -6384.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -10984.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 3.5f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -10984.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSpike),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -9792.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -9792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSpike),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -9616.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -9616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -11144.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -11144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -11200.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -11200.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -11256.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -11256.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -13048.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -13048.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -13000.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -13000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -13096.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -13096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -12952.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -12952.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSpike),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -12760.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -12760.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSpike),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 560.0f,
Y = -13000.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 560.0f,
Y = -13000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -14616.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.5f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -14616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = -14840.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.5f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = -14840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -15008.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 3.5f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -15008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CGlob),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 344.0f,
Y = -15368.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 4.5f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 344.0f,
Y = -15368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CGlob),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -15432.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 4.5f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -15432.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -504.0f,
Y = -14536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -504.0f,
Y = -14712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 504.0f,
Y = -15200.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -14712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -14536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -15200.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -15024.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 504.0f,
Y = -15024.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CGlob),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -15632.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 4.5f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -15632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CGlob),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -15632.0f,
},
},
MoverPresetName = "DownWaitUp",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 4.5f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -15632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -16296.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -44.91f,
Y = -16131.37f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -16304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CRaidTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 19.09f,
Y = -16132.14f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 19.09f,
Y = -16132.14f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -16168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 83.09f,
Y = -16131.37f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.0f,
Y = -16168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass4",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.0f,
Y = -16296.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.0f,
Y = -16296.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -16424.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.0f,
Y = -16424.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.0f,
Y = -16520.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -16520.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissileTriple),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -16520.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -16520.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissileTriple),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.0f,
Y = -16520.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.0f,
Y = -16520.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.0f,
Y = -9584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -11720.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -7816.0f,
},
},
MoverPresetName = "Left",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -7816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementHealthBar() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 448.0f,
Y = -17840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CBoss7),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -17536.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -17536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -504.0f,
Y = -17544.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -400.0f,
Y = -17544.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 90.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 520.0f,
Y = -17544.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 416.0f,
Y = -17544.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CCannon),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 416.0f,
Y = -17536.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 416.0f,
Y = -17536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CCannon),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -400.0f,
Y = -17536.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -400.0f,
Y = -17536.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
