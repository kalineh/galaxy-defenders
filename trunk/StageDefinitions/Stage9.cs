//
// Stage9.cs
//
namespace Galaxy {
namespace Stages {
public class Stage9 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("Stage9");
stage.ScrollSpeed = 3.0f;
    stage.DisplayName = "RELENT LESS";
stage.BackgroundSceneryName = "RelentLessBG";
stage.ForegroundSceneryName = "RelentLessFG";
stage.MusicName = "A";
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
