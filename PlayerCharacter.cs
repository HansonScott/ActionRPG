using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class PlayerCharacter: Actor
    {
        public enum ExperiencePaths
        {
            Melee,
            Thrown,
            Launcher
        }
        #region Declarations
        public string CurrentArea = "000";
        private int m_ExperienceMelee = 0;
        private int m_ExperienceThrown = 0;
        private int m_ExperienceLauncher = 0;

        private int m_LvlMelee = 1;
        private int m_LvlThrown = 1;
        private int m_LvlLauncher = 1;

        public const int DEFAULT_XP_GOAL = 50;
        #endregion

        #region Constructor and Setup
        public PlayerCharacter()
        {
            inventory.AddBodyItem(RPGWeapon.CreateRandomMeleeWeapon());
            //for (int i = 0; i < 3; i++)
            //{
            inventory.AddPackItem(RPGPotion.CreatePotionHealingSmall());
            //}

            this.AI.RunToAttackMelee = false;

            // for testing only:
            //SpellBook = RPGSpellBook.CreateRandomSpellbook();
            SpellBook = RPGSpellBook.CreateTestSpellbook();
        }
        public PlayerCharacter(string SavedGameFilename)
        {
            try
            {
                LoadSavedCharacter(System.IO.File.ReadAllText(SavedGameFilename));
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
        #endregion

        #region Public methods
        public void AddXP(ExperiencePaths p, int quantity)
        {
            switch (p)
            {
                case(ExperiencePaths.Melee):
                    {
                        m_ExperienceMelee += quantity;
                        break;
                    }
                case (ExperiencePaths.Thrown):
                    {
                        m_ExperienceThrown += quantity;
                        break;
                    }
                case (ExperiencePaths.Launcher):
                    {
                        m_ExperienceLauncher += quantity;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            // check for lvl ups
            if (CheckLvlUp())
            {
                Session.thisSession.TabPageAction.panelActionToolbar.Print(this.Name + " has leveled up!");

                // recalculate stats based on level

            }
        }
        #endregion

        #region Private methods
        private bool CheckLvlUp()
        {
            RPGCalc calc = new RPGCalc();
            bool bMelee = calc.CheckXPForLevel(m_ExperienceMelee, m_LvlMelee);
            bool bThrown = calc.CheckXPForLevel(m_ExperienceThrown, m_LvlThrown);
            bool bLauncher = calc.CheckXPForLevel(m_ExperienceThrown, m_LvlLauncher);

            if (bMelee || bThrown || bLauncher)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private void LoadSavedCharacter(string savedGame)
        {
            // parse text to fill in data.
        }
        #endregion
    }
}
