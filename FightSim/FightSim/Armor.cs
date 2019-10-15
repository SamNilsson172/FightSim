using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
    class Armor : Item
    {
        public int Def { get; }

        public Armor(string _name, int _weight, int _def) : base(_name, _weight)
        {
            Def = _def;
            typeText = "Defense: " + Def + "\r\n" + "Type: Armor";
            type = (int)Types.Armor;
        }
    }
}
