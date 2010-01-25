using System;
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

            //EventInput.EventInput.Initialize(this.Handle);
            //EventInput.EventInput.KeyUp += KeyUpHandler;
            //EventInput.EventInput.KeyDown += KeyDownHandler;

            this.InputCatcher.KeyDown += KeyDownHandler;
            this.InputCatcher.KeyUp += KeyUpHandler;
    //public delegate void KeyEventHandler(object sender, KeyEventArgs e);
        }

        //protected void KeyUpHandler(object sender, EventInput.KeyEventArgs e)
        //{
            //GameControl game_control = this.Game;
            //Galaxy.CGalaxy game = game_control.Game;
        //}

        //protected void KeyDownHandler(object sender, EventInput.KeyEventArgs e)
        //{
            //GameControl game_control = this.Game;
            //Galaxy.CGalaxy game = game_control.Game;
        //}

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
    }
}
