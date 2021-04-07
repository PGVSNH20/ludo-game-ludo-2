using GameEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Interfaces
{
    public interface ILudoBoard
    {
        static object[,]GameBoard { get; set; }

        Position TryToGetMovePosition(GamePiece piece, int diceRoll, Position position);

        bool TryMove(int posX, int posY);

        void Knuff(GamePiece piece);

        void CheckGameState();
    }
}
