using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    class RPGPotion: RPGItem
    {
        public RPGPotion(RPGEffect effect): base()
        {
            this.AddEffect(effect);
            this.Name = "P." + Enum.GetName(typeof(RPGEffect.EffectTargetBuff), effect.TargetBuff);
            this.UpdateDescription();
        }
        public override void UpdateDescription()
        {
            if (this.Effects != null && Effects[0] != null)
            {
                if (this.Effects[1] == null) // check to see if a second effect is present
                {
                    this.m_desc = Effects[0].GetDescriptionSimple();
                }
                else
                {
                    this.m_desc = "Complex Effects";
                }
            }
        }
        public static RPGPotion CreateRandomPotion()
        {
            RPGEffect effect = RPGEffect.CreateRandomPotionEffect();
            return new RPGPotion(effect);
        }

        public static RPGPotion CreatePotionHealingSmall()
        {
            return CreatePotionHealing(4, 12);
        }
        public static RPGPotion CreatePotionHealingMedium()
        {
            return CreatePotionHealing(10, 35);
        }
        public static RPGPotion CreatePotionHealingLarge()
        {
            return CreatePotionHealing(30, 60);
        }
        public static RPGPotion CreatePotionHealingMassive()
        {
            return CreatePotionHealing(50, 100);
        }
        public static RPGPotion CreatePotionHealing(int minPower, int maxPower)
        {
            RPGEffect effect = new RPGEffect(RPGEffect.DurationType.Permanent, 
                                            RPGEffect.EffectRange.Self, 
                                            RPGEffect.EffectTargetBuff.RestoreHP, 
                                            RPGEffect.EffectTargetAttack.DoPhysicalDamage, 
                                            true, RPGEffect.EffectTrigger.Immediately, 
                                            0, 0, new TimeSpan(0, 0, 0), 
                                            minPower, maxPower, RPGEffect.EffectPowerType.StaticAmount, 
                                            false);
            return new RPGPotion(effect);
        }
    }
}
