using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
    class Item
    {
        public string Name { get; }
        public int Weight { get; }

        protected enum Types { Weapon, Armor, Potion }
        protected int type;
        public int Type => type;
        protected string typeText;

        public Item(string _name, int _weight)
        {
            Weight = _weight;
            Name = _name;
        }

        public string Stats()
        {
            return Name + "\r\n" + "Weight: " + Weight + "\r\n" + typeText;
        }
    }
}
