using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RPG
{
    public partial class FormLoadGame : Form
    {
        #region Declarations
        private Game.ExitCommand cmd;
        public Game.ExitCommand ExitCommand
        {
            get { return cmd; }
        }

        private PlayerCharacter player;
        public PlayerCharacter GetLoadedActor()
        {
            return player;
        }

        private string file;
        public string GetLoadedFile()
        {
            return file;
        }
        #endregion

        #region Constructor
        public FormLoadGame()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmd = Game.ExitCommand.Cancel;
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            cmd = Game.ExitCommand.Done;
            this.Close();
        }
        #endregion
    }
}