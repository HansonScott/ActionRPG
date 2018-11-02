using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace RPG
{
    public partial class Game : Form
    {
        public enum ExitCommand
        {
            NewGame,
            LoadGame,
            Options,
            Exit,

            CreateCharacter,
            UsePremadeCharacter,

            Done,
            Cancel,

            Sound,
            Visuals,
            Gameplay
        }

        #region Declarations
        FormWelcome formWelcome;
        FormGameOptions formGameOptions;
        FormLoadGame formLoadGame;
        FormUsePremade formUsePremade;
        FormNewGame formNewGame;
        FormCharacterBasics formCGenBasics;
        FormAttribute formCGenAttribute;
        Session session;
        public PlayerCharacter player;

        Thread PlayThread;
        #endregion

        #region Constructor
        public Game()
        {
            InitializeComponent();

            PopWelcome();
        }
        #endregion

        #region Form Events
        void formWelcome_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (formWelcome.ExitCommand)
            {
                case (Game.ExitCommand.NewGame):
                    {
                        PopNewGame();
                        break;
                    }
                case (Game.ExitCommand.LoadGame):
                    {
                        PopLoadGame();
                        break;
                    }
                case (Game.ExitCommand.Options):
                    {
                        PopGameOptions();
                        break;
                    }
                case (Game.ExitCommand.Exit):
                    {
                        this.Close();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        void formGameOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (formGameOptions.ExitCommand)
            {
                case (Game.ExitCommand.Cancel):
                    {
                        PopWelcome();
                        break;
                    }
                case (Game.ExitCommand.Done):
                    {
                        PopWelcome();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        void formNewGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (formNewGame.ExitCommand)
            {
                case (Game.ExitCommand.CreateCharacter):
                    {
                        PopCGenBasics();
                        break;
                    }
                case (Game.ExitCommand.UsePremadeCharacter):
                    {
                        PopUsePremade();
                        break;
                    }
                case (Game.ExitCommand.Cancel):
                    {
                        PopWelcome();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        void formCGenBasics_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (formCGenBasics.ExitCommand)
            {
                case (Game.ExitCommand.Done):
                    {
                        PopCGenAttributes();
                        break;
                    }
                case (Game.ExitCommand.Cancel):
                    {
                        PopNewGame();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        void formCGenAttribute_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (formCGenAttribute.ExitCommand)
            {
                case (Game.ExitCommand.Done):
                    {
                        PopNewSession();
                        break;
                    }
                case (Game.ExitCommand.Cancel):
                    {
                        PopCGenBasics();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        void formLoadGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (formLoadGame.ExitCommand)
            {
                case (Game.ExitCommand.Done):
                    {
                        player = formLoadGame.GetLoadedActor();

                        PopNewSession();
                        break;
                    }
                case (Game.ExitCommand.Cancel):
                    {
                        PopWelcome();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        void formUsePremade_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (formUsePremade.ExitCommand)
            {
                case (Game.ExitCommand.Done):
                    {
                        PopNewSession();
                        break;
                    }
                case (Game.ExitCommand.Cancel):
                    {
                        PopNewGame();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        void session_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PlayThread.IsAlive)
            {
                PlayThread.Abort();
            }

            switch (session.ExitCommand)
            {
                case (Game.ExitCommand.LoadGame):
                    {
                        PopLoadGame();
                        break;
                    }
                case (Game.ExitCommand.Exit):
                    {
                        PopWelcome();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        #endregion

        #region Pop windows
        // welcome
        void PopWelcome()
        {
            formWelcome = new FormWelcome();
            formWelcome.FormClosing += new FormClosingEventHandler(formWelcome_FormClosing);
            formWelcome.Show();
            formWelcome.Focus();
        }
        void PopNewGame()
        {
            formNewGame = new FormNewGame();
            formNewGame.FormClosing += new FormClosingEventHandler(formNewGame_FormClosing);
            formNewGame.Show();
            formNewGame.Focus();
        }
        void PopLoadGame()
        {
            formLoadGame = new FormLoadGame();
            formLoadGame.FormClosing += new FormClosingEventHandler(formLoadGame_FormClosing);
            formLoadGame.Show();
        }

        void PopGameOptions()
        {
            formGameOptions = new FormGameOptions();
            formGameOptions.FormClosing += new FormClosingEventHandler(formGameOptions_FormClosing);
            formGameOptions.Show();
            formGameOptions.Focus();
        }
        // new game
        void PopUsePremade()
        {
            formUsePremade = new FormUsePremade(player);
            formUsePremade.FormClosing += new FormClosingEventHandler(formUsePremade_FormClosing);
            formUsePremade.Show();
            formUsePremade.Focus();
        }
        // CGen
        void PopCGenBasics()
        {
            player = new PlayerCharacter();
            formCGenBasics = new FormCharacterBasics(player);
            formCGenBasics.FormClosing += new FormClosingEventHandler(formCGenBasics_FormClosing);
            formCGenBasics.Show();
            formCGenBasics.Focus();
        }
        void PopCGenAttributes()
        {
            formCGenAttribute = new FormAttribute(player);
            formCGenAttribute.FormClosing += new FormClosingEventHandler(formCGenAttribute_FormClosing);
            formCGenAttribute.Show();
            formCGenAttribute.Focus();
        }

        void PopNewSession()
        {
            session = new Session();
            session.LoadPlayer(this.player);

            session.FormClosing += new FormClosingEventHandler(session_FormClosing);
            session.Show();
            session.Focus();
            session.inSession = true;

            // need a new thread to avoid UI stalling out in favor of the game loop.
            //session.Play();

            PlayThread = new Thread(new ThreadStart(session.Play));
            PlayThread.IsBackground = true; // this thread exits when game exits.
            PlayThread.Start();
        }
        #endregion
    }
}