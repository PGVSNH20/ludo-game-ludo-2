using System;
using System.Threading;
using GameEngine;

namespace Ludo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //char hello = '@';
            //Console.ForegroundColor = hello == '@' ? ConsoleColor.Cyan : ConsoleColor.White;
            //Console.Write(hello);

            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.Write('@');

            //Console.ForegroundColor
            //    = ConsoleColor.Blue;
            Rules rules = new Rules();
            Game game = new Game(rules);
            
        }
    }
}
