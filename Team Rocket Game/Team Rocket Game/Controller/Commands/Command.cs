using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Rocket_Game.Controller.Commands
{
    public interface Command
    {
        // Execute the command
        void execute();
    }
}
