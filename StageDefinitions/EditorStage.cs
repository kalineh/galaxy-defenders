//
// EditorStage.cs
//
namespace Galaxy {
namespace Stages {
public class EditorStage {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("EditorStage");
stage.ScrollSpeed = 1.0f;
stage.SceneryName = "Blend";
stage.MusicName = "Music/Stage1";
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
HealthMax = 3.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -616.24f,
Y = 104.65f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -763.31f,
Y = 108.07f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building3",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -776.41f,
Y = -1.93f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform4",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -790.35f,
Y = -265.63f,
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
X = -602.82f,
Y = -127.59f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building4",
HealthMax = 11.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -627.02f,
Y = 12.24f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Platform1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -605.7f,
Y = -274.5f,
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
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building6",
HealthMax = 13.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -773.8f,
Y = -127.37f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -20.83f,
Y = -57.52f,
},
},
CustomMover = new Galaxy.CMoverSin() {
Frequency = 0.05f,
Amplitude = 4.0f,
Down = 1.0f,
SpeedMultiplier = 0.0f,
StartFrame = 333,
Paused = false,
Name = "MoveSin",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -20.83f,
Y = -57.52f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementDecoration() {
TextureName = "Grass1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.0f,
Y = 144.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building8",
HealthMax = 10.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 247.0f,
Y = 301.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = 88.0f,
},
},
CustomMover = new Galaxy.CMoverSequence() {
Velocity = new System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2>() {
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -2.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.2f,
1.3f,
0.0f,
},
VelocityLerpRate = 0.04f,
AlwaysMaxSpeed = false,
SpeedMultiplier = 0.0f,
Paused = false,
Name = "MoveDownStopDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 328.0f,
Y = 88.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = 0.0f,
},
},
CustomMover = new Galaxy.CMoverSequence() {
Velocity = new System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2>() {
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -2.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.2f,
1.3f,
0.0f,
},
VelocityLerpRate = 0.04f,
AlwaysMaxSpeed = false,
SpeedMultiplier = 0.0f,
Paused = false,
Name = "MoveDownStopDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 224.0f,
Y = 0.0f,
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
Y = -32.0f,
},
},
CustomMover = new Galaxy.CMoverSequence() {
Velocity = new System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2>() {
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -2.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.2f,
1.3f,
0.0f,
},
VelocityLerpRate = 0.04f,
AlwaysMaxSpeed = false,
SpeedMultiplier = 0.0f,
Paused = false,
Name = "MoveDownStopDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -32.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementCameraStop() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = 288.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = 16.0f,
},
},
CustomMover = new Galaxy.CMoverSequence() {
Velocity = new System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2>() {
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -2.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.2f,
1.3f,
0.0f,
},
VelocityLerpRate = 0.04f,
AlwaysMaxSpeed = false,
SpeedMultiplier = 0.0f,
Paused = false,
Name = "MoveDownStopDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -408.0f,
Y = 16.0f,
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
Y = -48.0f,
},
},
CustomMover = new Galaxy.CMoverSequence() {
Velocity = new System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2>() {
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -2.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.2f,
1.3f,
0.0f,
},
VelocityLerpRate = 0.04f,
AlwaysMaxSpeed = false,
SpeedMultiplier = 0.0f,
Paused = false,
Name = "MoveDownStopDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -48.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = -72.0f,
},
},
CustomMover = new Galaxy.CMoverSequence() {
Velocity = new System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2>() {
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -2.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.2f,
1.3f,
0.0f,
},
VelocityLerpRate = 0.04f,
AlwaysMaxSpeed = false,
SpeedMultiplier = 0.0f,
Paused = false,
Name = "MoveDownStopDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = -72.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -80.0f,
Y = -96.0f,
},
},
CustomMover = new Galaxy.CMoverSequence() {
Velocity = new System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2>() {
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -2.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.2f,
1.3f,
0.0f,
},
VelocityLerpRate = 0.04f,
AlwaysMaxSpeed = false,
SpeedMultiplier = 0.0f,
Paused = false,
Name = "MoveDownStopDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -80.0f,
Y = -96.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -96.0f,
},
},
CustomMover = new Galaxy.CMoverSequence() {
Velocity = new System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2>() {
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -2.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.2f,
1.3f,
0.0f,
},
VelocityLerpRate = 0.04f,
AlwaysMaxSpeed = false,
SpeedMultiplier = 0.0f,
Paused = false,
Name = "MoveDownStopDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 24.0f,
Y = -96.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -80.0f,
},
},
CustomMover = new Galaxy.CMoverSequence() {
Velocity = new System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2>() {
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -2.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.2f,
1.3f,
0.0f,
},
VelocityLerpRate = 0.04f,
AlwaysMaxSpeed = false,
SpeedMultiplier = 0.0f,
Paused = false,
Name = "MoveDownStopDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -80.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 288.0f,
Y = -40.0f,
},
},
CustomMover = new Galaxy.CMoverSequence() {
Velocity = new System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2>() {
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -2.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.2f,
1.3f,
0.0f,
},
VelocityLerpRate = 0.04f,
AlwaysMaxSpeed = false,
SpeedMultiplier = 0.0f,
Paused = false,
Name = "MoveDownStopDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 288.0f,
Y = -40.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 336.0f,
Y = -8.0f,
},
},
CustomMover = new Galaxy.CMoverSequence() {
Velocity = new System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2>() {
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -2.0f,
},
new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
Duration = new System.Collections.Generic.List<System.Single>() {
0.2f,
1.3f,
0.0f,
},
VelocityLerpRate = 0.04f,
AlwaysMaxSpeed = false,
SpeedMultiplier = 0.0f,
Paused = false,
Name = "MoveDownStopDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 336.0f,
Y = -8.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
