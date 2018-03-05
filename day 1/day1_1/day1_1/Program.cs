using System;
using System.IO;

namespace day1_1
{
    // Opis zadań
    //   Part 1
    //The captcha requires you to review a sequence of digits(your puzzle input) and find the sum of all digits that match the next digit in the list.The list is circular, so the digit after the last digit is the first digit in the list.


//For example:

//    1122 produces a sum of 3 (1 + 2) because the first digit(1) matches the second digit and the third digit(2) matches the fourth digit.
//    1111 produces 4 because each digit (all 1) matches the next.
//    1234 produces 0 because no digit matches the next.
//    91212129 produces 9 because the only digit that matches the next one is the last digit, 9.

//What is the solution to your captcha?

    // Part 2
//    Now, instead of considering the next digit, it wants you to consider the digit halfway around the circular list.That is, if your list contains 10 items, only include a digit in your sum if the digit 10/2 = 5 steps forward matches it.Fortunately, your list has an even number of elements.


//For example:

//    1212 produces 6: the list contains 4 items, and all four digits match the digit 2 items ahead.
//    1221 produces 0, because every comparison is between a 1 and a 2.
//    123425 produces 4, because both 2s match each other, but no other digit has a match.
//    123123 produces 12.
//    12131415 produces 4.
    
    
    class Captcha //tworzę nową klasę do obłsugi captchy
    {
        public String Puzzle; //zmienna do kórej przypiszę moja captche
        int CaptchaLength; 
        //otwieranie pliku txt
        public void FileOpen()
        {
            try
            {
                using (StreamReader sr = new StreamReader("Captcha.txt"))
                {
                
                    // Read the stream to a string, and write the string to the console.
                    Puzzle = sr.ReadToEnd();
                    Console.WriteLine(Puzzle);          
                    
                }
            }
            catch (Exception e) // jeśli nie znajdzie pliku, wyswietli komunikat
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }            
        }
        //metoda do obliczania pierwszego podpunktu
        public int SumOfNumbers(string line)
        {
            CaptchaLength = line.Length;
            int Sum = 0;
            //sprawdzenie czy 1 i ostatnia cyfra są takie same
            if (line[0]==line[CaptchaLength-1])
            {
                Sum += Convert.ToInt32(line[0])-48;
            }

            //sprawdzenie czy sąsiednie cyfry są takie same
            for (int i = 0; i < CaptchaLength-1; i++)
            {
                if (line[i] == line[i + 1])
                {
                    Sum += Convert.ToInt32(line[i]) - 48; //-48 bo przy konwersji daje wynik w kodzie ascii
                }
            }

            //Console.WriteLine(line[0] + "  " + line[CaptchaLength-1]);
            return Sum;
        }

        //drugi podpunkt
        public int SumOfNumbers2(string line)
        {
            CaptchaLength = line.Length;

            int CaptchaStep = CaptchaLength / 2; //obliczenie kroku
            int Sum = 0;

            for (int i = 0; i < CaptchaStep; i++)
            {
                //int j = i + CaptchaStep;

                //if (j>=CaptchaLength)
                //{
                //    j = j - CaptchaLength;
                //}

                if (line[i] == line[i+CaptchaStep])
                {
                    Sum += 2* (Convert.ToInt32(line[i]) - 48); //-48 bo przy konwersji daje wynik w kodzie ascii
                }
            }

            //Console.WriteLine(line[0] + "  " + line[CaptchaLength-1]);
            return Sum;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Captcha captcha = new Captcha(); //utworzenie nowego obiektu
            captcha.FileOpen();

            Console.WriteLine("\nWynik zad 1: ");
            Console.WriteLine(captcha.SumOfNumbers(captcha.Puzzle));
            Console.WriteLine("\nWynik zad 2: ");
            Console.WriteLine(captcha.SumOfNumbers2(captcha.Puzzle));
        }
    }
}
