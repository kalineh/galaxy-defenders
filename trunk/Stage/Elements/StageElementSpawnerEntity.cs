//
// StageElementSpawnerEntity.cs
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CStageElementSpawnerEntity
        : CStageElement
    {
        public Type Type { get; set; }
        public CSpawnPosition SpawnPosition { get; set; }
        public CMover CustomMover { get; set; }
        public CSpawnerCustomElement CustomElement { get; set; }

        public override void Update(CWorld world)
        {
            Spawn(world);
        }

        public override bool IsExpired()
        {
            return true;
        }

        protected CEntity Spawn(CWorld world)
        {
            Vector2 spawn_position = SpawnPosition.GetSpawnPosition(world);

            try
            {
                CEntity entity = Activator.CreateInstance(Type, new object[] { world, spawn_position }) as CEntity;

                if (CustomMover != null)
                {
                    entity.Mover = CustomMover;
                }

                if (CustomElement != null)
                {
                    CustomElement.Customize(entity);
                }

                world.EntityAdd(entity);

                return entity;
            }
            catch (Exception exception)
            {
                throw exception.InnerException;
            }
        }
    }

    public abstract class CSpawnerCustomElement
    {
        public abstract void Customize(CEntity entity);
    }

    public class CSpawnerCustomCode
        : CSpawnerCustomElement
    {
        public delegate void CustomizeFunction(CEntity entity);

        public CustomizeFunction Code { get; set; }

        public override void Customize(CEntity entity)
        {
            Code.Invoke(entity);
        }
    }
}


