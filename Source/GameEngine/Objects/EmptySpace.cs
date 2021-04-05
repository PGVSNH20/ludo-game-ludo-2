using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Interfaces;

namespace GameEngine.Objects
{
    public class EmptySpace : IBoardObject
    {
        public char CharToDraw => ' ';

        public static char Character => (char)32;

        public Position Position { get;}
        public EmptySpace()
        {

        }
        public EmptySpace(Position pos)
        {
            Position = pos;
        }

        List<EmptySpace> _emptySpaces;
        public override string ToString() => CharToDraw.ToString();


        public List<EmptySpace> GetEmptySpacesOnBoardFromEntryPoints()
        {
            _emptySpaces = new List<EmptySpace>(); // (64)??
            object[,] gameBoard = new object[11, 11];

            var entryPoint1 = new Position(0, 0); // (0,0)
            var entryPoint2 = new Position(7, 0); // (7,0)
            var entryPoint3 = new Position(0, 7); // (0,7)
            var entryPoint4 = new Position(7, 7); // (0,0)

            List<Position> entryPoints = new List<Position>()
            {
                new Position(0,0),
                 new Position(7, 0),
                 new Position(0, 7),
                  new Position(7, 7)
            };


            var epDimension = 4; // width/height

            foreach (var entryPoint in entryPoints) // checkar för varje entrypoint
                for (int row = 0; row < gameBoard.GetLength(0); row++)
                    for (int col = 0; col < gameBoard.GetLength(1); col++)
                    {
                        if ((row >= entryPoint.Row && row < entryPoint.Row + epDimension)  &&  (col >= entryPoint.Col && col < entryPoint.Row + epDimension))
                        {
                            var emptyPosition = new Position(row, col);
                            _emptySpaces.Add(new EmptySpace(emptyPosition));

                        }
                    }
            return _emptySpaces;

        }

    }
}
