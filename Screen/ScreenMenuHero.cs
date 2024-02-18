using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    internal class ScreenMenuHero : Screen
    {
        int index;
        public ScreenMenuHero(Map map,Player player,GamePhase previousGamePhase,int indexToShow) : base(map,player, previousGamePhase) 
        {
            index = indexToShow;
        }

        public override GamePhase Start()
        {
            Console.Clear();
            StartLoop();
            return gamePhaseToReturn;
        }

        public override void StartLoop()
        {
            Console.Clear();
            player.team[index].showInfoComplete();
            CheckCursorInput();
        }

        public override void CheckCursorInput()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.E:
                        gamePhaseToReturn = GamePhase.menuStat;
                        active = false;
                        return;
                }
            }
        }


    }
}
