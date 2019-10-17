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
			Enemy Slime() //predefine all wanted instances in methods
			{
				Item[] loot = { Small() };
				return new Enemy("Slime", 20, Fist(), Naked(), 25, loot);
			}

			Enemy Troll()
			{
				Item[] loot = { Medium() };
				return new Enemy("Troll", 40, Sword(), Leather(), 100, loot);
			}

			Enemy Human()
			{
				Item[] loot = { Big() };
				return new Enemy("Human", 100, Gun(), Steel(), 625, loot);
			}

			Stage One()
			{
				Enemy[] enemies = { Slime(), Slime(), Slime() };
				return new Stage("One", enemies);
			}

			Stage Two()
			{
				Enemy[] enemies = { Troll(), Troll() };
				return new Stage("Two", enemies);
			}

			Stage Three()
			{
				Enemy[] enemies = { Human() };
				return new Stage("Three", enemies);
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

			Potion Small()
			{
				return new Potion("Small potion", 1, 10);
			}

			Potion Medium()
			{
				return new Potion("Medium potion", 2, 20);
			}

			Potion Big()
			{
				return new Potion("Big potion", 3, 30);
			}

			bool play = true; //while you still want to play, can be changed upton death
			while (play) //loop as long as you want to play
			{
				bool[] levelLocks = { false, true, true }; //bool array for what levels are locked, only the first one is avalible in the start
				string playerName = Input.String(2, 16, false, "Name your player."); //lets player choose a name
				int xp = 0;
				if (playerName == "Sam Nilsson") //cheat code
					xp = 99999999;
				Unit player = new Unit(playerName, 100, Fist(), Naked(), xp); //create player

				string[] selection = { "Fight", "Inventory", "Check stats", "Change Equipment", "Heal" }; //all the things you can do in the game

				while (player.IsAlive()) //loop as long as the player lives
				{
					int selected = Input.Selection(selection, "What would you like to do, " + player.Name + "?", 2); //lets you choose what to do
					Console.Clear();

					switch (selected)
					{
						case 0: //if fight
							Fight();
							break;

						case 1: //if check inventory
							player.inventory.CheckInventory(player.WeapSlot, player.ArmoSlot); //checks the inventory
							break;

						case 2: //if check stats
							Console.WriteLine(player.Stats()); //write players stats
							Input.ClickToContinue();
							break;

						case 3: //if change equipment
							player.ChangeEquipment(); //changes the equipment
							break;

						case 4: //if heal
							Console.Clear();
							player.HealToFull(); //heals the player
							break;
					}
				}

				void Fight() //method for fighting
				{
					Stage stage = new Stage(null, null);
					List<string> stages = new List<string>();
					for (int i = 0; i < levelLocks.Length; i++) //adds all unlocked stages to a list
					{
						if (!levelLocks[i])
							stages.Add((i + 1).ToString());
					}

					int stageSelected = Input.Selection(stages.ToArray(), "Pick a level", 1); //lets you choose one
					Console.Clear();
					stageSelected++; //increase to mach name of stages 
					switch (stageSelected)
					{ //create the stage you choose
						case 1:
							stage = One();
							break;

						case 2:
							stage = Two();
							break;

						case 3:
							stage = Three();
							break;
					}
					Console.Clear(); //to not print added items from enemies
					string[] fightOpt = { "Attack", "Heal", "Check Opponent", "Run" }; //what you can do in a fight
					bool run = false; //set to true to break battle loop
					while (player.IsAlive() && !stage.AllDead() && !run) //loops while player is alive, all enemies are not dead and you havnt run
					{
						int fightChoise = Input.Selection(fightOpt, FightStats(stage.Enemy, null, null), 2); //lets you choose what to do in the fight
						Console.Clear();
						switch (fightChoise)
						{
							case 0: //if attack
								int pDmg = player.Atk(); //get players dmg
								int eDmg = stage.Enemy.Atk(); //get enemys dmg
								stage.Enemy.Hurt(pDmg); //damage enemy
								player.Hurt(eDmg); //damge player
								Console.WriteLine(FightStats(stage.Enemy, pDmg, eDmg)); //write text for looks
								Input.ClickToContinue();
								break;

							case 1: //if heal
								int healed = player.inventory.UsePotion(); //gets the amount to heal from used potion
								player.Heal(healed); //heals
								Console.Clear();
								Console.WriteLine(FightStats(stage.Enemy, null, null));
								Console.WriteLine(player.Name + " healed for " + healed + " hp!"); //writes out txt from looks
								Input.ClickToContinue();
								break;

							case 2: //if check opponent
								Console.WriteLine(stage.Enemy.Stats()); //show enemy stats
								Input.ClickToContinue();
								break;

							case 3: //if run
								run = true; //set run to true to break loop
								Console.WriteLine(player.Name + " ran away.");
								Input.ClickToContinue();
								break;
						}

						if (!stage.Enemy.IsAlive()) //if an enemy died
						{
							int preLvl = player.Level; //save current level
							player.xp += stage.Enemy.xp / 4; //add 1/4 of the enemies xp to the players
							int newLvl = player.Level; //check new level
							Console.WriteLine(player.Name + " gained " + stage.Enemy.xp / 4 + " xp!"); //write xp gain
							if (preLvl != newLvl) //if leveld up write it
								Console.WriteLine(player.Name + " leveled up to lvl " + newLvl + "!");
							player.inventory.AddItem(stage.Enemy.Loot()); //add enemies loot to player
							Input.ClickToContinue();
						}
					}

					if (stage.AllDead()) //if all enemies died
					{
						Console.WriteLine(player.Name + " won!");
						if (stageSelected != levelLocks.Length && levelLocks[stageSelected]) //unlock new level if one above it exists and is not already unlocked
						{
							Console.WriteLine("Level " + (stageSelected + 1) + " was unlocked!");
							levelLocks[stageSelected] = false;
						}
						Input.ClickToContinue();
					}

					if (!player.IsAlive()) //if you died, let you quit or try again
					{
						string[] extOpt = { "Yes", "No" };
						int awnser = 0;
						awnser = Input.Selection(extOpt, player.Name + " died, restart?", 2);
						if (awnser == 1)
							play = false;
						Console.Clear();
					}
				}

				string FightStats(Unit enemy, int? pDmg, int? eDmg) //sum txt
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
}
