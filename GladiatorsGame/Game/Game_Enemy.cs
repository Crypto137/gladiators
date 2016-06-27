using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorsGame
{
    class Enemy
    {
        private string name;
        private int level, health, maxHealth;
        private int stun, bleed, bleedDamage, daze;

        private int baseNameID, prefixID, suffixID;
        private int healthModifier, damageModifier;

        public void Generate(int level)
        {
            Random rnd = new Random();
            baseNameID = rnd.Next(0, 10);

            if (GameLogic.CheckChance(level) == true)
            {
                prefixID = rnd.Next(1, 4);
            }
            else
            {
                prefixID = 0;
            }

            if (GameLogic.CheckChance(level) == true)
            {
                suffixID = rnd.Next(1, 4);
            }
            else
            {
                suffixID = 0;
            }

            name = InitializeName();

            maxHealth = rnd.Next(95, 106) + (level - 1) * rnd.Next(4, 7);
            health = maxHealth;

            stun = 0;
            bleed = 0;
            bleedDamage = 0;
            daze = 0;
        }

        public string Attack(Player Target)
        {
            Random rnd = new Random();
            int damage;
            string logText;

            damage = rnd.Next(8, 13);
            Target.SetHealth(Target.GetHealth() - damage);

            logText = name + " attacked you for " + damage.ToString() + " damage.";
            return logText;
        }

        public string InitializeName()
        {
            string[] nameBase = new string[10];
            string[] namePrefix = new string[4];
            string[] nameSuffix = new string[4];
            string finalName;

            nameBase[0] = "Gladiator";
            nameBase[1] = "Prisoner";
            nameBase[2] = "Criminal";
            nameBase[3] = "Barbarian";
            nameBase[4] = "Duelist";
            nameBase[5] = "Lion";
            nameBase[6] = "Tiger";
            nameBase[7] = "Panther";
            nameBase[8] = "Bull";
            nameBase[9] = "Bear";

            namePrefix[0] = "";
            namePrefix[1] = "Savage ";
            namePrefix[2] = "Wild ";
            namePrefix[3] = "Ruthless ";

            nameSuffix[0] = "";
            nameSuffix[1] = " from the Depths";
            nameSuffix[2] = " the Untamed";
            nameSuffix[3] = " of the Night";

            finalName = namePrefix[prefixID] + nameBase[baseNameID] + nameSuffix[suffixID];

            return finalName;
        }

        public string GetName()
        {
            return name;
        }

        public int GetLevel()
        {
            return level;
        }

        public int GetHealth()
        {
            return health;
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }

        public int GetStun()
        {
            return stun;
        }

        public int GetBleed()
        {
            return bleed;
        }

        public int GetBleedDamage()
        {
            return bleedDamage;
        }

        public int GetDaze()
        {
            return daze;
        }

        public void SetName(string newName)
        {
            name = newName;
        }

        public void SetLevel(int newLevel)
        {
            level = newLevel;
        }

        public void SetHealth(int newHealth)
        {
            health = newHealth;
        }

        public void SetMaxHealth(int newMaxHealth)
        {
            maxHealth = newMaxHealth;
        }

        public void SetStun(int newStun)
        {
            stun = newStun;
        }

        public void SetBleed(int newBleed)
        {
            bleed = newBleed;
        }

        public void SetBleedDamage(int newBleedDamage)
        {
            bleedDamage = newBleedDamage;
        }

        public void SetDaze(int newDaze)
        {
            daze = newDaze;
        }
    }
}
