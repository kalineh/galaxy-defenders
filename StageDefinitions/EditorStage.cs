//
// EditorStage.cs
//
namespace Galaxy {
namespace Stages {
public class EditorStage {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("EditorStage");
stage.DisplayName = "";
stage.ScrollSpeed = 0.15f;
stage.BackgroundSceneryName = "MysteriousCloudsBG";
stage.ForegroundSceneryName = "MysteriousCloudsFG";
stage.MusicName = "The_Voyage";
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building9",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -40.0f,
Y = -352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.EnemyDeathExplosion,
Cycle = 1.0f,
Expired = false,
CycleFrame = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = 248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.EnemyDeathExplosion,
Cycle = 1.0f,
Expired = false,
CycleFrame = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = 248.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
