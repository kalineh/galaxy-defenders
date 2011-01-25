//
// EditorStage.cs
//
namespace Galaxy {
namespace Stages {
public class EditorStage {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("EditorStage");
stage.DisplayName = "";
stage.ScrollSpeed = 0.0f;
stage.BackgroundSceneryName = "MysteriousCloudsBG";
stage.ForegroundSceneryName = "MysteriousCloudsFG";
stage.MusicName = "The_Voyage";
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CGlob),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -391.3f,
Y = -423.91f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -391.3f,
Y = -423.91f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CGlob),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.78f,
Y = -265.22f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.78f,
Y = -265.22f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CGlob),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 141.3f,
Y = -413.04f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 141.3f,
Y = -413.04f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CGlob),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 367.39f,
Y = -243.48f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 367.39f,
Y = -243.48f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
NoDropCoins = false,
Powerup = false,
Type = typeof(Galaxy.CGlob),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 471.74f,
Y = -393.48f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 471.74f,
Y = -393.48f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
