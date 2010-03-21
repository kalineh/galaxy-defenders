//
// SceneryPresets.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public static class SceneryPresets
    {
        public static CScenery Black(CWorld world)
        {
            return new CBackground(world, new Color(0, 0, 0));
        }

        public static CScenery BlueSky(CWorld world)
        {
            return new CSceneryChain(world,
                new CBackground(world, new Color(133, 145, 181)),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Cloud"), 1.2f, 6.0f),
                new CClouds(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Cloud"), 0.8f, 9.0f)
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

        public static CScenery Snowfall(CWorld world)
        {
            return new CSceneryChain(world,
                new CBackground(world, new Color(0, 0, 0)),
                new CStars(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Snow"), 1.2f, 4.0f),
                new CStars(world, CContent.LoadTexture2D(world.Game, "Textures/Background/Snow"), 0.8f, 6.0f)
            );
        }
    }
}

