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
        : CEditorEntitySpawnerEntity
    {
        [CategoryAttribute("Bonus")]
        public int Coins { get; set; }

        [CategoryAttribute("Bonus")]
        public bool Powerup { get; set; }

        public CEditorEntitySpawnerEnemy(CWorld world, Type type, Vector2 position)
            : base(world, type, position)
        {
        }

        public CEditorEntitySpawnerEnemy(CWorld world, Vector2 position)
            : base(world, position)
        {
        }

        public CEditorEntitySpawnerEnemy(CWorld world, CStageElement element)
            : base(world, element)
        {
            CStageElementSpawnerEnemy enemy = element as CStageElementSpawnerEnemy;
            Coins = enemy.Coins;
            Powerup = enemy.Powerup;
        }

    }
}
