//
// Camera.cs
//

using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    using WinRectangle = System.Drawing.Rectangle;

    public class CCamera
    {
        public CGalaxy Game { get; private set; }
        public Vector3 Position { get; set; }
        public Vector3 LookAt { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }

        public Matrix ViewMatrix { get; private set; }
        public Matrix ProjectionMatrix { get; private set; }
        public Matrix WorldMatrix { get; private set; }

        public CCamera(CGalaxy game)
        {
            Game = game;

            Position = Vector3.Zero;
            LookAt = Vector3.UnitZ * -1.0f;
            Zoom = 1.0f;

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
            Viewport viewport = Game.GraphicsDevice.Viewport;
            Vector3 viewport_ = new Vector3(viewport.Width, viewport.Height, 0.0f);
            WorldMatrix =
                Matrix.CreateTranslation(Position) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1.0f);
        }

        public Vector2 ScreenToWorld(Vector2 screen)
        {
            Vector2 transformed_screen = screen;
            
            // TODO: why is this necessary!?
            transformed_screen.Y *= 1.0375f;

            WinRectangle virtual_screen = System.Windows.Forms.SystemInformation.VirtualScreen;
            Viewport viewport = Game.GraphicsDevice.Viewport;
            Matrix translation = Matrix.CreateTranslation(Position);
            Matrix scale = Matrix.CreateScale(Zoom, Zoom, 1.0f);
            Matrix inverse_translation = Matrix.Invert(translation);
            Matrix inverse_scale = Matrix.Invert(scale);
            return Vector2.Transform(transformed_screen, inverse_scale * inverse_translation);
        }

        public Vector3 GetCenter()
        {
            // TODO: do this in a way that is correct, rather than incorrect
            Viewport viewport = Game.GraphicsDevice.Viewport;
            Vector3 viewport_ = new Vector3(viewport.Width, viewport.Height, 0.0f);
            Vector3 center = Position + viewport_ * 0.5f;
            return center;
        }
    }
}
