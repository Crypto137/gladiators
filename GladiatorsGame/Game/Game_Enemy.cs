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
        private int level, turnCount, health, maxHealth, healthRegen, critChance, critDamageMultiplier, damageReduction;
        private int baseMinDamage, baseMaxDamage;
        private int stun, bleed, bleedDamage, daze;

        private int baseNameID, prefixID, suffixID;
        private int healthModifier, damageModifier;

        private bool bleedImmune;

        public void Generate(int level)
        {
            baseNameID = rnd.Next(0, 10);

            if (GameLogic.CheckChance(level) == true)
            {
                prefixID = rnd.Next(1, 11);
            }
            else
            {
                prefixID = 0;
            }

            if (GameLogic.CheckChance(level) == true)
            {
                suffixID = rnd.Next(1, 11);
            }
            else
            {
                suffixID = 0;
            }

            name = InitializeName();

            baseMinDamage = 8;
            baseMaxDamage = 12;

            healthRegen = 0;
            critChance = 0;
            critDamageMultiplier = 2;
            healthModifier = (level - 1) * rnd.Next(4, 7);
            damageModifier = 0;
            damageReduction = 1;

            turnCount = 1;
            maxHealth = rnd.Next(95, 106) + healthModifier;
            health = maxHealth;

            stun = 0;
            bleed = 0;
            bleedDamage = 0;
            daze = 0;

            bleedImmune = false;

            switch (suffixID)
            {
                case 1:
                    //[NYI] of the Night: get +5 damage after turn 6;
                    break;
                case 2:
                    //[NYI] of the Dusk: -10 damage modifier, get damage every turn
                    break;
                case 3:
                    //[NYI] of the Dawn: +10 damage modifier, lose damage every turn
                    break;
                case 4:
                    //[NYI] the Untamed: deals more damage when less health
                    break;
                case 5:
                    //the Kingslayer: +1 hp for every player level
                    maxHealth = maxHealth + level;
                    health = maxHealth;
                    break;
                case 6:
                    //[NYI] the Insane: 20% chance to attack self
                    break;
                case 7:
                    //from the Depths: no damage range (always average)
                    baseMinDamage = (baseMinDamage + baseMaxDamage) / 2;
                    baseMaxDamage = baseMinDamage;
                    break;
                case 8:
                    //from the Mountains: no damage range (always min)
                    baseMaxDamage = baseMinDamage;
                    break;
                case 9:
                    //from the Abyss: no damage range (always max)
                    baseMinDamage = baseMaxDamage;
                    break;
                case 10:
                    //Blessed by the Gods: +10 health, +5 damage
                    healthModifier = healthModifier + 10;
                    damageModifier = damageModifier + 5;
                    break;
            }

            switch (prefixID)
            {
                case 1:
                    //Bleeding: start with 7 bleeding for 6 turns
                    bleed = 6;
                    bleedDamage = 7;
                    break;
                case 2:
                    //Savage: 15% critical hit chance on attack
                    critChance = 15;
                    break;
                case 3:
                    //Wild: recover 5 hp every turn
                    healthRegen = 5;
                    break;
                case 4:
                    //Ruthless: double damage modifier
                    damageModifier = damageModifier * 2;
                    break;
                case 5:
                    //Unstoppable: start with 20 hp higher than max
                    health = health + 20;
                    break;
                case 6:
                    //Reckless: increased amage range (-5 min, +5 max)
                    baseMinDamage = baseMinDamage - 5;
                    baseMaxDamage = baseMaxDamage + 5;
                    break;
                case 7:
                    //Lazy: skips the first turn
                    stun = 1;
                    break;
                case 8:
                    //Immortal - NYI: survives 1-3 turns with negative health
                    break;
                case 9:
                    //Ironbound: takes half damage from direct hits (not bleed)
                    damageReduction = 2;
                    break;
                case 10:
                    //Invincible: immune to bleed damage
                    bleedImmune = true;
                    break;
            }
        }

        public string Attack(Player Target)
        {
            int damage;
            string logText;

            damage = rnd.Next(baseMinDamage, baseMaxDamage + 1) + damageModifier;

            if (GameLogic.CheckChance(critChance) == true)
            {
                damage = damage * critDamageMultiplier;
            }

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
            namePrefix[8] = "Immortal ";
            namePrefix[9] = "Ironbound ";
            namePrefix[10] = "Invincible ";

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

        public int GetHealthRegen()
        {
            return healthRegen;
        }

        public int GetDamageReduction()
        {
            return damageReduction;
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

        public bool GetBleedImmune()
        {
            return bleedImmune;
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
