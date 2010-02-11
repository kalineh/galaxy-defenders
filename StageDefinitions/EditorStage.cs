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
X = 324.24f,
Y = -807.02f,
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
X = 324.24f,
Y = -807.02f,
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
X = -52.36f,
Y = -1734.29f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -52.36f,
Y = -1734.29f,
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
X = 14.13f,
Y = -1702.48f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 14.13f,
Y = -1702.48f,
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
X = 81.78f,
Y = -1668.43f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 81.78f,
Y = -1668.43f,
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
X = 440.98f,
Y = -4623.37f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 440.98f,
Y = -4623.37f,
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
X = -433.23f,
Y = -4620.79f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -433.23f,
Y = -4620.79f,
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
X = 468.29f,
Y = -1233.11f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 468.29f,
Y = -1233.11f,
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
X = -451.68f,
Y = -1319.79f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -451.68f,
Y = -1319.79f,
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
X = 461.66f,
Y = -1397.56f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 461.66f,
Y = -1397.56f,
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
X = 240.78f,
Y = -2350.46f,
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
X = 240.78f,
Y = -2350.46f,
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
X = 316.28f,
Y = -2338.54f,
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
X = 316.28f,
Y = -2338.54f,
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
X = -138.03f,
Y = -2240.52f,
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
X = -138.03f,
Y = -2240.52f,
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
X = 382.5f,
Y = -1922.64f,
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
X = 382.5f,
Y = -1922.64f,
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
X = 342.77f,
Y = -1943.84f,
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
X = 342.77f,
Y = -1943.84f,
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
X = -196.3f,
Y = -2293.5f,
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
X = -196.3f,
Y = -2293.5f,
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
X = -183.06f,
Y = -2221.98f,
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
X = -183.06f,
Y = -2221.98f,
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
X = -116.83f,
Y = -2220.66f,
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
X = -116.83f,
Y = -2220.66f,
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
X = -208.68f,
Y = -1647.18f,
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
X = -208.68f,
Y = -1647.18f,
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
X = -99.06f,
Y = -1747.61f,
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
X = -99.06f,
Y = -1747.61f,
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
X = -209.6f,
Y = -1640.56f,
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
X = -209.6f,
Y = -1640.56f,
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
X = -99.49f,
Y = -1748.82f,
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
X = -99.49f,
Y = -1748.82f,
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
X = 344.72f,
Y = -2657.83f,
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
X = 344.72f,
Y = -2657.83f,
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
X = 105.69f,
Y = -2656.2f,
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
X = 105.69f,
Y = -2656.2f,
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
X = -136.58f,
Y = -2656.2f,
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
X = -136.58f,
Y = -2656.2f,
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
X = -339.84f,
Y = -2661.08f,
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
X = -339.84f,
Y = -2661.08f,
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
X = 347.97f,
Y = -2659.45f,
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
X = 347.97f,
Y = -2659.45f,
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
X = 108.94f,
Y = -2657.83f,
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
X = 108.94f,
Y = -2657.83f,
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
X = -134.96f,
Y = -2661.08f,
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
X = -134.96f,
Y = -2661.08f,
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
X = -338.21f,
Y = -2664.33f,
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
X = -338.21f,
Y = -2664.33f,
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
X = 215.99f,
Y = -2888.34f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 215.99f,
Y = -2888.34f,
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
X = -14.7f,
Y = -2883.46f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -14.7f,
Y = -2883.46f,
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
X = -247.15f,
Y = -2888.75f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -247.15f,
Y = -2888.75f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CBeard),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.74f,
Y = -3256.24f,
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
X = -48.74f,
Y = -3256.24f,
},
});
stage.AddElement(0, 
new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CBeard),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Interval = 1.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 13.09f,
Y = -3256.16f,
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
X = 13.09f,
Y = -3256.16f,
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
X = -362.6f,
Y = -3226.93f,
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
X = -362.6f,
Y = -3226.93f,
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
X = -349.59f,
Y = -3222.05f,
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
X = -349.59f,
Y = -3222.05f,
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
X = 157.72f,
Y = -3413.92f,
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
X = 157.72f,
Y = -3413.92f,
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
X = 100.81f,
Y = -3461.08f,
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
X = 100.81f,
Y = -3461.08f,
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
X = 102.44f,
Y = -3461.08f,
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
X = 102.44f,
Y = -3461.08f,
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
X = 125.2f,
Y = -3438.31f,
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
X = 125.2f,
Y = -3438.31f,
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
X = 147.97f,
Y = -3461.08f,
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
X = 147.97f,
Y = -3461.08f,
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
X = 105.69f,
Y = -3420.43f,
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
X = 105.69f,
Y = -3420.43f,
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
X = 147.97f,
Y = -3422.05f,
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
X = 147.97f,
Y = -3422.05f,
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
X = -224.39f,
Y = -3781.4f,
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
X = -224.39f,
Y = -3781.4f,
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
X = -154.47f,
Y = -3708.23f,
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
X = -154.47f,
Y = -3708.23f,
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
X = 437.4f,
Y = -3968.39f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 5.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 437.4f,
Y = -3968.39f,
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
X = 439.03f,
Y = -4117.99f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 5.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 439.03f,
Y = -4117.99f,
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
X = -439.02f,
Y = -4026.93f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 5.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -439.02f,
Y = -4026.93f,
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
X = -439.02f,
Y = -4187.91f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -439.02f,
Y = -4187.91f,
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
X = -198.37f,
Y = -4226.93f,
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
X = -198.37f,
Y = -4226.93f,
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
X = 289.43f,
Y = -4048.07f,
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
X = 289.43f,
Y = -4048.07f,
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
X = -243.9f,
Y = -4265.96f,
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
X = -243.9f,
Y = -4265.96f,
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
X = 276.42f,
Y = -4061.08f,
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
X = 276.42f,
Y = -4061.08f,
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
X = -120.05f,
Y = -4873.32f,
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
X = -120.05f,
Y = -4873.32f,
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
X = 122.32f,
Y = -4883.49f,
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
X = 122.32f,
Y = -4883.49f,
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
X = 3.68f,
Y = -4727.56f,
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
X = 3.68f,
Y = -4727.56f,
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
X = 5.37f,
Y = -4730.95f,
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
X = 5.37f,
Y = -4730.95f,
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
X = -121.75f,
Y = -4878.41f,
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
X = -121.75f,
Y = -4878.41f,
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
X = 122.32f,
Y = -4888.58f,
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
X = 122.32f,
Y = -4888.58f,
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
X = -292.41f,
Y = -5455.51f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -292.41f,
Y = -5455.51f,
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
X = -251.42f,
Y = -5371.91f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -251.42f,
Y = -5371.91f,
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
X = -177.65f,
Y = -5304.69f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -177.65f,
Y = -5304.69f,
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
X = -91.63f,
Y = -5253.88f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -91.63f,
Y = -5253.88f,
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
X = 10.74f,
Y = -5243.55f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 10.74f,
Y = -5243.55f,
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
X = 114.4f,
Y = -5250.35f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 114.4f,
Y = -5250.35f,
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
X = 184.64f,
Y = -5288.3f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.64f,
Y = -5288.3f,
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
X = 259.22f,
Y = -5342.4f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 259.22f,
Y = -5342.4f,
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
X = 316.91f,
Y = -5418.61f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 316.91f,
Y = -5418.61f,
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
X = 346.59f,
Y = -5494.65f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 346.59f,
Y = -5494.65f,
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
X = -125.69f,
Y = -5420.53f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -125.69f,
Y = -5420.53f,
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
X = 159.22f,
Y = -5424.31f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 159.22f,
Y = -5424.31f,
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
X = -61.53f,
Y = -5352.61f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -61.53f,
Y = -5352.61f,
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
X = 12.05f,
Y = -5352.61f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 12.05f,
Y = -5352.61f,
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
X = 83.75f,
Y = -5350.72f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 83.75f,
Y = -5350.72f,
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
X = -63.42f,
Y = -5428.08f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -63.42f,
Y = -5428.08f,
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
X = 98.84f,
Y = -5428.08f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 98.84f,
Y = -5428.08f,
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
X = -61.53f,
Y = -5479.03f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -61.53f,
Y = -5479.03f,
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
X = 96.96f,
Y = -5477.14f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.96f,
Y = -5477.14f,
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
X = 153.56f,
Y = -5480.91f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 153.56f,
Y = -5480.91f,
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
X = -120.02f,
Y = -5479.03f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.02f,
Y = -5479.03f,
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
X = -10.89f,
Y = -5429.97f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -10.89f,
Y = -5429.97f,
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
X = 51.38f,
Y = -5428.08f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 51.38f,
Y = -5428.08f,
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
X = -6.82f,
Y = -5479.03f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -6.82f,
Y = -5479.03f,
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
X = 46.01f,
Y = -5480.91f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 46.01f,
Y = -5480.91f,
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
X = 10.99f,
Y = -352.99f,
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
X = 10.99f,
Y = -352.99f,
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
X = -184.26f,
Y = -352.96f,
},
},
CustomMover = new Galaxy.CMoverSequence() {
Velocity = new System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2>() {
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.3f,
0.0f,
},
VelocityLerpRate = 0.03f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 2.0f,
},
CustomElement = null,
SpawnRemaining = 0,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.26f,
Y = -352.96f,
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
X = 166.98f,
Y = -342.8f,
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
X = 166.98f,
Y = -342.8f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
