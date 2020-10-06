using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Rocket_Game.Model
{
    public abstract class Subject
    {
        private List<Observer> myObs = new List<Observer>();

        public void attach(Observer o)
        {
            this.myObs.Add(o);
        }

        public void detach(Observer o)
        {
            this.myObs.Remove(o);
        }

        public void notify()
        {
            foreach (Observer o in this.myObs)
            {
                o.obsUpdate(this);
            }
        }
    }
}
