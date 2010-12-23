//
// ParticleEffectDefinition.cs
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
        PlayerStageEndShipTrail,
        PlayerShipShieldDamage,
        PlayerShipArmorDamage,
        PlayerShipDestroyed,
        BuildingDestroyedSmall,
        BuildingDestroyedBig,
        EnemyMissileTrail,
        EnemyDeathExplosion,
        EnemyBlackHoleCenter,        // TODO
        EnemyBlackHolePull,        // TODO
        EnemyTeleporterAppear,        // TODO
        EnemyCutterAbsorb,        // TODO
        SkillDashBurst,        // TODO
        SkillAbsorbBullet,        // TODO
        SkillReflectBullet,        // TODO
        SkillGroundSmash,        // TODO
        SkillArmorRepair,        // TODO
        SkillDetonationExplosion,        // TODO
        WeaponLaserHit,
        WeaponMissileHit,
        WeaponMissileTrail,
        WeaponPlasmaHit,
        WeaponSeekBombHit,
    }

    public struct SEffectDefinition
    {
        public EParticleType Type;
        public int Count;
        public CVisual Visual;
        public Vector2 Position;
        public Vector2 PositionVariationCircle;
        public Vector2 PositionVariationBox;
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

        public static SEffectDefinition MakeDefaultEffectDefinition()
        {
            return new SEffectDefinition()
            {
                Type = EParticleType.None,
                Count = 1,
                Visual = CParticleEffectManager.Dot,
                Position = Vector2.Zero,
                PositionVariationCircle = Vector2.Zero,
                PositionVariationBox = Vector2.Zero,
                PositionDelta = Vector2.Zero,
                PositionDeltaVariation = Vector2.One,
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
        public static Dictionary<EParticleType, SEffectDefinition> MakeDefinitions()
        {
            return new Dictionary<EParticleType, SEffectDefinition>()
            {

                {
                    EParticleType.None,
                    SEffectDefinition.MakeDefaultEffectDefinition()
                },

                {
                    EParticleType.PlayerStageEndShipTrail,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 8,
                        Visual = CParticleEffectManager.Dot,
                        Position = new Vector2(0.0f, 32.0f),
                        PositionVariationCircle = Vector2.Zero,
                        PositionVariationBox = new Vector2(16.0f, 14.0f),
                        PositionDelta = Vector2.UnitY,
                        PositionDeltaVariation = new Vector2(0.5f, 1.5f),
                        Scale = new Vector2(0.5f, 0.5f),
                        ScaleVariation = Vector2.Zero,
                        ScaleDelta = new Vector2(-0.01f, -0.01f),
                        ScaleDeltaVariation = Vector2.Zero,
                        Angle = 0.0f,
                        AngleVariation = 0.0f,
                        AngleDelta = 0.0f,
                        AngleDeltaVariation = 0.0f,
                        Color = Color.White,
                        Alpha = 1.0f,
                        AlphaVariation = 0.0f,
                        AlphaDelta = -0.05f,
                        AlphaDeltaVariation = 0.0f,
                        Lifetime = 60,
                        LifetimeVariation = 15,
                    }
                },

                {
                    EParticleType.PlayerShipShieldDamage,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 3,
                        Visual = CParticleEffectManager.Dot,
                        Position = new Vector2(12.0f, 8.0f),
                        PositionVariationCircle = new Vector2(24.0f, 24.0f),
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = Vector2.One,
                        Scale = new Vector2(0.5f, 0.5f),
                        ScaleVariation = Vector2.Zero,
                        ScaleDelta = new Vector2(-0.01f, -0.01f),
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
                    }
                },

                {
                    EParticleType.PlayerShipArmorDamage,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 8,
                        Visual = CParticleEffectManager.Triangle,
                        Position = new Vector2(8.0f, 8.0f),
                        PositionVariationCircle = new Vector2(10.0f, 10.0f),
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = new Vector2(2.5f, 2.5f),
                        Scale = new Vector2(1.4f, 1.4f),
                        ScaleVariation = new Vector2(0.2f, 0.2f),
                        ScaleDelta = new Vector2(-0.1f, -0.1f),
                        ScaleDeltaVariation = Vector2.Zero,
                        Angle = 0.0f,
                        AngleVariation = MathHelper.TwoPi,
                        AngleDelta = 0.0f,
                        AngleDeltaVariation = 0.1f,
                        Color = Color.White,
                        Alpha = 1.0f,
                        AlphaVariation = 0.0f,
                        AlphaDelta = 0.0f,
                        AlphaDeltaVariation = 0.0f,
                        Lifetime = 60,
                        LifetimeVariation = 15,
                    }
                },

                {
                    EParticleType.PlayerShipDestroyed,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 24,
                        Visual = CParticleEffectManager.Triangle,
                        Position = Vector2.Zero,
                        PositionVariationCircle = new Vector2(10.0f, 10.0f),
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = new Vector2(5.0f, 5.0f),
                        Scale = new Vector2(2.0f, 2.0f),
                        ScaleVariation = new Vector2(0.2f, 0.2f),
                        ScaleDelta = new Vector2(-0.05f, -0.05f),
                        ScaleDeltaVariation = Vector2.Zero,
                        Angle = 0.0f,
                        AngleVariation = MathHelper.TwoPi,
                        AngleDelta = 0.0f,
                        AngleDeltaVariation = 0.2f,
                        Color = Color.White,
                        Alpha = 1.0f,
                        AlphaVariation = 0.0f,
                        AlphaDelta = 0.0f,
                        AlphaDeltaVariation = 0.0f,
                        Lifetime = 60,
                        LifetimeVariation = 15,
                    }
                },

                {
                    EParticleType.BuildingDestroyedSmall,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 16,
                        Visual = CParticleEffectManager.Triangle,
                        Position = Vector2.Zero,
                        PositionVariationCircle = Vector2.Zero,
                        PositionVariationBox = new Vector2(32.0f, 32.0f),
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = new Vector2(4.5f, 4.5f),
                        Scale = new Vector2(1.5f, 1.5f),
                        ScaleVariation = new Vector2(0.2f, 0.2f),
                        ScaleDelta = new Vector2(-0.05f, -0.05f),
                        ScaleDeltaVariation = Vector2.Zero,
                        Angle = 0.0f,
                        AngleVariation = MathHelper.TwoPi,
                        AngleDelta = 0.0f,
                        AngleDeltaVariation = 0.1f,
                        Color = Color.White,
                        Alpha = 1.0f,
                        AlphaVariation = 0.0f,
                        AlphaDelta = 0.0f,
                        AlphaDeltaVariation = 0.0f,
                        Lifetime = 60,
                        LifetimeVariation = 15,
                    }
                },

                {
                    EParticleType.BuildingDestroyedBig,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 16,
                        Visual = CParticleEffectManager.Triangle,
                        Position = Vector2.Zero,
                        PositionVariationCircle = Vector2.Zero,
                        PositionVariationBox = new Vector2(64.0f, 64.0f),
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = new Vector2(7.5f, 7.5f),
                        Scale = new Vector2(2.0f, 2.0f),
                        ScaleVariation = new Vector2(0.2f, 0.2f),
                        ScaleDelta = new Vector2(-0.1f, -0.1f),
                        ScaleDeltaVariation = Vector2.Zero,
                        Angle = 0.0f,
                        AngleVariation = MathHelper.TwoPi,
                        AngleDelta = 0.0f,
                        AngleDeltaVariation = 0.1f,
                        Color = Color.White,
                        Alpha = 1.0f,
                        AlphaVariation = 0.0f,
                        AlphaDelta = 0.0f,
                        AlphaDeltaVariation = 0.0f,
                        Lifetime = 60,
                        LifetimeVariation = 15,
                    }
                },

                {
                    EParticleType.EnemyMissileTrail,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 2,
                        Visual = CParticleEffectManager.Dot,
                        Position = Vector2.Zero,
                        PositionVariationCircle = new Vector2(2.0f, 2.0f),
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = new Vector2(0.5f, 0.5f),
                        Scale = new Vector2(0.25f, 0.25f),
                        ScaleVariation = new Vector2(0.1f, 0.1f),
                        ScaleDelta = Vector2.Zero,
                        ScaleDeltaVariation = Vector2.Zero,
                        Angle = 0.0f,
                        AngleVariation = 0.0f,
                        AngleDelta = 0.0f,
                        AngleDeltaVariation = 0.0f,
                        Color = CEnemy.EnemyOrangeColor,
                        Alpha = 1.0f,
                        AlphaVariation = 0.0f,
                        AlphaDelta = -0.15f,
                        AlphaDeltaVariation = 0.0f,
                        Lifetime = 60,
                        LifetimeVariation = 15,
                    }
                },

                {
                    EParticleType.EnemyDeathExplosion,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 24,
                        Visual = CParticleEffectManager.Triangle,
                        Position = Vector2.Zero,
                        PositionVariationCircle = Vector2.Zero,
                        PositionVariationBox = new Vector2(16.0f, 16.0f),
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = new Vector2(5.0f, 5.0f),
                        Scale = new Vector2(1.5f, 1.5f),
                        ScaleVariation = new Vector2(0.2f, 0.2f),
                        ScaleDelta = new Vector2(-0.05f, -0.05f),
                        ScaleDeltaVariation = Vector2.Zero,
                        Angle = 0.0f,
                        AngleVariation = MathHelper.TwoPi,
                        AngleDelta = 0.0f,
                        AngleDeltaVariation = 0.2f,
                        Color = CEnemy.EnemyOrangeColor,
                        Alpha = 1.0f,
                        AlphaVariation = 0.0f,
                        AlphaDelta = 0.0f,
                        AlphaDeltaVariation = 0.0f,
                        Lifetime = 60,
                        LifetimeVariation = 0,
                    }
                },

                {
                    EParticleType.EnemyBlackHoleCenter,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 1,
                        Visual = CParticleEffectManager.Dot,
                        Position = Vector2.Zero,
                        PositionVariationCircle = Vector2.Zero,
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = Vector2.One,
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
                    }
                },

                {
                    EParticleType.EnemyBlackHolePull,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 1,
                        Visual = CParticleEffectManager.Dot,
                        Position = Vector2.Zero,
                        PositionVariationCircle = Vector2.Zero,
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = Vector2.One,
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
                    }
                },

                {
                    EParticleType.EnemyTeleporterAppear,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 1,
                        Visual = CParticleEffectManager.Dot,
                        Position = Vector2.Zero,
                        PositionVariationCircle = Vector2.Zero,
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = Vector2.One,
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
                    }
                },

                {
                    EParticleType.EnemyCutterAbsorb,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 1,
                        Visual = CParticleEffectManager.Dot,
                        Position = Vector2.Zero,
                        PositionVariationCircle = Vector2.Zero,
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = Vector2.One,
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
                    }
                },

                {
                    EParticleType.SkillDashBurst,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 1,
                        Visual = CParticleEffectManager.Dot,
                        Position = Vector2.Zero,
                        PositionVariationCircle = Vector2.Zero,
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = Vector2.One,
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
                    }
                },

                {
                    EParticleType.SkillAbsorbBullet,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 1,
                        Visual = CParticleEffectManager.Dot,
                        Position = Vector2.Zero,
                        PositionVariationCircle = Vector2.Zero,
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = Vector2.One,
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
                    }
                },

                {
                    EParticleType.SkillReflectBullet,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 1,
                        Visual = CParticleEffectManager.Dot,
                        Position = Vector2.Zero,
                        PositionVariationCircle = Vector2.Zero,
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = Vector2.One,
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
                    }
                },

                {
                    EParticleType.SkillGroundSmash,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 1,
                        Visual = CParticleEffectManager.Dot,
                        Position = Vector2.Zero,
                        PositionVariationCircle = Vector2.Zero,
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = Vector2.One,
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
                    }
                },

                {
                    EParticleType.SkillArmorRepair,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 1,
                        Visual = CParticleEffectManager.Dot,
                        Position = Vector2.Zero,
                        PositionVariationCircle = Vector2.Zero,
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = Vector2.One,
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
                    }
                },

                {
                    EParticleType.SkillDetonationExplosion,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 1,
                        Visual = CParticleEffectManager.Dot,
                        Position = Vector2.Zero,
                        PositionVariationCircle = Vector2.Zero,
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = Vector2.One,
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
                    }
                },

                {
                    EParticleType.WeaponLaserHit,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 4,
                        Visual = CParticleEffectManager.Dot,
                        Position = Vector2.Zero,
                        PositionVariationCircle = new Vector2(3.0f, 3.0f),
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = Vector2.One,
                        Scale = new Vector2(0.3f, 0.3f),
                        ScaleVariation = Vector2.Zero,
                        ScaleDelta = new Vector2(-0.005f, -0.005f),
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
                    }
                },

                {
                    EParticleType.WeaponMissileHit,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 4,
                        Visual = CParticleEffectManager.Dot,
                        Position = Vector2.Zero,
                        PositionVariationCircle = new Vector2(3.0f, 3.0f),
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = new Vector2(0.4f, 0.4f),
                        Scale = new Vector2(2.0f, 2.0f),
                        ScaleVariation = Vector2.Zero,
                        ScaleDelta = new Vector2(-0.15f, -0.15f),
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
                    }
                },

                {
                    EParticleType.WeaponMissileTrail,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 4,
                        Visual = CParticleEffectManager.Dot,
                        Position = Vector2.Zero,
                        PositionVariationCircle = Vector2.Zero,
                        PositionVariationBox = new Vector2(4.0f, 16.0f),
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = new Vector2(0.2f, 0.2f),
                        Scale = new Vector2(0.35f, 0.35f),
                        ScaleVariation = new Vector2(0.1f, 0.1f),
                        ScaleDelta = new Vector2(-0.02f, -0.02f),
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
                    }
                },

                {
                    EParticleType.WeaponPlasmaHit,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 4,
                        Visual = CParticleEffectManager.Dot,
                        Position = Vector2.Zero,
                        PositionVariationCircle = new Vector2(3.0f, 3.0f),
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = Vector2.One,
                        Scale = new Vector2(0.6f, 0.6f),
                        ScaleVariation = Vector2.Zero,
                        ScaleDelta = new Vector2(-0.01f, -0.01f),
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
                    }
                },

                {
                    EParticleType.WeaponSeekBombHit,
                    new SEffectDefinition()
                    {
                        Type = EParticleType.None,
                        Count = 2,
                        Visual = CParticleEffectManager.Dot,
                        Position = Vector2.Zero,
                        PositionVariationCircle = new Vector2(3.0f, 3.0f),
                        PositionVariationBox = Vector2.Zero,
                        PositionDelta = Vector2.Zero,
                        PositionDeltaVariation = new Vector2(0.4f, 0.4f),
                        Scale = new Vector2(2.0f, 2.0f),
                        ScaleVariation = Vector2.Zero,
                        ScaleDelta = new Vector2(-0.15f, -0.15f),
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
                    }
                },
            };
        }
    }
}
