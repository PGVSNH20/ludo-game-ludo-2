using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Classes
{
    public class TraversablePath
    {
        static public Position Nest1 { get; set; } = new Position(0, 6);
        static public Position Nest2 { get; set; } = new Position(6, 10);
        static public Position Nest3 { get; set; } = new Position(10, 4);
        static public Position Nest4 { get; set; } = new Position(4, 0);

        static public Position InnerPathEntry1 { get; set; } = new Position(0, 5);
        static public Position InnerPathEntry2 { get; set; } = new Position(5, 0);
        static public Position InnerPathEntry3 { get; set; } = new Position(5, 10);
        static public Position InnerPathEntry4 { get; set; } = new Position(10, 5);
                               

        public static List<Position> CreatePath()
        {
            //https://www.spelregler.org/wp-content/uploads/fia-med-knuff.png

            var path = new List<Position>()
            {

                Nest1, //Top right starting position
                new Position(1, 6),
                new Position(2, 6),
                new Position(3, 6),
                new Position(4, 6),

                new Position(4, 7),
                new Position(4, 8),
                new Position(4, 9),
                new Position(4, 10),
                InnerPathEntry3,

                Nest2,
                new Position(6, 9),
                new Position(6, 8),
                new Position(6, 7),

                new Position(6, 6),
                new Position(7, 6),
                new Position(8, 6),
                new Position(9, 6),
                new Position(10, 6),
                InnerPathEntry4,

                Nest3,
                new Position(9, 4),
                new Position(8, 4),
                new Position(7, 4),
                new Position(6, 4),

                new Position(6, 3),
                new Position(6, 2),
                new Position(6, 1),
                new Position(6, 0),
                InnerPathEntry2,

                Nest4,
                new Position(4, 1),
                new Position(4, 2),
                new Position(4, 3),

                new Position(4, 4),
                new Position(3, 4),
                new Position(2, 4),
                new Position(1, 4),
                new Position(0, 4),
                InnerPathEntry1
            };

            return path;
        }
    }
}
