using System;
using System.Threading;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StageEditor
{
    /// <summary>
    /// Example control inherits from GraphicsDeviceControl, which allows it to
    /// render using a GraphicsDevice. This control shows how to use ContentManager
    /// inside a WinForms application. It loads a SpriteFont object through the
    /// ContentManager, then uses a SpriteBatch to draw text. The control is not
    /// animated, so it only redraws itself in response to WinForms paint messages.
    /// </summary>
    class GameControl
        : GraphicsDeviceControl
    {
        public Galaxy.EditorGame Game { get; set; }
        public Thread GameThread { get; set; }
        public IntPtr CachedHandle { get; set; }


        /// <summary>
        /// Initializes the control, creating the ContentManager
        /// and using it to load a SpriteFont.
        /// </summary>
        protected override void Initialize()
        {
            GameThread = new Thread(new ThreadStart(RunGameThread));
            CachedHandle = Handle;
            HandleDestroyed += (sender, event_args) => Dispose();
        }


        /// <summary>
        /// Disposes the control, unloading the ContentManager.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Game.UnloadContent();
                GameThread.Abort();
            }

            base.Dispose(disposing);
        }

        private void RunGameThread()
        {
            Game = new Galaxy.EditorGame();
            Game.Initialize();

            const double FrameTimeInSeconds = 1.0 / 60.0;
            while (true)
            {
                GameTime game_time = new GameTime(
                    TimeSpan.FromSeconds(FrameTimeInSeconds * Game.GameFrame),
                    TimeSpan.FromSeconds(FrameTimeInSeconds),
                    TimeSpan.FromSeconds(FrameTimeInSeconds * Game.GameFrame),
                    TimeSpan.FromSeconds(FrameTimeInSeconds),
                    false
                );

                Game.Update(game_time);
                Game.Draw(game_time);
                Game.GraphicsDevice.Present(CachedHandle);
                Thread.Sleep(TimeSpan.FromSeconds(FrameTimeInSeconds));
            }
        }

        /// <summary>
        /// Draws the control, using SpriteBatch and SpriteFont.
        /// </summary>
        protected override void Draw()
        {
            if (!GameThread.IsAlive)
            {
                GameThread.Start();
            }
        }
    }
}
