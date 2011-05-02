//
// BonusStage3.cs
//
namespace Galaxy {
namespace Stages {
public class BonusStage3 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("BonusStage3");
stage.DisplayName = "";
stage.ScrollSpeed = 3.0f;
stage.BackgroundSceneryName = "SimpleSpace";
stage.ForegroundSceneryName = "Empty";
stage.MusicName = "The_Hidden_Answer";
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
