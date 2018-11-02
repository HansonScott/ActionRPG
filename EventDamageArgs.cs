using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class EventDamageArgs: EventArgs
    {
        private Actor m_Source;
        private Actor m_Target;
        private int m_Amount;

        public EventDamageArgs(Actor Source, Actor Target, int Amount)
        {
            m_Source = Source;
            m_Target = Target;
            m_Amount = Amount;
        }

        public Actor Source
        {
            get{return m_Source;}
        }
        public Actor Target
        {
            get{return m_Target;}
        }
        public int Amount
        {
            get{return m_Amount;}
        }
    }
}
