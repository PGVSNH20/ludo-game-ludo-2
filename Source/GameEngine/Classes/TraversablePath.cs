using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Classes
{
    public class TraversablePath
    {
        public static List<Position> CreatePath()
        {
            //https://www.spelregler.org/wp-content/uploads/fia-med-knuff.png

            var path = new List<Position>()
            {
                new Position(0, 6), //Top right starting position
                new Position(1, 6),
                new Position(2, 6),
                new Position(3, 6),
                new Position(4, 6),
                new Position(4, 7),
                new Position(4, 8),
                new Position(4, 9),
                new Position(4, 10),
                new Position(5, 10),
                new Position(6, 10),
                new Position(6, 9),
                new Position(6, 8),
                new Position(6, 7),
                new Position(6, 6),
                new Position(7, 6),
                new Position(8, 6),
                new Position(9, 6),
                new Position(10, 6),
                new Position(10, 5),
                new Position(10, 4),
                new Position(9, 4),
                new Position(8, 4),
                new Position(7, 4),
                new Position(6, 4),
                new Position(6, 3),
                new Position(6, 2),
                new Position(6, 1),
                new Position(6, 0),
                new Position(5, 0),
                new Position(4, 0),
                new Position(4, 1),
                new Position(4, 2),
                new Position(4, 3),
                new Position(4, 4),
                new Position(3, 4),
                new Position(2, 4),
                new Position(1, 4),
                new Position(0, 4),
                new Position(0, 5)
            };

            return path;
        }
    }
}
