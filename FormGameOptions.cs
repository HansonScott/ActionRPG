using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RPG
{
    public partial class FormGameOptions : Form
    {
        private Game.ExitCommand cmd;

        public Game.ExitCommand ExitCommand
        {
            get { return cmd; }
        }
        public FormGameOptions()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmd = Game.ExitCommand.Cancel;
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Settings saved.");
            cmd = Game.ExitCommand.Done;
            this.Close();
        }
    }
}