﻿//
// Isosceles.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CIsosceles
        : CEnemy
    {
        public CIsosceles(CWorld world, Vector2 position)
            : base(world, "Isosceles")
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Visual = CVisual.MakeSprite(world, "Textures/Enemy/Isosceles.cs");
            Health = 1.0f;
        }
    }
}
