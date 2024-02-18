using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    internal class ObjectScreen : Screen
    {
        public string itemResult = "";
        public ObjectScreen(Map map, Player player, GamePhase previousGamePhase) : base(map, player, previousGamePhase)
        {

        }

        public override GamePhase Start()
        {
            Console.Clear();
            active = true;
            cursorIndex = 1;
            StartLoop();            
            return gamePhaseToReturn;
        }

        public override void StartLoop()
        {
            while (active)
            {
                Console.Clear();
                if (previousGamePhase == GamePhase.movement)
                {
                    choiceNumber = showItemsUsableInOverworld(player.items);
                }
                else if (previousGamePhase == GamePhase.combat)
                {
                    choiceNumber = showItemsUsableInCombat(player.items);
                }
                if(choiceNumber == 0) 
                {
                    Console.WriteLine("Vous n'avez pas d'objet utilisable");
                }
                Console.WriteLine();
                Console.Write(itemResult);
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
                        gamePhaseToReturn = previousGamePhase;
                        active = false;
                        return;
                    case ConsoleKey.Enter:
                        if (choiceNumber == 0) return;
                        ScreenMenuStat screenMenuStat = new ScreenMenuStat(map, player, GamePhase.Object,cursorIndex);
                        screenMenuStat.Start();
                        itemResult = screenMenuStat.itemResult;
                        cursorIndex = 1;
                        if(previousGamePhase == GamePhase.combat && itemResult != "Cet objet n'a aucune utilite sur ce hero") 
                        {
                            active = false;
                        }
                        return;
                }
            }
        }
    }
}
