//
// Stage8.cs
//
namespace Galaxy {
namespace Stages {
public class Stage8 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("Stage8");
stage.ScrollSpeed = 3.0f;
stage.BackgroundSceneryName = "SimpleSpace";
stage.ForegroundSceneryName = "Empty";
stage.MusicName = "B";
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
X = -16.0f,
Y = -19448.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
