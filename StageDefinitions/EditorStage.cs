//
// EditorStage.cs
//
namespace Galaxy {
namespace Stages {
public class EditorStage {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("EditorStage");
stage.AddElement(4, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CSinBall),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 0.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverSequence() {
Velocity = new System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2>() {
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 5.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -5.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
1.0f,
0.0f,
},
VelocityLerpRate = 0.02f,
AlwaysMaxSpeed = false,
},
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
