using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace RPG
{
    public class PanelActionToolbar: Panel
    {
        #region Declarations
        public int LBL_HEIGHT = 20;
        public int LBL_WIDTH = 100;
        public int BTN_HEIGHT = 30;
        public int BTN_WIDTH = 80;

        public int BTN_COL_1 = 670;
        public int BTN_COL_2 = 750;
        public int BTN_COL_3 = 830;
        public int BTN_COL_4 = 910;

        public int BTN_ROW_1 = 5;
        public int BTN_ROW_2 = 35;
        public int BTN_ROW_3 = 65;

        public Color BTN_COLOR_TEXT = Color.Black;
        public Color BTN_COLOR_BACK = Color.Gray;

        // info labels
        Label lblName;
        Label lblHP;
        Label lblMP;
        Label lblAttDef;

        // ActionTicker
        ActionTicker ticker;

        // command buttons
        ActionButton btnTalk;
        ActionButton btnAttack;
        ActionButton btnUse;

        ActionButton btnSpell1;
        ActionButton btnSpell2;
        ActionButton btnSpell3;
        ActionButton btnSkill1;
        ActionButton btnSkill2;
        ActionButton btnSkill3;
        ActionButton btnItem1;
        ActionButton btnItem2;
        ActionButton btnItem3;
        #endregion

        #region Constructors and Setup
        public PanelActionToolbar()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();

            #region Stat labels
            lblName = new Label();
            lblName.Font = new Font(lblName.Font, FontStyle.Bold); 
            lblName.Location = new Point(10, 10);
            lblName.Height = LBL_HEIGHT;
            lblName.Width = LBL_WIDTH;
            this.Controls.Add(lblName);

            lblHP = new Label();
            lblHP.Location = new Point(10, 30);
            lblHP.Height = LBL_HEIGHT;
            lblHP.Width = LBL_WIDTH;
            this.Controls.Add(lblHP);

            lblMP = new Label();
            lblMP.Location = new Point(10, 50);
            lblMP.Height = LBL_HEIGHT;
            lblMP.Width = LBL_WIDTH;
            this.Controls.Add(lblMP);

            lblAttDef = new Label();
            lblAttDef.Location = new Point(10, 70);
            lblAttDef.Height = LBL_HEIGHT;
            lblAttDef.Width = LBL_WIDTH;
            this.Controls.Add(lblAttDef);

            SetAllLabelTextsToDefault();
            #endregion

            // Ticker
            ticker = new ActionTicker();
            ticker.Width = 500;
            ticker.Height = 90;
            ticker.Location = new Point(150, 5);
            this.Controls.Add(ticker);

            #region Buttons
            btnTalk = new ActionButton(ActionButton.ActionButtonType.BasicAction);
            btnTalk.Location = new Point(BTN_COL_1, BTN_ROW_1);
            SetBtnSize(btnTalk);
            btnTalk.MouseClick += new MouseEventHandler(actionButton_MouseClick);
            this.Controls.Add(btnTalk);

            btnAttack = new ActionButton(ActionButton.ActionButtonType.BasicAction);
            btnAttack.Location = new Point(BTN_COL_1, BTN_ROW_2);
            SetBtnSize(btnAttack);
            btnAttack.MouseClick += new MouseEventHandler(actionButton_MouseClick);
            this.Controls.Add(btnAttack);

            btnUse = new ActionButton(ActionButton.ActionButtonType.BasicAction);
            btnUse.Location = new Point(BTN_COL_1, BTN_ROW_3);
            SetBtnSize(btnUse);
            btnUse.MouseClick += new MouseEventHandler(actionButton_MouseClick);
            this.Controls.Add(btnUse);

            btnSpell1 = new ActionButton(ActionButton.ActionButtonType.SpellAction);
            btnSpell1.Location = new Point(BTN_COL_2, BTN_ROW_1);
            SetBtnSize(btnSpell1);
            btnSpell1.MouseClick += new MouseEventHandler(actionButton_MouseClick);
            this.Controls.Add(btnSpell1);

            btnSpell2 = new ActionButton(ActionButton.ActionButtonType.SpellAction);
            btnSpell2.Location = new Point(BTN_COL_2, BTN_ROW_2);
            SetBtnSize(btnSpell2);
            btnSpell2.MouseClick += new MouseEventHandler(actionButton_MouseClick);
            this.Controls.Add(btnSpell2);

            btnSpell3 = new ActionButton(ActionButton.ActionButtonType.SpellAction);
            btnSpell3.Location = new Point(BTN_COL_2, BTN_ROW_3);
            SetBtnSize(btnSpell3);
            btnSpell3.MouseClick += new MouseEventHandler(actionButton_MouseClick);
            this.Controls.Add(btnSpell3);

            btnSkill1 = new ActionButton(ActionButton.ActionButtonType.SkillAction);
            btnSkill1.Location = new Point(BTN_COL_3, BTN_ROW_1);
            SetBtnSize(btnSkill1);
            btnSkill1.MouseClick += new MouseEventHandler(actionButton_MouseClick);
            this.Controls.Add(btnSkill1);

            btnSkill2 = new ActionButton(ActionButton.ActionButtonType.SkillAction);
            btnSkill2.Location = new Point(BTN_COL_3, BTN_ROW_2);
            SetBtnSize(btnSkill2);
            btnSkill2.MouseClick += new MouseEventHandler(actionButton_MouseClick);
            this.Controls.Add(btnSkill2);

            btnSkill3 = new ActionButton(ActionButton.ActionButtonType.SkillAction);
            btnSkill3.Location = new Point(BTN_COL_3, BTN_ROW_3);
            SetBtnSize(btnSkill3);
            btnSkill3.MouseClick += new MouseEventHandler(actionButton_MouseClick);
            this.Controls.Add(btnSkill3);

            btnItem1 = new ActionButton(ActionButton.ActionButtonType.ItemAction);
            btnItem1.Location = new Point(BTN_COL_4, BTN_ROW_1);
            SetBtnSize(btnItem1);
            btnItem1.MouseClick += new MouseEventHandler(actionButton_MouseClick);
            this.Controls.Add(btnItem1);

            btnItem2 = new ActionButton(ActionButton.ActionButtonType.ItemAction);
            btnItem2.Location = new Point(BTN_COL_4, BTN_ROW_2);
            SetBtnSize(btnItem2);
            btnItem2.MouseClick += new MouseEventHandler(actionButton_MouseClick);
            this.Controls.Add(btnItem2);

            btnItem3 = new ActionButton(ActionButton.ActionButtonType.ItemAction);
            btnItem3.Location = new Point(BTN_COL_4, BTN_ROW_3);
            SetBtnSize(btnItem3);
            btnItem3.MouseClick += new MouseEventHandler(actionButton_MouseClick);
            this.Controls.Add(btnItem3);

            SetAllButtonTextsToDefault();
            #endregion

            this.ResumeLayout(false);
        }

        public void LoadActor(Actor a)
        {
            //set Labels
            lblName.Text = "Name: " + a.Name;
            lblHP.Text = "HP: " + a.HPCurrent + " / " + a.HPCurrentMax;
            lblMP.Text = "MP: " + a.MPCurrent + " / " + a.MPCurrentMax;
            lblAttDef.Text = "Att: " + a.CurrentAttack + " / Def: " + a.CurrentDefense;
            
            //load Buttons - quick items
            RPGItem[] quickItems = a.inventory.GetQuickItems();
            for (int i = 0; i < quickItems.Length; i++)
            {
                LoadQuickItem(i, quickItems[i]);
            }

            // load Buttons - quick spells
            RPGSpellBook quickSpells = a.QuickSpellBook;
            
            for (int i = 0; i < quickSpells.SpellCountMax; i++)
            {
                LoadQuickSpell(i, quickSpells.GetSpellAtIndex(i));
            }

        }
        public void LoadDrop(RPGDrop d)
        {
            //Labels
            lblName.Text = "Item bag";
            lblHP.Text = "contains " + d.GetItems().Length + " item";
            if (d.GetItems().Length > 1)
            {
                lblHP.Text += "s";
            }
            lblMP.Text = "";
            lblAttDef.Text = "";

            // Buttons
        }
        #endregion

        #region Events
        void actionButton_MouseClick(object sender, MouseEventArgs e)
        {
            ActionButton btn = (sender as ActionButton);
            // clear others if appropriate
            btn.ActionButton_OnClick();

            if (btn.IsExclusive && btn.IsSelected)
            {
                // note this causes the buttons not to respond for some reason, taking two clicks.
                ClearAllButtonSelections(btn);
            }
        }
        public void ClearActor()
        {
            // clear labels
            SetAllLabelTextsToDefault();

            // clear button texts
            SetAllButtonTextsToDefault();

            // clear button selections
            ClearAllButtonSelections(); // using a new one will clear all.
        }
        public void Print(string msg)
        {
            this.ticker.Print(msg);
        }
        #endregion

        #region Public methods
        public bool isAnyButtonSelected()
        {
            return (GetSelectedButtons().Length > 0);
        }
        public bool isActionButtonSelected()
        {
            if (this.btnAttack.IsSelected) { return true; }
            if (this.btnTalk.IsSelected) { return true; }
            if (this.btnUse.IsSelected) { return true; }
            return false;

        }
        public bool isSkillButtonSelected()
        {
            if (this.btnSkill1.IsSelected) { return true; }
            if (this.btnSkill2.IsSelected) { return true; }
            if (this.btnSkill3.IsSelected) { return true; }
            return false;

        }
        public bool isSpellButtonSelected()
        {
            if (this.btnSpell1.IsSelected) { return true; }
            if (this.btnSpell2.IsSelected) { return true; }
            if (this.btnSpell3.IsSelected) { return true; }
            return false;
        }
        public bool isItemButtonSelected()
        {
            if (this.btnItem1.IsSelected) { return true; }
            if (this.btnItem2.IsSelected) { return true; }
            if (this.btnItem3.IsSelected) { return true; }
            return false;
        }
        public ActionButton[] GetSelectedButtons()
        {
            ArrayList btns = new ArrayList();
            if (this.btnAttack.IsSelected) { btns.Add(btnAttack); }
            if (this.btnTalk.IsSelected) { btns.Add(btnTalk); }
            if (this.btnUse.IsSelected) { btns.Add(btnUse); }
            if (this.btnSkill1.IsSelected) { btns.Add(btnSkill1); }
            if (this.btnSkill2.IsSelected) { btns.Add(btnSkill2); }
            if (this.btnSkill3.IsSelected) { btns.Add(btnSkill3); }
            if (this.btnSpell1.IsSelected) { btns.Add(btnSpell1); }
            if (this.btnSpell2.IsSelected) { btns.Add(btnSpell2); }
            if (this.btnSpell3.IsSelected) { btns.Add(btnSpell3); }
            if (this.btnItem1.IsSelected) { btns.Add(btnItem1); }
            if (this.btnItem2.IsSelected) { btns.Add(btnItem2); }
            if (this.btnItem3.IsSelected) { btns.Add(btnItem3); }

            ActionButton[] btnsArray = btns.ToArray(typeof(ActionButton)) as ActionButton[];
            return btnsArray;
        }
        #endregion

        #region Private methods
        private void SetBtnSize(ActionButton button)
        {
            button.Height = BTN_HEIGHT;
            button.Width = BTN_WIDTH;
        }
        private void SetAllLabelTextsToDefault()
        {
            lblName.Text = "Name: ";
            lblHP.Text = "HP / HP";
            lblMP.Text = "MP / MP";
            lblAttDef.Text = "ATT / DEF";
        }
        private void SetAllButtonTextsToDefault()
        {
            btnTalk.ButtonText = "Talk";
            btnAttack.ButtonText = "Attack";
            btnUse.ButtonText = "Use";
            btnSpell1.ButtonText = "Spell 1";
            btnSpell2.ButtonText = "Spell 2";
            btnSpell3.ButtonText = "Spell 3";
            btnSkill1.ButtonText = "Skill 1";
            btnSkill2.ButtonText = "Skill 2";
            btnSkill3.ButtonText = "Skill 3";
            btnItem1.ButtonText = "Item 1";
            btnItem2.ButtonText = "Item 2";
            btnItem3.ButtonText = "Item 3";
        }
        private void ClearAllButtonSelections()
        {
            ClearButton(btnTalk);
            ClearButton(btnAttack);
            ClearButton(btnUse);
            ClearButton(btnSpell1);
            ClearButton(btnSpell2);
            ClearButton(btnSpell3);
            ClearButton(btnSkill1);
            ClearButton(btnSkill2);
            ClearButton(btnSkill3);
            ClearButton(btnItem1);
            ClearButton(btnItem2);
            ClearButton(btnItem3);
        }
        private void ClearAllButtonSelections(ActionButton btn)
        {
            // clear all other buttons

            if (btn != btnTalk) ClearButton(btnTalk);
            if (btn != btnAttack) ClearButton(btnAttack);
            if (btn != btnUse) ClearButton(btnUse);
            if (btn != btnSpell1) ClearButton(btnSpell1);
            if (btn != btnSpell2) ClearButton(btnSpell2);
            if (btn != btnSpell3) ClearButton(btnSpell3);
            if (btn != btnSkill1) ClearButton(btnSkill1);
            if (btn != btnSkill2) ClearButton(btnSkill2);
            if (btn != btnSkill3) ClearButton(btnSkill3);
            if (btn != btnItem1) ClearButton(btnItem1);
            if (btn != btnItem2) ClearButton(btnItem2);
            if (btn != btnItem3) ClearButton(btnItem3);
        }
        private void ClearButton(ActionButton btn)
        {
            btn.IsSelected = false;
        }

        private void LoadQuickItem(int index, RPGItem item)
        {
            switch (index)
            {
                case (0):
                    {
                        btnItem1.LoadItem(item);
                        break;
                    }
                case (1):
                    {
                        btnItem2.LoadItem(item);
                        break;
                    }
                case (2):
                    {
                        btnItem3.LoadItem(item);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        private void LoadQuickSpell(int i, RPGSpell spell)
        {
            switch (i)
            {
                case (0):
                    {
                        btnSpell1.loadSpell(spell);
                        break;
                    }
                case (1):
                    {
                        btnSpell2.loadSpell(spell);
                        break;
                    }
                case (2):
                    {
                        btnSpell3.loadSpell(spell);
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
