using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    internal class ScreenMenuStat : Screen
    {
        int indexItemPlayerToUse = -1;
        public string itemResult = "";
        public ScreenMenuStat(Map map, Player player,GamePhase previousGamePhase,int indexItemPlayerToUse = -1) : base(map, player, GamePhase.movement)
        {
            active = true;
            cursorIndex = 1;
            choiceNumber = player.team.Count;
            this.indexItemPlayerToUse = indexItemPlayerToUse;
        }

        public override GamePhase Start()
        {
            Console.Clear();
            StartLoop();
            return gamePhaseToReturn;
        }

        public override void StartLoop()
        {
            while (active)
            {
                Console.Clear() ;
                for (int i = 1; i < choiceNumber + 1; i++)
                {
                    if (cursorIndex == i)
                    {
                        drawCursor();
                    }
                    player.team[i].showInfo();
                    Console.WriteLine("");
                }
                if(previousGamePhase != GamePhase.Object)
                {
                    Console.WriteLine();
                    Console.WriteLine("S : Met hero en premier");
                }
                
                CheckCursorInput();
            }

        }

        public override void CheckCursorInput()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (cursorIndex - 1 > 0)
                        {
                            cursorIndex--;
                        }
                        else
                        {
                            cursorIndex = choiceNumber;
                        }
                        return;
                    case ConsoleKey.RightArrow:
                        if (cursorIndex + 1 <= choiceNumber)
                        {
                            cursorIndex++;
                        }
                        else
                        {
                            cursorIndex = 1;
                        }
                        return;
                    case ConsoleKey.E:
                        gamePhaseToReturn = GamePhase.movement;
                        active = false;
                        return;
                    case ConsoleKey.S:
                        if(previousGamePhase != GamePhase.Object)
                        {
                            player.switchCurrentHero(cursorIndex);
                        }                       
                        return;
                    case ConsoleKey.Enter:
                        if(indexItemPlayerToUse != -1) 
                        {
                            itemResult = player.useItem(indexItemPlayerToUse - 1, cursorIndex);
                            active = false;
                        }
                        else
                        {
                            ScreenMenuHero screenMenuHero = new ScreenMenuHero(map, player, GamePhase.menuStat, cursorIndex);
                            screenMenuHero.Start();
                        }                        
                        return;
                }
            }
        }
    }
}
