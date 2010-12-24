//
// EditorStage.cs
//
namespace Galaxy {
namespace Stages {
public class EditorStage {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("EditorStage");
stage.DisplayName = "";
stage.ScrollSpeed = 0.01f;
stage.BackgroundSceneryName = "MysteriousCloudsBG";
stage.ForegroundSceneryName = "MysteriousCloudsFG";
stage.MusicName = "The_Voyage";
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -424.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = -424.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -424.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 56.0f,
Y = -424.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.0f,
Y = -424.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -48.0f,
Y = -424.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -416.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -136.0f,
Y = -416.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CMissilePod),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -416.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = -416.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.SkillDashBurst,
Cycle = 0.02f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = 320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CBall),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -12.0f,
Y = -246.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -12.0f,
Y = -246.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.SkillAbsorbBullet,
Cycle = 0.02f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -176.0f,
Y = 320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.SkillReflectBullet,
Cycle = 0.02f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -104.0f,
Y = 320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.SkillGroundSmash,
Cycle = 1.0f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = 320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.SkillArmorRepair,
Cycle = 0.02f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = 320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.SkillDetonationExplosion,
Cycle = 0.02f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 176.0f,
Y = 320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.SkillDashBurst,
Cycle = 0.02f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = 320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.PlayerShipShieldDamage,
Cycle = 0.02f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -328.0f,
Y = 168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.PlayerShipDestroyed,
Cycle = 1.0f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -216.0f,
Y = 168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.EnemyDeathExplosion,
Cycle = 1.0f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -112.0f,
Y = 184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.BuildingDestroyedSmall,
Cycle = 1.0f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -24.0f,
Y = 184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.BuildingDestroyedBig,
Cycle = 1.0f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 72.0f,
Y = 184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.WeaponLaserHit,
Cycle = 1.0f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 160.0f,
Y = 184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.WeaponMissileHit,
Cycle = 1.0f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 248.0f,
Y = 184.0f,
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
Y = -248.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 232.0f,
Y = -248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.0f,
Y = -224.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -184.0f,
Y = -224.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -240.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -272.0f,
Y = -240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -296.0f,
Y = -184.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -296.0f,
Y = -184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -136.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -304.0f,
Y = -136.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = -168.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -168.0f,
Y = -168.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -184.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -184.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -152.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 136.0f,
Y = -152.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -128.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 272.0f,
Y = -128.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementSpawnerEnemy() {
Coins = 1,
Powerup = false,
Type = typeof(Galaxy.CTripleShot),
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -136.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 312.0f,
Y = -136.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
