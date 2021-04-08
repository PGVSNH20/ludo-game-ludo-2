﻿using GameEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Objects
{
    public class GameBoardGenerator // satt den som public Jens för test
    {
        public static IBoardObject[,] Generate(int columns, int rows, List<Player> players)
        {
            IBoardObject[,] gameBoard = new IBoardObject[columns, rows];

            for (int i = 0; i < gameBoard.GetLength(0); i++) // Kanske göra detta till en metod eller för enhetstest eller populera arrayen någon annanstans
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    gameBoard[i, j] = new Path();
                }
            }

            gameBoard = PopulateWithNests(gameBoard, players); // Ytterligare en metod för population?
            gameBoard = PopulateWithEmptySpaces(gameBoard);
            gameBoard = PopulateWithDoorWays(gameBoard, players);
            gameBoard = PopulateWithInnerPath(gameBoard, players);
            gameBoard = PopulateWithGoal(gameBoard);


            return gameBoard;
        }

 

        public static IBoardObject[,] PopulateWithNests(IBoardObject[,] gameBoard, List<Player> players)
        {
            if (players.Count == 4)
            {
                gameBoard[0, 6] = new Nest(0);
                gameBoard[6, 10] = new Nest(1);
                gameBoard[10, 4] = new Nest(2);
                gameBoard[4, 0] = new Nest(3);
            }
            else if (players.Count == 3)
            {
                gameBoard[0, 4] = new Nest(0);
                gameBoard[6, 10] = new Nest(1);
                gameBoard[10, 4] = new Nest(2);
            }
            else if (players.Count == 2)
            {
                gameBoard[0, 6] = new Nest(0);
                gameBoard[10, 4] = new Nest(1);
            }
            else
            {
                throw new Exception("Du får inte spela själv!");
            }


            return gameBoard;
        }

        public static IBoardObject[,] PopulateWithGoal(IBoardObject[,] gameBoard)
        {
            gameBoard[5, 5] = new Goal();

            return gameBoard;
        }

        public static IBoardObject[,] PopulateWithEmptySpaces(IBoardObject[,] gameBoard)
        {
            var emptySpaces = new EmptySpace();

            foreach (var entryPoint in emptySpaces.GetEmptySpacesOnBoardFromEntryPoints())
                for (int row = 0; row < gameBoard.GetLength(0); row++)
                    for (int col = 0; col < gameBoard.GetLength(1); col++)
                        if (row == entryPoint.Position.Row && col == entryPoint.Position.Col)
                            gameBoard[row, col] = new EmptySpace();

            return gameBoard;
        }
        public static IBoardObject[,] PopulateWithDoorWays(IBoardObject[,] gameBoard, List<Player> players)
        {
            var changeColorBasedOnInterval = -1;

            var doorway = Roadmap.Doorways;

            for (int i = 0; i < Roadmap.Doorways.Count; i++)
            {
                if (i % 4 == 0)
                {
                    ++changeColorBasedOnInterval;
                }
                gameBoard[doorway[i].Row, doorway[i].Col] = new DoorwayToInnerPath(changeColorBasedOnInterval);
            }

            return gameBoard;
        }
        public static IBoardObject[,] PopulateWithInnerPath(IBoardObject[,] gameBoard, List<Player> players)
        {
            // Red
            var changeColorBasedOnInterval = -1;

            var paths = Player.GetAllInnerPaths();

            for (int i = 0; i < Player.GetAllInnerPaths().Count; i++)
            {
                if (i % 4 == 0)
                {
                    ++changeColorBasedOnInterval;
                }
                gameBoard[paths[i].Row, paths[i].Col] = new InnerSteppingStone(changeColorBasedOnInterval);
            }
            
            return gameBoard;
        }
    }
}
