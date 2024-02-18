using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    enum TypeTile 
    {
        Combat,marchand,none,walk,healCenter
    }
    internal class Tile
    {
        public TypeTile type;

        public Tile(TypeTile aType) 
        {
            type = aType;
        }

        public void showTile()
        {
            switch (type) 
            {
                case TypeTile.Combat:
                    Console.Write(" X ");
                    break; 
                case TypeTile.marchand:
                    Console.Write(" * ");
                    break; 
                case TypeTile.none:
                    Console.Write(' ');
                    break;
                case TypeTile.walk:
                    Console.Write(" O ");
                    break;
                case TypeTile.healCenter:
                    Console.Write(" H ");
                    break;
                default:
                    Console.Write(' ');
                    break;
            }
        }

    }
}
