//
// StageElementParticleEffectPlayer.cs
//

using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System;

namespace Galaxy
{
    public class CStageElementParticleEffectPlayer
        : CStageElement
    {
        public EParticleType Type { get; set; }
        public float Cycle { get; set; }
        public bool Expired { get; set; }
        private int CycleFrame { get; set; }

        public override void Update(CWorld world)
        {
            Expired = world.GameCamera.IsOffBottom(Position, 128.0f);

            if (CycleFrame == 0)
            {
                CycleFrame = (int)(Cycle * 60.0f);
                if (CycleFrame == 0)
                {
                    Expired = true;
                    return;
                }
            }

            if (world.Game.GameFrame % CycleFrame != 0)
                return;

            world.ParticleEffects.Spawn(Type, Position);
        }

        public override bool IsExpired()
        {
            return Expired;
        }
    }
}
