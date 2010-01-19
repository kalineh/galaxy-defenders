//
// GameViewport.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    // TODO: do this properly
    // TODO: reduce game area for sidebar
    // TODO: fix so all sprites will be correctly scaled at multiple resolution (360 will scale down?)

    public class CGameViewport
    {
        public CGalaxy Game { get; private set; }
        private Rectangle GameRectangle { get; set; }
        public Vector2 Center { get; private set; }

        public CGameViewport(CGalaxy game)
        {
            Game = game;
            Viewport viewport = Game.GraphicsDevice.Viewport;
            Rectangle title_safe = viewport.TitleSafeArea;

            int edge = 16;
            title_safe.Offset(edge, edge);
            GameRectangle = new Rectangle(
                title_safe.X + edge,
                title_safe.Y + edge,
                (int)(viewport.Width - edge),
                (int)(viewport.Height - edge)
            );

            Center = new Vector2(
                (float)GameRectangle.X + (float)(GameRectangle.Width / 2.0f),
                (float)GameRectangle.Y + (float)(GameRectangle.Height / 2.0f)
            );
        }

        public Vector2 ClampInside(Vector2 position, float buffer)
        {
            Vector2 clamped = position;

            clamped.X = Math.Max(GameRectangle.X + buffer, clamped.X);
            clamped.Y = Math.Max(GameRectangle.Y + buffer, clamped.Y);
            clamped.X = Math.Min(GameRectangle.X + GameRectangle.Width - buffer, clamped.X);
            clamped.Y = Math.Min(GameRectangle.Y + GameRectangle.Height - buffer, clamped.Y);

            return clamped;
        }

        public bool IsInside(Vector2 position, float buffer)
        {
            if (GameRectangle.X + buffer < GameRectangle.X ||
                GameRectangle.Y + buffer < GameRectangle.Y ||
                GameRectangle.X - buffer > GameRectangle.X + GameRectangle.Width ||
                GameRectangle.Y - buffer > GameRectangle.Y + GameRectangle.Height)
            {
                return false;
            }

            return true;
        }

        public bool IsOffBottom(Vector2 position, float buffer)
        {
            return position.Y > GameRectangle.Y + GameRectangle.Height - buffer;
        }
    }
}
