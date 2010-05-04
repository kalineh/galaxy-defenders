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
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = 16.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = 16.0f,
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
Y = 0.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
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
Y = 0.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = 0.0f,
},
},
CustomMover = new Galaxy.CMoverFixedVelocity() {
SpeedMultiplier = 4.0f,
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -1.0f,
},
Paused = false,
Name = "MoveUp",
},
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = 0.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
