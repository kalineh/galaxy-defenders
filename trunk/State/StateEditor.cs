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
    using WinPoint = System.Drawing.Point;
    using XnaColor = Microsoft.Xna.Framework.Graphics.Color;

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
        private Vector2 LastMouseInput { get; set; }
        private CShip SampleShip { get; set; }
        private SProfile WorkingProfile;
        private CStars Stars { get; set; }
        public WinPoint FormTopLeft { get; set; }

        public CStateEditor(CGalaxy game)
        {
            Game = game;
            Editor = new CEditor(game);
            World = new CWorld(game);
            WorkingProfile = CSaveData.GetCurrentProfile();
            SampleShip = new CShip(World, WorkingProfile, new Vector2(100.0f, 100.0f));
            World.EntityAdd(SampleShip);
            // TODO: .Handle will try and get the handle from the control which was created on another thread, and therefore fail
            // TODO: if it is a control, check InvokeRequired and call a delegate
            // TODO: control.Invoke( delegate ) // can use delegate return value!
            //Graphics = Graphics.FromHwnd(game.Window.Handle);
            //DefaultPen = new Pen(Brushes.White, 3.0f);

            Stars = new CStars(World, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 1.0f, 3.0f);
        }

        public override void Update()
        {
            if (Game.Input.IsKeyPressed(Keys.Escape))
            {
                Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
            }

            UpdateMouse();

            World.GameCamera.Update();
            Stars.Update();
            World.UpdateEntities();
        }

        public void UpdateMouse()
        {
            MouseState state = Mouse.GetState();
            Vector2 current = new Vector2(state.X, state.Y);
            Vector2 delta = current - LastMouseInput;
            Vector2 mouse = current + new Vector2(FormTopLeft.X, FormTopLeft.Y);

            if (state.LeftButton == ButtonState.Pressed)
            {
                Vector2 world = World.GameCamera.ScreenToWorld(mouse);
                SampleShip.Physics.PositionPhysics.Position = world;
            }

            if (state.MiddleButton == ButtonState.Pressed)
            {
                World.GameCamera.Position += new Vector3(delta, 0.0f) * 1.0f / World.GameCamera.Zoom;
            }

            if (state.RightButton == ButtonState.Pressed)
            {
                World.GameCamera.Zoom += delta.Y * 0.01f;
            }

            LastMouseInput = current;
        }

        public override void Draw()
        {
            Game.GraphicsDevice.Clear(Microsoft.Xna.Framework.Graphics.Color.Black);

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, World.GameCamera.WorldMatrix);
            Stars.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            World.DrawEntities(World.GameCamera);

            MouseState state = Mouse.GetState();
            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None);
            String text = String.Format("Mouse X: {0:0.00}, Y: {1:0.00}", (float)state.X, (float)state.Y);
            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, text, Vector2.Zero, XnaColor.White);
            Vector2 world = World.GameCamera.ScreenToWorld(new Vector2(state.X, state.Y));
            text = String.Format("World X: {0:0.00}, Y: {1:0.00}", world.X, world.Y);
            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, text, Vector2.UnitY * 20.0f, XnaColor.White);
            Vector2 player = SampleShip.Physics.PositionPhysics.Position;
            text = String.Format(" Ship X: {0:0.00}, Y: {1:0.00}", FormTopLeft.X, FormTopLeft.Y);
            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, text, Vector2.UnitY * 40.0f, XnaColor.White);
            Game.DefaultSpriteBatch.End();

            //System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Brushes.White, 5.0f);
            //System.Drawing.Graphics graphics = System.Drawing.Graphics.FromHwnd(Game.Window.Handle);
            //graphics.DrawLine(pen, new System.Drawing.PointF(10.0f, 10.0f), new System.Drawing.PointF(200.0f, 200.0f));
            //System.Drawing.Graphics.FromHwnd(Game.Window.Handle);
        }
    }
}
