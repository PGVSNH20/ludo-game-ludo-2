using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Classes
{
    public class Rules
    {
        public int RulesId { get; set; }
        [ForeignKey("SaveGame")]
        public int SaveGameId { get; set; } = -1;
        public int NumberOfPlayers { get; set; }
        public int PiecesPerPlayer { get; set; }
        public bool ThrowAgainOnSixEnabled { get; set; }

        //If 6 when trying to leave nest, choose either to put out 2 pieces or one piece at space 6
        public bool InitialSixRuleEnabled { get; set; }

        public Rules()
        {
            NumberOfPlayers = 4;
            PiecesPerPlayer = 4;
            ThrowAgainOnSixEnabled = true;
            InitialSixRuleEnabled = true;
        }

        public Rules(int numberOfPlayers, int piecesPerPlayer, bool throwAgainOnSixEnabled, bool initialSixRuleEnabled)
        {
            NumberOfPlayers = numberOfPlayers;
            PiecesPerPlayer = piecesPerPlayer;
            ThrowAgainOnSixEnabled = throwAgainOnSixEnabled;
            InitialSixRuleEnabled = initialSixRuleEnabled;
        }
    }
}
