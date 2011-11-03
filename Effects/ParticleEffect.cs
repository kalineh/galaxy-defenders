//
// ParticleEffect.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Galaxy
{
    public class CParticleEffectManager
    {
        public static CParticleEffectManager Instance { get; set; }

        public CWorld World { get; set; }
        public List<CParticle> Particles { get; set; }
        public List<CParticle> Cache { get; set; }
        public List<SEffectDefinition> Definitions { get; set; }
        public static CVisual Dot { get; set; }
        public static CVisual Triangle { get; set; }

        public static void CreateParticleEffectManager(CGalaxy game)
        {
            Instance = new CParticleEffectManager(game);
            Instance.Initialize(CGalaxyEffects.MakeDefinitions());
        }

        public CParticleEffectManager(CGalaxy game)
        {
            Particles = new List<CParticle>();
            Cache = new List<CParticle>(8192);
            for (int i = 0; i < 8192; ++i)
                Cache.Add(new CParticle());
            Definitions = new List<SEffectDefinition>(64);

            Dot = CVisual.MakeSpriteUncached(game, "Textures/Effects/DotParticle");
            Triangle = CVisual.MakeSpriteUncached(game, "Textures/Effects/TriangleParticle");
            Dot.Recache();
            Triangle.Recache();
        }

        public void Initialize(Dictionary<EParticleType, SEffectDefinition> definitions)
        {
            Definitions.Clear();
            for (int i = 0; i < definitions.Count; ++i)
                Definitions.Add(SEffectDefinition.MakeDefaultEffectDefinition());
            foreach (KeyValuePair<EParticleType, SEffectDefinition> kv in definitions)
                Definitions[(int)kv.Key] = kv.Value;
        }

        public void ResetWorld(CWorld world)
        {
            World = world;

            // force all alive particles to end lifetime
            for (int i = 0; i < Particles.Count; ++i)
            {
                CParticle particle = Particles[i];
                particle.Frame = particle.Lifetime;
            }

            // update to move particles back to cache
            Update();
        }

        public void Update()
        {
            int count = Particles.Count;
            for (int i = 0; i < Particles.Count; )
            {
                CParticle particle = Particles[i];
                particle.Update();

                if (particle.IsAlive() == false)
                {
                    if (i == Particles.Count)
                    {
                        Particles.Clear();
                        return;
                    }

                    Cache.Add(particle);
                    Particles[i] = Particles[Particles.Count - 1];
                    Particles.RemoveAt(Particles.Count - 1);
                }
                else
                {
                    ++i;
                }
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            for (int i = 0; i < Particles.Count; ++i)
            {
                CParticle particle = Particles[i];

                // TODO: faster
                Color color = particle.Color;
                color.A = (byte)(particle.Alpha * 255.0f);

                //sprite_batch.Draw(_Texture, position, _CacheFrameSourceRect, _CacheColor, rotation, _CacheOrigin, Scale, SpriteEffects.None, Depth);
                sprite_batch.Draw(
                    particle.Visual.Texture,
                    particle.Position,
                    null,
                    color,
                    particle.Angle,
                    particle.Visual._CacheOrigin,
                    particle.Scale,
                    SpriteEffects.None,
                    Dot.Depth
                );
            }
        }

        public void Spawn(EParticleType type, Vector2 position)
        {
            Spawn(type, position, null, null, null);
        }

        public void Spawn(EParticleType type, Vector2 position, Color? override_color, float? override_scale_factor, Vector2? override_position_delta)
        {
            SEffectDefinition d = Definitions[(int)type];
            if (d.Visual == null)
            {
                Console.WriteLine("asdf");
            }
            Random random = World.Random;
            Vector2 ignore_camera = World.ScrollSpeed * -Vector2.UnitY * 0.0f;

            Vector2 HalfPositionVariationBox = d.PositionVariationBox * 0.5f;
            Vector2 HalfPositionDeltaVariation = d.PositionDeltaVariation * 0.5f;
            float HalfAngleVariation = d.AngleVariation * 0.5f;
            float HalfAngleDeltaVariation = d.AngleDeltaVariation * 0.5f;
            Vector2 HalfScaleDeltaVariation = d.ScaleDeltaVariation * 0.5f;
            Vector2 HalfScaleVariation = d.ScaleVariation * 0.5f;
            float HalfAlphaVariation = d.AlphaVariation * 0.5f;
            float HalfAlphaDeltaVariation = d.AlphaDeltaVariation * 0.5f;
            float HalfLifetimeVariation = d.Lifetime * 0.5f;
            Vector2 position_delta = override_position_delta != null ? (Vector2)override_position_delta : d.PositionDelta;
            Color color = override_color != null ? (Color)override_color : d.Color;
            float scale_factor = override_scale_factor != null ? (float)override_scale_factor : 1.0f;


            for (int i = 0; i < d.Count; ++i)
            {
                CParticle particle = GetCachedParticle();

                particle.Visual = d.Visual;
                particle.Position = position - HalfPositionVariationBox + random.NextVector2Variable() * d.PositionVariationBox + d.PositionVariationCircle * random.NextVector2() + d.Position;
                particle.PositionDelta = position_delta - HalfPositionDeltaVariation + random.NextVector2Variable() * d.PositionDeltaVariation + ignore_camera;
                particle.Angle = d.Angle - HalfAngleVariation + d.AngleVariation * random.NextFloat();
                particle.AngleDelta = d.AngleDelta - HalfAngleDeltaVariation + d.AngleDeltaVariation * random.NextFloat();
                particle.Scale = (d.Scale - HalfScaleVariation + d.ScaleVariation * random.NextFloat()) * scale_factor;
                particle.ScaleDelta = d.ScaleDelta - HalfScaleDeltaVariation + d.ScaleDeltaVariation * random.NextFloat();
                particle.Color = color;
                particle.Alpha = d.Alpha - HalfAlphaVariation + d.AlphaVariation * random.NextFloat();
                particle.AlphaDelta = d.AlphaDelta - HalfAlphaDeltaVariation + d.AlphaDeltaVariation * random.NextFloat();
                particle.Lifetime = d.Lifetime + (int)(-HalfLifetimeVariation + d.LifetimeVariation * random.NextFloat());
                particle.Frame = 0;

                Vector2 out_ = particle.Position - position;
                if (!out_.IsEffectivelyZero())
                    particle.PositionDelta += out_.Normal() * d.OutDelta;

                Add(particle);
            }
        }

        private void Add(CParticle particle)
        {
            Particles.Add(particle);
        }

        private CParticle GetCachedParticle()
        {
            if (Cache.Count == 0)
            {
                Cache.Add(new CParticle());
            }

            CParticle result = Cache[Cache.Count - 1];
            Cache.RemoveAt(Cache.Count - 1);
            return result;
        }
    }

    public class CParticle
    {
        public CVisual Visual = null;
        public Color Color = Color.White;
        public Vector2 Position = Vector2.Zero;
        public Vector2 PositionDelta = Vector2.Zero;
        public Vector2 Scale = Vector2.One;
        public Vector2 ScaleDelta = Vector2.Zero;
        public float Angle = 0.0f;
        public float AngleDelta = 0.0f;
        public float Alpha = 1.0f;
        public float AlphaDelta = (1.0f / 60.0f) * -1.0f;
        public int Lifetime = 60;
        public int Frame = 0;

        public void Update()
        {
            Position = Vector2.Add(Position, PositionDelta);
            Scale = Vector2.Add(Scale, ScaleDelta);
            Angle += AngleDelta;
            Alpha += AlphaDelta;
            Frame += 1;
        }

        public bool IsAlive()
        {
            return Frame < Lifetime && Alpha > 0.0f && Scale.X > 0.0f && Scale.Y > 0.0f;
        }
    }
}

