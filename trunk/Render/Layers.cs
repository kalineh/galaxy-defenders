//
// Layers.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    // TODO: sprite batch per layer
    // TODO: shader/effect definition per layer?
    // TODO: sort sprites by texture in sub-layer render (for non-sublayer render?)
    public enum ELayers
    {
        Bottom,
        Background,
        Decoration,
        Static,
        Enemy,
        Entity,
        Player,
        Weapons,
        Effects,
        UI,
        FullScreen,
        Top,
    }

    public class CLayers
    {
        public static ELayers CalculateLayer(Texture texture)
        {
            String name = texture.Name;

            // TODO: automate this
            if (name.StartsWith("Textures/Top")) return ELayers.Top;
            if (name.StartsWith("Textures/FullScreen")) return ELayers.FullScreen;
            if (name.StartsWith("Textures/UI")) return ELayers.UI;
            if (name.StartsWith("Textures/Effects")) return ELayers.Effects;
            if (name.StartsWith("Textures/Weapons")) return ELayers.Weapons;
            if (name.StartsWith("Textures/Player")) return ELayers.Player;
            if (name.StartsWith("Textures/Entity")) return ELayers.Entity;
            if (name.StartsWith("Textures/Enemy")) return ELayers.Enemy;
            if (name.StartsWith("Textures/Static")) return ELayers.Static;
            if (name.StartsWith("Textures/Decoration")) return ELayers.Decoration;
            if (name.StartsWith("Textures/Background")) return ELayers.Background;
            if (name.StartsWith("Textures/Bottom")) return ELayers.Bottom;

            throw new Exception(String.Format("unknown texture depth for {0}", name));
        }
    }
}
