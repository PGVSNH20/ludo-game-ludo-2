using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class Player
    {
        public static List<GamePiece> Pieces;
        public ConsoleColor Color;

        public Player(int numberOfPieces, ConsoleColor color)
        {
            Color = color;
            Pieces = new List<GamePiece>();

            for (int i = 0; i < numberOfPieces; i++)
            {
                Pieces.Add(new GamePiece());
            }
        }
    }
}
