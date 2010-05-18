//
// EditorStage.cs
//
namespace Galaxy {
namespace Stages {
public class EditorStage {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("EditorStage");
stage.ScrollSpeed = 3.0f;
stage.SceneryName = "BlueSky";
stage.MusicName = "Music/Stage1";
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -272.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 40.0f,
Y = -272.0f,
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
Y = -368.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 2.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 120.0f,
Y = -368.0f,
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
Y = -464.0f,
},
},
MoverPresetName = "DownLeft",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -464.0f,
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
Y = -680.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -680.0f,
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
Y = -592.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 2.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -80.0f,
Y = -592.0f,
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
Y = -504.0f,
},
},
MoverPresetName = "DownRight",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 3.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -504.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -64.0f,
Y = -152.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 32.0f,
Y = -160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 104.0f,
Y = -160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = -160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -152.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
