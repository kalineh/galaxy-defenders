//
// EditorStageDefinitionProcessing.cs
//

// TODO: think of a better name for this file

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    using XnaColor = Microsoft.Xna.Framework.Graphics.Color;

    public class CStageGenerate
    {
        public static void GenerateStageEntitiesFromDefinition(CWorld world, CStageDefinition definition)
        {
            // TODO: there shouldnt be a ship really (replace with player spawn? (should always be center-bottom though really))
            CShip ship = CShipFactory.GenerateShip(world, CSaveData.GetCurrentProfile(), PlayerIndex.One);
            ship.Physics.PositionPhysics.Position = world.Game.PlayerSpawnPosition;
            world.EntityAdd(ship);

            foreach (KeyValuePair<int, List<CStageElement>> time_element in definition.Elements)
            {
                foreach (CStageElement element in time_element.Value)
                {
                    CEntity entity = GenerateEntity(world, time_element.Key, element);
                    world.EntityAdd(entity);
                }
            }
        }

        private static CEntity GenerateEntity(CWorld world, int time, CStageElement element)
        {
            Type src = element.GetType();
            Type dst = CEditorEntityTypes.ElementToEditorEntityMapping[src]; 

            if (dst == null)
            {
                Console.WriteLine(String.Format("CStageGenerate.GenerateEntity(): Unhandled element type: {0}", src.Name));
                return null;
            }

            CEditorEntityBase entity = Activator.CreateInstance(dst, new object[] { world, element }) as CEditorEntityBase;
            return entity;
        }

        public static CStageDefinition GenerateDefinitionFromStageEntities(CWorld world, string name)
        {
            CStageDefinition result = new CStageDefinition(name);

            // rollover existing stage properties
            result.ScrollSpeed = world.Game.StageDefinition.ScrollSpeed;
            result.BackgroundSceneryName = world.Game.StageDefinition.BackgroundSceneryName;
            result.ForegroundSceneryName = world.Game.StageDefinition.ForegroundSceneryName;
            result.MusicName = world.Game.StageDefinition.MusicName;

            // from EDITOR ENTITY to STAGE ELEMENT

            var entities = from e in world.GetEntities() where e is CEditorEntityBase select e;
            foreach (CEditorEntityBase entity in entities)
            {
                Type src = entity.GetType();
                Type dst = CEditorEntityTypes.EditorEntityToElementMapping[src];

                if (dst == null)
                {
                    Console.WriteLine(String.Format("CStageGenerate.GenerateDefinitionsFromStageEntities(): Unhandled entity type: {0}", src.Name));
                    continue;
                }

                CStageElement element = entity.GenerateStageElement();
                result.AddElement(0, element);
            }

            return result;
        }
    }
}
