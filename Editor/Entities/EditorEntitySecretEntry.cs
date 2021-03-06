﻿//
// EditorEntitySecretEntry.cs
//

using System;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    [TypeConverter(typeof(CEditorConverterGenerated))]
    public class CEditorEntitySecretEntry
        : CEditorEntityBase
    {
        [CategoryAttribute("Core")]
        public string SecretStage { get; set; }

        public CEditorEntitySecretEntry(CWorld world, Vector2 position)
            : base(world, position)
        {
            Visual = CVisual.MakeLabel(world.Game, "SECRET", Color.Blue);
            SecretStage = "BonusStage1";
        }

        public CEditorEntitySecretEntry(CWorld world, CStageElement element)
            : this(world, element.Position)
        {
            CStageElementSecretEntry secret = element as CStageElementSecretEntry;
            SecretStage = secret.SecretStage;
        }

        public override CStageElement GenerateStageElement()
        {
            return new CStageElementSecretEntry() { Position = Position, SecretStage = SecretStage };
        }
    }
}
