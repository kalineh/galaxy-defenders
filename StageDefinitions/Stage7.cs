//
// Stage7.cs
//
namespace Galaxy {
namespace Stages {
public class Stage7 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("Stage7");
stage.DisplayName = "TEMPEST NEBULA";
stage.ScrollSpeed = 3;
stage.BackgroundSceneryName = "TempestNebulaBG";
stage.ForegroundSceneryName = "TempestNebulaFG";
stage.MusicName = "Insurrection";
stage.AddElement(0, 
new Galaxy.CStageElementCameraStop() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -17584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementStageFinish() {
World = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -18896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CGlob),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -536.0f,
Y = -17088.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -536.0f,
Y = -17088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -384.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -512.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -640.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 496.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 624.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CGlob),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 528.0f,
Y = -17112.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 528.0f,
Y = -17112.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CGlob),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -544.0f,
Y = -17288.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -544.0f,
Y = -17288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CGlob),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 552.0f,
Y = -17256.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 552.0f,
Y = -17256.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -416.0f,
Y = -17176.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -416.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -17176.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -17176.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -17176.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 88.0f,
Y = -17176.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 88.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -17176.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 344.0f,
Y = -17176.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 344.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -17176.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building6",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building6",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building6",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building6",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -512.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building6",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 496.0f,
Y = -17176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = -17008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -17008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -17048.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = -17008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -17008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -17048.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building8",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -17008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building8",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -17008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building8",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -17008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building8",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -17008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building8",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -17008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building8",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -17008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -17072.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -17072.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -17072.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -17072.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -17328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -296.0f,
Y = -17328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -17288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -17328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -17328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Block3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -17288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -17328.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -17328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -17328.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -17328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -17328.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -17328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -17328.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -17328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -328.0f,
Y = -17328.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -328.0f,
Y = -17328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.0f,
Y = -17328.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.0f,
Y = -17328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CGlob),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 544.0f,
Y = -16952.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 544.0f,
Y = -16952.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CGlob),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -552.0f,
Y = -16896.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -552.0f,
Y = -16896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -912.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBlackHole),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -1800.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -1800.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -656.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -912.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -784.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
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
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -2656.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -2400.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -2528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -2656.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 528.0f,
Y = -2400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 464.0f,
Y = -2400.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 464.0f,
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
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -3816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -3688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -3688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -3560.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -3816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -3688.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -3688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -3688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -3816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -3688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -3560.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -3560.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -456.0f,
Y = -4520.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -456.0f,
Y = -4520.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CRaidTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -456.0f,
Y = -4776.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -456.0f,
Y = -4776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -456.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -456.0f,
Y = -4776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -40.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4360.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4360.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 88.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 344.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 472.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = -4648.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 344.0f,
Y = -4648.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 344.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 432.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -4648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -6608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -240.0f,
Y = -6480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -6224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -6352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -6608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CRaidTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -6480.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -6480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -6480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -6480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -6480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -6352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -6096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -6096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBlackHole),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -5488.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -5488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBlackHole),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -8504.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -8504.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBlackHole),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -8496.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -8496.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8592.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8208.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8336.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8592.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8464.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8336.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8080.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8080.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8848.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8848.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8720.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8208.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8208.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8472.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8472.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8720.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -8720.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = -10232.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -10104.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -9976.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -9976.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = -10232.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.0f,
Y = -10104.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.0f,
Y = -10104.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -10104.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -10104.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -10104.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -9976.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -9848.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -9848.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 296.0f,
Y = -9848.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -9848.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 552.0f,
Y = -9848.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -10104.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -10104.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -10104.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -9848.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -9848.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -9848.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -9848.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -10104.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -384.0f,
Y = -10104.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -512.0f,
Y = -10104.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -11848.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -11848.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -12096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -40.0f,
Y = -11968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -11968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -11840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -12096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -12104.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -12104.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -11968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -12096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = -11968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -11840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -11840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBlackHole),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -11104.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -11104.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -11152.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -504.0f,
Y = -11152.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -11152.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -12928.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 496.0f,
Y = -12928.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -12928.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -296.0f,
Y = -15024.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -192.0f,
Y = -14864.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -14776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -296.0f,
Y = -15024.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -14864.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -14864.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -14896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -14864.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -14864.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -14648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -14648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBlackHole),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -13648.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -13648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBlackHole),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = -15776.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = -15776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass3",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -14736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 288.0f,
Y = -14696.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 288.0f,
Y = -14696.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -14704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -14864.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -14864.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -14704.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -14704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -14864.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -1760.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 376.0f,
Y = -1760.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -3832.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -3832.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -3648.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -3648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
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
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
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
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -7632.0f,
},
},
MoverPresetName = "Right",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -7632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 456.0f,
Y = -7528.0f,
},
},
MoverPresetName = "Left",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 456.0f,
Y = -7528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 432.0f,
Y = -11216.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 432.0f,
Y = -11216.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -392.0f,
Y = -11256.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -392.0f,
Y = -11256.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -13200.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -13200.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -13128.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -13128.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -13272.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -13272.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -13056.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -13056.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -9056.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -9056.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -9056.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -9056.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 304.0f,
Y = -14224.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 304.0f,
Y = -14224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CSwirl),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 432.0f,
Y = -14080.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 432.0f,
Y = -14080.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -6480.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -6480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -6480.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -6480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -3688.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -3688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -3816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -3560.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass2",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CRotateTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -656.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -656.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy