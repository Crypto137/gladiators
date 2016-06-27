using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorsGame
{
    class GameLogic
    {
        private static Random rnd = new Random();

        public static bool CheckChance(int chance)
        {
            int chanceRoll;
            bool chanceResult;

            chanceRoll = rnd.Next(0, 101);

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
