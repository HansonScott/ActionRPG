using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RPG
{
    public partial class FormWelcome : Form
    {
        private Game.ExitCommand cmd;

        public Game.ExitCommand ExitCommand
        {
            get { return cmd; }
        }

        public FormWelcome()
        {
            InitializeComponent();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            cmd = Game.ExitCommand.NewGame;
            this.Close();
        }
        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            cmd = Game.ExitCommand.LoadGame;
            this.Close();
        }
        private void btnOptions_Click(object sender, EventArgs e)
        {
            cmd = Game.ExitCommand.Options;
            this.Close();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            cmd = Game.ExitCommand.Exit;
            this.Close();
        }
    }
}