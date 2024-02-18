using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    class Hero : Entity
    {
        public Hero() : base()
        {
            setNextLevelExpNeeded();
        }

        public int experience = 0;
        public int expNeedNextLevel = 0;

        private string levelUpString = "";

        public string addExperience(int value, bool first = true)
        {
            if (first)
            {
                levelUpString = "";
                levelUpString += name + " a gagne " + value + " experience\n";
            }
            experience += value;
            if (experience >= expNeedNextLevel)
            {
                int toAddLater = experience - expNeedNextLevel;
                levelUpString += levelUp() + "\n";
                addExperience(toAddLater, false);
            }
            return levelUpString;
        }

        public string levelUp()
        {
            level++;
            setInfo();
            setNextLevelExpNeeded();
            return name + " a Gagne un niveau !" + (level - 1) + " -> " + level;
        }

        public void setNextLevelExpNeeded()
        {
            expNeedNextLevel = (int)(0.8f * (level + 1) * (level + 1) * (level + 1));
        }


        public void showInfo()
        {
            Console.Write(name + " : " + "lvl " + level + " " + currenthp + "/" + hp + "\n");
        }

        public void showInfoComplete()
        {
            Console.WriteLine(name + " : ");
            Console.WriteLine("lvl " + level);
            Console.WriteLine("Hp : " + currenthp + " / " + hp);
            Console.WriteLine("Attaque : " + att + " ");
            Console.WriteLine("Defense : " + def + " ");
            Console.WriteLine("Magie : " + pm + " ");
            Console.WriteLine("Precision : " + precision + " ");
            Console.WriteLine("Vitesse : " + vitesse + " ");
            Console.WriteLine("Exp : " + experience + "/" + expNeedNextLevel);
            Console.WriteLine();
            Console.WriteLine("Magie :");
            Console.WriteLine();

            foreach (int magIndex in magies)
            {
                if (magIndex != 0)
                {
                    InfoManager.Instance.magieDex[magIndex].showInfo();
                }
            }
        }

        public string Heal(int value)
        {
            currenthp += value;
            if(currenthp > hp) currenthp = hp;
            return name + " a ete soigne de " + value + " hp";
        }

        public string Revive(int value) 
        {
            currenthp += value;
            if (currenthp > hp) currenthp = hp;
            return name + " a ete reanime";
        }

        public override void setLevel(int level)
        {
            base.setLevel(level);
            setNextLevelExpNeeded();
        }

    }
}
