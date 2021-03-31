using System;

namespace GameEngine
{
    class Dice
    {
        public int Roll()
        {
            Random rnd = new Random();
            return rnd.Next(1, 6);
        }
    }
}
