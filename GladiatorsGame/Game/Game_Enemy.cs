using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorsGame
{
    class Enemy
    {
        private string Name;
        private int Level, Health, MaxHealth;
        private int Stun, Bleed, Daze;

        public void Generate()
        {
            Random RNGesus = new Random();
            int NameID = RNGesus.Next(0, 10);

            switch (NameID)
            {
                case 0:
                    Name = "Gladiator";
                    break;
                case 1:
                    Name = "Prisoner";
                    break;
                case 2:
                    Name = "Criminal";
                    break;
                case 3:
                    Name = "Barbarian";
                    break;
                case 4:
                    Name = "Duelist";
                    break;
                case 5:
                    Name = "Lion";
                    break;
                case 6:
                    Name = "Tiger";
                    break;
                case 7:
                    Name = "Panther";
                    break;
                case 8:
                    Name = "Bull";
                    break;
                case 9:
                    Name = "Bear";
                    break;
            }

            Level = 1;

            MaxHealth = RNGesus.Next(95, 106);
            Health = MaxHealth;

            Stun = 0;
            Bleed = 0;
            Daze = 0;
        }

        public string Attack(Player Target)
        {
            Random RNGesus = new Random();
            int Damage;
            string LogText;

            Damage = RNGesus.Next(8, 13);
            Target.SetHealth(Target.GetHealth() - Damage);

            LogText = Name + " attacked you for " + Damage.ToString() + " damage.";
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

        public int GetHealth()
        {
            return Health;
        }

        public int GetMaxHealth()
        {
            return MaxHealth;
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

        public void SetName(string newName)
        {
            Name = newName;
        }

        public void SetLevel(int newLevel)
        {
            Level = newLevel;
        }

        public void SetHealth(int newHealth)
        {
            Health = newHealth;
        }

        public void SetMaxHealth(int newMaxHealth)
        {
            MaxHealth = newMaxHealth;
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
    }
}
