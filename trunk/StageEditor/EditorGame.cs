using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class EditorGame
        : Galaxy.CGalaxy
    {
        public StageEditor.GameControl GameControl { get; set; }
        public List<CEntity> PreviousSelectedEntities { get; set; }
        public Mutex AccessMutex;

        public EditorGame(StageEditor.GameControl game_control)
        {
            GameControl = game_control;
            PreviousSelectedEntities = new List<CEntity>();
            AccessMutex = new Mutex(false, "EditorGame.AccessMutex");
            EditorMode = true;

            // NOTE: reduce backbuffer so game area is full screen size
            GraphicsDeviceManager.PreferredBackBufferWidth = 1920 / 2;
            GraphicsDeviceManager.PreferredBackBufferHeight = 1080;
            GraphicsDeviceManager.ApplyChanges();
        }

        /// <summary>
        /// Initialize world and enter EditorState immediately.
        /// </summary>
        public new void Initialize()
        {
            base.Initialize();
            State = new CStateEditor(this);

            // Add editor entity types to entity tree.

            StageEditor.MainForm form = GameControl.FindForm() as StageEditor.MainForm;
            TreeView tree = form.GetEntityTreeView();

            //
            // Entities
            //
            TreeNode entities = new TreeNode("Elements");

            // TODO: refactor to general area (with entity dropdown stuff)
            foreach (Type type in CEditorEntityTypes.Types)
            {
                entities.Nodes.Add(new TreeNode() { Text = type.Name, Tag = type });
            }

            tree.Invoke((Action)(() => tree.Nodes.Add(entities)));
            tree.Invoke((Action)(() => entities.Expand()));

            tree.NodeMouseClick += new TreeNodeMouseClickEventHandler(EntityTreeNodeMouseClick);
            tree.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(EntityTreeNodeMouseDoubleClick);
        }

        void EntityTreeNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            CStateEditor editor = State as CStateEditor;
            if (editor == null)
                return;

            editor.SpawnEntityType = e.Node.Tag as Type;
        }

        void EntityTreeNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            CStateEditor editor = State as CStateEditor;
            if (editor == null)
                return;

            Vector3 position = editor.World.GameCamera.GetCenter();
            Type type = e.Node.Tag as Type;
            CEditorEntityBase entity = Activator.CreateInstance(type, new object[] { editor.World, position }) as CEditorEntityBase;
            editor.World.EntityAdd(entity);
            editor.SelectedEntities.Clear();
            editor.SelectedEntities.Add(entity);

            StageEditor.MainForm form = GameControl.FindForm() as StageEditor.MainForm;
            PropertyGrid grid = form.GetEntityPropertyGrid();
            grid.SelectedObject = entity;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public new void LoadContent()
        {
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public new void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public new void Update(GameTime game_time)
        {
            AccessMutex.WaitOne();

            base.Update(game_time);
            UpdateEditor();

            AccessMutex.ReleaseMutex();
        }

        /// <summary>
        /// Update editor! I'm a bad comment! (but it won't look neat if every function doesn't have a summary comment)
        /// </summary>
        public void UpdateEditor()
        {
            CStateEditor editor = State as CStateEditor;
            if (editor == null)
                return;

            // Update selection.
            StageEditor.MainForm form = GameControl.FindForm() as StageEditor.MainForm;
            PropertyGrid entity_grid = form.GetEntityPropertyGrid();
            List<CEntity> selected = editor.SelectedEntities;
            int difference = selected.Except(PreviousSelectedEntities).Count();
            if (difference > 0 || selected.Count != PreviousSelectedEntities.Count)
            {
                entity_grid.Invoke((Action)(() => entity_grid.SelectedObjects = selected.ToArray()));
                PreviousSelectedEntities = selected.ToList();
            }

            PropertyGrid stage_grid = form.GetStagePropertyGrid();
            if (stage_grid.SelectedObject == null || stage_grid.SelectedObject != editor.Game.StageDefinition)
            {
                stage_grid.Invoke((Action)(() => stage_grid.SelectedObject = editor.Game.StageDefinition));
                stage_grid.Invoke((Action)(() => stage_grid.Refresh()));
            }

            /*
             * CEditorEntityBase editor_entity = selected as CEditorEntityBase;
            if (editor_entity != null)
            {
                if (editor_entity.IsEditorDirty())
                {
                    grid.Invoke((Action)(() => grid.Refresh()));
                }
            }
             */
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public new void Draw(GameTime game_time)
        {
            AccessMutex.WaitOne();
            base.State.Draw();
            AccessMutex.ReleaseMutex();
        }
    }
}
