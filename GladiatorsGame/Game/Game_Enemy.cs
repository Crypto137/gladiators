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

        public void Generate()
        {
            Random rnd = new Random();
            int nameID = rnd.Next(0, 10);

            switch (nameID)
            {
                case 0:
                    name = "Gladiator";
                    break;
                case 1:
                    name = "Prisoner";
                    break;
                case 2:
                    name = "Criminal";
                    break;
                case 3:
                    name = "Barbarian";
                    break;
                case 4:
                    name = "Duelist";
                    break;
                case 5:
                    name = "Lion";
                    break;
                case 6:
                    name = "Tiger";
                    break;
                case 7:
                    name = "Panther";
                    break;
                case 8:
                    name = "Bull";
                    break;
                case 9:
                    name = "Bear";
                    break;
            }

            level = 1;

            maxHealth = rnd.Next(95, 106);
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
