using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    enum ItemEffect {Heal,Revive }
    internal class Item
    {
        public string name;
        public int value;
        public string description;
        public ItemEffect effect;

        public bool usableInCombat = false;
        public bool usableInOverworld=false;

        public Item(string name,int value,string description,ItemEffect effect,bool usableInCombat,bool usableInOverworld) 
        {
            this.name = name;
            this.value = value;
            this.description = description;
            this.effect = effect;
            this.usableInCombat = usableInCombat;
            this.usableInOverworld = usableInOverworld;
            
        }
        public string UseItem(Hero hero)
        {
            switch (effect) 
            {
                case ItemEffect.Heal:
                    if(hero.currenthp == hero.hp || !hero.isAlive())
                    {
                        return "";
                    }
                    return hero.Heal(value);
                case ItemEffect.Revive:
                    if(hero.isAlive())
                    {
                        return "";
                    }
                    return hero.Revive(value);
                default:
                    return hero.Heal(value);
            }
        }

        public void showItem()
        {
            Console.Write(name + " " + " : " + description);
        }
    }
}
