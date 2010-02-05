//
// EditorStageDefinitionProcessing.cs
//

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

    public class StageGenerate 
    {
        public static void GenerateStageEntitiesFromDefinition(CWorld world, CStageDefinition definition)
        {
            foreach (KeyValuePair<int, List<CStageElement>> time_element in definition.Elements)
            {
                foreach (CStageElement element in time_element.Value)
                {
                    // TODO: some nice generation of editor entity types for stage elements?
                    if (element.GetType() == typeof(CSpawnerEntity))
                    {
                        Editor.CSpawnerEntity entity = new Editor.CSpawnerEntity(world, element as CSpawnerEntity);
                        entity.StartTime = time_element.Key;
                        entity.Physics.PositionPhysics.Position = new Vector2(200.0f, time_element.Key * -1.0f);
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

        public static CStageDefinition GenerateDefinitionFromStageEntities(CWorld world, string stage_filename)
        {
            CStageDefinition result = new CStageDefinition(stage_filename);

            // TODO: how can we handle all editor entity types well?
            foreach (CEntity entity in world.GetEntitiesOfType(typeof(Editor.CSpawnerEntity)))
            {
                Editor.CSpawnerEntity spawner = entity as Editor.CSpawnerEntity;
                result.AddElement(spawner.StartTime, spawner.StageElement);
            }

            return result;
        }
    }
}
