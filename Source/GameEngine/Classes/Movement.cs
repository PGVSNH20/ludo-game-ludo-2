using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            // Innerpath Jens påfund, kom ihåg lägga ytterligare en out
            var innerPaths = Player.GetAllInnerPaths();
            if (piece.OnInnerPath == true)
                for (int index = 0; index < innerPaths.Count; index++)
                    if (innerPaths[index].Row == position.Row && innerPaths[index].Col == position.Col)
                        traversablePos = index;

            else // regular way
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
                int offset = 0;
                var currentDicePostion = 1 + i;

                if (traversablePos + currentDicePostion >= Game.Traversable.Count)
                {
                    offset = 0 - Game.Traversable.Count;
                }

                var nextStep = Game.Traversable[traversablePos + currentDicePostion + offset];

                bool lastStep = i == diceRoll - 1;

                if (!Movement.TryMove(piece, nextStep, lastStep, out bool onInnerPath)) // Om TryMove returnerar false misslyckas move, GamePiece kan inte flyttas
                {
                    //Move failed

                    piece.OnInnerPath = onInnerPath;

                    return false;
                }

                //TODO: If final move is not valid, don't move at all
                //TODO: Check out of range
                newPosition = Game.Traversable[traversablePos + currentDicePostion + offset];
            }

            //Place piece on new position and set the old position to the original value when we created the board
            Game.GameBoard[newPosition.Row, newPosition.Col] = piece;
            Game.GameBoard[position.Row, position.Col] = Game.OriginalGameBoard[position.Row, position.Col];
            return true;
        }

        public static bool TryMove(GamePiece gamePiece, Position position, bool isFinalStep, out bool onInnerPath) // Jens måste kanske ta bort out
        {
            onInnerPath = false;

            var row = position.Row;
            var column = position.Col;
            var boardObject = Game.GameBoard[row, column];
            var originalBoard = Game.OriginalGameBoard[row, column];
            var playerID = gamePiece.PlayerId;

            //If nest of the same color, move player to InnerPath towards the goal
            if (originalBoard.GetType() == typeof(Nest) && (originalBoard as Nest).PlayerId == playerID)
            {
                onInnerPath = true;
                gamePiece.OnInnerPath = onInnerPath;
                
                //TODO: Fix innerpath movement logic
                return false;
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
            else if (originalBoard.GetType() == typeof(Goal))
            {
                //TODO: Piece reached the goal logic
                //TODO: Check gamestate and potentially finish the game
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
