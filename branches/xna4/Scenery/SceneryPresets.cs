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

        public static CScenery Empty(CWorld world)
        {
            return new CSceneryChain(world);
        }

        public static CScenery FlyingOverCarrierBG(CWorld world)
        {
            return new CSceneryChain(world,
                new CGradientBackground(world,
                    new Color(92, 120, 196),
                    new Color(92, 120, 196),
                    new Color(92, 120, 196),
                    new Color(92, 120, 196)
                ),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Cloud"), 1.0f, Vector2.UnitY * 6.0f),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Cloud"), 1.0f, Vector2.UnitY * 9.0f)
            );
        }

        public static CScenery FlyingOverCarrierFG(CWorld world)
        {
            return Empty(world);
        }

        public static CScenery ForgottenWatersBG(CWorld world)
        {
            return new CSceneryChain(world,
                new CGradientBackground(world,
                    new Color(5, 5, 98),
                    new Color(5, 5, 98),
                    new Color(5, 5, 98),
                    new Color(5, 5, 98)
                ),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Water"), 1.0f, Vector2.UnitY * 1.0f),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Water"), 1.0f, Vector2.UnitY * 1.0f),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Water"), 1.0f, Vector2.UnitY * 1.0f),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Cloud"), 1.0f, Vector2.UnitY * 9.0f)
            );
        }

        public static CScenery ForgottenWatersFG(CWorld world)
        {
            return Empty(world);
        }

        public static CScenery DistantPlanetBG(CWorld world)
        {
            return new CSceneryChain(world,
                new CGradientBackground(world,
                    new Color(27, 40, 87),
                    new Color(27, 40, 87),
                    new Color(47, 76, 87),
                    new Color(47, 76, 87)
                ),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Droplet"), 1.0f, Vector2.UnitY * 6.0f),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Droplet"), 1.0f, Vector2.UnitY * 7.0f)
            );
        }

        public static CScenery DistantPlanetFG(CWorld world)
        {
            return Empty(world);
        }

        public static CScenery SpaceStationRX4BG(CWorld world)
        {
            return new CSceneryChain(world,
                new CBackground(world, new Color(0, 0, 0)),
                new CStars(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Star"), 1.2f, 6.0f),
                new CStars(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Star"), 0.8f, 9.0f)
            );
        }

        public static CScenery SpaceStationRX4FG(CWorld world)
        {
            return Empty(world);
        }

        public static CScenery StrikeInversionBG(CWorld world)
        {
            return new CSceneryChain(world,
                new CGradientBackground(world,
                    new Color(8, 8, 42),
                    new Color(8, 8, 42),
                    new Color(8, 8, 42),
                    new Color(8, 8, 42)
                ),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/BoxCloud"), 1.2f, new Vector2( 2.0f, 3.0f )),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/BoxCloud"), 0.8f, new Vector2( -2.0f, 3.0f ))
            );
        }

        public static CScenery StrikeInversionFG(CWorld world)
        {
            return new CSceneryChain(world);
        }

        public static CScenery GraniteLocationBG(CWorld world)
        {
            return new CSceneryChain(world,
                new CBackground(world, new Color(0, 0, 0)),
                new CStars(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Vertical"), 1.0f, 6.0f),
                new CStars(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Vertical"), 1.0f, 9.0f),
                new CStars(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Vertical"), 1.0f, 12.0f)
            );
        }

        public static CScenery GraniteLocationFG(CWorld world)
        {
            return new CSceneryChain(world);
        }

        public static CScenery TempestNebulaBG(CWorld world)
        {
            return new CSceneryChain(world,
                new CBackground(world, new Color(35, 51, 51)),
                new CBlendingGradientBackground(world, 1.0f,
                    new List<Color[]>() {
                        new Color[] {
                            new Color(0, 32, 0, 16),
                            new Color(0, 32, 0, 16),
                            new Color(0, 0, 0, 16),
                            new Color(0, 0, 0, 16)
                        },
                        new Color[] {
                            new Color(0, 32, 0, 16),
                            new Color(0, 32, 0, 16),
                            new Color(0, 32, 0, 16),
                            new Color(0, 32, 0, 16)
                        },
                        new Color[] {
                            new Color(0, 32, 0, 16),
                            new Color(0, 32, 0, 16),
                            new Color(0, 32, 0, 16),
                            new Color(0, 32, 0, 16)
                        },
                        new Color[] {
                            new Color(0, 0, 0, 0),
                            new Color(0, 0, 0, 0),
                            new Color(0, 32, 0, 16),
                            new Color(0, 32, 0, 16)
                        },
                    }
                )
            );
        }

        public static CScenery TempestNebulaFG(CWorld world)
        {
            return new CSceneryChain(world,
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Snow"), 1.0f, new Vector2(3.0f, 11.0f), 0.0f, 12),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Snow"), 0.8f, new Vector2(2.0f, 14.0f), 0.0f, 12),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Snow"), 0.8f, new Vector2(3.0f, 12.0f), 2.0f, 12)
            );
        }

        public static CScenery MysteriousCloudsBG(CWorld world)
        {
            return new CSceneryChain(world,
                new CGradientBackground(world,
                    new Color(98, 110, 186),
                    new Color(98, 110, 186),
                    new Color(98, 110, 186),
                    new Color(98, 110, 186)
                )
            );
        }

        public static CScenery MysteriousCloudsFG(CWorld world)
        {
            return new CSceneryChain(world,
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Sky/SpaceCloud1"), 1.2f, Vector2.UnitY * 6.0f, 0.0f, 3),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Sky/SpaceCloud2"), 0.7f, Vector2.UnitY * 9.0f, 0.0f, 3),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Sky/SpaceCloud3"), 0.8f, Vector2.UnitY * 9.0f, 0.0f, 3)
            );
        }

        public static CScenery RelentLessBG(CWorld world)
        {
            return new CSceneryChain(world,
                new CBackground(world, new Color(48, 48, 48)),
                new CStars(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Vertical"), 1.0f, 6.0f),
                new CStars(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Vertical"), 1.0f, 9.0f),
                new CStars(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Vertical"), 1.0f, 12.0f)
            );
        }

        public static CScenery RelentLessFG(CWorld world)
        {
            return new CSceneryChain(world);
        }

        public static CScenery InvasiveActionBG(CWorld world)
        {
            return new CSceneryChain(world,
                new CGradientBackground(world,
                    new Color(5, 5, 98),
                    new Color(5, 5, 98),
                    new Color(5, 5, 98),
                    new Color(5, 5, 98)
                ),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Water"), 1.0f, Vector2.UnitY * 1.0f),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Water"), 1.0f, Vector2.UnitY * 1.0f),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Water"), 1.0f, Vector2.UnitY * 1.0f)
            );
        }

        public static CScenery InvasiveActionFG(CWorld world)
        {
            return new CSceneryChain(world,
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Droplet"), 0.6f, Vector2.UnitY * 12.0f, 0.0f, 7)
            );
        }

        public static CScenery ShadowOfTearsBG(CWorld world)
        {
            return new CSceneryChain(world,
                new CGradientBackground(world,
                    new Color(92, 120, 196),
                    new Color(92, 120, 196),
                    new Color(92, 120, 196),
                    new Color(92, 120, 196)
                )
            );
        }

        public static CScenery ShadowOfTearsFG(CWorld world)
        {
            return new CSceneryChain(world,
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Snow"), 1.0f, new Vector2(3.0f, 11.0f), 0.0f, 8),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Snow"), 0.8f, new Vector2(2.0f, 14.0f), 0.0f, 8),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Snow"), 0.8f, new Vector2(3.0f, 12.0f), 2.0f, 8)
            );
        }

        public static CScenery LaminationXBG(CWorld world)
        {
            return new CSceneryChain(world,
                new CBackground(world, new Color(0, 0, 0)),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Star"), 1.2f, new Vector2(0.0f, 6.0f), 3.0f, 12),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Star"), 0.8f, new Vector2(0.0f, 9.0f), 2.0f, 15)
            );
        }

        public static CScenery LaminationXFG(CWorld world)
        {
            return new CSceneryChain(world);
        }


        public static CScenery BlueSky(CWorld world)
        {
            return new CSceneryChain(world,
                new CGradientBackground(world,
                    new Color(92, 120, 196),
                    new Color(92, 120, 196),
                    new Color(92, 120, 196),
                    new Color(92, 120, 196)
                ),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Cloud"), 1.2f, Vector2.UnitY * 6.0f),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Cloud"), 0.8f, Vector2.UnitY * 9.0f)
            );
        }

        public static CScenery OceanCloudsBG(CWorld world)
        {
            return new CSceneryChain(world,
                new CGradientBackground(world,
                    new Color(5, 5, 93),
                    new Color(5, 5, 93),
                    new Color(5, 5, 93),
                    new Color(5, 5, 93)
                ),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Water"), 1.0f, Vector2.UnitY * 1.0f),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Water"), 1.0f, Vector2.UnitY * 1.0f),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Water"), 1.0f, Vector2.UnitY * 1.0f)
            );
        }

        public static CScenery OceanCloudsFG(CWorld world)
        {
            return new CSceneryChain(world,
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Sky/SpaceCloud1"), 1.2f, Vector2.UnitY * 6.0f, 0.0f, 3),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Sky/SpaceCloud2"), 0.7f, Vector2.UnitY * 9.0f, 0.0f, 3),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Sky/SpaceCloud3"), 0.8f, Vector2.UnitY * 9.0f, 0.0f, 3)
            );
        }

        public static CScenery SpaceCloudsBG(CWorld world)
        {
            return new CSceneryChain(world,
                new CGradientBackground(world,
                    new Color(57, 54, 93),
                    new Color(57, 54, 93),
                    new Color(57, 54, 93),
                    new Color(57, 54, 93)
                )
            );
        }

        public static CScenery SpaceCloudsFG(CWorld world)
        {
            return new CSceneryChain(world,
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Sky/SpaceCloud1"), 1.2f, Vector2.UnitY * 6.0f, 0.0f, 3),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Sky/SpaceCloud2"), 0.7f, Vector2.UnitY * 9.0f, 0.0f, 3),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Sky/SpaceCloud3"), 0.8f, Vector2.UnitY * 9.0f, 0.0f, 3)
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
                new CStars(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Snow"), 1.2f, 8.0f),
                new CStars(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Snow"), 0.8f, 11.0f)
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

