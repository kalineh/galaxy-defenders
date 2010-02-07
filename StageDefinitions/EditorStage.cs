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
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 536.0f,
Y = 109.98f,
},
},
CustomMover = new Galaxy.CMoverSequence() {
Velocity = new System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2>() {
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 1.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
1.0f,
0.0f,
},
VelocityLerpRate = 0.1f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 1.0f,
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 536.0f,
Y = 109.98f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CAsteroid),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = 83.61f,
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
X = 400.0f,
Y = 83.61f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CAsteroid),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 231.0f,
Y = 73.24f,
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
X = 231.0f,
Y = 73.24f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CAsteroid),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 193.0f,
Y = 12.03f,
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
X = 193.0f,
Y = 12.03f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
