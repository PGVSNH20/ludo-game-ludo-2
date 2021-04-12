using GameEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace GameEngine.Classes
{
    public class SaveGame
    {

        public int SaveGameId { get; set; }
        public int CurrentPlayer { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        
        public SaveGame(int currentPlayer)
        {
            CurrentPlayer = currentPlayer;
        }
    }
}
