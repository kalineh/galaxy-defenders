﻿//
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
        public List<CParticle> Cache { get; set; }
        public static CVisual Dot { get; set; }
        public static CVisual Triangle { get; set; }

        public CParticleEffectManager(CWorld world)
        {
            World = world;
            Dot = CVisual.MakeSpriteUncached(world.Game, "Textures/Effects/DotParticle");
            Triangle = CVisual.MakeSpriteUncached(world.Game, "Textures/Effects/TriangleParticle");
            Dot.Recache();
            Triangle.Recache();
            Particles = new List<CParticle>();
            Cache = new List<CParticle>(8192);

            for (int i = 0; i < 1024; ++i)
            {
                Cache.Add(new CParticle());
            }
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

        public void Add(CParticle particle)
        {
            Particles.Add(particle);
        }

        public CParticle GetCachedParticle()
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
        public int Frame = 0;

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

    public class CParticleGroupSpawner
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
            Vector2 ignore_camera = manager.World.ScrollSpeed * -Vector2.UnitY * 0.5f;
            for (int i = 0; i < Count; ++i)
            {
                CParticle particle = manager.GetCachedParticle();

                particle.Position = Vector2.Add(Vector2.Subtract(Position, Vector2.Multiply(PositionVariation, 0.5f)), Vector2.Multiply(random.NextVector2(), PositionVariation));
                particle.PositionDelta = Vector2.Add(Vector2.Add(Vector2.Subtract(PositionDelta, Vector2.Multiply(PositionDeltaVariation, 0.5f)), Vector2.Multiply(random.NextVector2Variable(), PositionDeltaVariation)), ignore_camera);
                particle.Angle = Angle + -AngleDeltaVariation * 0.5f + AngleVariation * random.NextFloat();
                particle.AngleDelta = AngleDelta - AngleDeltaVariation * 0.5f + AngleDeltaVariation * random.NextFloat();
                particle.Scale = Vector2.Add(Vector2.Subtract(Scale, Vector2.Multiply(ScaleVariation, 0.5f)), Vector2.Multiply(ScaleVariation, random.NextFloat()));
                particle.ScaleDelta = Vector2.Add(Vector2.Subtract(ScaleDelta, Vector2.Multiply(ScaleDeltaVariation, 0.5f)), Vector2.Multiply(ScaleDeltaVariation, random.NextFloat()));
                particle.Color = Color;
                particle.Alpha = Alpha - AlphaVariation * 0.5f + AlphaVariation * random.NextFloat();
                particle.AlphaDelta = AlphaDelta - AlphaDeltaVariation * 0.5f + AlphaDeltaVariation * random.NextFloat();
                particle.Lifetime = Lifetime + (int)(LifetimeVariation * -0.5f + LifetimeVariation * random.NextFloat());
                particle.Frame = 0;


                particle.Visual = Visual;
                //particle.Position = Position - PositionVariation * 0.5f + random.NextVector2() * PositionVariation;
                //particle.PositionDelta = PositionDelta - PositionDeltaVariation * 0.5f + random.NextVector2Variable() * PositionDeltaVariation + ignore_camera;
                //particle.Angle = Angle + -AngleDeltaVariation * 0.5f + AngleVariation * random.NextFloat();
                //particle.AngleDelta = AngleDelta - AngleDeltaVariation * 0.5f + AngleDeltaVariation * random.NextFloat();
                //particle.Scale = Scale - ScaleVariation * 0.5f + ScaleVariation * random.NextFloat();
                //particle.ScaleDelta = ScaleDelta - ScaleDeltaVariation * 0.5f + ScaleDeltaVariation * random.NextFloat();
                //particle.Color = Color;
                //particle.Alpha = Alpha - AlphaVariation * 0.5f + AlphaVariation * random.NextFloat();
                //particle.AlphaDelta = AlphaDelta - AlphaDeltaVariation * 0.5f + AlphaDeltaVariation * random.NextFloat();
                //particle.Lifetime = Lifetime + (int)(LifetimeVariation * -0.5f + LifetimeVariation * random.NextFloat());
                particle.Frame = 0;

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

        public static CParticleGroupSpawner MakeSmallDirectionalExplosion(Vector2 position, Vector2 direction, Color color)
        {
            return new CParticleGroupSpawner()
            {
                Visual = CParticleEffectManager.Dot,
                Count = 8,
                Position = position,
                PositionDelta = direction * -0.5f + new Vector2(0.0f, 2.0f),
                PositionDeltaVariation = new Vector2(2.5f, 2.5f),
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
                Count = 24,
                Position = position,
                PositionDeltaVariation = new Vector2(8.0f, 8.0f),
                Alpha = 0.8f,
                AngleDeltaVariation = 0.1f,
                Scale = new Vector2(0.8f, 0.8f),
                ScaleVariation = new Vector2(-0.15f, -0.15f),
                ScaleDelta = new Vector2(-0.02f, -0.02f),
                Lifetime = 18,
                Color = color,
            };
        }

        public static CParticleGroupSpawner MakeBuildingExplosion(Vector2 position, Color color, float power)
        {
            return new CParticleGroupSpawner()
            {
                Visual = CParticleEffectManager.Triangle,
                Count = 6 + (int)power,
                Position = position,
                PositionVariation = new Vector2(16.0f, 16.0f),
                PositionDelta = new Vector2(0.0f, 2.0f), // anti-scroll
                PositionDeltaVariation = new Vector2(8.0f, 8.0f),
                Alpha = 0.8f,
                AngleDeltaVariation = 0.1f,
                Scale = new Vector2(0.5f, 0.5f) + new Vector2(0.05f * power),
                ScaleVariation = new Vector2(-0.1f, -0.1f),
                ScaleDeltaVariation = new Vector2(1.0f / 14.0f * -1.0f, 1.0f / 14.0f * -1.0f),
                Lifetime = 14,
                Color = color,
            };
        }

        public static CParticleGroupSpawner MakeMissileTrail(Vector2 position, Vector2 direction, Color color)
        {
            return new CParticleGroupSpawner()
            {
                Visual = CParticleEffectManager.Dot,
                Count = 4,
                Position = Vector2.Zero,
                PositionDelta = new Vector2(-0.4f, -0.4f),
                PositionDeltaVariation = new Vector2(0.8f, 0.8f),
                Alpha = 0.8f,
                AngleDelta = 0.0f,
                AngleDeltaVariation = 0.0f,
                Scale = new Vector2(0.5f, 0.5f),
                ScaleVariation = new Vector2(-0.1f, -0.1f),
                ScaleDelta = new Vector2(-0.1f, -0.1f),
                ScaleDeltaVariation = new Vector2(0.1f, 0.1f),
                Lifetime = 5,
                Color = color,
            };
        }

        public static CParticleGroupSpawner MakeEnemyMissileTrail(Vector2 position, Vector2 direction, Color color)
        {
            return new CParticleGroupSpawner()
            {
                Visual = CParticleEffectManager.Dot,
                Count = 3,
                Position = Vector2.Zero,
                PositionDelta = new Vector2(-0.4f, -0.4f),
                PositionDeltaVariation = new Vector2(0.8f, 0.8f),
                Alpha = 0.8f,
                AngleDelta = 0.0f,
                AngleDeltaVariation = 0.0f,
                Scale = new Vector2(0.5f, 0.5f),
                ScaleVariation = new Vector2(-0.1f, -0.1f),
                ScaleDelta = new Vector2(-0.1f, -0.1f),
                ScaleDeltaVariation = new Vector2(0.1f, 0.1f),
                Lifetime = 5,
                Color = color,
            };
        }

        public static CParticleGroupSpawner MakeBlackHoleCenter(Vector2 position, Color color)
        {
            return new CParticleGroupSpawner()
            {
                Visual = CParticleEffectManager.Dot,
                Count = 3,
                Position = Vector2.Zero,
                PositionVariation = new Vector2(0.8f, 0.8f),
                PositionDelta = new Vector2(0.0f, 0.0f),
                PositionDeltaVariation = new Vector2(2.6f, 2.6f),
                Alpha = 1.0f,
                AngleDelta = 0.0f,
                AngleDeltaVariation = 0.0f,
                Scale = new Vector2(1.3f, 1.3f),
                ScaleVariation = new Vector2(-0.1f, -0.1f),
                ScaleDelta = new Vector2(-0.1f, -0.1f),
                ScaleDeltaVariation = new Vector2(0.1f, 0.1f),
                Lifetime = 5,
                Color = color,
            };
        }

        public static CParticleGroupSpawner MakeBlackHolePull(Vector2 position, Color color)
        {
            return new CParticleGroupSpawner()
            {
                Visual = CParticleEffectManager.Dot,
                Count = 3,
                Position = Vector2.Zero,
                PositionVariation = new Vector2(2.8f, 2.8f),
                PositionDelta = new Vector2(-0.3f, -0.3f),
                PositionDeltaVariation = new Vector2(0.6f, 0.6f),
                Alpha = 1.0f,
                AngleVariation = MathHelper.TwoPi,
                AngleDelta = 0.0f,
                AngleDeltaVariation = 0.0f,
                Scale = new Vector2(1.0f, 1.0f),
                ScaleVariation = new Vector2(-0.1f, -0.1f),
                ScaleDelta = new Vector2(-0.1f, -0.1f),
                ScaleDeltaVariation = new Vector2(0.1f, 0.1f),
                Lifetime = 4,
                Color = color,
            };
        }

        public static CParticleGroupSpawner MakeTeleportOutEffect(Vector2 position)
        {
            return new CParticleGroupSpawner()
            {
                Visual = CParticleEffectManager.Dot,
                Count = 20,
                Position = position,
                PositionVariation = new Vector2(2.8f, 2.8f),
                PositionDelta = new Vector2(-0.3f, -0.3f),
                PositionDeltaVariation = new Vector2(0.6f, 0.6f),
                Alpha = 1.0f,
                AngleVariation = MathHelper.TwoPi,
                AngleDelta = 0.0f,
                AngleDeltaVariation = 0.0f,
                Scale = new Vector2(3.5f, 0.3f),
                ScaleVariation = new Vector2(-0.1f, -0.1f),
                ScaleDelta = new Vector2(-0.1f, -0.1f),
                ScaleDeltaVariation = new Vector2(0.1f, 0.1f),
                Lifetime = 15,
                Color = CEnemy.EnemyOrangeColor,
            };
        }

        public static CParticleGroupSpawner MakeTeleportInEffect(Vector2 position)
        {
            // TODO: not crap
            return new CParticleGroupSpawner()
            {
                Visual = CParticleEffectManager.Dot,
                Count = 20,
                Position = position,
                PositionVariation = new Vector2(1.5f, 1.5f),
                PositionDelta = new Vector2(0.0f, 0.0f),
                PositionDeltaVariation = new Vector2(1.0f, 1.0f),
                Alpha = 1.0f,
                AngleVariation = MathHelper.TwoPi,
                AngleDelta = 0.0f,
                AngleDeltaVariation = 0.0f,
                Scale = new Vector2(2.5f, 2.5f),
                ScaleVariation = new Vector2(-0.1f, -0.1f),
                ScaleDelta = new Vector2(-0.002f, -0.002f),
                ScaleDeltaVariation = new Vector2(0.0f, 0.0f),
                Lifetime = 12,
                Color = CEnemy.EnemyOrangeColor,
            };
        }

        public static CParticleGroupSpawner MakeTestEffect(Vector2 position)
        {
            // TODO: not crap
            return new CParticleGroupSpawner()
            {
                Visual = CParticleEffectManager.Triangle,
                Count = 32,
                Position = position,
                PositionDeltaVariation = new Vector2(8.0f, 8.0f),
                Alpha = 0.8f,
                AngleDeltaVariation = 0.1f,
                Scale = new Vector2(0.6f, 0.6f),
                ScaleVariation = new Vector2(-0.15f, -0.15f),
                ScaleDelta = new Vector2(-0.02f, -0.02f),
                Lifetime = 22,
                Color = Color.White,
            };
        }

    }
}

