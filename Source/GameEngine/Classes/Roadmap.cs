using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Classes;

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

        Position startPosition1 = new Position(6, 0);
        Position startPosition2 = new Position(0, 4);
        Position startPosition3 = new Position(10, 6);
        Position startPosition4 = new Position(4, 10);

        Position endPosition1 = new Position(5, 0);
        Position endPosition2 = new Position(0, 5);
        Position endPosition3 = new Position(10, 5);
        Position endPosition4 = new Position(5, 10);



        public List<List<Position>> GetAllPathsToTraverse()
        {

            var pathsToTravel = new List<List<Position>>();
            {
                new List<Position>
                {
                    endPosition1,
                    startPosition1,
                    new Position(6, 2),
                    new Position(6, 3),
                    new Position(6, 4),
                    // Kröken fröken ifrån fräken
                    new Position(7,4),
                    new Position(8,4),
                    new Position(9,4),
                    new Position(10,4)
                };
                new List<Position>
                {
                    endPosition2,
                    startPosition2,
                    new Position(1, 4),
                    new Position(2, 4),
                    new Position(6, 1),
                    new Position(3, 4),
                    new Position(4, 4),

                    new Position(4,1),
                    new Position(4,2),
                    new Position(4,3),
                    new Position(4,4)

                };
                new List<Position>
                {
                    endPosition3,
                    startPosition3,
                    new Position(9, 6),
                    new Position(7, 6),
                    new Position(6, 6),
                    new Position(6, 6),

                    new Position(6,7),
                    new Position(6,8),
                    new Position(6,9),
                    new Position(6,10),

                };
                new List<Position>
                {
                    endPosition4,
                    startPosition4,
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
                 new List<Position>
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


    }
}
