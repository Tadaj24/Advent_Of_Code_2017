using System;
using System.Collections.Generic;
using System.IO;

namespace day_6
{
                //    --- Day 6: Memory Reallocation ---

                //A debugger program here is having an issue: it is trying to repair a memory reallocation routine, but it keeps getting stuck in an infinite loop.

                //In this area, there are sixteen memory banks; each memory bank can hold any number of blocks.The goal of the reallocation routine is to balance the blocks between the memory banks.

                //The reallocation routine operates in cycles.In each cycle, it finds the memory bank with the most blocks (ties won by the lowest-numbered memory bank) and redistributes those blocks among the banks. To do this, it removes all of the blocks from the selected bank, then moves to the next (by index) memory bank and inserts one of the blocks. It continues doing this until it runs out of blocks; if it reaches the last memory bank, it wraps around to the first one.

                //The debugger would like to know how many redistributions can be done before a blocks-in-banks configuration is produced that has been seen before.

                //For example, imagine a scenario with only four memory banks:

                //    The banks start with 0, 2, 7, and 0 blocks. The third bank has the most blocks, so it is chosen for redistribution.
                //    Starting with the next bank (the fourth bank) and then continuing to the first bank, the second bank, and so on, the 7 blocks are spread out over the memory banks. The fourth, first, and second banks get two blocks each, and the third bank gets one back.The final result looks like this: 2 4 1 2.
                //   Next, the second bank is chosen because it contains the most blocks(four).Because there are four memory banks, each gets one block. The result is: 3 1 2 3.
                // Now, there is a tie between the first and fourth memory banks, both of which have three blocks. The first bank wins the tie, and its three blocks are distributed evenly over the other three banks, leaving it with none: 0 2 3 4.
                //    The fourth bank is chosen, and its four blocks are distributed such that each of the four banks receives one: 1 3 4 1.
                //    The third bank is chosen, and the same thing happens: 2 4 1 2.

                //At this point, we've reached a state we've seen before: 2 4 1 2 was already seen.The infinite loop is detected after the fifth block redistribution cycle, and so the answer in this example is 5.

                //Given the initial block counts in your puzzle input, how many redistribution cycles must be completed before a configuration is produced that has been seen before?

                //Your puzzle answer was 5042.
                //-- - Part Two-- -

                // Out of curiosity, the debugger would also like to know the size of the loop: starting from a state that has already been seen, how many block redistribution cycles must be performed before that same state is seen again ?

                // In the example above, 2 4 1 2 is seen again after four cycles, and so the answer in that example would be 4.

                //How many cycles are in the infinite loop that arises from the configuration in your puzzle input?

                //Your puzzle answer was 1086.

    class MemoryRelocation
    {
        string Line;
        List<int> list = new List<int>();
        List<string> Words = new List<string>();
        int NumberOfStep = 0;
        bool DidHeFind = false;

        public void FileOpen()
        {
            try
            {
                using (StreamReader sr = new StreamReader("E:\\Nauka\\Kurs C#\\Advent of Code 2017\\Puzzle\\day_6.txt"))
                //using (StreamReader sr = new StreamReader("E:day_6.txt"))
                {
                    Line = sr.ReadLine();
                    CreateList();
                    while (DidHeFind==false)
                    {
                        FindMaximum();
                        CheckValidate();
                        NumberOfStep++;


                    }
                    Console.WriteLine(NumberOfStep);
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Nie udało się otworzyć pliku:");
                Console.WriteLine(e.Message);
                
            }
        }
        
        public void CreateList()
        {
            String[] Foo = Line.Split(new char[] { '\t' });
            //int indeks = 0;
            foreach (string liczba in Foo) //przepisuje tablice char na int
            {
                list.Add(Convert.ToInt32(liczba));
            }
        }

        public void FindMaximum()
        {
            int maks = 0, indeks = 0, k=0;

            foreach (int item in list)
            {
                if (k==0)
                {
                    maks = item;
                }
                else if (list[indeks] < item)
                {
                    indeks = k;
                    maks = item;
                }
                k++;           
            }
            list[indeks] = 0;
            Rozdaj(maks,indeks);
        }

        public void Rozdaj(int Value,int indeks)
        {

            int ActualIndeks=indeks+1;
            while (Value>0)
            {
                if (ActualIndeks > list.Count-1)
                {
                    ActualIndeks = 0;
                }
                list[ActualIndeks]++;
                ActualIndeks++;
                Value--;
                //Console.WriteLine(Value);
            }
            CreateWord();
        }

        public void CreateWord()
        {
            string word="";
            foreach (var item in list)
            {
                word = word + Convert.ToString(item) + ".";
            }
            Words.Add(word);
            //Console.WriteLine(NumberOfStep +".\t"+ word);
        }

        public void CheckValidate()
        {
            for (int i = 0; i < Words.Count-1; i++)
            {
                for (int j = i+1; j < Words.Count; j++)
                {
                    if (Words[i] == Words[j])
                    {
                        DidHeFind = true;
                        Console.WriteLine(Words[i] + "     " + i + "    " + j);
                        Console.WriteLine("podpunkt 2: " + (j-i));
                        break;
                    }                        
                }
                if (DidHeFind == true)
                {
                    break;
                }
            }
        }

    }
        

    class Program
    {
        static void Main(string[] args)
        {
            MemoryRelocation memoryRelocation = new MemoryRelocation();
            memoryRelocation.FileOpen();
            //memoryRelocation.ShowList();
           
        }
    }
}
