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
}