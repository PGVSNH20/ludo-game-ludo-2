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
    class Game : ILudoBoard
    {
        public static Rules Rules;
        private Dice dice = new Dice();

        public Game(Rules rules)
        {
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
    }
}
