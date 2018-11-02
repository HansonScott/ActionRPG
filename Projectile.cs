using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RPG
{
    public class Projectile : RPGItem
    {
        public enum ProjectileType
        {
            Bullet,
            Arrow,
            Bolt,
            Axe,
            Dart,
            None
        }

        #region Public Defs
        public static int QTY_MAX_ARROWS = 30;
        public static int QTY_MAX_AXES = 20;
        public static int QTY_MAX_BOLTS = 25;
        public static int QTY_MAX_BULLETS = 30;
        public static int QTY_MAX_DARTS = 30;

        public static int SPEED_ARROW = 50;
        public static int SPEED_AXE = 40;
        public static int SPEED_BOLT = 45;
        public static int SPEED_BULLET = 40;
        public static int SPEED_DART = 50;

        public static int DMG_MIN_ARROW = 1;
        public static int DMG_MIN_AXE = 2;
        public static int DMG_MIN_BULLET = 1;
        public static int DMG_MIN_BOLT = 2;
        public static int DMG_MIN_DART = 1;

        public static int DMG_MAX_ARROW = 6;
        public static int DMG_MAX_AXE = 6;
        public static int DMG_MAX_BULLET = 4;
        public static int DMG_MAX_BOLT = 8;
        public static int DMG_MAX_DART = 3;
        #endregion

        #region Member Definitions
        public ProjectileType type;
        public Actor Owner;
        private Actor m_Target;

        private int minDmg;
        private int maxDmg;

        public int length;

        public bool IsFlying;
        #endregion

        #region Constructor
        public Projectile(Actor owner, ProjectileType t, Actor target)
        {
            Owner = owner;
            Target = target;
            type = t;

            ImpedesWalking = false;

            if (owner != null)
            {
                // set origin
                this.X = owner.X + owner.Width / 2; // middle
                this.Y = owner.Y + owner.Height / 2; // top 1/2
            }
            switch (t)
            {
                case(ProjectileType.Arrow):
                    {
                        CurrentSpeed = SPEED_ARROW;
                        minDmg = DMG_MIN_ARROW;
                        maxDmg = DMG_MAX_ARROW;

                        this.StackMaxQuantity = Projectile.QTY_MAX_ARROWS;
                        this.StackQuantity = this.StackMaxQuantity;

                        length = 20;
                        this.Size = new Size(8, 8);

                        this.Slot = Inventory.BodySlot.Ammo;

                        this.Color1 = Color.Brown;
                        this.Color2 = Color.Gray;
                        break;
                    }
                case (ProjectileType.Axe):
                    {
                        CurrentSpeed = SPEED_AXE;
                        minDmg = DMG_MIN_AXE;
                        maxDmg = DMG_MAX_AXE;

                        this.StackMaxQuantity = Projectile.QTY_MAX_AXES;
                        this.StackQuantity = this.StackMaxQuantity;

                        length = 15;
                        this.Size = new Size(9, 9);

                        this.Slot = Inventory.BodySlot.Hand1;

                        this.Color1 = Color.Brown;
                        this.Color2 = Color.Gray;
                        break;
                    }
                case (ProjectileType.Bolt):
                    {
                        CurrentSpeed = SPEED_BOLT;
                        minDmg = DMG_MIN_BOLT;
                        maxDmg = DMG_MAX_BOLT;

                        this.StackMaxQuantity = Projectile.QTY_MAX_BOLTS;
                        this.StackQuantity = this.StackMaxQuantity;

                        length = 10;
                        this.Size = new Size(6, 6);

                        this.Slot = Inventory.BodySlot.Ammo;

                        this.Color1 = Color.Brown;
                        this.Color2 = Color.Gray;
                        break;
                    }
                case (ProjectileType.Bullet):
                    {
                        CurrentSpeed = SPEED_BULLET;
                        minDmg = DMG_MIN_BULLET;
                        maxDmg = DMG_MAX_BULLET;

                        this.StackMaxQuantity = Projectile.QTY_MAX_BULLETS;
                        this.StackQuantity = this.StackMaxQuantity;

                        length = 3;
                        this.Size = new Size(5, 5);

                        this.Slot = Inventory.BodySlot.Ammo;

                        this.Color1 = Color.Gray;
                        this.Color2 = Color.Gray;

                        this.Height = 3;
                        this.Width = 3;
                        break;
                    }
                case(ProjectileType.Dart):
                    {
                        CurrentSpeed = SPEED_DART;
                        minDmg = DMG_MIN_DART;
                        maxDmg = DMG_MAX_DART;

                        this.StackMaxQuantity = Projectile.QTY_MAX_DARTS;
                        this.StackQuantity = this.StackMaxQuantity;

                        this.Slot = Inventory.BodySlot.Hand1;

                        length = 5;
                        this.Size = new Size(5, 5);

                        this.Color1 = Color.Yellow;
                        this.Color2 = Color.Red;
                        break;
                    }
                default:
                    {
                        break;
                    }
            } // end switch

            if (Target != null)
            {
                this.CalculateMovement(new Point(Target.Location.X + Target.Width / 2,
                                                Target.Location.Y + Target.Height / 3));
            }
            else
            {
                this.VX = 0;
                this.VY = 0;
            }

            UpdateDescription();
        }
        #endregion

        #region Properties
        public int MinDmg
        {
            get { return minDmg; }
            set { minDmg = value; }
        }
        public int MaxDmg
        {
            get { return maxDmg; }
            set { maxDmg = value; }
        }
        public Actor Target
        {
            get { return m_Target; }
            set
            {
                m_Target = value;

                CalculateMovement();
            }

        }
        #endregion

        #region override methods
        public override void DrawSelf(System.Drawing.Graphics g)
        {
            new RPGDraw().DrawProjectile(g, this);
        }
        public override void  UpdateSelf()
        {
            if (IsFlying == false) { return; }
            // check for collision with target
            RPGCalc calc = new RPGCalc();
            if (calc.ObjectsCollide(this, this.Target))
            {
                switch (calc.AttemptHit(this.Owner, this.Target))
                {
                    case (RPGCalc.ChallangeResult.Critical_Failure): // crit miss
                        {
                            // do nothing - maybe damage self?
                            this.Target.MissedMe();
                            break;
                        }
                    case (RPGCalc.ChallangeResult.Failure): // miss
                        {
                            // do nothing
                            this.Target.MissedMe();
                            break;
                        }
                    case (RPGCalc.ChallangeResult.Success): // hit
                        {
                            // damage target
                            Target.DamageMe(GetThisDmg(false));
                            break;
                        }
                    case (RPGCalc.ChallangeResult.Critical_Success): // crit hit
                        {
                            // damage target * 2
                            Target.DamageMe(GetThisDmg(true));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                } // end switch

                // delete self on collide
                this.DeleteMe = true;

            } // end if collide
            else
            {
                this.X += this.VX;
                this.Y += this.VY;

                // if off screen, then delete me.
                if (this.X + this.Width < 0 ||
                    this.X > Session.thisSession.TabPageAction.panelAction.Width ||
                    this.Y + this.Height < 0 ||
                    this.Y > Session.thisSession.TabPageAction.panelAction.Height)
                {
                    this.DeleteMe = true;
                }
            }
        }
        #endregion

        #region Public methods
        public Projectile GetOneOfStack()
        {
            // copy w/ quantity 1 and reduce this quantity by one.
            if (StackQuantity == 1)
            {
                return this;
            }
            else
            {
                // take one from our current stack
                StackQuantity--;

                Projectile p = new Projectile(this.Owner, this.type, this.Target);

                // copy all attributes of stack to this one...
                p.BaseSpeed = this.BaseSpeed;
                p.Color1 = this.Color1;
                p.Color2 = this.Color2;
                p.CurrentSpeed = this.CurrentSpeed;
                p.m_desc = this.Description;
                p.Direction = this.Direction;
                p.Height = this.Height;
                p.ImpedesWalking = this.ImpedesWalking;
                p.IsSelected = this.IsSelected;
                p.Location = this.Location;
                p.IsFlying = this.IsFlying;
                p.length = this.length;
                p.maxDmg = this.maxDmg;
                p.minDmg = this.minDmg;
                p.Name = this.Name;
                p.Size = this.Size;
                p.Slot = this.Slot;
                p.StackMaxQuantity = this.StackMaxQuantity;
                p.VX = this.VX;
                p.VY = this.VY;
                p.Width = this.Width;

                p.StackQuantity = 1;
                return p;
            }
        }
        #endregion

        #region private methods
        private void CalculateMovement()
        {
            if (m_Target != null)
            {
                this.CalculateMovement(new Point(m_Target.Location.X + m_Target.Width / 2,
                                                m_Target.Location.Y + m_Target.Height * 3 / 8));
            }
        }
        public override void UpdateDescription()
        {
            this.m_desc = "" + this.minDmg + " - " + this.maxDmg + " dmg, (" + this.StackQuantity + "/" + this.StackMaxQuantity + ")";
        }
        private int GetThisDmg(bool critical)
        {
            int projDmg = new RPGCalc().RollDmg(this.minDmg, this.maxDmg);

            if (critical) { projDmg *= 2; }

            RPGWeapon w = this.Owner.inventory.GetWpn();
                if (w == null){ return projDmg;}

            if (w.weaponType == RPGWeapon.WeaponType.Launcher)
            {
                if (w.minDmg == w.maxDmg)
                {
                    projDmg += w.minDmg;
                }
                else
                {
                    projDmg += new RPGCalc().RollDmg(w.minDmg, w.maxDmg);
                }
            }
            return projDmg;
        }
        #endregion

        public void UpdateLocation()
        {
            this.X = this.Owner.X + this.Owner.Width / 2;
            this.Y = this.Owner.Y + this.Owner.Height * 3 / 8;
        }
    }
}
