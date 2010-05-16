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

        public CEditorEntitySpawnerEntity(CWorld world, Type type, Vector2 position)
            : base(world, position)
        {
            Type = type;
            CEntity visual_get = Activator.CreateInstance(type, new object[] { world, Vector2.Zero }) as CEntity;
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, visual_get.Visual.Texture.Name), visual_get.Visual.Color);
        }

        public CEditorEntitySpawnerEntity(CWorld world, Vector2 position)
            : this(world, typeof(CBonus), position)
        {
        }

        public CEditorEntitySpawnerEntity(CWorld world, CStageElement element)
            : this(world, ((CStageElementSpawnerEntity)element).Type, element.Position)
        {
        }

        public override CStageElement GenerateStageElement()
        {
            System.Diagnostics.Debug.Assert(false, "replaced with spawner enemy");
            
            Galaxy.CStageElementSpawnerEntity result = new CStageElementSpawnerEntity()
            {
                Type = Type,
                Position = Position,
                SpawnPosition = new CSpawnPositionFixed() { Position = Position },
                CustomElement = null,
            };

            return result;
        }
    }
}
