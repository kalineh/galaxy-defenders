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
        [EditorAttribute(typeof(Editor.CEntityTypeSelector), typeof(UITypeEditor))]
        public Type Type { get; set; }

        [CategoryAttribute("Mover")]
        [EditorAttribute(typeof(Editor.CEntityMoverPresetSelector), typeof(UITypeEditor))]
        public new CMover Mover { get; set; }

        [CategoryAttribute("Mover")]
        public float MoveSpeed { get; set; }

        // TODO: replace me and use positional system in-game
        public int StartTime { get; set; }

        public CEditorEntitySpawnerEntity(CWorld world, Type type, Vector2 position)
            : base(world, position)
        {
            Type = type;
            CEntity visual_get = Activator.CreateInstance(type, new object[] { world, Vector2.Zero }) as CEntity;
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, visual_get.Visual.Texture.Name), visual_get.Visual.Color);
            Mover = CMoverPresets.MoveDown(1.0f);
            MoveSpeed = 1.0f;
        }

        public CEditorEntitySpawnerEntity(CWorld world, Vector2 position)
            : this(world, typeof(CAsteroid), position)
        {
        }

        public CEditorEntitySpawnerEntity(CWorld world, CStageElement element)
            : this(world, ((CStageElementSpawnerEntity)element).Type, element.Position)
        {
            CStageElementSpawnerEntity spawner = element as CStageElementSpawnerEntity;

            // TODO: not this hack
            Mover = spawner.CustomMover;
            MoveSpeed = world.Game.StageDefinition.ScrollSpeed;
            if (Mover as Galaxy.CMoverSequence != null)
            {
                MoveSpeed = ((Galaxy.CMoverSequence)Mover).SpeedMultiplier;
            }
            if (Mover as Galaxy.CMoverFixedVelocity != null)
            {
                MoveSpeed = ((Galaxy.CMoverFixedVelocity)Mover).SpeedMultiplier;
            }
        }

        public override CEditorEntityPreview GeneratePreviewEntity()
        {
            if (MoveSpeed == 0.0f)
                return null;

            if (Mover == null)
                return null;

            return new CEditorEntityPreview(World, this) { Mover = Mover };
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
                mover.SpeedMultiplier = MoveSpeed;
            }

            // TODO: here is where we need to set the speed multiplier on the SpawnerEntity from the mover
            if (Mover.GetType() == typeof(CMoverFixedVelocity))
            {
                CMoverFixedVelocity mover = result.CustomMover as CMoverFixedVelocity;
                mover.SpeedMultiplier = MoveSpeed;
            }

            return result;
        }
    }
}
