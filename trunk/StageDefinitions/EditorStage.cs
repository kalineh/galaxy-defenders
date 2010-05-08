//
// EditorStage.cs
//
namespace Galaxy {
namespace Stages {
public class EditorStage {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("EditorStage");
stage.ScrollSpeed = 4;
stage.SceneryName = "Blend";
stage.MusicName = "Music/Stage1";
stage.AddElement(0, 
new Galaxy.CStageElementCameraStop() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -1896.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -1552.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = true,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -248.0f,
Y = -1552.0f,
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
Y = -1552.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
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
Y = -1552.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -1552.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = true,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -1552.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -1552.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = false,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -1552.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -1552.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 0.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
Paused = true,
Name = "MoveDown",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 144.0f,
Y = -1552.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building6",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -336.0f,
Y = -1552.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building6",
HealthMax = 7.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 304.0f,
Y = -1552.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -97.0f,
Y = 49.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 23.0f,
Y = 65.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 48.0f,
Y = -600.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 152.0f,
Y = -760.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -56.0f,
Y = -480.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -42.0f,
Y = -50.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
HealthMax = 6.0f,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -141.0f,
Y = -61.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
