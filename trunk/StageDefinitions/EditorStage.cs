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
Type = typeof(Galaxy.CSpacePlatform),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -494.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -494.0f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -245.0f,
Y = -493.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -245.0f,
Y = -493.0f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CSpacePlatform),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -487.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -360.0f,
Y = -487.0f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CSpacePlatform),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -358.0f,
Y = -594.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -358.0f,
Y = -594.0f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CSpacePlatform),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -601.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -601.0f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CBigSpacePlatform),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 322.35f,
Y = -808.91f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 322.35f,
Y = -808.91f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 261.41f,
Y = -802.66f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 261.41f,
Y = -802.66f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CBuilding),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 342.66f,
Y = -801.1f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 342.66f,
Y = -801.1f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CBigSpacePlatform),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -301.73f,
Y = -1079.57f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -301.73f,
Y = -1079.57f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CBuilding),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -349.35f,
Y = -1111.95f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -349.35f,
Y = -1111.95f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CSinBall),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.39f,
Y = -1285.28f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.39f,
Y = -1285.28f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CSinBall),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 22.08f,
Y = -1224.33f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 22.08f,
Y = -1224.33f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CSinBall),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 94.46f,
Y = -1165.28f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 94.46f,
Y = -1165.28f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CPewPew),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -307.44f,
Y = -1643.38f,
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
VelocityLerpRate = 1.0f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 2.0f,
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -307.44f,
Y = -1643.38f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CPewPew),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -307.44f,
Y = -1574.81f,
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
VelocityLerpRate = 1.0f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 2.0f,
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -307.44f,
Y = -1574.81f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CSinBall),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 440.63f,
Y = -238.41f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 440.63f,
Y = -238.41f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
