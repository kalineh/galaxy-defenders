using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StageEditor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void RestartGameButton_Click(object sender, EventArgs e)
        {
            GameControl game = this.Game;
            game.Game.State = new Galaxy.CStateGame(game.Game);
        }
    }
}
