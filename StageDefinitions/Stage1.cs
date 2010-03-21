//
// Stage1.cs
//
namespace Galaxy {
namespace Stages {
public class Stage1 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("Stage1");
stage.ScrollSpeed = 4;
stage.SceneryName = "BlueSky";
stage.MusicName = "Music/Stage1";
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -856.0f,
Y = -1984.0f,
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
X = -1016.0f,
Y = -2088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -816.0f,
Y = -2512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1016.0f,
Y = -2352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1016.0f,
Y = -2496.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
HealthMax = 11.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -880.0f,
Y = -2104.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
HealthMax = 11.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1008.0f,
Y = -2224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = -344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -840.0f,
Y = -2368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = -968.0f,
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
X = 248.0f,
Y = -968.0f,
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
X = -1104.0f,
Y = -2752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = -912.0f,
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
X = 248.0f,
Y = -912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = -856.0f,
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
X = 248.0f,
Y = -856.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = -800.0f,
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
X = 248.0f,
Y = -800.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 448.0f,
Y = -344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -1448.0f,
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
X = -152.0f,
Y = -1448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -1392.0f,
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
X = -152.0f,
Y = -1392.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -1336.0f,
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
X = -152.0f,
Y = -1336.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -1280.0f,
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
X = -152.0f,
Y = -1280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 221.46f,
Y = -1949.37f,
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
X = 221.46f,
Y = -1949.37f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 221.46f,
Y = -1893.37f,
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
X = 221.46f,
Y = -1893.37f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 221.46f,
Y = -1837.37f,
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
X = 221.46f,
Y = -1837.37f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 221.46f,
Y = -1781.37f,
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
X = 221.46f,
Y = -1781.37f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -488.0f,
Y = -2336.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -488.0f,
Y = -2336.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -488.0f,
Y = -2280.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -488.0f,
Y = -2280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -616.0f,
Y = -5000.0f,
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
X = -616.0f,
Y = -5000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -624.0f,
Y = -4968.0f,
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
X = -624.0f,
Y = -4968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -2760.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -2760.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -2704.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -2704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -880.0f,
Y = -6072.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -899.03f,
Y = -6323.94f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building3",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1044.78f,
Y = -6174.85f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -860.78f,
Y = -6606.85f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1040.0f,
Y = -6448.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1052.78f,
Y = -6590.85f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
HealthMax = 11.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -904.0f,
Y = -6192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
HealthMax = 11.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1036.78f,
Y = -6310.85f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -872.0f,
Y = -6456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -858.47f,
Y = -6879.95f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform6",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1128.0f,
Y = -6848.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -840.0f,
Y = -11496.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -832.0f,
Y = -11728.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building3",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1000.34f,
Y = -11608.19f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -816.0f,
Y = -12032.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1000.0f,
Y = -11880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1008.34f,
Y = -12024.19f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
HealthMax = 11.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -864.0f,
Y = -11616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
HealthMax = 11.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -984.0f,
Y = -11744.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -840.0f,
Y = -11880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -808.0f,
Y = -12312.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform6",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1088.34f,
Y = -12280.19f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -52.74f,
Y = -2815.66f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 72.0f,
Y = -2816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -2696.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -2688.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.0f,
Y = -2816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 72.0f,
Y = -2816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -3256.0f,
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
X = 120.0f,
Y = -3256.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -3288.0f,
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
X = 56.0f,
Y = -3288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -3320.0f,
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
X = -8.0f,
Y = -3320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -3352.0f,
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
X = -72.0f,
Y = -3352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -4504.0f,
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
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -4504.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -4440.0f,
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
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -4440.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -4776.0f,
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
Y = 0.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -4776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -4720.0f,
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
Y = 0.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -4720.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -4112.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -4120.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -4080.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -4080.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform6",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -712.0f,
Y = -5416.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -427.76f,
Y = -1575.67f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -1064.0f,
Y = -1832.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -1576.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -5920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -5920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -5920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -5920.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -5920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 72.0f,
Y = -5376.0f,
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
X = 72.0f,
Y = -5376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -5328.0f,
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
X = 0.0f,
Y = -5328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -5280.0f,
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
X = -72.0f,
Y = -5280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -5232.0f,
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
X = -136.0f,
Y = -5232.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -5920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -5920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -504.0f,
Y = -3616.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -504.0f,
Y = -3616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -504.0f,
Y = -3568.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -504.0f,
Y = -3568.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -7136.0f,
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
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -7136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -7064.0f,
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
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -7064.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -7128.0f,
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
Y = 0.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -7128.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -7056.0f,
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
Y = 0.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.0f,
Y = -7056.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -6352.0f,
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
X = -96.0f,
Y = -6352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -6384.0f,
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
X = -152.0f,
Y = -6384.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -6392.0f,
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
X = 144.0f,
Y = -6392.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 88.0f,
Y = -6352.0f,
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
X = 88.0f,
Y = -6352.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -7224.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -7224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -7224.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -7224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -392.0f,
Y = -7224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -7224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -7224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -7224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -512.0f,
Y = -7616.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -512.0f,
Y = -7616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -512.0f,
Y = -7560.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -512.0f,
Y = -7560.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -7744.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -7744.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -7696.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -7696.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -504.0f,
Y = -7904.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -504.0f,
Y = -7904.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -504.0f,
Y = -7848.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -504.0f,
Y = -7848.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 496.0f,
Y = -8056.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 496.0f,
Y = -8056.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 496.0f,
Y = -8008.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 496.0f,
Y = -8008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform6",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -8176.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
HealthMax = 11.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = -8216.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -8120.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -8760.0f,
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
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -8760.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -8696.0f,
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
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -8696.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -8640.0f,
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
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -8640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -8584.0f,
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
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -8584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -80.0f,
Y = -9016.0f,
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
X = -80.0f,
Y = -9016.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -9064.0f,
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
X = -136.0f,
Y = -9064.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -9064.0f,
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
X = -232.0f,
Y = -9064.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -296.0f,
Y = -9000.0f,
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
X = -296.0f,
Y = -9000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -9224.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.0f,
Y = -9224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -9224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -9224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -9224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -10928.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -416.0f,
Y = -10928.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -9792.0f,
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
X = 208.0f,
Y = -9792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -9736.0f,
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
X = 208.0f,
Y = -9736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -9680.0f,
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
X = 208.0f,
Y = -9680.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -9632.0f,
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
X = 208.0f,
Y = -9632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -10928.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -416.0f,
Y = -10928.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -10312.0f,
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
X = -208.0f,
Y = -10312.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -10256.0f,
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
X = -208.0f,
Y = -10256.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -10208.0f,
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
X = -208.0f,
Y = -10208.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -10160.0f,
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
X = -208.0f,
Y = -10160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -11160.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -11160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -11120.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 488.0f,
Y = -11120.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -10912.0f,
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
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -10912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -10856.0f,
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
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -10856.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -10912.0f,
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
Y = 0.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -10912.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -10856.0f,
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
Y = 0.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -10856.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -488.0f,
Y = -11440.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -488.0f,
Y = -11440.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -488.0f,
Y = -11384.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 6.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -488.0f,
Y = -11384.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.0f,
Y = -11824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -11824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -11824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -11824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 432.0f,
Y = -11824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -11824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -312.0f,
Y = -11824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -432.0f,
Y = -11824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -11824.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -11824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -11824.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -11824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -12864.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform5",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -648.0f,
Y = -12704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
HealthMax = 11.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -12864.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -12864.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -12824.0f,
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
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -12824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -12768.0f,
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
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -12768.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -176.0f,
Y = -12824.0f,
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
Y = 0.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -176.0f,
Y = -12824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -176.0f,
Y = -12768.0f,
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
Y = 0.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.1f,
0.0f,
},
VelocityLerpRate = 0.13f,
AlwaysMaxSpeed = true,
SpeedMultiplier = 6.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -176.0f,
Y = -12768.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -56.0f,
Y = -13272.0f,
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
X = -56.0f,
Y = -13272.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = -13288.0f,
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
X = -112.0f,
Y = -13288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -13296.0f,
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
X = 176.0f,
Y = -13296.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -13272.0f,
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
X = 120.0f,
Y = -13272.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -14304.0f,
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
X = -208.0f,
Y = -14304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -14248.0f,
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
X = -208.0f,
Y = -14248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -14192.0f,
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
X = -208.0f,
Y = -14192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -14136.0f,
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
X = -208.0f,
Y = -14136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.0f,
Y = -14640.0f,
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
X = 184.0f,
Y = -14640.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.0f,
Y = -14584.0f,
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
X = 184.0f,
Y = -14584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.0f,
Y = -14528.0f,
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
X = 184.0f,
Y = -14528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.0f,
Y = -14472.0f,
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
X = 184.0f,
Y = -14472.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform3",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = -14016.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = -14120.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = -14008.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = -14008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
HealthMax = 11.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = -14120.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -261.25f,
Y = -15091.77f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -261.25f,
Y = -15091.77f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 266.75f,
Y = -15091.77f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 266.75f,
Y = -15091.77f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -389.25f,
Y = -15091.77f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 394.75f,
Y = -15091.77f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 266.75f,
Y = -15091.77f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -261.25f,
Y = -15091.77f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -261.25f,
Y = -15429.02f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -261.25f,
Y = -15429.02f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 266.75f,
Y = -15429.02f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 266.75f,
Y = -15429.02f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -389.25f,
Y = -15429.02f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 394.75f,
Y = -15429.02f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 266.75f,
Y = -15429.02f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -261.25f,
Y = -15429.02f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -15736.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -15736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -15744.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -15744.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -392.0f,
Y = -15736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 402.59f,
Y = -15746.67f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 274.59f,
Y = -15746.67f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -15736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -15960.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -15960.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -15960.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -16096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -16096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -16096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -15960.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -15960.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -15960.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -16096.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -16096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -16096.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -16096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -16096.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -16096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementCameraStop() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -16280.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementStageFinish() {
World = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 10.95f,
Y = -17356.88f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
