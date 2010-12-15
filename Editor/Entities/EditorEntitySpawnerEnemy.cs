//
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
        public string MoverPresetName { get; set; }

        [CategoryAttribute("Mover")]
        public float MoverSpeedMultiplier { get; set; }

        [CategoryAttribute("Mover")]
        public float MoverTransitionMultiplier { get; set; }

        [CategoryAttribute("Bonus")]
        public int Coins { get; set; }

        [CategoryAttribute("Bonus")]
        public bool Powerup { get; set; }

        public CEditorEntitySpawnerEnemy(CWorld world, Type type, Vector2 position)
            : base(world, position)
        {
            Type = type;
            Coins = 1;
            CEntity sample_instance = Activator.CreateInstance(type) as CEntity;
            sample_instance.Initialize(world);
            sample_instance.Physics.PositionPhysics.Position = position;
            Visual = new CVisual(world.Game, CContent.LoadTexture2D(world.Game, sample_instance.Visual.Texture.Name), sample_instance.Visual.Color);
            Visual.TileX = sample_instance.Visual.TileX;
            Visual.TileY = sample_instance.Visual.TileY;
            Visual.AnimationSpeed = sample_instance.Visual.AnimationSpeed;
            
            MoverPresetName = "None";
            MoverSpeedMultiplier = 1.0f;
            MoverTransitionMultiplier = 1.0f;
        }

        public CEditorEntitySpawnerEnemy(CWorld world, Vector2 position)
            : this(world, typeof(CBall), position)
        {
        }

        public CEditorEntitySpawnerEnemy(CWorld world, CStageElement element)
            : this(world, ((CStageElementSpawnerEnemy)element).Type, element.Position)
        {
            CStageElementSpawnerEnemy spawner = element as CStageElementSpawnerEnemy;

            MoverPresetName = spawner.MoverPresetName;
            MoverSpeedMultiplier = spawner.MoverSpeedMultiplier;
            MoverTransitionMultiplier = spawner.MoverTransitionMultiplier;

            Coins = spawner.Coins;
            Powerup = spawner.Powerup;
        }

        public override CEditorEntityPreview GeneratePreviewEntity()
        {
            if (MoverPresetName == null)
                return null;
            if (MoverSpeedMultiplier == 0.0f)
                return null;
            if (MoverPresetName != "")
                return null;
            if (MoverPresetName != "None")
                return null;
            if (MoverPresetName != "IgnoreCamera")
                return null;

            return new CEditorEntityPreview(World, this)
            {
                Mover = CMoverPresets.FromName(MoverPresetName, MoverSpeedMultiplier, MoverTransitionMultiplier),
            };
        }

        public override CStageElement GenerateStageElement()
        {
            Galaxy.CStageElementSpawnerEnemy result = new CStageElementSpawnerEnemy()
            {
                Type = Type,
                Position = Position,
                SpawnPosition = new CSpawnPositionFixed() { Position = Position },
                MoverPresetName = MoverPresetName,
                MoverSpeedMultiplier = MoverSpeedMultiplier,
                MoverTransitionMultiplier = MoverTransitionMultiplier,
                CustomElement = null,
                Coins = Coins,
                Powerup = Powerup,
            };

            return result;
        }
    }
}
