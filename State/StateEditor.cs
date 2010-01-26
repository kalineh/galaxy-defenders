//
// StateEditor.cs
//

using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    // TODO: camera zoom
    // TODO: drag-scroll
    // TODO: add entity
    // TODO: generate stage to text
    // TODO: place entity
    // TODO: edit entity
    // TODO: load stage definition

    public class CStateEditor
        : CState
    {
        public CGalaxy Game { get; private set; }
        public CWorld World { get; private set; }
        public CEditor Editor { get; private set; }
        private Graphics Graphics { get; set; }
        private Pen DefaultPen { get; set; }

        public CStateEditor(CGalaxy game)
        {
            Game = game;
            Editor = new CEditor(game);
            World = new CWorld(game);
            // TODO: .Handle will try and get the handle from the control which was created on another thread, and therefore fail
            // TODO: if it is a control, check InvokeRequired and call a delegate
            //Graphics = Graphics.FromHwnd(game.Window.Handle);
            //DefaultPen = new Pen(Brushes.White, 3.0f);
        }

        public override void Update()
        {
            if (Game.Input.IsKeyPressed(Keys.Escape))
            {
                Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
            }
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            Game.GraphicsDevice.Clear(Microsoft.Xna.Framework.Graphics.Color.Black);

            sprite_batch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None);

            sprite_batch.End();

            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Brushes.White, 5.0f);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromHwnd(Game.Window.Handle);
            graphics.DrawLine(pen, new System.Drawing.PointF(10.0f, 10.0f), new System.Drawing.PointF(200.0f, 200.0f));
            //System.Drawing.Graphics.FromHwnd(Game.Window.Handle);
        }
    }
}
