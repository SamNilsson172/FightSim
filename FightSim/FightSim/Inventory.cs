using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
    class Inventory
    {
        public List<Item> inventory { get; } = new List<Item>(); //list of all items in an inventory

        public void AddItem(Item[] items) //method that adds items to the inventory
        {
            foreach (Item i in items) //loop for all items that are requested to be added
            {
                if (CheckWeight(i.Weight) > 20) //if weight would be bigger than 20 it the item was added to it
                {
                    Console.WriteLine("Inventory is full!"); //dont add it and write out sum txt
                    break;
                }
                else
                {
                    inventory.Add(i); //add item and write out sum txt
                    Console.WriteLine("Added " + i.Name + " to inventory.");
                }
            }
        }

        public void CheckInventory(Weapon weapon, Armor armor) //method that lets you look at your items and lets you discard them, needs equiped gear to know not to discard them
        {
            List<string> inventoryNames = new List<string>(); //list of all names of all items in inventory

            foreach (Item i in inventory) //loop for all items in inventory
                inventoryNames.Add(i.Name); //add current name of item
            inventoryNames.Add("Back"); //add to list of names to be able to go back

            int inv = 0; //int for the index in name list
            while (inv != inventory.Count) //loop while you havnt pressed back
            {
                inv = Input.Selection(inventoryNames.ToArray(), "Pick an item " + "\r\n" + "Weight: " + CheckWeight(0) + "/20", 1); //lets you pick an item and returns the index in name list of the item
                Console.Clear();
                if (inv != inventory.Count) //if you didnt press back
                {
                    string[] itemOpt = { "Back", "Discard" }; //array of options for the current item
                    if (Input.Selection(itemOpt, inventory[inv].Stats(), 2) == 1) //if you discard the item also writes out stats of item
                        DiscardItem(inv, inventoryNames, weapon, armor); //removes the item from inventory
                    Console.Clear();
                }
            }
        }

        void DiscardItem(int index, List<string> nameList, Weapon weapon, Armor armor) //removes an item from inventory and a list, aslong as its not an equiped item
        {
            if (weapon == inventory[index] || armor == inventory[index]) //if item is equiped
            {
                Console.WriteLine("Item currently eqiped!"); //dont remove it
                Input.ClickToContinue();
            }
            else
            {
                inventory.RemoveAt(index); //remove it
                nameList.RemoveAt(index);
            }
        }

        int CheckWeight(int extraWeight) //gets the total weight of all items in inventory plus a potionsial item
        {
            int currentWeight = 0; //int for totWeight
            foreach (Item i in inventory) //loop for all items in inventory
                currentWeight += i.Weight; //adds weight of item to total weight
            return currentWeight + extraWeight; //returns total weight plus extra weight
        }

        public string[] AllItemsFromType(int whatType) //get an array of the names of all items of a specific type from inventory
        {
            List<string> items = new List<string>(); //list of the names of all items of a specific type
            foreach (Item i in inventory) //loop for all items in inventory
                if (i.Type == whatType) //if items is of requested type
                    items.Add(i.Name); //add it to the list
            if (whatType == 2) //if it's a potion, add back to list
                items.Add("Back");
            return items.ToArray(); //return names as an array
        }

        public Item GetItemFromName(string name) //find an item from its name
        {
            foreach (Item i in inventory) //loop for all items in inventory
                if (i.Name == name) //if items name is same as the one requested
                    return i; //return it
            return null; //if inventory dosnt contain requested item
        }

        public int UsePotion() //method for using a potion that returns amount potion heals
        {
            int healAmount = 0; //amount potion heals
            string[] potionList = AllItemsFromType(2); //get list of all potions
            int potionToUse = Input.Selection(potionList, "Which potion would you like to use?", 1); //let you choose a potion
            if (potionToUse != potionList.Length - 1) //if you didnt select back
            {
                Potion pot = (Potion)GetItemFromName(potionList[potionToUse]); //find the potion in inventory
                healAmount = pot.Heal; //get potions heal value 
                inventory.Remove(pot); //remove the potion from inventory
            }
            Console.Clear();

            return healAmount; //return the amount to heal
        }
    }
}

