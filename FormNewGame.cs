using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RPG
{
    public partial class FormNewGame : Form
    {
        private Game.ExitCommand cmd;

        public Game.ExitCommand ExitCommand
        {
            get { return cmd; }
        }

        public FormNewGame()
        {
            InitializeComponent();
        }

        private void btnNewCharacter_Click(object sender, EventArgs e)
        {
            cmd = Game.ExitCommand.CreateCharacter;
            this.Close();
        }

        private void btnUsePremadeCharacter_Click(object sender, EventArgs e)
        {
            cmd = Game.ExitCommand.UsePremadeCharacter;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmd = Game.ExitCommand.Cancel;
            this.Close();
        }
    }
}