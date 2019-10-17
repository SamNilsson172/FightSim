using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
    class Item
    {
        public string Name { get; } //name can only be given, is det in constructor
        public int Weight { get; } //weight of items, needed to limit count in inventory

        protected enum Types { Weapon, Armor, Potion } //all types of items
        protected int type; //the type of the item, can be set in subclasses
        public int Type => type; //can get type from anywhere
        protected string typeText; //text for a specific type

        public Item(string _name, int _weight) //set item values from constructor
        {
            Weight = _weight;
            Name = _name;
        }

        public string Stats() //string of stats 
        {
            return Name + "\r\n" + "Weight: " + Weight + "\r\n" + typeText;
        }
    }
}
