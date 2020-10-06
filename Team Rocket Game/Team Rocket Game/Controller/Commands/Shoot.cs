using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Rocket_Game.Controller.Commands
{
    public abstract class Shoot : Command
    {
        // execute
        public abstract void execute();
    }
}
