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
        public Inventory inventory = new Inventory();
        int maxHp;
        Weapon weapSlot;
        Armor armoSlot;
        static Random generator = new Random();

        int hp; //otherwise stackoverflow
        int Hp
        {
            get => hp;
            set
            {
                if (value < 0)
                    value = 0;
                if (value > maxHp)
                    value = maxHp;
                hp = value;
            }
        }


        public Unit(string _name, int _hp, Weapon _weapon, Armor _armor)
        {
            Name = _name;
            maxHp = _hp;
            Hp = maxHp;
            weapSlot = _weapon;
            armoSlot = _armor;
            Item[] startGear = { _weapon, _armor };
            inventory.AddItem(startGear);
        }

        public int Atk()
        {
            return generator.Next(weapSlot.Dmg / 2, weapSlot.Dmg + 1);
        }

        public void Hurt(int dmg)
        {
            Hp -= dmg / armoSlot.Def;
        }

        public bool IsAlive()
        {
            return Hp > 0;
        }

        public string Stats()
        {
            return Name + ": " + Hp + "/" + maxHp + " hp" + "\r\n"
                + "Damage: " + weapSlot.Dmg / 2 + "-" + weapSlot.Dmg + "   ~ " + weapSlot.Name + " equiped" + "\r\n"
                + "Defense: " + armoSlot.Def + "   ~ " + armoSlot.Name + " equiped";
        }
        public string BattleStats()
        {
            return Name + ": " + Hp + "/" + maxHp + " hp";
        }

        public void ChangeEquipment()
        {
            int itemLists = 0;
            while (itemLists != 2)
            {
                string[] itemTypes = { "Weapon", "Armor", "Back" };
                itemLists = Input.Selection(itemTypes, "Which would you like to change?", 1);
                Console.Clear();


                switch (itemLists)
                {
                    case 0:
                        string[] weaponNames = inventory.AllItemsFromType(itemLists);
                        AddTextToEquiped(weapSlot.Name, weaponNames);
                        int choosenWeaponIndex = Input.Selection(weaponNames, "Which one would you like to equip?", 1);
                        RemoveTextFromEquiped(weapSlot.Name, weaponNames);
                        weapSlot = (Weapon)inventory.GetItemFromName(weaponNames[choosenWeaponIndex]);
                        break;

                    case 1:
                        string[] armorNames = inventory.AllItemsFromType(itemLists);
                        AddTextToEquiped(armoSlot.Name, armorNames);
                        int choosenArmorIndex = Input.Selection(armorNames, "Which one would you like to equip?", 1);
                        RemoveTextFromEquiped(armoSlot.Name, armorNames);
                        armoSlot = (Armor)inventory.GetItemFromName(armorNames[choosenArmorIndex]);
                        break;

                    case 2:
                        break;
                }
                Console.Clear();
            }
        }

        void AddTextToEquiped(string currentlyEquiped, string[] items)
        {
            for (int i = 0; i < items.Length; i++)
                if (items[i] == currentlyEquiped)
                    items[i] += " ~ Equiped";
        }

        void RemoveTextFromEquiped(string currentlyEquiped, string[] items)
        {
            for (int i = 0; i < items.Length; i++)
                if (items[i].Contains(" ~ Equiped"))
                    items[i] = currentlyEquiped;
        }
    }
}

