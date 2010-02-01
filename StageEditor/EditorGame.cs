using System;
using System.Collections.Generic;
using System.Linq;
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

        public new void Initialize()
        {
            base.Initialize();
            State = new CStateEditor(this);
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

        public void UpdateEditor()
        {
            CStateEditor editor = State as CStateEditor;
            if (editor == null)
                return;

            StageEditor.MainForm form = GameControl.FindForm() as StageEditor.MainForm;
            PropertyGrid grid = form.GetEntityPropertyGrid();
            CEntity selected = editor.SelectedEntity;
            //if (selected == null)
                //return;

            //grid.SelectedObject = selected;
            grid.Invoke((Action)(() => grid.SelectedObject = selected));
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public new void Draw(GameTime game_time)
        {
            //base.Draw(game_time);
            base.State.Draw();
            //GraphicsDevice.Present();
        }
    }
}
