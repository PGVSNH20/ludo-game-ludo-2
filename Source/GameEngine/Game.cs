using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Interfaces;
using GameEngine.Objects;

namespace GameEngine
{
    /// <summary>
    /// Overlying logic for how the game should run
    /// </summary>
    public class Game : ILudoBoard
    {
        //TODO: Interface för object på vårt gameboard. Byta ut object till interfacet.
        public static object[,] GameBoard { get; set; }
        public static Rules Rules;
        private Dice dice = new Dice();
        public static string statusMessage { get; set; }
        public static List<Player> Players { get; set; }

        public Game(Rules rules)
        {
            Rules = rules;
            Players = AddPlayers(rules.NumberOfPlayers);
            GameBoard = GameBoardGenerator.Generate(11, 11, Players);
            
            //Dictionary for status messages?
        }
                      

        /// <summary>
        /// Populate a list of players.
        /// </summary>
        /// <param name="NumberOfPlayers">How many players there are</param>
        /// <returns>A list populated with players</returns>
        private List<Player> AddPlayers(int NumberOfPlayers)
        {
            List<Player> players = new List<Player>();

            for (int i = 0; i < NumberOfPlayers; i++)
            {
                players.Add(new Player(Rules.PiecesPerPlayer, ConsoleColor.Cyan)); // Ändrar kanske här
            }

            return players;
        }

        public void CheckGameState()
        {
            throw new NotImplementedException();
        }

        public void Knuff(GamePiece piece)
        {
            throw new NotImplementedException();
        }

        public void MovePiece(GamePiece piece, int diceRoll)
        {
            throw new NotImplementedException();
        }

        public bool TryMove(int posX, int posY)
        {
            throw new NotImplementedException();
        }


        public static List<DrawableChar> GenerateDrawable()
        {
            List<DrawableChar> drawableChars = new List<DrawableChar>();

            for (int i = 0; i < GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < GameBoard.GetLength(1); j++)
                {
                    var current = GameBoard[i, j];
                    if (current.GetType() == typeof(Nest))
                    {
                        ConsoleColor currentColor = Players[(current as Nest).PlayerId].Color;                       
                        drawableChars.Add(new DrawableChar((current as Nest).CharToDraw, currentColor));
                    }
                    else if (current.GetType() == typeof(Path))
                    {
                        drawableChars.Add(new DrawableChar('#', ConsoleColor.Red));
                    }
                    else if (current.GetType() == typeof(InnerSteppingStone))
                    {
                        var steppingStone = (current as InnerSteppingStone);
                        drawableChars.Add(new DrawableChar(steppingStone.CharToDraw, ConsoleColor.DarkBlue));
                    }
                    else if (current.GetType() == typeof(GamePiece))
                    {
                        drawableChars.Add(new DrawableChar('%', ConsoleColor.DarkYellow));
                    }
                    else if (current.GetType() == typeof(EmptySpace))
                    {
                        drawableChars.Add(new DrawableChar((current as EmptySpace).CharToDraw));
                    }
                    else
                    {
                        drawableChars.Add(new DrawableChar('%', ConsoleColor.DarkYellow));
                    }
                }
            }


            return drawableChars;
        }
    }

    
}
