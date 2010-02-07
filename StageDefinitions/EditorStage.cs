//
// EditorStage.cs
//
namespace Galaxy {
namespace Stages {
public class EditorStage {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("EditorStage");
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CPewPew),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 0.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 251.0f,
Y = 119.32f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 251.0f,
Y = 119.32f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
