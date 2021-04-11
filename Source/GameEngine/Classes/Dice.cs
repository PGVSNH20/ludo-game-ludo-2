using System;

namespace GameEngine
{
    public class Dice
    {
        //Fusktärning
        //int[] fixedNumbers = new int[] { 1, 6};
        public int Roll()
        {
            Random rnd = new Random();
            return rnd.Next(1, 7);
            //return rnd.Next(0, 1);
           
            //return fixedNumbers[rnd.Next(0, 1)];
        }
    }
}
