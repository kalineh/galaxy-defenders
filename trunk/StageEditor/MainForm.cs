﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace StageEditor
{
    using XnaKeys = Microsoft.Xna.Framework.Input.Keys;
    using WinKeys = System.Windows.Forms.Keys;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.InputCatcher.KeyDown += KeyDownHandler;
            this.InputCatcher.KeyUp += KeyUpHandler;
        }

        protected void UpdateEditorPosition()
        {
            this.Game.UpdateEditorPosition();
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            UpdateEditorPosition();
            base.OnClientSizeChanged(e);
        }

        protected override void OnMove(EventArgs e)
        {
            UpdateEditorPosition();
            base.OnMove(e);
        }

        protected void KeyDownHandler(object sender, KeyEventArgs e)
        {
            this.Game.OnKeyDown(e);
        }

        protected void KeyUpHandler(object sender, KeyEventArgs e)
        {
            this.Game.OnKeyUp(e);
        }

        private void RestartGameButton_Click(object sender, EventArgs e)
        {
            GameControl game_control = this.Game;
            Galaxy.CGalaxy game = game_control.Game;
            game_control.Game.State = new Galaxy.CStateGame(game);

            TextBox input_catcher = this.InputCatcher;
            while (!input_catcher.Focus())
            {
            }
            input_catcher.Visible = true;
        }

        private void EditorModeButton_Click(object sender, EventArgs e)
        {
            GameControl game_control = this.Game;
            Galaxy.CGalaxy game = game_control.Game;
            game_control.Game.State = new Galaxy.CStateEditor(game);
            game_control.UpdateEditorPosition();
        }

        public PropertyGrid GetEntityPropertyGrid()
        {
            return EntityPropertyGrid;
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewStageButton_Click(object sender, EventArgs e)
        {
            GameControl game_control = this.Game;
            Galaxy.CGalaxy game = game_control.Game;
            Galaxy.CStateEditor editor = game.State as Galaxy.CStateEditor;
            editor.ClearStage();
        }

        private void SaveAsButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Stage|*.cs";
            save.Title = "Save Stage";
            string cwd = Directory.GetCurrentDirectory();
            string base_ = cwd.Substring(0, cwd.LastIndexOf("StageEditor"));
            save.InitialDirectory = base_ + "StageDefinitions";
            save.ShowDialog();
            string filename = save.FileName;
            if (filename == null)
                return;

            GameControl game_control = this.Game;
            Galaxy.CGalaxy game = game_control.Game;
            Galaxy.CStateEditor editor = game.State as Galaxy.CStateEditor;
            editor.StageFilename = filename;
            Galaxy.CStageWriter.Save(editor.StageFilename, editor.CurrentStageDefinition);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            GameControl game_control = this.Game;
            Galaxy.CGalaxy game = game_control.Game;
            Galaxy.CStateEditor editor = game.State as Galaxy.CStateEditor;
            Galaxy.CStageWriter.Save(editor.StageFilename, editor.CurrentStageDefinition);
        }
    }
}