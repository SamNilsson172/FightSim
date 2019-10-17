using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
	class Stage 
	{
		public string Name { get; } //level name
		public int A { get; private set; } = 0; //index of active enemy
		readonly Enemy[] enemies = new Enemy[0]; //array of enemies
		public Enemy Enemy => enemies[A]; //active enemy

		public Stage(string _name, Enemy[] _enemies)
		{
			Name = _name;
			enemies = _enemies;
		}

		public bool AllDead() //check if all enemies are dead
		{
			if (A < enemies.Length) //if index of active enemy is out of index
				if (!enemies[A].IsAlive()) //if enemy is dead update active enemy
					A++;
			return A >= enemies.Length; //all dead if index is out of index
		}
	}



}
