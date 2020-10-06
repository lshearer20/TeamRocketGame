using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Model.Entities;

namespace Team_Rocket_Game.Controller.Collision
{
    //We really struggled with collision
    //Referenced an old project for collision help
    public class CollisionRegion
    {
        private HashSet<Component> components;

        public CollisionRegion()
        {
            components = new HashSet<Component>();
        }

        public HashSet<Component> GetComponents()
        {
            return this.components;
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        public Boolean RemoveComponent(Component component)
        {
            return components.Remove(component);
        }
    }
}