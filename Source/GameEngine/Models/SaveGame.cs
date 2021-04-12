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

        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string GamePiece1 { get; set; }
        public string GamePiece2 { get; set; }
        public string GamePiece3 { get; set; }
        public string GamePiece4 { get; set; }

        public string Color { get; set; }
        public DateTime Date { get; set; } = new DateTime();

        public SaveGame()
        {

        }
        public SaveGame(int playerId, ConsoleColor color)
        {
            PlayerId = playerId;
            Color = color.ToString();
            Date = DateTime.Now;
        }


        public SaveGame(Player id, GamePiece gp1, GamePiece gp2, GamePiece gp3, GamePiece gp4, ConsoleColor color)
        {
            PlayerId = id.Id;
            GamePiece1 = gp1.Position.ToString();
            GamePiece2 = gp2.Position.ToString();
            GamePiece3 = gp3.Position.ToString();
            GamePiece4 = gp4.Position.ToString();
            Color = id.Color.ToString();
            Date = DateTime.Now;
        }


        //public void CheckGameState()
        //{
        //    throw new NotImplementedException();
        //}

        //public void GetCurrentGameProgressToLoadIntoDB()
        //{

        //    //Position position = GameBoardGenerator.FindObject(Game.GameBoard, piece);
        //}
    }
}
