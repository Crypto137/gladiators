using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorsGame
{
    class Enemy
    {
        private static Random rnd = new Random();

        private string name;
        private int level, turnCount, health, maxHealth;
        private int baseMinDamage, baseMaxDamage;
        private int stun, bleed, bleedDamage, daze;

        private int baseNameID, prefixID, suffixID;
        private int healthModifier, damageModifier;

        public void Generate(int level)
        {
            baseNameID = rnd.Next(0, 10);

            if (GameLogic.CheckChance(level) == true)
            {
                prefixID = rnd.Next(1, 12);
            }
            else
            {
                prefixID = 0;
            }

            if (GameLogic.CheckChance(level) == true)
            {
                suffixID = rnd.Next(1, 12);
            }
            else
            {
                suffixID = 0;
            }

            name = InitializeName();

            baseMinDamage = 8;
            baseMaxDamage = 12;

            healthModifier = (level - 1) * rnd.Next(4, 7);
            damageModifier = 0;

            turnCount = 1;
            maxHealth = rnd.Next(95, 106) + healthModifier;
            health = maxHealth;

            stun = 0;
            bleed = 0;
            bleedDamage = 0;
            daze = 0;

            switch (prefixID)
            {
                case 1:
                    bleed = 6;
                    bleedDamage = 7;
                    break;
                case 2:
                    //Savage - NYI: 10% critical hit chance on attack
                    break;
                case 3:
                    //Wild - NYI: recover 5 hp every turn
                    break;
                case 4:
                    damageModifier = damageModifier * 2;
                    break;
                case 5:
                    health = health + 20;
                    break;
                case 6:
                    baseMinDamage = baseMinDamage - 5;
                    baseMaxDamage = baseMaxDamage + 5;
                    break;
                case 7:
                    stun = 1;
                    break;
                case 8:
                    //Immortal - NYI: survives 1-3 turns with negative health
                    break;
                case 9:
                    //Ironbound - NYI: takes 50% damage from direct hits (not bleed)
                    break;
                case 10:
                    //Invincible - NYI: immune to bleed damage
                    break;
            }

            switch (suffixID)
            {
                case 1:
                    //of the Night - NYI: get +5 damage after turn 6;
                    break;
                case 2:
                    //of the Dusk - NYI: -10 damage modifier, get damage every turn
                    break;
                case 3:
                    //of the Dawn - NYI: +10 damage modifier, lose damage every turn
                    break;
                case 4:
                    //the Untamed - NYI: deals more damage when less health
                    break;
                case 5:
                    //the Kingslayer - NYI: +1 hp for every player level
                    break;
                case 6:
                    //the Insane - NYI: 20% chance to attack self
                    break;
                case 7:
                    //from the Depths - NYI: no damage range (always average)
                    break;
                case 8:
                    //from the Mountains - NYI: no damage range (always min)
                    break;
                case 9:
                    //from the Abyss - NYI: no damage range (always max)
                    break;
                case 10:
                    //Blessed by the Gods - +healthmodifier, +damagemodifier
                    break;
            }
        }

        public string Attack(Player Target)
        {
            int damage;
            string logText;

            damage = rnd.Next(baseMinDamage, baseMaxDamage + 1) + damageModifier;
            Target.SetHealth(Target.GetHealth() - damage);
            turnCount = turnCount + 1;

            logText = name + " attacked you for " + damage.ToString() + " damage.";
            return logText;
        }

        public string InitializeName()
        {
            string[] nameBase = new string[10];
            string[] namePrefix = new string[11];
            string[] nameSuffix = new string[11];
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
            namePrefix[1] = "Bleeding ";
            namePrefix[2] = "Savage ";
            namePrefix[3] = "Wild ";
            namePrefix[4] = "Ruthless ";
            namePrefix[5] = "Unstoppable ";
            namePrefix[6] = "Reckless ";
            namePrefix[7] = "Lazy ";
            namePrefix[8] = "Immortal";
            namePrefix[9] = "Ironbound";
            namePrefix[10] = "Invincible";

            nameSuffix[0] = "";
            nameSuffix[1] = " of the Night";
            nameSuffix[2] = " of the Dusk";
            nameSuffix[3] = " of the Dawn";
            nameSuffix[4] = " the Untamed";
            nameSuffix[5] = " the Kingslayer";
            nameSuffix[6] = " the Insane";
            nameSuffix[7] = " from the Depths";
            nameSuffix[8] = " from the Mountains";
            nameSuffix[9] = " from the Abyss";
            nameSuffix[10] = ", Blessed by the Gods";

            finalName = namePrefix[prefixID] + nameBase[baseNameID] + nameSuffix[suffixID];

            return finalName;
        }

        public string GetEntranceText()
        {
            int entranceTextID;
            string entranceText;

            entranceTextID = rnd.Next(0, 4);

            switch (entranceTextID)
            {
                case 0:
                    entranceText = "You are now fighting " + name + ".";
                    break;
                case 1:
                    entranceText = name + " has entered the Arena.";
                    break;
                case 2:
                    entranceText = name + " will be your next opponent!";
                    break;
                case 3:
                    entranceText = "Looks like " + name + " wants to challenge you!";
                    break;
                default:
                    entranceText = name + " is your enemy";
                    break;
            }

            return entranceText;
        }

        public string GetDeathText()
        {
            int deathTextID;
            string deathText;

            deathTextID = rnd.Next(0, 4);

            switch (deathTextID)
            {
                case 0:
                    deathText = name + " falls breathless before you.";
                    break;
                case 1:
                    deathText = name + " is defeated!";
                    break;
                case 2:
                    deathText = name + " yields. You are victorious!";
                    break;
                case 3:
                    deathText = name + " bleeds out. You have won!";
                    break;
                default:
                    deathText = name + " is dead.";
                    break;
            }

            return deathText;
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
