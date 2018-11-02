using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RPG
{
    #region Public Enums
    public enum SpellRealm
    {
        Air,
        Life,
        Water,
        Nature,
        Earth,
        Death,
        Fire,
        Energy,
    }
    public enum SpellStage
    {
        Dormant,
        Casting,
        Traveling,
        Affecting,
        Lingering,
        Spent,
    }
    #endregion

    public class RPGSpell
    {
        #region Private Member Declarations
        private RPGEffect m_Effect; // can a spell have more than one effect? RPGEffect[]?
        private int m_cost;
        private Color m_Color;
        #endregion

        #region Public Declarations and Properties
        public SpellRealm Realm;
        public SpellStage Stage;
        public RPGEffect Effect
        {
            get{return m_Effect;}
            set
            {
                m_Effect = value;
                CalculateCost();
            }
        }
        public int Cost
        {
            get { return m_cost; }
        }
        public Color SpellColor
        {
            get
            {
                if (m_Color.IsEmpty)
                {
                    m_Color = GetColorFromRealm(this.Realm);
                }

                return m_Color;
            }
        }
        #endregion

        #region Constructor
        public RPGSpell()
        {
        }
        #endregion

        #region Static Functions
        public static RPGSpell CreateRandomSpell()
        {
            RPGSpell spell = new RPGSpell();
            spell.Stage = SpellStage.Dormant;
            spell.Effect = RPGEffect.CreateRandomSpellEffect();
            spell.Realm = new RPGCalc().RandomSpellRealm();
            return spell;
        }
        #endregion

        #region Public Methods
        public string GetShortEffectDescription()
        {
            string effect;

            if (this.Effect.EffectIsABuff)
            {
                effect = Enum.GetName(typeof(RPGEffect.EffectTargetBuff), this.Effect.TargetBuff);

                if (!(effect.StartsWith("Restore") ||
                    (effect.StartsWith("Raise"))))
                {
                    effect = "Buff " + effect;
                }
            }
            else
            {
                if (this.Effect.TargetAttack == RPGEffect.EffectTargetAttack.DoMagicalDamage)
                {
                    if (this.Realm == SpellRealm.Life)
                    {
                        effect = "Undead" + " Damage";
                    }
                    else
                    {
                        effect = this.Realm + " Damage";
                    }
                }
                else if (this.Effect.TargetAttack == RPGEffect.EffectTargetAttack.DoPhysicalDamage)
                {
                    effect = "Physical Damage";
                }
                else
                {
                    effect = Enum.GetName(typeof(RPGEffect.EffectTargetAttack), this.Effect.TargetAttack);
                }
            }

            return effect;
        }
        #endregion

        #region Private Functions
        private void CalculateCost()
        {
            // theory - the cost of a spell is related to its effect

            // base is avg power
            int result = Effect.AvgPower;

            // over time too.
            int sec  =(int)(Math.Max(1, Effect.Duration.TotalSeconds / 10));
            result *= sec;

            // 25% more cost for attack spells
            if (!Effect.EffectIsABuff)
            {
                result = (int)(result * 1.25);
            }

            // 25% more cost for range spells
            if (Effect.Range == RPGEffect.EffectRange.Target ||
                Effect.Range == RPGEffect.EffectRange.TargetArea)
            {
                result = (int)(result * 1.25);
            }

            // 25% more cost for area spells
            if (Effect.Range == RPGEffect.EffectRange.Area ||
                Effect.Range == RPGEffect.EffectRange.TargetArea)
            {
                result = (int)(result * 1.25);
            }

            this.m_cost = result;
        }
        private Color GetColorFromRealm(SpellRealm spellRealm)
        {
            switch(spellRealm)
            {
                case SpellRealm.Air:
                    return Color.LightBlue;
                    break;
                case SpellRealm.Death:
                    return Color.Black;
                    break;
                case SpellRealm.Earth:
                    return Color.Brown;
                    break;
                case SpellRealm.Energy:
                    return Color.Pink;
                    break;
                case SpellRealm.Fire:
                    return Color.Red;
                    break;
                case SpellRealm.Life:
                    return Color.White;
                    break;
                case SpellRealm.Nature:
                    return Color.Green;
                    break;
                case SpellRealm.Water:
                    return Color.Blue;
                    break;
                default:
                    return Color.Black;
                    break;
            }
        }

        #endregion
    }
}
