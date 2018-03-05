using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_17
{
    class Program
    {
        static void Main(string[] args)
        {
            Spinlock spinlock = new Spinlock();
            //spinlock.MakeOperations1();
            spinlock.MakeOperations2();

            //List<int> CircularBuffer = new List<int>();
            //CircularBuffer.Add(5);
            //CircularBuffer.Add(5);
            //CircularBuffer.Add(5);
            //CircularBuffer.Add(5);
            //CircularBuffer.Add(5);
            //CircularBuffer.Insert(0, 1);

            //foreach (var item in CircularBuffer)
            //{
            //    Console.WriteLine(item);
            //}
        }
    }
}
