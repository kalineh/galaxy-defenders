//
// Stage5.cs
//
namespace Galaxy {
namespace Stages {
public class Stage5 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("Stage5");
stage.ScrollSpeed = 4;
stage.SceneryName = "BlueSky";
stage.MusicName = "Music/BonusStage1";
stage.AddElement(0, 
new Galaxy.CStageElementCameraStop() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -17416.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementStageFinish() {
World = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -20752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 314.66f,
Y = -217.1f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -160.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
