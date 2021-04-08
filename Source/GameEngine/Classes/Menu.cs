using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameEngine.Classes
{
    public class Menu
    {
        // Start new game, load game, exit
        // 1. Start new game | 2. Load game | 3. High scores | 4. Exit game

        // 1. -> Players (2-4): INPUT

        // 2. -> Select a game to load: INPUT
        // ^(list of savefiles from db)

        // 3. -> (list of highscores from db)

        // 4. -> Exit game? (y/n)



        private Draw.Scene current = Draw.Scene.MainMenu;
        private int selectedOption = 0;
        //TODO: Load rules from DB?
        private Rules rules = new Rules();

        public Menu()
        {

            Update();
            while (true)
            {

                var input = Console.ReadKey();

                if (input.Key == ConsoleKey.UpArrow)
                {
                    selectedOption--;
                    if(selectedOption == -1)
                    {
                        selectedOption++;
                    }
                    Update();
                    
                }
                if (input.Key == ConsoleKey.DownArrow)
                {
                    selectedOption++;
                    if (selectedOption == 4)
                    {
                        selectedOption--;
                    }
                    Update();
                    
                }

                else if (input.Key == ConsoleKey.Enter && current == Draw.Scene.OptionsMenu)
                {

                    current = Draw.Scene.Game;
                    rules.NumberOfPlayers = selectedOption + 2;
                    Console.WriteLine("Starting game...");
                    Thread.Sleep(2000);
                    Game game = new Game(rules);

                    Update();

                }
                else if (input.Key == ConsoleKey.Enter && selectedOption == 0 && current == Draw.Scene.MainMenu)
                {
                    
                    current = Draw.Scene.OptionsMenu;
                    
                    Update();

                }

                else if (input.Key == ConsoleKey.Enter && selectedOption == 2 && current == Draw.Scene.MainMenu)
                {
                    current = Draw.Scene.HighScore;
                    Update();
                }

                else if (input.Key == ConsoleKey.Enter && selectedOption == 1 && current == Draw.Scene.MainMenu)
                {
                    //current = Draw.Scene.LoadGame;
                    Update();
                }

                else if (input.Key == ConsoleKey.Enter && selectedOption == 3 && current == Draw.Scene.MainMenu)
                {
                    Console.WriteLine("Ending game....");
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                    
                }

                else if(input.Key == ConsoleKey.Escape)
                {
                    current = Draw.Scene.MainMenu;
                    Update();
                }


            }

        }

        public static string[] StartMenu()
        {
            
            string[] startMenu = { "Start new Game", "Load Game", "High Score", "Exit game" };

            return startMenu;
        }

        public static string[] OptionsStartNewGame()
        {
            string[] optionMenu = { "2", "3", "4" };

            return optionMenu;
        }

        public static string[] HighScore()
        {
            string[] highScore = { "Något från databasen"};

            return highScore;
        }


        public static string[] LoadGame()
        {
            string[] loadGame = { "Något sparat spel från databasen" };

            return loadGame;
        }


        private void Update() => Draw.Update(current, null, selectedOption);


    }
}
