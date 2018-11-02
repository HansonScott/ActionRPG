using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace RPG
{
    class ComboBoxUtil
    {
        /// <summary>
        /// Uses an ArrayList of string[2]s to create the items.
        /// The string[]s are formatted as name first, then value second.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static CBItem[] MakeCBItemsFromStringArrayList(ArrayList list)
        {
            CBItem[] cbItems = new CBItem[list.Count];
            for(int i = 0; i < list.Count; i++)
            {
                String[] s = list[i] as string[];
                cbItems[i] = (new CBItem(s[1], s[0]));
            }
            return cbItems;
        }
        public static CBItem GetItemByValue(ComboBox.ObjectCollection items, string value)
        {
            foreach (object item in items)
            {
                if ((item as CBItem).ID == value)
                {
                    return (item as CBItem);
                }
            }
            return null;
        }

        public static void PopulateComboBoxWithEnum(ComboBox box, Type t)
        {
            string[] names = Enum.GetNames(t);
            int[] values = Enum.GetValues(t) as int[];
            ArrayList alist = new ArrayList(names.Length);

            for (int i = 0; i < names.Length; i++)
            {
                names[i] += ',' + values[i].ToString();
                alist.Add(names[i].Split(','));
            }

            CBItem[] items = ComboBoxUtil.MakeCBItemsFromStringArrayList(alist);
            foreach (CBItem item in items)
            {
                box.Items.Add(item);
            }
        }
    }
    public class CBItem
    {
        private string m_strId;
        private string m_strName;

        public string ID
        {
            get
            {
                return m_strId;
            }
            set
            {
                m_strId = value;
            }
        }
        public string Name
        {
            get
            {
                return m_strName;
            }
            set
            {
                m_strName = value;
            }
        }

        public CBItem(string ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }
}
