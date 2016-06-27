using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorsGame
{
    class Player
    {
        private static Random rnd = new Random();

        private string name;
        private int level, strength, agility, vitality, health, maxHealth, energy, maxEnergy;
        private int stun, bleed, daze;
        private bool finishedTurn, finishedLevelUp, finishedGame;
        private bool[] unlockedSkill = new bool[5];
        private int[] skillCost = new int[5];

        public void Initialize()
        {
            name = "Maximus";

            level = 1;

            strength = 10;
            agility = 10;
            vitality = 10;

            maxHealth = vitality * 10;
            health = maxHealth;
            maxEnergy = agility * 10;
            energy = maxEnergy;

            stun = 0;
            bleed = 0;
            daze = 0;

            finishedTurn = false;
            finishedLevelUp = true;
            finishedGame = false;

            unlockedSkill[0] = true;
            unlockedSkill[1] = true;
            unlockedSkill[2] = false;
            unlockedSkill[3] = false;
            unlockedSkill[4] = false;

            skillCost[0] = 0;
            skillCost[1] = 0;
            skillCost[2] = 20;
            skillCost[3] = 20;
            skillCost[4] = 20;
        }

        public void UpdateStats()
        {
            maxHealth = vitality * 10;
            health = maxHealth;
            maxEnergy = agility * 10;
            energy = maxEnergy;
        }

        public string Skill_HeroicAssault(Enemy target)
        {
            int damage;
            string logText;

            damage = rnd.Next(8, 13) + (strength - 10);
            target.SetHealth(target.GetHealth() - damage);

            logText = "Your Heroic Assault dealt " + damage.ToString() + " damage to " + target.GetName() + "!";
            return logText;
        }

        public string Skill_DesperateStrike(Enemy target)
        {
            int damage;
            string logText;

            damage = rnd.Next(1, 21) + (strength - 10);
            target.SetHealth(target.GetHealth() - damage);

            logText = "Your Desperate Strike dealt " + damage.ToString() + " damage to " + target.GetName() + "!";
            return logText;
        }

        public string Skill_Bash(Enemy target)
        {
            int damage;
            string logText;
            bool effect;

            damage = rnd.Next(2, 5);
            effect = GameLogic.CheckChance(50);
            logText = "Your Bash dealt " + damage.ToString() + " damage to " + target.GetName() + "! ";

            target.SetHealth(target.GetHealth() - damage);

            if (effect == true)
            {
                target.SetStun(2);
                logText = logText + target.GetName() + " got stunned.";
            }
            else
            {
                logText = logText + target.GetName() + " resisted stun.";
            }
            
            return logText;
        }

        public string Skill_DirtThrow(Enemy target)
        {
            int damage;
            string logText;
            bool effect;

            damage = rnd.Next(2, 5);
            effect = GameLogic.CheckChance(50);
            logText = "Your Dirt Throw dealt " + damage.ToString() + " damage to " + target.GetName() + "! ";

            target.SetHealth(target.GetHealth() - damage);

            if (effect == true)
            {
                target.SetDaze(3);
                logText = logText + target.GetName() + " is dizzy.";
            }
            else
            {
                logText = logText + target.GetName() + " resisted daze.";
            }

            return logText;
        }

        public string Skill_SavageCut(Enemy target)
        {
            int damage;
            string logText;
            bool effect;

            damage = 1 + (strength - 10);
            effect = GameLogic.CheckChance(50);

            if (effect == true)
            {
                target.SetBleed(4);
                target.SetBleedDamage(damage);
                logText = target.GetName() + " is bleeding!";
            }
            else
            {
                logText = target.GetName() + " evaded your Savage Cut.";
            }

            return logText;
        }

        public string LevelUp()
        {
            string logText, logTextUnlock;

            level = level + 1;
            finishedLevelUp = false;

            switch (level)
            {
                case 3:
                    unlockedSkill[2] = true;
                    logTextUnlock = " You have unlocked Bash.";
                    break;
                case 5:
                    unlockedSkill[3] = true;
                    logTextUnlock = " You have unlocked Dirt Throw.";
                    break;
                case 7:
                    unlockedSkill[4] = true;
                    logTextUnlock = " You have unlocked Savage Cut.";
                    break;
                default:
                    logTextUnlock = "";
                    break;
            }

            logText = "You have reached level " + level.ToString() + "!" + logTextUnlock;
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

        public int GetStrength()
        {
            return strength;
        }

        public int GetAgility()
        {
            return agility;
        }

        public int GetVitality()
        {
            return vitality;
        }

        public int GetHealth()
        {
            return health;
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }

        public int GetEnergy()
        {
            return energy;
        }

        public int GetMaxEnergy()
        {
            return maxEnergy;
        }

        public int GetStun()
        {
            return stun;
        }

        public int GetBleed()
        {
            return bleed;
        }

        public int GetDaze()
        {
            return daze;
        }

        public bool GetFinishedTurn()
        {
            return finishedTurn;
        }

        public bool GetFinishedLevelUp()
        {
            return finishedLevelUp;
        }

        public bool GetFinishedGame()
        {
            return finishedGame;
        }

        public bool[] GetUnlockedSkill()
        {
            return unlockedSkill;
        }

        public int[] GetSkillCost()
        {
            return skillCost;
        }

        public void SetName(string newName)
        {
            name = newName;
        }

        public void SetLevel(int newLevel)
        {
            level = newLevel;
        }

        public void SetStrength(int newStrength)
        {
            strength = newStrength;
        }

        public void SetAgility(int newAgility)
        {
            agility = newAgility;
        }

        public void SetVitality(int newVitality)
        {
            vitality = newVitality;
        }

        public void SetHealth(int newHealth)
        {
            health = newHealth;

            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }

        public void SetMaxHealth(int newMaxHealth)
        {
            maxHealth = newMaxHealth;
        }

        public void SetEnergy(int newEnergy)
        {
            energy = newEnergy;
        }

        public void SetMaxEnergy(int newMaxEnergy)
        {
            maxEnergy = newMaxEnergy;
        }

        public void SetStun(int newStun)
        {
            stun = newStun;
        }

        public void SetBleed(int newBleed)
        {
            bleed = newBleed;
        }

        public void SetDaze(int newDaze)
        {
            daze = newDaze;
        }

        public void SetFinishedTurn(bool newFinishedTurn)
        {
            finishedTurn = newFinishedTurn;
        }

        public void SetFinishedLevelUp(bool newFinishedLevelUp)
        {
            finishedLevelUp = newFinishedLevelUp;
        }

        public void SetFinishedGame(bool newFinishedGame)
        {
            finishedGame = newFinishedGame;
        }

        public void SetUnlockSkill(int id, bool newState)
        {
            unlockedSkill[id] = newState;
        }

        public void SetSkillCost(int id, int newCost)
        {
            skillCost[id] = newCost;
        }
    }
}
