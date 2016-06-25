using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorsGame
{
    class GameLogic
    {
        public static bool CheckChance(int chance)
        {
            int chanceRoll;
            bool chanceResult;
            Random RNGesus = new Random();

            chanceRoll = RNGesus.Next(0, 101);

            if (chanceRoll <= chance)
            {
                chanceResult = true;
            }
            else
            {
                chanceResult = false;
            }

            return chanceResult;
        }
    }
}
