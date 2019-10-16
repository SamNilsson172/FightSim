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
		readonly Enemy[] enemies = new Enemy[0];
		public Enemy Enemy => enemies[A];

		public Stage(string _name, Enemy[] _enemies)
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
