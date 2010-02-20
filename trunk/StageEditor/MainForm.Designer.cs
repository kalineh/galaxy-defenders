namespace StageEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RestartGameButton = new System.Windows.Forms.Button();
            this.InputCatcher = new System.Windows.Forms.TextBox();
            this.EditorModeButton = new System.Windows.Forms.Button();
            this.EntityPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SaveButton = new System.Windows.Forms.Button();
            this.NewStageButton = new System.Windows.Forms.Button();
            this.QuitButton = new System.Windows.Forms.Button();
            this.EntityTree = new System.Windows.Forms.TreeView();
            this.Game = new StageEditor.GameControl();
            this.EntityDeleteButton = new System.Windows.Forms.Button();
            this.PreviewEntitiesButton = new System.Windows.Forms.Button();
            this.StagePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.StageSelectDropdown = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // RestartGameButton
            // 
            this.RestartGameButton.Location = new System.Drawing.Point(12, 819);
            this.RestartGameButton.Name = "RestartGameButton";
            this.RestartGameButton.Size = new System.Drawing.Size(108, 22);
            this.RestartGameButton.TabIndex = 3;
            this.RestartGameButton.Text = "Restart &Game";
            this.RestartGameButton.UseVisualStyleBackColor = true;
            this.RestartGameButton.Click += new System.EventHandler(this.RestartGameButton_Click);
            // 
            // InputCatcher
            // 
            this.InputCatcher.Location = new System.Drawing.Point(12, 592);
            this.InputCatcher.Name = "InputCatcher";
            this.InputCatcher.Size = new System.Drawing.Size(10, 20);
            this.InputCatcher.TabIndex = 4;
            // 
            // EditorModeButton
            // 
            this.EditorModeButton.Location = new System.Drawing.Point(12, 847);
            this.EditorModeButton.Name = "EditorModeButton";
            this.EditorModeButton.Size = new System.Drawing.Size(108, 22);
            this.EditorModeButton.TabIndex = 6;
            this.EditorModeButton.Text = "&Editor Mode";
            this.EditorModeButton.UseVisualStyleBackColor = true;
            this.EditorModeButton.Click += new System.EventHandler(this.EditorModeButton_Click);
            // 
            // EntityPropertyGrid
            // 
            this.EntityPropertyGrid.HelpVisible = false;
            this.EntityPropertyGrid.Location = new System.Drawing.Point(818, 12);
            this.EntityPropertyGrid.Name = "EntityPropertyGrid";
            this.EntityPropertyGrid.Size = new System.Drawing.Size(354, 425);
            this.EntityPropertyGrid.TabIndex = 7;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(1016, 846);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 8;
            this.SaveButton.Text = "&Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // NewStageButton
            // 
            this.NewStageButton.Location = new System.Drawing.Point(935, 847);
            this.NewStageButton.Name = "NewStageButton";
            this.NewStageButton.Size = new System.Drawing.Size(75, 23);
            this.NewStageButton.TabIndex = 10;
            this.NewStageButton.Text = "&Clear Stage";
            this.NewStageButton.UseVisualStyleBackColor = true;
            this.NewStageButton.Click += new System.EventHandler(this.NewStageButton_Click);
            // 
            // QuitButton
            // 
            this.QuitButton.Location = new System.Drawing.Point(1097, 846);
            this.QuitButton.Name = "QuitButton";
            this.QuitButton.Size = new System.Drawing.Size(75, 23);
            this.QuitButton.TabIndex = 11;
            this.QuitButton.Text = "&Quit";
            this.QuitButton.UseVisualStyleBackColor = true;
            this.QuitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // EntityTree
            // 
            this.EntityTree.Location = new System.Drawing.Point(818, 443);
            this.EntityTree.Name = "EntityTree";
            this.EntityTree.Size = new System.Drawing.Size(354, 171);
            this.EntityTree.TabIndex = 13;
            // 
            // Game
            // 
            this.Game.CachedHandle = null;
            this.Game.Game = null;
            this.Game.GameThread = null;
            this.Game.Location = new System.Drawing.Point(12, 12);
            this.Game.Name = "Game";
            this.Game.Size = new System.Drawing.Size(800, 800);
            this.Game.TabIndex = 15;
            this.Game.Text = "Game";
            // 
            // EntityDeleteButton
            // 
            this.EntityDeleteButton.Location = new System.Drawing.Point(1097, 817);
            this.EntityDeleteButton.Name = "EntityDeleteButton";
            this.EntityDeleteButton.Size = new System.Drawing.Size(75, 23);
            this.EntityDeleteButton.TabIndex = 16;
            this.EntityDeleteButton.Text = "De&lete Entity";
            this.EntityDeleteButton.UseVisualStyleBackColor = true;
            this.EntityDeleteButton.Click += new System.EventHandler(this.EntityDeleteButton_Click);
            // 
            // PreviewEntitiesButton
            // 
            this.PreviewEntitiesButton.Location = new System.Drawing.Point(1016, 816);
            this.PreviewEntitiesButton.Name = "PreviewEntitiesButton";
            this.PreviewEntitiesButton.Size = new System.Drawing.Size(75, 23);
            this.PreviewEntitiesButton.TabIndex = 17;
            this.PreviewEntitiesButton.Text = "&Preview All";
            this.PreviewEntitiesButton.UseVisualStyleBackColor = true;
            this.PreviewEntitiesButton.Click += new System.EventHandler(this.PreviewEntitiesButton_Click);
            // 
            // StagePropertyGrid
            // 
            this.StagePropertyGrid.HelpVisible = false;
            this.StagePropertyGrid.Location = new System.Drawing.Point(818, 620);
            this.StagePropertyGrid.Name = "StagePropertyGrid";
            this.StagePropertyGrid.Size = new System.Drawing.Size(354, 192);
            this.StagePropertyGrid.TabIndex = 18;
            // 
            // StageSelectDropdown
            // 
            this.StageSelectDropdown.FormattingEnabled = true;
            this.StageSelectDropdown.Location = new System.Drawing.Point(889, 816);
            this.StageSelectDropdown.Name = "StageSelectDropdown";
            this.StageSelectDropdown.Size = new System.Drawing.Size(121, 21);
            this.StageSelectDropdown.TabIndex = 19;
            this.StageSelectDropdown.SelectedIndexChanged += new System.EventHandler(this.StageSelectDropdown_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 879);
            this.Controls.Add(this.StageSelectDropdown);
            this.Controls.Add(this.EntityPropertyGrid);
            this.Controls.Add(this.EntityTree);
            this.Controls.Add(this.StagePropertyGrid);
            this.Controls.Add(this.PreviewEntitiesButton);
            this.Controls.Add(this.EntityDeleteButton);
            this.Controls.Add(this.Game);
            this.Controls.Add(this.QuitButton);
            this.Controls.Add(this.NewStageButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.EditorModeButton);
            this.Controls.Add(this.InputCatcher);
            this.Controls.Add(this.RestartGameButton);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RestartGameButton;
        private System.Windows.Forms.TextBox InputCatcher;
        private System.Windows.Forms.Button EditorModeButton;
        private System.Windows.Forms.PropertyGrid EntityPropertyGrid;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button NewStageButton;
        private System.Windows.Forms.Button QuitButton;
        private System.Windows.Forms.TreeView EntityTree;
        private GameControl Game;
        private System.Windows.Forms.Button EntityDeleteButton;
        private System.Windows.Forms.Button PreviewEntitiesButton;
        private System.Windows.Forms.PropertyGrid StagePropertyGrid;
        private System.Windows.Forms.ComboBox StageSelectDropdown;
    }
}

