//
// Stage8.cs
//
namespace Galaxy {
namespace Stages {
public class OldStage8 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("Stage8");
stage.ScrollSpeed = 3.0f;
stage.BackgroundSceneryName = "OceanCloudsBG";
stage.ForegroundSceneryName = "OceanCloudsFG";
stage.MusicName = "fighting_for_control";
stage.AddElement(0, 
new Galaxy.CStageElementCameraStop() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -17584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementStageFinish() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -18896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "GreyBlock1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -1152.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "GreyBlock1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -1152.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "GreyBlock1",
DepthOffset = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1152.0f,
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
Y = -1152.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1152.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
