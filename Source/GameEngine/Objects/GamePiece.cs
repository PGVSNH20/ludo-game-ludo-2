using GameEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class GamePiece : IBoardObject
    {
        public char CharToDraw { get; }
        public int PlayerId { get; set; }


        public GamePiece(char charToDraw, int playerID)
        {
            CharToDraw = charToDraw;
            PlayerId = playerID;
        }

    }

    


}
