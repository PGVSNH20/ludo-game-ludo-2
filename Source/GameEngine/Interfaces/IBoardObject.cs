using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Interfaces
{
    public interface IBoardObject
    {
        public char CharToDraw { get;}
        //IBoardObject PopulateWithNests(IBoardObject[,] gameBoard, List<Player> players);
    }
}
