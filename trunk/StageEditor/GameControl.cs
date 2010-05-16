using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace StageEditor
{
    using WinPoint = System.Drawing.Point;
    using XnaKeys = Microsoft.Xna.Framework.Input.Keys;

    /// <summary>
    /// Example control inherits from GraphicsDeviceControl, which allows it to
    /// render using a GraphicsDevice. This control shows how to use ContentManager
    /// inside a WinForms application. It loads a SpriteFont object through the
    /// ContentManager, then uses a SpriteBatch to draw text. The control is not
    /// animated, so it only redraws itself in response to WinForms paint messages.
    /// </summary>
    [System.ComponentModel.DesignerCategory("Code")]
    public class GameControl
        : GraphicsDeviceControl
    {
        public Galaxy.EditorGame Game { get; set; }
        public Thread GameThread { get; set; }
        public object CachedHandle { get; set; }

        /// <summary>
        /// Initializes the control, creating the ContentManager
        /// and using it to load a SpriteFont.
        /// </summary>
        protected override void Initialize()
        {
            GameThread = new Thread(new ThreadStart(RunGameThread));
            CachedHandle = (object)this.Handle;
            HandleDestroyed += (sender, event_args) => Dispose();
            Click += new EventHandler(GameControl_Click);
        }

        void GameControl_Click(object sender, EventArgs e)
        {
            this.FindForm().Focus();
            this.Focus();
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

        public void UpdateEditorPosition()
        {
            if (Game == null)
                return;

            Galaxy.CStateEditor editor = Game.State as Galaxy.CStateEditor;
            WinPoint local = new WinPoint(ClientRectangle.Left, ClientRectangle.Top);
            WinPoint screen = PointToScreen(local);
            // HACK: the mouse coords we get in form mode are from -560, -310 to 1359, 889, so lets tell the form it's at a different position.
            // NOTE: this is only when in form mode; in game mode the mouse position starts at 0,0 for the viewport top-left.
            screen.X = 560 - screen.X;
            screen.Y = 310 - screen.Y;
            editor.FormTopLeft = screen;
            editor.Hwnd = this.Handle;
        }

        public delegate void PositionDelegate();

        private void RunGameThread()
        {
            Game = new Galaxy.EditorGame(this);
            Game.Initialize();
            Galaxy.CStateEditor editor = Game.State as Galaxy.CStateEditor;
            PositionDelegate callback = UpdateEditorPosition;
            this.Invoke(callback);

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

                DateTime pre = DateTime.Now;
                Game.Update(game_time);
                Game.Draw(game_time);
                Galaxy.CDebugRender.Render(Game);
                Game.GraphicsDevice.Present((IntPtr)CachedHandle);
                DateTime post = DateTime.Now;
                TimeSpan diff = post - pre;
                TimeSpan frame = TimeSpan.FromSeconds(FrameTimeInSeconds);
                TimeSpan sleep = diff > frame ? TimeSpan.FromSeconds(0.0f) : TimeSpan.FromSeconds(FrameTimeInSeconds) - diff;
                Thread.Sleep(sleep);
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
