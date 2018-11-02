using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class RPGDamage
    {
        private Actor m_thisActor;
        public RPGDamage(Actor a)
        {
            m_thisActor = a;
        }

        public int Physical
        {
            get 
            {
                return new RPGCalc().GetActorCurrentPhysicalDamage(m_thisActor);
            }
        }
    }
}
