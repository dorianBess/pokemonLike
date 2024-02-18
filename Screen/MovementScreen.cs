using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    internal class MovementScreen : Screen
    {
        bool canStartFight = false;
        string resultAction = "";
        Vector2Int lastFightPos = new Vector2Int(0,0);
        public MovementScreen(Map map,Player player,GamePhase previousGamePhase) : base(map,player, previousGamePhase) { }
        public override GamePhase Start() 
        {
            Console.Clear();
            map.show(player.Position);
            active = true;
            StartLoop();
            return gamePhaseToReturn;
        }

        public override void StartLoop()
        {
            bool returnFromBattle = false;
            while (active)
            { 
                returnFromBattle = false;
                Console.Clear();
                map.show(player.Position);
                if (lastFightPos.x != player.Position.x && lastFightPos.y != player.Position.y && canStartFight && map.GetTypeTile(player.Position) == TypeTile.Combat)
                {
                    CombatTile currentTile = (CombatTile)map.GetTile(player.Position);
                    Random random = new Random();
                    if (currentTile.chanceEncounter > random.Next(101))
                    {
                        CombatScreen combatScreen = new CombatScreen(map,player,GamePhase.movement);
                        combatScreen.Start();
                        lastFightPos = player.Position;
                        returnFromBattle = true;
                        canStartFight = false;
                    }
                }
                Console.WriteLine();
                Console.WriteLine("S : Sauvegarder");
                Console.WriteLine("T : Equipe");
                Console.WriteLine("O : Objet");
                Console.WriteLine();
                Console.Write(resultAction);
                if(!returnFromBattle) 
                {
                    CheckInput();
                }
                
            }
        }

        public override void CheckInput()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (true)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (player.Position.y - 1 >= 0
                            && map.GetTypeTile(player.Position.x, player.Position.y - 1) != TypeTile.none)
                        {
                            Console.Clear();
                            player.move(Direction.top);
                            canStartFight = true;
                        }
                        else 
                        {
                            canStartFight = false;
                        }
                        resultAction = "";
                        return;
                    case ConsoleKey.DownArrow:
                        if (player.Position.y + 1 < Map.HEIGHT
                            && map.GetTypeTile(player.Position.x, player.Position.y + 1) != TypeTile.none)
                        {
                            Console.Clear();
                            player.move(Direction.down);
                            canStartFight = true;
                            return;
                        }
                        else
                        {
                            canStartFight = false;
                        }
                        resultAction = "";
                        return;
                    case ConsoleKey.LeftArrow:
                        if (player.Position.x - 1 >= 0
                            && map.GetTypeTile(player.Position.x - 1, player.Position.y) != TypeTile.none)
                        {
                            Console.Clear();
                            player.move(Direction.left);
                            canStartFight = true;
                            return;
                        }
                        else
                        {
                            canStartFight = false;
                        }
                        resultAction = "";
                        return;
                    case ConsoleKey.RightArrow:
                        if (player.Position.x + 1 < Map.HEIGHT
                            && map.GetTypeTile(player.Position.x + 1, player.Position.y) != TypeTile.none)
                        {
                            Console.Clear();
                            player.move(Direction.right);
                            canStartFight = true;
                            return;
                        }
                        else
                        {
                            canStartFight = false;
                        }
                        resultAction = "";
                        return;
                    case ConsoleKey.T:
                        ScreenMenuStat screenMenuStat = new ScreenMenuStat(map,player,GamePhase.movement);
                        screenMenuStat.Start();
                        canStartFight = false;
                        return;
                    case ConsoleKey.S:
                        Save();
                        resultAction = "Partie sauvegarder";
                        canStartFight = false;
                        return;
                    case ConsoleKey.O:
                        ObjectScreen screenObjet = new ObjectScreen(map, player, GamePhase.movement);
                        screenObjet.Start();
                        canStartFight = false;
                        return;
                    case ConsoleKey.Enter:
                        if(map.GetTypeTile(player.Position) == TypeTile.healCenter)
                        {
                            HealCenterTile healCenter = (HealCenterTile)map.GetTile(player.Position);
                            healCenter.healTeam(player);
                            resultAction = "Votre equipe est en pleine forme";
                        }
                        else if(map.GetTypeTile(player.Position) == TypeTile.marchand)
                        {
                            MarchandScreen marchandScreen = new MarchandScreen(map,player);
                            marchandScreen.Start();
                        }
                        canStartFight = false;
                        return;
                    default: return;
                }
            }
        }

        public void Save()
        {
            string jsonData = JsonConvert.SerializeObject(player, Formatting.Indented);
            File.WriteAllText("Save.json", jsonData);
        }
    }
}
