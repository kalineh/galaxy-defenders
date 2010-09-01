//
// EditorStage.cs
//
namespace Galaxy {
namespace Stages {
public class EditorStage {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("EditorStage");
stage.ScrollSpeed = 3.0f;
stage.BackgroundSceneryName = "BlueSky";
stage.ForegroundSceneryName = "Empty";
stage.MusicName = "A";
stage.AddElement(0, 
new Galaxy.CStageElementStageFinish() {
World = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -4288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementCameraStop() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -40.0f,
Y = -3224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -3128.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -3128.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "DarkBlue1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -216.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
