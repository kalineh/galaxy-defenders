﻿//
// StageElementSpawnerEntity.cs
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using System.Reflection;

namespace Galaxy
{
    public class CStageElementSpawnerEntity
        : CStageElement
    {
        public Type Type { get; set; }
        public CSpawnPosition SpawnPosition { get; set; }
        public string MoverPresetName { get; set; }
        public float MoverSpeedMultiplier { get; set; }
        public float MoverTransitionMultiplier { get; set; }
        public CSpawnerCustomElement CustomElement { get; set; }

        public CStageElementSpawnerEntity()
        {
            MoverPresetName = "IgnoreCamera";
            MoverSpeedMultiplier = 1.0f;
            MoverTransitionMultiplier = 1.0f;
        }

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
#if XBOX360
                CEntity entity = Galaxy.ActivatorExtensions.CreateInstance(Type, new object[] { world, spawn_position }) as CEntity;
#else
                CEntity entity = Activator.CreateInstance(Type, new object[] { world, spawn_position }) as CEntity;
#endif

                if (MoverPresetName != "")
                {
                    entity.Mover = CMoverPresets.FromName(MoverPresetName, MoverSpeedMultiplier, MoverTransitionMultiplier);
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

