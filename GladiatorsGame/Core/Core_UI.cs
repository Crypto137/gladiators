﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorsGame
{
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

        public static void DrawPlayerInfo(string name, int level, int strength, int agility, int vitality, int health, int maxHealth, int energy, int maxEnergy, int stun, int bleed, int daze)
        {
            string status;
            Console.ForegroundColor = ConsoleColor.Green;

            status = "Gladiator " + name + " - Level " + level.ToString() + " ";

            if (stun >= 1)
            {
                status = status + "Stun(" + stun.ToString() + ") ";
            }

            if (bleed >= 1)
            {
                status = status + "Bleed(" + bleed.ToString() + ") ";
            }

            if (daze >= 1)
            {
                status = status + "Daze(" + daze.ToString() + ") ";
            }

            Console.WriteLine(status);
            Console.WriteLine("Strength: " + strength.ToString() + "         Agility: " + agility.ToString() + "         Vitality: " + vitality.ToString());
            Console.WriteLine("Health: " + health.ToString() + "/" + maxHealth.ToString() + "      Energy: " + energy.ToString() + "/" + maxEnergy.ToString());

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void DrawEnemyInfo(string name, int health, int maxHealth, int stun, int bleed, int daze)
        {
            string status;
            Console.ForegroundColor = ConsoleColor.Red;

            status = "Opponent: " + name + " ";

            if (stun >= 1)
            {
                status = status + "Stun(" + stun.ToString() + ") ";
            }

            if (bleed >= 1)
            {
                status = status + "Bleed(" + bleed.ToString() + ") ";
            }

            if (daze >= 1)
            {
                status = status + "Daze(" + daze.ToString() + ") ";
            }

            Console.WriteLine(status);
            Console.WriteLine("Health: " + health.ToString() + "/" + maxHealth.ToString());

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void DrawCombatLog(string[] lineText, ConsoleColor[] lineColor)
        {
            for (int i = 0; i <= lineText.Length - 1; i++)
            {
                Console.ForegroundColor = lineColor[i];
                Console.WriteLine(lineText[i]);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public static void DrawSkillBar(bool[] unlockedSkill)
        {
            string[] skillText = new string[5] { "1. Heroic Assault (0)", "2. Desperate Strike (0)", "3. Bash (0)", "4. Dirt Throw (0)", "5. Savage Cut (0)" };
            Console.WriteLine("Available Skills:");
            for (int i = 0; i <= 4; i++)
            {
                if (unlockedSkill[i] == true)
                {
                    Console.WriteLine(skillText[i]);
                }
            }
        }
    }

    class Log
    {
        private string[] lineText = new string[10];
        private ConsoleColor[] lineColor = new ConsoleColor[10];

        public void WriteLine(string newLineText, ConsoleColor newLineColor)
        {
            lineText[0] = lineText[1];
            lineText[1] = lineText[2];
            lineText[2] = lineText[3];
            lineText[3] = lineText[4];
            lineText[4] = lineText[5];
            lineText[5] = lineText[6];
            lineText[6] = lineText[7];
            lineText[7] = lineText[8];
            lineText[8] = lineText[9];
            lineText[9] = newLineText;

            lineColor[0] = lineColor[1];
            lineColor[1] = lineColor[2];
            lineColor[2] = lineColor[3];
            lineColor[3] = lineColor[4];
            lineColor[4] = lineColor[5];
            lineColor[5] = lineColor[6];
            lineColor[6] = lineColor[7];
            lineColor[7] = lineColor[8];
            lineColor[8] = lineColor[9];
            lineColor[9] = newLineColor;
        }

        public string[] GetLineText()
        {
            return lineText;
        }

        public ConsoleColor[] GetLineColor()
        {
            return lineColor;
        }

        public void Clear()
        {
            Array.Clear(lineText, 0, 10);
            Array.Clear(lineColor, 0, 10);
        }
    }
}
