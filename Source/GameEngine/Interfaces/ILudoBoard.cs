using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Interfaces
{
    interface ILudoBoard
    {
        object[][] GameBoard { get; set; }

        void MovePiece(GamePiece piece, int diceRoll);

        bool TryMove(int posX, int posY);

        void Knuff(GamePiece piece);

        void CheckGameState();
    }
}
