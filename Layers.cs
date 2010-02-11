//
// Layers.cs
//

using System;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    // TODO: sprite batch per layer
    // TODO: shader/effect definition per layer?
    // TODO: sort sprites by texture in sub-layer render (for non-sublayer render?)
    public class CLayers
    {
        public static float LayerIncrement = 0.05f;
        public static float SubLayerIncrement = 0.001f;

        public static float Top = 0.0f;
        public static float FullScreen = LayerIncrement * 10;
        public static float UI = LayerIncrement * 9;
        public static float Effects = LayerIncrement * 8;
        public static float Weapons = LayerIncrement * 7;
        public static float Player = LayerIncrement * 6;
        public static float Entity = LayerIncrement * 5;
        public static float Enemy = LayerIncrement * 4;
        public static float Static = LayerIncrement * 3;
        public static float Decoration = LayerIncrement * 2;
        public static float Background = LayerIncrement * 1;
        public static float Bottom = 1.0f;

        public static float CalculateDepth(Texture texture)
        {
            String name = texture.Name;

            // TODO: automate this
            if (name.StartsWith("Textures/Top")) return Top;
            if (name.StartsWith("Textures/FullScreen")) return FullScreen;
            if (name.StartsWith("Textures/UI")) return UI;
            if (name.StartsWith("Textures/Effects")) return Effects;
            if (name.StartsWith("Textures/Weapons")) return Weapons;
            if (name.StartsWith("Textures/Player")) return Player;
            if (name.StartsWith("Textures/Entity")) return Entity;
            if (name.StartsWith("Textures/Enemy")) return Enemy;
            if (name.StartsWith("Textures/Static")) return Static;
            if (name.StartsWith("Textures/Decoration")) return Decoration;
            if (name.StartsWith("Textures/Background")) return Background;
            if (name.StartsWith("Textures/Bottom")) return Bottom;

            throw new Exception(String.Format("unknown texture depth for {0}", name));
        }
    }
}
