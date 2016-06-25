using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorsGame
{
    class MainGame
    {
        static Player player1 = new Player();
        static Enemy currentEnemy = new Enemy();
        static Log combatLog = new Log();

        static void Main()
        {
            Console.Title = "Gladiators";

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
                player1.Initialize();
                combatLog.WriteLine("Welcome to The Arena! Fight for your live with the number keys.", ConsoleColor.Yellow);

                //Gameplay Session Loop
                do
                {
                    currentEnemy.Generate();
                    combatLog.WriteLine("You are now fighting " + currentEnemy.GetName() + ".", ConsoleColor.Yellow);

                    do
                    {
                        //Player Turn
                        player1.SetFinishedTurn(false);
                        UpdateScreen();

                        switch (GetPlayerInput())
                        {
                            case "D1":
                                if (player1.GetUnlockedSkill()[0] == true)
                                {
                                    combatLog.WriteLine(player1.Skill_HeroicAssault(currentEnemy), ConsoleColor.Green);
                                    player1.SetFinishedTurn(true);
                                }
                                break;
                            case "D2":
                                if (player1.GetUnlockedSkill()[1] == true)
                                {
                                    combatLog.WriteLine(player1.Skill_DesperateStrike(currentEnemy), ConsoleColor.Green);
                                    player1.SetFinishedTurn(true);
                                }
                                break;
                            case "D3":
                                if (player1.GetUnlockedSkill()[2] == true)
                                {
                                    combatLog.WriteLine(player1.Skill_Bash(currentEnemy), ConsoleColor.Green);
                                    player1.SetFinishedTurn(true);
                                }
                                break;
                            case "D4":
                                if (player1.GetUnlockedSkill()[3] == true)
                                {
                                    combatLog.WriteLine(player1.Skill_DirtThrow(currentEnemy), ConsoleColor.Green);
                                    player1.SetFinishedTurn(true);
                                }
                                break;
                            case "D5":
                                if (player1.GetUnlockedSkill()[4] == true)
                                {
                                    combatLog.WriteLine(player1.Skill_SavageCut(currentEnemy), ConsoleColor.Green);
                                    player1.SetFinishedTurn(true);
                                }
                                break;
                        }

                        UpdateScreen();
                        System.Threading.Thread.Sleep(250);

                        //Enemy Turn
                        if (currentEnemy.GetBleed() >= 1)
                        {
                            combatLog.WriteLine(currentEnemy.GetName() + " bleeds for " + currentEnemy.GetBleedDamage() + " damage!", ConsoleColor.Green);
                            currentEnemy.SetHealth(currentEnemy.GetHealth() - currentEnemy.GetBleedDamage());
                            currentEnemy.SetBleed(currentEnemy.GetBleed() - 1);
                        }

                        if (currentEnemy.GetHealth() <= 0)
                        {
                            combatLog.WriteLine(currentEnemy.GetName() + " falls breathless before you.", ConsoleColor.Yellow);
                        }
                        else if (player1.GetFinishedTurn() == true)
                        {
                            if (currentEnemy.GetStun() >= 1)
                            {
                                currentEnemy.SetStun(currentEnemy.GetStun() - 1);
                                combatLog.WriteLine(currentEnemy.GetName() + " is stunned and skips a turn.", ConsoleColor.Red);
                            }
                            else
                            {
                                if (currentEnemy.GetDaze() >= 1)
                                {
                                    currentEnemy.SetDaze(currentEnemy.GetDaze() - 1);

                                    if (GameLogic.CheckChance(50) == true)
                                    {
                                        combatLog.WriteLine(currentEnemy.GetName() + "'s attack missed you!", ConsoleColor.Red);
                                    }
                                    else
                                    {
                                        combatLog.WriteLine(currentEnemy.Attack(player1), ConsoleColor.Red);
                                    }   
                                }
                                else
                                {
                                    combatLog.WriteLine(currentEnemy.Attack(player1), ConsoleColor.Red);
                                }      
                            }
                            
                        }

                    } while ((currentEnemy.GetHealth() >= 1) && (player1.GetHealth() >= 1));

                    if (player1.GetLevel() == 100)
                    {
                        player1.SetFinishedGame(true);
                        break;
                    }

                    //Level Up
                    if (player1.GetHealth() >= 1)
                    {
                        combatLog.WriteLine(player1.LevelUp(), ConsoleColor.Cyan);
                        combatLog.WriteLine("Choose stat to level up:", ConsoleColor.Cyan);
                        combatLog.WriteLine("1. Strength    2. Agility    3. Vitality", ConsoleColor.Cyan);
                        UpdateScreen();

                        while (player1.GetFinishedLevelUp() == false)
                        {
                            switch (GetPlayerInput())
                            {
                                case "D1":
                                    player1.SetStrength(player1.GetStrength() + 1);
                                    player1.SetFinishedLevelUp(true);
                                    break;
                                case "D2":
                                    player1.SetAgility(player1.GetAgility() + 1);
                                    player1.SetFinishedLevelUp(true);
                                    break;
                                case "D3":
                                    player1.SetVitality(player1.GetVitality() + 1);
                                    player1.SetFinishedLevelUp(true);
                                    break;
                            }
                        }

                        player1.UpdateStats();
                    }
                } while (player1.GetHealth() >= 1);

                //Game Over
                if (player1.GetFinishedGame() == true)
                {
                    combatLog.WriteLine("You have finished level 100 and earned your freedom. Congratulations!", ConsoleColor.Cyan);
                    combatLog.WriteLine("Press any key to return to title screen.", ConsoleColor.Cyan);
                }
                else
                {
                    combatLog.WriteLine("You are dead. Press any key to return to title screen.", ConsoleColor.Yellow);
                }
                UpdateScreen();
                Console.ReadKey();
                combatLog.Clear();
            }
        }

        static void UpdateScreen()
        {
            Console.Clear();

            UserInterface.DrawHorizontalLine();
            UserInterface.DrawEnemyInfo(currentEnemy.GetName(), currentEnemy.GetHealth(), currentEnemy.GetMaxHealth(), currentEnemy.GetStun(), currentEnemy.GetBleed(), currentEnemy.GetDaze());
            UserInterface.DrawHorizontalLine();
            UserInterface.DrawCombatLog(combatLog.GetLineText(), combatLog.GetLineColor());
            UserInterface.DrawHorizontalLine();            
            UserInterface.DrawPlayerInfo(player1.GetName(), player1.GetLevel(), player1.GetStrength(), player1.GetAgility(), player1.GetVitality(), player1.GetHealth(), player1.GetMaxHealth(), player1.GetEnergy(), player1.GetMaxEnergy(), player1.GetStun(), player1.GetBleed(), player1.GetDaze());
            UserInterface.DrawHorizontalLine();
            UserInterface.DrawSkillBar(player1.GetUnlockedSkill());
            UserInterface.DrawHorizontalLine();
        }

        static string GetPlayerInput()
        {
            string playerInput;
            playerInput = Console.ReadKey().Key.ToString();

            return playerInput;
        }
    }
}