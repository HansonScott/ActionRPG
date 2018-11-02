using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RPG
{
    public class RPGDraw
    {
        #region Defs
        public int DAMAGE_DURATION = 100;
        #endregion

        #region Constructor
        public RPGDraw()
        {
        }
        #endregion

        #region Public Draw methods
        public void FillBack(RPGArea RPGArea, Graphics g)
        {
            // draw basics
            g.FillRectangle(Brushes.Green, 0, 0,
                    Session.thisSession.ActionPanelBackImage.Width,
                    Session.thisSession.ActionPanelBackImage.Height);

            // draw any area effects taking place.
            foreach (RPGEffect e in RPGArea.Effects)
            {
                if (e != null)
                {
                    if (e.Status == RPGEffect.EffectStatus.ActiveDone
                        || e.Status == RPGEffect.EffectStatus.Done)
                    {
                        DrawEffect(e, e.Location);
                    }
                }
            }
        }
        public void DrawObject(Graphics g, RPGObject a)
        {
            #region Decide to draw
            // make sure actor is on the screen or don't bother.
            bool onScreen = false;

            if (a.X + a.Width > 0
            && a.X < Session.thisSession.ActionPanelBackImage.Width
            && a.Y + a.Height > 0
            && a.Y < Session.thisSession.ActionPanelBackImage.Height)
            {
                onScreen = true;
            }

            if (onScreen == false)
            {
                return;
            }
            #endregion

            Pen p1 = new Pen(a.Color1);
            p1.Width = 4;
            Pen p2 = new Pen(a.Color2);
            p2.Width = 2;

            // box for basic testing of colissions, etc.
            g.DrawRectangle(p2, a.X, a.Y, a.Width, a.Height);
        }
        public void DrawActor(Graphics g, Actor a)
        {
            #region Decide to draw
            // make sure actor is on the screen or don't bother.
            bool onScreen = false;

            if (a.X + a.Width > 0
            && a.X < Session.thisSession.ActionPanelBackImage.Width
            && a.Y + a.Height > 0
            && a.Y < Session.thisSession.ActionPanelBackImage.Height)
            {
                onScreen = true;
            }

            if (onScreen == false)
            {
                return;
            }
            #endregion

            // NOTE: center is center of figure, not center of obj
            Point center = new Point(a.X + a.Width / 2, (a.Y + a.Height / 2) - 10); // -10 shift up
            #region Pen/Brush setup
            Pen p1 = new Pen(a.Color1);
            p1.Width = 4;
            SolidBrush b1 = new SolidBrush(p1.Color);

            Pen p2 = new Pen(a.Color2);
            p2.Width = 4;
            SolidBrush b2 = new SolidBrush(p2.Color);

            Pen PWpn1 = null;
            SolidBrush bWpn1 = null;
            Pen PWpn2 = null;
            SolidBrush bWpn2 = null;

            if (a.inventory.GetWpn() != null)
            {
                PWpn1 = new Pen(a.inventory.GetWpn().Color1);
                bWpn1 = new SolidBrush(PWpn1.Color);

                PWpn2 = new Pen(a.inventory.GetWpn().Color2);
                bWpn2 = new SolidBrush(PWpn2.Color);
            }
            #endregion

            // box for testing
            //g.DrawRectangle(p2, a.X, a.Y, a.Width, a.Height);

            #region Draw selection
            if (a.IsSelected == true)
            {
                Pen p3 = new Pen(Brushes.Gold, 2);
                if (a == Session.thisSession.player)
                {
                    p3.Color = Color.LightBlue;
                }
                else if (a.Relation == Actor.RelationToPC.Party)
                {
                    p3.Color = Color.Green;
                }
                else if (a.Relation == Actor.RelationToPC.Ally)
                {
                    p3.Color = Color.Green;
                }
                else if (a.Relation == Actor.RelationToPC.Neutral)
                {
                    p3.Color = Color.Yellow;
                }
                else if (a.Relation == Actor.RelationToPC.Enemy)
                {
                    p3.Color = Color.Red;
                }

                g.DrawEllipse(p3, a.X - 3, a.Y + a.Height + 16, a.Width + 6, 12);
            }
            #endregion

            #region Draw LOS
            //g.DrawEllipse(p1,   a.X + (a.Width / 2) - a.LOSRange, 
            //                    a.Y + (a.Height / 2) - a.LOSRange, 
            //                    a.LOSRange * 2, a.LOSRange * 2);
            #endregion

            #region Draw Stickfigure
            // head outline
            g.FillEllipse(b1, center.X - 10, center.Y - 20, 20, 20);

            // based on direction, draw features.
            switch(a.Direction)
            {
                #region North
                case (RPGObject.FacingDirection.North):
                    {
                        // NOTE: draw weapons before arms, because wpns behind arms
                        #region shield
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Hand2) != null)
                        {
                            // draw shield
                            RPGArmor armor = (RPGArmor)a.inventory.GetBodyItem((int)Inventory.BodySlot.Hand2);

                            Pen armorP1 = new Pen(armor.Color1);
                            Brush armorB1 = new SolidBrush(armor.Color1);

                            Pen armorP2 = new Pen(armor.Color2);
                            Brush armorB2 = new SolidBrush(armor.Color2);

                            //center.X, center.Y + 10, center.X + 14, center.Y + 14);

                            switch (armor.Class)
                            {
                                case (RPGArmor.ArmorClass.RoundShield):
                                    {
                                        g.FillEllipse(armorB1, center.X - 20, center.Y + 6, 12, 12);
                                        g.DrawEllipse(armorP2, center.X - 20, center.Y + 6, 12, 12);
                                        break;
                                    }
                                case (RPGArmor.ArmorClass.KiteShield):
                                    {
                                        Point[] points = new Point[3];
                                        points[0] = new Point(center.X - 22, center.Y + 6);
                                        points[1] = new Point(center.X - 8, center.Y + 6);
                                        points[2] = new Point(center.X - 15, center.Y + 28);
                                        g.FillPolygon(armorB1, points);
                                        g.DrawLine(armorP2, points[0], points[1]);
                                        g.DrawLine(armorP2, points[0], points[2]);
                                        g.DrawLine(armorP2, points[1], points[2]);
                                        break;
                                    }
                                case (RPGArmor.ArmorClass.TowerShield):
                                    {
                                        g.FillRectangle(armorB1, center.X - 22, center.Y + 0, 18, 28);
                                        g.DrawRectangle(armorP2, center.X - 22, center.Y + 0, 18, 28);
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            } // end switch
                        } // end if shield
                        #endregion

                        // torso
                        g.DrawLine(p1, center.X, center.Y, center.X, center.Y + 20);
                        // left leg
                        g.DrawLine(p1, center.X, center.Y + 20, center.X - 10, center.Y + 50);
                        // right leg
                        g.DrawLine(p1, center.X, center.Y + 20, center.X + 10, center.Y + 50);

                        #region boots
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet) != null)
                        {
                            Pen bootPen1 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet).Color1);
                            Pen bootPen2 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet).Color2);
                            Brush bootBrush1 = new SolidBrush(bootPen1.Color);
                            Brush bootBrush2 = new SolidBrush(bootPen2.Color);

                            switch (((RPGArmor)a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet)).Class)
                            {
                                case (RPGArmor.ArmorClass.LightBoots):
                                    {
                                        g.FillPie(bootBrush1, center.X + 7, center.Y + 50, 6, 6, 180, 180);
                                        g.FillPie(bootBrush1, center.X - 13, center.Y + 50, 6, 6, 180, 180);
                                        g.DrawPie(bootPen2, center.X + 7, center.Y + 50, 6, 6, 180, 180);
                                        g.DrawPie(bootPen2, center.X - 13, center.Y + 50, 6, 6, 180, 180);
                                        break;
                                    }
                                default: // heavy boots
                                    {
                                        bootPen1.Width = 5;
                                        g.DrawLine(bootPen1, center.X + 9, center.Y + 45, center.X + 10, center.Y + 50);
                                        g.DrawLine(bootPen1, center.X - 9, center.Y + 45, center.X - 10, center.Y + 50);

                                        g.DrawLine(bootPen2, center.X - 11, center.Y + 45, center.X - 12, center.Y + 50);
                                        g.DrawLine(bootPen2, center.X - 6, center.Y + 45, center.X - 7, center.Y + 50);

                                        g.DrawLine(bootPen2, center.X + 11, center.Y + 45, center.X + 12, center.Y + 50);
                                        g.DrawLine(bootPen2, center.X + 6, center.Y + 45, center.X + 7, center.Y + 50);

                                        bootPen1.Width = 1;
                                        g.FillPie(bootBrush1, center.X + 7, center.Y + 50, 6, 6, 180, 180);
                                        g.FillPie(bootBrush1, center.X - 13, center.Y + 50, 6, 6, 180, 180);
                                        g.DrawPie(bootPen2, center.X + 7, center.Y + 50, 6, 6, 180, 180);
                                        g.DrawPie(bootPen2, center.X - 13, center.Y + 50, 6, 6, 180, 180);

                                        break;
                                    }
                            } // end
                        }
                        #endregion

                        // draw back of head - hair
                        g.FillEllipse(b2, center.X - 10, center.Y - 20, 20, 16); // 16 - hair stops above neck
                        // draw weapon
                        if (a.inventory.GetWpn() != null)
                        {
                            #region Weapon
                            switch (a.inventory.GetWpn().weaponClass)
                            {
                                case (RPGWeapon.WeaponClass.Axe):
                                    {
                                        PWpn1.Width = 2;
                                        // base
                                        g.DrawLine(PWpn1, center.X + 13, center.Y + 3,
                                                            center.X + 13, center.Y + 20);
                                        // head
                                        g.DrawLine(PWpn2, center.X + 14, center.Y + 5,
                                                            center.X + 14, center.Y + 7);
                                        g.DrawLine(PWpn2, center.X + 15, center.Y + 5,
                                                            center.X + 15, center.Y + 7);
                                        g.DrawLine(PWpn2, center.X + 16, center.Y + 4,
                                                            center.X + 16, center.Y + 8);
                                        g.DrawLine(PWpn2, center.X + 17, center.Y + 3,
                                                            center.X + 17, center.Y + 9);
                                        g.DrawLine(PWpn2, center.X + 18, center.Y + 3,
                                                            center.X + 18, center.Y + 12);
                                        break;
                                    }
                                case (RPGWeapon.WeaponClass.Bow):
                                    {
                                        PWpn1.Width = 2;
                                        // base
                                        g.DrawArc(PWpn1, center.X + 7, center.Y - 1, 9, 28, 270, 180);

                                        // string
                                        g.DrawLine(PWpn2, center.X + 11, center.Y + 0,
                                                            center.X + 11, center.Y + 25);
                                        break;
                                    }
                                case (RPGWeapon.WeaponClass.Crossbow):
                                    {
                                        // base
                                        PWpn1.Width = 2;
                                        PWpn2.Width = 1;

                                        // center shaft
                                        g.DrawLine(PWpn1, center.X + 13, center.Y - 6,
                                                            center.X + 13, center.Y + 15);
                                        g.DrawLine(PWpn2, center.X + 13, center.Y - 5,
                                                            center.X + 13, center.Y + 15);
                                        // diagonals
                                        g.DrawLine(PWpn1, center.X + 7, center.Y + 0,
                                                            center.X + 13, center.Y + 12);
                                        g.DrawLine(PWpn1, center.X + 20, center.Y + 0,
                                                            center.X + 13, center.Y + 12);
                                        // bow arc
                                        g.DrawArc(PWpn1, center.X + 12 - 20, // left
                                                        center.Y - 2, // top
                                                        40, 60, // width, height
                                                        253, 39); // start arc, length of arc.
                                        break;
                                    }
                                case (RPGWeapon.WeaponClass.Dart):
                                    {
                                        // base
                                        g.DrawLine(PWpn1, center.X + 13, center.Y + 12,
                                                            center.X + 13, center.Y + 15);
                                        // head
                                        g.DrawLine(PWpn2, center.X + 13, center.Y + 9,
                                                            center.X + 13, center.Y + 11);

                                        break;
                                    }
                                case (RPGWeapon.WeaponClass.Mace):
                                    {
                                        // base
                                        g.DrawLine(PWpn1, center.X + 13, center.Y + 3,
                                                            center.X + 13, center.Y + 18);
                                        // head
                                        g.DrawLine(PWpn2, center.X + 11, center.Y + 2,
                                                            center.X + 11, center.Y + 2);
                                        g.DrawLine(PWpn2, center.X + 12, center.Y + 1,
                                                            center.X + 12, center.Y + 3);
                                        g.DrawLine(PWpn2, center.X + 13, center.Y - 1,
                                                            center.X + 13, center.Y + 5);
                                        g.DrawLine(PWpn2, center.X + 14, center.Y + 1,
                                                            center.X + 14, center.Y + 3);
                                        g.DrawLine(PWpn2, center.X + 15, center.Y + 2,
                                                            center.X + 15, center.Y + 2);
                                        break;
                                    }
                                case (RPGWeapon.WeaponClass.Sling):
                                    {
                                        // base - straps
                                        g.DrawLine(PWpn1, center.X + 12, center.Y + 15,
                                                            center.X + 13, center.Y + 25);
                                        g.DrawLine(PWpn1, center.X + 12, center.Y + 15,
                                                            center.X + 11, center.Y + 25);
                                        // head - pouch
                                        g.DrawLine(PWpn2, center.X + 11, center.Y + 25,
                                                            center.X + 13, center.Y + 25);

                                        break;
                                    }
                                case (RPGWeapon.WeaponClass.Staff):
                                    {
                                        // base
                                        g.DrawLine(PWpn1, center.X + 13, center.Y - 5,
                                                            center.X + 13, center.Y + 10);
                                        g.DrawLine(PWpn1, center.X + 12, center.Y - 5,
                                                            center.X + 12, center.Y + 10);
                                        g.DrawLine(PWpn1, center.X + 13, center.Y + 21,
                                                            center.X + 13, center.Y + 45);
                                        g.DrawLine(PWpn1, center.X + 12, center.Y + 21,
                                                            center.X + 12, center.Y + 45);

                                        // center handle
                                        g.DrawLine(PWpn2, center.X + 12, center.Y + 10,
                                                            center.X + 12, center.Y + 20);
                                        g.DrawLine(PWpn2, center.X + 13, center.Y + 10,
                                                            center.X + 13, center.Y + 20);
                                        break;
                                    }
                                case (RPGWeapon.WeaponClass.Sword):
                                    {
                                        // base
                                        g.DrawLine(PWpn1, center.X + 13, center.Y + 11,
                                                            center.X + 13, center.Y + 16);
                                        g.DrawLine(PWpn1, center.X + 10, center.Y + 10,
                                                            center.X + 16, center.Y + 10);
                                        // head
                                        g.DrawLine(PWpn2, center.X + 12, center.Y - 1,
                                                            center.X + 12, center.Y + 10);
                                        g.DrawLine(PWpn2, center.X + 13, center.Y - 3,
                                                            center.X + 13, center.Y + 10);
                                        g.DrawLine(PWpn2, center.X + 14, center.Y - 1,
                                                            center.X + 14, center.Y + 10);
                                        break;
                                    }
                                case (RPGWeapon.WeaponClass.ThrowingAxe):
                                    {
                                        // base
                                        g.DrawLine(PWpn1, center.X + 13, center.Y + 3,
                                                            center.X + 13, center.Y + 18);
                                        // head
                                        g.DrawLine(PWpn2, center.X + 14, center.Y + 5,
                                                            center.X + 14, center.Y + 7);
                                        g.DrawLine(PWpn2, center.X + 15, center.Y + 5,
                                                            center.X + 15, center.Y + 7);
                                        g.DrawLine(PWpn2, center.X + 16, center.Y + 4,
                                                            center.X + 16, center.Y + 8);
                                        g.DrawLine(PWpn2, center.X + 17, center.Y + 3,
                                                            center.X + 17, center.Y + 9);
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            } // end wpn
                            #endregion
                        }

                        // right arm
                        g.DrawLine(p1, center.X, center.Y + 10, center.X + 14, center.Y + 14);

                        // left arm
                        g.DrawLine(p1, center.X, center.Y + 10, center.X - 14, center.Y + 14);

                        #region torso armor
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Torso) != null)
                        {
                            RPGArmor torsoArmor = (RPGArmor)a.inventory.GetBodyItem((int)Inventory.BodySlot.Torso);

                            Pen taP1 = new Pen(torsoArmor.Color1);
                            Pen taP2 = new Pen(torsoArmor.Color2);
                            Brush taB1 = new SolidBrush(torsoArmor.Color1);
                            Brush taB2 = new SolidBrush(torsoArmor.Color2);

                            taP1.Width = 6;

                            g.DrawLine(taP1, center.X - 0, center.Y + 3, center.X - 0, center.Y + 25);
                            g.DrawRectangle(taP2, center.X - 4, center.Y + 3, 7, 22);

                            g.DrawLine(taP2, center.X - 2, center.Y + 23, center.X - 2, center.Y + 25);
                            g.DrawLine(taP2, center.X + 1, center.Y + 23, center.X + 1, center.Y + 25);

                            taP1.Width = 4;
                            g.DrawLine(taP1, center.X - 0, center.Y + 25, center.X - 7, center.Y + 40);
                            g.DrawLine(taP1, center.X - 0, center.Y + 25, center.X + 7, center.Y + 40);

                            g.DrawLine(taP2, center.X - 3, center.Y + 25, center.X - 9, center.Y + 40);
                            g.DrawLine(taP2, center.X + 3, center.Y + 25, center.X + 9, center.Y + 40);
                        }
                        #endregion

                        #region helmet
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Head) != null)
                        {
                            Pen P1 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head).Color1);
                            Pen P2 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head).Color2);
                            Brush B1 = new SolidBrush(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head).Color1);
                            Brush B2 = new SolidBrush(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head).Color2);

                            switch (((RPGArmor)a.inventory.GetBodyItem((int)Inventory.BodySlot.Head)).Class)
                            {
                                case (RPGArmor.ArmorClass.SmallHelm):
                                    {
                                        g.FillPie(B1, center.X - 10, center.Y - 24, 20, 24, 190, 160);
                                        g.DrawPie(P2, center.X - 10, center.Y - 24, 20, 24, 190, 160);

                                        g.DrawLine(P2, center.X, center.Y - 24, center.X, center.Y - 22);
                                        break;
                                    }
                                default: // full helm
                                    {
                                        P1.Width = 2;
                                        // top dome
                                        g.FillPie(B1, center.X - 10, center.Y - 24, 20, 24, 190, 160);

                                        // sides
                                        g.FillRectangle(B1, center.X - 10, center.Y - 13, 20, 14);

                                        g.DrawArc(P2, center.X - 10, center.Y - 24, 20, 24, 170, 200);
                                        g.DrawLine(P2, center.X - 10, center.Y - 10, center.X - 10, center.Y + 1);
                                        g.DrawLine(P2, center.X + 10, center.Y - 10, center.X + 10, center.Y + 1);

                                        // top
                                        g.DrawLine(P2, center.X, center.Y - 20, center.X, center.Y - 31);
                                        g.DrawLine(P2, center.X, center.Y - 24, center.X - 3, center.Y - 29);
                                        g.DrawLine(P2, center.X, center.Y - 24, center.X + 3, center.Y - 29);
                                        break;
                                    }
                            }

                        }
                        #endregion

                        #region Belt
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Belt) != null)
                        {
                            Pen BeltP1 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Belt).Color1);
                            Pen BeltP2 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Belt).Color2);

                            Brush BeltB1 = new SolidBrush(BeltP1.Color);

                            g.FillRectangle(BeltB1, center.X - 5, center.Y + 25, 10, 3);
                            g.DrawRectangle(BeltP2, center.X - 5, center.Y + 25, 10, 3);
                            g.DrawRectangle(BeltP2, center.X - 1, center.Y + 25, 2, 3);
                        }
                        #endregion

                        break;
                    }

                #endregion
                #region West
                case (RPGObject.FacingDirection.West):
                    {
                        // torso
                        g.DrawLine(p1, center.X, center.Y, center.X, center.Y + 20);

                        // left leg
                        g.DrawLine(p1, center.X, center.Y + 20, center.X - 10, center.Y + 50);
                        // right leg
                        g.DrawLine(p1, center.X, center.Y + 20, center.X + 10, center.Y + 50);

                        #region boots
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet) != null)
                        {
                            Pen bootPen1 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet).Color1);
                            Pen bootPen2 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet).Color2);
                            Brush bootBrush1 = new SolidBrush(bootPen1.Color);
                            Brush bootBrush2 = new SolidBrush(bootPen2.Color);

                            switch (((RPGArmor)a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet)).Class)
                            {
                                case (RPGArmor.ArmorClass.LightBoots):
                                    {
                                        g.FillPie(bootBrush1, center.X + 7, center.Y + 50, 6, 6, 180, 180);
                                        g.FillPie(bootBrush1, center.X - 13, center.Y + 50, 6, 6, 180, 180);
                                        g.DrawPie(bootPen2, center.X + 7, center.Y + 50, 6, 6, 180, 180);
                                        g.DrawPie(bootPen2, center.X - 13, center.Y + 50, 6, 6, 180, 180);
                                        break;
                                    }
                                default: // heavy boots
                                    {
                                        bootPen1.Width = 5;
                                        g.DrawLine(bootPen1, center.X + 9, center.Y + 45, center.X + 10, center.Y + 50);
                                        g.DrawLine(bootPen1, center.X - 9, center.Y + 45, center.X - 10, center.Y + 50);

                                        g.DrawLine(bootPen2, center.X - 11, center.Y + 45, center.X - 12, center.Y + 50);
                                        g.DrawLine(bootPen2, center.X - 6, center.Y + 45, center.X - 7, center.Y + 50);

                                        g.DrawLine(bootPen2, center.X + 11, center.Y + 45, center.X + 12, center.Y + 50);
                                        g.DrawLine(bootPen2, center.X + 6, center.Y + 45, center.X + 7, center.Y + 50);

                                        bootPen1.Width = 1;
                                        g.FillPie(bootBrush1, center.X + 7, center.Y + 50, 6, 6, 180, 180);
                                        g.FillPie(bootBrush1, center.X - 13, center.Y + 50, 6, 6, 180, 180);
                                        g.DrawPie(bootPen2, center.X + 7, center.Y + 50, 6, 6, 180, 180);
                                        g.DrawPie(bootPen2, center.X - 13, center.Y + 50, 6, 6, 180, 180);

                                        break;
                                    }
                            } // end
                        }
                        #endregion

                        // left arm (Actor's right arm)
                        g.DrawLine(p1, center.X, center.Y + 10, center.X - 11, center.Y + 16);

                        if (a.inventory.GetWpn() != null)
                        {
                            DrawRightHandItemWest(g, a, center, PWpn1, PWpn2);
                        }
                        // right arm (Actor's left arm)
                        g.DrawLine(p1, center.X, center.Y + 10, center.X + 11, center.Y + 16);

                        #region torso armor
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Torso) != null)
                        {
                            RPGArmor torsoArmor = (RPGArmor)a.inventory.GetBodyItem((int)Inventory.BodySlot.Torso);

                            Pen taP1 = new Pen(torsoArmor.Color1);
                            Pen taP2 = new Pen(torsoArmor.Color2);
                            Brush taB1 = new SolidBrush(torsoArmor.Color1);
                            Brush taB2 = new SolidBrush(torsoArmor.Color2);

                            taP1.Width = 6;

                            g.DrawLine(taP1, center.X - 0, center.Y + 3, center.X - 0, center.Y + 25);
                            g.DrawRectangle(taP2, center.X - 4, center.Y + 3, 7, 22);

                            g.DrawLine(taP2, center.X - 4, center.Y + 11, center.X - 1, center.Y + 11);

                            g.DrawLine(taP2, center.X - 2, center.Y + 23, center.X - 2, center.Y + 25);
                            g.DrawLine(taP2, center.X + 1, center.Y + 23, center.X + 1, center.Y + 25);

                            taP1.Width = 4;
                            g.DrawLine(taP1, center.X - 0, center.Y + 25, center.X - 7, center.Y + 40);
                            g.DrawLine(taP1, center.X - 0, center.Y + 25, center.X + 7, center.Y + 40);

                            g.DrawLine(taP2, center.X - 3, center.Y + 25, center.X - 9, center.Y + 40);
                            g.DrawLine(taP2, center.X + 3, center.Y + 25, center.X + 9, center.Y + 40);
                        }
                        #endregion

                        #region Belt
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Belt) != null)
                        {
                            Pen BeltP1 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Belt).Color1);
                            Pen BeltP2 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Belt).Color2);

                            Brush BeltB1 = new SolidBrush(BeltP1.Color);

                            g.FillRectangle(BeltB1, center.X - 5, center.Y + 25, 10, 3);
                            g.DrawRectangle(BeltP2, center.X - 5, center.Y + 25, 10, 3);
                            g.DrawRectangle(BeltP2, center.X - 1, center.Y + 25, 2, 3);
                        }
                        #endregion

                        #region shield
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Hand2) != null)
                        {
                            // draw shield
                            RPGArmor armor = (RPGArmor)a.inventory.GetBodyItem((int)Inventory.BodySlot.Hand2);

                            Pen armorP1 = new Pen(armor.Color1);
                            Brush armorB1 = new SolidBrush(armor.Color1);

                            Pen armorP2 = new Pen(armor.Color2);
                            Brush armorB2 = new SolidBrush(armor.Color2);

                            switch (armor.Class)
                            {
                                case (RPGArmor.ArmorClass.RoundShield):
                                    {
                                        g.FillEllipse(armorB1, center.X + 5, center.Y + 6, 12, 12);
                                        g.DrawEllipse(armorP2, center.X + 5, center.Y + 6, 12, 12);

                                        // center cross
                                        g.DrawLine(armorP2, center.X + 7, center.Y + 12, center.X + 15, center.Y + 12);
                                        g.DrawLine(armorP2, center.X + 11, center.Y + 8, center.X + 11, center.Y + 16);
                                        break;
                                    }
                                case (RPGArmor.ArmorClass.KiteShield):
                                    {
                                        Point[] points = new Point[3];
                                        points[0] = new Point(center.X + 22, center.Y + 6);
                                        points[1] = new Point(center.X + 8, center.Y + 6);
                                        points[2] = new Point(center.X + 15, center.Y + 28);
                                        g.FillPolygon(armorB1, points);
                                        g.DrawLine(armorP2, points[0], points[1]);
                                        g.DrawLine(armorP2, points[0], points[2]);
                                        g.DrawLine(armorP2, points[1], points[2]);
                                        break;
                                    }
                                case (RPGArmor.ArmorClass.TowerShield):
                                    {
                                        g.FillRectangle(armorB1, center.X + 4, center.Y + 0, 18, 28);
                                        g.DrawRectangle(armorP2, center.X + 4, center.Y + 0, 18, 28);
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            } // end switch
                        } // end if shield
                        #endregion

                        // draw left-profile
                        // hair
                        p2.Width = 5;
                        g.DrawArc(p2, center.X - 10, center.Y - 20, 20, 20, 220, 180);
                        g.DrawArc(p2, center.X - 6, center.Y - 16, 12, 12, 220, 180);

                        // eye
                        p2.Width = 2;
                        g.DrawLine(p2, center.X - 7, center.Y - 11, center.X - 5, center.Y - 11);
                        
                        // mouth
                        p2.Width = 2;
                        g.DrawLine(p2, center.X - 10, center.Y - 6, center.X - 3, center.Y - 6);

                        #region helmet
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Head) != null)
                        {
                            Pen P1 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head).Color1);
                            Pen P2 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head).Color2);
                            Brush B1 = new SolidBrush(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head).Color1);
                            Brush B2 = new SolidBrush(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head).Color2);

                            switch (((RPGArmor)a.inventory.GetBodyItem((int)Inventory.BodySlot.Head)).Class)
                            {
                                case (RPGArmor.ArmorClass.SmallHelm):
                                    {
                                        g.FillPie(B1, center.X - 10, center.Y - 24, 22, 24, 190, 180);
                                        g.DrawPie(P2, center.X - 10, center.Y - 24, 22, 24, 190, 180);

                                        g.DrawLine(P2, center.X + 4, center.Y - 24, center.X + 3, center.Y - 22);
                                        break;
                                    }
                                default: // full helm
                                    {
                                        P1.Width = 2;
                                        // top dome
                                        g.FillPie(B1, center.X - 11, center.Y - 24, 23, 24, 190, 160);
                                        g.DrawLine(P1, center.X - 9, center.Y - 13, center.X - 9, center.Y - 8);
                                        g.DrawArc(P2, center.X - 11, center.Y - 24, 23, 24, 170, 190);

                                        P1.Width = 4;
                                        g.DrawLine(P1, center.X + 2, center.Y - 13, center.X + 2, center.Y + 4);
                                        g.DrawLine(P1, center.X + 6, center.Y - 13, center.X + 6, center.Y + 4);
                                        g.DrawLine(P1, center.X + 10, center.Y - 13, center.X + 10, center.Y + 4);
                                        g.DrawLine(P2, center.X + 12, center.Y - 11, center.X + 12, center.Y + 4);

                                        // top
                                        g.DrawLine(P2, center.X, center.Y - 20, center.X, center.Y - 31);
                                        g.DrawLine(P2, center.X, center.Y - 24, center.X - 3, center.Y - 29);
                                        g.DrawLine(P2, center.X, center.Y - 24, center.X + 3, center.Y - 29);
                                        break;
                                    }
                            }

                        }
                        #endregion

                        break;
                    }
                #endregion
                #region East
                case (RPGObject.FacingDirection.East):
                    {
                        #region shield
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Hand2) != null)
                        {
                            // draw shield
                            RPGArmor armor = (RPGArmor)a.inventory.GetBodyItem((int)Inventory.BodySlot.Hand2);

                            Pen armorP1 = new Pen(armor.Color1);
                            Brush armorB1 = new SolidBrush(armor.Color1);

                            Pen armorP2 = new Pen(armor.Color2);
                            Brush armorB2 = new SolidBrush(armor.Color2);

                            //center.X, center.Y + 10, center.X + 14, center.Y + 14);

                            switch (armor.Class)
                            {
                                case (RPGArmor.ArmorClass.RoundShield):
                                    {
                                        g.FillEllipse(armorB1, center.X - 18, center.Y + 8, 12, 12);
                                        g.DrawEllipse(armorP2, center.X - 18, center.Y + 8, 12, 12);
                                        break;
                                    }
                                case (RPGArmor.ArmorClass.KiteShield):
                                    {
                                        Point[] points = new Point[3];
                                        points[0] = new Point(center.X - 22, center.Y + 6);
                                        points[1] = new Point(center.X - 8, center.Y + 6);
                                        points[2] = new Point(center.X - 15, center.Y + 28);
                                        g.FillPolygon(armorB1, points);
                                        g.DrawLine(armorP2, points[0], points[1]);
                                        g.DrawLine(armorP2, points[0], points[2]);
                                        g.DrawLine(armorP2, points[1], points[2]);
                                        break;
                                    }
                                case (RPGArmor.ArmorClass.TowerShield):
                                    {
                                        g.FillRectangle(armorB1, center.X - 22, center.Y + 0, 18, 28);
                                        g.DrawRectangle(armorP2, center.X - 22, center.Y + 0, 18, 28);
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            } // end switch
                        } // end if shield
                        #endregion

                        // torso
                        g.DrawLine(p1, center.X, center.Y, center.X, center.Y + 20);

                        // left leg
                        g.DrawLine(p1, center.X, center.Y + 20, center.X - 10, center.Y + 50);
                        // right leg
                        g.DrawLine(p1, center.X, center.Y + 20, center.X + 10, center.Y + 50);

                        #region boots
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet) != null)
                        {
                            Pen bootPen1 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet).Color1);
                            Pen bootPen2 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet).Color2);
                            Brush bootBrush1 = new SolidBrush(bootPen1.Color);
                            Brush bootBrush2 = new SolidBrush(bootPen2.Color);

                            switch (((RPGArmor)a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet)).Class)
                            {
                                case (RPGArmor.ArmorClass.LightBoots):
                                    {
                                        g.FillPie(bootBrush1, center.X + 7, center.Y + 50, 6, 6, 180, 180);
                                        g.FillPie(bootBrush1, center.X - 13, center.Y + 50, 6, 6, 180, 180);
                                        g.DrawPie(bootPen2, center.X + 7, center.Y + 50, 6, 6, 180, 180);
                                        g.DrawPie(bootPen2, center.X - 13, center.Y + 50, 6, 6, 180, 180);
                                        break;
                                    }
                                default: // heavy boots
                                    {
                                        bootPen1.Width = 5;
                                        g.DrawLine(bootPen1, center.X + 9, center.Y + 45, center.X + 10, center.Y + 50);
                                        g.DrawLine(bootPen1, center.X - 9, center.Y + 45, center.X - 10, center.Y + 50);

                                        g.DrawLine(bootPen2, center.X - 11, center.Y + 45, center.X - 12, center.Y + 50);
                                        g.DrawLine(bootPen2, center.X - 6, center.Y + 45, center.X - 7, center.Y + 50);

                                        g.DrawLine(bootPen2, center.X + 11, center.Y + 45, center.X + 12, center.Y + 50);
                                        g.DrawLine(bootPen2, center.X + 6, center.Y + 45, center.X + 7, center.Y + 50);

                                        bootPen1.Width = 1;
                                        g.FillPie(bootBrush1, center.X + 7, center.Y + 50, 6, 6, 180, 180);
                                        g.FillPie(bootBrush1, center.X - 13, center.Y + 50, 6, 6, 180, 180);
                                        g.DrawPie(bootPen2, center.X + 7, center.Y + 50, 6, 6, 180, 180);
                                        g.DrawPie(bootPen2, center.X - 13, center.Y + 50, 6, 6, 180, 180);

                                        break;
                                    }
                            } // end
                        }
                        #endregion

                        // left arm
                        g.DrawLine(p1, center.X, center.Y + 10, center.X - 11, center.Y + 16);
                        // right arm
                        g.DrawLine(p1, center.X, center.Y + 10, center.X + 11, center.Y + 16);


                        #region torso armor
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Torso) != null)
                        {
                            RPGArmor torsoArmor = (RPGArmor)a.inventory.GetBodyItem((int)Inventory.BodySlot.Torso);

                            Pen taP1 = new Pen(torsoArmor.Color1);
                            Pen taP2 = new Pen(torsoArmor.Color2);
                            Brush taB1 = new SolidBrush(torsoArmor.Color1);
                            Brush taB2 = new SolidBrush(torsoArmor.Color2);

                            taP1.Width = 6;

                            g.DrawLine(taP1, center.X - 0, center.Y + 3, center.X - 0, center.Y + 25);
                            g.DrawRectangle(taP2, center.X - 4, center.Y + 3, 7, 22);

                            g.DrawLine(taP2, center.X - 0, center.Y + 11, center.X + 3, center.Y + 11);

                            g.DrawLine(taP2, center.X - 2, center.Y + 23, center.X - 2, center.Y + 25);
                            g.DrawLine(taP2, center.X + 1, center.Y + 23, center.X + 1, center.Y + 25);

                            taP1.Width = 4;
                            g.DrawLine(taP1, center.X - 0, center.Y + 25, center.X - 7, center.Y + 40);
                            g.DrawLine(taP1, center.X - 0, center.Y + 25, center.X + 7, center.Y + 40);

                            g.DrawLine(taP2, center.X - 3, center.Y + 25, center.X - 9, center.Y + 40);
                            g.DrawLine(taP2, center.X + 3, center.Y + 25, center.X + 9, center.Y + 40);
                        }
                        #endregion

                        #region Belt
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Belt) != null)
                        {
                            Pen BeltP1 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Belt).Color1);
                            Pen BeltP2 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Belt).Color2);

                            Brush BeltB1 = new SolidBrush(BeltP1.Color);

                            g.FillRectangle(BeltB1, center.X - 5, center.Y + 25, 10, 3);
                            g.DrawRectangle(BeltP2, center.X - 5, center.Y + 25, 10, 3);
                            g.DrawRectangle(BeltP2, center.X - 1, center.Y + 25, 2, 3);
                        }
                        #endregion

                        // left hand item
                        
                        // draw right hand item
                        if (a.inventory.GetWpn() != null)
                        {
                            DrawRightHandItemEast(g, a, center, PWpn1, PWpn2);
                        }
                        // draw right-profile
                        // hair
                        p2.Width = 5;
                        g.DrawArc(p2, center.X - 10, center.Y - 20, 20, 20, 140, 180);
                        g.DrawArc(p2, center.X - 6, center.Y - 16, 12, 12, 140, 180);

                        // eye
                        p2.Width = 2;
                        g.DrawLine(p2, center.X + 7, center.Y - 11, center.X + 5, center.Y - 11);

                        // mouth
                        p2.Width = 2;
                        g.DrawLine(p2, center.X + 10, center.Y - 6, center.X + 3, center.Y - 6);

                        #region helmet
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Head) != null)
                        {
                            Pen P1 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head).Color1);
                            Pen P2 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head).Color2);
                            Brush B1 = new SolidBrush(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head).Color1);
                            Brush B2 = new SolidBrush(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head).Color2);

                            switch (((RPGArmor)a.inventory.GetBodyItem((int)Inventory.BodySlot.Head)).Class)
                            {
                                case (RPGArmor.ArmorClass.SmallHelm):
                                    {
                                        g.FillPie(B1, center.X - 12, center.Y - 24, 22, 24, 170, 180);
                                        g.DrawPie(P2, center.X - 12, center.Y - 24, 22, 24, 170, 180);

                                        g.DrawLine(P2, center.X - 3, center.Y - 24, center.X - 4, center.Y - 22);
                                        break;
                                    }
                                default: // full helm
                                    {
                                        P1.Width = 2;
                                        // top dome
                                        g.FillPie(B1, center.X - 12, center.Y - 24, 23, 24, 190, 160);
                                        g.DrawLine(P1, center.X + 10, center.Y - 13, center.X + 10, center.Y - 9);
                                        g.DrawArc(P2, center.X - 12, center.Y - 24, 23, 24, 170, 190);

                                        P1.Width = 4;
                                        g.DrawLine(P1, center.X - 2, center.Y - 13, center.X - 2 , center.Y + 4);
                                        g.DrawLine(P1, center.X - 5, center.Y - 13, center.X - 5, center.Y + 4);
                                        g.DrawLine(P1, center.X - 9, center.Y - 13, center.X - 9, center.Y + 4);
                                        g.DrawLine(P2, center.X - 12, center.Y - 10, center.X - 12, center.Y + 4);

                                        // top
                                        g.DrawLine(P2, center.X, center.Y - 20, center.X, center.Y - 31);
                                        g.DrawLine(P2, center.X, center.Y - 24, center.X - 3, center.Y - 29);
                                        g.DrawLine(P2, center.X, center.Y - 24, center.X + 3, center.Y - 29);
                                        break;
                                    }
                            }

                        }
                        #endregion
                        break;
                    }
                #endregion
                #region South
                default: // south
                    {
                        // torso
                        g.DrawLine(p1, center.X, center.Y, center.X, center.Y + 20);

                        // left leg
                        g.DrawLine(p1, center.X, center.Y + 20, center.X - 10, center.Y + 50);
                        // right leg
                        g.DrawLine(p1, center.X, center.Y + 20, center.X + 10, center.Y + 50);

                        #region boots
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet) != null)
                        {
                            Pen bootPen1 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet).Color1);
                            Pen bootPen2 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet).Color2);
                            Brush bootBrush1 = new SolidBrush(bootPen1.Color);
                            Brush bootBrush2 = new SolidBrush(bootPen2.Color);

                            switch(((RPGArmor)a.inventory.GetBodyItem((int)Inventory.BodySlot.Feet)).Class)
                            {
                                case (RPGArmor.ArmorClass.LightBoots):
                                    {
                                        g.FillPie(bootBrush1, center.X + 7, center.Y + 50, 6, 6, 180, 180);
                                        g.FillPie(bootBrush1, center.X - 13, center.Y + 50, 6, 6, 180, 180);
                                        g.DrawPie(bootPen2, center.X + 7, center.Y + 50, 6, 6, 180, 180);
                                        g.DrawPie(bootPen2, center.X - 13, center.Y + 50, 6, 6, 180, 180);
                                        break;
                                    }
                                default: // heavy boots
                                    {
                                        bootPen1.Width = 5;
                                        g.DrawLine(bootPen1, center.X + 9, center.Y + 45, center.X + 10, center.Y + 50);
                                        g.DrawLine(bootPen1, center.X - 9, center.Y + 45, center.X - 10, center.Y + 50);

                                        g.DrawLine(bootPen2, center.X - 11, center.Y + 45, center.X - 12, center.Y + 50);
                                        g.DrawLine(bootPen2, center.X - 6, center.Y + 45, center.X - 7, center.Y + 50);

                                        g.DrawLine(bootPen2, center.X + 11, center.Y + 45, center.X + 12, center.Y + 50);
                                        g.DrawLine(bootPen2, center.X + 6, center.Y + 45, center.X + 7, center.Y + 50);

                                        bootPen1.Width = 1;
                                        g.FillPie(bootBrush1, center.X + 7, center.Y + 50, 6, 6, 180, 180);
                                        g.FillPie(bootBrush1, center.X - 13, center.Y + 50, 6, 6, 180, 180);
                                        g.DrawPie(bootPen2, center.X + 7, center.Y + 50, 6, 6, 180, 180);
                                        g.DrawPie(bootPen2, center.X - 13, center.Y + 50, 6, 6, 180, 180);

                                        break;
                                    }
                            } // end
                        }
                        #endregion

                        // left arm (actor's right arm)
                        g.DrawLine(p1, center.X, center.Y + 10, center.X - 14, center.Y + 14);

                        // right arm (actor's left arm)
                        g.DrawLine(p1, center.X, center.Y + 10, center.X + 14, center.Y + 14);

                        #region torso armor
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Torso) != null)
                        {
                            RPGArmor torsoArmor = (RPGArmor)a.inventory.GetBodyItem((int)Inventory.BodySlot.Torso);

                            Pen taP1 = new Pen(torsoArmor.Color1);
                            Pen taP2 = new Pen(torsoArmor.Color2);
                            Brush taB1 = new SolidBrush(torsoArmor.Color1);
                            Brush taB2 = new SolidBrush(torsoArmor.Color2);

                            taP1.Width = 6;

                            g.DrawLine      (taP1, center.X - 0, center.Y + 3, center.X - 0, center.Y + 25);
                            g.DrawRectangle (taP2, center.X - 4, center.Y + 3, 7, 22);
                            g.DrawLine(taP2, center.X, center.Y + 8, center.X, center.Y + 11);
                            g.DrawLine(taP2, center.X - 2, center.Y + 11, center.X + 2, center.Y + 11);

                            g.DrawLine(taP2, center.X - 2, center.Y + 23, center.X - 2, center.Y + 25);
                            g.DrawLine(taP2, center.X + 1, center.Y + 23, center.X + 1, center.Y + 25);

                            taP1.Width = 4;
                            g.DrawLine(taP1, center.X - 0, center.Y + 25, center.X - 7, center.Y + 40);
                            g.DrawLine(taP1, center.X - 0, center.Y + 25, center.X + 7, center.Y + 40);

                            g.DrawLine(taP2, center.X - 3, center.Y + 25, center.X - 9, center.Y + 40);
                            g.DrawLine(taP2, center.X + 3, center.Y + 25, center.X + 9, center.Y + 40);
                        }
                        #endregion

                        // draw weapon
                        if (a.inventory.GetWpn() != null)
                        {
                            #region Weapon
                            switch (a.inventory.GetWpn().weaponClass)
                            {
                                case (RPGWeapon.WeaponClass.Axe):
                                    {
                                        PWpn1.Width = 2;
                                        // base
                                        g.DrawLine(PWpn1, center.X - 13, center.Y + 3,
                                                            center.X - 13, center.Y + 20);
                                        // head
                                        g.DrawLine(PWpn2, center.X - 14, center.Y + 5,
                                                            center.X - 14, center.Y + 7);
                                        g.DrawLine(PWpn2, center.X - 15, center.Y + 5,
                                                            center.X - 15, center.Y + 7);
                                        g.DrawLine(PWpn2, center.X - 16, center.Y + 4,
                                                            center.X - 16, center.Y + 8);
                                        g.DrawLine(PWpn2, center.X - 17, center.Y + 3,
                                                            center.X - 17, center.Y + 9);
                                        g.DrawLine(PWpn2, center.X - 18, center.Y + 3,
                                                            center.X - 18, center.Y + 12);
                                        break;
                                    }
                                case (RPGWeapon.WeaponClass.Bow):
                                    {
                                        PWpn1.Width = 2;
                                        // base
                                        g.DrawArc(PWpn1, center.X - 14, center.Y - 1, 9, 28, 270, -180);

                                        // string
                                        g.DrawLine(PWpn2, center.X - 11, center.Y + 0,
                                                            center.X - 11, center.Y + 25);
                                        break;
                                    }
                                case (RPGWeapon.WeaponClass.Crossbow):
                                    {
                                        // base
                                        PWpn1.Width = 2;
                                        PWpn2.Width = 1;

                                        // center shaft
                                        g.DrawLine(PWpn1, center.X - 13, center.Y - 6,
                                                            center.X - 13, center.Y + 15);
                                        g.DrawLine(PWpn2, center.X - 13, center.Y - 5,
                                                            center.X - 13, center.Y + 15);
                                        // diagonals
                                        g.DrawLine(PWpn1, center.X - 7, center.Y + 0,
                                                            center.X - 13, center.Y + 12);
                                        g.DrawLine(PWpn1, center.X - 20, center.Y + 0,
                                                            center.X - 13, center.Y + 12);
                                        // bow arc
                                        g.DrawArc(PWpn1, center.X - 12 - 20, // left
                                                        center.Y - 2, // top
                                                        40, 60, // width, height
                                                        250, 39); // start arc, length of arc.
                                        break;
                                    }
                                case (RPGWeapon.WeaponClass.Dart):
                                    {
                                        // base
                                        g.DrawLine(PWpn1, center.X - 13, center.Y + 12,
                                                            center.X - 13, center.Y + 15);
                                        // head
                                        g.DrawLine(PWpn2, center.X - 13, center.Y + 9,
                                                            center.X - 13, center.Y + 11);

                                        break;
                                    }
                                case (RPGWeapon.WeaponClass.Mace):
                                    {
                                        // base
                                        g.DrawLine(PWpn1, center.X - 13, center.Y + 3,
                                                            center.X - 13, center.Y + 18);
                                        // head
                                        g.DrawLine(PWpn2, center.X - 11, center.Y + 2,
                                                            center.X - 11, center.Y + 2);
                                        g.DrawLine(PWpn2, center.X - 12, center.Y + 1,
                                                            center.X - 12, center.Y + 3);
                                        g.DrawLine(PWpn2, center.X - 13, center.Y - 1,
                                                            center.X - 13, center.Y + 5);
                                        g.DrawLine(PWpn2, center.X - 14, center.Y + 1,
                                                            center.X - 14, center.Y + 3);
                                        g.DrawLine(PWpn2, center.X - 15, center.Y + 2,
                                                            center.X - 15, center.Y + 2);
                                        break;
                                    }
                                case (RPGWeapon.WeaponClass.Sling):
                                    {
                                        // base - straps
                                        g.DrawLine(PWpn1, center.X - 12, center.Y + 15,
                                                            center.X - 13, center.Y + 25);
                                        g.DrawLine(PWpn1, center.X - 12, center.Y + 15,
                                                            center.X - 11, center.Y + 25);
                                        // head - pouch
                                        g.DrawLine(PWpn2, center.X - 11, center.Y + 25,
                                                            center.X - 13, center.Y + 25);

                                        break;
                                    }
                                case (RPGWeapon.WeaponClass.Staff):
                                    {
                                        // base
                                        g.DrawLine(PWpn1, center.X - 13, center.Y - 5,
                                                            center.X - 13, center.Y + 10);
                                        g.DrawLine(PWpn1, center.X - 12, center.Y - 5,
                                                            center.X - 12, center.Y + 10);
                                        g.DrawLine(PWpn1, center.X - 13, center.Y + 21,
                                                            center.X - 13, center.Y + 45);
                                        g.DrawLine(PWpn1, center.X - 12, center.Y + 21,
                                                            center.X - 12, center.Y + 45);

                                        // center handle
                                        g.DrawLine(PWpn2, center.X - 12, center.Y + 10,
                                                            center.X - 12, center.Y + 20);
                                        g.DrawLine(PWpn2, center.X - 13, center.Y + 10,
                                                            center.X - 13, center.Y + 20);
                                        break;
                                    }
                                case (RPGWeapon.WeaponClass.Sword):
                                    {
                                        // base
                                        g.DrawLine(PWpn1, center.X - 13, center.Y + 11,
                                                            center.X - 13, center.Y + 16);
                                        g.DrawLine(PWpn1, center.X - 10, center.Y + 10,
                                                            center.X - 16, center.Y + 10);
                                        // head
                                        g.DrawLine(PWpn2, center.X - 12, center.Y - 1,
                                                            center.X - 12, center.Y + 10);
                                        g.DrawLine(PWpn2, center.X - 13, center.Y - 3,
                                                            center.X - 13, center.Y + 10);
                                        g.DrawLine(PWpn2, center.X - 14, center.Y - 1,
                                                            center.X - 14, center.Y + 10);
                                        break;
                                    }
                                case (RPGWeapon.WeaponClass.ThrowingAxe):
                                    {
                                        // base
                                        g.DrawLine(PWpn1, center.X - 13, center.Y + 3,
                                                            center.X - 13, center.Y + 18);
                                        // head
                                        g.DrawLine(PWpn2, center.X - 14, center.Y + 5,
                                                            center.X - 14, center.Y + 7);
                                        g.DrawLine(PWpn2, center.X - 15, center.Y + 5,
                                                            center.X - 15, center.Y + 7);
                                        g.DrawLine(PWpn2, center.X - 16, center.Y + 4,
                                                            center.X - 16, center.Y + 8);
                                        g.DrawLine(PWpn2, center.X - 17, center.Y + 3,
                                                            center.X - 17, center.Y + 9);
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            } // end wpn
                            #endregion
                        }

                        #region Belt
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Belt) != null)
                        {
                            Pen BeltP1 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Belt).Color1);
                            Pen BeltP2 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Belt).Color2);

                            Brush BeltB1 = new SolidBrush(BeltP1.Color);

                            g.FillRectangle(BeltB1, center.X - 5, center.Y + 25, 10, 3);
                            g.DrawRectangle(BeltP2, center.X - 5, center.Y + 25, 10, 3);
                            g.DrawRectangle(BeltP2, center.X - 1, center.Y + 25, 2, 3);
                        }
                        #endregion

                        #region shield
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Hand2) != null)
                        {
                            // draw shield
                            RPGArmor armor = (RPGArmor)a.inventory.GetBodyItem((int)Inventory.BodySlot.Hand2);

                            Pen armorP1 = new Pen(armor.Color1);
                            Brush armorB1 = new SolidBrush(armor.Color1);

                            Pen armorP2 = new Pen(armor.Color2);
                            Brush armorB2 = new SolidBrush(armor.Color2);

                            switch (armor.Class)
                            {
                                case (RPGArmor.ArmorClass.RoundShield):
                                    {
                                        g.FillEllipse(armorB1, center.X + 8, center.Y + 6, 12, 12);
                                        g.DrawEllipse(armorP2, center.X + 8, center.Y + 6, 12, 12);

                                        // center cross
                                        g.DrawLine(armorP2, center.X + 10, center.Y + 12, center.X + 18, center.Y + 12);
                                        g.DrawLine(armorP2, center.X + 14, center.Y + 8, center.X + 14, center.Y + 16);
                                        break;
                                    }
                                case (RPGArmor.ArmorClass.KiteShield):
                                    {
                                        Point[] points = new Point[3];
                                        points[0] = new Point(center.X + 22, center.Y + 6);
                                        points[1] = new Point(center.X + 8, center.Y + 6);
                                        points[2] = new Point(center.X + 15, center.Y + 28);
                                        g.FillPolygon(armorB1, points);
                                        g.DrawLine(armorP2, points[0], points[1]);
                                        g.DrawLine(armorP2, points[0], points[2]);
                                        g.DrawLine(armorP2, points[1], points[2]);
                                        break;
                                    }
                                case (RPGArmor.ArmorClass.TowerShield):
                                    {
                                        g.FillRectangle(armorB1, center.X + 4, center.Y + 0, 18, 28);
                                        g.DrawRectangle(armorP2, center.X + 4, center.Y + 0, 18, 28);
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            } // end switch
                        } // end if shield
                        #endregion

                        // draw face
                        // left eye
                        p2.Width = 2;
                        g.DrawLine(p2, center.X - 5, center.Y - 11, center.X - 3, center.Y - 11);

                        // right eye
                        p2.Width = 2;
                        g.DrawLine(p2, center.X + 5, center.Y - 11, center.X + 3, center.Y - 11);

                        // nose
                        p2.Width = 2;
                        g.DrawLine(p2, center.X, center.Y - 9, center.X, center.Y - 6);

                        // mouth
                        p2.Width = 2;
                        g.DrawLine(p2, center.X - 5, center.Y - 3, center.X + 5, center.Y - 3);

                        // hair
                        g.DrawArc(p2, center.X - 10, center.Y - 20, 20, 20, 200, 140);

                        #region helmet
                        if (a.inventory.GetBodyItem((int)Inventory.BodySlot.Head) != null)
                        {
                            Pen P1 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head).Color1);
                            Pen P2 = new Pen(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head).Color2);
                            Brush B1 = new SolidBrush(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head).Color1);
                            Brush B2 = new SolidBrush(a.inventory.GetBodyItem((int)Inventory.BodySlot.Head).Color2);

                            switch (((RPGArmor)a.inventory.GetBodyItem((int)Inventory.BodySlot.Head)).Class)
                            {
                                case (RPGArmor.ArmorClass.SmallHelm):
                                    {
                                        g.FillPie(B1, center.X - 10, center.Y - 24, 20, 24, 190, 160);
                                        g.DrawPie(P2, center.X - 10, center.Y - 24, 20, 24, 190, 160);

                                        g.DrawLine(P2, center.X, center.Y - 24, center.X, center.Y - 22);
                                        break;
                                    }
                                default: // full helm
                                    {
                                        P1.Width = 2;
                                        // top dome
                                        g.FillPie(B1, center.X - 10, center.Y - 24, 20, 24, 190, 160);
                                        g.DrawArc(P2, center.X - 10, center.Y - 24, 20, 24, 170, 200);

                                        // center nose guard
                                        g.DrawLine(P1, center.X + 0, center.Y - 13, center.X + 0, center.Y - 5);

                                        // sides
                                        g.DrawLine(P1, center.X - 8, center.Y - 13, center.X - 8, center.Y + 1);
                                        g.DrawLine(P2, center.X - 10, center.Y - 10, center.X - 10, center.Y + 1);

                                        g.DrawLine(P1, center.X + 9, center.Y - 13, center.X + 9, center.Y + 1);
                                        g.DrawLine(P2, center.X + 10, center.Y - 10, center.X + 10, center.Y + 1);

                                        // top
                                        g.DrawLine(P2, center.X, center.Y - 20, center.X, center.Y - 31);
                                        g.DrawLine(P2, center.X, center.Y - 24, center.X - 3, center.Y - 29);
                                        g.DrawLine(P2, center.X, center.Y - 24, center.X + 3, center.Y - 29);
                                        break;
                                    }
                            }

                        }
                        #endregion

                        break;
                    }
                #endregion
            }
            #endregion

            #region Draw Effects
            foreach (RPGEffect e in a.Effects)
            {
                if (e != null)
                {
                    // only draw if effect has been activated.
                    if (e.Status == RPGEffect.EffectStatus.ActiveDone
                    || e.Status == RPGEffect.EffectStatus.Done)
                    {
                        DrawEffect(g, e, a);
                    }
                }
            }
            #endregion
        }
        
        public void DrawProjectile(Graphics g, Projectile projectile)
        {
            // Color1 = base/frame color
            // Color2 = tip/edge color

            Pen p1 = new Pen(projectile.Color1);
            p1.Width = 2;
            Pen p2 = new Pen(projectile.Color2);
            p2.Width = 2;

            Point loc = projectile.Location;

            switch (projectile.type)
            {
                case (Projectile.ProjectileType.Bolt):
                case(Projectile.ProjectileType.Dart):
                case (Projectile.ProjectileType.Arrow):
                    {
                        // calculate angle
                        int distanceX = projectile.VX;
                        int distanceY = projectile.VY;

                        double angle = Math.Atan((double)distanceY / (double)distanceX);

                        int X2 = (int)(Math.Cos(angle) * projectile.length);
                        int Y2 = (int)(Math.Sin(angle) * projectile.length);

                        int X3 = (int)(Math.Cos(angle) * 2);
                        int Y3 = (int)(Math.Sin(angle) * 2);

                        g.DrawLine(p1, loc.X, loc.Y, loc.X + X2, loc.Y + Y2);
                        g.DrawLine(p2, loc.X, loc.Y, loc.X + X2 + X3, loc.Y + Y2 + Y3);
                        break;
                    }
                case (Projectile.ProjectileType.Axe):
                    {
                        // if east
                        if (projectile.VX >= 0)
                        {
                            // base
                            g.DrawLine(p1, loc.X, loc.Y,
                                            loc.X, loc.Y + 15);
                            // head
                            g.DrawLine(p2, loc.X + 1, loc.Y + 5,
                                                loc.X + 1, loc.Y + 7);
                            g.DrawLine(p2, loc.X + 2, loc.Y + 5,
                                                loc.X + 2, loc.Y + 7);
                            g.DrawLine(p2, loc.X + 3, loc.Y + 4,
                                                loc.X + 3, loc.Y + 8);
                            g.DrawLine(p2, loc.X + 4, loc.Y + 3,
                                                loc.X + 4, loc.Y + 9);
                        }
                        else // west
                        {
                            // base
                            g.DrawLine(p1, loc.X, loc.Y,
                                            loc.X, loc.Y + 15);
                            // head
                            g.DrawLine(p2, loc.X - 1, loc.Y + 5,
                                                loc.X - 1, loc.Y + 7);
                            g.DrawLine(p2, loc.X - 2, loc.Y + 5,
                                                loc.X - 2, loc.Y + 7);
                            g.DrawLine(p2, loc.X - 3, loc.Y + 4,
                                                loc.X - 3, loc.Y + 8);
                            g.DrawLine(p2, loc.X - 4, loc.Y + 3,
                                                loc.X - 4, loc.Y + 9);
                        }
                        
                        break;
                    }
                case (Projectile.ProjectileType.Bullet):
                    {
                        g.DrawEllipse(p1, loc.X, loc.Y, projectile.Width, projectile.Height);
                        g.DrawEllipse(p2, loc.X + 1, loc.Y + 1, projectile.Width - 2, projectile.Height - 2);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }        
        public void DrawDrop(Graphics g, RPGDrop d)
        {
            #region Draw selection
            if (d.IsSelected == true)
            {
                Pen p3 = new Pen(Brushes.Gold, 2);
                p3.Color = Color.Gold;

                g.DrawEllipse(p3, d.X, d.Y + d.Height + 5, d.Width, 10);
            }
            #endregion

            g.DrawImage(Res.getPic("bag2.gif"), d.Location);
        }

        public void DrawEffect(Graphics g, RPGEffect effect, Actor a)
        {
            #region Setup
            Color c = Color.Black;
            Font f = new Font(FontFamily.GenericMonospace, 9, FontStyle.Bold);
            Brush b = new SolidBrush(c);
            int X = a.Location.X;
            int Y = a.Location.Y - 30; // just over actor's head.
            #endregion

            #region If Buff
            if (effect.EffectIsABuff)
            {
                c = Color.White;
                b = new SolidBrush(c);

                switch (effect.TargetBuff)
                {
                    case RPGEffect.EffectTargetBuff.Attack:
                        {
                            g.DrawString("+Att", f, b, X, Y);
                            break;
                        }
                    case RPGEffect.EffectTargetBuff.Defense:
                        {
                            g.DrawString("+Def", f, b, X, Y);
                            break;
                        }
                    case RPGEffect.EffectTargetBuff.Charisma:
                        {
                            g.DrawString("+Cha", f, b, X, Y);
                            break;
                        }
                    case RPGEffect.EffectTargetBuff.Constitution:
                        {
                            g.DrawString("+Con", f, b, X, Y);
                            break;
                        }
                    case RPGEffect.EffectTargetBuff.Dexterity:
                        {
                            g.DrawString("+Dex", f, b, X, Y);
                            break;
                        }
                    case RPGEffect.EffectTargetBuff.Intelligence:
                        {
                            g.DrawString("+Int", f, b, X, Y);
                            break;
                        }
                    case RPGEffect.EffectTargetBuff.Strength:
                        {
                            g.DrawString("+Str", f, b, X, Y);
                            break;
                        }
                    case RPGEffect.EffectTargetBuff.Wisdom:
                        {
                            g.DrawString("+Wis", f, b, X, Y);
                            break;
                        }
                    case RPGEffect.EffectTargetBuff.RaiseDamage:
                        {
                            g.DrawString("+Dmg", f, b, X, Y);
                            break;
                        }
                    case RPGEffect.EffectTargetBuff.RaiseMaxHP:
                        {
                            g.DrawString("+MHP", f, b, X, Y);
                            break;
                        }
                    case RPGEffect.EffectTargetBuff.RaiseMaxMP:
                        {
                            g.DrawString("+MMP", f, b, X, Y);
                            break;
                        }
                    case RPGEffect.EffectTargetBuff.RestoreHP:
                        {
                            g.DrawString("+HP", f, b, X, Y);
                            break;
                        }
                    case RPGEffect.EffectTargetBuff.RestoreMP:
                        {
                            g.DrawString("+MP", f, b, X, Y);
                            break;
                        }
                    case RPGEffect.EffectTargetBuff.Speed:
                        {
                            g.DrawString("+Spd", f, b, X, Y);
                            break;
                        }
                    default:
                        {
                            g.DrawString("+", f, b, X, Y);
                            break;
                        }
                }
            }
            #endregion

            #region Else Attack
            else
            {
                c = Color.Red;
                b = new SolidBrush(c);

                switch (effect.TargetAttack)
                {
                    case RPGEffect.EffectTargetAttack.DoMagicalDamage:
                    case RPGEffect.EffectTargetAttack.DoPhysicalDamage:
                    case RPGEffect.EffectTargetAttack.DrainCharisma:
                    case RPGEffect.EffectTargetAttack.DrainConstitution:
                    case RPGEffect.EffectTargetAttack.DrainDexterity:
                    case RPGEffect.EffectTargetAttack.DrainIntelligence:
                    case RPGEffect.EffectTargetAttack.DrainMP:
                    case RPGEffect.EffectTargetAttack.DrainSpeed:
                    case RPGEffect.EffectTargetAttack.DrainStrength:
                    case RPGEffect.EffectTargetAttack.DrainWisdom:
                    case RPGEffect.EffectTargetAttack.LowerMaxHP:
                    case RPGEffect.EffectTargetAttack.LowerMaxMP:
                    case RPGEffect.EffectTargetAttack.ReduceAttack:
                    case RPGEffect.EffectTargetAttack.ReduceDamage:
                    case RPGEffect.EffectTargetAttack.ReduceDefense:
                        {
                            g.DrawString("-", f, b, X, Y);
                            break;
                        }
                    default:
                        {
                            g.DrawString("-", f, b, X, Y);
                            break;
                        }
                } // end switch
            } // end else
            #endregion
        } // end DrawEffect
        public void DrawEffect(RPGEffect effect, Point loc)
        {
        }
        public void DrawAnimation(Graphics g, RPGAnimation a)
        {
            // check if we've expired
            if (a.Start + a.Duration < DateTime.Now)
            {
                a.DeleteMe = true;
                return;
            }

            // otherwise, draw self
            switch (a.AnimType)
            {
                case AnimationType.Miss:
                    DrawMiss(g, a);
                    break;
                case AnimationType.Hit_Physical:
                    DrawHitPhysical(g, a);
                    break;
                default:
                    DrawObject(g, a);
                    break;
            }
        }
        private void DrawHitPhysical(Graphics g, RPGAnimation a)
        {
            Pen dmgPen = new Pen(Color.Red);
            dmgPen.Width = 3;
            Point center = new Point(a.X + a.Width / 2, (a.Y + a.Height / 2));

            g.DrawLine(dmgPen, new Point(center.X + 5, center.Y + 5), new Point(center.X - 5, center.Y - 5));
            g.DrawLine(dmgPen, new Point(center.X + 5, center.Y - 5), new Point(center.X - 5, center.Y + 5));
        }
        private void DrawMiss(Graphics g, RPGAnimation a)
        {
            Color c = Color.White;
            Font f = new Font(FontFamily.GenericMonospace, 9, FontStyle.Bold);
            Brush b = new SolidBrush(c);
            Point center = new Point(a.X + a.Width / 2, (a.Y + a.Height / 2));
            // adjust for the width of the word
            center.X -= 20;

            g.DrawString("MISS", f, b, center);
        }
        #endregion

        #region Private methods
        private void DrawRightHandItemEast(Graphics g, Actor a, Point center, Pen PWpn1, Pen PWpn2)
        {
            center.X -= 3;
            center.Y += 3;
            switch (a.inventory.GetWpn().weaponClass)
            {
                #region Axe
                case (RPGWeapon.WeaponClass.Axe):
                    {
                        PWpn1.Width = 2;
                        // base
                        g.DrawLine(PWpn1, center.X + 13, center.Y + 3, 
                                            center.X + 13, center.Y + 20);
                        // head
                        g.DrawLine(PWpn2, center.X + 14, center.Y + 5,
                                            center.X + 14, center.Y + 7);
                        g.DrawLine(PWpn2, center.X + 15, center.Y + 5,
                                            center.X + 15, center.Y + 7);
                        g.DrawLine(PWpn2, center.X + 16, center.Y + 4, 
                                            center.X + 16, center.Y + 8);
                        g.DrawLine(PWpn2, center.X + 17, center.Y + 3,
                                            center.X + 17, center.Y + 9);
                        g.DrawLine(PWpn2, center.X + 18, center.Y + 3,
                                            center.X + 18, center.Y + 12);
                        break;
                    }
                #endregion
                #region Bow
                case (RPGWeapon.WeaponClass.Bow):
                    {
                        PWpn1.Width = 2;
                        // base
                        g.DrawArc(PWpn1, center.X + 7, center.Y - 1, 9, 28, 270, 180);

                        // string
                        g.DrawLine(PWpn2, center.X + 11, center.Y + 0,
                                            center.X + 11, center.Y + 25);
                        break;
                    }
                #endregion
                #region Crossbow
                case (RPGWeapon.WeaponClass.Crossbow):
                    {
                        // base
                        PWpn1.Width = 2;
                        PWpn2.Width = 1;

                        // center shaft
                        g.DrawLine(PWpn1, center.X + 13, center.Y - 6,
                                            center.X + 13, center.Y + 15);
                        g.DrawLine(PWpn2, center.X + 13, center.Y - 5,
                                            center.X + 13, center.Y + 15);
                        // diagonals
                        g.DrawLine(PWpn1, center.X + 7, center.Y + 0,
                                            center.X + 13, center.Y + 12);
                        g.DrawLine(PWpn1, center.X + 20, center.Y + 0,
                                            center.X + 13, center.Y + 12);
                        // bow arc
                        g.DrawArc(PWpn1, center.X + 12 - 20, // left
                                        center.Y -2, // top
                                        40, 60, // width, height
                                        253, 39); // start arc, length of arc.
                        break;
                    }
                #endregion
                #region Dart
                case (RPGWeapon.WeaponClass.Dart):
                    {
                        // base
                        g.DrawLine(PWpn1, center.X + 13, center.Y + 12,
                                            center.X + 13, center.Y + 15);
                        // head
                        g.DrawLine(PWpn2, center.X + 13, center.Y + 9,
                                            center.X + 13, center.Y + 11);

                        break;
                    }
                #endregion
                #region Mace
                case (RPGWeapon.WeaponClass.Mace):
                    {
                        // base
                        g.DrawLine(PWpn1, center.X + 13, center.Y + 3,
                                            center.X + 13, center.Y + 18);
                        // head
                        g.DrawLine(PWpn2, center.X + 11, center.Y + 2,
                                            center.X + 11, center.Y + 2);
                        g.DrawLine(PWpn2, center.X + 12, center.Y + 1,
                                            center.X + 12, center.Y + 3);
                        g.DrawLine(PWpn2, center.X + 13, center.Y - 1 ,
                                            center.X + 13, center.Y + 5);
                        g.DrawLine(PWpn2, center.X + 14, center.Y + 1,
                                            center.X + 14, center.Y + 3);
                        g.DrawLine(PWpn2, center.X + 15, center.Y + 2,
                                            center.X + 15, center.Y + 2);
                        break;
                    }
                #endregion
                #region Sling
                case (RPGWeapon.WeaponClass.Sling):
                    {
                        // base - straps
                        g.DrawLine(PWpn1, center.X + 12, center.Y + 15,
                                            center.X + 13, center.Y + 25);
                        g.DrawLine(PWpn1, center.X + 12, center.Y + 15,
                                            center.X + 11, center.Y + 25);
                        // head - pouch
                        g.DrawLine(PWpn2, center.X + 11, center.Y + 25,
                                            center.X + 13, center.Y + 25);

                        break;
                    }
                #endregion
                #region Staff
                case (RPGWeapon.WeaponClass.Staff):
                    {
                        // base
                        g.DrawLine(PWpn1, center.X + 13, center.Y - 5,
                                            center.X + 13, center.Y + 10);
                        g.DrawLine(PWpn1, center.X + 12, center.Y - 5,
                                            center.X + 12, center.Y + 10);
                        g.DrawLine(PWpn1, center.X + 13, center.Y + 21,
                                            center.X + 13, center.Y + 45);
                        g.DrawLine(PWpn1, center.X + 12, center.Y + 21,
                                            center.X + 12, center.Y + 45);

                        // center handle
                        g.DrawLine(PWpn2, center.X + 12, center.Y + 10,
                                            center.X + 12, center.Y + 20);
                        g.DrawLine(PWpn2, center.X + 13, center.Y + 10,
                                            center.X + 13, center.Y + 20);
                        break;
                    }
                #endregion
                #region Sword
                case (RPGWeapon.WeaponClass.Sword):
                    {
                        // base
                        g.DrawLine(PWpn1, center.X + 13, center.Y + 11,
                                            center.X + 13, center.Y + 16);
                        g.DrawLine(PWpn1, center.X + 10, center.Y + 10,
                                            center.X + 16, center.Y + 10);
                        // head
                        g.DrawLine(PWpn2, center.X + 12, center.Y - 1,
                                            center.X + 12, center.Y + 10);
                        g.DrawLine(PWpn2, center.X + 13, center.Y - 3,
                                            center.X + 13, center.Y + 10);
                        g.DrawLine(PWpn2, center.X + 14, center.Y - 1,
                                            center.X + 14, center.Y + 10);
                        break;
                    }
                #endregion
                #region ThrowingAxe
                case (RPGWeapon.WeaponClass.ThrowingAxe):
                    {
                        // base
                        g.DrawLine(PWpn1, center.X + 13, center.Y + 3,
                                            center.X + 13, center.Y + 18);
                        // head
                        g.DrawLine(PWpn2, center.X + 14, center.Y + 5,
                                            center.X + 14, center.Y + 7);
                        g.DrawLine(PWpn2, center.X + 15, center.Y + 5,
                                            center.X + 15, center.Y + 7);
                        g.DrawLine(PWpn2, center.X + 16, center.Y + 4,
                                            center.X + 16, center.Y + 8);
                        g.DrawLine(PWpn2, center.X + 17, center.Y + 3,
                                            center.X + 17, center.Y + 9);
                        break;
                    }
                #endregion
                default:
                    {
                        break;
                    }
            } // end wpn
        }
        private void DrawRightHandItemWest(Graphics g, Actor a, Point center, Pen PWpn1, Pen PWpn2)
        {
            if (a.inventory.GetWpn() == null) { return; }
            center.X += 3;
            center.Y += 3;

            // draw weapon
            #region Weapon
            switch (a.inventory.GetWpn().weaponClass)
            {
                case (RPGWeapon.WeaponClass.Axe):
                    {
                        PWpn1.Width = 2;
                        // base
                        g.DrawLine(PWpn1, center.X - 13, center.Y + 3,
                                            center.X - 13, center.Y + 20);
                        // head
                        g.DrawLine(PWpn2, center.X - 14, center.Y + 5,
                                            center.X - 14, center.Y + 7);
                        g.DrawLine(PWpn2, center.X - 15, center.Y + 5,
                                            center.X - 15, center.Y + 7);
                        g.DrawLine(PWpn2, center.X - 16, center.Y + 4,
                                            center.X - 16, center.Y + 8);
                        g.DrawLine(PWpn2, center.X - 17, center.Y + 3,
                                            center.X - 17, center.Y + 9);
                        g.DrawLine(PWpn2, center.X - 18, center.Y + 3,
                                            center.X - 18, center.Y + 12);
                        break;
                    }
                case (RPGWeapon.WeaponClass.Bow):
                    {
                        PWpn1.Width = 2;
                        // base
                        g.DrawArc(PWpn1, center.X - 14, center.Y - 1, 9, 28, 270, -180);

                        // string
                        g.DrawLine(PWpn2, center.X - 11, center.Y + 0,
                                            center.X - 11, center.Y + 25);
                        break;
                    }
                case (RPGWeapon.WeaponClass.Crossbow):
                    {
                        // base
                        PWpn1.Width = 2;
                        PWpn2.Width = 1;

                        // center shaft
                        g.DrawLine(PWpn1, center.X - 13, center.Y - 6,
                                            center.X - 13, center.Y + 15);
                        g.DrawLine(PWpn2, center.X - 13, center.Y - 5,
                                            center.X - 13, center.Y + 15);
                        // diagonals
                        g.DrawLine(PWpn1, center.X - 7, center.Y + 0,
                                            center.X - 13, center.Y + 12);
                        g.DrawLine(PWpn1, center.X - 20, center.Y + 0,
                                            center.X - 13, center.Y + 12);
                        // bow arc
                        g.DrawArc(PWpn1, center.X - 12 - 20, // left
                                        center.Y - 2, // top
                                        40, 60, // width, height
                                        250, 39); // start arc, length of arc.
                        break;
                    }
                case (RPGWeapon.WeaponClass.Dart):
                    {
                        // base
                        g.DrawLine(PWpn1, center.X - 13, center.Y + 12,
                                            center.X - 13, center.Y + 15);
                        // head
                        g.DrawLine(PWpn2, center.X - 13, center.Y + 9,
                                            center.X - 13, center.Y + 11);

                        break;
                    }
                case (RPGWeapon.WeaponClass.Mace):
                    {
                        // base
                        g.DrawLine(PWpn1, center.X - 13, center.Y + 3,
                                            center.X - 13, center.Y + 18);
                        // head
                        g.DrawLine(PWpn2, center.X - 11, center.Y + 2,
                                            center.X - 11, center.Y + 2);
                        g.DrawLine(PWpn2, center.X - 12, center.Y + 1,
                                            center.X - 12, center.Y + 3);
                        g.DrawLine(PWpn2, center.X - 13, center.Y - 1,
                                            center.X - 13, center.Y + 5);
                        g.DrawLine(PWpn2, center.X - 14, center.Y + 1,
                                            center.X - 14, center.Y + 3);
                        g.DrawLine(PWpn2, center.X - 15, center.Y + 2,
                                            center.X - 15, center.Y + 2);
                        break;
                    }
                case (RPGWeapon.WeaponClass.Sling):
                    {
                        // base - straps
                        g.DrawLine(PWpn1, center.X - 12, center.Y + 15,
                                            center.X - 13, center.Y + 25);
                        g.DrawLine(PWpn1, center.X - 12, center.Y + 15,
                                            center.X - 11, center.Y + 25);
                        // head - pouch
                        g.DrawLine(PWpn2, center.X - 11, center.Y + 25,
                                            center.X - 13, center.Y + 25);

                        break;
                    }
                case (RPGWeapon.WeaponClass.Staff):
                    {
                        // base
                        g.DrawLine(PWpn1, center.X - 13, center.Y - 5,
                                            center.X - 13, center.Y + 10);
                        g.DrawLine(PWpn1, center.X - 12, center.Y - 5,
                                            center.X - 12, center.Y + 10);
                        g.DrawLine(PWpn1, center.X - 13, center.Y + 21,
                                            center.X - 13, center.Y + 45);
                        g.DrawLine(PWpn1, center.X - 12, center.Y + 21,
                                            center.X - 12, center.Y + 45);

                        // center handle
                        g.DrawLine(PWpn2, center.X - 12, center.Y + 10,
                                            center.X - 12, center.Y + 20);
                        g.DrawLine(PWpn2, center.X - 13, center.Y + 10,
                                            center.X - 13, center.Y + 20);
                        break;
                    }
                case (RPGWeapon.WeaponClass.Sword):
                    {
                        // base
                        g.DrawLine(PWpn1, center.X - 13, center.Y + 11,
                                            center.X - 13, center.Y + 16);
                        g.DrawLine(PWpn1, center.X - 10, center.Y + 10,
                                            center.X - 16, center.Y + 10);
                        // head
                        g.DrawLine(PWpn2, center.X - 12, center.Y - 1,
                                            center.X - 12, center.Y + 10);
                        g.DrawLine(PWpn2, center.X - 13, center.Y - 3,
                                            center.X - 13, center.Y + 10);
                        g.DrawLine(PWpn2, center.X - 14, center.Y - 1,
                                            center.X - 14, center.Y + 10);
                        break;
                    }
                case (RPGWeapon.WeaponClass.ThrowingAxe):
                    {
                        // base
                        g.DrawLine(PWpn1, center.X - 13, center.Y + 3,
                                            center.X - 13, center.Y + 18);
                        // head
                        g.DrawLine(PWpn2, center.X - 14, center.Y + 5,
                                            center.X - 14, center.Y + 7);
                        g.DrawLine(PWpn2, center.X - 15, center.Y + 5,
                                            center.X - 15, center.Y + 7);
                        g.DrawLine(PWpn2, center.X - 16, center.Y + 4,
                                            center.X - 16, center.Y + 8);
                        g.DrawLine(PWpn2, center.X - 17, center.Y + 3,
                                            center.X - 17, center.Y + 9);
                        break;
                    }
                default:
                    {
                        break;
                    }
            } // end wpn
            #endregion
        }
        #endregion
    }
}
