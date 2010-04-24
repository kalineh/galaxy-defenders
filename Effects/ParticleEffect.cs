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
        public CWorld World { get; set; }
        public List<CParticle> Particles { get; set; }
        public static CVisual Dot { get; set; }
        public static CVisual Triangle { get; set; }

        public CParticleEffectManager(CWorld world)
        {
            World = world;
            Dot = CVisual.MakeSpriteUncached(world, "Textures/Effects/DotParticle");
            Triangle = CVisual.MakeSpriteUncached(world, "Textures/Effects/TriangleParticle");
            Particles = new List<CParticle>();
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
                    Vector2.Zero,
                    particle.Scale,
                    SpriteEffects.None,
                    Dot.Depth
                );
            }
        }

        public void Add(CParticle particle)
        {
            Particles.Add(particle);
        }
    }

    public class CParticle
    {
        public CVisual Visual = null;
        public Vector2 Position = Vector2.Zero;
        public Vector2 PositionDelta = Vector2.Zero;
        public float Angle = 0.0f;
        public float AngleDelta = 0.0f;
        public Vector2 Scale = Vector2.One;
        public Vector2 ScaleDelta = Vector2.Zero;
        public Color Color = Color.White;
        public float Alpha = 1.0f;
        public float AlphaDelta = (1.0f / 60.0f) * -1.0f;
        public int Lifetime = 60;
        private int Frame = 0;

        public void Update()
        {
            Position += PositionDelta;
            Angle += AngleDelta;
            Scale += ScaleDelta;
            Alpha += AlphaDelta;
            Frame += 1;
        }

        public bool IsAlive()
        {
            return Frame < Lifetime && Alpha > 0.0f && Scale.X > 0.0f && Scale.Y > 0.0f;
        }
    }

    class CParticleGroupSpawner
    {
        public int Count = 1;
        public CVisual Visual = null;
        public Vector2 Position = Vector2.Zero;
        public Vector2 PositionVariation = Vector2.Zero;
        public Vector2 PositionDelta = Vector2.Zero;
        public Vector2 PositionDeltaVariation = Vector2.Zero;
        public float Angle = 0.0f;
        public float AngleVariation = 0.0f;
        public float AngleDelta = 0.0f;
        public float AngleDeltaVariation = 0.0f;
        public Vector2 Scale = Vector2.One;
        public Vector2 ScaleVariation = Vector2.Zero;
        public Vector2 ScaleDelta = Vector2.Zero;
        public Vector2 ScaleDeltaVariation = Vector2.Zero;
        public Color Color = Color.White;
        public float Alpha = 1.0f;
        public float AlphaVariation = 0.0f;
        public float AlphaDelta = 0.0f;
        public float AlphaDeltaVariation = 0.0f;
        public int Lifetime = 60;
        public int LifetimeVariation = 15;

        public void Spawn(CParticleEffectManager manager)
        {
            Random random = manager.World.Random;
            Vector2 ignore_camera = manager.World.Game.StageDefinition.ScrollSpeed * -Vector2.UnitY * 0.5f;
            for (int i = 0; i < Count; ++i)
            {
                CParticle particle = new CParticle()
                {
                    Visual = Visual,
                    Position = Position - PositionVariation * 0.5f + random.NextVector2() * PositionVariation,
                    PositionDelta = PositionDelta - PositionDeltaVariation * 0.5f + random.NextVector2Variable() * PositionDeltaVariation + ignore_camera,
                    Angle = Angle + -AngleDeltaVariation * 0.5f + AngleVariation * random.NextFloat(),
                    AngleDelta = AngleDelta - AngleDeltaVariation * 0.5f + AngleDeltaVariation * random.NextFloat(),
                    Scale = Scale - ScaleVariation * 0.5f + ScaleVariation * random.NextFloat(),
                    ScaleDelta = ScaleDelta - ScaleDeltaVariation * 0.5f + ScaleDeltaVariation * random.NextFloat(),
                    Color = Color,
                    Alpha = Alpha - AlphaVariation * 0.5f + AlphaVariation * random.NextFloat(),
                    AlphaDelta = AlphaDelta - AlphaDeltaVariation * 0.5f + AlphaDeltaVariation * random.NextFloat(),
                    Lifetime = Lifetime + (int)(LifetimeVariation * -0.5f + LifetimeVariation * random.NextFloat()),
                };

                manager.Add(particle);
            }
        }

        public static CParticleGroupSpawner MakeSmallExplosion(Vector2 position, Color color)
        {
            return new CParticleGroupSpawner()
            {
                Visual = CParticleEffectManager.Dot,
                Count = 8,
                Position = position,
                PositionDeltaVariation = new Vector2(4.0f, 4.0f),
                Alpha = 0.8f,
                AngleDeltaVariation = 0.1f,
                Scale = new Vector2(0.3f, 0.3f),
                ScaleVariation = new Vector2(-0.1f, -0.1f),
                ScaleDeltaVariation = new Vector2(1.0f / 10.0f * -1.0f, 1.0f / 10.0f * -1.0f),
                Lifetime = 10,
                Color = color,
            };
        }

        public static CParticleGroupSpawner MakeBigExplosion(Vector2 position, Color color)
        {
            return new CParticleGroupSpawner()
            {
                Visual = CParticleEffectManager.Triangle,
                Count = 14,
                Position = position,
                PositionDeltaVariation = new Vector2(8.0f, 8.0f),
                Alpha = 0.8f,
                AngleDeltaVariation = 0.1f,
                Scale = new Vector2(0.4f, 0.4f),
                ScaleVariation = new Vector2(-0.1f, -0.1f),
                ScaleDeltaVariation = new Vector2(1.0f / 14.0f * -1.0f, 1.0f / 14.0f * -1.0f),
                Lifetime = 14,
                Color = color,
            };
        }
    }
}

