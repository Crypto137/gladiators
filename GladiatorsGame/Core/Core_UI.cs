using System;
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
            Console.WriteLine("Strength: " + Strength.ToString() + "         Agility: " + Agility.ToString() + "         Vitality: " + Vitality.ToString());
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
}
