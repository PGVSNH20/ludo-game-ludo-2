using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            switch (scene)
            {
                case Scene.MainMenu:
                    DrawLogo();
                    DrawMainMenu();
                    break;
                
                case Scene.OptionsMenu:
                    //Display options
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

        #endregion
    }
}
