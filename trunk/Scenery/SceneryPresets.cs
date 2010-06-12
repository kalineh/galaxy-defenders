//
// CSceneryPresets.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public static class CSceneryPresets
    {
        // TODO: cannot use direct scenery! must be a chain!
        public static CScenery Black(CWorld world)
        {
            return new CSceneryChain(world,
                new CBackground(world, new Color(0, 0, 0))
            );
        }

        public static CScenery BlueSky(CWorld world)
        {
            return new CSceneryChain(world,
                new CGradientBackground(world,
                    new Color(112, 140, 220),
                    new Color(112, 140, 220),
                    new Color(112, 140, 220),
                    new Color(112, 140, 220)
                ),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Cloud"), 1.2f, Vector2.UnitY * 6.0f),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Cloud"), 0.8f, Vector2.UnitY * 9.0f)
            );
        }

        public static CScenery BrightBlueSky(CWorld world)
        {
            return new CSceneryChain(world,
                new CGradientBackground(world,
                    new Color(40, 30, 90),
                    new Color(40, 30, 90),
                    new Color(30, 20, 50),
                    new Color(30, 20, 50)
                ),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/StarCloud"), 1.2f, Vector2.UnitY * 6.0f, 0.1f),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/StarCloud"), 0.8f, Vector2.UnitY * 9.0f, 0.3f)
            );
        }

        public static CScenery DarkBlueSky(CWorld world)
        {
            return new CSceneryChain(world,
                new CGradientBackground(world,
                    new Color(8, 28, 68),
                    new Color(8, 28, 68),
                    new Color(8, 28, 68),
                    new Color(8, 28, 68)
                ),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/BoxCloud"), 1.2f, new Vector2( 2.0f, 3.0f )),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/BoxCloud"), 0.8f, new Vector2( -2.0f, 3.0f ))
            );
        }

        public static CScenery DullGray(CWorld world)
        {
            return new CSceneryChain(world,
                new CGradientBackground(world,
                    new Color(27, 76, 87),
                    new Color(27, 76, 87),
                    new Color(77, 76, 87),
                    new Color(77, 76, 87)
                ),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Droplet"), 0.6f, Vector2.UnitY * 11.0f),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Droplet"), 0.6f, Vector2.UnitY * 12.0f)
            );
        }

        public static CScenery Sunset(CWorld world)
        {
            return new CSceneryChain(world,
                new CGradientBackground(world,
                    new Color(133, 145, 181),
                    new Color(133, 145, 181),
                    new Color(233, 145, 181),
                    new Color(233, 145, 181)
                ),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Cloud"), 1.2f, Vector2.UnitY * 6.0f),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Cloud"), 0.8f, Vector2.UnitY * 9.0f)
            );
        }

        public static CScenery SimpleSpace(CWorld world)
        {
            return new CSceneryChain(world,
                new CBackground(world, new Color(0, 0, 0)),
                new CStars(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Star"), 1.2f, 6.0f),
                new CStars(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Star"), 0.8f, 9.0f)
            );
        }

        public static CScenery BonusStage1(CWorld world)
        {
            return new CSceneryChain(world,
                new CBackground(world, new Color(46, 46, 170)),
                new CBlendingGradientBackground(world, 1.0f,
                    new List<Color[]>() {
                        new Color[] {
                            new Color(0, 196, 0, 128),
                            new Color(0, 64, 0, 64),
                            new Color(0, 0, 0, 0),
                            new Color(0, 64, 0, 64)
                        },
                        new Color[] {
                            new Color(0, 64, 0, 64),
                            new Color(0, 196, 0, 128),
                            new Color(0, 64, 0, 64),
                            new Color(0, 0, 0, 0)
                        },
                        new Color[] {
                            new Color(0, 0, 0, 0),
                            new Color(0, 64, 0, 64),
                            new Color(0, 196, 0, 128),
                            new Color(0, 64, 0, 64)
                        },
                        new Color[] {
                            new Color(0, 64, 0, 64),
                            new Color(0, 0, 0, 0),
                            new Color(0, 64, 0, 64),
                            new Color(0, 196, 0, 128)
                        },
                    }
                ),
                new CStars(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Snow"), 1.2f, 4.0f),
                new CStars(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Snow"), 0.8f, 6.0f)
            );
        }

        public static CScenery Gradient(CWorld world)
        {
            return new CSceneryChain(world,
                new CGradientBackground(world,
                    new Color(133, 145, 181),
                    new Color(133, 145, 181),
                    new Color(133, 145, 181),
                    new Color(133, 145, 181)
                )
            );
        }

        public static CScenery Blend(CWorld world)
        {
            return new CSceneryChain(world,
                new CBlendingGradientBackground(world, 4.0f,
                    new List<Color[]>() {
                        new Color[] {
                            new Color(133, 145, 181),
                            new Color(133, 145, 181),
                            new Color(133, 145, 181),
                            new Color(133, 145, 181)
                        },
                        new Color[] {
                            new Color(102, 125, 161),
                            new Color(102, 125, 161),
                            new Color(102, 125, 161),
                            new Color(102, 125, 161)
                        },
                    }
                )
            );
        }
    }
}

