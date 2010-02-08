// // StateEditor.cs
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
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    using WinPoint = System.Drawing.Point;
    using XnaPoint = Microsoft.Xna.Framework.Point;
    using XnaColor = Microsoft.Xna.Framework.Graphics.Color;

    public enum EditorInteractionState
    {
        None,
        Dragging,
        Zooming,
    }

    // TODO: move the functionality of the world/editor state into Editor.cs
    // TODO: this should just be the framework/flow handling for the editor state itself
    public class CStateEditor
        : CState
    {
        public CGalaxy Game { get; private set; }
        public CWorld World { get; private set; }
        private Vector2 LastMouseInput { get; set; }
        // TODO: remove me! shouldnt need a sample ship in the editor world
        private CShip SampleShip { get; set; }
        private SProfile WorkingProfile;
        private CStars Stars { get; set; }
        public WinPoint FormTopLeft { get; set; }
        public IntPtr Hwnd { get; set; }
        public Vector2 SelectionDragOffset { get; set; }
        public CEntity SelectedEntity { get; set; }
        public CEntity SelectedEntityPreview { get; set; }
        public CEntity HoverEntity { get; set; }
        public EditorInteractionState InteractionState { get; set; }
        public Type SpawnEntityType { get; set; }
        private bool NoSpawnTillRelease { get; set; }

        public CStateEditor(CGalaxy game)
        {
            Game = game;
            ClearStage();
            Editor.CStageGenerate.GenerateStageEntitiesFromDefinition(World, game.StageDefinition);
            SampleShip = World.GetNearestShip(Vector2.Zero);
        }

        // TODO: find a nicer system for key input ;|
        [DllImport("user32.dll")]
        static extern int GetKeyState(int nVirtKey);
        //static extern int GetKeyState(VirtualKeyStates nVirtKey);

        public override void Update()
        {
            Game.Input.Update();

            if (Game.Input.IsKeyPressed(Keys.Escape))
            {
                Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
            }

            if (Game.Input.IsKeyPressed(Keys.Delete))
            {
                World.EntityDelete(SelectedEntity);
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

                case EditorInteractionState.Zooming:
                    UpdateInteractionZooming(mouse, delta, world);
                    break;
            }

            LastMouseInput = current;
        }
        
        public void UpdateInteractionNone(Vector2 mouse, Vector2 delta, Vector2 world)
        {
            // TODO: not a hack
            int left_alt_keystate = GetKeyState((int)Keys.LeftAlt);
            bool left_alt_down = (left_alt_keystate & 0x8000) != 0;

            MouseState state = Mouse.GetState();

            if (!IsInGameViewport(mouse))
            {
                HoverEntity = null;
                return;
            }

            HoverEntity = World.GetEntityAtPosition(world);
            CDebugRender.Box(World.GameCamera.WorldMatrix, world, Vector2.One * 5.0f, 2.5f, XnaColor.White);
            
            // TODO: cleanup to statefulness, and keybinding system (modifier + key)
            if (SpawnEntityType != null)
            {
                if (state.LeftButton == ButtonState.Pressed)
                {
                    if (!NoSpawnTillRelease)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) || left_alt_down)
                        {
                            CSpawnerEntity element = new CSpawnerEntity()
                            {
                                Type = SpawnEntityType,
                                Position = world,
                                CustomMover = CMoverPresets.MoveDown(1.0f),
                                SpawnCount = 1,
                                SpawnTimer = new CSpawnTimerInterval(),
                                SpawnPosition = new CSpawnPositionFixed() { Position = world },
                            };

                            Editor.CSpawnerEntity entity = new Editor.CSpawnerEntity(World, element);
                            World.EntityAdd(entity);
                            NoSpawnTillRelease = true;
                        }
                    }
                }

                if (state.LeftButton == ButtonState.Released)
                {
                    NoSpawnTillRelease = false;
                }
            }

            if (state.LeftButton == ButtonState.Pressed && !left_alt_down)
            {
                CEntity entity = World.GetEntityAtPosition(world);
                SelectedEntity = entity;

                if (SelectedEntity != null)
                {
                    // TODO: type fail :(
                    Editor.CSpawnerEntity spawner = SelectedEntity as Editor.CSpawnerEntity;
                    if (spawner != null)
                    {
                        SelectedEntityPreview = new Editor.CEditorPreviewEntity(World, spawner, spawner.Mover);
                        World.EntityAdd(SelectedEntityPreview);
                    }

                    InteractionState = EditorInteractionState.Dragging;
                    SelectionDragOffset = entity.Physics.PositionPhysics.Position - world;
                }
            }

            if (state.MiddleButton == ButtonState.Pressed)
            {
                World.GameCamera.Position -= new Vector3(delta, 0.0f) * 1.0f / World.GameCamera.Zoom;
            }

            if (state.RightButton == ButtonState.Pressed)
            {
                InteractionState = EditorInteractionState.Zooming;
                System.Windows.Forms.Cursor.Hide();
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

        public void UpdateInteractionZooming(Vector2 mouse, Vector2 delta, Vector2 world)
        {
            MouseState state = Mouse.GetState();
            if (state.RightButton == ButtonState.Released)
            {
                InteractionState = EditorInteractionState.None;
                System.Windows.Forms.Cursor.Show();
                return;
            }

            float apply = delta.Y * 0.005f;
            Vector3 detransform = world.ToVector3() / World.GameCamera.Zoom;
            Vector3 pre = detransform * World.GameCamera.Zoom;
            World.GameCamera.Zoom += apply;
            Vector3 post = detransform * World.GameCamera.Zoom;

            // close, but curves inward weird, need something else
            //Vector3 offset = world.ToVector3() - World.GameCamera.Position;
            //Vector3 offset = World.GameCamera.Position - world.ToVector3();
            Vector3 offset = post - pre;
            World.GameCamera.Position += offset;
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

            if (HoverEntity != null)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, World.GameCamera.WorldMatrix);
                Game.DefaultSpriteBatch.End();
                CDebugRender.Box(World.GameCamera.WorldMatrix, HoverEntity.Physics.PositionPhysics.Position, Vector2.One * HoverEntity.GetRadius() * 2.0f, 1.0f, XnaColor.White);
            }

            if (SelectedEntity != null)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, World.GameCamera.WorldMatrix);
                Game.DefaultSpriteBatch.End();
                CDebugRender.Box(World.GameCamera.WorldMatrix, SelectedEntity.Physics.PositionPhysics.Position, Vector2.One * SelectedEntity.GetRadius() * 2.0f, 1.0f, XnaColor.Green);
            }

            World.DrawEntities(World.GameCamera);

            // Debug render.
            CDebugRender.Line(World.GameCamera.WorldMatrix, new Vector2(-400.0f, 300.0f), Vector2.UnitY * -5000.0f, 2.0f, XnaColor.Blue);
            CDebugRender.Line(World.GameCamera.WorldMatrix, new Vector2(400.0f, 300.0f), Vector2.UnitY * -5000.0f, 2.0f, XnaColor.Blue);
            CDebugRender.Line(World.GameCamera.WorldMatrix, new Vector2(-400.0f, 300.0f), Vector2.UnitX * 800.0f, 0.5f, XnaColor.Blue);
            SampleShip = World.GetNearestShip(Vector2.Zero);
            if (SampleShip != null)
            {
                CDebugRender.Box(World.GameCamera.WorldMatrix, SampleShip.Physics.PositionPhysics.Position, new Vector2(800.0f, 600.0f), 2.0f, XnaColor.Red);
            }

            // render debug
            CDebugRender.Render(Game);
        }

        public void ClearStage()
        {
            CWorld old_world = World;

            World = new CWorld(Game);
            WorkingProfile = CSaveData.GetCurrentProfile();
            Stars = new CStars(World, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 1.0f, 3.0f);
            SelectedEntity = null;
            SelectedEntityPreview = null;
            HoverEntity = null;

            if (old_world != null)
            {
                World.GameCamera.Position = old_world.GameCamera.Position;
                World.GameCamera.Zoom = old_world.GameCamera.Zoom;
            }
        }

        public void DeleteSelectedEntity()
        {
            World.EntityDelete(SelectedEntity);
            World.EntityDelete(SelectedEntityPreview);
            SelectedEntity = null;
            SelectedEntityPreview = null;
        }

        // TODO: should this functionality be on the editor even? maybe in game?
        // TODO: make a stage definition handling system? (load/save/static currentdef)
        public void ReplaceStageDefinition(CStageDefinition definition)
        {
            ClearStage();
            Game.StageDefinition = definition;
            Editor.CStageGenerate.GenerateStageEntitiesFromDefinition(World, Game.StageDefinition);
        }

        public void UpdateStageDefinition()
        {
            Game.StageDefinition = Editor.CStageGenerate.GenerateDefinitionFromStageEntities(World, "EditorStage");
            ClearStage();
            Editor.CStageGenerate.GenerateStageEntitiesFromDefinition(World, Game.StageDefinition);
        }
    }
}
