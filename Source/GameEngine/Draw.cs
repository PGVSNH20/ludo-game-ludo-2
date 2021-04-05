using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        }

        //TODO: Where and how should input be handled? Should we have an input manager-class/thingy or something that calls Draw?
        //TODO: Bigger draws might need own class, mainly the game's draw.
        public static void Update(Scene scene)
        {
            Console.Clear();
            switch (scene)
            {
                case Scene.MainMenu:
                    DrawLogo();
                    DrawMainMenu();
                    break;
                
                case Scene.OptionsMenu:
                    //Display options
                    break;

                case Scene.Game:
                    DrawBoard();
                    //DrawScoreBoard();
                    //DrawGameStatus();
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

        private static void DrawMainMenu()
        {
            Console.WriteLine("Hello, this is main menu.");
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
                
                Console.ForegroundColor = drawableGameBoard[i].color;
                Console.Write(drawableGameBoard[i].character);
                
                Console.ForegroundColor = previousColor;
            }
        }

        #endregion
    }
}
