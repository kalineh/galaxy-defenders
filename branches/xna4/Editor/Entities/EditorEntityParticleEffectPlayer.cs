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
            //Visual = CVisual.MakeLabel(world.Game, Type.ToString());
            Visual = CVisual.MakeLabel(world.Game, "PFX");
            Cycle = 1.0f;
        }

        public override void Update()
        {
            base.Update();

            //if (Visual.DebugText != Type.ToString())
                //Visual = CVisual.MakeLabel(World.Game, Type.ToString());

            int cycle_frame = (int)(Cycle * 60.0f);
            if (cycle_frame != 0)
            {
                if (World.Game.GameFrame % cycle_frame == 0)
                    World.ParticleEffects.Spawn(Type, Position);     
            }
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
