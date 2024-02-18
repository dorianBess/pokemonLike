using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    struct MagieInfo
    {
        public int levelNeeded;
        public int magieIndex;

        public MagieInfo(int level, int index)
        {
            levelNeeded = level;
            magieIndex = index;
        }
    }
    struct EntityStatInfo
    {
        public int hpBase;
        public int defBase;
        public int attBase;
        public int pmBase;
        public int precisionBase;
        public int vitesseBase;

        public GameType typeBase;

        public List<MagieInfo> possibleMagie;

        public void init(int hp, int def, int att, int pm, int precision, int vitesse, GameType type)
        {
            hpBase = hp;
            defBase = def;
            attBase = att;
            pmBase = pm;
            precisionBase = precision;
            vitesseBase = vitesse;

            typeBase = type;
        }

        public void addPossibleMagie(MagieInfo magieInfo)
        {
            if (possibleMagie == null)
            {
                possibleMagie = new List<MagieInfo>();
            }
            possibleMagie.Add(magieInfo);
        }
    }

    internal class InfoManager
    {
        public Dictionary<string, EntityStatInfo> entityStats;
        public Dictionary<int, Magie> magieDex;
        public Dictionary <int, Item> itemDex;

        private static InfoManager instance;

        public static InfoManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InfoManager();
                }
                return instance;
            }
        }
        public InfoManager()
        {
            entityStats = new Dictionary<string, EntityStatInfo>();

            EntityStatInfo statMage = new EntityStatInfo();
            statMage.init(138, 70, 110, 80, 90, 50, GameType.water);
            statMage.addPossibleMagie(new MagieInfo(3, 7));
            statMage.addPossibleMagie(new MagieInfo(8, 6));
            statMage.addPossibleMagie(new MagieInfo(12, 8));
            entityStats.Add("Mage", statMage);

            EntityStatInfo statArcher = new EntityStatInfo();
            statArcher.init(140, 70, 100, 40, 100, 80, GameType.grass);
            statArcher.addPossibleMagie(new MagieInfo(3, 1));
            statArcher.addPossibleMagie(new MagieInfo(8, 2));
            statArcher.addPossibleMagie(new MagieInfo(12, 3));
            entityStats.Add("Archer", statArcher);

            EntityStatInfo statWarrior = new EntityStatInfo();
            statWarrior.init(200, 90, 70, 30, 95, 60, GameType.fire);
            statWarrior.addPossibleMagie(new MagieInfo(3, 9));
            statWarrior.addPossibleMagie(new MagieInfo(8, 4));
            statWarrior.addPossibleMagie(new MagieInfo(12, 5));
            entityStats.Add("Warrior", statWarrior);

            EntityStatInfo statTreant = new EntityStatInfo();
            statTreant.init(50, 40, 40, 30, 85, 30, GameType.grass);
            statTreant.addPossibleMagie(new MagieInfo(1, 2));
            statTreant.addPossibleMagie(new MagieInfo(8, 3));
            entityStats.Add("Treant", statTreant);

            EntityStatInfo statOgre = new EntityStatInfo();
            statOgre.init(120, 60, 20, 10, 100, 50, GameType.fire);
            statOgre.addPossibleMagie(new MagieInfo(10, 9));
            entityStats.Add("Ogre", statOgre);

            EntityStatInfo statNaga = new EntityStatInfo();
            statNaga.init(80, 30, 45, 20, 95, 60, GameType.water);
            statNaga.addPossibleMagie(new MagieInfo(3, 7));
            statNaga.addPossibleMagie(new MagieInfo(12, 5));
            statNaga.addPossibleMagie(new MagieInfo(20, 8));
            entityStats.Add("Naga", statNaga);

            EntityStatInfo statPantinFeu = new EntityStatInfo();
            statPantinFeu.init(100, 50, 50, 50, 100, 50, GameType.fire);
            statPantinFeu.addPossibleMagie(new MagieInfo(1, 4));
            statPantinFeu.addPossibleMagie(new MagieInfo(2, 5));
            statPantinFeu.addPossibleMagie(new MagieInfo(3, 9));
            entityStats.Add("Pfeu", statPantinFeu);

            EntityStatInfo statPantinEau = new EntityStatInfo();
            statPantinEau.init(100, 50, 50, 50, 100, 50, GameType.water);
            statPantinEau.addPossibleMagie(new MagieInfo(1, 6));
            statPantinEau.addPossibleMagie(new MagieInfo(2, 7));
            statPantinEau.addPossibleMagie(new MagieInfo(3, 8));
            entityStats.Add("Peau", statPantinEau);

            EntityStatInfo statPantinPlante = new EntityStatInfo();
            statPantinPlante.init(100, 50, 50, 50, 100, 50, GameType.grass);
            statPantinPlante.addPossibleMagie(new MagieInfo(1, 1));
            statPantinPlante.addPossibleMagie(new MagieInfo(2, 2));
            statPantinPlante.addPossibleMagie(new MagieInfo(3, 3));
            entityStats.Add("Pplante", statPantinPlante);

            magieDex = new Dictionary<int, Magie>();

            magieDex.Add(1, new Magie("Lame vegetale", 10, GameType.grass,5,100));
            magieDex.Add(2, new Magie("Racines Enchevetrees", 15, GameType.grass, 10,100));
            magieDex.Add(3, new Magie("Tornade de Feuilles", 25, GameType.grass, 12,90));
            magieDex.Add(4, new Magie("Tempete de feu", 15, GameType.fire, 6,100));
            magieDex.Add(5, new Magie("Combustion", 25, GameType.fire, 7,80));
            magieDex.Add(6, new Magie("Tsunami", 15, GameType.water, 8,100));
            magieDex.Add(7, new Magie("Lame oceanique", 10, GameType.water, 5,100));
            magieDex.Add(8, new Magie("Blizzard", 25, GameType.water, 15,90));
            magieDex.Add(9, new Magie("Boule de feu", 12, GameType.fire, 5,100));

            itemDex = new Dictionary<int, Item>();

            itemDex.Add(1, new Item("Petite potion",10,"Soigne de 10hp",ItemEffect.Heal,true,true));
            itemDex.Add(2, new Item("Potion",30,"Soigne de 30hp",ItemEffect.Heal,true,true));
            itemDex.Add(3, new Item("Grosse potion",50,"Soigne de 50hp",ItemEffect.Heal,true,true));
            itemDex.Add(4, new Item("Petite herbevie",1,"Reanime la cible avec 1hp",ItemEffect.Revive,true,true));
            itemDex.Add(5, new Item("Herbevie",5000, "Reanime la cible avec tous ces hp", ItemEffect.Revive,true,true));
        }
    }
}
