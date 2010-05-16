//
// Stage2.cs
//
namespace Galaxy {
namespace Stages {
public class Stage2 {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("Stage2");
stage.ScrollSpeed = 3.0f;
stage.SceneryName = "BlueSky";
stage.MusicName = "Music/B";
stage.AddElement(0, 
new Galaxy.CStageElementCameraStop() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -16368.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementStageFinish() {
World = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -19680.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -496.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -496.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -496.0f,
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
Y = -1536.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -1536.0f,
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
Y = -1568.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -1568.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -1600.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -1600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -3528.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -3528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -3560.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -3560.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -3592.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -3592.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -4992.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -4992.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -240.0f,
Y = -4992.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -240.0f,
Y = -4992.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -5000.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
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
X = 328.0f,
Y = -5000.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = -5000.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -4264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -496.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -496.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -400.0f,
Y = -496.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -400.0f,
Y = -496.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -2168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -2168.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -2168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -3008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -3008.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -3008.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -192.0f,
Y = -5616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -192.0f,
Y = -5616.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -192.0f,
Y = -5616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -5416.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -5616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -5616.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -5616.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -7472.0f,
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
Y = -7472.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -7472.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -7472.0f,
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
Y = -7472.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -7472.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -9416.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -9416.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -9416.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -9416.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -9416.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -9416.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -11816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -11816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -11960.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -11960.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = -11960.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -11960.0f,
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
Y = -11960.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -11960.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -4264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -1288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -1416.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 184.0f,
Y = -1296.0f,
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
Y = -1408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 304.0f,
Y = -1288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 304.0f,
Y = -1416.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -1296.0f,
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
Y = -1408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -1408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -328.0f,
Y = -6576.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -328.0f,
Y = -6704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -320.0f,
Y = -6584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -6696.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -6576.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -6704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -192.0f,
Y = -6584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.0f,
Y = -6696.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -6696.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -6576.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -6704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -6584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -6696.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 336.0f,
Y = -6576.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 336.0f,
Y = -6704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 344.0f,
Y = -6584.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 352.0f,
Y = -6696.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -6696.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CSpike),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -6880.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 0.5f,
SpeedMultiplier = 1.0f,
StartFrame = 17167,
Paused = false,
Name = null,
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -6880.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -96.0f,
Y = -4264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -4264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -4264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -4264.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -4264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -240.0f,
Y = -4264.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -240.0f,
Y = -4264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -4264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -4264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -5416.0f,
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
Y = -936.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -936.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -936.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -936.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -2648.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -2648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 288.0f,
Y = -2648.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 288.0f,
Y = -2648.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -280.0f,
Y = -496.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -8400.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -8400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -8400.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -8400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -8408.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -8408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = -8408.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = -8408.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -8544.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -40.0f,
Y = -8544.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = -8544.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -8544.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 88.0f,
Y = -8544.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CSpike),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -9600.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 0.5f,
SpeedMultiplier = 1.0f,
StartFrame = 17167,
Paused = false,
Name = null,
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -9600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 288.0f,
Y = -10160.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 288.0f,
Y = -10160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -208.0f,
Y = -10160.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
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
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -10344.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -10344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CIsosceles),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = -10344.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 3.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = -10344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 208.0f,
Y = -10920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -10920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = -10920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -40.0f,
Y = -10920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -10920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -10920.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -10920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -56.0f,
Y = -10920.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -56.0f,
Y = -10920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -152.0f,
Y = -10920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -10920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = -10920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 448.0f,
Y = -10920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 576.0f,
Y = -10920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -10920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -416.0f,
Y = -10920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -544.0f,
Y = -10920.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CSpike),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -12344.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 0.5f,
SpeedMultiplier = 3.0f,
StartFrame = 17167,
Paused = false,
Name = null,
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -12344.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CSpike),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -12080.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 0.5f,
SpeedMultiplier = 3.0f,
StartFrame = 17167,
Paused = false,
Name = null,
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = -12080.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CSpike),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 456.0f,
Y = -12072.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 0.5f,
SpeedMultiplier = 3.0f,
StartFrame = 17167,
Paused = false,
Name = null,
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 456.0f,
Y = -12072.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -12960.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -12960.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -12960.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -200.0f,
Y = -12960.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -12968.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = -12968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = -12968.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = -12968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 8.0f,
Y = -13528.0f,
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
Y = -13528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 112.0f,
Y = -13528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -13528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -13528.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = -14144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -14144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -14144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -288.0f,
Y = -14144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -14144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CSpike),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -14224.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 0.5f,
SpeedMultiplier = 1.0f,
StartFrame = 17167,
Paused = false,
Name = null,
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 240.0f,
Y = -14224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -312.0f,
Y = -14704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.0f,
Y = -14704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -14704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -14704.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -72.0f,
Y = -14704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -14704.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -14704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.0f,
Y = -14704.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -15968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -15968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 72.0f,
Y = -15968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -15968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -15968.0f,
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
Y = -15968.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -15968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -40.0f,
Y = -15968.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -40.0f,
Y = -15968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -15968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -15968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -15968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 440.0f,
Y = -15968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 568.0f,
Y = -15968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -15968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -392.0f,
Y = -15968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -520.0f,
Y = -15968.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -16088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -16088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 72.0f,
Y = -16088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -16088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -16088.0f,
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
Y = -16088.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = -16088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -40.0f,
Y = -16088.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -40.0f,
Y = -16088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -16088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -16088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -16088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 440.0f,
Y = -16088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 568.0f,
Y = -16088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -16088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -392.0f,
Y = -16088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -520.0f,
Y = -16088.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -16208.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -392.0f,
Y = -16208.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -520.0f,
Y = -16208.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 584.0f,
Y = -16216.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 456.0f,
Y = -16216.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = -16216.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -16216.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = -16216.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -16216.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -16216.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -16208.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -128.0f,
Y = -16208.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -40.0f,
Y = -16208.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -40.0f,
Y = -16208.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -16208.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -16208.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -16208.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -16208.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -16208.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = -16208.0f,
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
Y = -16248.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = -16248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.0f,
Y = -16248.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 384.0f,
Y = -16248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -312.0f,
Y = -16248.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -312.0f,
Y = -16248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CShootBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -16248.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -16248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -3128.0f,
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
Y = -11816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -11816.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -3624.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -3624.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -1632.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = -1632.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -2808.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 216.0f,
Y = -2808.0f,
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
Y = -2808.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 2.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "Down",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 280.0f,
Y = -2808.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -232.0f,
Y = -3136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 15,
Powerup = false,
Type = typeof(Galaxy.CBonusShip),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -2232.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 3.0f,
StartFrame = 1364,
Paused = false,
Name = "Sin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 16.0f,
Y = -2232.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 15,
Powerup = false,
Type = typeof(Galaxy.CBonusShip),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -6144.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 1.0f,
StartFrame = 1364,
Paused = false,
Name = "Sin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = -6144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 15,
Powerup = false,
Type = typeof(Galaxy.CBonusShip),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -9400.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 1.0f,
StartFrame = 1364,
Paused = false,
Name = "Sin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -9400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 15,
Powerup = false,
Type = typeof(Galaxy.CBonusShip),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -13680.0f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 1.0f,
StartFrame = 1364,
Paused = false,
Name = "Sin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -13680.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
