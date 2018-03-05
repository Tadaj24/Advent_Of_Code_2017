using System;
using System.Collections.Generic;
using System.IO;

namespace day_11
{
    class Path
    {
        List<string> Lista = new List<string>(); //lista z kierunkami
        List<int> Kierunki = new List<int>();
        public int maks = 0;
        public int suma = 0;
        public void FileOpen()
        {
            try
            {
                using (StreamReader sr = new StreamReader("E:\\Nauka\\Kurs C#\\Advent of Code 2017\\Puzzle\\day_11.txt"))
                {
                    Console.WriteLine("Udało mi się otworzyć plik.\n");
                    string Input = sr.ReadToEnd();

                    SplitText(Input);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SplitText(string input)
        {
            String[] Foo = input.Split(new char[] { ',' });

            for (int i = 0; i < Foo.Length; i++)
            {
                Lista.Add(Foo[i]);
            }
            Console.WriteLine(Lista.Count);
        }

        public void MakeOperations()
        {
            FileOpen();
            CountEachDirection();
        }

        //liczy ile jakie sa wspolrzedne w danym kierunku
        public void CountEachDirection()
        {
            for (int i = 0; i < 6; i++)
            {
                Kierunki.Add(0);
            }

            foreach (string item in Lista)
            {
                switch(item)
                {
                    case "n":
                        {
                            Kierunki[0]++;
                            break;
                        }
                    case "ne":
                        {
                            Kierunki[1]++;
                            break;
                        }
                    case "se":
                        {
                            Kierunki[2]++;
                            break;
                        }
                    case "s":
                        {
                            Kierunki[3]++;
                            break;
                        }
                    case "sw":
                        {
                            Kierunki[4]++;
                            break;
                        }
                    case "nw":
                        {
                            Kierunki[5]++;
                            break;
                        }
                   
                }
                ActualPosition();
            }

            Console.WriteLine("Maksimum: " + maks);

            foreach (var item2 in Kierunki)
            {
                Console.WriteLine(item2);
            }

            Console.WriteLine();

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(Kierunki[i] - Kierunki[i+3]);
            }

            Console.WriteLine();

            Console.WriteLine(maks);
            Console.WriteLine(suma);
        }

        public void ActualPosition()
        {
            int x = Kierunki[0] - Kierunki[3];
            int y = Kierunki[1] - Kierunki[4];
            int z=  Kierunki[2] - Kierunki[5];

            suma = Math.Abs(x) + Math.Abs(y);
            if(maks<suma)
            {
                maks = suma;
            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Path path = new Path();
            path.MakeOperations();

        }
    }
}
