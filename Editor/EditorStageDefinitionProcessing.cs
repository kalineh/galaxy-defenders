//
// EditorStageDefinitionProcessing.cs
//

// TODO: think of a better name for this file

using System;
using System.Globalization;
using System.ComponentModel;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    using WinPoint = System.Drawing.Point;
    using XnaPoint = Microsoft.Xna.Framework.Point;
    using XnaColor = Microsoft.Xna.Framework.Graphics.Color;

    namespace Editor
    {
        public class CStageGenerate
        {
            public static void GenerateStageEntitiesFromDefinition(CWorld world, CStageDefinition definition)
            {
                // TODO: there shouldnt be a ship really (replace with player spawn? (should always be center-bottom though really))
                CShip ship = new CShip(world, CSaveData.GetCurrentProfile(), Vector2.Zero);
                world.EntityAdd(ship);

                foreach (KeyValuePair<int, List<CStageElement>> time_element in definition.Elements)
                {
                    foreach (CStageElement element in time_element.Value)
                    {
                        // TODO: some nice generation of editor entity types for stage elements?
                        if (element.GetType() == typeof(Galaxy.CSpawnerEntity))
                        {
                            Editor.CSpawnerEntity entity = new Editor.CSpawnerEntity(world, element as Galaxy.CSpawnerEntity);
                            world.EntityAdd(entity);
                        }
                        else
                        {
                            Editor.CUnknown entity = new Editor.CUnknown(world, element);
                            entity.Physics.PositionPhysics.Position = new Vector2(400.0f, time_element.Key * -1.0f);
                            entity.Visual = new CVisual(CContent.LoadTexture2D(world.Game, "Textures/Top/Pixel"), XnaColor.Green);
                            entity.Visual.Scale = new Vector2(20.0f);
                            world.EntityAdd(entity);
                        }
                    }
                }
            }

            public static CStageDefinition GenerateDefinitionFromStageEntities(CWorld world, string name)
            {
                CStageDefinition result = new CStageDefinition(name);

                // TODO: how can we handle all editor entity types well?
                foreach (CEntity entity in world.GetEntitiesOfType(typeof(Editor.CSpawnerEntity)))
                {
                    Editor.CSpawnerEntity spawner = entity as Editor.CSpawnerEntity;

                    if (spawner != null)
                    {
                        // TODO: collect data from spawner into an element!
                        // TODO: custom element support? (inline code?)

                        Galaxy.CSpawnerEntity e = new Galaxy.CSpawnerEntity()
                        {
                            Type = spawner.Type,
                            Position = spawner.Position,
                            SpawnPosition = new CSpawnPositionFixed() { Position = spawner.Position },
                            SpawnCount = spawner.SpawnCount,
                            SpawnTimer = new CSpawnTimerInterval() { Interval = spawner.SpawnInterval },
                            CustomMover = spawner.Mover,
                            CustomElement = null,
                        };

                        // TODO: replace timing with positional access
                        result.AddElement(spawner.StartTime, e);

                        // TODO: here is where we need to set the speed multiplier on the SpawnerEntity from the mover
                        if (spawner.Mover.GetType() == typeof(CMoverSequence))
                        {
                            CMoverSequence mover = e.CustomMover as CMoverSequence;
                            mover.SpeedMultiplier = spawner.MoveSpeed;
                        }

                        // TODO: here is where we need to set the speed multiplier on the SpawnerEntity from the mover
                        if (spawner.Mover.GetType() == typeof(CMoverFixedVelocity))
                        {
                            CMoverFixedVelocity mover = e.CustomMover as CMoverFixedVelocity;
                            mover.SpeedMultiplier = spawner.MoveSpeed;
                        }
                    }

                    // TODO: other types!
                }

                return result;
            }
        }
    }
}
