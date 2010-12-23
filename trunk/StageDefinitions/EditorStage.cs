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
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.PlayerStageEndShipTrail,
Cycle = 0.02f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -344.0f,
Y = 160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.PlayerShipShieldDamage,
Cycle = 1.0f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -224.0f,
Y = 160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.PlayerShipArmorDamage,
Cycle = 1.0f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -120.0f,
Y = 160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.PlayerShipDestroyed,
Cycle = 1.0f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -16.0f,
Y = 160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.BuildingDestroyedSmall,
Cycle = 1.0f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 96.0f,
Y = 160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.EnemyMissileTrail,
Cycle = 1.0f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 320.0f,
Y = 160.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -176.0f,
Y = -400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 88.0f,
Y = -400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -400.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -264.0f,
Y = -488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -176.0f,
Y = -488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = -488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 88.0f,
Y = -488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building1",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -352.0f,
Y = -488.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -88.0f,
Y = -312.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -8.0f,
Y = -312.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 80.0f,
Y = -312.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = 64.0f,
Y = -248.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -32.0f,
Y = -240.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -176.0f,
Y = -320.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -312.0f,
Y = -328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -256.0f,
Y = -328.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementBuilding() {
Coins = 0,
Powerup = false,
TextureName = "Building2",
Position = new Microsoft.Xna.Framework.Vector2() {
X = -240.0f,
Y = -264.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementParticleEffectPlayer() {
Type = Galaxy.EParticleType.BuildingDestroyedBig,
Cycle = 1.0f,
Expired = false,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 192.0f,
Y = 160.0f,
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
Y = -128.0f,
},
},
MoverPresetName = "None",
MoverSpeedMultiplier = 1.0f,
MoverTransitionMultiplier = 1.0f,
CustomElement = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = -160.0f,
Y = -128.0f,
},
});
stage.AddElement(0, 
new Galaxy.CStageElementStageFinish() {
World = null,
Position = new Microsoft.Xna.Framework.Vector2() {
X = 157.0f,
Y = -102.0f,
},
});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
