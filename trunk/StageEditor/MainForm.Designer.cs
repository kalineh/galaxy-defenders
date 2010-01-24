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
            this.Game = new StageEditor.GameControl();
            this.RestartGameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Game
            // 
            // TODO: Code generation for 'this.Game.CachedHandle' failed because of Exception 'Invalid Primitive Type: System.IntPtr. Consider using CodeObjectCreateExpression.'.
            this.Game.Game = null;
            this.Game.GameThread = null;
            this.Game.Location = new System.Drawing.Point(12, 12);
            this.Game.Name = "Game";
            this.Game.Size = new System.Drawing.Size(800, 600);
            this.Game.TabIndex = 2;
            this.Game.Text = "Game";
            // 
            // RestartGameButton
            // 
            this.RestartGameButton.Location = new System.Drawing.Point(818, 589);
            this.RestartGameButton.Name = "RestartGameButton";
            this.RestartGameButton.Size = new System.Drawing.Size(181, 23);
            this.RestartGameButton.TabIndex = 3;
            this.RestartGameButton.Text = "Restart Game";
            this.RestartGameButton.UseVisualStyleBackColor = true;
            this.RestartGameButton.Click += new System.EventHandler(this.RestartGameButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 622);
            this.Controls.Add(this.RestartGameButton);
            this.Controls.Add(this.Game);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private StageEditor.GameControl Game;
        private System.Windows.Forms.Button RestartGameButton;


    }
}

