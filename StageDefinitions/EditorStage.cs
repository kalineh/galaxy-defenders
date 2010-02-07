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
Type = typeof(Galaxy.CAsteroid),
SpawnCount = 3,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 124.1f,
Y = -268.14f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 3,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 124.1f,
Y = -268.14f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
