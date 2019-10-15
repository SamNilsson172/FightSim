using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
    class Potion : Item
    {
        public int Heal { get; }

        public Potion(string _name, int _weight, int _heal) : base(_name, _weight)
        {
            Heal = _heal;
            typeText = "Heals: " + Heal + "\r\n" + "Type: Armor";
            type = (int)Types.Armor;
        }
    }
}
