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
            Unit A = new Unit(Input.String(1, 16, false, "Name the first fighter"), 100, 30);
            Unit B = new Unit(Input.String(1, 16, false, "Name the second fighter"), 100, 30);
            bool turn = true;
            while (A.isAlive() && B.isAlive())
            {
                A.PrintStats();
                B.PrintStats();
                Console.WriteLine("Press button to atk");
                Console.ReadKey();
                Console.Clear();

                if (turn)
                    Atk(A, B);
                else
                    Atk(B, A);

                turn = !turn;
            }
            if (A.isAlive())
                Console.WriteLine(A.name + " won");
            else
                Console.WriteLine(B.name + " won");
            Console.ReadKey();

            void Atk(Unit atker, Unit defer)
            {
                int atk = atker.Atk();
                defer.Hurt(atk);
                atker.PrintStats();
                defer.PrintStats();
                Console.WriteLine(atker.name + " did " + atk + " dmg!");
                Console.WriteLine("Press button to continue");
                Console.ReadKey();
                Console.Clear();
            }
        }


    }
}
