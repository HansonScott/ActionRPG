using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RPG
{
    public class RPGArmor: RPGItem
    {
        #region Enums
        public enum ArmorType
        {
            // corresponds to body slot
            Head = 0,
            Torso = 2,
            Shield = 4,
            Belt = 7,
            Feet = 8,
        }
        public enum ArmorClass
        {
            SmallHelm = 1,
            FullHelm = 2,

            LeatherArmor = 3,
            ChainArmor = 4,
            PlateArmor = 5,

            Belt = 6,

            LightBoots = 7,
            HeavyBoots = 8,

            RoundShield = 9,
            KiteShield = 10,
            TowerShield = 11,
        }
        #endregion

        #region public Defs
        // default armor bonus
        public static int DEF_HELMET_SMALL = 1;
        public static int DEF_HELMET_FULL = 2;
        public static int DEF_TORSO_LEATHER = 1;
        public static int DEF_TORSO_CHAIN = 3;
        public static int DEF_TORSO_PLATE = 5;
        public static int DEF_BELT = 2;
        public static int DEF_BOOTS_LIGHT = 1;
        public static int DEF_BOOTS_HEAVY = 2;
        public static int DEF_SHIELD_ROUND = 1;
        public static int DEF_SHIELD_KITE = 2;
        public static int DEF_SHIELD_TOWER = 3;

        // default maximum dexterity bonus while wearing
        public static int MAXDEX_HELMET_SMALL = 3;
        public static int MAXDEX_HELMET_FULL = 2;
        public static int MAXDEX_TORSO_LEATHER = 4;
        public static int MAXDEX_TORSO_CHAIN = 3;
        public static int MAXDEX_TORSO_PLATE = 2;
        public static int MAXDEX_BELT = 3;
        public static int MAXDEX_BOOTS_LIGHT = 3;
        public static int MAXDEX_BOOTS_HEAVY = 2;
        public static int MAXDEX_SHIELD_ROUND = 4;
        public static int MAXDEX_SHIELD_KITE = 3;
        public static int MAXDEX_SHIELD_TOWER = 2;

        // defaul max item durability points
        public static int MAXDUR_HELMET_SMALL = 200;
        public static int MAXDUR_HELMET_FULL = 400;
        public static int MAXDUR_TORSO_LEATHER = 300;
        public static int MAXDUR_TORSO_CHAIN = 500;
        public static int MAXDUR_TORSO_PLATE = 800;
        public static int MAXDUR_BELT = 100;
        public static int MAXDUR_BOOTS_LIGHT = 100;
        public static int MAXDUR_BOOTS_HEAVY = 200;
        public static int MAXDUR_SHIELD_ROUND = 400;
        public static int MAXDUR_SHIELD_KITE = 600;
        public static int MAXDUR_SHIELD_TOWER = 1000;

        // default armor values
        public static int VALUE_HELMET_SMALL = 25;
        public static int VALUE_HELMET_FULL = 50;
        public static int VALUE_TORSO_LEATHER = 100;
        public static int VALUE_TORSO_CHAIN = 300;
        public static int VALUE_TORSO_PLATE = 500;
        public static int VALUE_BELT = 50;
        public static int VALUE_BOOTS_LIGHT = 35;
        public static int VALUE_BOOTS_HEAVY = 75;
        public static int VALUE_SHIELD_ROUND = 50;
        public static int VALUE_SHIELD_KITE = 150;
        public static int VALUE_SHIELD_TOWER = 300;
        #endregion

        #region Member Declarations
        private ArmorClass m_class;
        private ArmorType m_type;

        private int m_Defense;
        private int m_MaxDex;
        private int m_DurabilityMax;
        private int m_DurabilityCurrent;
        #endregion

        #region Constructor and Setup
        public RPGArmor(ArmorClass a)
        {
            // set all armor similarities
            this.BaseSpeed = 0;
            this.Actions = null;
            this.Color1 = Color.Gray;
            this.Color2 = Color.Black;
            this.m_class = a;
            this.m_type = GetArmorTypeFromClass(a);
            this.Name = GetNameFromArmorClassEnum(a);
            this.Slot = GetSlotFromArmorClassEnum(a);

            // set piece-specific info
            switch (a)
            {
                case(ArmorClass.Belt):
                    {
                        this.m_Defense = DEF_BELT;
                        this.m_MaxDex = MAXDEX_BELT;
                        this.m_DurabilityMax = MAXDUR_BELT;
                        this.ItemValue = VALUE_BELT;
                        break;
                    }
                case (ArmorClass.SmallHelm):
                    {
                        this.m_Defense = DEF_HELMET_SMALL;
                        this.m_MaxDex = MAXDEX_HELMET_SMALL;
                        this.m_DurabilityMax = MAXDUR_HELMET_SMALL;
                        this.ItemValue = VALUE_HELMET_SMALL;
                        break;
                    }
                case (ArmorClass.FullHelm):
                    {
                        this.m_Defense = DEF_HELMET_FULL;
                        this.m_MaxDex = MAXDEX_HELMET_FULL;
                        this.m_DurabilityMax = MAXDUR_HELMET_FULL;
                        this.ItemValue = VALUE_HELMET_FULL;
                        break;
                    }
                case (ArmorClass.HeavyBoots):
                    {
                        this.m_Defense = DEF_BOOTS_HEAVY;
                        this.m_MaxDex = MAXDEX_BOOTS_HEAVY;
                        this.m_DurabilityMax = MAXDUR_BOOTS_HEAVY;
                        this.ItemValue = VALUE_BOOTS_HEAVY;
                        break;
                    }
                case (ArmorClass.LightBoots):
                    {
                        this.m_Defense = DEF_BOOTS_LIGHT;
                        this.m_MaxDex = MAXDEX_BOOTS_LIGHT;
                        this.m_DurabilityMax = MAXDUR_BOOTS_LIGHT;
                        this.ItemValue = VALUE_BOOTS_LIGHT;
                        break;
                    }
                case (ArmorClass.LeatherArmor):
                    {
                        this.m_Defense = DEF_TORSO_LEATHER;
                        this.m_MaxDex = MAXDEX_TORSO_LEATHER;
                        this.m_DurabilityMax = MAXDUR_TORSO_LEATHER;
                        this.ItemValue = VALUE_TORSO_LEATHER;
                        this.Color1 = Color.Brown;
                        this.Color2 = Color.Tan;
                        break;
                    }
                case (ArmorClass.ChainArmor):
                    {
                        this.m_Defense = DEF_TORSO_CHAIN;
                        this.m_MaxDex = MAXDEX_TORSO_CHAIN;
                        this.m_DurabilityMax = MAXDUR_TORSO_CHAIN;
                        this.ItemValue = VALUE_TORSO_CHAIN;
                        this.Color1 = Color.Gray;
                        this.Color2 = Color.Silver;
                        break;
                    }
                case (ArmorClass.PlateArmor):
                    {
                        this.m_Defense = DEF_TORSO_PLATE;
                        this.m_MaxDex = MAXDEX_TORSO_PLATE;
                        this.m_DurabilityMax = MAXDUR_TORSO_PLATE;
                        this.ItemValue = VALUE_TORSO_PLATE;
                        break;
                    }
                case (ArmorClass.RoundShield):
                    {
                        this.m_Defense = DEF_SHIELD_ROUND;
                        this.m_MaxDex = MAXDEX_SHIELD_ROUND;
                        this.m_DurabilityMax = MAXDUR_SHIELD_ROUND;
                        this.ItemValue = VALUE_SHIELD_ROUND;
                        break;
                    }
                case (ArmorClass.KiteShield):
                    {
                        this.m_Defense = DEF_SHIELD_KITE;
                        this.m_MaxDex = MAXDEX_SHIELD_KITE;
                        this.m_DurabilityMax = MAXDUR_SHIELD_KITE;
                        this.ItemValue = VALUE_SHIELD_KITE;
                        break;
                    }
                case (ArmorClass.TowerShield):
                    {
                        this.m_Defense = DEF_SHIELD_TOWER;
                        this.m_MaxDex = MAXDEX_SHIELD_TOWER;
                        this.m_DurabilityMax = MAXDUR_SHIELD_TOWER;
                        this.ItemValue = VALUE_SHIELD_TOWER;
                        break;
                    }
                default:
                    {
                        break;
                    }
            } // endswitch

            // wrap up
            ResetDurability();
            UpdateDescription();
        }
        public RPGArmor(ArmorClass a, int ArmorDefense, int MaxDexBonus, int MaxDurability, int Value, string Name)
        {
            // set all armor similarities
            this.BaseSpeed = 0;
            this.Actions = null;
            this.Color1 = Color.Gray;
            this.Color2 = Color.Black;
            this.m_class = a;
            this.m_type = GetArmorTypeFromClass(a);
            this.Name = Name;
            this.Slot = GetSlotFromArmorClassEnum(a);

            // set specifics
            this.m_Defense = ArmorDefense;
            this.m_MaxDex = MaxDexBonus;
            this.m_DurabilityMax = MaxDurability;
            this.ItemValue = Value;

            ResetDurability();
            UpdateDescription();
        }
        public override void UpdateDescription()
        {
            this.m_desc = "" + Defense + " AC, " + "(" + this.Durability + " / " + this.DurabilityMax + ")";
        }
        #endregion

        #region Property methods
        public ArmorClass Class
        {
            get { return m_class; }
        }
        public ArmorType Type
        {
            get { return m_type; }
        }

        public int Defense
        {
            get { return m_Defense; }
            set { m_Defense = value; }
        }
        public int MaxDeterityBonus
        {
            get { return m_MaxDex; }
            set { m_MaxDex = value; }
        }
        public int Durability
        {
            get { return m_DurabilityCurrent; }
            set { m_DurabilityCurrent = value; }
        }
        public int DurabilityMax
        {
            get { return m_DurabilityMax; }
            set { m_DurabilityMax = value; }
        }
        #endregion

        #region Static Methods
        public static RPGArmor CreateRandomArmorPiece()
        {
            int c = new RPGCalc().Roll(Enum.GetValues(typeof(ArmorClass)).Length);
            ArmorClass ac = (ArmorClass)Enum.Parse(typeof(ArmorClass), c.ToString());
            return new RPGArmor(ac);
        }
        public static RPGArmor CreateRandomTorsoArmor()
        {
            int c = new RPGCalc().Roll(3) + 2;
            ArmorClass ac = (ArmorClass)Enum.Parse(typeof(ArmorClass), c.ToString());
            return new RPGArmor(ac);
        }
        public static RPGArmor CreateRandomShield()
        {
            int c = new RPGCalc().Roll(3) + 8;
            ArmorClass ac = (ArmorClass)Enum.Parse(typeof(ArmorClass), c.ToString());
            return new RPGArmor(ac);
        }
        #endregion

        #region Public Methods
        public ArmorType GetArmorTypeFromClass(ArmorClass c)
        {
            switch (c)
            {
                case (ArmorClass.Belt):
                    {
                        return ArmorType.Belt;
                        //break;
                    }
                case (ArmorClass.LeatherArmor):
                case (ArmorClass.ChainArmor):
                case (ArmorClass.PlateArmor):
                    {
                        return ArmorType.Torso;
                        //break;
                    }
                case (ArmorClass.SmallHelm):
                case (ArmorClass.FullHelm):
                    {
                        return ArmorType.Head;
                        //break;
                    }
                case (ArmorClass.LightBoots):
                case (ArmorClass.HeavyBoots):
                    {
                        return ArmorType.Feet;
                        //break;
                    }
                case (ArmorClass.RoundShield):
                case (ArmorClass.KiteShield):
                case (ArmorClass.TowerShield):
                    {
                        return ArmorType.Shield;
                        //break;
                    }
                default:
                    {
                        return ArmorType.Torso;
                        //break;
                    }
            }
        }
        public void ResetDurability()
        {
            m_DurabilityCurrent = m_DurabilityMax;
        }
        public static string GetNameFromArmorClassEnum(ArmorClass ac)
        {
            string name = Enum.GetName(typeof(ArmorClass), ac);

            // often the name has two words, with midCaps involved.

            for (int i = 1; i < name.Length; i++) // start at 1 because it always starts with a cap
            {
                // if caps
                if (name[i].ToString() != name[i].ToString().ToLower())
                {
                    // then insert a space before this index.
                    name = name.Insert(i, " ");
                    break;
                }
            }
            return name;
        }
        public Inventory.BodySlot GetSlotFromArmorClassEnum(ArmorClass ac)
        {
            // since we specifically wrote the armor type values to match the slots, we're set.
            return (Inventory.BodySlot)Enum.Parse(typeof(Inventory.BodySlot), ((int)GetArmorTypeFromClass(ac)).ToString());
        }
        #endregion
    }
}
