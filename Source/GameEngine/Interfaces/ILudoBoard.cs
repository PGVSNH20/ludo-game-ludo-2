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
        static object[,]GameBoard { get; set; }

        bool MovePiece(GamePiece piece, int diceRoll);

        bool TryMove(Position position);

        void Knuff(GamePiece piece);

        void CheckGameState();
    }
}
