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


            return gameBoard;
        }

        public static object[,] PopulateWithNests(object[,] gameBoard, List<Player> players)
        {
            //Place nests
            if (players.Count == 4)
            {
                gameBoard[5, 0] = new Nest();
            }


            return gameBoard;
        }
    }
}
