﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Classes;
using GameEngine.Objects;



namespace GameEngine
{
    public class Draw
    {

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

        //TODO: Where and how should input be handled? Should we have an input manager-class/thingy or something that calls Draw?
        //TODO: Bigger draws might need own class, mainly the game's draw.
        public static void Update(Scene scene, Game game = null, int selected = 0)
        {

            var startMenu = Menu.StartMenu();
            var optionsStartNewGame = Menu.OptionsStartNewGame();
            var loadGame = Menu.LoadGame();
            var highScore = Menu.HighScore();

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
                    break;

                case Scene.LoadGame:
                    DrawLogo();
                    DrawMenu(loadGame, selected);
                    break;

                case Scene.HighScore:
                    DrawMenu(highScore);
                    break;

                case Scene.Game:
                    DrawBoard();
                    //DrawScoreBoard();
                    DrawGameStatus(game.StatusMessage);
                    DrawGameActionStatus(game.ActionMessage);
                    
                    /*
                    INSERT BEAUTIFUL BOARD THAT JENS APPROVED THE COLORS OF

                    Player Red knocked out Player Blue's piece.
                    Player Green's turn. Roll the dice!
                    
                    ...
                    */
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
        }

        private static void DrawGameActionStatus(string message)
        {
            Console.WriteLine(message);
        }

        private static void DrawGameStatus(string message)
        {
            Console.WriteLine(message);
        }


        public static void DrawMenu(string[] menu, int selected = 0)
        {
            string currentOption;

            for (int i = 0; i < menu.Length; i++)
            {
                currentOption = menu[i];
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

           
            
        }

        #endregion
    }
}
