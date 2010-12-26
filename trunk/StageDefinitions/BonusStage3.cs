//
// BonusStage3.cs
//
namespace Galaxy {
namespace Stages {
public class BonusStage3 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("BonusStage3");
stage.ScrollSpeed = 2.5f;
stage.BackgroundSceneryName = "SimpleSpace";
stage.ForegroundSceneryName = "Empty";
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
X = -24.0f,
Y = -18768.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
