using GameEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Classes
{
    public class Result
    {
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string Player3 { get; set; }
        public string Player4 { get; set; }

        public string Positions { get; set; }

        public DateTime Date { get; set; } = new DateTime();

                    //Player1                Player2                Player3                 Player4             Date
        //Positions //(3,4)()(5,5)(2,1)     (3,4)()(5,5)(2,1)       (3,4)()(5,5)(2,1)       (3,4)()(5,5)(2,1)   2021/01/01

        public override string ToString()
        {
            // Drawain component
            Console.WriteLine("BlueTeam: [3] Home"); Console.WriteLine("BlueTeam:[1] Goal");
            Console.WriteLine("RedTeam: [3] Home"); Console.WriteLine("Team: [1] Goal");
            Console.WriteLine("Team: [3] Home"); Console.WriteLine("Team: [1] Goal");
            Console.WriteLine("Team: [3] Home"); Console.WriteLine("Team: [1] Goal");
            return null;                                                  
        }

        public void CheckGameState()
        {
            throw new NotImplementedException();
        }

        public void GetCurrentGameProgressToLoadIntoDB()
        {

            //Position position = GameBoardGenerator.FindObject(Game.GameBoard, piece);
        }
    }
}
