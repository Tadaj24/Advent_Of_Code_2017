using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_19
{
    class Program
    {
        static void Main(string[] args)
        {
            Tubes tubes = new Tubes();
            tubes.MakeOperations1();

            Console.WriteLine(Convert.ToInt16('a'));
            Console.WriteLine(Convert.ToInt16('z'));
            Console.WriteLine(Convert.ToInt16('A'));
            Console.WriteLine(Convert.ToInt16('Z'));
        }
    }
}
