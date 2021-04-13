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
        /// <summary>
        /// Move a GamePiece and check if the move was valid.
        /// </summary>
        /// <param name="piece">GamePiece to move</param>
        /// <param name="diceRoll">How far to move (dice)</param>
        /// <returns>True if the move succeeded, else false</returns>
        public static bool MovePiece(GamePiece piece, int diceRoll)
        {
            Position position = GameBoardGenerator.FindObject(Game.GameBoard, piece);
            Position newPosition = new Position();
            
            int traversablePos = -1;
            List<Position> currentPath;

            var onInnerPath = piece.OnInnerPath;

            currentPath = piece.OnInnerPath ? Player.GetAllInnerPaths() : Game.Traversable;

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
                var currentDicePosition = 1 + i;

                if (traversablePos + currentDicePosition >= currentPath.Count)
                {
                    offset = 0 - currentPath.Count;
                }

                var nextStep = currentPath[traversablePos + currentDicePosition + offset];

                bool finalStep = i == diceRoll - 1;

                if (!Movement.TryMove(piece, nextStep, finalStep)) // Om TryMove returnerar false misslyckas move, GamePiece kan inte flyttas
                {
                    //Move failed

                    return false;
                }

                if (piece.OnInnerPath && !onInnerPath)
                {
                    currentPath = Player.GetAllInnerPaths();

                    diceRoll -= currentDicePosition;
                    currentDicePosition = 0;
                    offset = 0;
                    i = -1;

                    traversablePos = (currentPath.Count / Game.Rules.NumberOfPlayers) * piece.PlayerId;

                    onInnerPath = true;
                }
                //TODO: If final move is not valid, don't move at all
                newPosition = currentPath[traversablePos + currentDicePosition + offset];
            }
            //Place piece on new position and set the old position to the original value when we created the board
            Game.GameBoard[newPosition.Row, newPosition.Col] = piece;
            Game.GameBoard[position.Row, position.Col] = Game.OriginalGameBoard[position.Row, position.Col];
            return true;
        }

        /// <summary>
        /// Checks the index of a position on a path
        /// </summary>
        /// <param name="path">A list of positions</param>
        /// <param name="pos">Position to check</param>
        /// <param name="piece">I... don't know why this is here and too late to change</param>
        /// <returns>The index in the list that the position is on, or -1</returns>
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

        /// <summary>
        /// Check what happens when moving a piece to a position
        /// </summary>
        /// <param name="gamePiece">The piece to check</param>
        /// <param name="position">The position to check</param>
        /// <param name="isFinalStep">Is it the final position to check on a diceroll?</param>
        /// <returns></returns>
        public static bool TryMove(GamePiece gamePiece, Position position, bool isFinalStep) // Jens måste kanske ta bort out. // Mattias tog bort den istället. //Fabian blev nöjd. //Belinda skrattade mycket.
        {
            var row = position.Row;
            var column = position.Col;
            var boardObject = Game.GameBoard[row, column];
            var originalBoard = Game.OriginalGameBoard[row, column];
            var playerID = gamePiece.PlayerId;




            //TODO: Messaging here to keep the players slightly less confused
            if (originalBoard.GetType() == typeof(Goal) && isFinalStep)
            {
                gamePiece.IsPlacedOnBoard = false;
                gamePiece.OnInnerPath = false;
                gamePiece.HasFinished = true;
                Game.SetStatusMessage($"One of player {Game.Players[gamePiece.PlayerId].Color}'s pieces reached the goal!");
                //On inner path still?
                return true;
            }
            else if (originalBoard.GetType() == typeof(Goal))
            {
                //Piece missed the goal
                Game.SetStatusMessage($"You missed the goal! :(");
                return false;
            }
            //If nest of the same color, move player to InnerPath towards the goal
            else if (originalBoard.GetType() == typeof(Nest) && (originalBoard as Nest).PlayerId == playerID && gamePiece.IsPlacedOnBoard)
            {
                gamePiece.OnInnerPath = true;
                return true;
            }
            //If piece of own color is found along the way, move is not valid
            else if (boardObject.GetType() == typeof(GamePiece) && (boardObject as GamePiece).PlayerId == playerID)
            {
                Game.SetStatusMessage($"You can't move past your own piece!");
                return false;
            }
            //If another player's piece and it's the last step, commence knuff
            else if (boardObject.GetType() == typeof(GamePiece) && isFinalStep)
            {
                Knuff(boardObject as GamePiece);
                Game.SetStatusMessage($"Player {Game.Players[(boardObject as GamePiece).PlayerId].Color}'s piece was knuff'd! Oh no!");
                return true;
            }

            int pieceID = GetPieceIdFromIndex(gamePiece);
      
            Game.SetStatusMessage($"Player {Game.Players[gamePiece.PlayerId].Color} moved piece {pieceID}.");
            return true;
        }
        
        /// <summary>
        /// Get piece ID from index.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns>Piece ID</returns>
        static public int GetPieceIdFromIndex(GamePiece piece)
        {
            for (int i = 0; i < Game.Players[piece.PlayerId].Pieces.Count; i++)
            {
                if (Game.Players[piece.PlayerId].Pieces[i].Equals(piece))
                    return i + 1;
            }
            return -1; 
                
        }

        /// <summary>
        /// Jens wanted to use ref, so this is unused, probably because of that
        /// </summary>
        /// <param name="boardObj"></param>
        /// <param name="playerID"></param>
        /// <param name="isFinalStep"></param>
        /// <param name="gamePiece"></param>
        /// <returns></returns>
        public bool CheckTypeOfBoardSubjects(object boardObj, int playerID, bool isFinalStep, ref GamePiece gamePiece)
        {

            switch (boardObj )
            {
                case Nest nest when nest.PlayerId.Equals(playerID) && gamePiece.IsPlacedOnBoard:
                    
                    gamePiece.OnInnerPath = true;
                    return true;
                case GamePiece piece when piece.PlayerId == playerID:
                    
                    Game.SetStatusMessage($"You can't move past your own piece!");
                    return false;
                case GamePiece piece when isFinalStep:
                    
                    Knuff(piece);
                    Game.SetStatusMessage($"Player {GetColorFrom(piece)}'s piece was knuff'd! Oh no!");
                    return true;
                case Goal when isFinalStep:
                    
                    gamePiece.IsPlacedOnBoard = false;
                    gamePiece.HasFinished = true;
                    Game.SetStatusMessage($"One of the player {GetColorFrom(gamePiece)}'s piece reached the goal!");
                    return true;
                case Goal:
                Game.SetStatusMessage($"You missed the goal! :( ");
                    return false;

                default:
                    break;
            }
            return false;

        }

        /// <summary>
        /// Get the color of a gamepiece.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A color</returns>
        public ConsoleColor GetColorFrom(GamePiece id) => Game.Players[id.PlayerId].Color;

        /// <summary>
        /// When one piece knocks out another piece.
        /// </summary>
        /// <param name="piece">The piece that does the knocking</param>
        public static void Knuff(GamePiece piece)
        {
            piece.IsPlacedOnBoard = false;
        }
    }
}
