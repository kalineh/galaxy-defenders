//
// EditorEntitySpawnerEntity.cs
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    /// <summary>
    /// Editor entity for CSpawnerEntity.
    /// </summary>
    //[TypeConverter(typeof(CEditorSpawnerEntityConverter))]
    public class CEditorEntityAsteroidField
        : CEditorEntityBase
    {
        [CategoryAttribute("AsteroidField")]
        public int SpawnCount { get; set; }
        [CategoryAttribute("AsteroidField")]
        public float Frequency { get; set; }
        [CategoryAttribute("AsteroidField")]
        public bool Big { get; set; }

        // TODO: rearrange so the stageelement constructor is the core constructor
        // TODO: the other constructors can generate a default StageElement value and load from that
        // TODO: this means that all the default values will be in the StageElement itself (and not duplicated here)
        public CEditorEntityAsteroidField(CWorld world, Type type, Vector2 position)
            : base(world, position)
        {
            SpawnCount = 1;
            Frequency = 0.001f;

            Visual = CVisual.MakeLabel(world, "Asteroid Field", Color.Blue);
        }

        public CEditorEntityAsteroidField(CWorld world, Vector2 position)
            : this(world, typeof(CAsteroid), position)
        {
        }

        public CEditorEntityAsteroidField(CWorld world, CStageElement element)
            : this(world, ((CAsteroidField)element).Type, element.Position)
        {
            // load from element data
            CAsteroidField field = element as CAsteroidField;

            SpawnCount = field.SpawnCount;
            Frequency = ((CSpawnTimerRandom)field.SpawnTimer).Frequency;
            Big = field.CustomElement is CSpawnerCustomBigAsteroid;
        }

        public override CStageElement GenerateStageElement()
        {
            CAsteroidField result = new CAsteroidField()
            {
                Type = typeof(CAsteroid),
                Position = Position,
                SpawnPosition = new CSpawnPositionRandom() { BasePosition = Position },
                SpawnCount = SpawnCount,
                SpawnTimer = new CSpawnTimerRandom() { Frequency = Frequency, IncreaseRate = 0.0f },
                CustomMover = null,
                CustomElement = Big ? new CSpawnerCustomBigAsteroid() : new CSpawnerCustomAsteroid(),
            };
            return result;
        }
    }
}
