using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorsGame
{
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
}
