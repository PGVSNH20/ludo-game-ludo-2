using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.Classes
{
    public class MainMenu
    {

        // Start new game, load game, exit
        // 1. Start new game | 2. Load game | 3. High scores | 4. Exit game

        // 1. -> Players (2-4): INPUT

        // 2. -> Select a game to load: INPUT
        // ^(list of savefiles from db)

        // 3. -> (list of highscores from db)

        // 4. -> Exit game? (y/n)

        private Draw.Scene current = Draw.Scene.MainMenu;
        private int selectedOption = 1;


        public MainMenu()
        {
            //Draw.Update(current, null, 1);
            Update();
            while (true)
            {
                var input = Console.ReadKey();
                if(input.Key == ConsoleKey.UpArrow)
                {
                    selectedOption--;
                    Update();
                }
                if (input.Key == ConsoleKey.DownArrow)
                {
                    selectedOption++;
                    Update();
                }
                else if (input.Key == ConsoleKey.Enter && selectedOption == 2)
                {
                    current = Draw.Scene.OptionsMenu;
                    Update();
                }
            }



        }

        

        private void Update() => Draw.Update(current, null, selectedOption);
    





}
}
