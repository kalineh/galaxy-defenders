// // StateEditor.cs
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    using WinPoint = System.Drawing.Point;
    using XnaColor = Microsoft.Xna.Framework.Graphics.Color;

    public enum EditorInteractionState
    {
        None,
        DragSelect,
        DragEntity,
        ZoomCamera,
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
        public Vector2 DragSelectStart { get; set; }
        public Vector2 DragEntityStart { get; set; }
        public List<CEntity> CopyEntities { get; set; }
        public List<CEntity> SelectedEntities { get; set; }
        public List<CEntity> SelectedEntitiesPreview { get; set; }
        public Dictionary<CEntity, Vector2> SelectedEntitiesOffset { get; set; }
        public List<CEntity> HoverEntities { get; set; }
        public EditorInteractionState InteractionState { get; set; }
        public Type SpawnEntityType { get; set; }
        private bool NoSpawnTillRelease { get; set; }

        public CStateEditor(CGalaxy game)
        {
            Game = game;
            ClearStage();
            Editor.CStageGenerate.GenerateStageEntitiesFromDefinition(World, game.StageDefinition);
            SampleShip = World.GetNearestShip(Vector2.Zero);
            SelectedEntities = new List<CEntity>();
            SelectedEntitiesPreview = new List<CEntity>();
            SelectedEntitiesOffset = new Dictionary<CEntity, Vector2>();
            HoverEntities = new List<CEntity>();
        }

        // TODO: find a nicer system for key input ;|
        [DllImport("user32.dll")]
        static extern int GetKeyState(int nVirtKey);
        [DllImport("user32.dll")]
        static extern int GetAsyncKeyState(int nVirtKey);
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
                SelectedEntities.ForEach(entity => World.EntityDelete(entity));
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

                case EditorInteractionState.DragSelect:
                    UpdateInteractionDragSelect(mouse, delta, world);
                    break;

                case EditorInteractionState.DragEntity:
                    UpdateInteractionDragEntity(mouse, delta, world);
                    break;

                case EditorInteractionState.ZoomCamera:
                    UpdateInteractionZoomCamera(mouse, delta, world);
                    break;
            }

            // TODO: not a hack
            // TODO: function me (updatecopypaste)
            // TODO: proper key release state
            int left_ctrl_keystate = GetKeyState((int)Keys.LeftControl);
            bool left_ctrl_down = (left_ctrl_keystate & 0x8000) != 0;
            int c_keystate = GetAsyncKeyState((int)Keys.C);
            bool c_down = ((c_keystate & 0x8000) != 0) && ((c_keystate & 0x0001) != 0);
            int v_keystate = GetAsyncKeyState((int)Keys.V);
            bool v_down = ((v_keystate & 0x8000) != 0) && ((v_keystate & 0x0001) != 0);

            // copy
            if (c_down)
            {
                CopyEntities = new List<CEntity>(SelectedEntities);
            }

            // paste
            if (v_down)
            {
                Vector2 total = Vector2.Zero;
                foreach (CEntity entity in CopyEntities)
                {
                    total += entity.Physics.PositionPhysics.Position;
                }
                Vector2 average = total / CopyEntities.Count;
                Vector2 offset = world - average;

                foreach (CEntity entity in CopyEntities)
                {
                    // TODO: move me somewhere
                    CEditorEntityBase editor_entity = entity as CEditorEntityBase;
                    Type type = editor_entity.GetType();
                    Vector2 position = editor_entity.Physics.PositionPhysics.Position;
                    CStageElement element = editor_entity.GenerateStageElement();
                    CEditorEntityBase new_entity = Activator.CreateInstance(type, new object[] { World, element }) as CEditorEntityBase;
                    new_entity.Position += offset;
                    World.EntityAdd(new_entity);
                }
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
                HoverEntities.Clear();
                return;
            }

            // TODO: function me (update hover)
            HoverEntities.Clear();
            CEntity hover = World.GetHighestEntityAtPosition(world);
            if (hover != null)
                HoverEntities.Add(hover);

            // TODO: function me (selection cursor)
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
                            // TODO: function me: SpawnEntity()
                            CEntity entity = SpawnEntity(world);
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
                CEntity entity = World.GetHighestEntityAtPosition(world);

                if (entity == null)
                {
                    SelectedEntities.Clear();
                    InteractionState = EditorInteractionState.DragSelect;
                    DragSelectStart = mouse;
                }
                else
                {
                    if (SelectedEntities.Contains(entity))
                    {
                        InteractionState = EditorInteractionState.DragEntity;
                        foreach (CEntity selected in SelectedEntities)
                        {
                            SelectedEntitiesOffset[selected] = selected.Physics.PositionPhysics.Position - world;
                        }
                        DragEntityStart = mouse;
                    }
                    else
                    {
                        SelectedEntities.Clear();
                        SelectedEntities.Add(entity);

                        CEditorEntityBase editor_entity = entity as CEditorEntityBase;

                        // TODO: no support for non-editor entities?
                        if (editor_entity != null)
                        {
                            CEntity preview = editor_entity.GeneratePreviewEntity();
                            if (preview != null)
                            {
                                World.EntityAdd(preview);
                            }
                        }

                        InteractionState = EditorInteractionState.DragEntity;
                        SelectedEntitiesOffset.Clear();
                        SelectedEntitiesOffset[entity] = entity.Physics.PositionPhysics.Position - world;
                        DragEntityStart = mouse;
                    }
                }
            }

            if (state.MiddleButton == ButtonState.Pressed)
            {
                World.GameCamera.Position -= new Vector3(delta, 0.0f) * 1.0f / World.GameCamera.Zoom;
            }

            if (state.RightButton == ButtonState.Pressed)
            {
                InteractionState = EditorInteractionState.ZoomCamera;
                System.Windows.Forms.Cursor.Hide();
            }
        }

        public void UpdateInteractionDragSelect(Vector2 mouse, Vector2 delta, Vector2 world)
        {
            // TODO: make this type restriction?
            int left_alt_keystate = GetKeyState((int)Keys.LeftAlt);
            bool left_alt_down = (left_alt_keystate & 0x8000) != 0;

            Vector2 drag_start = World.GameCamera.ScreenToWorld(DragSelectStart);
            Vector2 drag_end = world;
            Vector2 offset = drag_end - drag_start;
            Vector2 drag_center = drag_start + offset * 0.5f;

            Vector2 tl = Vector2.Min(drag_start, drag_end);
            Vector2 br = Vector2.Max(drag_start, drag_end);
            Vector2 size = new Vector2(Math.Abs(offset.X), Math.Abs(offset.Y));
            CollisionAABB box = new CollisionAABB(tl, size);

            HoverEntities.Clear();
            HoverEntities = World.GetEntitiesInBox(box).Where(e => e is CEditorEntityBase).ToList();

            // TODO: box interface should be topleft/bottomright? this is wrong anyway
            CDebugRender.Box(World.GameCamera.WorldMatrix, drag_center, offset, 2.0f, XnaColor.Green);

            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Released)
            {
                InteractionState = EditorInteractionState.None;
                SelectedEntities = World.GetEntitiesInBox(box).Where(e => e is CEditorEntityBase).ToList();
            }
        }

        private CEntity SpawnEntity(Vector2 position)
        {
            CEditorEntityBase editor_entity = Activator.CreateInstance(SpawnEntityType, new object[] { World, position }) as CEditorEntityBase;
            editor_entity.Physics.PositionPhysics.Position = position;
            return editor_entity;
        }
        
        public void UpdateInteractionDragEntity(Vector2 mouse, Vector2 delta, Vector2 world)
        {
            MouseState state = Mouse.GetState();

            HoverEntities.Clear();

            if (!IsInGameViewport(mouse))
                return;

            if (SelectedEntities.Count <= 0)
            {
                InteractionState = EditorInteractionState.None;
                return;
            }

            if (state.LeftButton == ButtonState.Released)
            {
                InteractionState = EditorInteractionState.None;

                Vector2 offset = mouse - DragEntityStart;
                if (offset.Length() < 2.0f)
                {
                    SelectedEntities.Clear();
                    HoverEntities.Clear();
                    CEntity hover = World.GetHighestEntityAtPosition(world);
                    if (hover != null)
                    {
                        SelectedEntities.Add(hover);
                        HoverEntities.Add(hover);
                    }
                }

                return;
            }

            // TODO: each entity needs a selection offset
            SelectedEntities.ForEach(entity => entity.Physics.PositionPhysics.Position = world + SelectedEntitiesOffset[entity]);

            // TODO: is this a bad hack?
            foreach (CEntity entity in SelectedEntities)
            {
                if (entity is CEditorEntityBase)
                    ((CEditorEntityBase)entity).EditorDirty = true;
            }
        }

        public void UpdateInteractionZoomCamera(Vector2 mouse, Vector2 delta, Vector2 world)
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

            foreach (CEntity entity in HoverEntities)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, World.GameCamera.WorldMatrix);
                Game.DefaultSpriteBatch.End();
                CDebugRender.Box(World.GameCamera.WorldMatrix, entity.Physics.PositionPhysics.Position, Vector2.One * entity.GetRadius() * 2.0f, 1.0f, XnaColor.White);
            }

            foreach (CEntity entity in SelectedEntities)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, World.GameCamera.WorldMatrix);
                Game.DefaultSpriteBatch.End();
                CDebugRender.Box(World.GameCamera.WorldMatrix, entity.Physics.PositionPhysics.Position, Vector2.One * entity.GetRadius() * 2.0f, 1.0f, XnaColor.Green);
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
            SelectedEntities = new List<CEntity>();
            SelectedEntitiesPreview = new List<CEntity>();
            HoverEntities = new List<CEntity>();

            if (old_world != null)
            {
                World.GameCamera.Position = old_world.GameCamera.Position;
                World.GameCamera.Zoom = old_world.GameCamera.Zoom;
            }
        }

        public void DeleteSelectedEntities()
        {
            foreach (CEntity entity in SelectedEntities)
            {
                World.EntityDelete(entity);
                // TODO: delete previews? (for all world entities, if is preview and parent is entity)
                var previews = from e in World.GetEntities() where e is CEditorEntityPreview && ((CEditorEntityPreview)e).Parent == entity select e;
                foreach (CEntity preview in previews)
                    World.EntityDelete(entity);
            }
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
