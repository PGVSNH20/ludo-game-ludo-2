using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Classes;
using GameEngine.Objects;



namespace GameEngine
{
    public class Draw
    {

        static ConsoleColor standard = ConsoleColor.Black;

        //What menu should be displayed
        //private static Scene CurrentScene { get; set; }

        public enum Scene
        {
            MainMenu,
            NewGameMenu,
            OptionsMenu,
            ExitMenu,
            Game,
            ResultScreen,
            HighScore,
            LoadGame,

        }

        /// <summary>
        /// Method with a switch case that updates and changes to which part of the program to be displayed
        /// </summary>
        
        //TODO: Where and how should input be handled? Should we have an input manager-class/thingy or something that calls Draw?
        //TODO: Bigger draws might need own class, mainly the game's draw.
        public static void Update(Scene scene, Game game = null, int selected = 0)
        {

            var startMenu = Menu.StartMenu();
            var optionsStartNewGame = Menu.OptionsStartNewGame();
            var loadGame = Menu.LoadGame();
            var highScore = Menu.HighScore();
            var listFromDB = Menu.LoadGame();

            Console.Clear();
            switch (scene)
            {
                case Scene.MainMenu:
                    DrawLogo();
                    DrawMenu(startMenu, selected);
                    break;

                case Scene.OptionsMenu:
                    DrawLogo();
                    DrawMenu(optionsStartNewGame, selected);
                    Console.BackgroundColor = standard;
                    break;

                case Scene.LoadGame:
                    DrawLogo();
                    DrawSavedGames(selected);
                    break;

                case Scene.HighScore:
                    DrawMenu(highScore);
                    break;

                case Scene.Game:
                    DrawBoard();
                    //TODO: Draw results
                    DrawGameStatus(Game.StatusMessage);
                    DrawGameActionStatus(Game.ActionMessage);
                    DrawButtonInfo();

                    
                    break;

            }
        }

        #region DrawComponents

        private static void DrawLogo()
        {
            Console.WriteLine("   __           _");
            Console.WriteLine("  / / _   _  __| | ___");
            Console.WriteLine(" / / | | | |/ _` |/ _ \\ ");
            Console.WriteLine("/ /__| |_| | (_| | (_) |");
            Console.WriteLine("\\____/\\__,_|\\__,_|\\___/");
            Console.WriteLine();
        }
        /// <summary>
        /// Different methods that we use to draw all the events in the game.
        /// </summary>
        private static void DrawBoard()
        {

            var drawableGameBoard = Game.GenerateDrawable();

            for (int i = 0; i < drawableGameBoard.Count; i++)
            {
                var previousColor = Console.ForegroundColor;

                //TODO: Better way of getting width
                if (i % 11 == 0)
                {
                    Console.WriteLine();
                }

                Console.ForegroundColor = drawableGameBoard[i].Color;
                Console.Write(drawableGameBoard[i].Character);

                Console.ForegroundColor = previousColor;
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void DrawGameActionStatus(string message)
        {
            ColorFormattedWriteLine(message);
        }

        private static void DrawGameStatus(string message)
        {
            ColorFormattedWriteLine(message);
        }

        private static void DrawButtonInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Roll dice: Spacebar | Select piece: 1-4 | Save game: 'S'");
        }

        public static void DrawSavedGames(int selected)
        {
            var result = new List<string>();
            var context = new DbModel();
            foreach (var saveGame in context.SaveGames)
            {
                result.Add($"{saveGame.SaveGameId}. {saveGame.Date}");
            }

            DrawMenu(result.ToArray(), selected);
        }

        public static void DrawMenu(string[] menu, int selected = 0)
        {
            for (int i = 0; i < menu.Length; i++)
            {
                var currentOption = menu[i];
                string prefix;

                if (i == selected)
                {
                    prefix = "*";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Green;

                }

                else
                {
                    prefix = "";
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.BackgroundColor = ConsoleColor.Black;
                }


                Console.WriteLine($"{prefix} << {currentOption} >>");

            }


            Console.BackgroundColor = standard;
            Console.ForegroundColor = ConsoleColor.White;

            selected = 0;
        }

        #endregion

        /// <summary>
        /// Adds color to words that match ConsoleColor and prints them.
        /// </summary>
        /// <param name="text">String to be colorized</param>
        private static void ColorFormattedWriteLine(string text)
        {
            var originalColor = Console.ForegroundColor;
            var colors = ConsoleColor.GetValues(typeof(ConsoleColor));

            var foundColor = false;
            foreach (ConsoleColor consoleColor in colors)
            {
                if (text.Contains(consoleColor.ToString()))
                {
                    var splitText = text.Split(new string[] { consoleColor.ToString() }, StringSplitOptions.None);
                    var splitList = new List<string>();
                    for (var i = 0; i < splitText.Length; i++)
                    {
                        var span = splitText[i];
                        splitList.Add(span);
                        if (i < splitText.Length - 1)
                        {
                            splitList.Add(consoleColor.ToString());
                        }
                    }

                    for (int i = 0; i < splitList.Count; i++)
                    {
                        if (i % 2 == 1)
                        {
                            Console.ForegroundColor = consoleColor;
                            Console.Write(splitList[i]);
                            Console.ForegroundColor = originalColor;
                        }
                        else
                        {
                            Console.Write(splitList[i]);
                        }
                    }
                    Console.WriteLine();
                    foundColor = true;
                }
            }

            if (!foundColor)
            {
                Console.WriteLine(text);
            }
        }



    }
}
