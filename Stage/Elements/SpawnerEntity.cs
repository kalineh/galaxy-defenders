//
// SpawnerEntity.cs
//

using System;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CSpawnerEntity
        : CStageElement
    {
        [EditorAttribute(typeof(Editor.CEntityTypeSelector), typeof(UITypeEditor))]        
        //[TypeConverter(typeof(Editor.CEntityTypeConverter))]
        public Type Type { get; set; }
        public int SpawnCount { get; set; }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public CSpawnTimer SpawnTimer { get; set; }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public CSpawnPosition SpawnPosition { get; set; }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public CMover CustomMover { get; set; }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public CSpawnerCustomElement CustomElement { get; set; }

        public override void Update(CWorld world)
        {
            int create = SpawnTimer.Update(world);

            while (create-- > 0)
            {
                Spawn(world);
            }
        }

        public override bool IsExpired()
        {
            return SpawnCount <= 0;
        }

        private void Spawn(CWorld world)
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
                SpawnCount -= 1;
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

    /*
    public class CSpawnerCustomMover
        : CSpawnerCustomElement
    {
        public CMover Mover { get; set; }

        public override void Customize(CEntity entity)
        {
            entity.Mover = Mover;
        }
    }
    */
}


