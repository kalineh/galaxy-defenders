//
// ShipParts.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Galaxy
{
    public class CChassisPart
    {
        public int Price { get; set; }
        public string Texture { get; set; }
        public float VisualScale { get; set; }
        public float Armor { get; set; }
        public float Speed { get; set; }
        public float Friction { get; set; }
    }

    public class CGeneratorPart
    {
        public int Price { get; set; }
        public float Energy { get; set; }
        public float Regen { get; set; }
    }

    public class CShieldPart
    {
        public int Price { get; set; }
        public float Shield { get; set; }
        public float Regen { get; set; }
    }

    public class CWeaponPart
    {
        public int Price { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }
    }
}
