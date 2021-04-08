using GameEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Objects
{
    class DoorwayToInnerPath : IBoardObject
    {
        public char CharToDraw => 'o';
        public int PlayerId { get; set; }

        public DoorwayToInnerPath(int playerID)
        {
            PlayerId = playerID;
        }
    }
}
