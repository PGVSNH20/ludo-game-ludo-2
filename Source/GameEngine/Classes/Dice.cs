using System;

namespace GameEngine
{
    public class Dice
    {
        public int Roll()
        {
            Random rnd = new Random();
            return rnd.Next(1, 6);
        }
    }
}
