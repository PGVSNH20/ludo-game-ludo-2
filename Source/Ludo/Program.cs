using System;
using System.Threading;
using GameEngine;

namespace Ludo
{
    class Program
    {
        static void Main(string[] args)
        {
            Draw.Update(Draw.Scene.MainMenu);

            Rules rules = new Rules();
            Game game = new Game(rules);
            
        }
    }
}
