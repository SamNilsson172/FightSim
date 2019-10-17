using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
    class Enemy : Unit //enemy is a unit, basically just for a loot drop table
    {
        public Enemy(string _name, int _hp, Weapon _weapon, Armor _armor, int _xp, Item[] _items) : base(_name, _hp, _weapon, _armor, _xp)
        {
            inventory.AddItem(_items); //loot that the enmy can drop
        }

        public Item[] Loot()
        {
            List<Item> loot = new List<Item>(); //list of items that will be given to the player
            foreach (Item i in inventory.inventory) //loop for all items the enemy has
            {
                int randNumb = generator.Next(0, 101);
                switch (i.Name) //run code depending on items name
                {
                    case "Small potion": //if its a small potion
                        if (randNumb > 10) //90% chance to get the item
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
            return loot.ToArray(); //make an array to return it
        }
    }
}
