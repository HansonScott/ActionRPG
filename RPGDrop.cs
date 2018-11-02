using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RPG
{
    public class RPGDrop: RPGObject
    {
        #region Public Declarations
        public static double DEFAULT_DURATION = 2*60*1000; // 2 min.
        #endregion

        #region Member Declarations
        private ArrayList m_items;
        private DateTime m_Dropped;
        private double PersistDuration; // in ms
        private bool Persist;
        #endregion

        #region Constructor and Setup
        public RPGDrop()
        {
            m_items = new ArrayList();
            this.Height = Res.getPic("bag2.gif").Height;
            this.Width = Res.getPic("bag2.gif").Width;

            this.ImpedesWalking = false;

            m_Dropped = DateTime.Now;
            Persist = false;
            PersistDuration = DEFAULT_DURATION;

            //AddRandomItem();
        }
        #endregion
        #region Public Access
        public void AddItem(RPGObject item)
        {
            m_items.Add(item);
        }
        public void RemoveItem(RPGObject item)
        {
            m_items.Remove(item);
        }
        public RPGObject GetRandomItem()
        {
            RPGObject[] items = GetItems();
            if (items.Length == 0)
            {
                return null;
            }
            else if (items.Length == 1)
            {
                return items[0];
            }
            else
            {
                // roll for a random item
                int index = new RPGCalc().Roll(items.Length) - 1;
                // remove it from the list of items
                RemoveItem(items[index]);
                // return the removed item
                return items[index];
            }
        }
        public RPGObject[] GetItems()
        {
            return (RPGObject[])m_items.ToArray(typeof(RPGObject));
        }
        #endregion

        #region Public Methods
        public void AddRandomItem()
        {
            AddItem(RPGWeapon.CreateRandomWeapon());
        }
        #endregion

        #region Private methods
        public override void DrawSelf(System.Drawing.Graphics g)
        {
            new RPGDraw().DrawDrop(g, this);
        }
        public override void UpdateSelf()
        {
            if(Persist == false 
                && DateTime.Now.CompareTo(m_Dropped.AddMilliseconds(PersistDuration)) > 0)
            {
                // then item has expired, delete it.
                DeleteMe = true;
            }
        }
        #endregion
    }
}
