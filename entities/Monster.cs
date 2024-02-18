using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    class Monster : Entity
    {
        public Monster() : base()
        {

        }

        public bool goodIA = false;

        public int expToGive = 3;
        public int goldToGive = 1;


        public string selectAttack(Hero currentHero,bool forTest = false)
        {
            int compteur = 0;
            foreach(int magie in magies)
            {
                if(magie != 0) 
                {
                    compteur++;
                }
            }
            int[] option = new int[compteur + 1];
            int bestOptionIndex = 0;
            if(compteur > 0)
            {
                if (this.magies[0] > 0 && InfoManager.Instance.magieDex[magies[bestOptionIndex]].cost <= pm)
                {
                    for (int i = 0; i < compteur; i++)
                    {
                        option[i] = calculateDamageFromMagie(currentHero, this.magies[i]);
                        if (pm >= InfoManager.Instance.magieDex[magies[bestOptionIndex]].cost
                            && option[i] > option[bestOptionIndex])
                        {
                            bestOptionIndex = i;
                        }
                        else if (option[i] == option[bestOptionIndex] &&
                                InfoManager.Instance.magieDex[magies[i]].cost < InfoManager.Instance.magieDex[magies[bestOptionIndex]].cost)
                        {
                            bestOptionIndex = i;
                        }

                    }
                    option[option.Count() - 1] = calculateDamageFromAttack(this, att);
                    if (option[option.Count() - 1] > option[bestOptionIndex])
                    {
                        bestOptionIndex = option.Count() - 1;
                        return attack(currentHero,forTest);
                        //return currentHero.takeDamageFromAttack(this, att);
                    }
                    //return currentHero.takeDamageFromMagie(this, magies[bestOptionIndex]);
                }
                return attackMagie(currentHero, magies[bestOptionIndex],forTest);
            }
            else
            {
                return attack(currentHero, forTest);
                //return currentHero.takeDamageFromAttack(this, att);
            }
        }
        public void calculateExpToGive()
        {
            expToGive += 5 * level / 7;
        }

        public void calculategoldToGive()
        {
            goldToGive += level / 4;
        }

        public override void setLevel(int level)
        {
            this.level = level;
            calculateExpToGive();
            calculategoldToGive();
            setInfo();
        }
    }
}
