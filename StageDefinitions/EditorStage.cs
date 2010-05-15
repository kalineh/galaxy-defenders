//
// EditorStage.cs
//
namespace Galaxy {
namespace Stages {
public class EditorStage {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("EditorStage");
stage.ScrollSpeed = 2.5f;
stage.SceneryName = "Blend";
stage.MusicName = "Music/Stage1";
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -976.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
