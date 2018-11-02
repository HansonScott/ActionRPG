using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace RPG
{
    public class ActionButton: Panel
    {
        #region Enums
        public enum ActionButtonType
        {
            BasicAction,
            SkillAction,
            SpellAction,
            ItemAction
        }
        #endregion

        #region Public Declarations
        public static int BOARDER_WIDTH = 5;
        public static Color SELECT_COLOR = Color.Yellow;
        public static Color NONSELECT_COLOR = Color.Gray;
        public static Color BACK_COLOR = Color.LightGray;
        public static Color TEXT_COLOR = Color.Black;

        public Point TextInset = new Point(8, 6);
        #endregion

        #region Member Declarations
        private bool m_IsSelected;
        private bool m_IsExclusive;
        private Pen PBoarder;
        private Color CSelect;
        private Brush BBack;
        private Brush BText;
        Graphics ThisGraphics;
        Graphics BackGraphics;
        Image BackGraphicsImage;
        private Font m_Font;
        private ActionButtonType m_ActionType;
        private RPGItem m_item;
        private RPGSpell m_Spell;
        private string m_ButtonText;
        #endregion

        #region Properties
        public bool IsSelected
        {
            get { return m_IsSelected; }
            set 
            {
                if (m_IsSelected != value)
                {
                    m_IsSelected = value;
                    ResetColors();
                    Refresh();
                }
            }
        }
        public bool IsExclusive
        {
            get { return m_IsExclusive; }
        }
        public ActionButtonType ActionType
        {
            get { return m_ActionType; }
        }
        public RPGItem Item
        {
            get { return m_item; }
        }
        public RPGSpell Spell
        {
            get { return m_Spell; }
        }
        public string ButtonText
        {
            get { return m_ButtonText; }
            set { m_ButtonText = value; }
        }
        #endregion

        #region Constructor
        public ActionButton(ActionButtonType type)
        {
            //this.DoubleBuffered = true;
            m_IsSelected = false;
            m_IsExclusive = true;

            CSelect = NONSELECT_COLOR;
            PBoarder = new Pen(new SolidBrush(CSelect));
            PBoarder.Width = BOARDER_WIDTH;
            BBack = new SolidBrush(BACK_COLOR);
            BText = new SolidBrush(TEXT_COLOR);
            BackGraphicsImage = new Bitmap(this.Width, this.Height);
            BackGraphics = Graphics.FromImage(BackGraphicsImage);
            ThisGraphics = this.CreateGraphics();
            m_Font = new Font(this.Font.Name, 8f);
            m_ActionType = type;
        }
        #endregion

        #region Events
        public void ActionButton_OnClick()
        {
            if (this.ActionType == ActionButtonType.ItemAction && this.Item == null)
            {
                // not select the button.
                return;
            }
            else                
            {
                IsSelected = !IsSelected;
            }
        }
        #endregion

        #region Public methods
        public void LoadItem(RPGItem item)
        {
            if (item != null)
            {
                this.m_item = item;
                if (item.Name != null
                    && item.Name.Length > 0)
                {
                    ButtonText = item.Name;
                }
                else
                {
                    ButtonText = item.ToString();
                }

                this.Refresh();
            }
            else
            {
                ClearItem();
            }
        }
        public void ClearItem()
        {
            if (m_item != null)
            {
                m_item = null;
            }
            ButtonText = "-";
            IsSelected = false;

            this.Refresh();
        }
        public void loadSpell(RPGSpell spell)
        {
            if (spell != null)
            {
                ButtonText = spell.GetShortEffectDescription();
                this.BText = new SolidBrush(spell.SpellColor);
                this.Refresh();
            }
            else
            {
                ClearSpell();
            }
        }
        public void ClearSpell()
        {
            if (m_Spell != null)
            {
                m_Spell = null;
            }
            ButtonText = "-";
            this.BText = new SolidBrush(TEXT_COLOR);
            IsSelected = false;

            this.Refresh();
        }
        #endregion

        #region Overridden methods
        protected override void OnPaint(PaintEventArgs e)
        {
            // draw boarder (selection)
            BackGraphics.DrawRectangle(PBoarder, 1, 1, this.Width - BOARDER_WIDTH, this.Height - BOARDER_WIDTH);

            // draw center
            BackGraphics.FillRectangle(BBack, 2, 2, this.Width - BOARDER_WIDTH, this.Height - BOARDER_WIDTH);

            // draw text
            BackGraphics.DrawString(ButtonText, this.Font, BText, TextInset);

            // transfer back to front.
            ThisGraphics.DrawImage(BackGraphicsImage, 0, 0);

            base.OnPaint(e);
        }
        #endregion

        #region Private methods
        private void ResetColors()
        {
            if (this.IsSelected)
            {
                CSelect = SELECT_COLOR;
                PBoarder.Color = SELECT_COLOR;
            }
            else
            {
                CSelect = NONSELECT_COLOR;
                PBoarder.Color = NONSELECT_COLOR;
            }
            this.Refresh();
        }
        #endregion      
    }
}
