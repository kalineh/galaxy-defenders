//
// EditorStage.cs
//
namespace Galaxy {
namespace Stages {
public class EditorStage {
public static CStageDefinition GenerateDefinition() {
CStageDefinition stage = new CStageDefinition("EditorStage");
float StageTime = 0.0f;
stage.AddElement(0, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverSequence() {
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new System.Single() {
});
new System.Single() {
});
VelocityLerpRate = 0.02f,
AlwaysMaxSpeed = false,
},
},

});
stage.AddElement(0, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverSequence() {
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new System.Single() {
});
new System.Single() {
});
VelocityLerpRate = 0.02f,
AlwaysMaxSpeed = false,
},
},

});
stage.AddElement(0, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 600.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverSequence() {
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new System.Single() {
});
new System.Single() {
});
VelocityLerpRate = 0.02f,
AlwaysMaxSpeed = false,
},
},

});

stage.AddElement(120, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 3,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.2f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverSequence() {
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new System.Single() {
});
new System.Single() {
});
new System.Single() {
});
new System.Single() {
});
new System.Single() {
});
VelocityLerpRate = 0.03f,
AlwaysMaxSpeed = true,
},
},

});

stage.AddElement(180, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 3,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.2f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 600.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverSequence() {
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new System.Single() {
});
new System.Single() {
});
new System.Single() {
});
new System.Single() {
});
new System.Single() {
});
VelocityLerpRate = 0.03f,
AlwaysMaxSpeed = true,
},
},

});
stage.AddElement(180, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CSpacePlatform),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 600.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverFixedVelocity() {
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
},

});
stage.AddElement(180, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CBuilding),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 600.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverFixedVelocity() {
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
},

});

stage.AddElement(300, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 3,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.2f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverSequence() {
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new System.Single() {
});
new System.Single() {
});
new System.Single() {
});
new System.Single() {
});
new System.Single() {
});
VelocityLerpRate = 0.03f,
AlwaysMaxSpeed = true,
},
},

});

stage.AddElement(360, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 3,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.2f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 600.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverSequence() {
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new System.Single() {
});
new System.Single() {
});
new System.Single() {
});
new System.Single() {
});
new System.Single() {
});
VelocityLerpRate = 0.03f,
AlwaysMaxSpeed = true,
},
},

});

stage.AddElement(540, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CBigSpacePlatform),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverFixedVelocity() {
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
},

});
stage.AddElement(540, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CBuilding),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverFixedVelocity() {
Velocity = new Microsoft.Xna.Framework.Vector2() {
X = 0.0f,
Y = 1.0f,
},
},
},

});
stage.AddElement(540, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverSequence() {
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new System.Single() {
});
new System.Single() {
});
VelocityLerpRate = 0.02f,
AlwaysMaxSpeed = false,
},
},

});
stage.AddElement(540, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverSequence() {
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new System.Single() {
});
new System.Single() {
});
VelocityLerpRate = 0.02f,
AlwaysMaxSpeed = false,
},
},

});
stage.AddElement(540, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 600.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverSequence() {
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new System.Single() {
});
new System.Single() {
});
VelocityLerpRate = 0.02f,
AlwaysMaxSpeed = false,
},
},

});

stage.AddElement(720, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 3,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.2f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 600.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverSequence() {
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new System.Single() {
});
new System.Single() {
});
VelocityLerpRate = 0.03f,
AlwaysMaxSpeed = true,
},
},

});
stage.AddElement(720, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 3,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.2f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverSequence() {
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new System.Single() {
});
new System.Single() {
});
VelocityLerpRate = 0.03f,
AlwaysMaxSpeed = true,
},
},

});
stage.AddElement(720, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 400.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverSequence() {
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new System.Single() {
});
new System.Single() {
});
VelocityLerpRate = 0.02f,
AlwaysMaxSpeed = false,
},
},

});
stage.AddElement(720, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 200.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverSequence() {
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new System.Single() {
});
new System.Single() {
});
VelocityLerpRate = 0.02f,
AlwaysMaxSpeed = false,
},
},

});
stage.AddElement(720, new Galaxy.CSpawnerEntity() {
Type = typeof(Galaxy.CTurret),
SpawnCount = 1,
SpawnTimer = new Galaxy.CSpawnTimerInterval() {
Delay = 0.0f,
},
SpawnPosition = new Galaxy.CSpawnPositionFixed() {
Position = new Microsoft.Xna.Framework.Vector2() {
X = 600.0f,
Y = -128.0f,
},
},
CustomElement = new Galaxy.CSpawnerCustomMover() {
Mover = new Galaxy.CMoverSequence() {
new Microsoft.Xna.Framework.Vector2() {
});
new Microsoft.Xna.Framework.Vector2() {
});
new System.Single() {
});
new System.Single() {
});
VelocityLerpRate = 0.02f,
AlwaysMaxSpeed = false,
},
},

});

stage.AddElement(2520, new Galaxy.CStageFinish() {

});

return stage;
}
}
} // namespace Stages
} // namespace Galaxy
