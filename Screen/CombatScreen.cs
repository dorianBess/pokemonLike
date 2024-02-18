using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    internal class CombatScreen : Screen
    {
        CombatTile currentTile;

        bool whoGoFirstDetermined;
        bool playersGoFirst;
        bool currentMonsterDead = false;
        bool monsterPlay = false;
        bool playerPlay = false;
        bool needToSwitch = false;
        string resultAction;
        string resultMonsterAction;

        SelectedAction selectedAction;

        public CombatScreen(Map map, Player player,GamePhase previousGamePhase) : base(map, player, previousGamePhase)
        {
            currentTile = (CombatTile)map.GetTile(player.Position);
            currentTile.startCombat(player);
        }
        public override GamePhase Start()
        {
            Console.Clear();
            currentTile.showDetailledTile(player.team[1]);
            whoGoFirstDetermined = true;
            playersGoFirst = currentTile.currentMonster.vitesse > player.team[1].vitesse ? false : true;
            active = true;
            cursorIndex = 1;
            StartLoop();
            return gamePhaseToReturn;
        }

        public override void StartLoop()
        {
            while (active)
            {
                if (!whoGoFirstDetermined)
                {
                    whoGoFirstDetermined = true;
                    playersGoFirst = currentTile.currentMonster.vitesse > player.team[1].vitesse ? false : true;
                    playerPlay = false;
                    monsterPlay = false;
                    player.team[1].recupPm();
                    currentTile.currentMonster.recupPm();
                }

                if (!player.team[1].isAlive() && !needToSwitch)
                {
                    if (!player.team[1].isAlive() && !player.team[2].isAlive() && !player.team[3].isAlive()) 
                    {
                        Console.Clear() ;
                        Console.WriteLine("GAME OVER");
                        Environment.Exit(0);
                    }
                    needToSwitch = true;
                    selectedAction = SelectedAction.Switch;
                }
                else if (playersGoFirst && !playerPlay || !playerPlay && monsterPlay)
                {
                    
                    Console.Clear();
                    currentTile.showDetailledTile(player.team[1]);
                    Console.WriteLine();
                    if (selectedAction == SelectedAction.None)
                    {
                        choiceNumber = 4;
                        showAction();
                    }
                    else if (selectedAction == SelectedAction.Magie)
                    {
                        showMagie();
                    }
                    else if (selectedAction == SelectedAction.Objet)
                    {
                        ObjectScreen objectScreen = new ObjectScreen(map, player, GamePhase.combat);
                        objectScreen.Start();
                        if(objectScreen.itemResult != "Cet objet n'a aucune utilite sur ce hero" 
                            && objectScreen.itemResult != "")
                        {
                            Console.Clear();
                            currentTile.showDetailledTile(player.team[1]);
                            Console.WriteLine("\n" + objectScreen.itemResult);
                            selectedAction = SelectedAction.ValidateResult;
                        }
                        else
                        {
                            selectedAction = SelectedAction.None;
                        }
                        
                    }
                    else if (selectedAction == SelectedAction.Switch)
                    {
                        showSwitch();
                    }
                    else if (selectedAction == SelectedAction.ValidateResult)
                    {
                        Console.WriteLine(resultAction);
                    }
                    CheckCursorInput();
                }

                if (!currentTile.currentMonster.isAlive() && !currentMonsterDead)
                {
                    
                    currentMonsterDead = true;
                    resultAction = player.team[1].name + " a vaincu " + currentTile.currentMonster.name + "\n";
                    resultAction += player.addGold(currentTile.currentMonster.goldToGive) + "\n";
                    foreach (Hero hero in player.team.Values)
                    {
                        if (hero.isAlive())
                            resultAction += hero.addExperience(currentTile.currentMonster.expToGive);
                    }
                    Console.Clear();
                    currentTile.showDetailledTile(player.team[1]);
                    Console.WriteLine();
                    Console.WriteLine(resultAction);
                    selectedAction = SelectedAction.EndBattle;
                    player.team[1].recupPm();
                    currentTile.alreadyFight = true;
                    CheckCursorInput();
                }
                else if (!currentMonsterDead && (playersGoFirst && playerPlay || !playersGoFirst && !monsterPlay))
                {
                    resultMonsterAction = currentTile.currentMonster.selectAttack(player.team[1]);
                    selectedAction = SelectedAction.ValidateResult;

                    Console.Clear();
                    currentTile.showDetailledTile(player.team[1]);
                    Console.WriteLine();
                    if (selectedAction == SelectedAction.ValidateResult)
                    {
                        Console.WriteLine(resultMonsterAction);
                    }
                    CheckCursorInput();
                }

                if (playerPlay && monsterPlay)
                {
                    whoGoFirstDetermined = false;
                    selectedAction = SelectedAction.None;
                }
            }
        }

        public override void CheckCursorInput()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key != ConsoleKey.Enter && selectedAction == SelectedAction.ValidateResult)
                {

                }
                else
                {
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
                        case ConsoleKey.E:
                            if (!needToSwitch && selectedAction != SelectedAction.EndBattle)
                            {
                                selectedAction = SelectedAction.None;
                                Console.Clear();
                                return;
                            }
                            break;
                        case ConsoleKey.Enter:
                            if (selectedAction != SelectedAction.Switch || player.team[cursorIndex].isAlive())
                            {
                                Validate();
                                needToSwitch = false;
                                return;
                            }
                            break;
                    }
                }
            }
        }

        void Validate()
        {
            CombatTile currentTile = (CombatTile)map.GetTile(player.Position);
            if (selectedAction == SelectedAction.None)
            {
                if (cursorIndex == 1)
                {
                    resultAction = player.team[1].attack(currentTile.currentMonster);
                    selectedAction = SelectedAction.ValidateResult;
                }
                else if (cursorIndex == 2)
                {
                    selectedAction = SelectedAction.Magie;
                    cursorIndex = 1;
                }
                else if (cursorIndex == 3)
                {
                    selectedAction = SelectedAction.Objet;
                }
                else if (cursorIndex == 4)
                {
                    selectedAction = SelectedAction.Switch;
                    cursorIndex = 1;
                }
            }
            else if (selectedAction == SelectedAction.Magie)
            {
                if (cursorIndex == 0 || player.team[1].magies[cursorIndex - 1] == 0)
                {
                    cursorIndex = 1;
                }
                else
                {
                    if (player.team[1].pm >= InfoManager.Instance.magieDex[player.team[1].magies[cursorIndex -1 ]].cost)
                    {
                        resultAction = player.team[1].attackMagie(currentTile.currentMonster, player.team[1].magies[cursorIndex - 1]);
                        selectedAction = SelectedAction.ValidateResult;
                    }                   
                }               
            }
            else if (selectedAction == SelectedAction.Objet)
            {
            }
            else if (selectedAction == SelectedAction.Switch)
            {
                player.switchCurrentHero(cursorIndex);
                playerPlay = true;
                
            }
            else if (selectedAction == SelectedAction.EndBattle)
            {
                Console.Clear();
                currentMonsterDead = false;
                resultAction = "";
                selectedAction = SelectedAction.None;
                gamePhaseToReturn = GamePhase.movement;
                active = false;
            }
            else if (selectedAction == SelectedAction.ValidateResult)
            {
                if (playersGoFirst && !playerPlay || monsterPlay)
                {
                    playerPlay = true;
                }
                else
                {
                    monsterPlay = true;
                }
                selectedAction = SelectedAction.None;
            }
        }
    }
}
