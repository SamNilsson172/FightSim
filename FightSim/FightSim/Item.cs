using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
    class Item
    {
        string name;
        int weight;

        public Item(string _name, int _weight)
        {
            weight = _weight;
            name = _name;
        }

        public string Name => name;
        public int Weight => weight;
    }
}
