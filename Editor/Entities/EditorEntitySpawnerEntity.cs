//
// EditorEntitySpawnerEntity.cs
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    /// <summary>
    /// Editor entity for CStageElementSpawnerEntity.
    /// </summary>
    [TypeConverter(typeof(CEditorConverterGenerated))]
    public class CEditorEntitySpawnerEntity
        : CEditorEntityBase
    {
        [CategoryAttribute("Core")]
        [EditorAttribute(typeof(CTypeSelector<CEntityTypes>), typeof(UITypeEditor))]
        public Type Type { get; set; }

        [CategoryAttribute("Mover")]
        [EditorAttribute(typeof(CEntityMoverPresetSelector), typeof(UITypeEditor))]
        [TypeConverter(typeof(CEntityMoverTypeConverter))]
        public new CMover Mover { get; set; }

        [CategoryAttribute("Mover")]
        public float SpeedMultiplier { get; set; }

        public CEditorEntitySpawnerEntity(CWorld world, Type type, Vector2 position)
            : base(world, position)
        {
            Type = type;
            CEntity visual_get = Activator.CreateInstance(type, new object[] { world, Vector2.Zero }) as CEntity;
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, visual_get.Visual.Texture.Name), visual_get.Visual.Color);
            Mover = CMoverPresets.IgnoreCamera(0.0f);
        }

        public CEditorEntitySpawnerEntity(CWorld world, Vector2 position)
            : this(world, typeof(CBonus), position)
        {
        }

        public CEditorEntitySpawnerEntity(CWorld world, CStageElement element)
            : this(world, ((CStageElementSpawnerEntity)element).Type, element.Position)
        {
            CStageElementSpawnerEntity spawner = element as CStageElementSpawnerEntity;

            // TODO: not this hack
            Mover = spawner.CustomMover;
            SpeedMultiplier = world.Game.StageDefinition.ScrollSpeed;
            if (Mover as Galaxy.CMoverSequence != null)
            {
                SpeedMultiplier = ((Galaxy.CMoverSequence)Mover).SpeedMultiplier;
            }
            if (Mover as Galaxy.CMoverFixedVelocity != null)
            {
                SpeedMultiplier = ((Galaxy.CMoverFixedVelocity)Mover).SpeedMultiplier;
            }
            if (Mover as Galaxy.CMoverIgnoreCamera != null)
            {
                SpeedMultiplier = 0.0f;
            }
        }

        public override CStageElement GenerateStageElement()
        {
            Galaxy.CStageElementSpawnerEntity result = new CStageElementSpawnerEntity()
            {
                Type = Type,
                Position = Position,
                SpawnPosition = new CSpawnPositionFixed() { Position = Position },
                CustomMover = Mover,
                CustomElement = null,
            };

            // TODO: here is where we need to set the speed multiplier on the SpawnerEntity from the mover
            if (Mover.GetType() == typeof(CMoverSequence))
            {
                CMoverSequence mover = result.CustomMover as CMoverSequence;
                mover.SpeedMultiplier = SpeedMultiplier;
            }

            // TODO: here is where we need to set the speed multiplier on the SpawnerEntity from the mover
            if (Mover.GetType() == typeof(CMoverFixedVelocity))
            {
                CMoverFixedVelocity mover = result.CustomMover as CMoverFixedVelocity;
                mover.SpeedMultiplier = SpeedMultiplier;
            }

            // TODO: here is where we need to set the speed multiplier on the SpawnerEntity from the mover
            if (Mover.GetType() == typeof(CMoverSin))
            {
                CMoverSin mover = result.CustomMover as CMoverSin;
                mover.SpeedMultiplier = SpeedMultiplier;
            }

            return result;
        }
    }
}
