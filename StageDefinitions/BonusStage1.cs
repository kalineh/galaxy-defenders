//
// BonusStage1.cs
//
namespace Galaxy {
namespace Stages {
public class BonusStage1 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("BonusStage1");
stage.ScrollSpeed = 4;
stage.SceneryName = "BonusStage1";
stage.MusicName = "Music/BonusStage1";
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -608.0f,
Y = 104.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -761.72f,
Y = 109.02f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building3",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -774.82f,
Y = -0.98f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -788.76f,
Y = -264.68f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -783.9f,
Y = -402.35f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building5",
HealthMax = 11.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -601.23f,
Y = -126.64f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
HealthMax = 11.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -625.43f,
Y = 13.19f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -604.11f,
Y = -273.55f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform6",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -869.01f,
Y = -658.12f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -680.0f,
Y = 304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -238.0f,
Y = -110.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -520.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -210.0f,
Y = -1310.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 179.0f,
Y = -1498.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -87.67f,
Y = -1752.5f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -196.0f,
Y = -2181.67f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 229.0f,
Y = -2019.17f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 199.83f,
Y = -2410.83f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 316.5f,
Y = -2648.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -254.33f,
Y = -2715.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -2936.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -179.33f,
Y = -3210.83f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 256.0f,
Y = -3240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 195.67f,
Y = -3656.67f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.83f,
Y = -3685.83f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 29.0f,
Y = -4002.5f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -225.17f,
Y = -4248.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = -4664.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.33f,
Y = -4819.17f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 387.33f,
Y = -4923.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.5f,
Y = -5244.17f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -246.0f,
Y = -5523.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 233.17f,
Y = -5769.17f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -246.0f,
Y = -6190.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 158.17f,
Y = -6485.83f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.0f,
Y = -6808.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -212.67f,
Y = -6615.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -175.17f,
Y = -7340.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 249.83f,
Y = -7435.83f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -116.83f,
Y = -7898.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 116.5f,
Y = -8298.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -229.33f,
Y = -8377.5f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 304.0f,
Y = -8744.17f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -250.17f,
Y = -9023.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 141.5f,
Y = -9298.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -240.0f,
Y = -9536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = -4320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 429.0f,
Y = -1169.17f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -483.5f,
Y = -1685.83f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -508.5f,
Y = -919.17f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -487.67f,
Y = -2373.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -550.17f,
Y = -3490.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 529.0f,
Y = -3902.5f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -496.0f,
Y = -4573.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -471.0f,
Y = -5281.67f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 479.0f,
Y = -5585.83f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -516.83f,
Y = -5890.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 554.0f,
Y = -6323.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -316.83f,
Y = -7056.67f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 441.5f,
Y = -7248.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -6048.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 58.17f,
Y = 126.66f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 408.17f,
Y = 435.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -296.0f,
Y = 705.83f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 95.67f,
Y = 572.5f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -391.83f,
Y = 214.16f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -704.0f,
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
X = 40.0f,
Y = -704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 168.0f,
Y = -456.0f,
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
X = 168.0f,
Y = -456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -192.0f,
Y = -480.0f,
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
X = -192.0f,
Y = -480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -288.0f,
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
X = 8.0f,
Y = -288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -223.15f,
Y = -955.32f,
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
X = -223.15f,
Y = -955.32f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 197.9f,
Y = -1146.9f,
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
X = 197.9f,
Y = -1146.9f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -170.52f,
Y = -1450.06f,
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
X = -170.52f,
Y = -1450.06f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 134.74f,
Y = -1690.06f,
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
X = 134.74f,
Y = -1690.06f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -286.31f,
Y = -2029.01f,
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
X = -286.31f,
Y = -2029.01f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 94.74f,
Y = -1955.32f,
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
X = 94.74f,
Y = -1955.32f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 362.11f,
Y = -2300.59f,
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
X = 362.11f,
Y = -2300.59f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -145.26f,
Y = -2509.01f,
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
X = -145.26f,
Y = -2509.01f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.42f,
Y = -2669.01f,
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
X = 128.42f,
Y = -2669.01f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -265.26f,
Y = -2959.53f,
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
X = -265.26f,
Y = -2959.53f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -71.58f,
Y = -3037.43f,
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
X = -71.58f,
Y = -3037.43f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -29.47f,
Y = -3283.74f,
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
X = -29.47f,
Y = -3283.74f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 305.27f,
Y = -3161.64f,
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
X = 305.27f,
Y = -3161.64f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 378.95f,
Y = -3521.64f,
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
X = 378.95f,
Y = -3521.64f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -174.73f,
Y = -3586.9f,
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
X = -174.73f,
Y = -3586.9f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 105.27f,
Y = -3784.8f,
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
X = 105.27f,
Y = -3784.8f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -3992.0f,
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
X = -280.0f,
Y = -3992.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -4200.0f,
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
X = 128.0f,
Y = -4200.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -165.27f,
Y = -4508.28f,
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
X = -165.27f,
Y = -4508.28f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 291.87f,
Y = -4622.56f,
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
X = 291.87f,
Y = -4622.56f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 63.3f,
Y = -4870.18f,
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
X = 63.3f,
Y = -4870.18f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -193.85f,
Y = -5108.28f,
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
X = -193.85f,
Y = -5108.28f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 234.73f,
Y = -5193.99f,
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
X = 234.73f,
Y = -5193.99f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 6.15f,
Y = -5555.9f,
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
X = 6.15f,
Y = -5555.9f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -231.94f,
Y = -5870.18f,
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
X = -231.94f,
Y = -5870.18f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 206.15f,
Y = -6146.37f,
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
X = 206.15f,
Y = -6146.37f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 349.01f,
Y = -5898.75f,
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
X = 349.01f,
Y = -5898.75f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -108.13f,
Y = -6365.42f,
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
X = -108.13f,
Y = -6365.42f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 253.77f,
Y = -6622.56f,
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
X = 253.77f,
Y = -6622.56f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -212.89f,
Y = -6851.14f,
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
X = -212.89f,
Y = -6851.14f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 129.96f,
Y = -7079.71f,
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
X = 129.96f,
Y = -7079.71f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -22.42f,
Y = -7517.8f,
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
X = -22.42f,
Y = -7517.8f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 149.01f,
Y = -7879.71f,
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
X = 149.01f,
Y = -7879.71f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.7f,
Y = -8127.33f,
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
X = -136.7f,
Y = -8127.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -403.37f,
Y = -7565.42f,
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
X = -403.37f,
Y = -7565.42f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 329.96f,
Y = -7660.66f,
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
X = 329.96f,
Y = -7660.66f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 358.53f,
Y = -7041.61f,
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
X = 358.53f,
Y = -7041.61f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 282.34f,
Y = -8270.18f,
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
X = 282.34f,
Y = -8270.18f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 34.73f,
Y = -8603.52f,
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
X = 34.73f,
Y = -8603.52f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -212.89f,
Y = -8641.61f,
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
X = -212.89f,
Y = -8641.61f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 291.87f,
Y = -8936.85f,
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
X = 291.87f,
Y = -8936.85f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -193.85f,
Y = -9298.75f,
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
X = -193.85f,
Y = -9298.75f,
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
Y = -9624.0f,
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
Y = -9624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 6.15f,
Y = -9003.52f,
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
X = 6.15f,
Y = -9003.52f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -328.0f,
Y = -4296.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -328.0f,
Y = -4296.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = -4832.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = -4832.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -5304.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -5304.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -222.08f,
Y = -5721.65f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -222.08f,
Y = -5721.65f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -42.59f,
Y = -6167.81f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -42.59f,
Y = -6167.81f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -314.39f,
Y = -6475.5f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -314.39f,
Y = -6475.5f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -22.08f,
Y = -6906.27f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -22.08f,
Y = -6906.27f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -345.16f,
Y = -7296.01f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -345.16f,
Y = -7296.01f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -57.98f,
Y = -7716.53f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -57.98f,
Y = -7716.53f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -328.0f,
Y = -7976.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -328.0f,
Y = -7976.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -9096.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -9096.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -8856.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -8856.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -47.72f,
Y = -9172.94f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -47.72f,
Y = -9172.94f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -192.0f,
Y = -9704.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -192.0f,
Y = -9704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -8464.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -8464.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 162.53f,
Y = -8090.89f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 162.53f,
Y = -8090.89f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 398.43f,
Y = -7521.66f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 398.43f,
Y = -7521.66f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 172.79f,
Y = -7280.63f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 172.79f,
Y = -7280.63f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 357.41f,
Y = -6829.35f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 357.41f,
Y = -6829.35f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -6456.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -6456.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -5944.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -5944.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 249.71f,
Y = -5460.12f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 249.71f,
Y = -5460.12f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 357.41f,
Y = -5080.63f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 357.41f,
Y = -5080.63f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 229.2f,
Y = -4424.22f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 229.2f,
Y = -4424.22f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 167.66f,
Y = -3470.38f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 167.66f,
Y = -3470.38f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 331.76f,
Y = -2901.14f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 331.76f,
Y = -2901.14f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 147.15f,
Y = -2208.84f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 147.15f,
Y = -2208.84f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = -1192.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = -1192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -1120.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -1120.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -323.52f,
Y = -2341.11f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -323.52f,
Y = -2341.11f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -274.74f,
Y = -3245.98f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -274.74f,
Y = -3245.98f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -274.74f,
Y = -8472.81f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -274.74f,
Y = -8472.81f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 181.36f,
Y = -9492.32f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 181.36f,
Y = -9492.32f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -9856.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -9856.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -335.72f,
Y = -9936.23f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -43.03f,
Y = -10099.64f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.28f,
Y = -9799.64f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -10040.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 381.36f,
Y = -9660.62f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 440.0f,
Y = -9088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementStageFinish() {
World = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -14984.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 178.31f,
Y = -10002.41f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 178.31f,
Y = -10002.41f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -10208.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -10208.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 101.2f,
Y = -10334.94f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 366.27f,
Y = -10489.15f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 108.43f,
Y = -10754.21f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 298.8f,
Y = -10934.94f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -10904.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -93.98f,
Y = -10587.95f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -375.9f,
Y = -10416.86f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -416.0f,
Y = -9952.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -416.0f,
Y = -9952.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -204.82f,
Y = -10166.26f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -204.82f,
Y = -10166.26f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -81.93f,
Y = -10373.49f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -81.93f,
Y = -10373.49f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -320.48f,
Y = -10600.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -320.48f,
Y = -10600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -262.65f,
Y = -10800.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -262.65f,
Y = -10800.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.0f,
Y = -10840.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.0f,
Y = -10840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 93.98f,
Y = -10508.43f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 93.98f,
Y = -10508.43f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 327.71f,
Y = -10662.65f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 327.71f,
Y = -10662.65f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -10816.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -10816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.67f,
Y = -9925.3f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.67f,
Y = -9925.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 281.93f,
Y = -9371.08f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 281.93f,
Y = -9371.08f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -9040.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -9040.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 327.71f,
Y = -8657.83f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 327.71f,
Y = -8657.83f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -349.4f,
Y = -9185.54f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -349.4f,
Y = -9185.54f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -339.76f,
Y = -9513.25f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -339.76f,
Y = -9513.25f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -255.56f,
Y = -11259.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 102.78f,
Y = -11367.63f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -169.44f,
Y = -11637.08f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 180.56f,
Y = -11853.74f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 344.44f,
Y = -11573.19f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -297.22f,
Y = -12056.52f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 338.89f,
Y = -12359.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -27.78f,
Y = -12550.96f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 19.44f,
Y = -12120.41f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 502.78f,
Y = -12003.74f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "WhiteBlock2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -447.22f,
Y = -12431.52f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 61.11f,
Y = -11137.08f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 61.11f,
Y = -11137.08f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 408.0f,
Y = -11248.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 408.0f,
Y = -11248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 230.56f,
Y = -11134.3f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 230.56f,
Y = -11134.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 336.11f,
Y = -11437.08f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 336.11f,
Y = -11437.08f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.11f,
Y = -11612.08f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.11f,
Y = -11612.08f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 0,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 286.11f,
Y = -11812.08f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 286.11f,
Y = -11812.08f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -12008.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -12008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -161.11f,
Y = -11109.3f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -161.11f,
Y = -11109.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -80.56f,
Y = -11373.19f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -80.56f,
Y = -11373.19f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -311.11f,
Y = -11428.74f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -311.11f,
Y = -11428.74f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -377.78f,
Y = -11650.96f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -377.78f,
Y = -11650.96f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -283.33f,
Y = -11914.85f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -283.33f,
Y = -11914.85f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -141.67f,
Y = -11725.96f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -141.67f,
Y = -11725.96f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -83.33f,
Y = -12014.85f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -83.33f,
Y = -12014.85f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -305.56f,
Y = -12212.08f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -305.56f,
Y = -12212.08f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -261.11f,
Y = -12475.96f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -261.11f,
Y = -12475.96f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -150.0f,
Y = -12314.85f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -150.0f,
Y = -12314.85f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.78f,
Y = -12253.74f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.78f,
Y = -12253.74f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 258.33f,
Y = -12375.96f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 258.33f,
Y = -12375.96f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -12112.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -12112.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 86.11f,
Y = -12450.96f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 86.11f,
Y = -12450.96f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -12512.0f,
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
SpeedMultiplier = 3.0f,
Name = "MoveDownLerpLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 368.0f,
Y = -12512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementCameraStop() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -13664.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
