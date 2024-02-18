using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    struct ItemPrice
    {
        public int itemIndex;
        public int price;

        public ItemPrice(int itemIndex, int price)
        {
            this.itemIndex = itemIndex;
            this.price = price;
        }
    }
    internal class MarchandScreen : Screen
    {
        public List<ItemPrice> catalogue;
        public MarchandScreen(Map map,Player player) : base(map,player,GamePhase.movement) 
        {
            catalogue = new List<ItemPrice>();

            catalogue.Add(new ItemPrice(1, 5));
            catalogue.Add(new ItemPrice(2, 15));
            catalogue.Add(new ItemPrice(3, 25));
            catalogue.Add(new ItemPrice(4, 20));
            catalogue.Add(new ItemPrice(5, 40));

        }

        public override GamePhase Start()
        {
            Console.Clear();
            active = true;
            choiceNumber = catalogue.Count;
            cursorIndex = 1;
            StartLoop();
            return gamePhaseToReturn;
        }

        public override void StartLoop()
        {            
            while(active)
            {
                Console.Clear();
                showCatalogue();
                Console.WriteLine();
                Console.WriteLine("Gold : " + player.gold);
                CheckCursorInput();
            }
            
        }

        public void showCatalogue()
        {
            for (int i = 0; i < catalogue.Count;i++)
            {
                if(cursorIndex == i +1)
                {
                    drawCursor();
                }
                InfoManager.Instance.itemDex[catalogue[i].itemIndex].showItem();
                Console.WriteLine(" " + catalogue[i].price + " gold");
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
                    case ConsoleKey.Enter:
                        if(player.gold >= catalogue[cursorIndex - 1].price)
                        {
                            acheterObjet();
                        }
                        return;
                    case ConsoleKey.E:
                        gamePhaseToReturn = GamePhase.movement;
                        active = false;
                        return;
                }
            }
        }

        public void acheterObjet()
        {
            player.addItem(catalogue[cursorIndex - 1].itemIndex);
            player.gold -= catalogue[cursorIndex - 1].price;
        }
    }
}
