using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
    class Unit
    {
        public string Name { get; }
        public Inventory inventory = new Inventory(); //items the unit has
        readonly int maxHp; //if hp gets to big, cant be changed
        public Weapon WeapSlot { get; private set; } //weapon unit has, dictates dmg
        public Armor ArmoSlot { get; private set; } //armor unit has, dictates how much dmg is dealt
        public static Random generator = new Random(); //get random number
        public int xp; //amount of xp, dictates level
        public int Level => (int)Math.Sqrt(xp); //level is the squre root of xp

        int hp; //otherwise stackoverflow
        int Hp
        {
            get => hp;
            set //make sure value dosnt break rules
            {
                if (value < 0)
                    value = 0;
                if (value > maxHp)
                    value = maxHp;
                hp = value;
            }
        }


        public Unit(string _name, int _hp, Weapon _weapon, Armor _armor, int _xp)
        { //assing given values
            Name = _name;
            maxHp = _hp;
            Hp = maxHp;
            WeapSlot = _weapon;
            ArmoSlot = _armor;
            Item[] startGear = { _weapon, _armor };
            inventory.AddItem(startGear); //add the armor and wepon to inventory
            xp = _xp;
        }

        public int Atk() //how much dmg unit does
        {
            return generator.Next(WeapSlot.Dmg / 2, WeapSlot.Dmg + 1) + Level; //returns random value between equiped weapons dmg and half of it, plus the level
        }

        public void Hurt(int dmg) //deals dmg
        {
            Hp -= dmg / ArmoSlot.Def; //devides dmg by equiped armors defense
        }

        public void Heal(int heal) //heals
        {
            Hp += heal;
        }

        public void HealToFull() //sets health to max hp and writes sum txt
        {
            Hp = maxHp;
            Console.WriteLine(Name + " healed to full hp!");
            Input.ClickToContinue();
        }

        public bool IsAlive() //if unity is alive
        {
            return Hp > 0; //returns true if hp is bigger than 0
        }

        public string Stats() //gets string of all units stats
        {
            return Name + ": " + Hp + "/" + maxHp + " hp" + "\r\n"
                + "Level: " + Level + "\r\n"
                + "Damage: " + ((WeapSlot.Dmg / 2) + Level) + "-" + (WeapSlot.Dmg + Level) + "   ~ " + WeapSlot.Name + " equiped" + "\r\n"
                + "Defense: " + ArmoSlot.Def + "   ~ " + ArmoSlot.Name + " equiped";
        }
        public string BattleStats()
        { //just the important stats for battle 
            return Name + ": " + Hp + "/" + maxHp + " hp" + " ~ lvl: " + Level;
        }

        public void ChangeEquipment() //method that changes equiped weapon or armor
        {
            int itemLists = 0; //what type of items you want a list of
            while (itemLists != 2) //loops as long as you havnt choosen to go back
            {
                string[] itemTypes = { "Weapon", "Armor", "Back" }; 
                itemLists = Input.Selection(itemTypes, "Which would you like to change?", 1); //lets you choose what equipment you want to change
                Console.Clear();

                switch (itemLists)
                {
                    case 0: //if weapon
                        string[] weaponNames = inventory.AllItemsFromType(itemLists); //get a list of af the names of all weapons in units inventory
                        AddTextToEquiped(WeapSlot.Name, weaponNames); //show which weapon that is currently equiped
                        int choosenWeaponIndex = Input.Selection(weaponNames, "Which one would you like to equip?", 1); //let you choose what weapon you want to equip
                        RemoveTextFromEquiped(WeapSlot.Name, weaponNames); //so you can press currently equiped weapon whithout weapSlot setting to null
                        WeapSlot = (Weapon)inventory.GetItemFromName(weaponNames[choosenWeaponIndex]); //set weapoSlot to choosen weapon 
                        break;

                    case 1: //if armor, same as above
                        string[] armorNames = inventory.AllItemsFromType(itemLists);
                        AddTextToEquiped(ArmoSlot.Name, armorNames);
                        int choosenArmorIndex = Input.Selection(armorNames, "Which one would you like to equip?", 1);
                        RemoveTextFromEquiped(ArmoSlot.Name, armorNames);
                        ArmoSlot = (Armor)inventory.GetItemFromName(armorNames[choosenArmorIndex]);
                        break;

                    case 2: //if back
                        break;
                }
                Console.Clear();
            }
        }

        void AddTextToEquiped(string currentlyEquiped, string[] items) //adds text to equiped item to a given list
        {
            for (int i = 0; i < items.Length; i++)
                if (items[i] == currentlyEquiped)
                    items[i] += " ~ Equiped";
        }

        void RemoveTextFromEquiped(string currentlyEquiped, string[] items) //removes the equiped txt
        {
            for (int i = 0; i < items.Length; i++)
                if (items[i].Contains(" ~ Equiped"))
                    items[i] = currentlyEquiped;
        }
    }
}

