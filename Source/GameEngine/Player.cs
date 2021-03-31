using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class Player
    {
        public static List<GamePiece> Pieces;
        public static ConsoleColor Color;

        public Player(ConsoleColor color)
        {
            Color = color;

            Pieces = new List<GamePiece>();
            //TODO: Get how many pieces we should have from somewhere
            for (int i = 0; i < 4; i++)
            {
                Pieces.Add(new GamePiece());
            }
        }
    }
}
