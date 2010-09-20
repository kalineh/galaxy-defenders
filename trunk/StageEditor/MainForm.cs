using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Galaxy;

namespace StageEditor
{
    public partial class MainForm
        : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // put all stages into dropdown
            Assembly assembly = Assembly.GetAssembly(typeof(Galaxy.CEntity));
            IEnumerable<Type> types = assembly.GetTypes().Where(t => String.Equals(t.Namespace, "Galaxy.Stages", StringComparison.Ordinal));
            List<string> typenames = new List<String>(from t in types select t.Name);
            List<string> sorted = Galaxy.CNaturalSorter.NaturalSort(typenames);

            foreach (string name in sorted)
            {
                StageSelectDropdown.Items.Add(name);
            }

            // TODO: last selected stage
            StageSelectDropdown.Text = "EditorStage";
            StageSelectDropdown.DropDownStyle = ComboBoxStyle.DropDownList;

            StagePropertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(StagePropertyGrid_PropertyValueChanged);
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

        private void RestartGameButton_Click(object sender, EventArgs e)
        {
            GameControl game_control = this.Game;
            Galaxy.CGalaxy game = game_control.Game;
            game.GameFrame = 0;

            Galaxy.CStageDefinition definition = null;

            // TODO: generalize cleanup?
            Galaxy.CStateGame state_game = game.State as Galaxy.CStateGame;
            if (state_game != null)
            {
                definition = state_game.World.Stage.Definition;
                state_game.World.Stop();
            }

            Galaxy.CStateEditor state_editor = game.State as Galaxy.CStateEditor;
            if (state_editor != null)
            {
                definition = state_editor.StageDefinition;
            }

            definition = definition ?? Galaxy.CStageDefinition.GetStageDefinitionByName("EditorStage");


            StageSelectDropdown.Enabled = false;
            EntityTree.Enabled = false;

            game.State = new Galaxy.CStateGame(game, definition);

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
            Galaxy.CStageDefinition existing = null;
            if (state_game != null)
            {
                existing = state_game.World.Stage.Definition;
                state_game.World.Stop();
            }
            existing = existing ?? Galaxy.CStageDefinition.GetStageDefinitionByName("EditorStage");

            Galaxy.CStateEditor editor_state = new Galaxy.CStateEditor(game, existing);
            game_control.Game.State = editor_state;
            game_control.UpdateEditorPosition();

            StageSelectDropdown.Enabled = true;
            EntityTree.Enabled = true;
        }

        public PropertyGrid GetEntityPropertyGrid()
        {
            return EntityPropertyGrid;
        }

        public TreeView GetEntityTreeView()
        {
            return EntityTree;
        }

        public PropertyGrid GetStagePropertyGrid()
        {
            return StagePropertyGrid;
        }

        void StagePropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            // TODO: validate this elsewhere?
            // TODO: this is crap, dont do text and validation, make some easier dropdown selectors (types, types in namespace, functions in class, etc)
            Galaxy.CStageDefinition definition = StagePropertyGrid.SelectedObject as Galaxy.CStageDefinition;

            if (e.ChangedItem.Label == "Music")
            {
                try
                {
                    Game.Game.Content.Load<Song>(definition.MusicName);
                    if (typeof(Galaxy.CSceneryPresets).GetMethod(definition.BackgroundSceneryName) == null)
                        throw new Exception();
                    if (typeof(Galaxy.CSceneryPresets).GetMethod(definition.ForegroundSceneryName) == null)
                        throw new Exception();
                }
                catch
                {
                    e.ChangedItem.PropertyDescriptor.SetValue(StagePropertyGrid.SelectedObject, e.OldValue);
                    NotifyIcon notify = new NotifyIcon();
                    notify.BalloonTipText = "invalid";
                    return;
                }
            }

            GameControl game_control = this.Game;
            Galaxy.EditorGame game = game_control.Game;
            game.AccessMutex.WaitOne();
            Galaxy.CStateEditor editor = game.State as Galaxy.CStateEditor;
            editor.RefreshStageDefinition();
            game.AccessMutex.ReleaseMutex();
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
            Galaxy.EditorGame game = game_control.Game;
            Galaxy.CStateEditor editor = game.State as Galaxy.CStateEditor;

            game.AccessMutex.WaitOne();

            // NOTE: save on this thread before we touch the game thread in case it breaks
            editor.RefreshStageDefinition();
            Galaxy.CStageCodeWriter.Save(editor.StageDefinition);
            StagePropertyGrid.SelectedObject = editor.StageDefinition;

            game.AccessMutex.ReleaseMutex();
        }

        private void EntityDeleteButton_Click(object sender, EventArgs e)
        {
            GameControl game_control = this.Game;
            Galaxy.EditorGame game = game_control.Game;
            Galaxy.CStateEditor editor = game.State as Galaxy.CStateEditor;

            game.AccessMutex.WaitOne();
            editor.DeleteSelectedEntities();
            game.AccessMutex.ReleaseMutex();
        }

        private void PreviewEntitiesButton_Click(object sender, EventArgs e)
        {
            GameControl game_control = this.Game;
            Galaxy.EditorGame game = game_control.Game;
            Galaxy.CStateEditor editor = game.State as Galaxy.CStateEditor;

            game.AccessMutex.WaitOne();
            editor.PreviewAllEntities();
            game.AccessMutex.ReleaseMutex();
        }

        private void StageSelectDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameControl game_control = this.Game;
            Galaxy.EditorGame game = game_control.Game;
            if (game == null)
                return;

            game.AccessMutex.WaitOne();
            Galaxy.CStateEditor editor = game.State as Galaxy.CStateEditor;
            Galaxy.CStageDefinition result = Galaxy.CStageDefinition.GetStageDefinitionByName(StageSelectDropdown.Text);
            editor.ReplaceStageDefinition(result);
            StagePropertyGrid.SelectedObject = result;
            game.AccessMutex.ReleaseMutex();
        }
    }
}
