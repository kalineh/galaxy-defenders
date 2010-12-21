//
// ParticleEffectConfiguration.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Galaxy
{
    public enum EParticleType
    {
        None,
        PlayerMissileExplosion,
        EnemyDeathExplosion,
    }

    public struct SEffectConfiguration
    {
        public EParticleType Type;
        public int Count;
        public CVisual Visual;
        public Vector2 Position;
        public Vector2 PositionVariation;
        public Vector2 PositionDelta;
        public Vector2 PositionDeltaVariation;
        public Vector2 Scale;
        public Vector2 ScaleVariation;
        public Vector2 ScaleDelta;
        public Vector2 ScaleDeltaVariation;
        public float Angle;
        public float AngleVariation;
        public float AngleDelta;
        public float AngleDeltaVariation;
        public Color Color;
        public float Alpha;
        public float AlphaVariation;
        public float AlphaDelta;
        public float AlphaDeltaVariation;
        public int Lifetime;
        public int LifetimeVariation;

        public static SEffectConfiguration MakeDefaultEffectConfiguration()
        {
            return new SEffectConfiguration()
            {
                Type = EParticleType.None,
                Count = 0,
                Visual = null,
                Position = Vector2.Zero,
                PositionVariation = Vector2.Zero,
                PositionDelta = Vector2.Zero,
                PositionDeltaVariation = Vector2.Zero,
                Scale = Vector2.One,
                ScaleVariation = Vector2.Zero,
                ScaleDelta = Vector2.Zero,
                ScaleDeltaVariation = Vector2.Zero,
                Angle = 0.0f,
                AngleVariation = 0.0f,
                AngleDelta = 0.0f,
                AngleDeltaVariation = 0.0f,
                Color = Color.White,
                Alpha = 1.0f,
                AlphaVariation = 0.0f,
                AlphaDelta = 0.0f,
                AlphaDeltaVariation = 0.0f,
                Lifetime = 60,
                LifetimeVariation = 15,
            };
        }
    }

    public class CGalaxyEffects
    {
        public static Dictionary<EParticleType, SEffectConfiguration> MakeConfigurations()
        {
            return new Dictionary<EParticleType, SEffectConfiguration>()
            {

                {
                    EParticleType.None,
                    SEffectConfiguration.MakeDefaultEffectConfiguration()
                },

                {
                    EParticleType.PlayerMissileExplosion,
                    new SEffectConfiguration()
                    {
                        
                    }
                },

                {
                    EParticleType.EnemyDeathExplosion,
                    new SEffectConfiguration()
                    {
                        Visual = CParticleEffectManager.Triangle,
                        Count = 256,
                        Position = Vector2.Zero,
                        PositionDeltaVariation = new Vector2(8.0f, 8.0f),
                        Alpha = 0.8f,
                        AngleDeltaVariation = 0.1f,
                        Scale = new Vector2(0.6f, 0.6f),
                        ScaleVariation = new Vector2(-0.15f, -0.15f),
                        ScaleDelta = new Vector2(-0.02f, -0.02f),
                        Lifetime = 22,
                        Color = Color.White,
                    }
                },
            };
        }
    }
}
