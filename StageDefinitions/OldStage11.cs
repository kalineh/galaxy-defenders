//
// Stage11.cs
//
namespace Galaxy {
namespace Stages {
public class OldStage11 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("Stage11");
stage.ScrollSpeed = 3.0f;
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
X = 0.0f,
Y = -18864.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
