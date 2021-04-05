using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class Nest
    {
        public int PlayerId { get; set; }
        public char CharToDraw { get; set; }

        //Colorful.Console(nest.ToString(), Players[nest.PlayerId].Color);

        public override string ToString() => CharToDraw.ToString();
        
    }
}
