using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RPG
{
    public partial class FormAttribute : Form
    {
        #region Public Declarations
        public const int DEFAULT_BUY_POINTS = 30;
        public const int MAX_ATTRIBUTE = 18;
        public const int MIN_ATTRIBUTE = 3;
        #endregion

        #region Private Declarations
        private Actor m_thisActor;
        private int m_currentPointsLeft;
        private Game.ExitCommand cmd;

        #endregion

        #region Constructor and Setup
        public FormAttribute():this(new Actor()){}
        public FormAttribute(Actor actor)
        {
            InitializeComponent();

            m_thisActor = actor;
            LoadActor();

            m_currentPointsLeft = DEFAULT_BUY_POINTS;
            SetTotalBuyPointsLeft();
        }
        private void LoadActor()
        {
            // load actor's attributes into form controls
            lblValStr.Text = m_thisActor.BaseStrength.ToString();
            lblValDex.Text = m_thisActor.BaseDexterity.ToString();
            lblValCon.Text = m_thisActor.BaseConstitution.ToString();
            lblValInt.Text = m_thisActor.BaseIntelligence.ToString();
            lblValWis.Text = m_thisActor.BaseWisdom.ToString();
            lblValCha.Text = m_thisActor.BaseCharisma.ToString();

            SetBonus(lblBonus1, m_thisActor.BaseStrength);
            SetBonus(lblBonus2, m_thisActor.BaseDexterity);
            SetBonus(lblBonus3, m_thisActor.BaseConstitution);
            SetBonus(lblBonus4, m_thisActor.BaseIntelligence);
            SetBonus(lblBonus5, m_thisActor.BaseWisdom);
            SetBonus(lblBonus6, m_thisActor.BaseCharisma);
        }
        private void SetBonus(Label lbl, int att)
        {
            int b = new RPGCalc().GetBonusFromAttribute(att);
            lbl.Text = b.ToString();
            if (b > 0)
            {
                lbl.ForeColor = Color.Green;
            }
            else if (b < 0)
            {
                lbl.ForeColor = Color.Red;
            }
        }
        private void SetTotalBuyPointsLeft()
        {
            lblPointTotal.Text = "Total Points Left: " + m_currentPointsLeft;
        }
        #endregion

        #region Property Methods
        public Actor ThisActor
        {
            get { return m_thisActor; }
            set { m_thisActor = value; }
        }
        public Game.ExitCommand ExitCommand
        {
            get { return cmd; }
        }

        #endregion

        #region Attribute Description Events
        private void lblStatStr_Click(object sender, EventArgs e)
        {

        }
        private void lblStatDex_Click(object sender, EventArgs e)
        {

        }
        private void lblStatCon_Click(object sender, EventArgs e)
        {

        }
        private void lblStatInt_Click(object sender, EventArgs e)
        {

        }
        private void lblStatWis_Click(object sender, EventArgs e)
        {

        }
        private void lblStatCha_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region Change Attribute Value Events
        private void btnUp1_Click(object sender, EventArgs e)
        {
            // determine cost based on current value
            int cost = GetCostForCurrentValue(Int32.Parse(lblValStr.Text));

            // check for points available compared to cost
            // and value can be changed
            if (m_currentPointsLeft >= cost
                && CanAddPoint(ThisActor.BaseStrength))
            {
                // subtract points from total left
                m_currentPointsLeft -= cost;

                // add point to character
                ThisActor.BaseStrength++;
                ThisActor.CurrentStrength++;

                // set label to character attribute
                RefreshData();
            }
        }
        private void btnUp2_Click(object sender, EventArgs e)
        {
            // determine cost based on current value
            int cost = GetCostForCurrentValue(Int32.Parse(lblValDex.Text));

            // check for points available compared to cost
            // and value can be changed
            if (m_currentPointsLeft >= cost
                && CanAddPoint(ThisActor.BaseDexterity))
            {
                // subtract points from total left
                m_currentPointsLeft -= cost;

                // add point to character
                ThisActor.BaseDexterity++;
                ThisActor.CurrentDexterity++;

                // set label to character attribute
                RefreshData();
            }
        }
        private void btnUp3_Click(object sender, EventArgs e)
        {
            // determine cost based on current value
            int cost = GetCostForCurrentValue(Int32.Parse(lblValCon.Text));

            // check for points available compared to cost
            // and value can be changed
            if (m_currentPointsLeft >= cost
                && CanAddPoint(ThisActor.BaseConstitution))
            {
                // subtract points from total left
                m_currentPointsLeft -= cost;

                // add point to character
                ThisActor.BaseConstitution++;
                ThisActor.CurrentConstitution++;

                // set label to character attribute
                RefreshData();
            }
        }
        private void btnUp4_Click(object sender, EventArgs e)
        {
            // determine cost based on current value
            int cost = GetCostForCurrentValue(Int32.Parse(lblValInt.Text));

            // check for points available compared to cost
            // and value can be changed
            if (m_currentPointsLeft >= cost
                && CanAddPoint(ThisActor.BaseIntelligence))
            {
                // subtract points from total left
                m_currentPointsLeft -= cost;

                // add point to character
                ThisActor.BaseIntelligence++;
                ThisActor.CurrentIntelligence++;

                // set label to character attribute
                RefreshData();
            }
        }
        private void btnUp5_Click(object sender, EventArgs e)
        {
            // determine cost based on current value
            int cost = GetCostForCurrentValue(Int32.Parse(lblValWis.Text));

            // check for points available compared to cost
            // and value can be changed
            if (m_currentPointsLeft >= cost
                && CanAddPoint(ThisActor.BaseWisdom))
            {
                // subtract points from total left
                m_currentPointsLeft -= cost;

                // add point to character
                ThisActor.BaseWisdom++;
                ThisActor.CurrentWisdom++;

                // set label to character attribute
                RefreshData();
            }
        }
        private void btnUp6_Click(object sender, EventArgs e)
        {
            // determine cost based on current value
            int cost = GetCostForCurrentValue(Int32.Parse(lblValCha.Text));

            // check for points available compared to cost
            // and value can be changed
            if (m_currentPointsLeft >= cost
                && CanAddPoint(ThisActor.BaseCharisma))
            {
                // subtract points from total left
                m_currentPointsLeft -= cost;

                // add point to character
                ThisActor.BaseCharisma++;
                ThisActor.CurrentCharisma++;

                // set label to character attribute
                RefreshData();
            }
        }
        
        private void btnDn1_Click(object sender, EventArgs e)
        {
            // determine cost based on current value
            int cost = GetCostForCurrentValue(Int32.Parse(lblValStr.Text));

            // check that value can be changed
            if (CanSubtractPoint(ThisActor.BaseStrength))
            {
                // subtract points from total left
                m_currentPointsLeft += cost;

                // add point to character
                ThisActor.BaseStrength--;
                ThisActor.CurrentStrength--;

                // set label to character attribute
                RefreshData();
            }
        }
        private void btnDn2_Click(object sender, EventArgs e)
        {
            // determine cost based on current value
            int cost = GetCostForCurrentValue(Int32.Parse(lblValDex.Text));

            // check that value can be changed
            if (CanSubtractPoint(ThisActor.BaseDexterity))
            {
                // subtract points from total left
                m_currentPointsLeft += cost;

                // add point to character
                ThisActor.BaseDexterity--;
                ThisActor.CurrentDexterity--;

                // set label to character attribute
                RefreshData();
            }
        }
        private void btnDn3_Click(object sender, EventArgs e)
        {
            // determine cost based on current value
            int cost = GetCostForCurrentValue(Int32.Parse(lblValCon.Text));

            // check that value can be changed
            if (CanSubtractPoint(ThisActor.BaseConstitution))
            {
                // subtract points from total left
                m_currentPointsLeft += cost;

                // add point to character
                ThisActor.BaseConstitution--;
                ThisActor.CurrentConstitution--;

                // set label to character attribute
                RefreshData();
            }
        }
        private void btnDn4_Click(object sender, EventArgs e)
        {
            // determine cost based on current value
            int cost = GetCostForCurrentValue(Int32.Parse(lblValInt.Text));

            // check that value can be changed
            if (CanSubtractPoint(ThisActor.BaseIntelligence))
            {
                // subtract points from total left
                m_currentPointsLeft += cost;

                // add point to character
                ThisActor.BaseIntelligence--;
                ThisActor.CurrentIntelligence--;

                // set label to character attribute
                RefreshData();
            }
        }
        private void btnDn5_Click(object sender, EventArgs e)
        {
            // determine cost based on current value
            int cost = GetCostForCurrentValue(Int32.Parse(lblValWis.Text));

            // check that value can be changed
            if (CanSubtractPoint(ThisActor.BaseWisdom))
            {
                // subtract points from total left
                m_currentPointsLeft += cost;

                // add point to character
                ThisActor.BaseWisdom--;
                ThisActor.CurrentWisdom--;

                // set label to character attribute
                RefreshData();
            }
        }
        private void btnDn6_Click(object sender, EventArgs e)
        {
            // determine cost based on current value
            int cost = GetCostForCurrentValue(Int32.Parse(lblValCha.Text));

            // check that value can be changed
            if (CanSubtractPoint(ThisActor.BaseCharisma))
            {
                // subtract points from total left
                m_currentPointsLeft += cost;

                // add point to character
                ThisActor.BaseCharisma--;
                ThisActor.CurrentCharisma--;

                // set label to character attribute
                RefreshData();
            }
        }
        #endregion
        #region Form Events
        private void btn_Done_Click(object sender, EventArgs e)
        {
            if (m_currentPointsLeft > 0)
            {
                DialogResult dr = MessageBox.Show("You have points left to spend, are you sure you are done?", "", MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes)
                {
                    return;
                }
            }

            RPGCalc calc = new RPGCalc();
            ThisActor.HPBaseMax = calc.GetBaseHP(ThisActor);
            ThisActor.MPBaseMax = calc.GetBaseMP(ThisActor);

            this.ThisActor.ResetStats();

            cmd = Game.ExitCommand.Done;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmd = Game.ExitCommand.Cancel;
            this.Close();
        }
        #endregion

        #region Private Methods
        private int GetCostForCurrentValue(int val)
        {
            switch (val)
            {
                case (1):
                case (2):
                case (18):
                    {
                        return 5;
                    }

                case (3):
                case (4):
                case (16):
                case (17):
                    {
                        return 4;
                    }

                case (5):
                case (6):
                case (14):
                case (15):
                    {
                        return 3;
                    }

                case (7):
                case (8):
                case (12):
                case (13):
                    {
                        return 2;
                    }

                case (9):
                case (10):
                case (11):
                default:
                    {
                        return 1;
                    }
            }
        }
        private bool CanAddPoint(int att)
        {
            return (att < MAX_ATTRIBUTE); 
        }
        private bool CanSubtractPoint(int att)
        {
            return (att > MIN_ATTRIBUTE);
        }
        private void RefreshData()
        {
            LoadActor();
            SetTotalBuyPointsLeft();
        }
        #endregion

    }
}