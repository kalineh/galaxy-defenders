//
// EditorStage.cs
//
namespace Galaxy {
namespace Stages {
public class EditorStage {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("EditorStage");
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -614.65f,
Y = 105.6f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -622.25f,
Y = -133.09f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building3",
HealthMax = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -774.82f,
Y = -0.98f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -788.76f,
Y = -264.68f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -783.9f,
Y = -402.35f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
HealthMax = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -631.49f,
Y = 2.31f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
HealthMax = 0.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -775.43f,
Y = -131.55f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -604.11f,
Y = -273.55f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform6",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -869.01f,
Y = -658.12f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
