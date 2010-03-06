﻿//
// EditorEntitySpawnerEnemy.cs
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
    /// Editor entity for CSpawnerEnemy
    /// </summary>
    [TypeConverter(typeof(CEditorConverterGenerated))]
    public class CEditorEntitySpawnerEnemy
        : CEditorEntityBase
    {
        [CategoryAttribute("Core")]
        [EditorAttribute(typeof(CTypeSelector<CEnemyTypes>), typeof(UITypeEditor))]
        public Type Type { get; set; }

        [CategoryAttribute("Mover")]
        [EditorAttribute(typeof(CEntityMoverPresetSelector), typeof(UITypeEditor))]
        [TypeConverter(typeof(CEntityMoverTypeConverter))]
        public new CMover Mover { get; set; }

        [CategoryAttribute("Mover")]
        public float MoveSpeed { get; set; }

        // TODO: replace me and use positional system in-game
        public int StartTime { get; set; }

        [CategoryAttribute("Bonus")]
        public int Coins { get; set; }

        [CategoryAttribute("Bonus")]
        public bool Powerup { get; set; }

        public CEditorEntitySpawnerEnemy(CWorld world, Type type, Vector2 position)
            : base(world, position)
        {
            Type = type;
            CEntity visual_get = Activator.CreateInstance(type, new object[] { world, Vector2.Zero }) as CEntity;
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, visual_get.Visual.Texture.Name), visual_get.Visual.Color);
            Visual.TileX = visual_get.Visual.TileX;
            Visual.TileY = visual_get.Visual.TileY;
            Visual.AnimationSpeed = visual_get.Visual.AnimationSpeed;
            Mover = CMoverPresets.MoveDown(0.0f);
        }

        public CEditorEntitySpawnerEnemy(CWorld world, Vector2 position)
            : this(world, typeof(CBall), position)
        {
        }

        public CEditorEntitySpawnerEnemy(CWorld world, CStageElement element)
            : this(world, ((CStageElementSpawnerEnemy)element).Type, element.Position)
        {
            CStageElementSpawnerEnemy spawner = element as CStageElementSpawnerEnemy;

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

            Coins = spawner.Coins;
            Powerup = spawner.Powerup;
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
            Galaxy.CStageElementSpawnerEnemy result = new CStageElementSpawnerEnemy()
            {
                Type = Type,
                Position = Position,
                SpawnPosition = new CSpawnPositionFixed() { Position = Position },
                CustomMover = Mover,
                CustomElement = null,
                Coins = Coins,
                Powerup = Powerup,
            };

            // TODO: here is where we need to set the speed multiplier on the SpawnerEnemy from the mover
            if (Mover.GetType() == typeof(CMoverSequence))
            {
                CMoverSequence mover = result.CustomMover as CMoverSequence;
                mover.SpeedMultiplier = MoveSpeed;
            }

            // TODO: here is where we need to set the speed multiplier on the SpawnerEnemy from the mover
            if (Mover.GetType() == typeof(CMoverFixedVelocity))
            {
                CMoverFixedVelocity mover = result.CustomMover as CMoverFixedVelocity;
                mover.SpeedMultiplier = MoveSpeed;
            }

            return result;
        }
    }
}
