using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
    class Weapon : Item
    {
        public int Dmg { get; }

        public Weapon(string _name, int _weight, int _dmg) : base(_name, _weight)
        {
            Dmg = _dmg;
            typeText = "Damage: " + Dmg + "\r\n" + "Type: Weapon";
            type = (int)Types.Weapon;
        }
    }
}
