using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Rocket_Game.Model
{
    public interface Observer
    {
        // funtion that updates the game data
        // when the subject has changed or 
        // notified. 
        void obsUpdate(Subject mySubject);
    }
}
