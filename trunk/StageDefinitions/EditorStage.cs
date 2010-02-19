//
// EditorStage.cs
//
namespace Galaxy {
namespace Stages {
public class EditorStage {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("EditorStage");
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "SpacePlatform",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 74.0f,
Y = 291.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
