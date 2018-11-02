using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class RPGItem: RPGObject
    {
        #region public stack constants
        public static int STACKSIZE_AMMO = 40;

        #endregion
        public Inventory.BodySlot Slot;
        public int StackMaxQuantity;
        private int m_StackQuantity;
        private int m_Value;

        public RPGItem():base()
        {
            this.StackMaxQuantity = 1;
            this.StackQuantity = 1;
        }

        public int StackQuantity
        {
            get { return m_StackQuantity; }
            set
            {
                m_StackQuantity = value;
                UpdateDescription();
            }
        }
        public int ItemValue
        {
            get { return m_Value; }
            set { m_Value = value; }
        }
        public virtual void UpdateDescription()
        {
        }
    }
}
