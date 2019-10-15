using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Unit Slime()
            {
                return new Unit("Slime", 20, Fist(), Naked());
            }

            Stage One()
            {
                Unit[] enemies = { Slime(), Slime() };
                return new Stage("One", enemies);
            }

            Armor Naked()
            {
                return new Armor("Naked", 0, 1);
            }

            Armor Leather()
            {
                return new Armor("Leather armor", 2, 2);
            }

            Armor Steel()
            {
                return new Armor("Steel armor", 8, 5);
            }

            Weapon Fist()
            {
                return new Weapon("Fist", 0, 10);
            }

            Weapon Sword()
            {
                return new Weapon("Sword", 3, 30);
            }

            Weapon Gun()
            {
                return new Weapon("Gun", 2, 70);
            }
            Unit player = new Unit(Input.String(2, 16, false, "Name your player."), 100, Fist(), Naked());
            Item[] testStuff = { Steel(), Gun() };
            player.inventory.AddItem(testStuff);

            string[] selection = { "Fight", "Inventory", "Check stats", "Change Equipment" };

            while (player.IsAlive())
            {
                int selected = Input.Selection(selection, "What would you like to do, " + player.Name + "?", 2);
                Console.Clear();

                switch (selected)
                {
                    case 0:
                        Fight();
                        break;

                    case 1:
                        player.inventory.CheckInventory();
                        break;

                    case 2:
                        Console.WriteLine(player.Stats());
                        Input.ClickToContinue();
                        break;

                    case 3:
                        player.ChangeEquipment();
                        break;
                }
            }

            void Fight()
            {
                Stage stage = new Stage(null, null);
                string[] stages = { "One" };
                int stageSelected = Input.Selection(stages, "Pick a level", 1);
                Console.Clear();
                stageSelected++;
                switch (stageSelected)
                {
                    case 1:
                        stage = One();
                        break;
                }
                Console.Clear(); //to not print added items from enemies
                string[] fightOpt = { "Attack", "Heal", "Check Opponent", "Run" };
                bool run = false;
                while (player.IsAlive() && !stage.AllDead() && !run)
                {
                    int fightChoise = Input.Selection(fightOpt, FightStats(stage.Enemy, null, null), 2);
                    Console.Clear();
                    switch (fightChoise)
                    {
                        case 0:
                            int pDmg = player.Atk();
                            int eDmg = stage.Enemy.Atk();
                            stage.Enemy.Hurt(pDmg);
                            player.Hurt(eDmg);
                            Console.WriteLine(FightStats(stage.Enemy, pDmg, eDmg));
                            Input.ClickToContinue();
                            break;
                        case 1:
                            break;

                        case 2:
                            Console.WriteLine(stage.Enemy.Stats());
                            Input.ClickToContinue();
                            break;

                        case 3:
                            run = true;
                            Console.WriteLine(player.Name + " ran away.");
                            Input.ClickToContinue();
                            break;
                    }
                }
            }

            string FightStats(Unit enemy, int? pDmg, int? eDmg)
            {
                string text = "";
                text += player.BattleStats();
                text += "\r\n";
                text += enemy.BattleStats();
                text += "\r\n";
                if (pDmg != null)
                    text += player.Name + " attacked for " + pDmg + " dmg!" + "\r\n";
                if (eDmg != null)
                    text += enemy.Name + " attacked for " + eDmg + " dmg!" + "\r\n";
                return text;
            }
        }
    }
}
