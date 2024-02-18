using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    internal class Screen
    {
        public Map map;
        public Player player;
        public bool active;

        public int choiceNumber;
        public int cursorIndex;

        public GamePhase gamePhaseToReturn;
        public GamePhase previousGamePhase;

        public Screen(Map map, Player player, GamePhase previousGamePhase)
        {
            this.map = map;
            this.player = player;
            active = false;
            this.previousGamePhase = previousGamePhase;
        }

        public virtual GamePhase Start() { return GamePhase.combat; }
        public virtual void StartLoop() { }
        public virtual void CheckInput() { }
        public virtual void CheckCursorInput() { }

        public void showSwitch()
        {
            for (int i = 1; i < player.team.Count + 1; i++)
            {
                if (cursorIndex == i)
                {
                    drawCursor();
                }
                player.team[i].showInfo();
            }
            choiceNumber = player.team.Count;
        }

        public void showMagie()
        {
            int avaibleMagiesNumber = 0;
            for (int i = 0; i < player.team[1].magies.Length; i++)
            {
                if (player.team[1].magies[i] != 0)
                {
                    if (cursorIndex == i + 1)
                    {
                        drawCursor();
                    }
                    InfoManager.Instance.magieDex[player.team[1].magies[i]].showInfo();
                    avaibleMagiesNumber++;
                }
            }
            if (avaibleMagiesNumber == 0)
            {
                Console.WriteLine("Vous n'avez pas de magie disponible");
            }
            else
            {
                choiceNumber = avaibleMagiesNumber;
            }

        }

        public void showAction()
        {
            for (int i = 1; i <= 4; i++)
            {
                if (cursorIndex == i)
                {
                    drawCursor();
                }
                switch (i)
                {
                    case 1:
                        Console.Write("Attaque    ");
                        break;
                    case 2:
                        Console.Write("Magie    ");
                        break;
                    case 3:
                        Console.Write("Objet    ");
                        break;
                    case 4:
                        Console.Write("Change    ");
                        break;
                }
            }
        }

        public int showItemsUsableInCombat(List<int> items)
        {
            int nbItems = 0;
            for (int i = 0; i < items.Count; i++)
            {
                Item item = InfoManager.Instance.itemDex[items[i]];
                if (item.usableInCombat)
                {
                    if (cursorIndex == i + 1)
                    {
                        drawCursor();
                    }
                    item.showItem();
                    Console.Write("\n");
                    //Console.WriteLine(item.name + " " + item.value + " : " + item.description);
                    nbItems++;
                }
            }
            return nbItems;
        }

        public int showItemsUsableInOverworld(List<int> items)
        {
            int nbItems = 0;
            for (int i = 0; i < items.Count; i++)
            {
                Item item = InfoManager.Instance.itemDex[items[i]];
                if (item.usableInOverworld)
                {
                    if (cursorIndex == i + 1)
                    {
                        drawCursor();
                    }
                    item.showItem();
                    Console.Write("\n");
                    //Console.WriteLine(item.name + " " + item.value + " : " + item.description);
                    nbItems++;
                }
            }
            return nbItems;
        }

        public void drawCursor()
        {
            Console.Write("> ");
        }
    }
}
