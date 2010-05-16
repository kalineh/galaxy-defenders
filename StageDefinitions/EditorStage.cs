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
X = -24.0f,
Y = -72.0f,
},
},
MoverPresetName = "Sin",
MoverSpeedMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = -72.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
