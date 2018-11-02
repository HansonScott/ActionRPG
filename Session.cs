using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RPG
{
    public partial class Session : Form
    {
        #region Declarations
        public static Session thisSession;
        public bool inSession;
        public bool isPaused = false;
        public DateTime lastPause;
        public DateTime lastUnPause;
        public TimeSpan lastPauseDuration;
        public bool ShouldDraw;

        // Game-wide variable
        private Game.ExitCommand cmd;
        public Game.ExitCommand ExitCommand
        {
            get { return cmd; }
            set { cmd = value; }
        }

        public PlayerCharacter player;
        public RPGArea thisArea;

        public Bitmap ActionPanelBackImage;
        public Graphics ActionPanelBackGraphics;
        public Graphics ActionPanelForeGraphics;

        private DateTime lastFrame;
        private int desiredFPS = 20;

        private string lastTabSelected = "";
        #endregion

        #region Constructor and Setup
        public Session()
        {
            Session.thisSession = this;

            InitializeComponent();

            ActionPanelBackImage = new Bitmap(TabPageAction.panelAction.Width, TabPageAction.panelAction.Height);
            ActionPanelBackGraphics = Graphics.FromImage(ActionPanelBackImage);
            ActionPanelForeGraphics = TabPageAction.panelAction.CreateGraphics();

            // controls drawing based on tab selection
            ShouldDraw = true;

            tabControl1.KeyPress += new KeyPressEventHandler(tabControl1_KeyPress);
        }

        void tabControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                Session.thisSession.TogglePauseGame();
            }
        }
        private void TogglePauseGame()
        {
            if (isPaused)
            {
                Print("* * * Game unpaused * * *");
                lastPauseDuration = DateTime.Now - lastPause;
                lastUnPause = DateTime.Now;
            }
            else
            {
                Print("* * * Game paused * * *");
                lastPause = DateTime.Now;
            }

            isPaused = !isPaused;
        }

        public void LoadPlayer(PlayerCharacter pc)
        {
            this.player = pc;
            thisArea = new RPGArea(player.CurrentArea);

            // propigate this actor to all tabs
            LoadActorToTabs(pc);
        }
        public void LoadActorToTabs(Actor a)
        {
            this.TabPageAttributes.LoadActor(a);
            this.TabPageInventory.LoadActor(a);
            this.TabPageAction.panelActionToolbar.LoadActor(a);
            this.TabPageMagic.LoadActor(a);
        }
        public void LoadSaveFile(string Filename)
        {
            // get all info from filename
            MessageBox.Show("This feature not implemented yet.");
        }
        #endregion

        public void Play()
        {
            while (inSession)
            {
                if (isPaused == false)
                {
                    // update game-wide
                    UpdateSession();

                    // update area objects
                    for (int i = 0; i < thisArea.GetObjects().Length; i++)
                    {
                        if (thisArea.GetObjects()[i] != null
                            && (thisArea.GetObjects()[i]).DeleteMe == true)
                        {
                            thisArea.GetObjects()[i] = null;
                        }
                        else
                        {
                            if (thisArea.GetObjects()[i] != null)
                            {
                                (thisArea.GetObjects()[i]).UpdateSelf();
                            }
                        }
                    }
                } // end if not paused.

                if (ShouldDraw)
                {
                    thisArea.DrawArea(ActionPanelBackGraphics);

                    // now transfer all dynamically drawn stuff to the current graphics (transfer buffer)
                    ActionPanelForeGraphics.DrawImage(ActionPanelBackImage, 0, 0);
                }

                // sleep until next update
                int ms = System.DateTime.Now.Millisecond;
                if (ms - lastFrame.Millisecond < 0)
                {
                    ms += 1000;
                }

                int sleepMS = Math.Max(0, (int)(1000 / desiredFPS) - (ms - lastFrame.Millisecond));
                System.Threading.Thread.Sleep(sleepMS);
                lastFrame = System.DateTime.Now;
            }
        }
        private void UpdateSession()
        {
            // update any game-wide variables or effects.
        }
        void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // before we set the new tab, look at the old, and wrap anything up.
            if (lastTabSelected == "Inventory" && 
                tabControl1.SelectedTab.Text != "Inventory")
            {
                // then we've left the inventory screen, set the items to the actionPanel
                Session.thisSession.TabPageInventory.SetGroundItemsToActionPanel();
            }

            // user has changed tabs, gather necessary data depending on tab selected.
            switch (tabControl1.SelectedTab.Text)
            {
                case "Action":
                    {
                        ShouldDraw = true;
                        if (Session.thisSession.TabPageAction.panelAction.SelectedObject == null)
                        {
                            // load nothing
                        }
                        else if (Session.thisSession.TabPageAction.panelAction.SelectedObject.GetType()
                            == typeof(PlayerCharacter))
                        {
                            Session.thisSession.TabPageAction.panelActionToolbar.LoadActor(
                                (PlayerCharacter)Session.thisSession.TabPageAction.panelAction.SelectedObject);
                        }
                        else if (Session.thisSession.TabPageAction.panelAction.SelectedObject.GetType()
                                == typeof(Actor))
                        {
                            Session.thisSession.TabPageAction.panelActionToolbar.LoadActor(
                                (Actor)Session.thisSession.TabPageAction.panelAction.SelectedObject);
                        }
                        else if (Session.thisSession.TabPageAction.panelAction.SelectedObject.GetType()
                                == typeof(RPGDrop))
                        {
                            Session.thisSession.TabPageAction.panelActionToolbar.LoadDrop(
                                (RPGDrop)Session.thisSession.TabPageAction.panelAction.SelectedObject);
                        }
                        break;
                    }
                case "Map":
                    {
                        ShouldDraw = false;
                        break;
                    }
                case "Character Info":
                    {
                        ShouldDraw = false;
                        if (Session.thisSession.TabPageAction.panelAction.SelectedObject == null)
                        {
                            Session.thisSession.TabPageAttributes.LoadActor(Session.thisSession.player);
                        }
                        else if (Session.thisSession.TabPageAction.panelAction.SelectedObject.GetType()
                            == typeof(PlayerCharacter))
                        {
                            Session.thisSession.TabPageAttributes.LoadActor(
                                (PlayerCharacter)Session.thisSession.TabPageAction.panelAction.SelectedObject);
                        }
                        else if (Session.thisSession.TabPageAction.panelAction.SelectedObject.GetType()
                                == typeof(Actor))
                        {
                            Session.thisSession.TabPageAttributes.LoadActor(
                                (Actor)Session.thisSession.TabPageAction.panelAction.SelectedObject);
                        }
                        break;
                    }
                case "Inventory":
                    {
                        ShouldDraw = false;

                        if (Session.thisSession == null
                        || Session.thisSession.TabPageAction == null
                        || Session.thisSession.TabPageAction.panelAction == null
                        || Session.thisSession.TabPageAction.panelAction.SelectedObject == null)
                        {
                            Session.thisSession.TabPageInventory.LoadActor(this.player);
                            break;
                        }

                        if (Session.thisSession.TabPageAction.panelAction.SelectedObject.GetType()
                            == typeof(PlayerCharacter))
                        {
                            Session.thisSession.TabPageInventory.LoadActor(
                                (PlayerCharacter)Session.thisSession.TabPageAction.panelAction.SelectedObject);
                        }
                        else if(Session.thisSession.TabPageAction.panelAction.SelectedObject.GetType()
                                == typeof(Actor))
                        {
                            Session.thisSession.TabPageInventory.LoadActor(
                                (Actor)Session.thisSession.TabPageAction.panelAction.SelectedObject);
                        }
                        break;
                    }
                case "Magic":
                    {
                        ShouldDraw = false;
                        if (Session.thisSession == null
                        || Session.thisSession.TabPageAction == null
                        || Session.thisSession.TabPageAction.panelAction == null 
                        || Session.thisSession.TabPageAction.panelAction.SelectedObject == null)
                        {
                            Session.thisSession.TabPageMagic.LoadActor(this.player);
                            break;
                        }

                        if (Session.thisSession.TabPageAction.panelAction.SelectedObject.GetType()
                            == typeof(PlayerCharacter))
                        {
                            Session.thisSession.TabPageMagic.LoadActor(
                                (PlayerCharacter)Session.thisSession.TabPageAction.panelAction.SelectedObject);
                        }
                        else if (Session.thisSession.TabPageAction.panelAction.SelectedObject.GetType()
                                == typeof(Actor))
                        {
                            Session.thisSession.TabPageMagic.LoadActor(
                                (Actor)Session.thisSession.TabPageAction.panelAction.SelectedObject);
                        }
                        break;
                    }
                case "Menu":
                    {
                        ShouldDraw = false;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            // now set choice as last
            lastTabSelected = tabControl1.SelectedTab.Text;

        }
        
        public static void Print(string msg)
        {
            if (thisSession != null)
            {
                thisSession.TabPageAction.panelActionToolbar.Print(msg);
            }
        }
    }
}