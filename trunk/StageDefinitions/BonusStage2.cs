//
// BonusStage2.cs
//
namespace Galaxy {
namespace Stages {
public class BonusStage2 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("BonusStage2");
stage.DisplayName = "";
stage.ScrollSpeed = 2.5f;
stage.BackgroundSceneryName = "SimpleSpace";
stage.ForegroundSceneryName = "Empty";
stage.MusicName = "The_Hidden_Answer";
stage.AddElement(0, 
new Galaxy.CStageElementCameraStop() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -12096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementStageFinish() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -13424.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
