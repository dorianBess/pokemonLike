using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    enum GamePhase { combat, talk, movement, menuStat, menuHero,Object }
    enum Direction { left, right, top, down }
    enum SelectedAction { None, Attack, Magie, Objet, Switch, EndBattle, ValidateResult }
    class GameManager
    {
        public GamePhase phase;

        private static GameManager instance;
        public Map map { get; }
        public Player player { get; private set; }
        public GameManager()
        {
            phase = GamePhase.movement;
            map = new Map();
            map.load("pokemonLike.json");

            player = new Player();
            RunGameLoop();
            MenuScreen menu = new MenuScreen(map,player);
            menu.Start();
        }

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        public void RunGameLoop()
        {
            //while (true)
            //{
            //    if (phase == GamePhase.talk)
            //    {

            //    }
            //    else if (phase == GamePhase.movement)
            //    {
            //        MovementScreen moveScreen = new MovementScreen(map, player,GamePhase.movement);
            //        phase = moveScreen.Start();
            //    }
            //    else if (phase == GamePhase.combat)
            //    {
            //        CombatScreen combatScreen = new CombatScreen(map, player, GamePhase.movement);
            //        phase = combatScreen.Start();
            //        CombatTile currentTile = (CombatTile)map.GetTile(player.Position);
            //        currentTile.alreadyFight = false;
            //    }
            //    else if (phase == GamePhase.menuStat)
            //    {
            //        ScreenMenuStat screenMenuStat = new ScreenMenuStat(map, player, GamePhase.movement);
            //        phase = screenMenuStat.Start();
            //    }
            //}
        }
    }
}
