using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    enum GameType { fire, water, grass }
    internal class gameTypeManager
    {
        static float getMultAttack(GameType attType, GameType defType)
        {
            if (attType == GameType.fire && defType == GameType.water
                || attType == GameType.water && defType == GameType.grass
                || attType == GameType.grass && defType == GameType.fire)
            {
                return 0.5f;
            }

            if (attType == GameType.fire && defType != GameType.grass
               || attType == GameType.water && defType == GameType.fire
               || attType == GameType.grass && defType == GameType.water)
            {
                return 2f;
            }

            return 1f;
        }
    }
}
