using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Objects
{
    public class DrawableChar
    {
        public char Character { get; set; }
        public ConsoleColor Color { get; set; }

        public DrawableChar(char character, ConsoleColor color)
        {
            Character = character;
            Color = color;
        }
    }
}
