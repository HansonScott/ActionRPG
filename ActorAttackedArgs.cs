using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class ActorAttackedArgs: EventArgs
    {
        private Actor m_Attacker;
        private Actor m_Target;

        public Actor Attacker
        {
            get { return m_Attacker; }
        }
        public Actor Target
        {
            get { return m_Target; }
        }

        public ActorAttackedArgs(Actor Attacker, Actor Target)
        {
            m_Attacker = Attacker;
            m_Target = Target;
        }
    }
}
