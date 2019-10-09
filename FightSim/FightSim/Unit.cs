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
        int hp;
        readonly string name;
        Weapon myWeapon;
        static Random generator = new Random();


        public int Hp
        {
            get
            {
                return hp;
            }
            set
            {
                if (value < 0)
                    value = 0;
                if (value > maxHp)
                    value = maxHp;
                hp = value;
            }
        }

        public string Name => name;

        public Unit(string _name, int _hp, int _dmg, Weapon _weapon)
        {
            name = _name;
            maxHp = _hp;
            hp = maxHp;
            maxDmg = _dmg;
            myWeapon = _weapon;
        }

        public int Atk()
        {
            return generator.Next(maxDmg / 2, maxDmg + 1);
        }

        public void Hurt(int dmg)
        {
            Hp -= dmg;
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
