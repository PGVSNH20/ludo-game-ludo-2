using GameEngine.Interfaces;
using GameEngine.Objects;
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

        
        public bool IsPlacedOnBoard { get; set; } = false;

        public Position Position { get; set; }


        public GamePiece(char charToDraw, int playerID)
        {
            CharToDraw = charToDraw;
            PlayerId = playerID;
        }

    }

    


}
