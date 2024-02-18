using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pokemonLike
{
    internal class CombatTile : Tile
    {
        public int chanceEncounter = 70;
        public string[] entityName = new string[3];
        public Monster currentMonster;
        public bool alreadyFight = false;

        public CombatTile() : base(TypeTile.Combat)
        {
            entityName[0] = "Treant";
            entityName[1] = "Ogre";
            entityName[2] = "Naga";
        }

        public void startCombat(Player player)
        {
            Console.WriteLine("Start combat");
            Random random = new Random();
            int encounterIndex = random.Next(0, 3);
            currentMonster = new Monster();
            currentMonster.name = entityName[encounterIndex];
            int levelMin = player.team[1].level < player.team[2].level ? player.team[1].level : player.team[2].level;
            levelMin = levelMin < player.team[3].level ? levelMin : player.team[3].level;
            int levelToGive = levelMin + random.Next(-2,3);
            if (levelToGive == 0) levelToGive = 1;
            currentMonster.setLevel(levelToGive);
            currentMonster.setInfo();
        }

        public void showDetailledTile(Hero hero) 
        {
            drawHorizontalLine();
            Console.Write("\n");
            drawNameLine(currentMonster.name);
            Console.Write("\n");
            drawHorizontalLineLifeBar(currentMonster);
            Console.Write("\n");
            for (int i = 0;i < 2; i++) 
            {
                drawVerticalLine();
                Console.WriteLine();
            }
            drawSeparationLine();
            Console.Write("\n");
            for (int i = 0; i < 2; i++)
            {
                drawVerticalLine();
                Console.WriteLine();
            }
            drawHorizontalLineLifeBar(hero);
            Console.Write("\n");
            drawHeroLine(hero.name,hero.pm);
            Console.Write("\n");
            drawHorizontalLine();

        }

        void drawHorizontalLine()
        {
            for (int i = 0; i < 30; i++) { Console.Write("-"); }
        }

        void drawHorizontalLineLifeBar(Entity entite)
        {
            Console.Write("|");
            for (int i = 0; i < (28 - 12) / 2; i++) { Console.Write(" "); }
            entite.showLifeBar();
            for (int i = 0; i < (28 - 12) / 2; i++) { Console.Write(" "); }
            Console.Write("|");
        }

        void drawVerticalLine()
        {
            Console.Write("|");
            for (int j = 0; j < 28; j++)
            {
                Console.Write(" ");
            }
            Console.Write("|");
        }

        void drawSeparationLine()
        {
            Console.Write("|");
            for (int j = 0; j < 28; j++)
            {
                Console.Write("-");
            }
            Console.Write("|");
        }

        void drawNameLine(string name)
        {
            Console.Write("|");
            for (int i = 0; i < (28 - name.Length) / 2; i++) { Console.Write(" "); }
            Console.Write(name);
            for (int i = 0; i < (28 - name.Length) / 2; i++) { Console.Write(" "); }
            Console.Write("|");
        }

        void drawHeroLine(string name,int pm)
        {
            string pmS = pm.ToString() + "pm";
            Console.Write("|");
            for (int i = 0; i < (28 - name.Length - pmS.Length) / 2; i++) { Console.Write(" "); }
            Console.Write(name);
            Console.Write(" " + pmS);
            for (int i = 0; i < (28 - name.Length - pmS.Length) / 2; i++) { Console.Write(" "); }
            Console.Write("|");
        }
    }
}
