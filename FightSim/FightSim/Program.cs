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
            Weapon fist = new Weapon("Fist", 0, 1);

            Item[] inventory = new Item[500]; //cant make a list, so i use a big array

            Unit A = new Unit(Input.String(1, 16, false, "Name the first fighter"), 100, 30, new Weapon(fist.Name, fist.Weight, fist.Dmg));
            Unit B = new Unit(Input.String(1, 16, false, "Name the second fighter"), 100, 30, new Weapon(fist.Name, fist.Weight, fist.Dmg));
            bool turn = true;
            while (A.isAlive() && B.isAlive())
            {
                A.PrintStats();
                B.PrintStats();
                Console.WriteLine("Press button to atk");
                Console.ReadKey();
                Console.Clear();

                if (turn)
                    Atk(A, B, turn);
                else
                    Atk(B, A, turn);

                turn = !turn;
            }
            if (A.isAlive())
                Console.WriteLine(A.Name + " won");
            else
                Console.WriteLine(B.Name + " won");
            Console.ReadKey();

            void Atk(Unit atker, Unit defer, bool trn)
            {
                int atk = atker.Atk();
                defer.Hurt(atk);
                if (turn)
                {
                    atker.PrintStats();
                    defer.PrintStats();
                }
                else
                {
                    defer.PrintStats();
                    atker.PrintStats();
                }
                Console.WriteLine(atker.Name + " did " + atk + " dmg!");
                Console.WriteLine("Press button to continue");
                Console.ReadKey();
                Console.Clear();
            }
        }


    }
}
