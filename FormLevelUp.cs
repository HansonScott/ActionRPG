using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RPG
{
    public partial class FormLevelUp : Form
    {
        public FormLevelUp(Actor a)
        {
            InitializeComponent();
            LoadData(a);
        }
        private void LoadData(Actor a)
        {
            // declar vars
            int oldLvl = a.Level;
            int oldHP = a.HPBaseMax;
            int oldMP = a.MPBaseMax;
            int oldAtt = a.CurrentAttack;
            int oldDef = a.CurrentDefense;

            RPGCalc calc = new RPGCalc();

            calc.LevelUpActor(a);

            int newLvl = a.Level;
            int newHP = a.HPBaseMax;
            int newMP = a.MPBaseMax;
            int newAtt = a.CurrentAttack;
            int newDef = a.CurrentDefense;
            
            // display change in vars
            Add("Level: " + '\t' + oldLvl.ToString() + '\t' + " -> " + '\t' + newLvl);
            Add("HP: " + '\t' + oldHP.ToString() + '\t' + " -> " + '\t' + newHP);
            Add("MP: " + '\t' + oldMP.ToString() + '\t' + " -> " + '\t' + newMP);
            Add("Att: " + '\t' + oldAtt.ToString() + '\t' + " -> " + '\t' + newAtt);
            Add("Def: " + '\t' + oldDef.ToString() + '\t' + " -> " + '\t' + newDef);
        }
        private void Add(string line)
        {
            TB_Description.Text += (line + Environment.NewLine);
        }
        private void btn_Done_Click(object sender, EventArgs e)
        {
            // reset actor's stats for next level
            this.Close();
        }
    }
}