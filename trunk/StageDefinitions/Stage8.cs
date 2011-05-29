//
// Stage8.cs
//
namespace Galaxy {
namespace Stages {
public class Stage8 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("Stage8");
stage.DisplayName = "MYSTERIOUS CLOUDS";
stage.ScrollSpeed = 2.5f;
stage.BackgroundSceneryName = "MysteriousCloudsBG";
stage.ForegroundSceneryName = "MysteriousCloudsFG";
stage.MusicName = "Troubled_Dreams";
stage.AddElement(0, 
new Galaxy.CStageElementCameraStop() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -19712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementStageFinish() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -20632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -792.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -792.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -1688.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -1688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -1688.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -1688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -2448.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -2448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -2448.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -2448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.0f,
Y = -3184.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.0f,
Y = -3184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -3184.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -3184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -10624.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -10624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -10624.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -10624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -10840.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -10840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -10840.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -10840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -11280.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -11280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 640.0f,
Y = -11272.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 640.0f,
Y = -11272.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -12128.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -12128.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -12128.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -12128.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -11480.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -11480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 640.0f,
Y = -11480.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 640.0f,
Y = -11480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -12856.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -12856.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -12856.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -12856.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -13248.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -13248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -13248.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -13248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -14232.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -14232.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 440.0f,
Y = -14232.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 440.0f,
Y = -14232.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -1464.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 6.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -1464.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -2336.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -2336.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -2280.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -2280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -2448.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -2448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -2392.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -2392.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3800.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3800.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3856.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3856.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3912.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -6712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -6712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -6840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -6840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -6968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -6968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CDoubleTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -6720.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -6720.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CDoubleTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -6720.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -6720.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissileTriple),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -6968.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -6968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissileTriple),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -6968.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -6968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -6672.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -6672.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -6672.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -6672.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -6840.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -6840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 512.0f,
Y = -6840.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 512.0f,
Y = -6840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -6992.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -6992.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -6992.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -6992.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = -7456.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = -7456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 528.0f,
Y = -7456.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 528.0f,
Y = -7456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -7456.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -7456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -7456.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -7456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -9328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -9328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -9824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
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
X = 240.0f,
Y = -9824.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
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
X = 328.0f,
Y = -9824.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = -9824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CSpike),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -432.0f,
Y = -9800.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -432.0f,
Y = -9800.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -1000.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -1000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 520.0f,
Y = -1008.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 520.0f,
Y = -1008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -3504.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -3504.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 448.0f,
Y = -3504.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 448.0f,
Y = -3504.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -6840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -400.0f,
Y = -6840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -1584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -1584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -392.0f,
Y = -1584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building7",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -1584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -1536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -4128.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -4096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -4008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4032.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -4128.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -7352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -7352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 304.0f,
Y = -7352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 432.0f,
Y = -7352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 560.0f,
Y = -7352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -496.0f,
Y = -10736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -368.0f,
Y = -10736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -240.0f,
Y = -10736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -10736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -10736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -10736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -10736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.0f,
Y = -10736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 512.0f,
Y = -10736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -10864.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -10992.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -11120.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -11248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -11376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.0f,
Y = -11376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 512.0f,
Y = -11376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -12240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -12240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -12240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -12368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -12496.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -12624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -12752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -9328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -9328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -9456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -9584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -9704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -9824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -2664.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -2536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -2408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -2280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -2152.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -2024.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -1896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -1896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -1896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -1408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -1280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -1152.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -1024.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.0f,
Y = -896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 512.0f,
Y = -896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -80.0f,
Y = -1584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -1584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -1584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -1768.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -1584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -1640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3504.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3120.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -2344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -2344.0f,
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
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -2552.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -2344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -2344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -4880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -4736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -464.0f,
Y = -1584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -592.0f,
Y = -1584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -2480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -2992.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -2864.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -2736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -2608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -504.0f,
Y = -3304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -376.0f,
Y = -3304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -600.0f,
Y = -3304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -4128.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -7352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -7352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -80.0f,
Y = -7352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -7096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -7224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -384.0f,
Y = -9328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -512.0f,
Y = -9328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -9824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -10856.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -296.0f,
Y = -11000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -11000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -2800.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -2736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -1472.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -1472.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -1472.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -1472.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 336.0f,
Y = -1792.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 336.0f,
Y = -1792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 616.0f,
Y = -1792.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 616.0f,
Y = -1792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 428.92f,
Y = -1892.72f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -1896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 504.0f,
Y = -1896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -2240.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -2240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -2240.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -2240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -4880.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -4880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3224.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3168.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3112.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3112.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3056.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3056.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -1408.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 6.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -1408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -3760.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -3760.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -3760.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -3760.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -9328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -9816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = -7472.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = -7664.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 464.0f,
Y = -9824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CDoubleTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.0f,
Y = -10736.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.0f,
Y = -10736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CDoubleTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -10736.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -10736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -10736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -10736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 448.0f,
Y = -10736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CDoubleTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -12240.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -12240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CDoubleTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -12240.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -12240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CRaidTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -11376.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -11376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -12752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 288.0f,
Y = -12752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 416.0f,
Y = -12752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -12752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -12752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -12880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -13008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -13136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -12640.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -12640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -80.0f,
Y = -12640.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -80.0f,
Y = -12640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -12752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -480.0f,
Y = -12752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -320.0f,
Y = -13136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -13136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -320.0f,
Y = -13136.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -320.0f,
Y = -13136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CRaidTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -13136.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -13136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -13136.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -13136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -13016.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -12752.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -12752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -12752.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -12752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CDownTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -12752.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -12752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -12752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -12752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -416.0f,
Y = -12752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -480.0f,
Y = -12752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -12240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -14344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -14344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 416.0f,
Y = -14344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CDoubleTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -14344.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -14344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -14344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 416.0f,
Y = -14344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -488.0f,
Y = -18640.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -488.0f,
Y = -18640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -176.0f,
Y = -18640.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -176.0f,
Y = -18640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -18648.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -18648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 512.0f,
Y = -18648.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 512.0f,
Y = -18648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -18920.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -18920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -18920.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -18920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSecretEntry() {
SecretStage = "BonusStage2",
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 496.0f,
Y = -11376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CAirship),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -5392.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 2.0f,
MoverTransitionMultiplier = 5.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -5392.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CAirship),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -5872.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 2.0f,
MoverTransitionMultiplier = 5.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 360.0f,
Y = -5872.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CAirship),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -9112.0f,
},
},
MoverPresetName = "DownWait",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 4.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -9112.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CAirship),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -12352.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 2.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -12352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CAirship),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -14024.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 2.0f,
MoverTransitionMultiplier = 4.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -14024.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -624.0f,
Y = -5600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -496.0f,
Y = -5600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -368.0f,
Y = -5600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -5600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementMusicChanger() {
MusicName = "Boss 9",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -616.0f,
Y = -19008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -472.0f,
Y = -1584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -4600.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -4600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -4592.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 496.0f,
Y = -4736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -2808.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -2808.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -2664.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3744.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3744.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3688.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -3688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -4056.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -5024.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -5168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -5168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -5168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -80.0f,
Y = -5168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -5168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -5312.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -5456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -5600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -5600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -456.0f,
Y = -5600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -544.0f,
Y = -5056.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -544.0f,
Y = -5056.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -5056.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -5056.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -7488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -7600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "SecretHint",
DepthOffset = 0.01f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 544.0f,
Y = -11376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = -7840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7744.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -7744.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -7888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -7888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -7888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -7888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -7888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8032.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8032.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -8176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 568.0f,
Y = -8176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8464.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8608.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -8000.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -8000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -8000.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -8000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CAirship),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8264.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 3.0f,
MoverTransitionMultiplier = 2.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -8264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CDoubleTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -7896.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -7896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -9016.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 2.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -9016.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -9040.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8968.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 2.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8912.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 2.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8864.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 2.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8864.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8816.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 2.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8768.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 5.0f,
MoverTransitionMultiplier = 2.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -8768.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -9184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -368.0f,
Y = -10736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -240.0f,
Y = -10736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissileTriple),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -11000.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -11000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -432.0f,
Y = -14576.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -432.0f,
Y = -14576.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -14576.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -14576.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -14688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -14688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -14688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CDoubleTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -14688.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -14688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -14688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -14688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -14872.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -14872.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 440.0f,
Y = -14872.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 440.0f,
Y = -14872.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -14984.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -14984.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 416.0f,
Y = -14984.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CDoubleTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -14984.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -14984.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -14984.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 416.0f,
Y = -14984.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -432.0f,
Y = -15192.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -432.0f,
Y = -15192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -15192.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -15192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -15304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -15304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -15304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CDoubleTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -15304.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 4.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -15304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -15304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -15304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CAirship),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -15336.0f,
},
},
MoverPresetName = "Down",
MoverSpeedMultiplier = 2.0f,
MoverTransitionMultiplier = 4.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -15336.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -16176.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -16176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -16184.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -16184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -16072.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -16072.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -16072.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.0f,
Y = -16072.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -16072.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -16200.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -16328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CDoubleTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -16072.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -16072.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -56.0f,
Y = -16072.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -16072.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "SmallBuilding4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -16064.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -16072.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -488.0f,
Y = -16072.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 600.0f,
Y = -16072.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 456.0f,
Y = -16072.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CDoubleTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -16064.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -16064.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -16328.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -16328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -17728.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -18016.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -17728.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -17704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -17728.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -17872.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -18016.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -400.0f,
Y = -18016.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -544.0f,
Y = -18016.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -17728.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissileStorm),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -17744.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -17744.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -17776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = -17776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = -17704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -16472.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -16616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -16688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -16688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -16784.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
DepthOffset = 0.0f,
Rotation = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -16784.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CMissileStorm),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -16744.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -16744.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CRaidTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -17728.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -17728.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -17624.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -17624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CFence),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -17624.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -17624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CBoss8),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -19320.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -19320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementHealthBar() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -19416.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
