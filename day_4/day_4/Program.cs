using System;
using System.Collections.Generic;
using System.IO;

namespace day_4
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


    class Passphrases
    {
        public int CorrectLinesTask1 = 0;
        public int CorrectLinesTask2 = 0;
        public void FileOpen()
        {

            try
            {
                //using (StreamReader sr = new StreamReader("Puzzle.txt"))
                using (StreamReader sr = new StreamReader("haslo.txt"))
                {
                    while (sr.EndOfStream == false)
                    {
                        string Password = sr.ReadLine();
                        CheckLineTask1(Password);
                        CheckLineTask2(Password);
                    }

                    Console.WriteLine("W zadaniu 1 jest " + CorrectLinesTask1 + " poprawnych hasel.");
                    Console.WriteLine("W zadaniu 2 jest " + CorrectLinesTask2 + " poprawnych hasel.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Podany plik nie może zostać otworzony: ");
                Console.WriteLine(e.Message);                
            }
        }

        public bool SortAndCheck(string word1, string word2) //metoda sortujaca i sprawdzajaca czy 2 wyrazy sa takie same
        {
            bool WordsAreSame = false;           

            //deklaracja list potrzebnych do posortowania
            List<char> Word1 = new List<char>();
            List<char> Word2 = new List<char>();
            string NewWord1 = "", NewWord2 = "";

            if (word1.Length == word2.Length)
            {
                for(int i = 0; i < word1.Length; i++) //stworzenie 2 list znakow
                {
                    Word1.Add(word1[i]);
                    Word2.Add(word2[i]);
                }
                
                Word1.Sort();               
                Word2.Sort();                

                for (int k = 0; k < Word1.Count; k++) //tworzenie nowego posortowanego wyrazu
                {
                    NewWord1 += Word1[k];
                    NewWord2 += Word2[k];                    
                }                

                if(NewWord1 == NewWord2) //porownanie psoortowanych wyrazow
                {
                    WordsAreSame = true;
                }
            }            
            return WordsAreSame;
        }

        public void CheckLineTask1(string password)
        {
            bool DidHeFind = false;
            String[] Foo = password.Split(new char[] { ' ' });
            int TableLength = Foo.Length;

            for (int i = 0; i < TableLength-1; i++)
            {
                for (int j = i+1; j < TableLength; j++)
                {
                    if (Foo[i] == Foo[j])
                    {
                        DidHeFind = true;
                        break;
                    }
                }
                if (DidHeFind == true)
                {
                    break;
                }
            }

            if (DidHeFind == false)
            {
                CorrectLinesTask1++;
            }            
        }

        public void CheckLineTask2(string password)
        {
            bool DidHeFind = false;
            String[] Foo = password.Split(new char[] { ' ' });
            int TableLength = Foo.Length;

            for (int i = 0; i < TableLength - 1; i++)
            {
                for (int j = i + 1; j < TableLength; j++) 
                {
                     DidHeFind=SortAndCheck(Foo[i], Foo[j]);

                    if (DidHeFind ==true)
                    {
                        break;                        
                    }                 
                }

                if (DidHeFind == true)
                {
                    break;
                }
            }
            if (DidHeFind == false)
            {
                CorrectLinesTask2++;
            }
        }

        
    }

    class Program
    {
        static void Main(string[] args)
        {
            Passphrases passphrases = new Passphrases();
            passphrases.FileOpen();
        }
    }
}
