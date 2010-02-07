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
            this.SaveAsButton = new System.Windows.Forms.Button();
            this.NewStageButton = new System.Windows.Forms.Button();
            this.QuitButton = new System.Windows.Forms.Button();
            this.EntityTree = new System.Windows.Forms.TreeView();
            this.EntityTabs = new System.Windows.Forms.TabControl();
            this.EntityPropertyTab = new System.Windows.Forms.TabPage();
            this.EntityTreeTab = new System.Windows.Forms.TabPage();
            this.Game = new StageEditor.GameControl();
            this.EntityDeleteButton = new System.Windows.Forms.Button();
            this.EntityTabs.SuspendLayout();
            this.EntityPropertyTab.SuspendLayout();
            this.EntityTreeTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // RestartGameButton
            // 
            this.RestartGameButton.Location = new System.Drawing.Point(12, 618);
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
            this.EditorModeButton.Location = new System.Drawing.Point(12, 646);
            this.EditorModeButton.Name = "EditorModeButton";
            this.EditorModeButton.Size = new System.Drawing.Size(108, 22);
            this.EditorModeButton.TabIndex = 6;
            this.EditorModeButton.Text = "&Editor Mode";
            this.EditorModeButton.UseVisualStyleBackColor = true;
            this.EditorModeButton.Click += new System.EventHandler(this.EditorModeButton_Click);
            // 
            // EntityPropertyGrid
            // 
            this.EntityPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.EntityPropertyGrid.Name = "EntityPropertyGrid";
            this.EntityPropertyGrid.Size = new System.Drawing.Size(346, 574);
            this.EntityPropertyGrid.TabIndex = 7;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(1016, 646);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 8;
            this.SaveButton.Text = "&Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SaveAsButton
            // 
            this.SaveAsButton.Location = new System.Drawing.Point(935, 646);
            this.SaveAsButton.Name = "SaveAsButton";
            this.SaveAsButton.Size = new System.Drawing.Size(75, 23);
            this.SaveAsButton.TabIndex = 9;
            this.SaveAsButton.Text = "Save &As";
            this.SaveAsButton.UseVisualStyleBackColor = true;
            // 
            // NewStageButton
            // 
            this.NewStageButton.Location = new System.Drawing.Point(854, 646);
            this.NewStageButton.Name = "NewStageButton";
            this.NewStageButton.Size = new System.Drawing.Size(75, 23);
            this.NewStageButton.TabIndex = 10;
            this.NewStageButton.Text = "&New Stage";
            this.NewStageButton.UseVisualStyleBackColor = true;
            this.NewStageButton.Click += new System.EventHandler(this.NewStageButton_Click);
            // 
            // QuitButton
            // 
            this.QuitButton.Location = new System.Drawing.Point(1097, 646);
            this.QuitButton.Name = "QuitButton";
            this.QuitButton.Size = new System.Drawing.Size(75, 23);
            this.QuitButton.TabIndex = 11;
            this.QuitButton.Text = "&Quit";
            this.QuitButton.UseVisualStyleBackColor = true;
            this.QuitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // EntityTree
            // 
            this.EntityTree.Location = new System.Drawing.Point(0, 0);
            this.EntityTree.Name = "EntityTree";
            this.EntityTree.Size = new System.Drawing.Size(346, 578);
            this.EntityTree.TabIndex = 13;
            // 
            // EntityTabs
            // 
            this.EntityTabs.Controls.Add(this.EntityPropertyTab);
            this.EntityTabs.Controls.Add(this.EntityTreeTab);
            this.EntityTabs.Location = new System.Drawing.Point(818, 12);
            this.EntityTabs.Name = "EntityTabs";
            this.EntityTabs.SelectedIndex = 0;
            this.EntityTabs.Size = new System.Drawing.Size(354, 600);
            this.EntityTabs.TabIndex = 14;
            // 
            // EntityPropertyTab
            // 
            this.EntityPropertyTab.Controls.Add(this.EntityPropertyGrid);
            this.EntityPropertyTab.Location = new System.Drawing.Point(4, 22);
            this.EntityPropertyTab.Name = "EntityPropertyTab";
            this.EntityPropertyTab.Padding = new System.Windows.Forms.Padding(3);
            this.EntityPropertyTab.Size = new System.Drawing.Size(346, 574);
            this.EntityPropertyTab.TabIndex = 0;
            this.EntityPropertyTab.Text = "Properties";
            this.EntityPropertyTab.UseVisualStyleBackColor = true;
            // 
            // EntityTreeTab
            // 
            this.EntityTreeTab.Controls.Add(this.EntityTree);
            this.EntityTreeTab.Location = new System.Drawing.Point(4, 22);
            this.EntityTreeTab.Name = "EntityTreeTab";
            this.EntityTreeTab.Padding = new System.Windows.Forms.Padding(3);
            this.EntityTreeTab.Size = new System.Drawing.Size(346, 574);
            this.EntityTreeTab.TabIndex = 1;
            this.EntityTreeTab.Text = "Tree";
            this.EntityTreeTab.UseVisualStyleBackColor = true;
            // 
            // Game
            // 
            this.Game.CachedHandle = null;
            this.Game.Game = null;
            this.Game.GameThread = null;
            this.Game.Location = new System.Drawing.Point(12, 34);
            this.Game.Name = "Game";
            this.Game.Size = new System.Drawing.Size(800, 578);
            this.Game.TabIndex = 15;
            this.Game.Text = "Game";
            // 
            // EntityDeleteButton
            // 
            this.EntityDeleteButton.Location = new System.Drawing.Point(1097, 618);
            this.EntityDeleteButton.Name = "EntityDeleteButton";
            this.EntityDeleteButton.Size = new System.Drawing.Size(75, 23);
            this.EntityDeleteButton.TabIndex = 16;
            this.EntityDeleteButton.Text = "De&lete Entity";
            this.EntityDeleteButton.UseVisualStyleBackColor = true;
            this.EntityDeleteButton.Click += new System.EventHandler(this.EntityDeleteButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 673);
            this.Controls.Add(this.EntityDeleteButton);
            this.Controls.Add(this.Game);
            this.Controls.Add(this.EntityTabs);
            this.Controls.Add(this.QuitButton);
            this.Controls.Add(this.NewStageButton);
            this.Controls.Add(this.SaveAsButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.EditorModeButton);
            this.Controls.Add(this.InputCatcher);
            this.Controls.Add(this.RestartGameButton);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.EntityTabs.ResumeLayout(false);
            this.EntityPropertyTab.ResumeLayout(false);
            this.EntityTreeTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RestartGameButton;
        private System.Windows.Forms.TextBox InputCatcher;
        private System.Windows.Forms.Button EditorModeButton;
        private System.Windows.Forms.PropertyGrid EntityPropertyGrid;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button SaveAsButton;
        private System.Windows.Forms.Button NewStageButton;
        private System.Windows.Forms.Button QuitButton;
        private System.Windows.Forms.TreeView EntityTree;
        private System.Windows.Forms.TabControl EntityTabs;
        private System.Windows.Forms.TabPage EntityPropertyTab;
        private System.Windows.Forms.TabPage EntityTreeTab;
        private GameControl Game;
        private System.Windows.Forms.Button EntityDeleteButton;
    }
}

