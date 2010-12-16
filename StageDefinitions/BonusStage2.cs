//
// BonusStage2.cs
//
namespace Galaxy {
namespace Stages {
public class BonusStage2 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("BonusStage2");
stage.ScrollSpeed = 3.0f;
stage.BackgroundSceneryName = "SimpleSpace";
stage.ForegroundSceneryName = "Empty";
stage.MusicName = "C";
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
X = 0.0f,
Y = -18912.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy