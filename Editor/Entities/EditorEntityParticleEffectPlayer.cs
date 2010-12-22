//
// EditorEntityParticleEffectPlayer.cs
//

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    [TypeConverter(typeof(CEditorConverterGenerated))]
    public class CEditorEntityParticleEffectPlayer
        : CEditorEntityBase
    {
        [CategoryAttribute("Core")]
        public Galaxy.EParticleType Type { get; set; }
        [CategoryAttribute("Core")]
        public float Cycle { get; set; }

        public CEditorEntityParticleEffectPlayer(CWorld world, Vector2 position)
            : base(world, position)
        {
            Type = EParticleType.None;
            Visual = CVisual.MakeLabel(world.Game, Type.ToString());
            Cycle = 1.0f;
        }

        public override void Update()
        {
            base.Update();

            if (Visual.DebugText != Type.ToString())
                Visual = CVisual.MakeLabel(World.Game, Type.ToString());
        }

        public CEditorEntityParticleEffectPlayer(CWorld world, CStageElement element)
            : this(world, element.Position)
        {
            Type = (element as CStageElementParticleEffectPlayer).Type;
            Cycle = (element as CStageElementParticleEffectPlayer).Cycle;
        }

        public override CStageElement GenerateStageElement()
        {
            return new CStageElementParticleEffectPlayer()
            {
                Type = Type,
                Cycle = Cycle,
                Position = Position,
            };
        }
    }
}
