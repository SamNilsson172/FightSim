using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
    class Inventory
    {
        readonly List<Item> inventory = new List<Item>();

        public void AddItem(Item[] items)
        {
            foreach (Item i in items)
            {
                if (CheckWeight(i.Weight) > 20)
                {
                    Console.WriteLine("Inventory is full!");
                    break;
                }
                else
                {
                    inventory.Add(i);
                    Console.WriteLine("Added " + i.Name + " to inventory.");
                }
            }
        }

        public void CheckInventory()
        {
            List<string> inventoryNames = new List<string>();
            for (int i = 0; i < inventory.Count; i++)
                inventoryNames.Add(inventory[i].Name);
            inventoryNames.Add("Back");

            int inv = 0;
            while (inv != inventory.Count)
            {
                inv = Input.Selection(inventoryNames.ToArray(), "Pick an item " + "\r\n" + "Weight: " + CheckWeight(0) + "/20", 1);
                Console.Clear();
                if (inv != inventory.Count)
                {
                    string[] itemOpt = { "Back", "Discard" };
                    if (Input.Selection(itemOpt, inventory[inv].Stats(), 2) == 1 && inventory[inv].Weight > 0)
                    {
                        DiscardItem(inv, inventoryNames, null);
                        inv = 0;
                    }
                    Console.Clear();
                }
            }
        }

        void DiscardItem(int index, List<string> nameList, Item eqiped)
        {
            if (eqiped == inventory[index]) //wip, can't delete eqipued items
                Console.WriteLine("Item currently eqiped!");
            else
            {
                inventory.RemoveAt(index);
                nameList.RemoveAt(index);
            }
        }

        int CheckWeight(int extraWeight)
        {
            int currentWeight = 0;
            for (int x = 0; x < inventory.Count; x++)
                currentWeight += inventory[x].Weight;
            return currentWeight + extraWeight;
        }

        public string[] AllItemsFromType(int whatType)
        {
            List<string> items = new List<string>();
            foreach (Item i in inventory)
                if (i.Type == whatType)
                    items.Add(i.Name);
            return items.ToArray();
        }

        public Item GetItemFromName(string name)
        {
            foreach (Item i in inventory)
            {
                if (i.Name == name)
                {
                    return i;
                }
            }
            return null;
        }
    }
}

