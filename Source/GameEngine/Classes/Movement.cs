using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Objects;

namespace GameEngine.Classes
{
    class Movement
    {
        public static bool MovePiece(GamePiece piece, int diceRoll)
        {
            Position position = GameBoardGenerator.FindObject(Game.GameBoard, piece);
            Position newPosition = new Position();

            int traversablePos = -1;
            for (int i = 0; i < Game.Traversable.Count; i++)
            {
                if (Game.Traversable[i].Row == position.Row && Game.Traversable[i].Col == position.Col)
                {
                    traversablePos = i;
                }
            }

            if (traversablePos == -1) // Hur får den till -1 eller ska vi jämföra under minus
            {
                //Couldn't find position
                return false;
            }

            for (int i = 0; i < diceRoll; i++)
            {
                //TODO: Inner path code
                //TODO: Check out of range
                var nextStep = Game.Traversable[traversablePos + 1];
                if (!Movement.TryMove(nextStep, piece.PlayerId)) // Om TryMove returnerar false misslyckas move, GamePiece kan inte flyttas
                {
                    //Move failed
                    return false;
                }

                //TODO: If final move is not valid, don't move at all
                //TODO: Check out of range
                newPosition = Game.Traversable[traversablePos + i + 1];
            }

            //Place piece on new position and set the old position to the original value when we created the board
            Game.GameBoard[newPosition.Row, newPosition.Col] = piece;
            Game.GameBoard[position.Row, position.Col] = Game.OriginalGameBoard[position.Row, position.Col];

            return true;
        }

        public static bool TryMove(Position position, int playerID)
        {
            var row = position.Row;
            var column = position.Col;
            var boardObject = Game.GameBoard[row, column];
            //TODO: Check if can move
            if (boardObject.GetType() == typeof(Nest) && (boardObject as Nest).PlayerId == playerID)
            {
                return false;
            }

            return true;
        }

        public static void Knuff(GamePiece piece)
        {
            throw new NotImplementedException();
        }

    }
}
