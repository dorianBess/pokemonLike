using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    internal class MenuScreen : Screen
    {
        string[] option = new string[4];
        public MenuScreen(Map map, Player player) : base(map, player, GamePhase.movement)
        {
            option[0] = "Nouvelle partie";
            option[1] = "Continuer";
            option[2] = "Test unitaire";
            option[3] = "Quitter";
        }

        public override GamePhase Start()
        {
            Console.Clear();
            active = true;
            choiceNumber = 4;
            cursorIndex = 1;
            StartLoop();
            return gamePhaseToReturn;
        }

        public override void StartLoop()
        {
            while (active)
            {
                Console.Clear();
                for (int i = 1; i < choiceNumber + 1; i++)
                {
                    if (cursorIndex == i)
                    {
                        drawCursor();
                    }
                    Console.WriteLine(option[i - 1]);
                }
                CheckCursorInput();
            }
        }

        public override void CheckCursorInput()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (cursorIndex - 1 > 0)
                        {
                            cursorIndex--;
                        }
                        else
                        {
                            cursorIndex = choiceNumber;
                        }
                        return;
                    case ConsoleKey.RightArrow:
                        if (cursorIndex + 1 <= choiceNumber)
                        {
                            cursorIndex++;
                        }
                        else
                        {
                            cursorIndex = 1;
                        }
                        return;
                    case ConsoleKey.Enter:
                        switch (cursorIndex)
                        {
                            case 1:
                                Save();
                                MovementScreen movementScreen = new MovementScreen(map, player, GamePhase.movement);
                                movementScreen.Start();
                                return;
                            case 2:
                                LoadSave();
                                MovementScreen movementScreen2 = new MovementScreen(map, player, GamePhase.movement);
                                movementScreen2.Start();
                                return;
                            case 3:
                                UnitTestScreen unitTestScreen = new UnitTestScreen();
                                unitTestScreen.start();
                                return;
                            case 4:
                                Environment.Exit(0);
                                return;
                            default: 
                                return;

                        }
                }
            }
        }

        public void LoadSave()
        {
            if(!File.Exists("Save.json"))
            {
                Save();
                return;
            }
            string jsonContent = File.ReadAllText("Save.json");

            dynamic saveData = JsonConvert.DeserializeObject(jsonContent);

            player.Position = new Vector2Int((int)saveData.Position.x, (int)saveData.Position.y);
            player.gold = (int)saveData.gold;
            foreach( var item in saveData.items)
            {
                player.items.Add((int)item);
            }

            player.team.Clear();

            for(int i = 1; i < 4;i++)
            {
                player.team[i] = new Hero();
                player.team[i].name = saveData.team[i.ToString()].name;
                player.team[i].setLevel((int)saveData.team[i.ToString()].level);
                player.team[i].setInfo();
                player.team[i].currenthp = (int)saveData.team[i.ToString()].currenthp;
                player.team[i].pm = (int)saveData.team[i.ToString()].pm;
                player.team[i].experience = (int)saveData.team[i.ToString()].experience;
            }
            
        }

        public void Save()
        {
            string jsonData = JsonConvert.SerializeObject(player, Formatting.Indented);
            File.WriteAllText("Save.json", jsonData);
        }
    }
}
