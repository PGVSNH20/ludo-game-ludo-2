using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Interfaces;

namespace GameEngine
{
    /// <summary>
    /// Overlying logic for how the game should run
    /// </summary>
    public class Game : ILudoBoard
    {
        //TODO: Interface för object på vårt gameboard. Byta ut object till interfacet.
        public object[,] GameBoard { get; set; }
        public static Rules Rules;
        private Dice dice = new Dice();

        public Game(Rules rules)
        {
            GameBoard = new object[11, 11];
            Rules = rules;
            List<Player> players = AddPlayer(rules.NumberOfPlayers);
        }
                      

        /// <summary>
        /// Populate a list of players.
        /// </summary>
        /// <param name="NumberOfPlayers">How many players there are</param>
        /// <returns>A list populated with players</returns>
        private List<Player> AddPlayer(int NumberOfPlayers)
        {
            List<Player> players = new List<Player>();

            for (int i = 0; i < NumberOfPlayers; i++)
            {
                players.Add(new Player(Rules.PiecesPerPlayer, ConsoleColor.Cyan));
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
    }
}
