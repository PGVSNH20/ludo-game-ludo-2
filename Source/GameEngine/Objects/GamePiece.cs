using GameEngine.Interfaces;
using GameEngine.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Classes;

namespace GameEngine
{
    public class GamePiece : IBoardObject
    {
        public int GamePieceId { get; set; }

        public int PlayerId { get; set; }

        [ForeignKey("SaveGame")]
        public int SaveGameId { get; set; } = -1;
        
        public char CharToDraw { get; set; }

        public bool IsPlacedOnBoard { get; set; } = false;

        public bool OnInnerPath { get; set; } = false;

        public bool HasFinished { get; set; } = false;

        public int Row { get; set; }

        public int Col { get; set; }

        public GamePiece()
        {
            
        }

        public GamePiece(char charToDraw, int playerID)
        {
            CharToDraw = charToDraw.ToString().First();
            PlayerId = playerID;
        }
    }

    


}
