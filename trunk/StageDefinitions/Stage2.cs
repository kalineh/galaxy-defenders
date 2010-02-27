//
// Stage2.cs
//
namespace Galaxy {
namespace Stages {
public class Stage2 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("Stage2");
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -862.65f,
Y = -1998.4f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -870.25f,
Y = -2237.09f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building3",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1022.82f,
Y = -2104.98f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -843.17f,
Y = -2532.28f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1036.76f,
Y = -2368.68f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1031.9f,
Y = -2506.35f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
HealthMax = 11.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -879.49f,
Y = -2101.69f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
HealthMax = 11.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1023.43f,
Y = -2235.55f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -852.11f,
Y = -2377.55f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -829.69f,
Y = -2793.1f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform6",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1117.01f,
Y = -2762.12f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementStageFinish() {
World = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 59.19f,
Y = -13040.95f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -325.42f,
Y = -514.7f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 1.0f,
},
Name = "MoveDownRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -325.42f,
Y = -514.7f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -325.42f,
Y = -467.39f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 1.0f,
},
Name = "MoveDownRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -325.42f,
Y = -467.39f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 3,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = -11640.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 0.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = -11640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 3,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -11640.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 0.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -11640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 3,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -56.0f,
Y = -11640.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 0.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -56.0f,
Y = -11640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 3,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -11640.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 0.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -11640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 3,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -11640.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 0.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -11640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 3,
Powerup = false,
TextureName = "Building3",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -11560.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 3,
Powerup = false,
TextureName = "Building3",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -11560.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 3,
Powerup = false,
TextureName = "Building3",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -11560.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 3,
Powerup = false,
TextureName = "Building3",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = -11560.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 3,
Powerup = false,
TextureName = "Building3",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -11560.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 3,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -11640.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 0.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -11640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementCameraStop() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -349.57f,
Y = -11546.26f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -325.42f,
Y = -558.52f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 1.0f,
},
Name = "MoveDownRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -325.42f,
Y = -558.52f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -308.72f,
Y = -1211.74f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 1.0f,
},
Name = "MoveDownRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -308.72f,
Y = -1211.74f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -308.72f,
Y = -1169.65f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 1.0f,
},
Name = "MoveDownRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -308.72f,
Y = -1169.65f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -308.72f,
Y = -1257.31f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 1.0f,
},
Name = "MoveDownRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -308.72f,
Y = -1257.31f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 322.93f,
Y = -875.39f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 1.0f,
},
Name = "MoveDownLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 322.93f,
Y = -875.39f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 322.93f,
Y = -831.57f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 1.0f,
},
Name = "MoveDownLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 322.93f,
Y = -831.57f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 324.67f,
Y = -919.22f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 1.0f,
},
Name = "MoveDownLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 324.67f,
Y = -919.22f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 317.71f,
Y = -1490.7f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 1.0f,
},
Name = "MoveDownLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 317.71f,
Y = -1490.7f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 317.71f,
Y = -1441.65f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 1.0f,
},
Name = "MoveDownLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 317.71f,
Y = -1441.65f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 317.71f,
Y = -1532.78f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 1.0f,
},
Name = "MoveDownLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 317.71f,
Y = -1532.78f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 17.1f,
Y = -1697.83f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 17.1f,
Y = -1697.83f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -62.9f,
Y = -1727.83f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -62.9f,
Y = -1727.83f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 123.77f,
Y = -1737.83f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 123.77f,
Y = -1737.83f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -162.9f,
Y = -1754.49f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -162.9f,
Y = -1754.49f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 217.1f,
Y = -1771.16f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 217.1f,
Y = -1771.16f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -76.23f,
Y = -2211.16f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -76.23f,
Y = -2211.16f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
