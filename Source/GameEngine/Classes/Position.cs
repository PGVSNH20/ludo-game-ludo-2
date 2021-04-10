using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Classes
{
    public struct Position
    {
        public byte Row { get; set; }
        public byte Col { get; set; }
        public Position(byte row, byte col)
        {
            this.Row = row;
            this.Col = col;
        }
        public Position(int row, int col)
        {
            this.Row = (byte)row;
            this.Col = (byte)col;
        }

        public override string ToString() => $"({Row},{Col})";
      
    }
}
