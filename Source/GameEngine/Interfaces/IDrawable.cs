using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Objects;

namespace GameEngine.Interfaces
{
    interface IDrawable
    {
        string StatusMessage { get; set; }
        string ActionMessage { get; set; }

        void Update();
    }
}
