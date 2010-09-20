//
// Stage12.cs
//
namespace Galaxy {
namespace Stages {
public class Stage12 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("Stage12");
stage.ScrollSpeed = 3.0f;
    stage.DisplayName = "LAMINATION X";
stage.BackgroundSceneryName = "LaminationXBG";
stage.ForegroundSceneryName = "LaminationXFG";
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
X = -24.0f,
Y = -18928.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
