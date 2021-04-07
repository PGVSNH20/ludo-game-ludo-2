using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Objects
{
    /*       outerBoard = new char[11, 11]
            {
                {' ',' ',' ',' ','#','#','@',' ',' ',' ',' '},
                {' ',' ',' ',' ','#',' ','#',' ',' ',' ',' '},
                {' ',' ',' ',' ','#',' ','#',' ',' ',' ',' '},
                {' ',' ',' ',' ','#',' ','#',' ',' ',' ',' '},
                {'@','#','#','#','#',' ','#','#','#','#','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#','#','#','#','#',' ','#','#','#','#','@'},
                {' ',' ',' ',' ','#',' ','#',' ',' ',' ',' '},
                {' ',' ',' ',' ','#',' ','#',' ',' ',' ',' '},
                {' ',' ',' ',' ','#',' ','#',' ',' ',' ',' '},
                {' ',' ',' ',' ','@','#','#',' ',' ',' ',' '},
             };
    */
    public class Roadmap
    {
        public List<Position> TravelPlan { get; set; }

        public static Position StartPosition1 => new Position(6, 0);
       public static Position StartPosition2 => new Position(0, 4);
       public static Position StartPosition4 => new Position(4, 10);
       public static Position StartPosition3 => new Position(10, 6);

     

        Position endPosition1 = new Position(5, 0);
        Position endPosition2 = new Position(0, 5);
        Position endPosition3 = new Position(10, 5);
        Position endPosition4 = new Position(5, 10);



        public List<List<Position>> GetAllPathsToTraverse()
        {

            var pathsToTravel = new List<List<Position>>();
            {
                new List<Position> // Red
                {
                    endPosition1,
                    StartPosition1,
                    new Position(6, 1),
                    new Position(6, 2),
                    new Position(6, 3),
                    new Position(6, 4),
                    // Kröken fröken ifrån fräken
                    new Position(7,4),
                    new Position(8,4),
                    new Position(9,4),
                    new Position(10,4)
                };
                new List<Position> // Green
                {
                    endPosition2,
                    StartPosition2,
                    new Position(1, 4),
                    new Position(2, 4),
                    new Position(3, 4),
                    new Position(4, 4),

                    new Position(4,1),
                    new Position(4,2),
                    new Position(4,3),
                    new Position(4,4)

                };
                new List<Position> // Yellow
                {
                    endPosition3,
                    StartPosition3,
                    new Position(9, 6),
                    new Position(7, 6),
                    new Position(6, 6),
                    new Position(6, 6),

                    new Position(6,7),
                    new Position(6,8),
                    new Position(6,9),
                    new Position(6,10),

                };
                new List<Position> // Blue
                {
                    endPosition4,
                    StartPosition4,
                    new Position(4, 9),
                    new Position(4, 8),
                    new Position(4, 7),
                    new Position(4, 6),

                    new Position(3,6),
                    new Position(2,6),
                    new Position(1,6),
                    new Position(0,6),

                };


            };

            return pathsToTravel;

        }

        public List<List<Position>> GetAllInnerPaths()
        {
            var innerPathsToTravel = new List<List<Position>>()
            {
                 new List<Position> // Red
                 {
                      new Position(5, 1),
                      new Position(5, 2),
                      new Position(5, 3),
                      new Position(5, 4)

                 },
                 new List<Position>
                 {

                     new Position(1, 5),
                     new Position(2, 5),
                     new Position(3, 5),
                     new Position(4, 5)

                 },
                  new List<Position>
                  {
                      new Position(9, 5),
                      new Position(8, 5),
                      new Position(7, 5),
                      new Position(6, 5)

                  },
                   new List<Position>
                   {
                       new Position(5, 9),
                       new Position(5, 8),
                       new Position(5, 7),
                       new Position(5, 6)
                   }

             };


            return innerPathsToTravel;
        }

        public List<Position> GetRoadToTravelFromPlayerSpecification(Position startPosition)
        {
           
            var travelPlan = new List<Position>();
            Position ableToWalkInnerPath;
          
            if (startPosition.Row == StartPosition1.Row && startPosition.Col == StartPosition1.Col)
            {
                travelPlan.AddRange(GetAllPathsToTraverse()[0].Skip(1));
                travelPlan.AddRange(GetAllPathsToTraverse()[1]);
                travelPlan.AddRange(GetAllPathsToTraverse()[2]);
                travelPlan.AddRange(GetAllPathsToTraverse()[3]);

                ableToWalkInnerPath = (GetAllPathsToTraverse()[0]).First();
                travelPlan.Add(ableToWalkInnerPath);
                // Innerpath
                travelPlan.AddRange(GetAllInnerPaths()[0]);

            }
            else if (startPosition.Row == StartPosition2.Row && startPosition.Col == StartPosition2.Col)
            {
                travelPlan.AddRange(GetAllPathsToTraverse()[1].Skip(1));
                travelPlan.AddRange(GetAllPathsToTraverse()[2]);
                travelPlan.AddRange(GetAllPathsToTraverse()[3]);
                travelPlan.AddRange(GetAllPathsToTraverse()[0]);

                ableToWalkInnerPath = (GetAllPathsToTraverse()[1]).First();
                travelPlan.Add(ableToWalkInnerPath);
                // Innerpath
                travelPlan.AddRange(GetAllInnerPaths()[1]);

            }
            else if (startPosition.Row == StartPosition3.Row && startPosition.Col == StartPosition3.Col)
            {
                travelPlan.AddRange(GetAllPathsToTraverse()[2].Skip(1));
                travelPlan.AddRange(GetAllPathsToTraverse()[3]);
                travelPlan.AddRange(GetAllPathsToTraverse()[0]);
                travelPlan.AddRange(GetAllPathsToTraverse()[1]);

                ableToWalkInnerPath = (GetAllPathsToTraverse()[2]).First();
                travelPlan.Add(ableToWalkInnerPath);
                // Innerpath
                travelPlan.AddRange(GetAllInnerPaths()[2]);
            }
            else if (startPosition.Row == StartPosition4.Row && startPosition.Col == StartPosition4.Col)
            {
                travelPlan.AddRange(GetAllPathsToTraverse()[3].Skip(1));
                travelPlan.AddRange(GetAllPathsToTraverse()[0]);
                travelPlan.AddRange(GetAllPathsToTraverse()[1]);
                travelPlan.AddRange(GetAllPathsToTraverse()[2]);

                ableToWalkInnerPath = (GetAllPathsToTraverse()[3]).First();
                travelPlan.Add(ableToWalkInnerPath);
                // Innerpath
                travelPlan.AddRange(GetAllInnerPaths()[3]);

            }
            else throw new ArgumentException("No valid startpositon");

            return travelPlan;
        }

        

    }
}
