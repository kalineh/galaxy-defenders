//
// Stage4.cs
//
namespace Galaxy {
namespace Stages {
public class Stage4 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("Stage4");
stage.ScrollSpeed = 4;
stage.SceneryName = "DullGreen";
stage.MusicName = "Music/Stage1";
stage.AddElement(0, 
new Galaxy.CStageElementCameraStop() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -16280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementStageFinish() {
World = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 10.95f,
Y = -17356.88f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
