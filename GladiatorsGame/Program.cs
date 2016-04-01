using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    TODO:
    - [DONE] Implement title screen
    - [DONE] Fix Random
    - [DONE] Implement turns (and fix enemy attack on incorrect player input)
    - [DONE] Implement random chance check (percentage)
    - [DONE] Add color coding for combat log
    - [DONE] Implement stat upgrades on level up
    - [DONE] Implement skill unlock on levels 3, 5 and 7
    - [DONE] Implement skill scaling with strength
    - [DONE] Implement ending after level 100.
    - Implement additional skills (+ states)
    - Implement skill costs
    - Implement enemy scaling
    - Implement enemy affixes
    - Write tutorial for title screen
    - Add multiple death strings for enemies.
    - Implement proper pauses with timers, not Threading.Sleep
*/

namespace GladiatorsGame
{
    class MainGame
    {
        static Player Player1 = new Player();
        static Enemy CurrentEnemy = new Enemy();
        static Log CombatLog = new Log();

        static void Main()
        {
            Console.Title = "Gladiators (Prototype)";

            //Main game loop
            while (true)
            {
                //Title Screen
                Console.Clear();
                UserInterface.DrawHorizontalLine();
                UserInterface.DrawTitle();
                UserInterface.DrawHorizontalLine();
                Console.WriteLine("Press any key to start the game.");
                Console.ReadKey();

                //In-Game
                Player1.Initialize();
                CombatLog.WriteLine("Welcome to The Arena! Fight for your live with the number keys.", ConsoleColor.Yellow);

                //Gameplay Session Loop
                do
                {
                    CurrentEnemy.Generate();
                    CombatLog.WriteLine("You are now fighting " + CurrentEnemy.GetName() + ".", ConsoleColor.Yellow);

                    do
                    {
                        Player1.SetFinishedTurn(false);
                        UpdateScreen();

                        //Get player input and use skill accordingly
                        switch (GetPlayerInput())
                        {
                            case "D1":
                                if (Player1.GetUnlockedSkill()[0] == true)
                                {
                                    CombatLog.WriteLine(Player1.Skill_HeroicAssault(CurrentEnemy), ConsoleColor.Green);
                                    Player1.SetFinishedTurn(true);
                                }
                                break;
                            case "D2":
                                if (Player1.GetUnlockedSkill()[1] == true)
                                {
                                    CombatLog.WriteLine(Player1.Skill_DesperateStrike(CurrentEnemy), ConsoleColor.Green);
                                    Player1.SetFinishedTurn(true);
                                }
                                break;
                            case "D3":
                                if (Player1.GetUnlockedSkill()[2] == true)
                                {
                                    CombatLog.WriteLine(Player1.Skill_Bash(CurrentEnemy), ConsoleColor.Green);
                                    Player1.SetFinishedTurn(true);
                                }
                                break;
                            case "D4":
                                if (Player1.GetUnlockedSkill()[3] == true)
                                {
                                    CombatLog.WriteLine(Player1.Skill_DirtThrow(CurrentEnemy), ConsoleColor.Green);
                                    Player1.SetFinishedTurn(true);
                                }
                                break;
                            case "D5":
                                if (Player1.GetUnlockedSkill()[4] == true)
                                {
                                    CombatLog.WriteLine(Player1.Skill_SavageCut(CurrentEnemy), ConsoleColor.Green);
                                    Player1.SetFinishedTurn(true);
                                }
                                break;
                        }

                        UpdateScreen();
                        System.Threading.Thread.Sleep(250);

                        //Enemy reaction (attack or death)
                        if (CurrentEnemy.GetHealth() <= 0)
                        {
                            CombatLog.WriteLine(CurrentEnemy.GetName() + " falls breathless before you.", ConsoleColor.Yellow);
                        }
                        else if (Player1.GetFinishedTurn() == true)
                        {
                            CombatLog.WriteLine(CurrentEnemy.Attack(Player1), ConsoleColor.Red);
                        }
                    } while ((CurrentEnemy.GetHealth() >= 1) && (Player1.GetHealth() >= 1));

                    if (Player1.GetLevel() == 100)
                    {
                        Player1.SetFinishedGame(true);
                        break;
                    }

                    //Level Up
                    if (Player1.GetHealth() >= 1)
                    {
                        CombatLog.WriteLine(Player1.LevelUp(), ConsoleColor.Cyan);
                        CombatLog.WriteLine("Choose stat to level up:", ConsoleColor.Cyan);
                        CombatLog.WriteLine("1. Strength    2. Agility    3. Vitality", ConsoleColor.Cyan);
                        UpdateScreen();

                        while (Player1.GetFinishedLevelUp() == false)
                        {
                            switch (GetPlayerInput())
                            {
                                case "D1":
                                    Player1.SetStrength(Player1.GetStrength() + 1);
                                    Player1.SetFinishedLevelUp(true);
                                    break;
                                case "D2":
                                    Player1.SetAgility(Player1.GetAgility() + 1);
                                    Player1.SetFinishedLevelUp(true);
                                    break;
                                case "D3":
                                    Player1.SetVitality(Player1.GetVitality() + 1);
                                    Player1.SetFinishedLevelUp(true);
                                    break;
                            }
                        }

                        Player1.UpdateStats();
                    }
                } while (Player1.GetHealth() >= 1);

                //Game Over
                if (Player1.GetFinishedGame() == true)
                {
                    CombatLog.WriteLine("You have finished level 100 and earned your freedom. Congratulations!", ConsoleColor.Cyan);
                    CombatLog.WriteLine("Press any key to return to title screen.", ConsoleColor.Cyan);
                }
                else
                {
                    CombatLog.WriteLine("You are dead. Press any key to return to title screen.", ConsoleColor.Yellow);
                }
                UpdateScreen();
                Console.ReadKey();
                CombatLog.Clear();
            }
        }

        static void UpdateScreen()
        {
            Console.Clear();

            UserInterface.DrawHorizontalLine();
            UserInterface.DrawEnemyInfo(CurrentEnemy.GetName(), CurrentEnemy.GetHealth(), CurrentEnemy.GetMaxHealth(), CurrentEnemy.GetStun(), CurrentEnemy.GetBleed(), CurrentEnemy.GetDaze());
            UserInterface.DrawHorizontalLine();
            UserInterface.DrawCombatLog(CombatLog.GetLineText(), CombatLog.GetLineColor());
            UserInterface.DrawHorizontalLine();            
            UserInterface.DrawPlayerInfo(Player1.GetName(), Player1.GetLevel(), Player1.GetStrength(), Player1.GetAgility(), Player1.GetVitality(), Player1.GetHealth(), Player1.GetMaxHealth(), Player1.GetEnergy(), Player1.GetMaxEnergy(), Player1.GetStun(), Player1.GetBleed(), Player1.GetDaze());
            UserInterface.DrawHorizontalLine();
            UserInterface.DrawSkillBar(Player1.GetUnlockedSkill());
            UserInterface.DrawHorizontalLine();
        }

        static string GetPlayerInput()
        {
            string PlayerInput;
            PlayerInput = Console.ReadKey().Key.ToString();

            return PlayerInput;
        }
    }

    class GameLogic
    {
        public static bool CheckChance(int Chance)
        {
            int ChanceRoll;
            bool ChanceResult;
            Random RNGesus = new Random();

            ChanceRoll = RNGesus.Next(0, 101);

            if (ChanceRoll <= Chance)
            {
                ChanceResult = true;
            }
            else
            {
                ChanceResult = false;
            }

            return ChanceResult;
        }
    }

    class UserInterface
    {
        public static void DrawTitle()
        {
            Console.WriteLine("Gladiators (Prototype) - Version 1.0");
        }

        public static void DrawHorizontalLine()
        {
            Console.WriteLine("----------------------------------------------------------------");
        }

        public static void DrawPlayerInfo(string Name, int Level, int Strength, int Agility, int Vitality, int Health, int MaxHealth, int Energy, int MaxEnergy, int Stun, int Bleed, int Daze)
        {
            string Status;
            Console.ForegroundColor = ConsoleColor.Green;

            Status = "Gladiator " + Name + " - Level " + Level.ToString() + " ";

            if (Stun >= 1)
            {
                Status = Status + "Stun(" + Stun.ToString() + ") ";
            }

            if (Bleed >= 1)
            {
                Status = Status + "Bleed(" + Bleed.ToString() + ") ";
            }

            if (Daze >= 1)
            {
                Status = Status + "Daze(" + Daze.ToString() + ") ";
            }

            Console.WriteLine(Status);
            Console.WriteLine("Strength: "+ Strength.ToString() + "         Agility: " + Agility.ToString() + "         Vitality: " + Vitality.ToString());
            Console.WriteLine("Health: " + Health.ToString() + "/" + MaxHealth.ToString() + "      Energy: " + Energy.ToString() + "/" + MaxEnergy.ToString());

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void DrawEnemyInfo(string Name, int Health, int MaxHealth, int Stun, int Bleed, int Daze)
        {
            string Status;
            Console.ForegroundColor = ConsoleColor.Red;

            Status = "Opponent: " + Name + " ";

            if (Stun >= 1)
            {
                Status = Status + "Stun(" + Stun.ToString() + ") ";
            }

            if (Bleed >= 1)
            {
                Status = Status + "Bleed(" + Bleed.ToString() + ") ";
            }

            if (Daze >= 1)
            {
                Status = Status + "Daze(" + Daze.ToString() + ") ";
            }

            Console.WriteLine(Status);
            Console.WriteLine("Health: " + Health.ToString() + "/" + MaxHealth.ToString());

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void DrawCombatLog(string[] LineText, ConsoleColor[] LineColor)
        {
            for (int i = 0; i <= LineText.Length - 1; i++)
            {
                Console.ForegroundColor = LineColor[i];
                Console.WriteLine(LineText[i]);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public static void DrawSkillBar(bool[] UnlockedSkill)
        {
            string[] SkillText = new string[5] { "1. Heroic Assault (0)", "2. Desperate Strike (0)", "3. Bash (0)", "4. Dirt Throw (0)", "5. Savage Cut (0)" };
            Console.WriteLine("Available Skills:");
            for (int i = 0; i <= 4; i++)
            {
                if (UnlockedSkill[i] == true)
                {
                    Console.WriteLine(SkillText[i]);
                }
            }
        }
    }

    class Log
    {
        private string[] LineText = new string[10];
        private ConsoleColor[] LineColor = new ConsoleColor[10];

        public void WriteLine(string NewLineText, ConsoleColor NewLineColor)
        {
            LineText[0] = LineText[1];
            LineText[1] = LineText[2];
            LineText[2] = LineText[3];
            LineText[3] = LineText[4];
            LineText[4] = LineText[5];
            LineText[5] = LineText[6];
            LineText[6] = LineText[7];
            LineText[7] = LineText[8];
            LineText[8] = LineText[9];
            LineText[9] = NewLineText;

            LineColor[0] = LineColor[1];
            LineColor[1] = LineColor[2];
            LineColor[2] = LineColor[3];
            LineColor[3] = LineColor[4];
            LineColor[4] = LineColor[5];
            LineColor[5] = LineColor[6];
            LineColor[6] = LineColor[7];
            LineColor[7] = LineColor[8];
            LineColor[8] = LineColor[9];
            LineColor[9] = NewLineColor;
        }

        public string[] GetLineText()
        {
            return LineText;
        }

        public ConsoleColor[] GetLineColor()
        {
            return LineColor;
        }

        public void Clear()
        {
            Array.Clear(LineText, 0, 10);
            Array.Clear(LineColor, 0, 10);
        }
    }

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

        public string Skill_Bash(Enemy Target)
        {
            //NYI
            return null;
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

        public void SetHealth (int newHealth)
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