using GameEngine.Objects;
using System;
using System.Collections.Generic;
using System.Numerics;
using GameEngine.Classes;

namespace GameEngine
{
    public class Player
    {
        public int PlayerId { get; set; }

        public int SaveGameId { get; set; } = -1;

        public int Row { get; }

        public int Col { get; }

        public List<GamePiece> Pieces;

        public ConsoleColor Color;

        public Player()
        {

        }

        public Player(int numberOfPieces, ConsoleColor color, int playerId, Position startPosition)
        {
            Color = color;
            Pieces = new List<GamePiece>();
            Row = startPosition.Row;
            Col = startPosition.Col;

            for (int i = 0; i < numberOfPieces; i++)
            {
                char pieceNumber = (i + 1).ToString()[0];
                Pieces.Add(new GamePiece(pieceNumber, playerId));
            }
        }

        public List<Position> GetInnerPathFromColor(Player player)
        {

            // istället för player för inparamter kanske man kan använda this.Color få ut
            // det automatiskt

            switch (player.Color)
            {
                case ConsoleColor.Blue: // count 5
                    return new List<Position>
                    {
                        new Position(0,5),
                         new Position(1,5),
                          new Position(2,5),
                           new Position(3,5),
                            new Position(4,5),
                    };
                case ConsoleColor.Red:
                    return new List<Position>
                    {
                        new Position(5,0),
                         new Position(5,1),
                          new Position(5,2),
                           new Position(5,3),
                            new Position(5,4),
                    };
                case ConsoleColor.Green:
                    return new List<Position>
                    {
                        new Position(10,5),
                         new Position(9,5),
                          new Position(8,5),
                           new Position(7,5),
                            new Position(6,5),
                    };
                case ConsoleColor.Yellow:
                    return new List<Position>
                    {
                        new Position(5,10),
                         new Position(5,9),
                          new Position(5,8),
                           new Position(5,7),
                            new Position(5,6),
                    };
                default:
                    throw new Exception("The Colour Out of Space.");
            }
        }
        public static List<Position> GetAllInnerPaths()
        {

            // istället för player för inparamter kanske man kan använda this.Color få ut
            // det automatiskt

            return new List<Position>
            {
                // Player 1
                //new Position(5, 0),
                new Position(1, 5),
                new Position(2, 5),
                new Position(3, 5),
                new Position(4, 5),
                new Position(5, 5), //Goal

                // Player 2
                //new Position(5, 10),
                new Position(5, 9),
                new Position(5, 8),
                new Position(5, 7),
                new Position(5, 6),
                new Position(5, 5), //Goal

                // Player 3
                //new Position(10, 5),
                new Position(9, 5),
                new Position(8, 5),
                new Position(7, 5),
                new Position(6, 5),
                new Position(5, 5), //Goal

                // Player 4
                //new Position(0, 5),
                new Position(5, 1),
                new Position(5, 2),
                new Position(5, 3),
                new Position(5, 4),
                new Position(5, 5) //Goal
            };

        }
    }
}
