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
        public int A { get; private set; } = 0;  //Active unit
        readonly Unit[] enemies = new Unit[0];
        public Unit Enemy => enemies[A];

        public Stage(string _name, Unit[] _enemies)
        {
            Name = _name;
            enemies = _enemies;
        }

        public bool AllDead()
        {
            if (A < enemies.Length)
                if (!enemies[A].IsAlive())
                    A++;
            return A >= enemies.Length;
        }
    }



}
