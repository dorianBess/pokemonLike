using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    internal class Entity
    {
        public string name = "Default";
        public int level = 1;
        public int hp = 1;
        public int currenthp = 1;
        public int def = 1;
        public int att = 1;
        public int vitesse = 1;
        public int pm = 1;
        public int maxPm = 1;
        public int precision = 100;


        public int[] magies = new int[3];

        public GameType type = GameType.fire;

        public Entity() { }

        public void setType(GameType type)
        {
            this.type = type;
        }

        public void addMagie(int indexMagie)
        {
            for (int i = 0; i < magies.Length; i++)
            {
                if (magies[i] == 0)
                {
                    magies[i] = indexMagie;
                    break;
                }
            }
        }

        public string takeDamageFromAttack(Entity entite, int value,bool isCrit = false)
        {
            int damageTaken = (entite.level * 2 / 5 * value / 25 / (isCrit ? def : 1) + 2) * 2;
            currenthp = currenthp - damageTaken;
            if (currenthp < 0) currenthp = 0;
            return entite.name + " a infligez " + damageTaken + " a " + name;
        }

        public string takeDamageFromMagie(Entity entite, int indexMagie,bool isCrit = false)
        {
            Magie mag = InfoManager.Instance.magieDex[indexMagie];
            int damageTaken = (int)((entite.level * 2 / 5 * mag.valeur * 5 / 25 / (isCrit ? def : 1) + 2) * getMultAttack(mag.type));
            currenthp = currenthp - damageTaken;
            if (currenthp < 0) currenthp = 0;
            return entite.name + " a infligez " + damageTaken + " a " + name;
        }

        public int calculateDamageFromAttack(Entity entite, int value)
        {
            int damageTaken = (entite.level * 2 / 5 * value / 25 / def + 2) * 2;
            return damageTaken;
        }

        public int calculateDamageFromMagie(Entity entite, int indexMagie)
        {
            Magie mag = InfoManager.Instance.magieDex[indexMagie];
            int damageTaken = (int)((level * 2 / 5 * mag.valeur * 5 / 25 / entite.def + 2) * getMultAttack(mag.type,entite.type));
            return damageTaken;
        }

        private float getMultAttack(GameType attType)
        {
            if (attType == GameType.fire && type == GameType.water
                || attType == GameType.water && type == GameType.grass
                || attType == GameType.grass && type == GameType.fire)
            {
                return 0.5f;
            }

            if (attType == GameType.fire && type == GameType.grass
               || attType == GameType.water && type == GameType.fire
               || attType == GameType.grass && type == GameType.water)
            {
                return 2f;
            }

            return 1f;
        }
        private float getMultAttack(GameType attType,GameType defType)
        {
            if (attType == GameType.fire && defType == GameType.water
                || attType == GameType.water && defType == GameType.grass
                || attType == GameType.grass && defType == GameType.fire)
            {
                return 0.5f;
            }

            if (attType == GameType.fire && defType == GameType.grass
               || attType == GameType.water && defType == GameType.fire
               || attType == GameType.grass && defType == GameType.water)
            {
                return 2f;
            }

            return 1f;
        }


        public bool isAlive()
        {
            return currenthp > 0;
        }

        public void showLifeBar()
        {
            Console.Write("[");
            int nbBarre = 100 * currenthp / hp / 10;
            if (currenthp > 0 && nbBarre == 0) nbBarre++;
            for (int i = 0; i < nbBarre; i++) Console.Write("|");
            for (int i = 0; i < 10 - nbBarre; i++) Console.Write(" ");
            Console.Write("]");
        }

        public void setInfo()
        {
            EntityStatInfo infos = InfoManager.Instance.entityStats[name];

            setType(infos.typeBase);

            hp = (int)(infos.hpBase * (level / 100f) + level + 10);
            currenthp = hp;
            def = (int)(infos.defBase * (level / 100f) + 5);
            att = (int)(infos.attBase * (level / 100f) + 5);
            pm = (int)(infos.pmBase * (level / 100f) + 5);
            maxPm = pm;
            precision = infos.precisionBase;
            vitesse = (int)(infos.vitesseBase * (level / 100f) + 5);

            magies = new int[3];
            foreach (MagieInfo info in infos.possibleMagie)
            {
                if (info.levelNeeded <= level)
                {
                    addMagie(info.magieIndex);
                }
            }
        }

        public void recupPm()
        {
            pm += maxPm / 4;
            if(pm > maxPm) 
            {
                pm = maxPm;
            }
        }

        public virtual void setLevel(int level)
        {
            this.level = level;
            setInfo();
        }

        public string attack(Entity cible, bool forTest = false)
        {
            if (forTest)
            {
                return cible.takeDamageFromAttack(this,att);
            }
            Random r = new Random();
            if(r.Next(0,101) < precision)
            {
                if(r.Next(0,11) == 1) 
                {
                    return cible.takeDamageFromAttack(this, att,true);
                }
                return cible.takeDamageFromAttack(this, att);
            }
            return "Attaque manque";
        }

        public string attackMagie(Entity cible,int indexMagie,bool forTest = false) 
        {
            if(forTest) 
            {
                return cible.takeDamageFromMagie(this, indexMagie);
            }
            Random r = new Random();
            if (r.Next(0, 101) < ((precision + InfoManager.Instance.magieDex[indexMagie].precision) / 2))
            {
                pm -= InfoManager.Instance.magieDex[indexMagie].cost;
                if (r.Next(0, 11) == 1)
                {
                    return cible.takeDamageFromMagie(this, indexMagie,true);
                }
                return cible.takeDamageFromMagie(this, indexMagie);
            }
            return name + " a rate son attaque";
        }
    }
}
