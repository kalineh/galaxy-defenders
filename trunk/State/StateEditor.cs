//
// StateEditor.cs
//

using System;
using System.Globalization;
using System.ComponentModel;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    using WinPoint = System.Drawing.Point;
    using XnaPoint = Microsoft.Xna.Framework.Point;
    using XnaColor = Microsoft.Xna.Framework.Graphics.Color;

    // TODO: camera zoom
    // TODO: drag-scroll
    // TODO: add entity
    // TODO: generate stage to text
    // TODO: place entity
    // TODO: edit entity
    // TODO: load stage definition

    public enum EditorInteractionState
    {
        None,
        Dragging,
    }

    public class CStateEditor
        : CState
    {
        public CGalaxy Game { get; private set; }
        public CWorld World { get; private set; }
        private Vector2 LastMouseInput { get; set; }
        private CShip SampleShip { get; set; }
        private SProfile WorkingProfile;
        private CStars Stars { get; set; }
        public WinPoint FormTopLeft { get; set; }
        public IntPtr Hwnd { get; set; }
        public Vector2 SelectionDragOffset { get; set; }
        public CEntity SelectedEntity { get; set; }
        public CEntity HoverEntity { get; set; }
        public EditorInteractionState InteractionState { get; set; }
        public CVisual SelectionBox { get; set; }
        public CVisual HoverBox { get; set; }
        public string StageFilename { get; set; }
        public CStageDefinition CurrentStageDefinition { get; set; }

        public CStateEditor(CGalaxy game)
        {
            Game = game;
            string cwd = Directory.GetCurrentDirectory();
            string base_ = cwd.Substring(0, cwd.LastIndexOf("StageEditor"));
            StageFilename = base_ + "StageDefinitions\\EditorStage.cs";

            // TODO: temp save testing
            CurrentStageDefinition = Galaxy.Stages.Stage1.GenerateDefinition();

            ClearStage();
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
            Vector2 world = World.GameCamera.ScreenToWorld(mouse);

            switch (InteractionState)
            {
                case EditorInteractionState.None:
                    UpdateInteractionNone(mouse, delta, world);
                    break;

                case EditorInteractionState.Dragging:
                    UpdateInteractionDragging(mouse, delta, world);
                    break;
            }

            LastMouseInput = current;
        }
        
        public void UpdateInteractionNone(Vector2 mouse, Vector2 delta, Vector2 world)
        {
            MouseState state = Mouse.GetState();

            if (!IsInGameViewport(mouse))
            {
                HoverEntity = null;
                return;
            }

            HoverEntity = World.GetEntityAtPosition(world);

            if (state.LeftButton == ButtonState.Pressed)
            {
                CEntity entity = World.GetEntityAtPosition(world);
                SelectedEntity = entity;

                if (SelectedEntity != null)
                {
                    InteractionState = EditorInteractionState.Dragging;
                    SelectionDragOffset = entity.Physics.PositionPhysics.Position - world;
                }
            }

            if (state.MiddleButton == ButtonState.Pressed)
            {
                World.GameCamera.Position += new Vector3(delta, 0.0f) * 1.0f / World.GameCamera.Zoom;
            }

            if (state.RightButton == ButtonState.Pressed)
            {
                World.GameCamera.Zoom += delta.Y * 0.01f;
            }
        }
        
        public void UpdateInteractionDragging(Vector2 mouse, Vector2 delta, Vector2 world)
        {
            MouseState state = Mouse.GetState();

            HoverEntity = null;

            if (!IsInGameViewport(mouse))
                return;

            if (SelectedEntity == null)
            {
                InteractionState = EditorInteractionState.None;
                return;
            }

            if (state.LeftButton == ButtonState.Released)
            {
                InteractionState = EditorInteractionState.None;
                return;
            }

            SelectedEntity.Physics.PositionPhysics.Position = world + SelectionDragOffset;
        }

        public bool IsInGameViewport(Vector2 mouse)
        {
            Viewport viewport = World.Game.GraphicsDevice.Viewport;
            if (mouse.X < viewport.X)
                return false;
            if (mouse.Y < viewport.Y)
                return false;
            if (mouse.X > viewport.X + viewport.Width)
                return false;
            if (mouse.Y > viewport.Y + viewport.Height)
                return false;
            return true;
        }

        public override void Draw()
        {
            Game.GraphicsDevice.Clear(Microsoft.Xna.Framework.Graphics.Color.Black);

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, World.GameCamera.WorldMatrix);
            Stars.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            if (SelectedEntity != null)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, World.GameCamera.WorldMatrix);
                SelectionBox.Scale = new Vector2(SelectedEntity.GetRadius() * 2.0f);
                SelectionBox.Draw(Game.DefaultSpriteBatch, SelectedEntity.Physics.PositionPhysics.Position, 0.0f);
                Game.DefaultSpriteBatch.End();
            }

            if (HoverEntity != null && HoverEntity != SelectedEntity)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, World.GameCamera.WorldMatrix);
                HoverBox.Scale = new Vector2(HoverEntity.GetRadius() * 2.0f);
                HoverBox.Draw(Game.DefaultSpriteBatch, HoverEntity.Physics.PositionPhysics.Position, 0.0f);
                Game.DefaultSpriteBatch.End();
            }

            World.DrawEntities(World.GameCamera);

            // TODO: flickering occurs rendering GDI on top of d3d
            //System.Drawing.Bitmap bitmap = new Bitmap(320, 200);
            //System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Brushes.Red, 5.0f);
            //System.Drawing.Graphics graphics = System.Drawing.Graphics.FromHwnd(Hwnd);
            //graphics.DrawLine(pen, new System.Drawing.PointF(10.0f, 10.0f), new System.Drawing.PointF(200.0f, 200.0f));
            //graphics.DrawLine(pen, new System.Drawing.PointF(10.0f, 10.0f), new System.Drawing.PointF(200.0f, 200.0f));
            //graphics.DrawRectangle(pen, 0.0f, 0.0f, 100.0f, 100.0f);
        }

        private void GenerateEntities()
        {
            SampleShip = new CShip(World, WorkingProfile, new Vector2(100.0f, 100.0f));
            World.EntityAdd(SampleShip);

            foreach (KeyValuePair<int, List<CStageElement>> time_element in CurrentStageDefinition.Elements)
            {
                foreach (CStageElement element in time_element.Value)
                {
                    if (element.GetType() == typeof(CSpawnerEntity))
                    {
                        Editor.CSpawnerEntity entity = new Editor.CSpawnerEntity(World, element as CSpawnerEntity);
                        entity.Physics.PositionPhysics.Position = new Vector2(200.0f, time_element.Key * -1.0f);
                        World.EntityAdd(entity);
                    }
                    else
                    {
                        Editor.CUnknown entity = new Editor.CUnknown(World, element);
                        entity.Physics.PositionPhysics.Position = new Vector2(400.0f, time_element.Key * -1.0f);
                        entity.Visual = new CVisual(CContent.LoadTexture2D(Game, "Textures/Top/Pixel"), XnaColor.Green);
                        entity.Visual.Scale = new Vector2(20.0f);
                        World.EntityAdd(entity);
                    }
                }
            }
        }

        public void ClearStage()
        {
            World = new CWorld(Game);
            WorkingProfile = CSaveData.GetCurrentProfile();
            GenerateEntities();
            Stars = new CStars(World, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 1.0f, 3.0f);
            SelectionBox = new CVisual(CContent.LoadTexture2D(Game, "Textures/Top/Pixel"), XnaColor.Red);
            SelectionBox.Alpha = 0.2f;
            HoverBox = new CVisual(CContent.LoadTexture2D(Game, "Textures/Top/Pixel"), XnaColor.White);
            HoverBox.Alpha = 0.2f;
            SelectedEntity = null;
            HoverEntity = null;
        }
    }
}
