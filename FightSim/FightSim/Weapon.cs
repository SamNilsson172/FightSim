using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
    class Weapon : Item
    {
        int dmg;

        public Weapon(string _name, int _weight, int _dmg)
            : base(_name, _weight)
        {
            dmg = _dmg;
        }

        public void PrintStats()
        {
            Console.WriteLine(base.Name);
            Console.WriteLine("Weight: " + base.Weight);
            Console.WriteLine("Damage: " + dmg);
        }

        public int Dmg => dmg;
    }
}
