using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RPG
{
    public partial class FormUsePremade : Form
    {
        private Game.ExitCommand cmd;
        private Actor thisActor;

        public Game.ExitCommand ExitCommand
        {
            get { return cmd; }
        }

        public FormUsePremade(Actor actor)
        {
            thisActor = actor;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmd = Game.ExitCommand.Cancel;
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            // load data into actor

            cmd = Game.ExitCommand.Done;
            this.Close();
        }
    }
}