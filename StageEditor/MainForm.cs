using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

namespace StageEditor
{

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.InputCatcher.KeyDown += KeyDownHandler;
            this.InputCatcher.KeyUp += KeyUpHandler;

            // put all stages into dropdown
            Assembly assembly = Assembly.GetAssembly(typeof(Galaxy.CEntity));
            IEnumerable<Type> types = assembly.GetTypes().Where(t => String.Equals(t.Namespace, "Galaxy.Stages", StringComparison.Ordinal));
            foreach (Type type in types)
            {
                StageSelectDropdown.Items.Add(type.Name);
            }
            // TODO: last selected stage
            StageSelectDropdown.Text = "EditorStage";
            StageSelectDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
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
            game.GameFrame = 0;

            // TODO: generalize cleanup?
            Galaxy.CStateGame state_game = game.State as Galaxy.CStateGame;
            if (state_game != null)
            {
                state_game.World.Stop();
            }

            game.State = new Galaxy.CStateGame(game);

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
            Galaxy.CState state = game_control.Game.State;

            // TODO: generalize cleanup?
            Galaxy.CStateGame state_game = game.State as Galaxy.CStateGame;
            if (state_game != null)
            {
                state_game.World.Stop();
            }

            game_control.Game.State = new Galaxy.CStateEditor(game);
            game_control.UpdateEditorPosition();
        }

        public PropertyGrid GetEntityPropertyGrid()
        {
            return EntityPropertyGrid;
        }

        public TreeView GetEntityTreeView()
        {
            return EntityTree;
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

        private void SaveButton_Click(object sender, EventArgs e)
        {
            GameControl game_control = this.Game;
            Galaxy.CGalaxy game = game_control.Game;
            Galaxy.CStateEditor editor = game.State as Galaxy.CStateEditor;
            editor.UpdateStageDefinition();
            editor.ReplaceStageDefinition(game.StageDefinition);
            Galaxy.CStageCodeWriter.Save(game.StageDefinition);
        }

        private void EntityDeleteButton_Click(object sender, EventArgs e)
        {
            GameControl game_control = this.Game;
            Galaxy.CGalaxy game = game_control.Game;
            Galaxy.CStateEditor editor = game.State as Galaxy.CStateEditor;
            editor.DeleteSelectedEntities();
        }

        private void PreviewEntitiesButton_Click(object sender, EventArgs e)
        {
            GameControl game_control = this.Game;
            Galaxy.CGalaxy game = game_control.Game;
            Galaxy.CStateEditor editor = game.State as Galaxy.CStateEditor;
            editor.PreviewAllEntities();
        }

        private void StageSelectDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameControl game_control = this.Game;
            Galaxy.CGalaxy game = game_control.Game;
            if (game == null)
                return;

            Galaxy.CStateEditor editor = game.State as Galaxy.CStateEditor;
            Galaxy.CStageDefinition result = Galaxy.CStageDefinition.GetStageDefinitionByName(StageSelectDropdown.Text);
            editor.ReplaceStageDefinition(result);
        }
    }
}
