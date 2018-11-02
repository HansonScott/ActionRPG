using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RPG
{
    public class RPGEffect
    {
        public static int MAX_EFFECTS = 10;
        public static string NL = Environment.NewLine;

        #region ENUMs
        public enum EffectRange
        {
            Self,       // caster of effect
            Touch,      // single target in touch range
            Target,     // single target in LOS
            Area,       // radius centered on self
            TargetArea, // radius centered on target
        }
        public enum DurationType
        {
            ForTime,        // duration of spell, like a boost spell, or shield
            WhileEquipped,  // most normal magic equipment, effects only when equipped
            Permanent       // permanent effects, like a charm in inventory or door w/ trap
        }
        public enum EffectTargetBuff
        {
            Strength,
            Dexterity,
            Constitution,
            Intelligence,
            Wisdom,
            Charisma,
            RestoreHP,
            RaiseMaxHP,
            RestoreMP,
            RaiseMaxMP,
            Speed,
            Attack,
            Defense,
            RaiseDamage,
        }
        public enum EffectTargetAttack
        {
            DrainStrength,
            DrainDexterity,
            DrainConstitution,
            DrainIntelligence,
            DrainWisdom,
            DrainCharisma,
            LowerMaxHP,
            DrainMP,
            LowerMaxMP,
            DrainSpeed,
            ReduceAttack,
            ReduceDefense,
            ReduceDamage,
            DoPhysicalDamage,
            DoMagicalDamage,
        }
        public enum EffectTrigger
        {
            Immediately,
            onUse,
            onEquip,
            onHit,
            onAttackAttempt,
            onAttackLanded,
            onDeath,
        }
        public enum EffectStatus
        {
            Ready,
            ActiveToDo,
            ActiveDone,
            Done,
        }
        public enum EffectPowerType
        {
            StaticAmount,
            PerLevel,
        }
        #endregion

        #region Declarations
        private EffectRange range;
        private DurationType durationType;
        private EffectTargetBuff targetBuff;
        private EffectTargetAttack targetAttack;
        private bool effectIsABuff = true;
        private EffectTrigger trigger;
        private EffectStatus status;
        private EffectPowerType powerType;

        private Point location;
        private RPGObject sourceObject;
        private RPGObject targetObject;

        private int distance;   // only used if range is target or targetArea.
        private int radius;     // only used if range is area or targetArea.
        private TimeSpan durationValue;
        private int minPower;
        private int maxPower;
        private int m_Power;

        private DateTime lastStart;
        private DateTime lastPause;
        private TimeSpan durationRemaining;
        private int repeatCount;
        private TimeSpan m_repeatDelay;
        private DateTime m_lastRepeat;
        private bool m_shouldReverse;        
        #endregion

        #region Constructor and Setup
        // Don't allow a blank constructor
        private RPGEffect(){}
        public RPGEffect(RPGEffect copy)
        {
            range = copy.range;
            durationType = copy.durationType;
            targetBuff = copy.targetBuff;
            targetAttack = copy.targetAttack;
            effectIsABuff = copy.effectIsABuff;
            trigger = copy.trigger;
            status = copy.status;
            location = copy.location;
            sourceObject = copy.sourceObject;
            targetObject = copy.targetObject;

            distance = copy.distance;
            radius = copy.radius;
            durationValue = copy.durationValue;
            minPower = copy.minPower;
            maxPower = copy.maxPower;
            m_Power = copy.Power;
            lastStart = copy.lastStart;
            lastPause = copy.lastPause;
            durationRemaining = copy.durationRemaining;
            repeatCount = copy.repeatCount;
            m_repeatDelay = copy.m_repeatDelay;
            m_lastRepeat =  copy.m_lastRepeat;
            m_shouldReverse = copy.m_shouldReverse;
            powerType = copy.PowerType;
        }

        public RPGEffect(DurationType t,
                            EffectRange r, 
                            EffectTargetBuff buff,
                            EffectTargetAttack attack,
                            bool isABuff,
                            EffectTrigger trigger,
                            int Distance, 
                            int Radius, TimeSpan Duration, 
                            int MinPower, int MaxPower, 
                            EffectPowerType powerType,
                            bool ReverseWhenExpires)
        {
            this.durationType = t; 
            this.range = r;
            this.TargetBuff = buff;
            this.TargetAttack = attack;
            this.effectIsABuff = isABuff;
            this.status = EffectStatus.Ready;
            this.trigger = trigger;
            this.repeatCount = 0;
            this.distance = Distance;
            this.radius = Radius;
            this.Duration = Duration;
            this.minPower = MinPower;
            this.maxPower = MaxPower;
            this.powerType = powerType;
            this.m_Power = new RPGCalc().RollDmg(minPower, maxPower);
            this.m_shouldReverse = ReverseWhenExpires;
        }

        #endregion

        #region Property access
        public EffectRange Range
        {
            get { return range; }
            set { range = value; }
        }
        public DurationType DurationEffectType
        {
            get { return this.durationType; }
            set { durationType = value; }
        }
        public EffectTargetBuff TargetBuff
        {
            get { return targetBuff; }
            set { targetBuff = value; }
        }
        public EffectTargetAttack TargetAttack
        {
            get { return targetAttack; }
            set { targetAttack = value; }
        }
        public bool EffectIsABuff
        {
            get { return this.effectIsABuff;}
        }
        public EffectTrigger Trigger
        {
            get { return trigger; }
            set { trigger = value; }
        }
        public EffectPowerType PowerType
        {
            get { return powerType; }
            set { powerType = value; }
        }
        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }
        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }
        public TimeSpan Duration
        {
            get { return durationValue; }
            set 
            { 
                durationValue = value;
                ResetDuration();
            }
        }
        public int MinPower
        {
            get { return minPower; }
            set { minPower = value; }
        }
        public int MaxPower
        {
            get { return maxPower; }
            set { maxPower = value; }
        }
        public int AvgPower
        {
            get { return (int)((minPower + maxPower) / 2); }
        }
        public int Power
        {
            get
            {
                return m_Power;
            }
        }
        public bool IsActive
        {
            get { return this.status == EffectStatus.ActiveToDo 
                    || this.status == EffectStatus.ActiveDone; }
        }
        public EffectStatus Status
        {
            get { return status; }
            set 
            { 
                status = value;
                if (status == EffectStatus.Ready)
                {
                    m_lastRepeat = DateTime.Now;
                }
            }
        }
        public bool ShouldReverse
        {
            get { return m_shouldReverse; }
        }

        public bool ShouldRepeat()
        {
            return this.repeatCount > 0;
        }
        public void SetRepeat(int i, TimeSpan delay)
        {
            this.repeatCount = i;
            this.m_repeatDelay = delay;
        }
        public bool ShouldRepeatNow()
        {
            bool resultTime = (DateTime.Now.CompareTo(m_lastRepeat.Add(m_repeatDelay)) > 0);
            bool resultAny = this.repeatCount > 0;
            return (resultAny && resultTime);
        }
        public RPGObject TargetObject
        {
            get { return targetObject; }
            set { targetObject = value; }
        }
        public RPGObject SourceObject
        {
            get { return sourceObject; }
            set { sourceObject = value; }
        }
        public Point Location
        {
            get { return location; }
            set { location = value; }
        }
        #endregion

        #region Public methods
        public void Enable()
        {
            if (status == EffectStatus.Ready)
            {
                lastStart = DateTime.Now;
            }
            else if (status == EffectStatus.Done)
            {
                if (this.repeatCount > 0)
                {
                    status = EffectStatus.Ready;
                    lastStart = DateTime.Now;
                }
            }
        }
        public void Disable()
        {
            status = EffectStatus.Done;
        }
        public void Pause()
        {
            lastPause = DateTime.Now;
            long ticksRan = lastPause.Ticks - lastStart.Ticks;
            long ticksLeft = durationRemaining.Ticks - ticksRan;

            durationRemaining = new TimeSpan(ticksLeft);
            status = EffectStatus.Ready;
        }
        public void Resume()
        {
            lastStart = DateTime.Now;
        }
        public void ResetDuration()
        {
            durationRemaining = durationValue;
        }
        public bool HasExpired()
        {
            // NOTE: need to take into account the pausing...
            //if (ThereHasBeenAPauseDuringThisDuration())
            //{
                if (DateTime.Now.CompareTo(lastStart.Add(durationRemaining) + Session.thisSession.lastPauseDuration) > 0)
                {
                    // then we have expired.
                    return true;
                }
                else
                {
                    return false;
                }
            //}
        }

        private bool ThereHasBeenAPauseDuringThisDuration()
        {
            return lastStart.CompareTo(Session.thisSession.lastUnPause) < 0;
        }
        public void ApplyToTarget(RPGObject targetObject)
        {
            this.TargetObject = targetObject;
            Actor targetActor = targetObject as Actor;
            string PrintLine = "";
            if (targetObject.isOfType(typeof(Actor)))
            {
                targetActor = targetObject as Actor;
                PrintLine += targetActor.Name;
            }

            // actually change the target based on the effect
            if (EffectIsABuff)
            {
                switch (TargetBuff)
                {
                    case(EffectTargetBuff.Attack):
                        {
                            targetActor.BaseAttack += Power;
                            targetActor.UpdateAttack();
                            PrintLine += " Attack changed by " + Power;
                            break;
                        }
                    case (EffectTargetBuff.Charisma):
                        {
                            targetActor.BaseCharisma += Power;
                            targetActor.ResetAttributes();
                            PrintLine += " Charisma changed by " + Power;
                            break;
                        }
                    case (EffectTargetBuff.Constitution):
                        {
                            targetActor.BaseConstitution += Power;
                            targetActor.ResetAttributes();
                            PrintLine += " Constitution changed by " + Power;
                            break;
                        }
                    case (EffectTargetBuff.Defense):
                        {
                            targetActor.BaseDefense += Power;
                            targetActor.UpdateDefense();
                            PrintLine += " Defense changed by " + Power;
                            break;
                        }
                    case (EffectTargetBuff.Dexterity):
                        {
                            targetActor.BaseDexterity += Power;
                            targetActor.ResetAttributes();
                            PrintLine += " Dexterity changed by " + Power;
                            break;
                        }
                    case (EffectTargetBuff.Intelligence):
                        {
                            targetActor.BaseIntelligence += Power;
                            targetActor.ResetAttributes();
                            PrintLine += " Intelligence changed by " + Power;
                            break;
                        }
                    case (EffectTargetBuff.RaiseDamage):
                        {
                            targetActor.BaseDamage += Power;
                            PrintLine += " Damage changed by " + Power;
                            break;
                        }
                    case (EffectTargetBuff.RaiseMaxHP):
                        {
                            targetActor.HPBaseMax += Power;
                            targetActor.HPAdjustCurrentMax(Power);
                            targetActor.HPAdjustCurrent(Power);
                            PrintLine += " Max HP changed by " + Power;
                            break;
                        }
                    case (EffectTargetBuff.RaiseMaxMP):
                        {
                            targetActor.MPBaseMax += Power;
                            targetActor.MPAdjustCurrentMax(Power);
                            targetActor.MPAdjustCurrent(Power);
                            PrintLine += " Max MP changed by " + Power;
                            break;
                        }
                    case (EffectTargetBuff.RestoreHP):
                        {
                            // because we are only restoring, it cannot go over the max
                            targetActor.HPAdjustCurrent(Math.Min(Power, Math.Max(targetActor.HPCurrentMax - targetActor.HPCurrent, 0)));
                            PrintLine += " HP restored by " + Power;
                            break;
                        }
                    case (EffectTargetBuff.RestoreMP):
                        {
                            // because we are only restoring, it cannot go over the max
                            targetActor.MPAdjustCurrent(Math.Min(Power, Math.Max(targetActor.MPCurrentMax - targetActor.MPCurrent, 0)));
                            PrintLine += " MP restored by " + Power;
                            break;
                        }
                    case (EffectTargetBuff.Speed):
                        {
                            targetActor.BaseSpeed += Power;
                            targetActor.CurrentSpeed += Power;
                            PrintLine += " Speed changed by " + Power;
                            break;
                        }
                    case (EffectTargetBuff.Strength):
                        {
                            targetActor.BaseStrength += Power;
                            targetActor.ResetAttributes();
                            PrintLine += " Strength changed by " + Power;
                            break;
                        }
                    case (EffectTargetBuff.Wisdom):
                        {
                            targetActor.BaseWisdom += Power;
                            targetActor.ResetAttributes();
                            PrintLine += " Wisdom changed by " + Power;
                            break;
                        }
                } // end switch
            }
            else
            {
            }

            Session.Print(PrintLine);
        }
        public void UnApplyToTarget(RPGObject targetObject)
        {
            m_Power = -m_Power;
            ApplyToTarget(targetObject);
        }
        
        public string GetDescriptionSimple()
        {
            string result = "" + this.Power;

            if (this.EffectIsABuff)
            {
                result += " " + Enum.GetName(typeof(EffectTargetBuff), this.targetBuff);
            }
            else
            {
                result += " " + Enum.GetName(typeof(EffectTargetAttack), this.targetAttack);
            }

            result += " to " + Enum.GetName(typeof(RPGEffect.EffectRange), this.Range);
            string duration = GetDurationAsString();
            if(duration.Length > 0)
            {
                result += " for " + duration;
            }
            return result;
        }
        public string GetDescriptionFull()
        {
            string result = "";

            if (this.EffectIsABuff)
            {
                result += "Effect: " + Enum.GetName(typeof(RPGEffect.EffectTargetBuff), this.targetBuff) + NL;
            }
            else
            {
                result += "Effect: " + Enum.GetName(typeof(RPGEffect.EffectTargetAttack), this.targetAttack) + NL;
            }
            result += "Range: " + Enum.GetName(typeof(RPGEffect.EffectRange), this.Range) + NL;

            if (this.Range == EffectRange.Area
                || this.Range == EffectRange.TargetArea)
            {
                result += "Range: " + this.radius + " pxls" + NL;
            }

            result += "Duration: " + Enum.GetName(typeof(RPGEffect.DurationType), this.durationType) + NL;

            if (this.durationType == DurationType.ForTime)
            {
                result += "Duration: " + GetDurationAsString();

                result += NL;
            }

            result += "Power: " + this.minPower + " - " + this.maxPower + "(" + this.Power + ")" + NL;

            return result;
        }

        public bool IsCorrectTarget(RPGObject source, RPGObject target, Point targetLocation)
        {
            if (source == null || target == null) { return false; }

            bool result = false;
            RPGCalc calc = new RPGCalc();
            switch (range)
            {
                case (EffectRange.Self):
                    {
                        result = (source == target);
                        break;
                    }
                case (EffectRange.Target):
                    {
                        // target is roughly at location
                        result = calc.ObjectOnPoint(target, targetLocation);
                        break;
                    }
                case (EffectRange.Touch):
                    {
                        // target within touch distance
                        int d = calc.DistanceBetween(source, target);
                        result = (d <= RPGCalc.DEFAULT_TOUCH_RANGE);
                        break;
                    }
                case (EffectRange.Area):
                    {
                        // target within item distance
                        int d = calc.DistanceBetween(source, target);
                        result = (d <= this.distance);
                        break;
                    }
                case (EffectRange.TargetArea):
                    {
                        // target within item distance and radius
                        int d = calc.DistanceBetween(target, targetLocation);
                        result = (d <= this.radius);
                        break;
                    }
                default:
                    {
                        break;
                    }
            } // end switch
            return result;
        }
        #endregion

        #region private methods
        public string GetDurationAsString()
        {
            string result = "";
            if (this.durationValue.TotalHours >= 1)
            {
                result += "" + this.durationValue.TotalHours + " hours";
            }
            else if (this.durationValue.TotalMinutes >= 1)
            {
                result += "" + this.durationValue.TotalMinutes + " minutes";
            }
            else if (this.durationValue.TotalSeconds >= 1)
            {
                result += "" + this.durationValue.TotalSeconds + " seconds";
            }
            else
            {
                result += "";
            }
            return result;
        }
        #endregion

        #region Static methods
        public static RPGEffect CreateRandomEffect()
        {
            RPGCalc calc = new RPGCalc();
            DurationType t = DurationType.ForTime;
            TimeSpan duration = new TimeSpan(0, 0, 0);
            EffectRange r = EffectRange.Target;
            EffectTrigger trigger = EffectTrigger.Immediately;
            EffectTargetBuff targetBuff;
            EffectTargetAttack targetAttack;
            bool isBuff = true;
            bool reverse = true;

            int dist = 0;
            int radius = 0;
            int minPower = 0;
            int maxPower = 0;

            // roll for type
            t = (DurationType)calc.GetRandomEnum(typeof(DurationType));
            duration = new TimeSpan(0);

            if (t == DurationType.ForTime)
            {
                duration = calc.RollRandomEffectDuration(); // ticks (I think)
            }

            // roll for range
            r = (EffectRange)calc.GetRandomEnum(typeof(EffectRange));

            // roll for distance depending on range
            dist = 0;
            switch (r)
            {
                case (EffectRange.Self):
                    {
                        break;
                    }
                case (EffectRange.Touch):
                    {
                        dist = RPGCalc.DEFAULT_TOUCH_RANGE;
                        break;
                    }
                case (EffectRange.Target):
                    {
                        dist = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        break;
                    }
                case (EffectRange.Area):
                    {
                        dist = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        radius = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        break;
                    }
                case (EffectRange.TargetArea):
                    {
                        dist = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        radius = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        break;
                    }
            } // end switch

            // roll for target
            targetBuff = (EffectTargetBuff)calc.GetRandomEnum(typeof(EffectTargetBuff));
            targetAttack = (EffectTargetAttack)calc.GetRandomEnum(typeof(EffectTargetAttack));
            isBuff = (calc.Roll(2) == 1);

            // roll for min/max power
            minPower = calc.Roll(10);
            maxPower = minPower + calc.Roll(10);

            // roll for trigger
            trigger = (EffectTrigger)calc.GetRandomEnum(typeof(EffectTrigger));

            RPGEffect e = new RPGEffect(t, r, targetBuff, targetAttack, isBuff, trigger, dist, radius, duration, minPower, maxPower, RPGEffect.EffectPowerType.StaticAmount, reverse);
            return e;
        }
        public static RPGEffect CreateRandomArmorEffect()
        {
            RPGCalc calc = new RPGCalc();
            DurationType t = DurationType.WhileEquipped;
            TimeSpan duration = new TimeSpan(0,0,0);
            EffectRange r;
            EffectTrigger trigger = EffectTrigger.Immediately;
            EffectTargetBuff targetBuff;
            EffectTargetAttack targetAttack;
            bool isBuff = true;
            bool shouldReverse = true;

            int dist = 0;
            int radius = 0;
            int minPower = 0;
            int maxPower = 0;

            // roll for range
            r = (EffectRange)calc.GetRandomEnum(typeof(EffectRange));

            // roll for distance depending on range
            dist = 0;
            switch (r)
            {
                case (EffectRange.Self):
                    {
                        break;
                    }
                case (EffectRange.Touch):
                    {
                        dist = RPGCalc.DEFAULT_TOUCH_RANGE;
                        break;
                    }
                case (EffectRange.Target):
                    {
                        dist = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        break;
                    }
                case (EffectRange.Area):
                    {
                        dist = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        radius = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        break;
                    }
                case (EffectRange.TargetArea):
                    {
                        dist = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        radius = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        break;
                    }
            } // end switch

            // roll for target
            targetBuff = (EffectTargetBuff)calc.GetRandomEnum(typeof(EffectTargetBuff));
            targetAttack = (EffectTargetAttack)calc.GetRandomEnum(typeof(EffectTargetAttack));

            // roll for min/max power
            minPower = calc.Roll(10);
            maxPower = minPower + calc.Roll(10);

            // roll for trigger
            while (trigger == EffectTrigger.onAttackAttempt 
                || trigger == EffectTrigger.onAttackLanded 
                || trigger == EffectTrigger.onUse)
            {
                trigger = (EffectTrigger)calc.GetRandomEnum(typeof(EffectTrigger));
            }

            RPGEffect e = new RPGEffect(t, r, targetBuff, targetAttack, isBuff, trigger, dist, radius, duration, minPower, maxPower, RPGEffect.EffectPowerType.StaticAmount, shouldReverse);
            return e;
        }
        public static RPGEffect CreateRandomWeaponEffect()
        {
            RPGCalc calc = new RPGCalc();
            DurationType t = DurationType.WhileEquipped;
            TimeSpan duration = new TimeSpan(0, 0, 0);
            EffectRange r;
            EffectTrigger trigger = EffectTrigger.onAttackLanded;
            EffectTargetBuff targetBuff;
            EffectTargetAttack targetAttack;
            bool isBuff = false;
            bool reverse = true;

            int dist = 0;
            int radius = 0;
            int minPower = 0;
            int maxPower = 0;

            // roll for range
            r = (EffectRange)calc.GetRandomEnum(typeof(EffectRange));

            // roll for distance depending on range
            switch (r)
            {
                case (EffectRange.Self):
                    {
                        isBuff = true;
                        break;
                    }
                case (EffectRange.Touch):
                    {
                        dist = RPGCalc.DEFAULT_TOUCH_RANGE;
                        break;
                    }
                case (EffectRange.Target):
                    {
                        dist = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        break;
                    }
                case (EffectRange.Area):
                    {
                        dist = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        radius = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        break;
                    }
                case (EffectRange.TargetArea):
                    {
                        dist = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        radius = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        break;
                    }
            } // end switch

            // roll for target
            targetBuff = (EffectTargetBuff)calc.GetRandomEnum(typeof(EffectTargetBuff));
            targetAttack = (EffectTargetAttack)calc.GetRandomEnum(typeof(EffectTargetAttack));

            // roll for min/max power
            minPower = calc.Roll(10);
            maxPower = minPower + calc.Roll(10);

            // roll for trigger
            while (trigger == EffectTrigger.onUse || trigger == EffectTrigger.onHit)
            {
                trigger = (EffectTrigger)calc.GetRandomEnum(typeof(EffectTrigger));
            }

            RPGEffect e = new RPGEffect(t, r, targetBuff, targetAttack, isBuff, trigger, dist, radius, duration, minPower, maxPower, RPGEffect.EffectPowerType.StaticAmount, reverse);
            return e;
        }
        public static RPGEffect CreateRandomPotionEffect()
        {
            RPGCalc calc = new RPGCalc();
            DurationType t = DurationType.ForTime;
            TimeSpan duration = new TimeSpan(0, 0, 0);
            EffectRange r = EffectRange.Touch;
            EffectTrigger trigger = EffectTrigger.Immediately;
            EffectTargetBuff targetBuff = EffectTargetBuff.RestoreHP;
            EffectTargetAttack targetAttack = EffectTargetAttack.DoPhysicalDamage;
            bool isBuff = true;

            int dist = 0;
            int radius = 0;
            int minPower = 0;
            int maxPower = 0;

            bool reverse = true;

            // roll for target
            targetBuff = (EffectTargetBuff)calc.GetRandomEnum(typeof(EffectTargetBuff));
            targetAttack = (EffectTargetAttack)calc.GetRandomEnum(typeof(EffectTargetAttack));

            if (targetBuff == EffectTargetBuff.RestoreHP
                || targetBuff == EffectTargetBuff.RestoreMP)
            {
                reverse = false;
            }
            else
            {
                duration = calc.RollRandomEffectDuration();
            }

            // roll for range
            while (r == EffectRange.Touch)
            {
                r = (EffectRange)calc.GetRandomEnum(typeof(EffectRange));
            }

            // roll for distance depending on range
            dist = 0;
            switch (r)
            {
                case (EffectRange.Self):
                    {
                        break;
                    }
                case (EffectRange.Touch):
                    {
                        dist = RPGCalc.DEFAULT_TOUCH_RANGE;
                        break;
                    }
                case (EffectRange.Target):
                    {
                        dist = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        break;
                    }
                case (EffectRange.Area):
                    {
                        dist = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        radius = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        break;
                    }
                case (EffectRange.TargetArea):
                    {
                        dist = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        radius = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        break;
                    }
            } // end switch

            // roll for min/max power
            minPower = calc.Roll(10);
            maxPower = minPower + calc.Roll(10);

            // trigger - potion will cause effect to be used immediately.
            trigger = EffectTrigger.Immediately;

            RPGEffect e = new RPGEffect(t, r, targetBuff, targetAttack, isBuff, trigger, dist, radius, duration, minPower, maxPower, RPGEffect.EffectPowerType.StaticAmount, reverse);
            return e;
        }

        public static RPGEffect CreateRandomSpellEffect()
        {
            #region Setup Vars
            RPGCalc calc = new RPGCalc();
            DurationType t = DurationType.Permanent;
            TimeSpan duration = new TimeSpan(0, 0, 0);
            EffectRange r = (EffectRange)calc.GetRandomEnum(typeof(EffectRange));
            EffectTrigger trigger = EffectTrigger.Immediately;
            EffectTargetBuff targetBuff = EffectTargetBuff.RestoreHP;
            EffectTargetAttack targetAttack = EffectTargetAttack.DoMagicalDamage;
            bool isBuff = true;

            int dist = 0;
            int radius = 0;
            int minPower = 0;
            int maxPower = 0;

            bool reverse = true;
            #endregion

            #region Buff or Attack
            // 50/50 percent of offensive for completely random
            if (calc.Roll(2) == 1)
            {
                isBuff = true;
            }
            else
            {
                isBuff = false;
            }
            #endregion

            #region Range and Distance
            // check to avoid attacking self
            while (!isBuff && r == EffectRange.Self)
            {
                r = (EffectRange)calc.GetRandomEnum(typeof(EffectRange));
            }
            // roll for distance depending on range
            dist = 0;
            switch (r)
            {
                case (EffectRange.Self):
                    {
                        break;
                    }
                case (EffectRange.Touch):
                    {
                        dist = RPGCalc.DEFAULT_TOUCH_RANGE;
                        break;
                    }
                case (EffectRange.Target):
                    {
                        dist = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        break;
                    }
                case (EffectRange.Area):
                    {
                        dist = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        radius = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        break;
                    }
                case (EffectRange.TargetArea):
                    {
                        dist = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        radius = calc.Roll(RPGCalc.DEFAULT_LOS_RANGE);
                        break;
                    }
            } // end switch
            #endregion

            #region Attack and Buff Effects
            // target buff effect
            targetBuff = (EffectTargetBuff)calc.GetRandomEnum(typeof(EffectTargetBuff));

            // target attack effect
            // 20% chance of being completely random
            if (calc.Roll(10) >= 8)
            {
                targetAttack = (EffectTargetAttack)calc.GetRandomEnum(typeof(EffectTargetAttack));
            }
            else // 80% chance of being magical damage
            {
                targetAttack = EffectTargetAttack.DoMagicalDamage;
            }
            #endregion

            #region Set Duration Types based on effect
            // isBuff and is restore, or isNotBuff and is Dmg
            if (isBuff)
            {
                // then we want to reverse all but the restore
                if (targetBuff == EffectTargetBuff.RestoreHP ||
                    targetBuff == EffectTargetBuff.RestoreMP)
                {
                    reverse = false;
                    t = DurationType.Permanent;
                }
                else
                {
                    reverse = true;
                    duration = calc.RollRandomEffectDuration(0, 0, 0, 60);
                    t = DurationType.ForTime;
                }
            }
            else // it's an attack
            {
                // check to avoid reverse on dmg spells
                if (targetAttack == EffectTargetAttack.DoMagicalDamage ||
                    targetAttack == EffectTargetAttack.DoPhysicalDamage)
                {
                    reverse = false;
                    t = DurationType.Permanent;
                }
                else
                {
                    reverse = true;
                    duration = calc.RollRandomEffectDuration(0, 0, 0, 60);
                    t = DurationType.ForTime;
                }
            }
            #endregion

            #region Power
            // roll for min/max power - start with generic
            minPower = calc.Roll(10);
            maxPower = minPower + calc.Roll(10);

            // NOTE: dmg depends on duration and duration type - if long, then small dmg...

            #endregion

            #region Trigger
            // trigger - spell will cause effect immediately.
            trigger = EffectTrigger.Immediately;
            #endregion

            RPGEffect e = new RPGEffect(t, r, targetBuff, targetAttack, isBuff, trigger, dist, radius, duration, minPower, maxPower, RPGEffect.EffectPowerType.StaticAmount, reverse);
            return e;
        }
        #endregion
    }
}
