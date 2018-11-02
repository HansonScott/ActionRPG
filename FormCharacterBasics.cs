using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace RPG
{
    public partial class FormCharacterBasics : Form
    {
        private Game.ExitCommand cmd;

        public Game.ExitCommand ExitCommand
        {
            get { return cmd; }
        }

        private Actor m_thisActor;

        public FormCharacterBasics():this(new Actor()){}
        public FormCharacterBasics(Actor actor)
        {
            InitializeComponent();
            ComboBoxUtil.PopulateComboBoxWithEnum(cbAlignment, typeof(Actor.Alignment));
            ComboBoxUtil.PopulateComboBoxWithEnum(cbGender, typeof(Actor.Gender));

            ThisActor = actor;
            LoadActor();
        }

        public Actor ThisActor
        {
            get { return m_thisActor; }
            set { m_thisActor = value; }
        }

        private void btn_Done_Click(object sender, EventArgs e)
        {
            // set values to new character

            // check name
            if (tbName.Text.Length < 1)
            {
                MessageBox.Show("Please type in your character's name.");
                return;
            }
            else
            {
                ThisActor.Name = tbName.Text;
            }

            if (cbGender.SelectedText == null)
            {
                MessageBox.Show("Please select your character's gender.");
                return;
            }
            else
            {
                ThisActor.GenderAsString = cbGender.SelectedText;
            }

            if (cbAlignment.SelectedText == null)
            {
                MessageBox.Show("Please select your character's alignment.");
                return;
            }
            else
            {
                ThisActor.SetAlignment(
                    Int32.Parse(((CBItem)(cbAlignment.SelectedItem)).ID));
            }
            cmd = Game.ExitCommand.Done;
            this.Close();
        }

        private void LoadActor()
        {
            // name
            tbName.Text = m_thisActor.Name;

            // Gender
            for (int i = 0; i < cbGender.Items.Count; i++)
            {
                if (m_thisActor.GenderAsString ==
                    (cbGender.Items[i] as CBItem).Name)
                {
                    cbGender.SelectedIndex = i;
                }
            }

            // alignment
            for (int i = 0; i < cbAlignment.Items.Count; i++)
            {
                if (m_thisActor.AlignmentAsString ==
                    (cbAlignment.Items[i] as CBItem).Name)
                {
                    cbAlignment.SelectedIndex = i;
                }
            }

            // colors.
            this.btnColor1.BackColor = m_thisActor.Color1;
            this.btnColor2.BackColor = m_thisActor.Color2;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmd = Game.ExitCommand.Cancel;
            this.Close();
        }

        private void btnColor1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            DialogResult dr = cd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                m_thisActor.Color1 = cd.Color;
                btnColor1.BackColor = cd.Color;
            }
        }

        private void btnColor2_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            DialogResult dr = cd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                m_thisActor.Color2 = cd.Color;
                btnColor2.BackColor = cd.Color;
            }
        }
    }
}