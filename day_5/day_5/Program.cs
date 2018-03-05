using System;
using System.Collections.Generic;
using System.IO;

namespace day_5
{
            //    --- Day 4: High-Entropy Passphrases ---

            //A new system policy has been put in place that requires all accounts to use a passphrase instead of simply a password.A passphrase consists of a series of words (lowercase letters) separated by spaces.

            //To ensure security, a valid passphrase must contain no duplicate words.

            //For example:

            //    aa bb cc dd ee is valid.
            //    aa bb cc dd aa is not valid - the word aa appears more than once.
            //    aa bb cc dd aaa is valid - aa and aaa count as different words.

            //The system's full passphrase list is available as your puzzle input. How many passphrases are valid?

            //Your puzzle answer was 386.
            //--- Part Two ---

            //For added security, yet another system policy has been put in place.Now, a valid passphrase must contain no two words that are anagrams of each other - that is, a passphrase is invalid if any word's letters can be rearranged to form any other word in the passphrase.

            //For example:

            //    abcde fghij is a valid passphrase.
            //    abcde xyz ecdab is not valid - the letters from the third word can be rearranged to form the first word.
            //    a ab abc abd abf abj is a valid passphrase, because all letters need to be used when forming another word.
            //    iiii oiii ooii oooi oooo is valid.
            //    oiii ioii iioi iiio is not valid - any of these words can be rearranged to form any other word.

            //Under this new system policy, how many passphrases are valid?

        //Your puzzle answer was 208.

    class Steps
    {
        public List<int> list = new List<int>();
        public void FileOpen()
        {
            try
            {
                using (StreamReader sr = new StreamReader("E:\\Nauka\\Kurs C#\\Advent of Code 2017\\day_5\\Steps.txt"))
                {
                    while (sr.EndOfStream == false)
                    {
                        list.Add(Convert.ToInt32(sr.ReadLine()));
                    }
                }
                //Console.WriteLine(list.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine("Nie udało się otworzyć pliku: ");
                Console.WriteLine(e.Message);
            }
            
        }

        public void Step1()
        {
            int indeks = 0;
            int Counter = 0, SizeOfStep = 0;
            //Console.WriteLine("Licznik:Krok\tindeks:");
            //Console.WriteLine(list[6]);
            while (indeks<list.Count)
            {
                SizeOfStep = list[indeks];
                list[indeks]++;
                indeks += SizeOfStep;
                
                //Console.WriteLine(Counter + "\t" + SizeOfStep +  "\t" + indeks);
                Counter++;
            }

            Console.WriteLine("Odpowiedz do zadania 1: " + Counter);
        }

        public void Step2()
        {
            int indeks = 0;
            int Counter = 0, SizeOfStep = 0;
            while (indeks < list.Count)
            {
                SizeOfStep = list[indeks];
                if(list[indeks]<3)
                {
                    list[indeks]++;
                }
                else
                {
                    list[indeks]--;
                }
                indeks += SizeOfStep;
                Counter++;
            }

            Console.WriteLine("Dopowiedz do zadania 2: " + Counter);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Steps steps1 = new Steps();
            steps1.FileOpen();
            steps1.Step1();
            //foreach (var item in steps.list)
            //{
            //    Console.WriteLine(item);
            //}
            steps1.list.Clear();
            steps1.FileOpen();
            steps1.Step2();
            //foreach (var item in steps1.list)
            //{
            //    Console.WriteLine(item);
            //}
        }
    }
}
