using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    internal class Magie
    {
        public string name = "DefaultMagie";
        public int valeur = 10;
        public GameType type = GameType.fire;
        public int cost;
        public int precision = 100;

        public Magie(string name,int valeur,GameType type,int cost,int precision) 
        {
            this.name = name;
            this.valeur = valeur;
            this.type = type;
            this.cost = cost;
            this.precision = precision;
        }

        public void showInfo()
        {
            Console.WriteLine(name + " " +  valeur + " " + type + " " + cost + " pm " + precision + " precision"); 
        }
    }
}
