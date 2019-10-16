using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
	class Enemy : Unit
	{
		public Enemy(string _name, int _hp, Weapon _weapon, Armor _armor, int _xp, Item[] _items) : base(_name, _hp, _weapon, _armor, _xp)
		{
			xp = _xp;
			inventory.AddItem(_items);
		}

		public Item[] Loot()
		{
			List<Item> loot = new List<Item>();
			foreach (Item i in inventory.inventory)
			{
				int randNumb = generator.Next(0, 101);
				switch (i.Name)
				{
					case "Small potion":
						if (randNumb > 10)
							loot.Add(i);
						break;

					case "Medium potion":
						if (randNumb > 25)
							loot.Add(i);
						break;

					case "Big potion":
						if (randNumb > 50)
							loot.Add(i);
						break;

					case "Sword":
						if (randNumb > 80)
							loot.Add(i);
						break;

					case "Gun":
						if (randNumb > 95)
							loot.Add(i);
						break;

					case "Leather armor":
						if (randNumb > 80)
							loot.Add(i);
						break;

					case "Steel armor":
						if (randNumb > 95)
							loot.Add(i);
						break;
				}
			}
			return loot.ToArray();
		}
	}
}
