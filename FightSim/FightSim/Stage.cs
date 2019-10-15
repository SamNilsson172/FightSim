using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
    class Stage
    {
        public string Name { get; }
        public int a { get; private set; } = 0;  //Active unit
        Unit[] enemies = new Unit[0];
        public Unit Enemy => enemies[a];

        public Stage(string _name, Unit[] _enemies)
        {
            Name = _name;
            enemies = _enemies;
        }

        public bool AllDead()
        {
            if (!enemies[a].IsAlive())
                a++;
            return a >= enemies.Length;
        }
    }



}
