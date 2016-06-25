using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorsGame
{
    class Player
    {
        private string Name;
        private int Level, Strength, Agility, Vitality, Health, MaxHealth, Energy, MaxEnergy;
        private int Stun, Bleed, Daze;
        private bool FinishedTurn, FinishedLevelUp, FinishedGame;
        private bool[] UnlockedSkill = new bool[5];

        public void Initialize()
        {
            Name = "Maximus";

            Level = 1;

            Strength = 10;
            Agility = 10;
            Vitality = 10;

            MaxHealth = Vitality * 10;
            Health = MaxHealth;
            MaxEnergy = Agility * 10;
            Energy = MaxEnergy;

            Stun = 0;
            Bleed = 0;
            Daze = 0;

            FinishedTurn = false;
            FinishedLevelUp = true;
            FinishedGame = false;

            UnlockedSkill[0] = true;
            UnlockedSkill[1] = true;
            UnlockedSkill[2] = false;
            UnlockedSkill[3] = false;
            UnlockedSkill[4] = false;
        }

        public void UpdateStats()
        {
            MaxHealth = Vitality * 10;
            Health = MaxHealth;
            MaxEnergy = Agility * 10;
            Energy = MaxEnergy;
        }

        public string Skill_HeroicAssault(Enemy Target)
        {
            Random RNGesus = new Random();
            int Damage;
            string LogText;

            Damage = RNGesus.Next(8, 13) + (Strength - 10);
            Target.SetHealth(Target.GetHealth() - Damage);

            LogText = "Your Heroic Assault dealt " + Damage.ToString() + " damage to " + Target.GetName() + "!";
            return LogText;
        }

        public string Skill_DesperateStrike(Enemy Target)
        {
            Random RNGesus = new Random();
            int Damage;
            string LogText;

            Damage = RNGesus.Next(1, 21) + (Strength - 10);
            Target.SetHealth(Target.GetHealth() - Damage);

            LogText = "Your Desperate Strike dealt " + Damage.ToString() + " damage to " + Target.GetName() + "!";
            return LogText;
        }

        public string Skill_Bash(Enemy target)
        {
            Random RNGesus = new Random();
            int damage;
            string logText;
            bool effect;

            damage = RNGesus.Next(4, 9) + (Strength - 10);
            effect = GameLogic.CheckChance(50);
            logText = "Your Bash dealt " + damage.ToString() + " damage to " + target.GetName() + "! ";

            target.SetHealth(target.GetHealth() - damage);

            if (effect == true)
            {
                target.SetStun(1);
                logText = logText + target.GetName() + " got stunned for 1 turn.";
            }
            else
            {
                logText = logText + target.GetName() + " resisted stun.";
            }
            
            return logText;
        }

        public string Skill_DirtThrow(Enemy Target)
        {
            //NYI
            return null;
        }

        public string Skill_SavageCut(Enemy Target)
        {
            //NYI
            return null;
        }

        public string LevelUp()
        {
            string LogText, LogTextUnlock;

            Level = Level + 1;
            FinishedLevelUp = false;

            switch (Level)
            {
                case 3:
                    UnlockedSkill[2] = true;
                    LogTextUnlock = " You have unlocked Bash.";
                    break;
                case 5:
                    UnlockedSkill[3] = true;
                    LogTextUnlock = " You have unlocked Dirt Throw.";
                    break;
                case 7:
                    UnlockedSkill[4] = true;
                    LogTextUnlock = " You have unlocked Savage Cut.";
                    break;
                default:
                    LogTextUnlock = "";
                    break;
            }

            LogText = "You have reached level " + Level.ToString() + "!" + LogTextUnlock;
            return LogText;
        }

        public string GetName()
        {
            return Name;
        }

        public int GetLevel()
        {
            return Level;
        }

        public int GetStrength()
        {
            return Strength;
        }

        public int GetAgility()
        {
            return Agility;
        }

        public int GetVitality()
        {
            return Vitality;
        }

        public int GetHealth()
        {
            return Health;
        }

        public int GetMaxHealth()
        {
            return MaxHealth;
        }

        public int GetEnergy()
        {
            return Energy;
        }

        public int GetMaxEnergy()
        {
            return MaxEnergy;
        }

        public int GetStun()
        {
            return Stun;
        }

        public int GetBleed()
        {
            return Bleed;
        }

        public int GetDaze()
        {
            return Daze;
        }

        public bool GetFinishedTurn()
        {
            return FinishedTurn;
        }

        public bool GetFinishedLevelUp()
        {
            return FinishedLevelUp;
        }

        public bool GetFinishedGame()
        {
            return FinishedGame;
        }

        public bool[] GetUnlockedSkill()
        {
            return UnlockedSkill;
        }

        public void SetName(string newName)
        {
            Name = newName;
        }

        public void SetLevel(int newLevel)
        {
            Level = newLevel;
        }

        public void SetStrength(int newStrength)
        {
            Strength = newStrength;
        }

        public void SetAgility(int newAgility)
        {
            Agility = newAgility;
        }

        public void SetVitality(int newVitality)
        {
            Vitality = newVitality;
        }

        public void SetHealth(int newHealth)
        {
            Health = newHealth;

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }

        public void SetMaxHealth(int newMaxHealth)
        {
            MaxHealth = newMaxHealth;
        }

        public void SetEnergy(int newEnergy)
        {
            Energy = newEnergy;
        }

        public void SetMaxEnergy(int newMaxEnergy)
        {
            MaxEnergy = newMaxEnergy;
        }

        public void SetStun(int newStun)
        {
            Stun = newStun;
        }

        public void SetBleed(int newBleed)
        {
            Bleed = newBleed;
        }

        public void SetDaze(int newDaze)
        {
            Daze = newDaze;
        }

        public void SetFinishedTurn(bool newFinishedTurn)
        {
            FinishedTurn = newFinishedTurn;
        }

        public void SetFinishedLevelUp(bool newFinishedLevelUp)
        {
            FinishedLevelUp = newFinishedLevelUp;
        }

        public void SetFinishedGame(bool newFinishedGame)
        {
            FinishedGame = newFinishedGame;
        }

        public void SetUnlockSkill(int id, bool newState)
        {
            UnlockedSkill[id] = newState;
        }
    }
}
