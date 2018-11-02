using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class Inventory
    {
        #region Enums
        public enum BodySlot
        {
            Head = 0,
            Neck = 1,
            Torso = 2,
            Hand1 = 3,
            Hand2 = 4,
            Ring1 = 5,
            Ring2 = 6,
            Belt = 7,
            Feet = 8,
            Ammo = 9
        }
        #endregion

        #region Declarations
        public const int PACK_SIZE = 20;
        public const int QUICK_SIZE = 3;
        private RPGItem[] BodyItems;
        private RPGItem[] PackItems;
        private RPGItem[] QuickItems;

        private Actor Owner;
        #endregion

        #region Constructor
        public Inventory(Actor a)
        {
            Owner = a;
            BodyItems = new RPGItem[Enum.GetValues(typeof(BodySlot)).Length];
            PackItems = new RPGItem[PACK_SIZE];
            QuickItems = new RPGItem[QUICK_SIZE];
        }
        #endregion

        #region Public methods
        public RPGWeapon GetWpn()
        {
            RPGItem item = BodyItems[(int)BodySlot.Hand1];
            if (item != null)
            {
                return (RPGWeapon)item;
            }
            else
            {
                return null;
            }
        }
        public bool AddItem(RPGObject obj)
        {
            if (obj.isOfType(typeof(RPGDrop)))
            {
                RPGObject[] objs = ((RPGDrop)obj).GetItems();
                for (int i = 0; i < objs.Length; i++)
                {
                    AddItem((RPGItem)objs[i]);
                }
                return true;
            }
            else if(obj.isOfType(typeof(RPGItem)))
            {
                return AddPackItem((RPGItem)obj);
            }
            else
            {
                // don't recognize item type.
                return false;
            }
        }

        public RPGItem GetBodyItem(BodySlot slot)
        {
            return GetBodyItem((int)slot);
        }
        public RPGItem GetBodyItem(int slotIndex)
        {
            if (BodyItems[slotIndex] != null)
            {
                return BodyItems[slotIndex];
            }
            else
            {
                return null;
            }
        }
        public bool AddBodyItem(RPGItem item)
        {
            if (item.isOfType(typeof(RPGPotion)))
            {
                return false;
            }
            else if (BodyItems[(int)item.Slot] != null)
            {
                return false;
            }
            else
            {
                // the slot is empty, make sure we CAN set it here
                // if item is a wpn 
                if (item.Slot == BodySlot.Hand1)
                {
                    // if wpn is two handed
                    if (((RPGWeapon)item).is2Handed == true)
                    {
                        //make sure 2nd hand is empty too.
                        if (BodyItems[(int)BodySlot.Hand2] != null)
                        {
                            return false;
                        }
                    }
                }
                // if item to be equipped is a shield
                else if (item.Slot == BodySlot.Hand2)
                {
                    // make sure 1st hand is not a two handed weapon
                    if (BodyItems[(int)BodySlot.Hand1] != null)
                    {
                        // we have something in the wpn hand, check it.
                        RPGItem itemInHand = BodyItems[(int)BodySlot.Hand1];
                        if (itemInHand.isOfType(typeof(RPGWeapon)))
                        {
                            RPGWeapon wpn = itemInHand as RPGWeapon;
                            if (wpn.is2Handed)
                            {
                                return false;
                            }
                        }
                    }
                }

                BodyItems[(int)item.Slot] = item;

                // this could change our stats
                Owner.UpdateAttack();
                Owner.UpdateDefense();
                return true;
            }
        }
        public RPGItem RemoveBodyItem(BodySlot slot)
        {
            return RemoveBodyItem((int)slot);
        }
        public RPGItem RemoveBodyItem(int slotIndex)
        {
            if (BodyItems[slotIndex] != null)
            {
                RPGItem item = BodyItems[slotIndex];
                BodyItems[slotIndex] = null;
                return item;
            }
            else
            {
                return null;
            }
        }

        public RPGObject GetPackItem(int packSlotIndex)
        {
            if (PackItems[packSlotIndex] != null)
            {
                return PackItems[packSlotIndex];
            }
            else
            {
                return null;
            }
        }
        public RPGObject RemovePackItem(int packSlotIndex)
        {
            RPGObject obj = GetPackItem(packSlotIndex);
            if (obj != null)
            {
                PackItems[packSlotIndex] = null;
                return obj;
            }
            else
            {
                return null;
            }
        }
        public bool AddPackItem(RPGItem item)
        {
            try
            {
                PackItems[GetOpenPackSlot()] = item;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public int GetOpenPackSlot()
        {
            for (int i = 0; i < PackItems.Length; i++)
            {
                if (PackItems[i] == null)
                {
                    return i;
                }
            }

            return -1;
        }

        public RPGItem[] GetQuickItems()
        {
            return this.QuickItems;
        }
        public RPGItem GetQuickItem(int slotZeroBased)
        {
            return this.QuickItems[slotZeroBased];
        }
        public int GetOpenQuickSlot()
        {
            bool open = false;
            int slot = 0;
            while (open == false && slot < QUICK_SIZE)
            {
                if (QuickItems[slot] == null)
                {
                    open = true;
                    return slot;
                }
                else
                {
                    slot++;
                }
            }
            return -1;
        }
        public bool AddQuickItem(RPGItem item)
        {
            int slot = GetOpenQuickSlot();
            bool result = false;
            if (slot >= 0 && slot < QUICK_SIZE)
            {
                result = AddQuickItem(item, slot);
            }
            return result;
        }
        public RPGItem RemoveQuickItem(RPGItem item)
        {
            if (item != null)
            {
                RPGItem[] items = GetQuickItems();
                for (int i = 0; i < items.Length; i++)
                {
                    if (QuickItems[i] == item)
                    {
                        QuickItems[i] = null;
                        return item;
                    }
                }
                return item;
            }
            else
            {
                return null;
            }
        }
        public bool AddQuickItem(RPGItem item, int slotZeroBased)
        {
            if (QuickItems[slotZeroBased] != null)
            {
                return false;
            }
            else
            {
                QuickItems[slotZeroBased] = item;
                return true;
            }
        }
        public RPGItem RemoveQuickItem(int quickSlotIndex)
        {
            RPGItem item = GetQuickItem(quickSlotIndex);
            if (item != null)
            {
                QuickItems[quickSlotIndex] = null;
                return item;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Private methods
        #endregion
    }
}
