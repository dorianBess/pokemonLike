using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    struct Vector2Int
    {
        public int x;
        public int y;

        public Vector2Int(int x,int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    class Player
    {
        public Vector2Int Position;
        public Dictionary<int, Hero> team;
        public int gold;
        public List<int> items;
        public Player()
        {
            team = new Dictionary<int, Hero>();

            Position = new Vector2Int();

            Hero mage = new Hero();
            mage.level = 1;
            mage.name = "Mage";
            mage.setType(GameType.water);
            mage.setInfo();
            Hero archer = new Hero();
            archer.level = 1;
            archer.name = "Archer";
            archer.setType(GameType.grass);
            archer.setInfo();
            Hero warrior = new Hero();
            warrior.level = 1;
            warrior.name = "Warrior";
            warrior.setType(GameType.fire);
            warrior.setInfo();

            team.Add(1, mage);
            team.Add(2, archer);
            team.Add(3, warrior);

            items = new List<int>();

            gold = 0;
        }

        public void move(Direction dir)
        {
            switch (dir)
            {
                case Direction.top:
                    Position.y--;
                    break;
                case Direction.left:
                    Position.x--;
                    break;
                case Direction.down:
                    Position.y++;
                    break;
                case Direction.right:
                    Position.x++;
                    break;

            }
        }

        public void loadInfo()
        {

        }

        public string addGold(int value)
        {
            gold += value;
            return "Vous avez gagne " + value + " gold";
        }

        public void switchCurrentHero(int IndexHeroToPutFirst)
        {
            Hero saveHero = team[1];
            team[1] = team[IndexHeroToPutFirst];
            team[IndexHeroToPutFirst] = saveHero;
        }

        public string useItem(int indexItemToUse,int indexHero)
        {
            string toReturn = InfoManager.Instance.itemDex[items[indexItemToUse]].UseItem(team[indexHero]);
            if (toReturn == "")
            {
                toReturn = "Cet objet n'a aucune utilite sur ce hero";
            }
            else 
            {
                items.RemoveAt(indexItemToUse);
            }            
            return toReturn;
        }

        public void addItem(int itemIndex)
        {
            items.Add(itemIndex);
        }

        
    }
}
