using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RPG
{
    class RPGCalc
    {
        #region Setup Defs
        private static Random rand = new Random();
        #endregion

        #region Actor Defs
        public static int DEFAULT_SPEED = 8;
        public static int DEFAULT_TOUCH_RANGE = 30;

        public static int DEFAULT_BASE_ATTACK = 5;
        public static int DEFAULT_BASE_DEFENSE = 10;
        public static int DEFAULT_BASE_DAMAGE = 0;

        public static int DEFAULT_BASE_HP = 6;
        public static int DEFAULT_BASE_MP = 6;
        public static int DEFAULT_MIN_HP = 2;
        public static int DEFAULT_MIN_MP = 2;

        public static int DEFAULT_HP_PER_LEVEL = DEFAULT_BASE_HP;
        public static int DEFAULT_MP_PER_LEVEL = DEFAULT_BASE_MP;

        public static int DEFAULT_EXP_PER_LEVEL = 10;
        public static int DEFAULT_LOS_RANGE = 400;
        #endregion

        #region spacial calculations
        public bool ObjectsCollide(RPGObject a, RPGObject b)
        {
            Rectangle intersection 
                = Rectangle.Intersect(new Rectangle(a.Location, a.Size), 
                                        new Rectangle(b.Location, b.Size));

            return (intersection.IsEmpty == false);
        }
        public bool ObjectOnPoint(RPGObject obj, Point p)
        {
            return (obj.X <= p.X
                && obj.X + obj.Width >= p.X
                && obj.Y <= p.Y
                && obj.Y + obj.Height >= p.Y);
        }
        public bool ActorStandingNearPoint(RPGObject obj, Point p)
        {
            // point p has been adjusted already, use straight
            bool inX = (Math.Abs(obj.X - p.X) <= Math.Abs(obj.VX));
            bool inY = (Math.Abs(obj.Y - p.Y) <= Math.Abs(obj.VY));
            return (inX && inY);
        }
        public int DistanceBetween(RPGObject a, RPGObject b)
        {
            int dX = Math.Abs(a.X - b.X);
            int dY = Math.Abs(a.Y - b.Y);

            if (dX > a.Width) { dX -= a.Width; }
            if (dY > a.Height) { dY -= a.Height; }

            return (int)Math.Sqrt((dX * dX) + (dY * dY));
        }
        public int DistanceBetween(RPGObject a, Point p)
        {
            // closest edge of actor to point

            int dX;
            int dY;

            if (a.X > p.X)
            {
                dX = Math.Abs(a.X - p.X);
            }
            else if (a.X + a.Width < p.X)
            {
                dX = Math.Abs(p.X - a.X - a.Width);
            }
            else
            {
                dX = Math.Abs(a.X - p.X);
            }

            if (a.Y > p.Y)
            {
                dY = Math.Abs(a.Y - p.Y);
            }
            else if (a.Y + a.Height < p.Y)
            {
                dY = Math.Abs(p.Y - a.Y - a.Height);
            }
            else
            {
                dY = Math.Abs(a.Y - p.Y);
            }

            return (int)Math.Sqrt((dX * dX) + (dY * dY));
        }
        public int DistanceBetween(Point p1, Point p2)
        {
            int dX = Math.Abs(p1.X - p2.X);
            int dY = Math.Abs(p1.Y - p2.Y);

            return (int)Math.Sqrt((dX * dX) + (dY * dY));
        }

        /// <summary>
        /// calculates the location (Point) that is towards the target 
        /// in order to be within the source's weapon range.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public Point PointToBeInRange(Actor source, RPGObject target)
        {
            return PointToBeInRange(source, source.inventory.GetWpn().Range, target);
        }
        /// <summary>
        /// Calculates the location (Point) that is towards the target 
        /// in order to be within the given range
        /// </summary>
        /// <param name="source"></param>
        /// <param name="Range"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public Point PointToBeInRange(Actor source, int Range, RPGObject target)
        {
            // so, use the distance between and the actor's range to figure where the point needs to be.

            int distanceX = target.X - source.X;
            int distanceY = target.Y - source.Y;

            int diagDistance = (int)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

            // diagDistance is the entire distance between the two, we only need enough to get in range.

            diagDistance -= Range;

            // now diagDistance is the distance between source and target IN RANGE

            double angle = Math.Atan((double)distanceY / (double)distanceX);

            int changeX = (int)(Math.Cos(angle) * diagDistance * 1.1); // 1.1 = get inside range
            int changeY = (int)(Math.Sin(angle) * diagDistance * 1.1);

            // sometimes the x value turns out backwards, so make a general
            // sweeping assumption to check for the correct direction.
            if (source.X > target.X)
            {
                // changeX needs to be negative
                changeX = -Math.Abs(changeX);
            }
            else if (source.X < target.X)
            {
                // changeX needs to be postive
                changeX = Math.Abs(changeX);
            }

            // check the y value too.
            if (source.Y > target.Y)
            {
                // changeY needs to be negative
                changeY = -Math.Abs(changeY);
            }
            else if (source.Y < target.Y)
            {
                // changeY needs to be positive
                changeY = Math.Abs(changeY);
            }

            return new Point(source.X + changeX, source.Y + changeY);

        }

        public bool ActorIsInArea(Actor a, Point p, int Radius)
        {
            return (DistanceBetween(a.Location, p) <= Radius);
        }
        public bool AreaEffectAppliesToActor(RPGEffect effect, Actor a)
        {
            switch (effect.Range)
            {
                case (RPGEffect.EffectRange.Area):
                    {
                        // then if actor a is in the area defined by a point and radius
                        return ActorIsInArea(a, effect.Location, effect.Radius);
                    }
                case (RPGEffect.EffectRange.Self):
                    {
                        return a == effect.SourceObject;
                    }
                case (RPGEffect.EffectRange.Target):
                    {
                        return a == effect.TargetObject;
                    }
                case (RPGEffect.EffectRange.TargetArea):
                    {
                        return ActorIsInArea(a, effect.TargetObject.Location, effect.Radius);
                    }
                case (RPGEffect.EffectRange.Touch):
                    {
                        // ammend, not only the target, but also within touch range
                        return a == effect.TargetObject;
                    }
                default:
                    {
                        //when in doubt, it doesn't apply
                        return false;
                    }
            }
        }
        #endregion

        #region Actor statistics
        public int GetCostForCurrentValue(int val)
        {
            switch (val)
            {
                case (1):
                case (2):
                case (18):
                    {
                        return 5;
                    }

                case (3):
                case (4):
                case (16):
                case (17):
                    {
                        return 4;
                    }

                case (5):
                case (6):
                case (14):
                case (15):
                    {
                        return 3;
                    }

                case (7):
                case (8):
                case (12):
                case (13):
                    {
                        return 2;
                    }

                case (9):
                case (10):
                case (11):
                default:
                    {
                        return 1;
                    }
            }
        }
        public int GetBonusFromAttribute(int attributeValue)
        {
            int cost = GetCostForCurrentValue(attributeValue) - 1;
            if (attributeValue < 10)
            {
                return -cost;
            }
            else
            {
                return cost;
            }
        }

        public int GetBaseHP(Actor actor)
        {
            return Math.Max(DEFAULT_BASE_HP + GetBonusFromAttribute(actor.BaseConstitution), DEFAULT_MIN_HP);
        }
        public int GetCurrentMaxHP(Actor actor)
        {
            int result = Math.Max(DEFAULT_BASE_HP + GetBonusFromAttribute(actor.CurrentConstitution), DEFAULT_MIN_HP);

            // adjust by all stats/items/effects, etc.

            return result;
        }
        public int GetBaseMP(Actor actor)
        {
            return Math.Max(DEFAULT_BASE_MP + GetBonusFromAttribute(actor.BaseIntelligence), DEFAULT_MIN_MP);
        }
        public int GetCurrentMaxMP(Actor actor)
        {
            int result = Math.Max(DEFAULT_BASE_MP + GetBonusFromAttribute(actor.CurrentIntelligence), DEFAULT_MIN_MP);

            // adjust by all stats/items/effects, etc.

            return result;
        }

        public void LevelUpActor(Actor actor)
        {
            // apply changes to character
            actor.Level += 1;

            int HPAdj = GetLevelUpHPAdj(actor);
            actor.HPBaseMax += HPAdj;
            // don't reset, just adjust - could be mid-action
            actor.HPAdjustCurrentMax(HPAdj);
            actor.HPAdjustCurrent(HPAdj);

            int MPAdj = GetLevelUpMPAdj(actor);
            actor.MPBaseMax += MPAdj;
            // don't reset, just adjust - could be mid-action
            actor.MPAdjustCurrentMax(MPAdj);
            actor.MPAdjustCurrent(MPAdj);

            actor.UpdateAttack();
            actor.UpdateDefense();

            actor.ReadyToLevelUp = false;
        }

        public int GetLevelUpHPAdj(Actor actor)
        {
            return DEFAULT_HP_PER_LEVEL + GetBonusFromAttribute(actor.BaseConstitution);
        }
        public int GetLevelUpMPAdj(Actor actor)
        {
            return DEFAULT_MP_PER_LEVEL + GetBonusFromAttribute(actor.BaseIntelligence);
        }
        public int GetLevelUpAttAdj(Actor actor)
        {
            // maybe stat based?
            return 1;
        }
        public int GetLevelUpDefAdj(Actor actor)
        {
            // maybe stat based?
            return 1;
        }

        public int GetActorCurrentAttack(Actor a)
        {
            // base attack
            int result = a.BaseAttack;

            // increase with levels
            result += a.Level;

            if (a.inventory.GetWpn() != null)
            {
                // actor current attribute bonus
                switch (a.inventory.GetWpn().weaponType)
                {
                    case (RPGWeapon.WeaponType.Thrown):
                        {
                            // Dex + 1/2 Str
                            result += (GetBonusFromAttribute(a.CurrentDexterity)
                                    + (int)(GetBonusFromAttribute(a.CurrentStrength) / 2));
                            break;
                        }
                    case (RPGWeapon.WeaponType.Launcher):
                        {
                            // Dex + 1/2 Int
                            result += (GetBonusFromAttribute(a.CurrentDexterity)
                                    + (int)(GetBonusFromAttribute(a.CurrentIntelligence) / 2));
                            break;
                        }
                    default: // melee
                        {
                            // STR + 1/2 Dex
                            result += (GetBonusFromAttribute(a.CurrentStrength)
                                    + (int)(GetBonusFromAttribute(a.CurrentDexterity) / 2));
                            break;
                        }
                } // switch
            } // end if wpn
            // equipment bonus
            for (int i = 0; i < Enum.GetValues(typeof(Inventory.BodySlot)).Length; i++)
            {
                if (a.inventory.GetBodyItem(i) != null)
                {
                    // check if item has bonus to attack
                    // look through all body effects
                    foreach (RPGEffect effect in a.inventory.GetBodyItem(i).Effects)
                    {
                        if (effect != null && effect.IsActive)
                        {
                            if (effect.TargetBuff == RPGEffect.EffectTargetBuff.Attack)
                            {
                                result += effect.Power;
                            }
                        }
                    }
                }
            }

            // look through all pack effects
            for (int i = 0; i < Inventory.PACK_SIZE; i++)
            {
                if (a.inventory.GetPackItem(i) != null)
                {
                    if (a.inventory.GetPackItem(i).GetType() == typeof(RPGItem))
                    {
                        foreach (RPGEffect effect in ((RPGItem)a.inventory.GetPackItem(i)).Effects)
                        {
                            if (effect != null && effect.IsActive)
                            {
                                if (effect.TargetBuff == RPGEffect.EffectTargetBuff.Attack)
                                {
                                    result += effect.Power;
                                }
                            }
                        }
                    }
                } // if item exists
            } // for all body items

            // active effect bonus in area
            if (Session.thisSession != null 
                && Session.thisSession.thisArea != null 
                && Session.thisSession.thisArea.Effects != null)
                {
                    foreach (RPGEffect effect in Session.thisSession.thisArea.Effects)
                    {
                        if (effect != null && effect.IsActive)
                        {
                            if (AreaEffectAppliesToActor(effect, a))
                            {
                                if (effect.TargetBuff == RPGEffect.EffectTargetBuff.Attack)
                                {
                                    result += effect.Power;
                                }
                            }
                        }
                    }
                }

            return result;
        }
        public int GetActorCurrentDefense(Actor a)
        {
            // base defense
            int result = a.BaseDefense;

            // increase by level
            result += a.Level;

            // actor current Dex Dodge bonus
            result += GetCurrentDexDodgeBonusForActor(a);

            // equipment bonus
            for (int i = 0; i < Enum.GetValues(typeof(Inventory.BodySlot)).Length; i++)
            {
                if (a.inventory.GetBodyItem(i) != null)
                {
                    // check if item has bonus to defense
                    if (a.inventory.GetBodyItem(i).GetType() == typeof(RPGArmor))
                    {
                        // then it may have AC bonus
                        result += ((RPGArmor)a.inventory.GetBodyItem(i)).Defense;
                    }

                    // look through all body effects
                    foreach (RPGEffect effect in a.inventory.GetBodyItem(i).Effects)
                    {
                        if (effect != null && effect.IsActive)
                        {
                            if (effect.TargetBuff == RPGEffect.EffectTargetBuff.Attack)
                            {
                                result += effect.Power;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < Inventory.PACK_SIZE; i++)
            {
                if (a.inventory.GetPackItem(i) != null)
                {
                    // look through all pack effects
                    if (a.inventory.GetPackItem(i).GetType() == typeof(RPGItem))
                    {
                        foreach (RPGEffect effect in ((RPGItem)a.inventory.GetPackItem(i)).Effects)
                        {
                            if (effect != null && effect.IsActive)
                            {
                                if (effect.TargetBuff == RPGEffect.EffectTargetBuff.Attack)
                                {
                                    result += effect.Power;
                                }
                            }
                        }
                    }
                } // if item exists
            } // for all body items

            // active effect bonus in area
            if (Session.thisSession != null 
                && Session.thisSession.thisArea != null
                && Session.thisSession.thisArea.Effects != null)
            {
                foreach (RPGEffect effect in Session.thisSession.thisArea.Effects)
                {
                    if (effect != null && effect.IsActive)
                    {
                        if (AreaEffectAppliesToActor(effect, a))
                        {
                            if (effect.TargetBuff == RPGEffect.EffectTargetBuff.Attack)
                            {
                                result += effect.Power;
                            }
                        }
                    }
                }
            }

            return result;
        }
        public int GetCurrentDexDodgeBonusForActor(Actor a)
        {
            int result = GetBonusFromAttribute(a.CurrentDexterity);

            // reduce by armor values
            for (int i = 0; i < Enum.GetValues(typeof(Inventory.BodySlot)).Length; i++)
            {
                // if item exists
                if (a.inventory.GetBodyItem(i) != null)
                {
                    // if item is armor
                    if (a.inventory.GetBodyItem(i).isOfType(typeof(RPGArmor)))
                    {
                        // if item has lower dox allowance than current value, set.
                        result = Math.Min(result, ((RPGArmor)a.inventory.GetBodyItem(i)).MaxDeterityBonus);
                    }
                }
            }

            return result;
        }
        #endregion

        #region Damage
        public RPGDamage GetActorCurrentDamage(Actor a)
        {
            // go through attack of actor's weapon and bonuses, effects, etc.
            RPGDamage dmg = new RPGDamage(a);
            return dmg;
        }
        public int GetActorCurrentPhysicalDamage(Actor a)
        {
            int d = a.BaseDamage;
            d += new RPGCalc().RollDmg(a.inventory.GetWpn().minDmg,
                                            a.inventory.GetWpn().maxDmg);

            // add any effects from any items, etc.
            return d;
        }
        #endregion

        #region Basic Dice Rolling
        public int Roll(int DieSides)
        {
            return rand.Next(DieSides) + 1; // between 1 and DX
        }
        public int Roll(int DieCount, int DieSides)
        {
            int result = 0;
            for(int i = 0; i < DieCount; i++)
            {
                result += Roll(DieSides);
            }
            return result;
        }
        public int[] RollSet(int DieCount, int DieSides)
        {
            int[] result = new int[DieCount];
            for(int i = 0; i < DieCount; i++)
            {
                result[i] = Roll(DieSides);
            }
            return result;
        }
        public int[] RollAttributes()
        {
            int[] rolls = RollSet(6, 16);
            for (int i = 0; i < rolls.Length; i++)
            {
                rolls[i] += 2;
            }
            return rolls;
        }
        public Actor.Alignment RollAlignment()
        {
            Actor.Alignment a = (Actor.Alignment)GetRandomEnum(typeof(Actor.Alignment));
            return a;
        }

        public int RollDmg(int minDmg, int maxDmg)
        {
            return Roll(maxDmg - minDmg + 1) - 1 + minDmg; // +1-1 to allow for min dmg total.
        }

        public enum ChallangeResult
        {
            Critical_Failure,
            Failure,
            Success,
            Critical_Success
        }
        public ChallangeResult RollChallenge(int challenge)
        {
            // compare attacker's attack vs defender's defense.
            int roll = Roll(20);

            string msg = "Challange: " + challenge + ", rolls: " + roll + ". ";

            // Critical Hit
            if (roll == 20)
            {
                msg += "Critical Success";
                Session.thisSession.TabPageAction.panelActionToolbar.Print(msg);
                return ChallangeResult.Critical_Success;
            }
            // Critical Miss
            else if (roll == 1)
            {
                msg += "Critical Fail";
                Session.thisSession.TabPageAction.panelActionToolbar.Print(msg);
                return ChallangeResult.Critical_Failure;
            }
            else
            {
                if (roll >= challenge)
                {
                    msg += "Success";
                    Session.thisSession.TabPageAction.panelActionToolbar.Print(msg);
                    return ChallangeResult.Success;
                }
                else
                {
                    msg += "Fail";
                    Session.thisSession.TabPageAction.panelActionToolbar.Print(msg);
                    return ChallangeResult.Failure;
                }
            }
        }

        public TimeSpan RollRandomEffectDuration()
        {
            return RollRandomEffectDuration(0, 0, 0, 0);
        }
        public TimeSpan RollRandomEffectDuration(int maxDays, int maxHours, int maxMin, int maxSec)
        {
            int days = 0;
            int hours = 0;
            int min = 0;
            int sec = 0;

            #region Completely random
            if (maxDays == 0 &&
                maxHours == 0 &&
                maxMin == 0 &&
                maxSec == 0)
            {

                int group = Roll(4); // which 'digit' to roll a duration for.
                switch (group)
                {
                    case (1):
                        {
                            days = Roll(3); // 1 to 3 days
                            break;
                        }
                    case (2):
                        {
                            hours = Roll(4) * 6; // 6 - 24 hrs
                            break;
                        }
                    case (3):
                        {
                            min = Roll(4) * 15; // 15 - 60 min
                            break;
                        }
                    case (4):
                        {
                            sec = Roll(12) * 5; // 5 - 60 sec.
                            break;
                        }
                    default:
                        {
                            break;
                        }
                } // end switch
            } // end if completely random
            #endregion
            else // guided random
            {
                if (maxDays > 0)
                {
                    days = Roll(maxDays);
                }
                if (maxHours > 0)
                {
                    hours = Roll(maxHours);
                }
                if (maxMin > 0)
                {
                    min = Roll(maxMin);
                }
                if (maxSec > 0)
                {
                    sec = Roll(maxSec / 5) * 5;
                }
            }
            TimeSpan t = new TimeSpan(days, hours, min, sec);
            return t;
        }

        public Object GetRandomEnum(Type type)
        {
            int l = Enum.GetValues(type).Length;
            int randomEnumValue = Roll(l) - 1;
            Object obj = Enum.Parse(type, randomEnumValue.ToString());
            return obj;
        }
        #endregion

        #region Colors
        public Color RandomColor()
        {
            return Color.FromArgb(Roll(256) - 1, 
                                    Roll(256) - 1, 
                                    Roll(256) - 1);
        }
        #endregion

        #region Attack and Interaction
        /// <summary>
        /// Calculate roll against the chance to hit using the current attack and defence numbers of the actors
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        /// <returns>-1 = Crit miss, 0 = miss, 1 = hit, 2 = Crit hit</returns>
        public ChallangeResult AttemptHit(Actor attacker, Actor defender)
        {
            string msg = "" + attacker.Name + " attempts to hit " + defender.Name + ".";
            Session.thisSession.TabPageAction.panelActionToolbar.Print(msg);

            return RollChallenge(defender.CurrentDefense - attacker.CurrentAttack);
        }
        #endregion

        #region Experience
        public bool CheckXPForLevel(int xp, int lvl)
        {
            // check if xp is enough to gain lvl up.
            return (xp >= GetXPForNextLevel(lvl));
        }
        public int GetXPForNextLevel(int lvl)
        {
            int result = (PlayerCharacter.DEFAULT_XP_GOAL * (lvl - 1))
                        + (PlayerCharacter.DEFAULT_XP_GOAL * (lvl));
            return result;
        }
        public int GetExpForKill(Actor a)
        {
            // future: put in complex based on skills, races, etc., but for now, just a multiplier
            return a.Level* DEFAULT_EXP_PER_LEVEL;
        }
        #endregion


        public SpellRealm RandomSpellRealm()
        {
            SpellRealm spell = (SpellRealm)GetRandomEnum(typeof(SpellRealm));
            return spell;
        }
    }
}
