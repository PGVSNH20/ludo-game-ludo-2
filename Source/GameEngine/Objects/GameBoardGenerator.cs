using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Objects
{
    class GameBoardGenerator
    {
        public static object[,] Generate(int columns, int rows, List<Player> players)
        {
            object[,] gameBoard = new object[columns, rows];

            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    gameBoard[i, j] = new Path();
                }
            }

            gameBoard = PopulateWithNests(gameBoard, players);
            gameBoard = PopulateWithEmptySpaces(gameBoard);


            return gameBoard;
        }

        public static object[,] PopulateWithNests(object[,] gameBoard, List<Player> players)
        {
            if (players.Count == 4)
            {
                gameBoard[0, 6] = new Nest(0);
                gameBoard[4, 0] = new Nest(1);
                gameBoard[6, 10] = new Nest(2);
                gameBoard[10, 4] = new Nest(3);
            }
            else if (players.Count == 3)
            {
                gameBoard[0, 4] = new Nest(0);
                gameBoard[6, 0] = new Nest(1);
                gameBoard[10, 6] = new Nest(2);
            }
            else if (players.Count == 2)
            {
                gameBoard[0, 4] = new Nest(0);
                gameBoard[10, 6] = new Nest(1);
            }
            else
            {
                throw new Exception("Du får inte spela själv!");
            }


            return gameBoard;
        }
        public static object[,] PopulateWithEmptySpaces(object[,] gameBoard)
        {
            var emptySpaces = new EmptySpace();

            foreach (var entryPoint in emptySpaces.GetEmptySpacesOnBoardFromEntryPoints())
                for (int i = 0; i < gameBoard.GetLength(0); i++)
                    for (int j = 0; j < gameBoard.GetLength(1); j++)
                    {
                        var position = new Position(j, i);
                        if (position.Row == entryPoint.Position.Row && position.Col == entryPoint.Position.Col )
                        {
                            gameBoard[j, i] = new EmptySpace();
                        }
                    }

            return gameBoard;
        }
    }
}
