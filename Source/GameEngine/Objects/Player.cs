using GameEngine.Objects;
using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class Player
    {

        public Position StartPosition { get; set; }

        private List<Position> innerPath;


        public Position TravelPlan { get; set; }

        public  List<GamePiece> Pieces;
       
        public ConsoleColor Color;

        public Player(int numberOfPieces, ConsoleColor color, int playerId)
        {
            Color = color;
            Pieces = new List<GamePiece>();


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

                    // Red
                //new Position(5, 0),
                new Position(5, 1),
                new Position(5, 2),
                new Position(5, 3),
                new Position(5, 4),

                // Blue
                //new Position(0, 5),
                new Position(1, 5),
                new Position(2, 5),
                new Position(3, 5),
                new Position(4, 5),

            

                // Green
                //new Position(10, 5),
                new Position(9, 5),
                new Position(8, 5),
                new Position(7, 5),
                new Position(6, 5),

                // Yellow
                //new Position(5, 10),
                new Position(5, 9),
                new Position(5, 8),
                new Position(5, 7),
                new Position(5, 6)
            };

        }
    }
}
