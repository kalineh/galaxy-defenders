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
            this.Game = new StageEditor.GameControl();
            this.EditorModeButton = new System.Windows.Forms.Button();
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
            // Game
            // 
            this.Game.CachedHandle = null;
            this.Game.Game = null;
            this.Game.GameThread = null;
            this.Game.Location = new System.Drawing.Point(12, 12);
            this.Game.Name = "Game";
            this.Game.Size = new System.Drawing.Size(800, 600);
            this.Game.TabIndex = 5;
            this.Game.Text = "Game";
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 673);
            this.Controls.Add(this.EditorModeButton);
            this.Controls.Add(this.InputCatcher);
            this.Controls.Add(this.RestartGameButton);
            this.Controls.Add(this.Game);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RestartGameButton;
        private System.Windows.Forms.TextBox InputCatcher;
        private GameControl Game;
        private System.Windows.Forms.Button EditorModeButton;
    }
}

