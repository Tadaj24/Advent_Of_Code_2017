using System;
using System.Collections.Generic;
using System.Linq;

namespace day_17
{
    class Spinlock
    {
        public int Input=335;
        public List<int> CircularBuffer = new List<int>();
        public int ActualPosition=0;        

        public void MakeOperations1()
        {
            CircularBuffer.Add(0);
            //CircularBuffer.Add(1);
            ActualPosition = 0;

            for (int i = 1; i < 2018; i++)
            {
                InjectNumber(i);               
            }

            foreach (var item in CircularBuffer)
            {
                if (item==0)
                {
                    Console.WriteLine("TUUUUUUUuuuuuuuuuuuuuuuuuuuuuuuuuuu");
                    Console.ReadKey();
                }

                Console.WriteLine(item);
            }

            //Console.WriteLine(CircularBuffer[ActualPosition-1] + " " + CircularBuffer[ActualPosition]);
        }

        public void MakeOperations2()
        {
            CircularBuffer.Clear();
            CircularBuffer.Add(0);
            ActualPosition = 0;

            int index=0;
            int Value=0;
            for (int i = 1; i < 50000000; i++)
            {
                index = ActualPosition + Input;
                if (index >= i)
                {
                    index = index % i;
                }

                //Console.WriteLine(index + " " + Length);

                
                if(index==0)
                {
                    Value = i;
                }

                ActualPosition = index + 1;
            }
            Console.WriteLine("Liczba wystepujaca po 0: " + Value);

            //Console.WriteLine();
        }

        public void InjectNumber(int numberToInject)
        {
            int Length = CircularBuffer.Count(); //dlugosc aktualnego bloku
            int index = ActualPosition + Input; //znajduje miejsce gdzie  hragan sie zatrzyma

            if (index>=Length)
            {
                index = index % Length;
            }

            //Console.WriteLine(index + " " + Length);

            if (index==Length-1)
            {
                CircularBuffer.Add(numberToInject);
            }
            else 
            {
                CircularBuffer.Insert(index+1, numberToInject);
            }
            
            ActualPosition = index + 1;
        }
    }
}
