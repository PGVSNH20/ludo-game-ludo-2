using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Classes;

namespace GameEngine.Interfaces
{
    public interface ILudoBoard
    {
        static object[,] GameBoard { get; set; }

        static object[,] OriginalGameBoard { get; set; }

        static List<Player> Players { get; set; }
    }
}
