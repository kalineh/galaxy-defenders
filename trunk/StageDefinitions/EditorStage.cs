//
// EditorStage.cs
//
namespace Galaxy {
namespace Stages {
public class EditorStage {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("EditorStage");
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CSinBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -302.71f,
Y = -388.27f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -302.71f,
Y = -388.27f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CSinBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -236.71f,
Y = -452.27f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -236.71f,
Y = -452.27f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CSinBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 253.29f,
Y = -624.27f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 253.29f,
Y = -624.27f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CSinBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 193.29f,
Y = -686.27f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 193.29f,
Y = -686.27f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CSinBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 243.47f,
Y = -1141.27f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 243.47f,
Y = -1141.27f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CSinBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 185.29f,
Y = -1203.27f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 185.29f,
Y = -1203.27f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CSinBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -269.89f,
Y = -924.27f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -269.89f,
Y = -924.27f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CSinBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -211.16f,
Y = -1001.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -211.16f,
Y = -1001.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CBigSpacePlatform),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -439.38f,
Y = -1392.31f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -439.38f,
Y = -1392.31f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CSpacePlatform),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 354.57f,
Y = -1099.33f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 354.57f,
Y = -1099.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CSpacePlatform),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -121.79f,
Y = -1061.15f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -121.79f,
Y = -1061.15f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CSpacePlatform),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 354.57f,
Y = -1002.97f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 354.57f,
Y = -1002.97f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CBuilding),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 355.52f,
Y = -1004.57f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 355.52f,
Y = -1004.57f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CBuilding),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -386.81f,
Y = -1339.26f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -386.81f,
Y = -1339.26f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -487.25f,
Y = -1593.88f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -487.25f,
Y = -1593.88f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -430.88f,
Y = -1530.24f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -430.88f,
Y = -1530.24f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -480.88f,
Y = -1858.43f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -480.88f,
Y = -1858.43f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -424.52f,
Y = -1794.79f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -424.52f,
Y = -1794.79f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 440.93f,
Y = -2012.97f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 440.93f,
Y = -2012.97f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 497.3f,
Y = -1949.33f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 497.3f,
Y = -1949.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 431.84f,
Y = -2178.43f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 431.84f,
Y = -2178.43f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.21f,
Y = -2114.79f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.21f,
Y = -2114.79f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CBigSpacePlatform),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 20.03f,
Y = -1990.24f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 20.03f,
Y = -1990.24f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CBuilding),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -23.61f,
Y = -1946.61f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -23.61f,
Y = -1946.61f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CSpacePlatform),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 209.12f,
Y = -1637.52f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 209.12f,
Y = -1637.52f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CBuilding),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 355.2f,
Y = -1095.2f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 355.2f,
Y = -1095.2f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CBuilding),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -385.6f,
Y = -1455.2f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -385.6f,
Y = -1455.2f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CBuilding),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 212.8f,
Y = -1639.2f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 212.8f,
Y = -1639.2f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CBuilding),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -118.4f,
Y = -1063.2f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -118.4f,
Y = -1063.2f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
