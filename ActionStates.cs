using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class ActionStates
    {
        public enum States
        {
            Dying = 0,
            Attacked = 1,
            Attacking = 2,
            Casting = 3,
        }
        private bool[] m_States;
        private Actor self;

        public ActionStates(Actor a)
        {
            self = a;
            m_States = new bool[Enum.GetNames(typeof(ActionStates.States)).Length];
        }

        public bool IsBeingAttacked
        {
            get
            {
                return m_States[(int)ActionStates.States.Attacked];
            }
            set
            {
                m_States[(int)ActionStates.States.Attacked] = value;
            }
        }
        public bool IsAttacking
        {
            get
            {
                return m_States[(int)ActionStates.States.Attacking];
            }
            set
            {
                m_States[(int)ActionStates.States.Attacking] = value;
            }
        }
        public bool IsDying
        {
            get
            {
                return m_States[(int)ActionStates.States.Dying];
            }
            set
            {
                m_States[(int)ActionStates.States.Dying] = value;
            }
        }
        public bool IsCasting
        {
            get 
            {
                return m_States[(int)ActionStates.States.Casting];
            }
            set 
            {
                m_States[(int)ActionStates.States.Casting] = value;
            }
        }
    }
}
