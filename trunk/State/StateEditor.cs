// // StateEditor.cs
//

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    using WinPoint = System.Drawing.Point;
    using XnaColor = Microsoft.Xna.Framework.Graphics.Color;
    using System.Reflection;

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
        private CShip SampleShip { get; set; }
        private SProfile WorkingProfile;
        private CScenery BackgroundScenery { get; set; }
        private CScenery ForegroundScenery { get; set; }
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
        private bool NoSelectTillRelease { get; set; }
        public bool SnapToGrid { get; set; }
        public float GridSize { get; set; }
        public CStageDefinition StageDefinition { get; set; }

        public CStateEditor(CGalaxy game, CStageDefinition stage_definition)
        {
            Game = game;
            ClearStage();
            StageDefinition = stage_definition ?? CStageDefinition.GetStageDefinitionByName("EditorStage");
            CStageGenerate.GenerateWorldFromStageDefinition(World, StageDefinition);
            SelectedEntities = new List<CEntity>();
            SelectedEntitiesPreview = new List<CEntity>();
            SelectedEntitiesOffset = new Dictionary<CEntity, Vector2>();
            HoverEntities = new List<CEntity>();
            SnapToGrid = true;
            GridSize = 8.0f;

            MethodInfo bg_method = typeof(CSceneryPresets).GetMethod(StageDefinition.BackgroundSceneryName);
            bg_method = bg_method ?? typeof (CSceneryPresets).GetMethod("Empty");
            BackgroundScenery = bg_method.Invoke(null, new object[] { World }) as CScenery;
            MethodInfo fg_method = typeof(CSceneryPresets).GetMethod(StageDefinition.ForegroundSceneryName);
            fg_method = fg_method ?? typeof (CSceneryPresets).GetMethod("Empty");
            ForegroundScenery = fg_method.Invoke(null, new object[] { World }) as CScenery;

            RecreateSampleShip();
        }

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
                SelectedEntities.Clear();
            }

            if (Game.Input.IsKeyDown(Keys.LeftControl) && Game.Input.IsKeyPressed(Keys.G))
            {
                SnapToGrid = !SnapToGrid;
            }

            UpdateMouse();

            CShip player = World.GetNearestShipEditor(Vector2.Zero);
            if (player != null)
                Game.PlayerSpawnPosition = player.Physics.Position;

            World.GameCamera.Update();
            BackgroundScenery.Update();
            ForegroundScenery.Update();
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

            UpdateCopyPaste(world);

            LastMouseInput = current;
        }

        private void UpdateCopyPaste(Vector2 world)
        {
            bool ctrl = CInput.IsRawKeyDown(Keys.LeftControl);
            bool copy = CInput.IsRawKeyPressed(Keys.C);
            bool paste = CInput.IsRawKeyPressed(Keys.V);

            if (!ctrl)
                return;

            if (copy)
            {
                CopyEntities = new List<CEntity>(SelectedEntities);
            }

            if (paste && CopyEntities != null)
            {
                Vector2 total = Vector2.Zero;
                foreach (CEntity entity in CopyEntities)
                {
                    total += entity.Physics.Position;
                }
                Vector2 average = total / CopyEntities.Count;
                Vector2 offset = world - average;

                foreach (CEntity entity in CopyEntities)
                {
                    // TODO: move this somewhere gooderer
                    CEditorEntityBase editor_entity = entity as CEditorEntityBase;
                    Type type = editor_entity.GetType();
                    Vector2 position = editor_entity.Physics.Position;
                    CStageElement element = editor_entity.GenerateStageElement();
                    CEditorEntityBase new_entity = Activator.CreateInstance(type, new object[] { World, element }) as CEditorEntityBase;
                    new_entity.Position += offset;
                    new_entity.Position = SnapPositionToGrid(new_entity.Position);

                    World.EntityAdd(new_entity);
                }

                SelectedEntities.Clear();
            }
        }
        
        public void UpdateInteractionNone(Vector2 mouse, Vector2 delta, Vector2 world)
        {
            bool left_alt_down = CInput.IsRawKeyDown(Keys.LeftAlt);
            bool left_ctrl_down = CInput.IsRawKeyDown(Keys.LeftControl);

            MouseState state = Mouse.GetState();

            if (!IsInGameViewport(mouse))
            {
                HoverEntities.Clear();
                return;
            }

            // TODO: UpdateHover();
            HoverEntities.Clear();
            CEntity hover = World.GetHighestEntityAtPosition(world);
            if (hover != null)
                HoverEntities.Add(hover);

            // TODO: DrawCursor()
            CDebugRender.Box(World.GameCamera.WorldMatrix, world, Vector2.One * 5.0f, 2.5f, XnaColor.White);
            
            // TODO: cleanup to statefulness, and keybinding system (modifier + key)
            // TODO: if (TrySpawnEntity(...))
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
                    NoSelectTillRelease = false;
                }
            }

            // TODO: if (TrySelectEntity())
            if (state.LeftButton == ButtonState.Released)
            {
                NoSelectTillRelease = false;
            }

            if (state.LeftButton == ButtonState.Pressed && !left_alt_down)
            {
                CEntity entity = World.GetHighestEntityAtPosition(world);

                if (entity == null)
                {
                    // TODO: add to selection (just ignore to avoid misclicks for now)
                    if (!CInput.IsRawKeyDown(Keys.LeftShift))
                    {
                        SelectedEntities.Clear();
                        InteractionState = EditorInteractionState.DragSelect;
                        DragSelectStart = mouse;
                        // TODO: return when functioned
                    }
                }
                else
                {
                    if (CInput.IsRawKeyDown(Keys.LeftShift))
                    {
                        if (!NoSelectTillRelease)
                        {
                            if (SelectedEntities.Contains(hover))
                                SelectedEntities.Remove(hover);
                            else
                                SelectedEntities.Add(hover);
                            NoSelectTillRelease = true;
                        }
                    }
                    else
                    {
                        if (SelectedEntities.Contains(entity))
                        {
                            InteractionState = EditorInteractionState.DragEntity;
                            foreach (CEntity selected in SelectedEntities)
                            {
                                SelectedEntitiesOffset[selected] = selected.Physics.Position - world;
                            }
                            DragEntityStart = mouse;
                        }
                        else
                        {
                            SelectedEntities.Clear();
                            SelectedEntities.Add(entity);

                            PreviewEntity(entity);

                            InteractionState = EditorInteractionState.DragEntity;
                            SelectedEntitiesOffset.Clear();
                            SelectedEntitiesOffset[entity] = entity.Physics.Position - world;
                            DragEntityStart = mouse;
                        }
                    }
                }
            }

            if (state.LeftButton == ButtonState.Pressed && left_ctrl_down)
            {
                SampleShip.Physics.Position = world;
            }

            if (state.MiddleButton == ButtonState.Pressed)
            {
                InteractionState = EditorInteractionState.ZoomCamera;
                System.Windows.Forms.Cursor.Hide();
            }

            if (state.RightButton == ButtonState.Pressed)
            {
                World.GameCamera.Position -= new Vector3(delta, 0.0f) * 1.0f / World.GameCamera.Zoom;
            }

            if (state.ScrollWheelValue != 0)
            {
                float apply = state.ScrollWheelValue * 0.005f;
                World.GameCamera.Zoom += apply;
                World.GameCamera.Zoom = MathHelper.Clamp(World.GameCamera.Zoom, 0.05f, 1.75f);
            }
        }

        public void UpdateInteractionDragSelect(Vector2 mouse, Vector2 delta, Vector2 world)
        {
            bool left_alt_down = CInput.IsRawKeyDown(Keys.LeftAlt);

            Vector2 drag_start = World.GameCamera.ScreenToWorld(DragSelectStart);
            Vector2 drag_end = world;
            Vector2 offset = drag_end - drag_start;
            Vector2 drag_center = drag_start + offset * 0.5f;

            Vector2 tl = Vector2.Min(drag_start, drag_end);
            Vector2 br = Vector2.Max(drag_start, drag_end);
            Vector2 size = new Vector2(Math.Abs(offset.X), Math.Abs(offset.Y));
            CollisionAABB box = CCollision.GetCacheAABB(this, tl, size);

            HoverEntities.Clear();
            HoverEntities = World.GetEntitiesInBox(box).Where(e => e is CEditorEntityBase).ToList();

            // TODO: box interface should be topleft/bottomright? this is wrong anyway
            CDebugRender.Box(World.GameCamera.WorldMatrix, drag_center, offset, 2.0f, XnaColor.Green);

            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Released)
            {
                InteractionState = EditorInteractionState.None;

                SelectedEntities = World.GetEntitiesInBox(box).Where(e => e is CEditorEntityBase).ToList();
                foreach (CEntity entity in SelectedEntities)
                {
                    PreviewEntity(entity);
                }
            }
        }

        public void PreviewAllEntities()
        {
            foreach (CEntity entity in World.GetEntities())
            {
                PreviewEntity(entity);
            }
        }

        public void PreviewEntity(CEntity entity)
        {
            CEditorEntityBase editor_entity = entity as CEditorEntityBase;

            if (editor_entity != null)
            {
                CEntity preview = editor_entity.GeneratePreviewEntity();
                if (preview != null)
                {
                    World.EntityAdd(preview);
                }
            }
        }

        private CEntity SpawnEntity(Vector2 position)
        {
            CEditorEntityBase editor_entity = Activator.CreateInstance(SpawnEntityType, new object[] { World, position }) as CEditorEntityBase;
            editor_entity.Physics.Position = position;
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

                Vector2 offset_inner = mouse - DragEntityStart;
                if (offset_inner.Length() < 4.0f)
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

            // ignore until sufficient movement
            Vector2 offset = mouse - DragEntityStart;
            if (offset.Length() < 4.0f)
                return;

            Vector2 snapped = SnapPositionToGrid(world);

            SelectedEntities.ForEach(entity => entity.Physics.Position = snapped + SelectedEntitiesOffset[entity]);
            SelectedEntities.ForEach(entity => entity.Physics.Position = SnapPositionToGrid(entity.Physics.Position));

            // TODO: is this a bad hack?
            foreach (CEntity entity in SelectedEntities)
            {
                if (entity is CEditorEntityBase)
                    ((CEditorEntityBase)entity).EditorDirty = true;
            }
        }

        public Vector2 SnapPositionToGrid(Vector2 position)
        {
            if (!SnapToGrid)
                return position;

            // TODO: fix grid calculation to work around 0.0f mark
            Vector2 hack = new Vector2(5000.0f, 1000.0f);
            Vector2 snapped = position + hack;
            snapped = snapped - new Vector2(snapped.X % GridSize, snapped.Y % GridSize);
            snapped -= hack;
            return snapped;
        }

        public void UpdateInteractionZoomCamera(Vector2 mouse, Vector2 delta, Vector2 world)
        {
            MouseState state = Mouse.GetState();
            if (state.MiddleButton == ButtonState.Released)
            {
                InteractionState = EditorInteractionState.None;
                System.Windows.Forms.Cursor.Show();
                return;
            }

            float apply = delta.Y * 0.005f;
            //Vector3 detransform = world.ToVector3() / World.GameCamera.Zoom;
            //Vector3 pre = detransform * World.GameCamera.Zoom;
            World.GameCamera.Zoom += apply;
            World.GameCamera.Zoom = MathHelper.Clamp(World.GameCamera.Zoom, 0.05f, 1.75f);
            //Vector3 post = detransform * World.GameCamera.Zoom;

            // close, but curves inward weird, need something else
            //Vector3 offset = post - pre;
            //World.GameCamera.Position += offset;
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
            Game.GraphicsDevice.Clear(Color.Black);
            BackgroundScenery.Draw(Game.DefaultSpriteBatch);

            foreach (CEntity entity in HoverEntities)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, World.GameCamera.WorldMatrix);
                Game.DefaultSpriteBatch.End();
                CDebugRender.Box(World.GameCamera.WorldMatrix, entity.Physics.Position, Vector2.One * entity.GetRadius() * 2.0f, 1.0f, XnaColor.White);
            }

            foreach (CEntity entity in SelectedEntities)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, World.GameCamera.WorldMatrix);
                Game.DefaultSpriteBatch.End();
                CDebugRender.Box(World.GameCamera.WorldMatrix, entity.Physics.Position, Vector2.One * entity.GetRadius() * 2.0f, 1.0f, XnaColor.Green);
            }

            World.DrawEntities(World.GameCamera);

            ForegroundScenery.Draw(Game.DefaultSpriteBatch);


            // Debug render.
            float game_width = World.GameCamera.GetGameWidth();
            float half_game_width = game_width / 2.0f;
            float game_height = World.GameCamera.ScreenSize.Y;
            CDebugRender.Line(World.GameCamera.WorldMatrix, new Vector2(-half_game_width, game_height), Vector2.UnitY * -25000.0f, 2.0f, XnaColor.Blue);
            CDebugRender.Line(World.GameCamera.WorldMatrix, new Vector2(half_game_width, game_height), Vector2.UnitY * -25000.0f, 2.0f, XnaColor.Blue);
            CDebugRender.Line(World.GameCamera.WorldMatrix, new Vector2(-half_game_width, game_height), Vector2.UnitX * World.GameCamera.GetGameWidth(), 2.0f, XnaColor.Blue);
            SampleShip = World.GetNearestShipEditor(Vector2.Zero);
            if (SampleShip != null)
            {
                CDebugRender.Box(World.GameCamera.WorldMatrix, SampleShip.Physics.Position, World.GameCamera.ScreenSize, 2.0f, XnaColor.Red);
            }

            // render debug
            CDebugRender.Render(Game);
        }

        public void ClearStage()
        {
            CWorld old_world = World;

            World = new CWorld(Game, StageDefinition);
            WorkingProfile = CSaveData.GetCurrentProfile();
            SelectedEntities = new List<CEntity>();
            SelectedEntitiesPreview = new List<CEntity>();
            HoverEntities = new List<CEntity>();

            if (old_world != null)
            {
                World.GameCamera.Position = old_world.GameCamera.Position;
                World.GameCamera.Zoom = old_world.GameCamera.Zoom;
            }

            RecreateSampleShip();
        }

        public void DeleteSelectedEntities()
        {
            foreach (CEntity entity in SelectedEntities)
            {
                World.EntityDelete(entity);
                // TODO: delete previews? (for all world entities, if is preview and parent is entity)
                var previews = from e in World.GetEntities() where e is CEditorEntityPreview && ((CEditorEntityPreview)e).Parent == entity select e;
                foreach (CEntity preview in previews)
                {
                    World.EntityDelete(entity);
                }
            }

            SelectedEntities.Clear();
            HoverEntities.Clear();
        }

        // TODO: should this functionality be on the editor even? maybe in game?
        // TODO: make a stage definition handling system? (load/save/static currentdef)
        public void ReplaceStageDefinition(CStageDefinition definition)
        {
            ClearStage();
            StageDefinition = definition;
            CStageGenerate.GenerateWorldFromStageDefinition(World, StageDefinition);

            MethodInfo bg_method = typeof(CSceneryPresets).GetMethod(StageDefinition.BackgroundSceneryName);
            bg_method = bg_method ?? typeof (CSceneryPresets).GetMethod("Empty");
            BackgroundScenery = bg_method.Invoke(null, new object[] { World }) as CScenery;
            MethodInfo fg_method = typeof(CSceneryPresets).GetMethod(StageDefinition.ForegroundSceneryName);
            fg_method = fg_method ?? typeof (CSceneryPresets).GetMethod("Empty");
            ForegroundScenery = fg_method.Invoke(null, new object[] { World }) as CScenery;
        }

        // TODO: rename me, move out of state editor too?
        public void RefreshStageDefinition()
        {
            // TODO: if we have queued entities to add, we should add them now so they get serialized
            World.ProcessEntityAdd();

            // generate entities from world
            CStageDefinition new_definition = CStageGenerate.GenerateDefinitionFromWorld(World, StageDefinition.Name);

            // rollover existing stage properties
            new_definition.DisplayName = StageDefinition.DisplayName;
            new_definition.ScrollSpeed = StageDefinition.ScrollSpeed;
            new_definition.BackgroundSceneryName = StageDefinition.BackgroundSceneryName;
            new_definition.ForegroundSceneryName = StageDefinition.ForegroundSceneryName;
            new_definition.MusicName = StageDefinition.MusicName;

            StageDefinition = new_definition;

            ClearStage();
            CStageGenerate.GenerateWorldFromStageDefinition(World, StageDefinition);

            if (!Game.EditorMode)
                CAudio.PlayMusic("Title");

            MethodInfo bg_method = typeof(CSceneryPresets).GetMethod(StageDefinition.BackgroundSceneryName);
            bg_method = bg_method ?? typeof (CSceneryPresets).GetMethod("Empty");
            BackgroundScenery = bg_method.Invoke(null, new object[] { World }) as CScenery;
            MethodInfo fg_method = typeof(CSceneryPresets).GetMethod(StageDefinition.ForegroundSceneryName);
            fg_method = fg_method ?? typeof (CSceneryPresets).GetMethod("Empty");
            ForegroundScenery = fg_method.Invoke(null, new object[] { World }) as CScenery;
        }

        public void RecreateSampleShip()
        {
            SampleShip = CShipFactory.GenerateShip(World, CSaveData.CreateDefaultProfile("Sample").Game.Pilots[0], GameControllerIndex.One);
            World.EntityAdd(SampleShip);
        }
    }
}
