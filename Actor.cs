using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace RPG
{
    public class Actor: RPGObject
    {
        #region Public Declarations
        public const int ATTRIBUTE_COUNT = 6;
        public const int ATTRIBUTE_DEFAULT = 10;

        public enum Attribute
        {
            Strength = 0,
            Dexterity = 1,
            Constitution = 2,
            Intelligence = 3,
            Wisdom = 4,
            Charisma = 5
        }
        public enum Alignment
        {
            Lawful_Good = 0,
            Lawful_Neutral = 1,
            Lawful_Evil = 2,
            Neutral_Good = 3,
            True_Neutral = 4,
            Neutral_Evil = 5,
            Chaotic_Good = 6,
            Chaotic_Neutral = 7,
            Chaotic_Evil = 8
        }
        public enum Gender
        {
            Male = 0,
            Female = 1
        }
        public enum RelationToPC
        {
            Is = 0,
            Party = 1,
            Ally = 2,
            Neutral = 3,
            Enemy = 4
        }
        #endregion

        #region Member Declarations
        private int[] m_Base; // attributes
        private int[] m_Current; // attributes
        private bool m_IsFemale;
        private Alignment m_Alignment;

        private int m_baseMaxHP;
        private int m_currentMaxHP;
        private int m_currentHP;

        private int m_baseMaxMP;
        private int m_currentMaxMP;
        private int m_currentMP;

        private int m_baseAttack;
        private int m_CurrentAttack;
        private int m_baseDefense;
        private int m_CurrentDefense;
        private int m_baseDamage;

        public Inventory inventory;

        public DateTime lastAttack;
        public DateTime lastDamage;
        public RelationToPC Relation;

        public ArtificialIntelligence AI;
        public bool AIActive;
        public ActionStates States;

        private int m_LOSRange;

        private int m_Exp;
        private int m_Lvl;
        private bool m_ReadyToLevelUp = false;

        private int m_ExpForKill;

        private RPGSpellBook m_SpellBook;
        private RPGSpellBook m_QuickBook;
        #endregion

        #region Constructor
        public Actor()
        {
            SetAttributesToDefault();

            this.HPBaseMax = new RPGCalc().GetBaseHP(this);
            this.MPBaseMax = new RPGCalc().GetBaseMP(this);

            Name = "Joseph";

            BaseSpeed = RPGCalc.DEFAULT_SPEED;
            CurrentSpeed = BaseSpeed;
            
            m_baseAttack = RPGCalc.DEFAULT_BASE_ATTACK;
            m_baseDefense = RPGCalc.DEFAULT_BASE_DEFENSE;
            m_baseDamage = RPGCalc.DEFAULT_BASE_DAMAGE;
            
            LOSRange = RPGCalc.DEFAULT_LOS_RANGE;

            inventory = new Inventory(this);

            m_SpellBook = new RPGSpellBook();
            m_QuickBook = new RPGSpellBook(3);

            lastAttack = System.DateTime.Now;
            lastDamage = new DateTime();

            this.ImpedesWalking = true;

            Relation = RelationToPC.Neutral;

            RPGCalc calc = new RPGCalc();

            m_Lvl = 1;
            m_ExpForKill = calc.GetExpForKill(this);

            AI = new ArtificialIntelligence(this);
            //AIActive = false; // enemies don't respond.
            AIActive = true;

            States = new ActionStates(this);

            // use all we have so far to calc complex numbers (HP/MP/Att/Def, etc);
            this.HPBaseMax = calc.GetBaseHP(this);
            this.MPBaseMax = calc.GetBaseMP(this);
            this.ResetStats();
        }
        #endregion

        #region Attribute Properties
        public int[] BaseAttributes
        {
            get { return m_Base; }
            set { m_Base = value; }
        }
        public int BaseStrength
        {
            get { return m_Base[(int)Attribute.Strength]; }
            set { m_Base[(int)Attribute.Strength] = value; }
        }
        public int BaseDexterity
        {
            get { return m_Base[(int)Attribute.Dexterity]; }
            set { m_Base[(int)Attribute.Dexterity] = value; }
        }
        public int BaseConstitution
        {
            get { return m_Base[(int)Attribute.Constitution]; }
            set { m_Base[(int)Attribute.Constitution] = value; }
        }
        public int BaseIntelligence
        {
            get { return m_Base[(int)Attribute.Intelligence]; }
            set { m_Base[(int)Attribute.Intelligence] = value; }
        }
        public int BaseWisdom
        {
            get { return m_Base[(int)Attribute.Wisdom]; }
            set { m_Base[(int)Attribute.Wisdom] = value; }
        }
        public int BaseCharisma
        {
            get { return m_Base[(int)Attribute.Charisma]; }
            set { m_Base[(int)Attribute.Charisma] = value; }
        }

        public void ResetAttributes()
        {
            this.CurrentStrength = this.BaseStrength;
            this.CurrentDexterity = this.BaseDexterity;
            this.CurrentConstitution = this.BaseConstitution;
            this.CurrentIntelligence = this.BaseIntelligence;
            this.CurrentWisdom = this.BaseWisdom;
            this.CurrentCharisma = this.BaseCharisma;
        }

        public int CurrentStrength
        {
            get { return m_Current[(int)Attribute.Strength]; }
            set { m_Current[(int)Attribute.Strength] = value; }
        }
        public int CurrentDexterity
        {
            get { return m_Current[(int)Attribute.Dexterity]; }
            set { m_Current[(int)Attribute.Dexterity] = value; }
        }
        public int CurrentConstitution
        {
            get { return m_Current[(int)Attribute.Constitution]; }
            set { m_Current[(int)Attribute.Constitution] = value; }
        }
        public int CurrentIntelligence
        {
            get { return m_Current[(int)Attribute.Intelligence]; }
            set { m_Current[(int)Attribute.Intelligence] = value; }
        }
        public int CurrentWisdom
        {
            get { return m_Current[(int)Attribute.Wisdom]; }
            set { m_Current[(int)Attribute.Wisdom] = value; }
        }
        public int CurrentCharisma
        {
            get { return m_Current[(int)Attribute.Charisma]; }
            set { m_Current[(int)Attribute.Charisma] = value; }
        }
        #endregion

        #region Other Properties
        public bool IsFemale
        {
            get { return m_IsFemale; }
            set { m_IsFemale = value; }
        }
        public string GenderAsString
        {
            get 
            {
                if (m_IsFemale)
                {
                    return "Female";
                }
                else
                {
                    return "Male";
                }
            }
            set
            {
                if (value == "Female")
                {
                    m_IsFemale = true;
                }
                else
                {
                    m_IsFemale = false;
                }
            }
        }
        public Alignment GetAlignment()
        {
            return m_Alignment;
        }
        public void SetAlignment(int i)
        {
            Alignment a = (Actor.Alignment)Enum.Parse(typeof(Actor.Alignment), i.ToString());
            SetAlignment(a);
        }
        public void SetAlignment(Alignment a)
        {
            m_Alignment = a;

            // now change color of actor to fit alignment...
        }
        public string AlignmentAsString
        {
            get { return Enum.GetName(typeof(Actor.Alignment), m_Alignment); }
        }

        /// <summary>
        ///  resest all current stats as if the actor slept or teleported, etc.
        /// </summary>
        public void ResetStats()
        {
            HPResetMax();
            HPResetCurrent();

            MPResetMax();
            MPResetCurrent();

            UpdateAttack();
            UpdateDefense();
        }

        public int HPBaseMax
        {
            get { return m_baseMaxHP; }
            set { m_baseMaxHP = value; }
        }
        public int HPCurrentMax
        {
            // read only because complex, use Update to set.
            get { return m_currentMaxHP; }
            //set { m_currentmaxHP = value; }
        }
        public int HPCurrent
        {
            // read only because complex, use Update to set.
            get { return m_currentHP; }
            //set { m_currentHP = value; }
        }
        public void HPAdjustCurrent(int change)
        {
            m_currentHP += change;
        }
        public void HPAdjustCurrentMax(int change)
        {
            m_currentMaxHP += change;
        }
        public void HPResetMax()
        {
            m_currentMaxHP = m_baseMaxHP;
        }
        public void HPResetCurrent()
        {
            m_currentHP = m_currentMaxHP;
        }

        public int MPBaseMax
        {
            get { return m_baseMaxMP; }
            set { m_baseMaxMP = value; }
        }
        public int MPCurrentMax
        {
            // read only because complex, use UpdateMP()
            get { return m_currentMaxMP; }
            //set { m_currentMaxMP = value; }
        }
        public int MPCurrent
        {
            // read only because complex, use UpdateMP()
            get { return m_currentMP; }
            //set { m_currentMP = value; }
        }
        public void MPAdjustCurrent(int change)
        {
            m_currentMP += change;
        }
        public void MPAdjustCurrentMax(int change)
        {
            m_currentMaxMP += change;
        }
        public void MPResetMax()
        {
            m_currentMaxMP = m_baseMaxMP;
        }
        public void MPResetCurrent()
        {
            m_currentMP = m_currentMaxMP;
        }

        public int BaseAttack 
        {
            get { return m_baseAttack; }
            set 
            { 
                m_baseAttack = value;
                UpdateAttack();
            }
        }
        public int CurrentAttack
        {
            // NOTE: read-only because it is a complex number
            // that requires calculation - use UpdateAttack() to recalculate
            get { return m_CurrentAttack; }
        }
        public int UpdateAttack()
        {
            // calculate this actor's attack,
            m_CurrentAttack = new RPGCalc().GetActorCurrentAttack(this);

            // return the value;
            return m_CurrentAttack;
        }

        public int BaseDefense
        {
            get { return m_baseDefense; }
            set 
            { 
                m_baseDefense = value;
                UpdateDefense();
            }
        }
        public int CurrentDefense
        {
            // NOTE: read-only because it is a complex number
            // that requires calculation - use UpdateDefense() to recalculate
            get { return m_CurrentDefense; }
        }
        public int UpdateDefense()
        {
            // calculate this actor's defense,
            m_CurrentDefense = new RPGCalc().GetActorCurrentDefense(this);

            // return the value;
            return m_CurrentDefense;
        }

        public int BaseDamage
        {
            get { return m_baseDamage; }
            set { m_baseDamage = value; }
        }

        public int Experience
        {
            get { return m_Exp; }
        }
        public void ExpAdjust(int change)
        {
            m_Exp += change;

            // notify
            if (change > 0)
            {
                Session.Print(this.Name + " has gained " + change + " experience.");
            }
            else
            {
                Session.Print(this.Name + " has lost " + change + " experience.");
            }

            // check for lvl up.
            if (ReadyToLevelUp == false)
            {
                if (new RPGCalc().CheckXPForLevel(this.m_Exp, this.m_Lvl))
                {
                    // then we just attained enough xp to lvl up.
                    this.m_ReadyToLevelUp = true;

                    // this is the event, so make some noise or notification, or something.
                    Session.Print(this.Name + " has leveled up!");
                }
            }
        }
        public int Level
        {
            get { return m_Lvl; }
            set
            {
                m_Lvl = value;
                m_ExpForKill = new RPGCalc().GetExpForKill(this);

            }

        }

        public bool ReadyToLevelUp
        {
            get { return m_ReadyToLevelUp; }
            set { m_ReadyToLevelUp = value; }
        }

        public int ExpForKill
        {
            get { return m_ExpForKill; }
        }

        public int LOSRange
        {
            get { return m_LOSRange; }
            set { m_LOSRange = value; }
        }

        public RPGSpellBook SpellBook
        {
            get { return m_SpellBook; }
            set { m_SpellBook = value; }
        }

        public RPGSpellBook QuickSpellBook
        {
            get { return m_QuickBook; }
            set { m_QuickBook = value; }
        }
        #endregion

        #region Events
        public override void DrawSelf(Graphics g)
        {
            new RPGDraw().DrawActor(g, this);
        }
        public override void UpdateSelf()
        {
            if (this.currentState == ActionState.Dying)
            {
                this.Actions.Clear();
                this.currentAction = null;

                // do any effects that triger 'on death'...

                // check if we've been dying long enough
                if (System.DateTime.Now.CompareTo(lastDamage.AddMilliseconds(new RPGDraw().DAMAGE_DURATION)) > 0)
                {
                    this.DeleteMe = true;
                }
            }
            else
            {
                // before the actor can do anything, do any effects
                CheckEffects();

                // double check our priorities periodically
                if (System.DateTime.Now.CompareTo(this.AI.LastUpdate.AddMilliseconds(this.AI.AIUpdateDelay)) > 0)
                {
                    if (this.AIActive)
                    {
                        this.AI.DecideWhatToDo();
                    }
                }

                #region Do Current Action
                if (currentAction != null && currentAction.Accomplished == false)
                {
                    // then do action.
                    switch (currentAction.type)
                    {
                        #region Case: Walk
                        case (RPGAction.ActionType.Walk):
                            {
                                // see if action has been accomplished
                                bool Near;
                                if (currentAction.target != null)
                                {
                                    Near = new RPGCalc().ActorStandingNearPoint(this, currentAction.target.Location);
                                }
                                else
                                {
                                    Near = new RPGCalc().ActorStandingNearPoint(this, currentAction.destination);
                                }
                                if (Near)
                                {
                                    currentAction.Accomplished = true;
                                    this.currentState = ActionState.Standing;
                                    StopMoving();
                                }
                                else
                                {
                                    // try to walk towards target.

                                    // Pathing -check for obstacles and plan to go around

                                    if (currentAction.NeedsUpdating == true)
                                    {
                                        if (currentAction.target != null)
                                        {
                                            CalculateMovement(currentAction.target.Location);
                                        }
                                        else
                                        {
                                            CalculateMovement(currentAction.destination);
                                        }
                                        currentAction.NeedsUpdating = false;
                                    }
                                    else
                                    {
                                        currentAction.Check(); // maybe needs updating next time...
                                    }

                                    if (Move() == false)
                                    {
                                        // stopped moving because we hit something,
                                        currentAction.Accomplished = true;
                                        this.currentState = ActionState.Standing;
                                        StopMoving();
                                        currentAction = null;
                                    }
                                    else
                                    {
                                        // we took a step, so turn toward direction walking
                                        Turn(currentAction.destination);
                                    }
                                }
                                break;
                            }
                        #endregion

                        #region Case: Attack
                        case (RPGAction.ActionType.Attack):
                            {
                                // if target it dead or dying
                                if (currentAction.target == null ||
                                    currentAction.target.currentState == ActionState.Dying)
                                {
                                    currentAction.Accomplished = true;

                                    // stop attacking
                                    this.currentState = ActionState.Standing;
                                }
                                else
                                {
                                    // turn toward target
                                    Turn(currentAction.target.Location);

                                    // try to attack - check if in range
                                    int dist = new RPGCalc().DistanceBetween(this, currentAction.target);

                                    if (this.inventory.GetWpn() == null && dist <= 30)
                                    {
                                        AttemptToEngageTarget();                                        
                                    }
                                    else if(this.inventory.GetWpn() != null 
                                        && dist <= this.inventory.GetWpn().Range)
                                    {
                                        AttemptToEngageTarget();                                        
                                    } // if dist ok
                                    else
                                    {
                                        // add action to attack to front of list
                                        Actions.AddFirst(currentAction);

                                        // add action to walk to target to front of list. (before attack);
                                        RPGAction walk = new RPGAction(RPGAction.ActionType.Walk, currentAction.target);

                                        if (this.inventory.GetWpn() == null 
                                            || this.inventory.GetWpn().weaponType == RPGWeapon.WeaponType.Melee)
                                        {
                                            walk.target = currentAction.target;
                                        }
                                        else
                                        {
                                            walk.destination = new RPGCalc().PointToBeInRange(this, currentAction.target);
                                        }
                                        walk.UpdateFrequency = 100; // update really often

                                        Actions.AddFirst(walk);

                                        // CRUCIAL: switch to walking, since target out of range.
                                        currentAction = walk;
                                    }
                                }
                                break;
                            }
                        #endregion

                        #region Case: Get
                        case (RPGAction.ActionType.Get):
                            {
                                // if target is gone
                                if (currentAction.target == null)
                                {
                                    currentAction.Accomplished = true;

                                    // stop getting
                                    this.currentState = ActionState.Standing;
                                }
                                else
                                {
                                    // turn toward target
                                    Turn(currentAction.target.Location);

                                    // try to get - check if in range
                                    int dist = new RPGCalc().DistanceBetween(this, currentAction.target);

                                    if (dist <= 40) // arbitrary reach of actor
                                    {
                                        this.inventory.AddItem(currentAction.target);
                                        Session.thisSession.thisArea.RemoveObject(currentAction.target);
                                        currentAction.Accomplished = true;
                                        Session.Print(this.Name + " picked up something.");
                                    } // if dist ok
                                    else
                                    {
                                        // add get action to front of list
                                        Actions.AddFirst(currentAction);

                                        // add action to walk to target to front of list. (before the get);
                                        RPGAction walk = new RPGAction(RPGAction.ActionType.Walk, currentAction.target);

                                        walk.UpdateFrequency = 500; // update not too often because it is a get action

                                        Actions.AddFirst(walk);

                                        // CRUCIAL: switch to walking, since target out of range.
                                        currentAction = walk;
                                    }
                                } break;
                            }
                        #endregion
                        default:
                            {
                                break;
                            }
                    } // end switch
                }// end if currentAction
                #endregion
                
                #region or GetNextAction
                else
                {
                    currentAction = Actions.GetNextAction();

                    // if nothing in queue
                    if (currentAction == null)
                    {
                        // check object's AI to determine what to do next.
                    }
                }
                #endregion
            } // end if still alive
        }
        public void DamageMe(int dmg)
        {
            this.HPAdjustCurrent(-dmg);
            string msg = "" + this.Name + " takes " + dmg + " damage.";
            Session.Print(msg);
            lastDamage = System.DateTime.Now;

            RPGAnimation hit = new RPGAnimation(AnimationType.Hit_Physical);
            hit.X = this.X;
            hit.Y = this.Y + 1;
            Session.thisSession.thisArea.AddObject(hit);

            // then check for death
            if (this.HPCurrent <= 0)
            {
                ApplyDeath();
            }
        }
        public void DamageMe(RPGDamage dmg)
        {
            // for now, just go to the easy one...
            DamageMe(dmg.Physical);

            // go through damage object and apply all damages that apply to this actor.
            
            // then check for death
            //if (this.HPCurrent <= 0)
            //{
            //    ApplyDeath();
            //}
        }
        public void MissedMe()
        {
            RPGAnimation miss = new RPGAnimation(AnimationType.Miss);
            miss.X = this.X;
            miss.Y = this.Y + 1;
            Session.thisSession.thisArea.AddObject(miss);
        }
        #endregion

        #region Private Methods
        private void CheckEffects()
        {
            for (int i = 0; i < Effects.Length; i++)
            {
                if (Effects[i] != null)
                {
                    RPGEffect effect = Effects[i];

                    // check to activate effect now
                    if (effect.Status == RPGEffect.EffectStatus.Ready)
                    {
                        // decide if we should activate it right now.
                        if (effect.Trigger == RPGEffect.EffectTrigger.Immediately)
                        {
                            effect.Status = RPGEffect.EffectStatus.ActiveToDo;
                        }

                        // Other triggers should have their status set as active at the event.
                    }

                    // once activeToDo or activeDone
                    if (effect.IsActive)
                    {
                        // do effect
                        if (effect.Status == RPGEffect.EffectStatus.ActiveToDo)
                        {
                            effect.Resume(); // sets the start time.
                            effect.ApplyToTarget(this);
                            effect.Status = RPGEffect.EffectStatus.ActiveDone;
                        }

                        // Now, decide what to do once the effect has taken place.                        
                        // repeat immediately
                        if (effect.ShouldRepeatNow())
                        {
                            effect.Status = RPGEffect.EffectStatus.ActiveToDo;
                        }
                        // repeat later
                        else if (effect.ShouldRepeat())
                        {
                            effect.Status = RPGEffect.EffectStatus.Ready;
                        }
                        // don't repeat, just be done.
                        else if (effect.HasExpired())
                        {
                            effect.Status = RPGEffect.EffectStatus.Done;
                        }
                    }

                    //once done, see if we should undo the effect.
                    if (effect.Status == RPGEffect.EffectStatus.Done)
                    {
                        if (effect.ShouldReverse)
                        {
                            effect.UnApplyToTarget(this);
                        }

                        if (!effect.ShouldRepeat())
                        {
                            // remove it.
                            Effects[i] = null;
                        }
                    }
                }
            }
        }
        private void ApplyDeath()
        {
            // die.
            Session.Print(this.Name + " has died.");

            this.currentState = ActionState.Dying;

            // drop actor's items to ground.
            RPGDrop drop = new RPGDrop();
            drop.X = this.X + this.Width / 2 - drop.Width / 2;
            drop.Y = this.Y + this.Height / 2;

            for (int i = 0; i < Enum.GetValues(typeof(Inventory.BodySlot)).Length; i++)
            {
                if (inventory.GetBodyItem(i) != null)
                {
                    drop.AddItem(inventory.GetBodyItem(i));
                }
            }
            for (int i = 0; i < Inventory.PACK_SIZE; i++)
            {
                if (inventory.GetPackItem(i) != null)
                {
                    drop.AddItem(inventory.GetPackItem(i));
                }
            }

            Session.thisSession.thisArea.AddObject(drop);

            // now give exp points to killer.
            if (this.AI.lastAttacker != null)
            {
                this.AI.lastAttacker.ExpAdjust(this.ExpForKill);
            }
        }
        // only used within attacking action.
        private void AttemptToEngageTarget()
        {
            // check timing to attack
            int coolDown = RPGWeapon.COOLDOWN_DART; // for fists
            if (this.inventory.GetWpn() != null)
            {
                coolDown = this.inventory.GetWpn().CoolDown;
            }

            if(DateTime.Now.CompareTo(lastAttack.AddMilliseconds(coolDown)) < 1)
            {
                return;
            }

            // if can attack now, start new attack
            ((Actor)currentAction.target).States.IsBeingAttacked = true;
            ((Actor)currentAction.target).AI.lastAttacker = this;

            // Attack with different weapon types...
            if (this.inventory.GetWpn() == null)
            {
                AttemptMeleeAttack();
            }
            else if (this.inventory.GetWpn().weaponType == RPGWeapon.WeaponType.Launcher)
            {
                AttemptShootLauncher();
            }
            else if(this.inventory.GetWpn().weaponType == RPGWeapon.WeaponType.Thrown)
            {
                AttemptThrowAttack();
            }
            else // melee
            {
                AttemptMeleeAttack();
            }
        } // end attempEngage
        private void AttemptShootLauncher()
        {
            if (this.inventory.GetBodyItem((int)Inventory.BodySlot.Ammo) == null)
            {
                // then we have no ammo
                string msg = "" + this.Name + ": I have no ammo equipped to attack with this "
                    + Enum.GetName(typeof(RPGWeapon.WeaponClass), this.inventory.GetWpn().weaponClass) + "!";
                Session.Print(msg);

                // if currentAction is attacking, then stop attacking
                if (currentAction.type == RPGAction.ActionType.Attack)
                {
                    currentAction.Accomplished = true; // this will stop attacking.
                }

            }
            else
            {
                // we have ammo, make sure it matches
                if (this.inventory.GetWpn().GetProjectileType() !=
                (this.inventory.GetBodyItem((int)Inventory.BodySlot.Ammo) as Projectile).type)
                {
                    // then wrong ammo type
                    string msg = "" + this.Name + ": I have the wrong ammo equipped to attack with this "
                        + Enum.GetName(typeof(RPGWeapon.WeaponClass), this.inventory.GetWpn().weaponClass) + "!";
                    Session.Print(msg);

                    // if currentAction is attacking, then stop attacking
                    if (currentAction.type == RPGAction.ActionType.Attack)
                    {
                        currentAction.Accomplished = true; // this will stop attacking.
                    }
                }
                else
                {
                    // we have the correct ammo type, shoot one
                    lastAttack = DateTime.Now;

                    // have to create a projectile
                    Projectile p = (inventory.GetBodyItem((int)Inventory.BodySlot.Ammo) as Projectile).GetOneOfStack();
                    if (p != null)
                    {
                        // NOTE: projectile does not start at top-left corner,
                        // but is set to be the center 2/3 of character, set in constructor
                        p.Owner = this;
                        p.UpdateLocation();
                        p.Target = (Actor)currentAction.target;
                        p.IsFlying = true;
                        Session.thisSession.thisArea.AddObject(p);
                    }
                    // reduce/remove thrown item from inventory
                    (inventory.GetBodyItem((int)Inventory.BodySlot.Ammo) as Projectile).StackQuantity--;
                    if ((inventory.GetBodyItem((int)Inventory.BodySlot.Ammo) as Projectile).StackQuantity == 0)
                    {
                        // remove this throwing weapon.
                        inventory.RemoveBodyItem(Inventory.BodySlot.Ammo);
                    }
                }
            }
        } // end AttemptShootLauncher
        private void AttemptThrowAttack()
        {
            // shoot one of stack
            lastAttack = DateTime.Now;

            // have to create a projectile

            Projectile p = this.inventory.GetWpn().getThrownProjectileFromWeapon();
            if (p != null)
            {
                p.X = this.X;
                p.Y = this.Y;
                p.Owner = this;
                p.Target = (Actor)currentAction.target;
                p.IsFlying = true;
                Session.thisSession.thisArea.AddObject(p);
            }

            // reduce/remove thrown item from inventory
            this.inventory.GetWpn().StackQuantity--;
            if (this.inventory.GetWpn().StackQuantity == 0)
            {
                // remove this throwing weapon.
                this.inventory.RemoveBodyItem(Inventory.BodySlot.Hand1);
            }
        }
        private void AttemptMeleeAttack()
        {
            lastAttack = DateTime.Now;

            // hit melee
            switch (new RPGCalc().AttemptHit(this, (Actor)currentAction.target))
            {
                case (RPGCalc.ChallangeResult.Critical_Failure): // crit miss
                    {
                        // draw a 'miss' on target
                        ((Actor)currentAction.target).MissedMe();

                        // - maybe damage self?

                        break;
                    }
                case (RPGCalc.ChallangeResult.Failure): // miss
                    {
                        // draw a 'miss' on target
                        ((Actor)currentAction.target).MissedMe();
                        break;
                    }
                case (RPGCalc.ChallangeResult.Success): // hit
                    {
                        if(this.inventory.GetWpn() != null)
                        {
                            RPGDamage dmg = new RPGDamage(this);
                            ((Actor)currentAction.target).DamageMe(dmg);
                        }
                        else
                        {
                            // assume fists, and minimum damage (str bonus?)
                            ((Actor)currentAction.target).DamageMe(1);
                        }
                        break;
                    }
                case (RPGCalc.ChallangeResult.Critical_Success): // crit hit
                    {
                        if(this.inventory.GetWpn() != null)
                        {
                            // crit = * 2
                            ((Actor)currentAction.target)
                                .DamageMe(new RPGCalc().RollDmg(this.inventory.GetWpn().minDmg,
                                                                this.inventory.GetWpn().maxDmg) * 2);
                        }
                        else
                        {
                            // assume fists, and minimum damage (str bonus?)
                            ((Actor)currentAction.target).DamageMe(2);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        private void SetAttributesToDefault()
        {
            m_Base = new int[ATTRIBUTE_COUNT];
            for (int i = 0; i < m_Base.Length; i++)
            {
                m_Base[i] = ATTRIBUTE_DEFAULT;
            }

            m_Current = new int[ATTRIBUTE_COUNT];
            for (int i = 0; i < m_Current.Length; i++)
            {
                m_Current[i] = ATTRIBUTE_DEFAULT;
            }
        }        
        #endregion

        #region Static methods
        public static Actor CreateRandomActor()
        {
            RPGCalc calc = new RPGCalc();
            int primary = calc.Roll(6);
            int secondary = calc.Roll(6);
            while (primary == secondary)
            {
                secondary = calc.Roll(6);
            }

            return CreateRandomActor((Attribute)Enum.Parse(typeof(Attribute), primary.ToString()),
                                     (Attribute)Enum.Parse(typeof(Attribute), secondary.ToString()));
        }
        public static Actor CreateRandomActor(Actor.Attribute PrimaryAttribute, Actor.Attribute SecondaryAttribute)
        {
            RPGCalc calc = new RPGCalc();

            Actor newActor = new Actor();
            int[] rolls = calc.RollAttributes();

            // order int array by value
            Array.Sort(rolls, 0, rolls.Length);

            // clear actor's current attributes
            for (int i = 0; i < newActor.BaseAttributes.Length; i++)
            {
                newActor.BaseAttributes[i] = 0;
            }

            // fill important attributes with values
            int next = 5;
            newActor.BaseAttributes[(int)PrimaryAttribute] = rolls[next--];
            newActor.BaseAttributes[(int)SecondaryAttribute] = rolls[next--];

            // fill rest
            for (int i = 0; i < newActor.BaseAttributes.Length; i++)
            {
                // find blank and fill with next
                if (newActor.BaseAttributes[i] == 0)
                {
                    newActor.BaseAttributes[i] = rolls[next--];
                }
            }

            newActor.Name = "Random Actor";

            // set current stats to match
            newActor.ResetAttributes();
            newActor.HPBaseMax = calc.GetBaseHP(newActor);
            newActor.MPBaseMax = calc.GetBaseMP(newActor);
            newActor.ResetStats();

            newActor.BaseSpeed = RPGCalc.DEFAULT_SPEED;
            newActor.CurrentSpeed = newActor.BaseSpeed;
            newActor.lastAttack = System.DateTime.Now;
            newActor.lastDamage = new DateTime();

            newActor.SetAlignment(calc.RollAlignment());

            newActor.inventory.AddPackItem(RPGPotion.CreateRandomPotion());

            //newActor.SpellBook = RPGSpellBook.CreateRandomSpellbook();
            newActor.SpellBook = RPGSpellBook.CreateTestSpellbook();

            return newActor;
        }
        public static Actor CreateRandomFighter()
        {
            Actor a = CreateRandomActor(Attribute.Strength, Attribute.Constitution);

            a.Name = "Random Fighter";

            // equip with fighter gear
            a.inventory.AddBodyItem(RPGArmor.CreateRandomTorsoArmor());
            a.inventory.AddBodyItem(RPGArmor.CreateRandomShield());

            // 50/50 for a belt
            if (new RPGCalc().Roll(100) > 50)
            {
                a.inventory.AddBodyItem(new RPGArmor(RPGArmor.ArmorClass.Belt));
            }

            // boots
            // 20% heavy, 40% light, 40% none
            int bootsRoll = new RPGCalc().Roll(100);
            if (bootsRoll > 80)
            {
                a.inventory.AddBodyItem(new RPGArmor(RPGArmor.ArmorClass.HeavyBoots));
            }
            else if(bootsRoll > 40)
            {
                a.inventory.AddBodyItem(new RPGArmor(RPGArmor.ArmorClass.LightBoots));
            }

            // helm
            // 20% full, 40% small, 40% none
            int helmRoll = new RPGCalc().Roll(100);
            if (helmRoll > 80)
            {
                a.inventory.AddBodyItem(new RPGArmor(RPGArmor.ArmorClass.FullHelm));
            }
            else if (helmRoll > 40)
            {
                a.inventory.AddBodyItem(new RPGArmor(RPGArmor.ArmorClass.SmallHelm));
            }

            // add random melee weapon
            RPGWeapon wpn = RPGWeapon.CreateRandomMeleeWeapon();

            if (a.inventory.AddBodyItem(wpn) == false)
            {
                a.inventory.AddPackItem(wpn);
            }

            a.AI.RunToAttackMelee = true;

            return a;
        }
        public static Actor CreateRandomArcher()
        {
            Actor a = CreateRandomActor(Attribute.Dexterity, Attribute.Intelligence);

            a.Name = "Random Archer";

            // equip with archer gear
            a.inventory.AddBodyItem(RPGArmor.CreateRandomTorsoArmor());

            a.AI.RunToAttackMelee = false;

            // 25% for a belt
            if (new RPGCalc().Roll(100) > 75)
            {
                a.inventory.AddBodyItem(new RPGArmor(RPGArmor.ArmorClass.Belt));
            }

            // boots
            // 35% light, 65% none
            int bootsRoll = new RPGCalc().Roll(100);
            if (bootsRoll > 65)
            {
                a.inventory.AddBodyItem(new RPGArmor(RPGArmor.ArmorClass.LightBoots));
            }

            // helm
            // 25% small
            int helmRoll = new RPGCalc().Roll(100);
            if (helmRoll > 75)
            {
                a.inventory.AddBodyItem(new RPGArmor(RPGArmor.ArmorClass.SmallHelm));
            }

            // bow or crossbow or sling
            RPGWeapon wpn = (RPGWeapon)RPGWeapon.CreateRandomLauncherWeapon();
            a.inventory.AddBodyItem(wpn);
            a.inventory.AddBodyItem(new Projectile(a, wpn.GetProjectileType(), null));

            return a;
        }
        public static Actor CreateRandomThug()
        {
            // create fighter-type
            Actor a = CreateRandomActor(Attribute.Strength, Attribute.Constitution);
            a.Name = "Thug";

            a.SetAlignment(Alignment.Neutral_Evil);

            a.AI.RunToAttackMelee = true;

            // add random melee weapon
            RPGWeapon wpn = RPGWeapon.CreateRandomMeleeWeapon();
            if (a.inventory.AddBodyItem(wpn) == false)
            {
                a.inventory.AddPackItem(wpn);
            }

            // add one piece of armor
            RPGArmor apiece = RPGArmor.CreateRandomArmorPiece();
            a.inventory.AddBodyItem(apiece);

            return a;
        }
        public static Actor CreateRandomRobber()
        {
            Actor a = CreateRandomActor(Attribute.Strength, Attribute.Dexterity);
            a.Name = "Robber";

            a.SetAlignment(Alignment.Neutral_Evil);

            a.AI.RunToAttackMelee = true;

            // add random melee weapon
            RPGWeapon wpn = RPGWeapon.CreateRandomMeleeWeapon();
            if (a.inventory.AddBodyItem(wpn) == false)
            {
                a.inventory.AddPackItem(wpn);
            }

            return a;
        }
        #endregion
    }
}
