using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RPG
{
    public class RPGWeapon: RPGItem
    {
        public enum WeaponType
        {
            Melee = 1,
            Thrown = 2,
            Launcher = 3
        }
        public enum WeaponClass
        {
            Sword = 1,
            Axe = 2,
            Mace = 3,
            Staff = 4,

            ThrowingAxe = 5,
            Dart = 6,

            Bow = 7,
            Crossbow = 8,
            Sling = 9,
        }

        #region Weapon Defs
        public static int RANGE_SWORD = 50;
        public static int RANGE_AXE = 50;
        public static int RANGE_MACE = 40;
        public static int RANGE_STAFF = 90;
        public static int RANGE_THROWINGAXE = 250;
        public static int RANGE_DART = 300;
        public static int RANGE_BOW = 700;
        public static int RANGE_CROSSBOW = 500;
        public static int RANGE_SLING = 700;

        public static int COOLDOWN_SWORD = 900;
        public static int COOLDOWN_AXE = 1200;
        public static int COOLDOWN_MACE = 1000;
        public static int COOLDOWN_STAFF = 1000;
        public static int COOLDOWN_THROWINGAXE = 1250;
        public static int COOLDOWN_DART = 600;
        public static int COOLDOWN_BOW = 1400;
        public static int COOLDOWN_CROSSBOW = 1600;
        public static int COOLDOWN_SLING = 1200;

        public static int DMG_MIN_SWORD = 1;
        public static int DMG_MIN_AXE = 2;
        public static int DMG_MIN_MACE = 2;
        public static int DMG_MIN_STAFF = 1;
        public static int DMG_MIN_THROWINGAXE = 2;
        public static int DMG_MIN_DART = 1;

        // note: launcher dmg added to projectile damage (for magical launchers, esp.)
        public static int DMG_MIN_BOW = 1;
        public static int DMG_MIN_CROSSBOW = 2;
        public static int DMG_MIN_SLING = 0;

        public static int DMG_MAX_SWORD = 8;
        public static int DMG_MAX_AXE = 8;
        public static int DMG_MAX_MACE = 6;
        public static int DMG_MAX_STAFF = 6;
        public static int DMG_MAX_THROWINGAXE = 8;
        public static int DMG_MAX_DART = 3;

        public static int DMG_MAX_BOW = 1;
        public static int DMG_MAX_CROSSBOW = 2;
        public static int DMG_MAX_SLING = 0;
        #endregion

        private int range; // in pxls
        public int minDmg;
        public int maxDmg;
        public int CoolDown; // in ms
        public WeaponType weaponType;
        public WeaponClass weaponClass;
        public bool is2Handed;
        public int Range
        {
            get { return range; }
            set { range = value; }
        }

        #region Constructors and Setup
        public RPGWeapon()
        {
            StackMaxQuantity = 1;
            StackQuantity = StackMaxQuantity;
        }
        public RPGWeapon(WeaponClass c)
        {
            is2Handed = false;

            switch (c)
            {
                case (WeaponClass.ThrowingAxe):
                    {
                        range = RANGE_THROWINGAXE;
                        CoolDown = COOLDOWN_THROWINGAXE;
                        minDmg = DMG_MIN_THROWINGAXE;
                        maxDmg = DMG_MAX_THROWINGAXE;
                        weaponType = WeaponType.Thrown;
                        Slot = Inventory.BodySlot.Hand1;
                        Color1 = Color.Brown;
                        Color2 = Color.Gray;
                        Name = "Thor's Chucker";
                        StackQuantity = Projectile.QTY_MAX_AXES;
                        break;
                    }
                case (WeaponClass.Dart):
                    {
                        range = RANGE_DART;
                        CoolDown = COOLDOWN_DART;
                        minDmg = DMG_MIN_DART;
                        maxDmg = DMG_MAX_DART;
                        weaponType = WeaponType.Thrown;
                        Slot = Inventory.BodySlot.Hand1;
                        Color1 = Color.Brown;
                        Color2 = Color.Yellow;
                        Name = "Adel's Nail";
                        StackQuantity = Projectile.QTY_MAX_DARTS;
                        break;
                    }
                case (WeaponClass.Bow):
                    {
                        range = RANGE_BOW;
                        CoolDown = COOLDOWN_BOW;
                        minDmg = DMG_MIN_BOW;
                        maxDmg = DMG_MAX_BOW;
                        is2Handed = true;
                        weaponType = WeaponType.Launcher;
                        Slot = Inventory.BodySlot.Hand1;
                        Color1 = Color.Brown;
                        Color2 = Color.Gray;
                        Name = "Legolas's Bow";
                        break;
                    }
                case (WeaponClass.Crossbow):
                    {
                        range = RANGE_CROSSBOW;
                        CoolDown = COOLDOWN_CROSSBOW;
                        minDmg = DMG_MIN_CROSSBOW;
                        maxDmg = DMG_MAX_CROSSBOW;
                        is2Handed = true;
                        weaponType = WeaponType.Launcher;
                        Slot = Inventory.BodySlot.Hand1;
                        Color1 = Color.Brown;
                        Color2 = Color.Gray;
                        Name = "RobinHood's Crossbow";
                        break;
                    }
                case (WeaponClass.Sling):
                    {
                        range = RANGE_SLING;
                        CoolDown = COOLDOWN_SLING;
                        minDmg = DMG_MIN_SLING;
                        maxDmg = DMG_MAX_SLING;
                        weaponType = WeaponType.Launcher;
                        Slot = Inventory.BodySlot.Hand1;
                        Color1 = Color.Brown;
                        Color2 = Color.Orange;
                        Name = "David's Sling";
                        break;
                    }
                case (WeaponClass.Axe):
                    {
                        range = RANGE_AXE;
                        CoolDown = COOLDOWN_AXE;
                        minDmg = DMG_MIN_AXE;
                        maxDmg = DMG_MAX_AXE;
                        weaponType = WeaponType.Melee;
                        Slot = Inventory.BodySlot.Hand1;
                        Color1 = Color.Brown;
                        Color2 = Color.Gray;
                        Name = "Gimli's Axe";
                        break;
                    }
                case (WeaponClass.Mace):
                    {
                        range = RANGE_MACE;
                        CoolDown = COOLDOWN_MACE;
                        minDmg = DMG_MIN_MACE;
                        maxDmg = DMG_MAX_MACE;
                        weaponType = WeaponType.Melee;
                        Slot = Inventory.BodySlot.Hand1;
                        Color1 = Color.Gray;
                        Color2 = Color.Black;
                        Name = "Joe's Mace";
                        break;
                    }
                case (WeaponClass.Staff):
                    {
                        range = RANGE_STAFF;
                        CoolDown = COOLDOWN_STAFF;
                        minDmg = DMG_MIN_STAFF;
                        maxDmg = DMG_MAX_STAFF;
                        is2Handed = true;
                        weaponType = WeaponType.Melee;
                        Slot = Inventory.BodySlot.Hand1;
                        Color1 = Color.Brown;
                        Color2 = Color.Yellow;
                        Name = "Gandalf's Staff";
                        break;
                    }
                case (WeaponClass.Sword):
                    {
                        range = RANGE_SWORD;
                        CoolDown = COOLDOWN_SWORD;
                        minDmg = DMG_MIN_SWORD;
                        maxDmg = DMG_MAX_SWORD;
                        weaponType = WeaponType.Melee;
                        Slot = Inventory.BodySlot.Hand1;
                        Color1 = Color.Brown;
                        Color2 = Color.Gray;
                        Name = "Strider's Sword";
                        break;
                    }
                default:
                    {
                        break;
                    }
            } // end switch        

            weaponClass = c;
            UpdateDescription();
        }
        #endregion

        #region Public methods
        public Projectile.ProjectileType GetProjectileType()
        {
            if (this.weaponType != WeaponType.Melee)
            {
                switch (this.weaponClass)
                {
                    case (WeaponClass.Bow):
                        {
                            return Projectile.ProjectileType.Arrow;
                        }
                    case (WeaponClass.Crossbow):
                        {
                            return Projectile.ProjectileType.Bolt;
                        }
                    case (WeaponClass.Sling):
                        {
                            return Projectile.ProjectileType.Bullet;
                        }
                    case (WeaponClass.ThrowingAxe):
                        {
                            return Projectile.ProjectileType.Axe;
                        }
                    case (WeaponClass.Dart):
                        {
                            return Projectile.ProjectileType.Dart;
                        }
                    default:
                        {
                            return Projectile.ProjectileType.Arrow;
                        }
                }
            }
            else
            {
                return Projectile.ProjectileType.None;
            }
        }
        public Projectile getThrownProjectileFromWeapon()
        {
            if (this.weaponType != WeaponType.Thrown)
            {
                return null;
            }

            // we assume it is a thrown weapon
            Projectile p = new Projectile(null, this.GetProjectileType(), null);
            p.Name = this.Name;
            p.MinDmg = this.minDmg;
            p.MaxDmg = this.maxDmg;
            if (this.BaseSpeed != 0)
            {
                p.BaseSpeed = this.BaseSpeed;
            }
            return p;
        }
        public override void UpdateDescription()
        {
            this.m_desc = "" + minDmg + "-" + maxDmg + " dmg, " + range + " pxl range";

            if (StackQuantity > 1)
            {
                this.m_desc += " (" + this.StackQuantity + ")";
            }
        }
        #endregion

        #region Static Methods
        public static RPGItem CreateRandomWeapon()
        {
            // roll for all types of weapons
            int c = new RPGCalc().Roll(Enum.GetValues(typeof(WeaponClass)).Length);
            // create a weapon of that type
            WeaponClass wc = (WeaponClass)Enum.Parse(typeof(WeaponClass), c.ToString());
            return new RPGWeapon(wc);
        }
        public static RPGWeapon CreateRandomMeleeWeapon()
        {
            /*
            Sword = 1,
            Axe = 2,
            Mace = 3,
            Staff = 4,
            */

            int c = new RPGCalc().Roll(4);
            WeaponClass wc = (WeaponClass)Enum.Parse(typeof(WeaponClass), c.ToString());
            return new RPGWeapon(wc);
        }
        public static RPGItem CreateRandomLauncherWeapon()
        {
            /*
            Bow = 7,
            Crossbow = 8,
            Sling = 9,
            */

            int c = new RPGCalc().Roll(3) + 6;
            WeaponClass wc = (WeaponClass)Enum.Parse(typeof(WeaponClass), c.ToString());
            return new RPGWeapon(wc);
        }
        public static RPGItem CreateRandomThrownWeapon()
        {
            /*
            ThrowingAxe = 5,
            Dart = 6,
            */

            int c = new RPGCalc().Roll(2) + 4;
            WeaponClass wc = (WeaponClass)Enum.Parse(typeof(WeaponClass), c.ToString());
            return new RPGWeapon(wc);
        }
        #endregion

        #region private Methods
        #endregion
    }
}
