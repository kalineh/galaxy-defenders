//
// NewParticleEffect.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.Threading;

namespace Galaxy
{
    public class CNewParticleEffectManager
    {
        public struct SAddRequest
        {
            public EParticleType Type;
            public Vector2 Position;
            public Vector2 Scale;
            public int Count;
        }

        public struct SParticle
        {
            public EParticleType Type;
            public int Index;
            public CVisual Visual;
            public Vector2 Position;
            public Vector2 PositionDelta;
            public Vector2 Scale;
            public Vector2 ScaleDelta;
            public float Angle;
            public float AngleDelta;
            public Color Color;
            public float Alpha;
            public float AlphaDelta;
            public int Lifetime;
            public int Frame;

            public static SParticle MakeDefaultParticle()
            {
                return new SParticle()
                {
                    Type = EParticleType.None,
                    Index = -1,
                    Visual = null,
                    Position = Vector2.Zero,
                    PositionDelta = Vector2.Zero,
                    Scale = Vector2.One,
                    ScaleDelta = Vector2.Zero,
                    Angle = 0.0f,
                    AngleDelta = 0.0f,
                    Color = Color.White,
                    Alpha = 1.0f,
                    AlphaDelta = (1.0f / 60.0f) * -1.0f,
                    Lifetime = 60,
                    Frame = 0,
                };
            }

            public bool Update()
            {
                Position = Vector2.Add(Position, PositionDelta);
                Scale = Vector2.Add(Scale, ScaleDelta);
                Angle += AngleDelta;
                Alpha += AlphaDelta;
                Frame += 1;
                return Frame < Lifetime && Alpha > 0.0f && Scale.X > 0.0f && Scale.Y > 0.0f;
            }
        }

        public CWorld World { get; set; }
        public List<int> Alive { get; set; }
        public List<int> Dead { get; set; }
        public List<SParticle> Particles { get; set; }
        public List<SAddRequest> AddRequests { get; set; }
        public List<SAddRequest> ThreadLocalAddRequests { get; set; }
        public List<SEffectDefinition> Definitions { get; set; }
        public List<SParticle> DrawList { get; set; }
        public Mutex AddRequestMutex { get; set; }
        public Mutex DrawMutex { get; set; }
        public static CVisual Dot { get; set; }
        public static CVisual Triangle { get; set; }
        public Random Random { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public bool SpriteBatchInUse { get; set; }

        public CNewParticleEffectManager(CWorld world)
        {
            World = world;
            Dot = CVisual.MakeSpriteUncached(world.Game, "Textures/Effects/DotParticle");
            Triangle = CVisual.MakeSpriteUncached(world.Game, "Textures/Effects/TriangleParticle");
            Dot.Recache();
            Triangle.Recache();
            Alive = new List<int>(8192);
            Dead = new List<int>(8192);
            Particles = new List<SParticle>(8192);
            DrawList = new List<SParticle>(8192);
            AddRequests = new List<SAddRequest>(256);
            ThreadLocalAddRequests = new List<SAddRequest>(256);
            Definitions = new List<SEffectDefinition>(256);
            AddRequestMutex = new Mutex();
            DrawMutex = new Mutex();
            Random = new Random();
            SpriteBatch = new SpriteBatch(world.Game.GraphicsDevice);

            for (int i = 0; i < 8192; ++i)
            {
                Particles.Add(SParticle.MakeDefaultParticle());
                Dead.Add(i);
            }

            for (int i = 0; i < 256; ++i)
            {
                Definitions.Add(SEffectDefinition.MakeDefaultEffectDefinition());        
            }
        }

        public void Initialize(Dictionary<EParticleType, SEffectDefinition> definitions)
        {
            foreach (KeyValuePair<EParticleType, SEffectDefinition> kv in definitions)
            {
                Definitions[(int)kv.Key] = kv.Value;
            }
        }

        public void Update()
        {
            Vector2 ignore_camera = World.ScrollSpeed * -Vector2.UnitY * 0.5f;

            lock (AddRequestMutex)
            {
                ThreadLocalAddRequests.Clear();
                foreach (SAddRequest request in AddRequests)
                    ThreadLocalAddRequests.Add(request);
                AddRequests.Clear();
            }

            int adds = ThreadLocalAddRequests.Count;
            for (int i = 0; i < ThreadLocalAddRequests.Count; ++i)
            {
                SAddRequest request = ThreadLocalAddRequests[i];
                EParticleType type = request.Type;
                SEffectDefinition definition = Definitions[(int)type];

                for (int request_count = 0; request_count < request.Count; ++request_count)
                {
                    for (int particle_count = 0; particle_count < definition.Count; ++particle_count)
                    {
                        if (Dead.Count > 0)
                        {
                            int index = Dead[Dead.Count - 1];
                            Dead.RemoveAt(Dead.Count - 1);
                            SParticle particle = new SParticle()
                            {
                                Type = type,
                                Index = index,
                                Visual = definition.Visual,
                                Position = request.Position - definition.PositionVariationBox * 0.5f + Random.NextVector2Variable() * definition.PositionVariationBox + definition.PositionVariationCircle * Random.NextVector2(),
                                PositionDelta = definition.PositionDelta - definition.PositionDeltaVariation * 0.5f + Random.NextVector2Variable() * definition.PositionDeltaVariation + ignore_camera,
                                Angle = -definition.AngleDeltaVariation * 0.5f + definition.AngleVariation * Random.NextFloat(),
                                AngleDelta = definition.AngleDelta - definition.AngleDeltaVariation * 0.5f + definition.AngleDeltaVariation * Random.NextFloat(),
                                Scale = request.Scale * (definition.Scale - definition.ScaleVariation * 0.5f + definition.ScaleVariation * Random.NextFloat()),
                                ScaleDelta = definition.ScaleDelta - definition.ScaleDeltaVariation * 0.5f + definition.ScaleDeltaVariation * Random.NextFloat(),
                                Color = definition.Color,
                                Alpha = definition.Alpha - definition.AlphaVariation * 0.5f + definition.AlphaVariation * Random.NextFloat(),
                                AlphaDelta = definition.AlphaDelta - definition.AlphaDeltaVariation * 0.5f + definition.AlphaDeltaVariation * Random.NextFloat(),
                                Lifetime = definition.Lifetime + (int)(definition.LifetimeVariation * -0.5f + definition.LifetimeVariation * Random.NextFloat()),
                                Frame = 0,
                            };
                            Particles[index] = particle;
                            Alive.Add(index);
                        }
                    }
                }
            }

            for (int i = 0; i < Alive.Count; )
            {
                int index = Alive[i];
                SParticle particle = Particles[index];
                bool alive = particle.Update();
                Particles[index] = particle;
                if (alive)
                {
                    ++i;
                    continue;
                }

                Dead.Add(index);
                // TODO-OPT: swap/remove to reduce shuffling
                Alive.Remove(index);
            }

            lock (DrawMutex)
            {
                if (!SpriteBatchInUse)
                {
                    float depth = Dot.Depth;
                    SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, World.GameCamera.WorldMatrix);
                    SpriteBatchInUse = true;
                    for (int i = 0; i < Alive.Count; ++i)
                    {
                        int index = Alive[i];
                        SParticle particle = Particles[index];

                        // TODO: faster
                        Color color = particle.Color;
                        color.A = (byte)(particle.Alpha * 255.0f);

                        SpriteBatch.Draw(
                            particle.Visual.Texture,
                            particle.Position,
                            null,
                            color,
                            particle.Angle,
                            particle.Visual._CacheOrigin,
                            particle.Scale,
                            SpriteEffects.None,
                            depth
                        );
                    }
                }
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            lock (DrawMutex)
            {
                if (SpriteBatchInUse)
                    SpriteBatch.End();
                SpriteBatchInUse = false;
            }
        }

        public void AddRequest(EParticleType type, Vector2 position)
        {
            SAddRequest request = new SAddRequest()
            {
                Type = type,
                Count = 1,
                Position = position,
                Scale = Vector2.One,
            };

            lock (AddRequestMutex)
            {
                AddRequests.Add(request);
            }
        }
    }
}

