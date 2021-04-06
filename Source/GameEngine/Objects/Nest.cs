using GameEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Nest : IBoardObject
    {
        public int PlayerId { get; set; }
        public char CharToDraw => '@';

        //Colorful.Console(nest.ToString(), Players[nest.PlayerId].Color);

        public override string ToString() => CharToDraw.ToString();

        public Nest(int playerID)
        {
            PlayerId = playerID;
        }
        
    }
}
