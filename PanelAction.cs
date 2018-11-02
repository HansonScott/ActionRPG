using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace RPG
{
    /// <summary>
    /// The visual playing field of the game
    /// </summary>
    public class PanelAction: Panel
    {
        #region Declarations
        public static int PANEL_WIDTH = 992;
        public static int PANEL_HEIGHT = 585;

        public RPGObject SelectedObject;
        private PanelActionToolbar m_toolbar;
        #endregion

        #region Constructor
        public PanelAction(PanelActionToolbar toolbar)
        {
            m_toolbar = toolbar;
            this.MouseClick += new MouseEventHandler(PanelAction_MouseClick);
        }

        #endregion

        #region UI Events
        void PanelAction_MouseClick(object sender, MouseEventArgs e)
        {
            RPGObject target = Session.thisSession.thisArea.GetObjectAt(e.Location);
            if (target == null)
            {
                // user clicked the ground
                if (e.Button == MouseButtons.Right)
                {
                    LocationRightClick(e.Location);
                }
                else
                {
                    LocationClick(e.Location);
                }
            }
            else
            {
                // then user selected this object
                if (e.Button == MouseButtons.Right)
                {
                    ObjectRightClick(target);
                }
                else
                {
                    ObjectClick(target);
                }
            }
        }
        #endregion

        #region Medium level events
        private void LocationClick(Point loc)
        {
            if (SelectedObject == null)
            {
                // then user has chosen to click on the ground for some reason...
                GroundClicked(loc);
            }
            else
            {
                // then user has selected the ground, unselect our current object
                SelectedObject.IsSelected = false;
                SelectedObject = null;
            }
        }
        private void LocationRightClick(Point loc)
        {
            // clear selected object
            if (SelectedObject != null)
            {
                TargetGround(loc);
            }
        }
        private void ObjectClick(RPGObject obj)
        {
            if (SelectedObject != null)
            {
                if (SelectedObject != obj)
                {
                    // unselect old object
                    SelectedObject.IsSelected = false;

                    // clicked a new object
                    obj.IsSelected = true;
                    SelectedObject = obj;
                    
                    // update toolbar based on selection
                    if (obj.GetType() == typeof(PlayerCharacter))
                    {
                        Session.thisSession.LoadActorToTabs((Actor)obj);

                        // any PC-specific things...
                    }
                    else if (obj.GetType() == typeof(Actor))
                    {
                        Session.thisSession.LoadActorToTabs((Actor)obj);
                    }
                    else if (obj.GetType() == typeof(RPGDrop))
                    {
                        Session.thisSession.TabPageAction.panelActionToolbar.LoadDrop((RPGDrop)obj);
                    }
                    else
                    {
                        // we have selected an unknown object, do nothing...
                    }
                }
                else
                {
                    // clicked on the same object twice,
                }
            }
            else
            {
                // new selection
                SelectedObject = obj;
                SelectedObject.IsSelected = true;

                // update toolbar based on selection
                if (obj.GetType() == typeof(PlayerCharacter))
                {
                    Session.thisSession.LoadActorToTabs((Actor)obj);
                    // load any pc-specific things...
                }
                else if (obj.GetType() == typeof(Actor))
                {
                    Session.thisSession.LoadActorToTabs((Actor)obj);
                }
                else if (obj.GetType() == typeof(RPGDrop))
                {
                    Session.thisSession.TabPageAction.panelActionToolbar.LoadDrop((RPGDrop)obj);
                }
                else
                {
                    // we have selected an object not an actor, do nothing...
                }
            }
        }
        private void ObjectRightClick(RPGObject obj)
        {
            if (SelectedObject == null)
            {
                // right click something first?
            }
            else
            {
                //if (SelectedObject != obj)
                //{
                //    // right clicked a different object
                TargetObject(obj);
                //}
                //else
                //{
                //    // right clicked same object again?
                //}
            }
        }
        #endregion

        #region High level events
        private void GroundClicked(Point loc)
        {
        }
        private void TargetGround(Point loc)
        {
            // check the ActionButtons
            if (m_toolbar.isAnyButtonSelected())
            {
                ActionButton[] btns = m_toolbar.GetSelectedButtons();
                if (btns.Length == 1)
                {
                    // only one selected, do action of button.
                    ActionButton btn = btns[0];
                    switch (btn.ActionType)
                    {
                        case (ActionButton.ActionButtonType.BasicAction):
                            {
                                TargetGroundWithBasicAction(btn, loc);
                                break;
                            }
                        case (ActionButton.ActionButtonType.SkillAction):
                            {
                                TargetGroundWithSkillAction(btn, loc);
                                break;
                            }
                        case (ActionButton.ActionButtonType.SpellAction):
                            {
                                TargetGroundWithSpellAction(btn, loc);
                                break;
                            }
                        case (ActionButton.ActionButtonType.ItemAction):
                            {
                                TargetGroundWithItemAction(btn, loc);
                                break;
                            }
                        default:
                            {
                                break;
                            }

                    } // end switch
                }
            }
            else
            {
                // if no buttons pressed, the default is to move.
                SelectedObject.Act(RPGObject.Action.Move, loc, null);
            }
        }
        private void TargetGroundWithBasicAction(ActionButton btn, Point loc)
        {
            // do the button action to the target
            MessageBox.Show("Not done yet.");
        }
        private void TargetGroundWithSkillAction(ActionButton btn, Point loc)
        {
            // do the button action to the target
            MessageBox.Show("Not done yet.");
        }
        private void TargetGroundWithSpellAction(ActionButton btn, Point loc)
        {
            // do the button action to the target
            MessageBox.Show("Not done yet.");
        }
        private void TargetGroundWithItemAction(ActionButton btn, Point loc)
        {
            // use the button item on this location
            foreach (RPGEffect effect in btn.Item.Effects)
            {
                if (effect == null)
                {
                    continue;
                }

                // go through all possible targets and see if each applies.
                bool targetFound = false;
                foreach (RPGObject target in Session.thisSession.thisArea.GetObjects())
                {
                    if (effect.IsCorrectTarget(SelectedObject, target, loc))
                    {
                        // need to use a copy constructor - one for each actor!
                        if (targetFound == false)
                        {
                            targetFound = true;
                            Session.Print((SelectedObject as Actor).Name + " used " + btn.Item.Name);
                        }
                        target.AddEffect(new RPGEffect(effect));
                    } // end if good target
                }

                // if we found a target, or we used it on an open space, then use up the item.
                if (targetFound 
                    || btn.Item.Effects[0].Range == RPGEffect.EffectRange.Area 
                    || btn.Item.Effects[0].Range == RPGEffect.EffectRange.TargetArea)
                {
                    // assuming a potion for now, the item gets consumed.
                    (SelectedObject as Actor).inventory.RemoveQuickItem(btn.Item);
                    btn.ClearItem();
                }
                else
                {
                    Session.Print((SelectedObject as Actor).Name + " did not use " + btn.Item.Name + ", no target found");
                }
            }
        }
        private void TargetObject(RPGObject target)
        {
            // object is selected, and a new target is right-clicked
            // check ActionButtons
            if (m_toolbar.isAnyButtonSelected())
            {
                ActionButton[] btns = m_toolbar.GetSelectedButtons();
                if (btns.Length == 1)
                {
                    // only one selected, do action of button.
                    ActionButton btn = btns[0];
                    switch (btn.ActionType)
                    {
                        case (ActionButton.ActionButtonType.BasicAction):
                            {
                                TargetObjectWithBasicAction(btn, target);
                                break;
                            }
                        case (ActionButton.ActionButtonType.SkillAction):
                            {
                                TargetObjectWithSkillAction(btn, target);
                                break;
                            }
                        case (ActionButton.ActionButtonType.SpellAction):
                            {
                                TargetObjectWithSpellAction(btn, target);
                                break;
                            }
                        case (ActionButton.ActionButtonType.ItemAction):
                            {
                                TargetObjectWithItemAction(btn, target);
                                break;
                            }
                        default:
                            {
                                break;
                            }

                    } // end switch
                }
                else
                {
                    // multiple buttons selected, do complex logic.
                }
            }
            else
            {

                if (target == SelectedObject)
                {
                    // right clicked on same selected item, do anything?
                    // No
                    return;
                }

                // if no action button was selected, then do the default...
                // based on friend/foe/neutral, do default action...
                if (target.GetType() == typeof(Actor))
                {
                    SelectedObject.Act(RPGObject.Action.Attack, new Point(target.X, target.Y), target);
                }
                else if (target.GetType() == typeof(PlayerCharacter))
                {
                    SelectedObject.Act(RPGObject.Action.Attack, new Point(target.X, target.Y), target);
                }
                else if (target.GetType() == typeof(RPGDrop))
                {
                    SelectedObject.Act(RPGObject.Action.Get, new Point(target.X, target.Y), target);
                }
            } // end else - no ActionButton selected
        }
        private void TargetObjectWithBasicAction(ActionButton btn, RPGObject target)
        {
            // do the button action to the target
            MessageBox.Show("Not done yet.");
        }
        private void TargetObjectWithSkillAction(ActionButton btn, RPGObject target)
        {
            // perform the button skill on the target
            MessageBox.Show("Not done yet.");
        }
        private void TargetObjectWithSpellAction(ActionButton btn, RPGObject target)
        {
            // cast the button spell on the target
            MessageBox.Show("Not done yet.");
        }
        private void TargetObjectWithItemAction(ActionButton btn, RPGObject target)
        {
            TargetGroundWithItemAction(btn, target.Location);

            // use the button item on the target
            // do the effects.
            //foreach (RPGEffect effect in btn.Item.Effects)
            //{
            //    if (effect == null)
            //    {
            //        continue;
            //    }

            //    // make sure the target matches the requirements
            //    if (effect.IsCorrectTarget(SelectedObject, target))
            //    {
            //        target.AddEffect(effect);
            //        Session.Print((SelectedObject as Actor).Name + " used " + btn.Item.Name);

            //        // assuming a potion for now, the item gets consumed.
            //        (SelectedObject as Actor).inventory.RemoveQuickItem(btn.Item);
            //        btn.ClearItem();
            //    } // end if good target
            //}
        }

        #endregion

        #region private methods
        #endregion
    }
}
