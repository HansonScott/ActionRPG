using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace RPG
{
    public class TabPageCharacterInfo: TabPage
    {
        #region Declarations
        private Actor thisActor;
        private Label lblName;
        private Label lblAlignment;
        private Label lblGender;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Attribute;
        private DataGridViewTextBoxColumn Base;
        private DataGridViewTextBoxColumn Current;
        private DataGridViewTextBoxColumn Bonus;
        private DataTable dtAttributes;

        private Label lblAttack;
        private Label lblAttackValue;
        private Label lblDefense;
        private Label lblDefenseValue;

        private Label lblHP;
        private Label lblHPValue;
        private Label lblMP;
        private Label lblMPValue;

        private Label lblLevel;
        private Label lblLevelValue;

        private Label lblExp;
        private Label lblExpValue;

        private Button btnLevelUp;
        #endregion

        #region Constructor and Setup
        public TabPageCharacterInfo()
        {
            // basic visuals for this TabPage set in session designer (container)

            #region dataGrid and columns
            // 
            // Attribute
            // 
            this.Attribute = new DataGridViewTextBoxColumn();
            this.Attribute.DataPropertyName = "Attribute";
            this.Attribute.HeaderText = "Attribute";
            this.Attribute.Name = "Attribute";
            this.Attribute.ReadOnly = true;
            this.Attribute.Width = 125;
            // 
            // Base
            // 
            this.Base = new DataGridViewTextBoxColumn();
            this.Base.DataPropertyName = "Base";
            this.Base.HeaderText = " Base";
            this.Base.Name = "Base";
            this.Base.ReadOnly = true;
            this.Base.Width = 75;
            // 
            // Current
            // 
            this.Current = new DataGridViewTextBoxColumn();
            this.Current.DataPropertyName = "Current";
            this.Current.HeaderText = "Current";
            this.Current.Name = "Current";
            this.Current.ReadOnly = true;
            this.Current.Width = 75;
            // 
            // Bonus
            // 
            this.Bonus = new DataGridViewTextBoxColumn();
            this.Bonus.DataPropertyName = "Bonus";
            this.Bonus.HeaderText = "Bonus";
            this.Bonus.Name = "Bonus";
            this.Bonus.ReadOnly = true;
            this.Bonus.Width = 75;

            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Attribute,
            this.Base,
            this.Current,
            this.Bonus});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.Location = new System.Drawing.Point(340, 190);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.RowHeadersWidth = 4;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(327, 175);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.Controls.Add(this.dataGridView1);
            #endregion

            #region Labels
            this.lblName = new Label();
            SetupLabelDefaults(lblName, 256, 48, 494, 20, "Name");
            this.lblName.Font
                = new System.Drawing.Font("Microsoft Sans Serif",
                                        12F,
                                        System.Drawing.FontStyle.Bold,
                                        System.Drawing.GraphicsUnit.Point,
                                        ((byte)(0)));

            this.lblAlignment = new Label();
            SetupLabelDefaults(lblAlignment, 370, 90, 115, 23, "Alignment");

            this.lblGender = new Label();
            SetupLabelDefaults(lblGender, 528, 90, 125, 23, "Gender");

            this.lblLevel = new Label();
            SetupLabelDefaults(lblLevel, 375, 120, 80, 23, "Level");

            this.lblLevelValue = new Label();
            SetupLabelDefaults(lblLevelValue, 375, 150, 80, 23, "-");

            this.lblExp = new Label();
            SetupLabelDefaults(lblExp, 550, 120, 80, 23, "Exp.");

            this.lblExpValue = new Label();
            SetupLabelDefaults(lblExpValue, 550, 150, 80, 23, "- / -");

            this.lblHP = new Label();
            SetupLabelDefaults(lblHP, 75, 90, 80, 23, "HP");
            lblHP.ForeColor = Color.Red;

            this.lblHPValue = new Label();
            SetupLabelDefaults(lblHPValue, 75, 120, 80, 23, " - / - ");
            lblHPValue.ForeColor = Color.Red;

            this.lblMP = new Label();
            SetupLabelDefaults(lblMP, 175, 90, 80, 23, "MP");
            lblMP.ForeColor = Color.Blue;

            this.lblMPValue = new Label();
            SetupLabelDefaults(lblMPValue, 175, 120, 80, 23, " - / - ");
            lblMPValue.ForeColor = Color.Blue;

            this.lblAttack = new Label();
            SetupLabelDefaults(lblAttack, 75, 170, 80, 23, "Attack");

            this.lblAttackValue = new Label();
            SetupLabelDefaults(lblAttackValue, 75, 200, 80, 23, "-");

            this.lblDefense = new Label();
            SetupLabelDefaults(lblDefense, 175, 170, 80, 23, "Defense");
            
            this.lblDefenseValue = new Label();
            SetupLabelDefaults(lblDefenseValue, 175, 200, 80, 23, "-");

        #endregion

            #region Buttons
            btnLevelUp = new Button();
            btnLevelUp.Text = "Level Up!";
            btnLevelUp.Location = new Point(750, 100);
            btnLevelUp.Click += new EventHandler(btnLevelUp_Click);
            this.Controls.Add(btnLevelUp);
            #endregion
        }

        private void SetupLabelDefaults(Label l, int posX, int posY, int Width, int Height, string text)
        {
            l.Font = new System.Drawing.Font("Microsoft Sans Serif", 
                                            10F, 
                                            FontStyle.Regular, 
                                            GraphicsUnit.Point, 
                                            ((byte)(0)));
            l.Location = new System.Drawing.Point(posX, posY);
            l.Size = new System.Drawing.Size(Width, Height);
            l.Text = text;
            l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Controls.Add(l);
        }

        public void LoadActor(Actor a)
        {
            this.thisActor = a;
            lblName.Text = a.Name;
            lblGender.Text = a.GenderAsString;
            lblAlignment.Text = a.AlignmentAsString;
            LoadDataTable(a);

            lblHPValue.Text = "" + a.HPCurrent + " / " + a.HPCurrentMax;
            lblMPValue.Text = "" + a.MPCurrent + " / " + a.MPCurrentMax;

            lblAttackValue.Text = new RPGCalc().GetActorCurrentAttack(a).ToString();
            lblDefenseValue.Text = new RPGCalc().GetActorCurrentDefense(a).ToString();

            lblLevelValue.Text = "" + a.Level;
            lblExpValue.Text = a.Experience + " / " + new RPGCalc().GetXPForNextLevel(a.Level);

            if (a.ReadyToLevelUp)
            {
                btnLevelUp.Enabled = true;
                btnLevelUp.ForeColor = Color.Black;
            }
            else
            {
                btnLevelUp.Enabled = false;
                btnLevelUp.ForeColor = Color.Gray;
            }
        }
        private void LoadDataTable(Actor a)
        {
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();

            dtAttributes = new DataTable();
            dtAttributes.Columns.Add("Attribute");
            dtAttributes.Columns.Add("Base");
            dtAttributes.Columns.Add("Current");
            dtAttributes.Columns.Add("Bonus");

            DataRow drStr = dtAttributes.NewRow();
            drStr[0] = "Strength";
            drStr[1] = a.BaseStrength;
            drStr[2] = a.CurrentStrength;
            drStr[3] = new RPGCalc().GetBonusFromAttribute(a.CurrentStrength);
            dtAttributes.Rows.Add(drStr);

            DataRow drDex = dtAttributes.NewRow();
            drDex[0] = "Dexterity";
            drDex[1] = a.BaseDexterity;
            drDex[2] = a.CurrentDexterity;
            drDex[3] = new RPGCalc().GetBonusFromAttribute(a.CurrentDexterity);
            dtAttributes.Rows.Add(drDex);

            DataRow drCon = dtAttributes.NewRow();
            drCon[0] = "Constitution";
            drCon[1] = a.BaseConstitution;
            drCon[2] = a.CurrentConstitution;
            drCon[3] = new RPGCalc().GetBonusFromAttribute(a.CurrentConstitution);
            dtAttributes.Rows.Add(drCon);

            DataRow drInt = dtAttributes.NewRow();
            drInt[0] = "Intelligence";
            drInt[1] = a.BaseIntelligence;
            drInt[2] = a.CurrentIntelligence;
            drInt[3] = new RPGCalc().GetBonusFromAttribute(a.CurrentIntelligence);
            dtAttributes.Rows.Add(drInt);

            DataRow drWis = dtAttributes.NewRow();
            drWis[0] = "Wisdom";
            drWis[1] = a.BaseWisdom;
            drWis[2] = a.CurrentWisdom;
            drWis[3] = new RPGCalc().GetBonusFromAttribute(a.CurrentWisdom);
            dtAttributes.Rows.Add(drWis);

            DataRow drCha = dtAttributes.NewRow();
            drCha[0] = "Charisma";
            drCha[1] = a.BaseCharisma;
            drCha[2] = a.CurrentCharisma;
            drCha[3] = new RPGCalc().GetBonusFromAttribute(a.CurrentCharisma);
            dtAttributes.Rows.Add(drCha);

            dataGridView1.DataSource = dtAttributes;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // format current cell compared to base
                int b = Int32.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                int c = Int32.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                if (c > b)
                {
                    dataGridView1.Rows[i].Cells[2].Style.ForeColor = Color.Green;
                }
                else if (c < b)
                {
                    dataGridView1.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                }
                else
                {
                    dataGridView1.Rows[i].Cells[2].Style.ForeColor = Color.Black;
                }

                // format bonus
                int s = Int32.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                if (s > 0)
                {
                    dataGridView1.Rows[i].Cells[3].Style.ForeColor = Color.Green;
                }
                else if (s < 0)
                {
                    dataGridView1.Rows[i].Cells[3].Style.ForeColor = Color.Red;
                }
                else
                {
                    dataGridView1.Rows[i].Cells[3].Style.ForeColor = Color.Black;
                }
            }
        }
        #endregion

        #region Events
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // if cell is in column Attribute, pop attribute description
            MessageBox.Show("This feature not implemented yet.");
        }
        void btnLevelUp_Click(object sender, EventArgs e)
        {
            // pop new stat change window
            FormLevelUp form1 = new FormLevelUp(this.thisActor);
            form1.ShowDialog();
            btnLevelUp.Enabled = false;
            this.LoadActor(this.thisActor);
        }
        #endregion

        #region Private methods
        #endregion

    }
}
