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
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 436.4f,
Y = 210.47f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 436.4f,
Y = 210.47f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 435.85f,
Y = 111.97f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 435.85f,
Y = 111.97f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 445.28f,
Y = 45.93f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 445.28f,
Y = 45.93f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 443.4f,
Y = -6.9f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 443.4f,
Y = -6.9f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 445.28f,
Y = -76.71f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 445.28f,
Y = -76.71f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 445.28f,
Y = -157.84f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 445.28f,
Y = -157.84f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 443.4f,
Y = -223.88f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 443.4f,
Y = -223.88f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 443.4f,
Y = -284.26f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = -1.0f,
Y = 0.0f,
},
Name = "MoveLeft",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 443.4f,
Y = -284.26f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -479.31f,
Y = 96.12f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 1.0f,
},
Name = "MoveDownRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -479.31f,
Y = 96.12f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -469.88f,
Y = 30.08f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 1.0f,
},
Name = "MoveDownRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -469.88f,
Y = 30.08f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -471.76f,
Y = -22.75f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 1.0f,
},
Name = "MoveDownRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -471.76f,
Y = -22.75f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -469.88f,
Y = -92.56f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 1.0f,
},
Name = "MoveDownRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -469.88f,
Y = -92.56f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -469.88f,
Y = -173.69f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 1.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 1.0f,
Y = 1.0f,
},
Name = "MoveDownRight",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -469.88f,
Y = -173.69f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
