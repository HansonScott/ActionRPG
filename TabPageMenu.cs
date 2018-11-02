using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace RPG
{
    class TabPageMenu: TabPage
    {
        #region Declarations
        private Button btnExit;
        private Button btnSaveGame;
        private Button btnLoadGame;
        private FormLoadGame flg;
        #endregion

        #region Constructor
        public TabPageMenu()
        {
            // basic visuals set in Session Designer (Container)

            this.btnExit = new Button();
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(450, 370);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(115, 30);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit Game";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            this.btnLoadGame = new Button();
            this.btnLoadGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadGame.Location = new System.Drawing.Point(450, 300);
            this.btnLoadGame.Name = "btnLoadGame";
            this.btnLoadGame.Size = new System.Drawing.Size(115, 30);
            this.btnLoadGame.TabIndex = 1;
            this.btnLoadGame.Text = "Load Game";
            this.btnLoadGame.UseVisualStyleBackColor = true;
            this.btnLoadGame.Click += new System.EventHandler(this.btnLoadGame_Click);

            this.btnSaveGame = new Button();
            this.btnSaveGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveGame.Location = new System.Drawing.Point(450, 230);
            this.btnSaveGame.Name = "btnSaveGame";
            this.btnSaveGame.Size = new System.Drawing.Size(115, 30);
            this.btnSaveGame.TabIndex = 0;
            this.btnSaveGame.Text = "Save Game";
            this.btnSaveGame.UseVisualStyleBackColor = true;
            this.btnSaveGame.Click += new System.EventHandler(this.btnSaveGame_Click);

            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLoadGame);
            this.Controls.Add(this.btnSaveGame);
        }
        #endregion

        #region Events
        private void btnSaveGame_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                // get the filename.
                string filename = sfd.FileName;

                // save all data to file.
                MessageBox.Show("This feature not implemented yet...");
            }
        }
        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            // don't want to mix with the initial linking, so do our own.
            flg = new FormLoadGame();
            flg.FormClosing += new FormClosingEventHandler(flg_FormClosing);
            flg.Show();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Session.thisSession.ExitCommand = Game.ExitCommand.Exit;
            Session.thisSession.Close();
        }
        void flg_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (flg.ExitCommand)
            {
                case (Game.ExitCommand.Cancel):
                    {
                        // do nothing, just return;
                        break;
                    }
                case (Game.ExitCommand.Done):
                    {
                        // get file from dialog
                        string file = flg.GetLoadedFile();

                        // load data from file
                        Session.thisSession.LoadSaveFile(file);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        #endregion
    }
}
