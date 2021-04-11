using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameEngine.Objects;
using Microsoft.EntityFrameworkCore;

namespace GameEngine.Classes
{
    class Movement
    {
        public static bool MovePiece(GamePiece piece, int diceRoll)
        {
            Position position = GameBoardGenerator.FindObject(Game.GameBoard, piece);
            Position newPosition = new Position();
            
            int traversablePos = -1;
            List<Position> currentPath;

            var onInnerPath = piece.OnInnerPath;

            if (piece.OnInnerPath)
            {
                currentPath = Player.GetAllInnerPaths();
            }
            else // regular way
            {
                currentPath = Game.Traversable;
            }

            traversablePos = FindOnPath(currentPath, position, piece);

            if (traversablePos == -1)
            {
                //Couldn't find position
                return false;
            }

            for (int i = 0; i < diceRoll; i++)
            {
                //TODO: Inner path code
                int offset = 0;
                var currentDicePostion = 1 + i;

                if (traversablePos + currentDicePostion >= currentPath.Count)
                {
                    offset = 0 - currentPath.Count;
                }

                var nextStep = currentPath[traversablePos + currentDicePostion + offset];

                bool finalStep = i == diceRoll - 1;

                if (!Movement.TryMove(piece, nextStep, finalStep)) // Om TryMove returnerar false misslyckas move, GamePiece kan inte flyttas
                {
                    //Move failed

                    return false;
                }

                if (piece.OnInnerPath && !onInnerPath)
                {
                    currentPath = Player.GetAllInnerPaths();

                    diceRoll -= currentDicePostion;
                    currentDicePostion = 0;
                    offset = 0;
                    i = -1;

                    traversablePos = (currentPath.Count / Game.Rules.NumberOfPlayers) * piece.PlayerId;

                    onInnerPath = true;
                }

                //TODO: If final move is not valid, don't move at all
                newPosition = currentPath[traversablePos + currentDicePostion + offset];
            }

            //Place piece on new position and set the old position to the original value when we created the board

            Game.GameBoard[newPosition.Row, newPosition.Col] = piece;
            Game.GameBoard[position.Row, position.Col] = Game.OriginalGameBoard[position.Row, position.Col];
            return true;
        }

        private static int FindOnPath(List<Position> path, Position pos, GamePiece piece)
        {
            for (int i = 0; i < path.Count; i++)
            {
                if (path[i].Row == pos.Row && path[i].Col == pos.Col)
                {
                    return i;
                }
            }

            return -1;
        }

        public static bool TryMove(GamePiece gamePiece, Position position, bool isFinalStep) // Jens måste kanske ta bort out. // Mattias tog bort den istället. //Fabian blev nöjd.
        {
            

            var row = position.Row;
            var column = position.Col;
            var boardObject = Game.GameBoard[row, column];
            var originalBoard = Game.OriginalGameBoard[row, column];
            var playerID = gamePiece.PlayerId;

            //TODO: Messaging here to keep the players slightly less confused
            //If nest of the same color, move player to InnerPath towards the goal
            if (originalBoard.GetType() == typeof(Nest) && (originalBoard as Nest).PlayerId == playerID)
            {
                gamePiece.OnInnerPath = true;
                
                //TODO: Fix innerpath movement logic
                return true;
            }
            //If piece of own color is found along the way, move is not valid
            else if (boardObject.GetType() == typeof(GamePiece) && (boardObject as GamePiece).PlayerId == playerID)
            {
                return false;
            }
            //If another player's piece and it's the last step, commence knuff
            else if (boardObject.GetType() == typeof(GamePiece) && isFinalStep)
            {
                Knuff(boardObject as GamePiece);
                return true;
            }
            else if (originalBoard.GetType() == typeof(Goal) && isFinalStep)
            {
                //TODO: Piece reached the goal logic
                //TODO: Check gamestate and potentially finish the game
                gamePiece.IsPlacedOnBoard = false;
                /*gamePiece.OnInnerPath = false;*/ // Comment out?
                gamePiece.HasFinished = true;
                //On inner path still?
                return true;
            }
            else if (originalBoard.GetType() == typeof(Goal))
            {
                //Piece missed the goal
                return false;
            }

            //TODO: Logic for what happens if a piece misses the goal, walks too far

            return true;
        }

        public static void Knuff(GamePiece piece)
        {
            piece.IsPlacedOnBoard = false;
            Game.SetActionMessage($"Player {Game.Players[piece.PlayerId].Color.ToString()}'s piece was knuff'd! Oh no!");
        }

    }
}
