using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RPG
{
    public partial class FormItemDetails : Form
    {
        private RPGObject thisItem;
        public string NL = Environment.NewLine;

        public FormItemDetails(RPGObject item)
        {
            thisItem = item;
            InitializeComponent();

            this.lbl_Name.Text = item.Name;
            this.tb_Details.Text = CreateDetailText();
        }
        private string CreateDetailText()
        {
            string result = "";
            if (thisItem.isOfType(typeof(RPGWeapon)))
            {
                result += "Type: " + Enum.GetName(typeof(RPGWeapon.WeaponType), (thisItem as RPGWeapon).weaponType) + NL;
                result += "Type: " + Enum.GetName(typeof(RPGWeapon.WeaponClass), (thisItem as RPGWeapon).weaponClass) + NL;
                result += "Dmg: " + (thisItem as RPGWeapon).minDmg + " - " + (thisItem as RPGWeapon).maxDmg + NL;
                result += "Range (px): " + (thisItem as RPGWeapon).Range + NL;
                result += "Speed (ms delay): " + (thisItem as RPGWeapon).CoolDown + NL;
                if ((thisItem as RPGWeapon).is2Handed)
                {
                    result += "2 Handed" + NL;
                }

                if ((thisItem as RPGWeapon).StackQuantity > 1)
                {
                    result += "Quantity: " + (thisItem as RPGWeapon).StackQuantity + NL;
                }
                if ((thisItem as RPGWeapon).Effects == null ||
                    (thisItem as RPGWeapon).Effects[0] == null)
                {
                    result += "Effects: None" + NL;
                }
                else
                {

                    result += "Effects:" + NL;
                    for (int i = 0; i < thisItem.Effects.Length; i++)
                    {
                        if (thisItem.Effects[i] != null)
                        {
                            result += "* " + thisItem.Effects[i].GetDescriptionSimple() + NL;
                        }
                    }
                }
            } // end RPGWeapon

            else if (thisItem.isOfType(typeof(RPGArmor)))
            {
                result += "Type: " + Enum.GetName(typeof(RPGArmor.ArmorClass), (thisItem as RPGArmor).Class) + NL;
                result += "Defense: " + (thisItem as RPGArmor).Defense + " AC" + NL;
                result += "Max Dex Bonus: " + (thisItem as RPGArmor).MaxDeterityBonus + NL;
                result += "Durability: " + (thisItem as RPGArmor).Durability + " / "
                                        + (thisItem as RPGArmor).DurabilityMax + NL;

                if ((thisItem as RPGArmor).Effects == null ||
                    (thisItem as RPGArmor).Effects[0] == null)
                {
                    result += "Effects: None" + NL;
                }
                else
                {

                    result += "Effects:" + NL;
                    for (int i = 0; i < thisItem.Effects.Length; i++)
                    {
                        if (thisItem.Effects[i] != null)
                        {
                            result += "* " + thisItem.Effects[i].GetDescriptionSimple() + NL;
                        }
                    }
                }
            }
            else if (thisItem.isOfType(typeof(RPGPotion)))
            {
                foreach(RPGEffect effect in thisItem.Effects)
                {
                    if(effect != null)
                    {
                        result += effect.GetDescriptionFull() + NL;
                    }
                }
            }
            return result;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}