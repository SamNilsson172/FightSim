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
			Enemy Slime()
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

			bool play = true;
			while (play)
			{


				bool[] levelLocks = { false, true, true };
				string playerName = Input.String(2, 16, false, "Name your player.");
				int xp = 0;
				if (playerName == "Sam Nilsson")
					xp = 99999999;
				Unit player = new Unit(playerName, 100, Fist(), Naked(), xp);

				string[] selection = { "Fight", "Inventory", "Check stats", "Change Equipment", "Heal" };

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
							player.inventory.CheckInventory(player.weapSlot, player.armoSlot);
							break;

						case 2:
							Console.WriteLine(player.Stats());
							Input.ClickToContinue();
							break;

						case 3:
							player.ChangeEquipment();
							break;

						case 4:
							Console.Clear();
							player.HealToFull();
							break;
					}
				}

				void Fight()
				{
					Stage stage = new Stage(null, null);
					List<string> stages = new List<string>();
					for (int i = 0; i < levelLocks.Length; i++)
					{
						if (!levelLocks[i])
							stages.Add((i + 1).ToString());
					}

					int stageSelected = Input.Selection(stages.ToArray(), "Pick a level", 1);
					Console.Clear();
					stageSelected++;
					switch (stageSelected)
					{
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
								int healed = player.inventory.UsePotion();
								player.Heal(healed);
								Console.Clear();
								Console.WriteLine(FightStats(stage.Enemy, null, null));
								Console.WriteLine(player.Name + " healed for " + healed + " hp!");
								Input.ClickToContinue();
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

						if (!stage.Enemy.IsAlive())
						{
							int preLvl = player.Level;
							player.xp += stage.Enemy.xp / 4;
							int newLvl = player.Level;
							Console.WriteLine(player.Name + " gained " + stage.Enemy.xp / 4 + " xp!");
							if (preLvl != newLvl)
								Console.WriteLine(player.Name + " leveled up to lvl " + newLvl + "!");
							player.inventory.AddItem(stage.Enemy.Loot());
							Input.ClickToContinue();
						}
					}

					if (stage.AllDead())
					{
						Console.WriteLine(player.Name + " won!");
						if (stageSelected != levelLocks.Length && levelLocks[stageSelected])
						{
							Console.WriteLine("Level " + (stageSelected + 1) + " was unlocked!");
							levelLocks[stageSelected] = false;
						}
						Input.ClickToContinue();
					}

					if (!player.IsAlive())
					{
						string[] extOpt = { "Yes", "No" };
						int awnser = 0;
						awnser = Input.Selection(extOpt, player.Name + " died, restart?", 2);
						if (awnser == 1)
							play = false;
						Console.Clear();
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
}
