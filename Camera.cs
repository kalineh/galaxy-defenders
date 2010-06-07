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
        public float Rotation { get; set; }
        public float Zoom { get; set; }
        public float Border { get; set; }
        public float SpawnBorder { get; set; }
        public float PanLimit { get; set; }
        public Vector2 ScreenSize { get; set; }

        public Matrix ViewMatrix { get; private set; }
        public Matrix ProjectionMatrix { get; private set; }
        public Matrix WorldMatrix { get; private set; }

        public CCamera(CGalaxy game)
        {
            Game = game;

            Position = Vector3.Zero;
            LookAt = Vector3.UnitZ * -1.0f;
            Zoom = 1.0f;
            Border = 150.0f;
            SpawnBorder = 100.0f;
            PanLimit = 100.0f;

#if XBOX360
            ScreenSize = new Vector2(1920.0f * 0.5f, 1080.0f);
#else
            ScreenSize = new Vector2(1920.0f * 0.5f, 1080.0f);
#endif

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
                Matrix.Identity *
                Matrix.CreateTranslation(-Position) *
                Matrix.CreateScale(Zoom, Zoom, 1.0f) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateTranslation(viewport_ / 2.0f);
        }

        public Vector2 ScreenToWorld(Vector2 screen)
        {
            Viewport viewport = Game.GraphicsDevice.Viewport;
            Vector2 viewport_size = new Vector2(viewport.Width * 0.5f, viewport.Height * 0.5f);

            Matrix translation = Matrix.CreateTranslation(Position);
            Matrix scale = Matrix.CreateScale(Zoom, Zoom, 1.0f);
            Matrix inverse_scale = Matrix.Invert(scale);

            Vector2 transformed_screen = screen - viewport_size;
            Vector2 result = Vector2.Transform(transformed_screen, inverse_scale * translation);
            //Console.WriteLine(String.Format("{0} -> {1}", screen.ToString(), result.ToString()));

            // TODO: fixme! math power!
            result -= new Vector2(80.0f * 1.0f / Zoom, 240.0f * 1.0f / Zoom);

            return result;
        }

        public Vector3 GetCenter()
        {
            return Position;
        }

        public Vector2 GetTopLeft()
        {
            //Viewport viewport = Game.GraphicsDevice.Viewport;
            //Vector3 viewport_size = new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0.0f) * 1.0f / Zoom;
            //return (Position - viewport_size).ToVector2();
            return Position.ToVector2() - (ScreenSize * 1.0f / Zoom) * 0.5f;
        }

        public Vector2 GetBottomRight()
        {
            //Viewport viewport = Game.GraphicsDevice.Viewport;
            //Vector3 viewport_size = new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0.0f) * 1.0f / Zoom;
            //return (Position + viewport_size).ToVector2();
            return Position.ToVector2() + (ScreenSize * 1.0f / Zoom) * 0.5f;
        }

        public Vector2 ClampInside(Vector2 position, float buffer)
        {
            Vector2 clamped = position;
            Vector2 tl = GetTopLeft();
            Vector2 br = GetBottomRight();

            // HACK: bottom y limit is increased a bit because of the UI
            br += new Vector2(0.0f, -32.0f);

            clamped.X = Math.Max(tl.X + buffer, clamped.X);
            clamped.Y = Math.Max(tl.Y + buffer, clamped.Y);
            clamped.X = Math.Min(br.X - buffer, clamped.X);
            clamped.Y = Math.Min(br.Y - buffer, clamped.Y);

            return clamped;
        }

        public float GetVisibleWidth()
        {
            return (GetBottomRight() - GetTopLeft()).X;
        }

        public float GetGameWidth()
        {
            return ScreenSize.X + PanLimit;
        }

        public bool IsInside(Vector2 position, float buffer)
        {
            Vector2 tl = GetTopLeft();
            Vector2 br = GetBottomRight();

            if (position.X + buffer < tl.X)
                return false;
            if (position.X - buffer > br.X)
                return false;
            if (position.Y + buffer < tl.Y)
                return false;
            if (position.Y - buffer > br.Y)
                return false;

            return true;
        }

        public bool IsAboveActiveRegionForDeath(Vector2 position)
        {
            Vector2 tl = GetTopLeft();
            return position.Y < tl.Y - 200.0f;
        }

        public bool IsAboveActiveRegion(Vector2 position)
        {
            Vector2 tl = GetTopLeft();
            return position.Y < tl.Y - 100.0f;
        }

        public bool IsOffBottom(Vector2 position, float buffer)
        {
            Vector2 br = GetBottomRight();

            return position.Y > br.Y + buffer;
        }

        public Vector2 GetSpawnBorderLine()
        {
            float half_screen = ScreenSize.Y / 2.0f;
            return Position.ToVector2() + new Vector2(0.0f, -half_screen + -SpawnBorder);
        }
    }
}
