using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Galaxy
{
    public class EditorGame
        : Galaxy.CGalaxy
    {
        public StageEditor.GameControl GameControl { get; set; }

        public EditorGame(StageEditor.GameControl game_control)
        {
            GameControl = game_control;
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

            TreeNode entity_spawner = new TreeNode("EntitySpawner");

            entity_spawner.Nodes.Add("CAsteroid");
            entity_spawner.Nodes.Add("CBeard");
            entity_spawner.Nodes.Add("CPewPew");
            entity_spawner.Nodes.Add("CSinBall");
            entity_spawner.Nodes.Add("CTurret");

            tree.Nodes.Add(entity_spawner);

            entity_spawner.Expand();

            tree.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(EntityTreeNodeMouseDoubleClick);

        }

        void EntityTreeNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            CStateEditor editor = State as CStateEditor;
            if (editor == null)
                return;

            string typename = "Galaxy." + e.Node.Text;
            Type type = Assembly.GetAssembly(typeof(CEntity)).GetType(typename);

            Vector3 position = editor.World.GameCamera.GetCenter();

            CSpawnerEntity element = new CSpawnerEntity()
            {
                Type = type,
                Position = position.ToVector2(),
                CustomMover = CMoverPresets.MoveDown(1.0f),
                SpawnCount = 1,
                SpawnTimer = new CSpawnTimerInterval(),
                SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(300.0f, 300.0f) },
            };

            Editor.CSpawnerEntity entity = new Editor.CSpawnerEntity(editor.World, element);

            // actual entity gen
            //CEntity entity = Activator.CreateInstance(type, new object[] { editor.World, new Vector2(300.0f, 300.0f) }) as CEntity;

            editor.World.EntityAdd(entity);

            StageEditor.MainForm form = GameControl.FindForm() as StageEditor.MainForm;
            PropertyGrid grid = form.GetEntityPropertyGrid();
            editor.SelectedEntity = entity;
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
            base.Update(game_time);
            UpdateEditor();
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
            PropertyGrid grid = form.GetEntityPropertyGrid();
            CEntity selected = editor.SelectedEntity;
            if (selected != grid.SelectedObject)
            {
                grid.Invoke((Action)(() => grid.SelectedObject = selected));
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public new void Draw(GameTime game_time)
        {
            base.State.Draw();
        }
    }
}
