//
// Camera.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CCamera
    {
        public CGalaxy Game { get; private set; }
        public Vector3 Position { get; set; }
        public Vector3 LookAt { get; set; }
        public Matrix ViewMatrix { get; set; }
        public Matrix ProjectionMatrix { get; set; }

        public CCamera(CGalaxy game)
        {
            Game = game;

            Position = Vector3.Zero;
            LookAt = Vector3.UnitZ * -1.0f;

            float aspect_ratio = (float)game.GraphicsDevice.Viewport.Width / (float)game.GraphicsDevice.Viewport.Height;

            ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(90.0f),
                aspect_ratio,
                1.0f,
                10000.0f
            );

            Update();
        }

        public void Update()
        {
            ViewMatrix = Matrix.CreateLookAt(Position, LookAt, Vector3.Up);
        }
    }
}
