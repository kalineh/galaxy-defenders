//
// Stage3.cs
//
namespace Galaxy {
namespace Stages {
public class Stage3 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("Stage3");
stage.ScrollSpeed = 4;
stage.SceneryName = "SimpleSpace";
stage.MusicName = "Music/Stage1";
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
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 10.6f,
Y = -1083.57f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 138.6f,
Y = -1083.57f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 266.6f,
Y = -1083.57f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.15f,
Y = -1357.88f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 239,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.15f,
Y = -1357.88f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.15f,
Y = -1413.88f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 253,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.15f,
Y = -1413.88f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.15f,
Y = -1469.88f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 6258,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.15f,
Y = -1469.88f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -2008.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 1163,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -2008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -2064.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 1307,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -2064.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -2120.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 646,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -2120.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -205.9f,
Y = -2656.33f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -205.9f,
Y = -2656.33f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -2712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -2712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -2712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -2840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -736.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 123,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -736.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -792.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 137,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -792.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -848.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 151,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -848.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 260.76f,
Y = -2853.66f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 260.76f,
Y = -2853.66f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -400.1f,
Y = -5550.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.1f,
Y = -5550.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -144.1f,
Y = -5550.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.1f,
Y = -5678.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -26.76f,
Y = -7070.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 101.24f,
Y = -7070.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 229.24f,
Y = -7070.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 101.24f,
Y = -7198.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 133.24f,
Y = -11003.63f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 261.24f,
Y = -11003.63f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 389.24f,
Y = -11003.63f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 261.24f,
Y = -11131.63f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -866.76f,
Y = -14083.63f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -738.76f,
Y = -14083.63f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -610.76f,
Y = -14083.63f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -738.76f,
Y = -14211.63f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 439.9f,
Y = -15190.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 567.9f,
Y = -15190.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 695.9f,
Y = -15190.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 567.9f,
Y = -15318.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -8488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -8248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 432.0f,
Y = -8248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -8368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 387.99f,
Y = -4591.22f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 387.99f,
Y = -4351.22f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 507.99f,
Y = -4351.22f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 387.99f,
Y = -4471.22f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -500.9f,
Y = -9820.85f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -500.9f,
Y = -9580.85f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -380.9f,
Y = -9580.85f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -500.9f,
Y = -9700.85f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -537.94f,
Y = -15783.82f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -537.94f,
Y = -15543.82f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -417.94f,
Y = -15543.82f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -537.94f,
Y = -15663.82f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -12872.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -12632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -384.0f,
Y = -12872.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -12752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 287.02f,
Y = -10111.78f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 287.02f,
Y = -9871.78f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 159.02f,
Y = -10111.78f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 287.02f,
Y = -9991.78f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -125.48f,
Y = -3774.28f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -125.48f,
Y = -3534.28f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -253.48f,
Y = -3774.28f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -125.48f,
Y = -3654.28f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -249.9f,
Y = -3230.99f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -249.9f,
Y = -3230.99f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 60.39f,
Y = -3685.52f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3333333.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 60.39f,
Y = -3685.52f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 285.49f,
Y = -3393.52f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 285.49f,
Y = -3393.52f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -281.43f,
Y = -141.4f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -157.01f,
Y = -259.79f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -281.19f,
Y = -260.32f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -281.19f,
Y = -386.72f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -125.69f,
Y = -1086.76f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -355.02f,
Y = -8529.28f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -480.0f,
Y = -8400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -8400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -480.0f,
Y = -8528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -13896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -13768.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -13768.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -13896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 528.0f,
Y = -12192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -12064.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 528.0f,
Y = -12064.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -12192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -488.0f,
Y = -10672.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -608.0f,
Y = -10544.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -480.0f,
Y = -10544.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -608.0f,
Y = -10672.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -184.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 123,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -240.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 137,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -296.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 151,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -296.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -6080.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3333333.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -6080.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -6424.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3333333.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -6424.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -192.0f,
Y = -6720.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3333333.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -192.0f,
Y = -6720.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -9232.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3333333.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -9232.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -56.0f,
Y = -8800.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3333333.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -56.0f,
Y = -8800.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -9344.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3333333.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -9344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -125.43f,
Y = -11559.62f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3333333.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -125.43f,
Y = -11559.62f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 348.69f,
Y = -11760.25f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3333333.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 348.69f,
Y = -11760.25f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -343.75f,
Y = -11965.88f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3333333.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -343.75f,
Y = -11965.88f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -12064.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3333333.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -12064.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -14880.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3333333.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -14880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -14512.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3333333.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -14512.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 282.62f,
Y = -14577.44f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3333333.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 282.62f,
Y = -14577.44f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -14752.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3333333.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -14752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -4272.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 1163,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -4272.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -4328.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 1307,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -4328.0f,
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
Y = -4384.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 646,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -4384.0f,
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
Y = -5368.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 1163,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -5368.0f,
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
Y = -5424.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 1307,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -5424.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -5480.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 646,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -5480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -7592.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 1163,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -7592.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -7648.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 1307,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -7648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -7704.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 646,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -7704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -8184.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 1163,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -8184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -8240.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 1307,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -8240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -8296.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 646,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -8296.0f,
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
Y = -9776.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 1163,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 88.0f,
Y = -9776.0f,
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
Y = -9832.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 1307,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 88.0f,
Y = -9832.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 88.0f,
Y = -9888.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 646,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 88.0f,
Y = -9888.0f,
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
Y = -10080.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 1163,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -10080.0f,
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
Y = -10136.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 1307,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -10136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -10192.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 4.0f,
StartFrame = 646,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -10192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -570.84f,
Y = -13428.45f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Paused = false,
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -570.84f,
Y = -13428.45f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -571.99f,
Y = -13292.58f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Paused = false,
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -571.99f,
Y = -13292.58f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -566.51f,
Y = -13358.69f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Paused = false,
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -566.51f,
Y = -13358.69f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -632.0f,
Y = -10896.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Paused = false,
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -632.0f,
Y = -10896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -632.0f,
Y = -10760.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Paused = false,
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -632.0f,
Y = -10760.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -624.0f,
Y = -10824.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Paused = false,
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -624.0f,
Y = -10824.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -616.0f,
Y = -8784.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Paused = false,
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -616.0f,
Y = -8784.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -616.0f,
Y = -8648.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Paused = false,
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -616.0f,
Y = -8648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBeamer),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -608.0f,
Y = -8712.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 0.0f,
},
Paused = false,
Name = "MoveRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -608.0f,
Y = -8712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 319.67f,
Y = -8251.38f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 319.67f,
Y = -8251.38f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -8488.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -8488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -350.33f,
Y = -8398.5f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -350.33f,
Y = -8398.5f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -384.0f,
Y = -9576.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -384.0f,
Y = -9576.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -10112.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -10112.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -472.0f,
Y = -10536.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -472.0f,
Y = -10536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -11008.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 128.0f,
Y = -11008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -12064.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 392.0f,
Y = -12064.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.26f,
Y = -12194.89f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.26f,
Y = -12194.89f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -12872.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -12872.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -13752.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -13752.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -13896.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -13896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -424.0f,
Y = -15536.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -424.0f,
Y = -15536.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -1080.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -1080.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -1080.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -1080.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -2840.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -2840.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 92.03f,
Y = -2709.55f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 92.03f,
Y = -2709.55f,
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
Y = -3776.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -3776.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.0f,
Y = -4600.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.0f,
Y = -4600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 512.0f,
Y = -4344.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 512.0f,
Y = -4344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 111,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -268.22f,
Y = -5566.3f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -268.22f,
Y = -5566.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 270.63f,
Y = -1086.25f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -279.72f,
Y = -384.8f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -2712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -2712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -3528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = -3664.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1Building1Building1Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.03f,
Y = -4350.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -5552.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -5680.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -392.0f,
Y = -5552.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 103.16f,
Y = -7068.43f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -7064.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -7064.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -7192.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -7192.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -7056.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -7056.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -8376.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 424.0f,
Y = -8240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -8520.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -496.0f,
Y = -9712.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -9992.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -9864.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -472.0f,
Y = -10664.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 264.53f,
Y = -11125.93f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 259.28f,
Y = -11010.93f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.03f,
Y = -11012.68f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.03f,
Y = -11012.68f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 531.78f,
Y = -12190.05f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -252.84f,
Y = -12764.93f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -254.59f,
Y = -12643.3f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -13888.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -13768.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 440.0f,
Y = -15184.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
