using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class RPGSpellBook
    {
        #region Public Definitions
        public static int DEFAULT_MAX_SPELL_COUNT = 10;
        #endregion

        #region Private Member Delcarations
        private RPGSpell[] Spells;
        #endregion

        #region Constructors
        public RPGSpellBook(): this(DEFAULT_MAX_SPELL_COUNT){}
        public RPGSpellBook(int max)
        {
            Spells = new RPGSpell[max];
        }
        #endregion

        #region Public Methods
        public int SpellCountMax
        {
            get { return this.Spells.Length; }
        }
        public int SpellCountCurrent
        {
            get 
            { 
                int result = GetOpenSpellSlot();
                if (result == -1)
                {
                    return SpellCountMax;
                }
                else
                {
                    return result;
                }
            }
        }
        public bool AddSpell(RPGSpell s)
        {
            int slot = GetOpenSpellSlot();
            if (slot > -1)
            {
                Spells[slot] = s;
                return true;
            }
            else
            {
                return false;
            }
        }
        public RPGSpell GetSpellAtIndex(int i)
        {
            if(i < Spells.Length)
            {
                return Spells[i];
            }
            else
            {
                return null;
            }
        }
        public bool RemoveSpell(RPGSpell s)
        {
            int slot = FindSpell(s);
            if (slot > -1)
            {
                Spells[slot] = null;
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Private Functions
        private int GetOpenSpellSlot()
        {
            for (int i = 0; i < Spells.Length; i++)
            {
                if (Spells[i] == null)
                {
                    return i;
                }
            }

            return -1;
        }
        private int FindSpell(RPGSpell s)
        {
            for (int i = 0; i < Spells.Length; i++)
            {
                if (Spells[i] == null) { continue; }

                if (Spells[i].Realm == s.Realm &&
                    Spells[i].Effect.GetDescriptionFull() == s.Effect.GetDescriptionFull())
                {
                    return i;
                }
            }

            return -1;
        }
        #endregion

        public static RPGSpellBook CreateRandomSpellbook()
        {
            RPGCalc calc = new RPGCalc();
            int size = calc.Roll(DEFAULT_MAX_SPELL_COUNT * 2);
            RPGSpellBook book = new RPGSpellBook(size);

            int count = calc.Roll(size);
            for (int i = 0; i < count; i++)
            {
                book.AddSpell(RPGSpell.CreateRandomSpell());
            }

            return book;
        }
        public static RPGSpellBook CreateTestSpellbook()
        {
            RPGCalc calc = new RPGCalc();
            int size = DEFAULT_MAX_SPELL_COUNT * 2;
            RPGSpellBook book = new RPGSpellBook(size);

            int count = size;
            for (int i = 0; i < count; i++)
            {
                book.AddSpell(RPGSpell.CreateRandomSpell());
            }

            return book;
        }
    }
}
