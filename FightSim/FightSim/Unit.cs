using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
    class Unit
    {
        int maxHp;
        int maxDmg = 0;
        float hp;
        public string name;
        static Random generator = new Random();

        public Unit(string _name, int _hp, int _dmg)
        {
            name = _name;
            maxHp = _hp;
            hp = maxHp;
            maxDmg = _dmg;
        }

        public int Atk()
        {
            return generator.Next(0, maxDmg + 1);
        }

        public void Hurt(int dmg)
        {
            hp -= dmg;

            if (hp < 0)
                hp = 0;
            if (hp > maxHp)
                hp = maxHp;
        }

        public bool isAlive()
        {
            return hp > 0;
        }

        public void PrintStats()
        {
            Console.WriteLine(name + ": " + hp);
        }

    }
}
