using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    internal class UnitTestScreen
    {
        public UnitTestScreen() { }

        public void start()
        {
            Console.Clear();
            // J'utilise les infos des pantins car leurs statistiques sont plus simple a calculer 
            Hero heroFeu = new Hero();
            heroFeu.name = "Pfeu";
            heroFeu.setInfo();
            Hero heroEau = new Hero();
            heroEau.name = "Peau";
            heroEau.setInfo();
            Hero heroPlante = new Hero();
            heroPlante.name = "Pplante";
            heroPlante.setInfo();


            Monster monsterFeu = new Monster();
            monsterFeu.name = "Pfeu";
            monsterFeu.setInfo();

            Monster monsterEau = new Monster();
            monsterEau.name = "Peau";
            monsterEau.setInfo();

            Monster monsterPlante = new Monster();
            monsterPlante.name = "Pplante";
            monsterPlante.setInfo();

            // Attaque de base (feu -> feu)
            monsterFeu.takeDamageFromAttack(heroFeu, heroFeu.att);
            Console.WriteLine("Attaque base feu -> feu : " + ((monsterFeu.currenthp == 8) ? true : monsterFeu.currenthp));
            // Attaque de base (feu -> eau)
            monsterEau.takeDamageFromAttack(heroFeu, heroFeu.att);
            Console.WriteLine("Attaque base feu -> eau : " + ((monsterFeu.currenthp == 8) ? true : monsterFeu.currenthp));

            monsterFeu.currenthp = monsterFeu.hp;
            monsterEau.currenthp = monsterEau.hp;

            Hero heroFeu10 = new Hero();
            heroFeu10.name = "Pfeu";
            heroFeu10.setLevel(10);

            monsterFeu.setLevel(10);
            monsterEau.setLevel(10);
            monsterPlante.setLevel(10);
            
            // Magie feu contre feu
            monsterFeu.takeDamageFromMagie(heroFeu10, heroFeu10.magies[0]);
            Console.WriteLine("Magie feu -> feu : " + ((monsterFeu.currenthp == 16) ? true : monsterFeu.currenthp));
            // Magie feu contre eau
            monsterEau.takeDamageFromMagie(heroFeu10, heroFeu10.magies[0]);
            Console.WriteLine("Magie feu -> eau : " + ((monsterEau.currenthp == 23) ? true : monsterEau.currenthp));
            // Magie feu contre plante
            monsterPlante.takeDamageFromMagie(heroFeu10, heroFeu10.magies[0]);
            Console.WriteLine("Magie feu -> plante : " + ((monsterPlante.currenthp == 2) ? true : monsterPlante.currenthp));

            // monstre mort
            monsterPlante.setLevel(1);
            monsterPlante.takeDamageFromMagie(heroFeu10, heroFeu10.magies[1]);
            Console.WriteLine("Monster dead : " + !monsterPlante.isAlive());


            monsterPlante.setLevel(20);
            heroFeu.setLevel(20);
            heroEau.setLevel(20);
            heroPlante.setLevel(20);
            // Monstre IA selectionne meilleure attaque contre hero eau avec pm
            monsterPlante.selectAttack(heroEau, true);
            Console.WriteLine("IA plante -> eau : " + ((heroEau.currenthp == 0) ? true : heroEau.currenthp));
            // Monstre IA selectionne meilleure attaque contre hero feu avec pm 
            monsterPlante.selectAttack(heroFeu, true);
            Console.WriteLine("IA plante -> feu : " + ((heroFeu.currenthp == 38) ? true : heroFeu.currenthp));
            // Monstre IA selectionne meilleure attaque contre hero plante avec pm 
            monsterPlante.selectAttack(heroPlante, true);
            Console.WriteLine("IA plante -> plante : " + ((heroPlante.currenthp == 8) ? true : heroPlante.currenthp));

            heroEau.currenthp = heroEau.hp;
            heroFeu.currenthp = heroFeu.hp;
            heroPlante.currenthp = heroPlante.hp;
            monsterPlante.pm = monsterPlante.maxPm / 2;
            // Monstre IA selectionne meilleure attaque contre hero eau avec moitie de pm
            monsterPlante.selectAttack(heroEau,true);
            Console.WriteLine("IA plante -> eau 50%pm: " + ((heroEau.currenthp == 0) ? true : heroEau.currenthp));
            // Monstre IA selectionne meilleure attaque contre hero feu avec moitie de pm
            monsterPlante.selectAttack(heroFeu, true);
            Console.WriteLine("IA plante -> feu 50%pm: " + ((heroFeu.currenthp == 38) ? true : heroFeu.currenthp));
            // Monstre IA selectionne meilleure attaque contre hero plante avec moitie de pm
            monsterPlante.selectAttack(heroPlante, true);
            Console.WriteLine("IA plante -> plante 50%pm : " + ((heroPlante.currenthp == 8) ? true : heroPlante.currenthp));


            heroEau.currenthp = heroEau.hp;
            heroFeu.currenthp = heroFeu.hp;
            heroPlante.currenthp = heroPlante.hp;
            monsterPlante.pm = 0;
            // Monstre IA selectionne meilleure attaque contre hero eau sans pm
            monsterPlante.selectAttack(heroEau, true);
            Console.WriteLine("IA plante -> eau 0%pm : " + ((heroEau.currenthp == 14) ? true : heroEau.currenthp));
            // Monstre IA selectionne meilleure attaque contre hero feu sans pm
            monsterPlante.selectAttack(heroFeu, true);
            Console.WriteLine("IA plante -> feu 0%pm : " + ((heroFeu.currenthp == 41) ? true : heroFeu.currenthp));
            // Monstre IA selectionne meilleure attaque contre hero plante sans pm
            monsterPlante.selectAttack(heroPlante, true);
            Console.WriteLine("IA plante -> plante 0%pm : " + ((heroPlante.currenthp == 32) ? true : heroPlante.currenthp));
            Environment.Exit(0);
        }
    }
}
