using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
    class Armor : Item //armor is an item
    {
        public int Def { get; } //int that damg is devided by

        public Armor(string _name, int _weight, int _def) : base(_name, _weight) //constructor for armor
        { //assign given values
            Def = _def;
            typeText = "Defense: " + Def + "\r\n" + "Type: Armor";
            type = (int)Types.Armor;
        }
    }
}
