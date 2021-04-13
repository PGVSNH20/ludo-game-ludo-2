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
      
        private Draw.Scene current = Draw.Scene.MainMenu;
        private int selectedOption = 0;
        private Rules rules = new Rules();

        /// <summary>
        /// Logic to step in the menu, start local game and start saved game from database.
        /// </summary>

        public Menu()
        {

            var mainMenu = StartMenu();
            var optionsStartNewGame = OptionsStartNewGame();

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
                    if(selectedOption == optionsStartNewGame.Length && current == Draw.Scene.OptionsMenu)
                    {
                        selectedOption = 0;
                    }
                    if (selectedOption == mainMenu.Length)
                    {  
                        selectedOption = 0;
                    }
                    Update();
                    
                }

                else if (input.Key == ConsoleKey.Enter && current == Draw.Scene.OptionsMenu)
                {
                    //TODO: Fix playing with less than 4 players
                    if (selectedOption < 2)
                    {
                        selectedOption = 2;
                    }

                    current = Draw.Scene.Game;
                    rules.NumberOfPlayers = selectedOption + 2;
                    Console.WriteLine("Starting game...");
                    Thread.Sleep(2000);
                    Game game = new Game(rules);
                    current = Draw.Scene.MainMenu;
                    selectedOption = 0;
                    Update();
                }

                else if (input.Key == ConsoleKey.Enter && current == Draw.Scene.LoadGame)
                {
                    current = Draw.Scene.Game;
                    Console.WriteLine("Resuming game...");
                    Thread.Sleep(2000);

                    var context = new DbModel();
                    Game game = new Game(context.Rules.Find(selectedOption + 1), context.SaveGames.Find(selectedOption + 1));

                    current = Draw.Scene.MainMenu;
                    selectedOption = 0;
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
                    current = Draw.Scene.LoadGame;
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

        /// <summary>
        /// Returns a string array with Startmenu choises.
        /// </summary>
        /// <returns></returns>
        public static string[] StartMenu()
        {
            
            string[] startMenu = { "Start new Game", "Load Game", "High Score", "Exit game" };

            return startMenu;
        }

        /// <summary>
        /// Returns a string array with options to startmenu.
        /// </summary>
        /// <returns></returns>
        public static string[] OptionsStartNewGame()
        {
            string[] optionMenu = { "2", "3", "4" };

            return optionMenu;
        }

        /// <summary>
        /// Returns a string array with saved highscores from database.
        /// </summary>
        /// <returns></returns>
        public static string[] HighScore()
        {
            string[] highScore = { "Något från databasen"};

            return highScore;
        }

        /// <summary>
        /// Returns a string array with loaded games from method FromDB()
        /// </summary>
        /// <returns></returns>
        public static string[] LoadGame()
        {
            var listFromDB = FromDB();

            return new string[0];
        }

        
        /// <summary>
        /// Method that saves saved games from database into a list and returns the list
        /// </summary>
        /// <returns></returns>
        private static List<SaveGame> FromDB()
        {
            var context = new DbModel();
            List<SaveGame> data = context.SaveGames.ToList();
            return data;
        }
        
        private void Update() => Draw.Update(current, null, selectedOption);
    }
}
