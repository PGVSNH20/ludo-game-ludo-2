using System;
using System.Threading;
using GameEngine;
using GameEngine.Classes;

namespace Ludo
{
    class Program
    {
        static void Main(string[] args)
        {
            Draw.Update(Draw.Scene.MainMenu);

            Rules rules = new Rules();
            Game game = new Game(rules);

            //Draw.Update(Draw.Scene.Game);
        }
    }
}
