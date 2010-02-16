//
// EditorStage.cs
//
namespace Galaxy {
namespace Stages {
public class EditorStage {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("EditorStage");
stage.AddElement(0, 
new Galaxy.CStageElementAsteroidField() {
SpawnCount = 25,
SpawnTimer = new Galaxy.CSpawnTimerRandom() {
Frequency = 0.1f,
IncreaseRate = 0.0f,
},
Type = typeof(Galaxy.CAsteroid),
SpawnPosition = new Galaxy.CSpawnPositionRandom() {
BasePosition = new Microsoft.Xna.Framework.Vector2() {
X = 30.6f,
Y = -246.43f,
},
},
CustomMover = null,
CustomElement = new Galaxy.CSpawnerCustomAsteroid() {
},
Position = new Microsoft.Xna.Framework.Vector2() {
X = 30.6f,
Y = -246.43f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
