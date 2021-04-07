using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Classes
{
    public struct Position
    {
        public byte Col { get; set; }
        public byte Row { get; set; }
        public Position(byte col, byte row)
        {
            this.Col = col;
            this.Row = row;
        }
        public Position(int col, int row)
        {
            this.Col = (byte)col;
            this.Row = (byte)row;
        }
    }
}
