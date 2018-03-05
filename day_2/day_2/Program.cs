using System;
using System.IO;

namespace day_2
{
    class Program
    {
        class Checksum
        {
            public String Numbers;
            public int Suma = 0;
            public int NumbersLength = 0;
            public int[] RowNumbers;
            
            
            //otwieranie pliku
            public void FileOpen()
            {
                try
                {
                    using (StreamReader sr = new StreamReader("E:\\Nauka\\Kurs C#\\Advent of Code 2017\\day_2\\Checksum.txt"))
                    {
                        while(sr.EndOfStream == false) // sprawdza czy nie jest koniec pliku
                        {
                            Numbers = sr.ReadLine(); //odczytuje linie
                            NumbersLength = CreateIntTable(); //dzieli linie na tablice string a potem na inty

                            Suma=Suma+ResultOfLine(NumbersLength); //szuka dzielnika
                        }

                        //Console.WriteLine(Suma);
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }

            //metoda podzialu linii na pojedyncze wyrazy a nastepnieprzekonwertowuje je na int
            public int CreateIntTable()
            {
                String[] Foo = Numbers.Split(new char[] { '\t' }); //dzieli linię na tablice z odseparowanymi liczbami
                RowNumbers = new int[Foo.Length]; //tworzy nowa tabele o takiej samej dlugosci jak Foo
                
                int indeks= 0; // indeks tablicy intów

                foreach (string liczba in Foo) //przepisuje tablice char na int
                {
                    RowNumbers[indeks] = Convert.ToInt32(liczba);
                    indeks++;
                }
                //Console.WriteLine("kur3a");

                return indeks; //zwraca ilość liczb w lini
                
            }

            //metoda licząca wartośc lini dla zad 2
            public int ResultOfLine(int line_length)
            {
                bool DidHeFind = false;
                int element=0;
                for (int i = 0; i <line_length; i++)
                {
                    for (int k = i+1; k < line_length; k++)
                    {
                        if(RowNumbers[i]%RowNumbers[k]==0)
                        {
                            element = RowNumbers[i] / RowNumbers[k];
                            DidHeFind = true;
                            Console.WriteLine(RowNumbers[k] + "   " + RowNumbers[i] + " Wynik dzielenia: " +element);
                            break;
                        }
                        else if (RowNumbers[k] % RowNumbers[i] == 0)
                        {
                            element = RowNumbers[k] / RowNumbers[i];
                            DidHeFind = true;
                            Console.WriteLine(RowNumbers[k] + "   " + RowNumbers[i] + " Wynik dzielenia: " + element);
                            break;
                            
                        }

                        if (DidHeFind == true)

                            break;
                    }
                    if (DidHeFind == true)
                        
                    break;
                }

                return element;
            }


            

        }


        static void Main(string[] args)
        {
            #region Task           
            // Task 1
            //            he spreadsheet consists of rows of apparently-random numbers. To make sure the recovery process is on the right track, they need you to calculate the spreadsheet's checksum. For each row, determine the difference between the largest value and the smallest value; the checksum is the sum of all of these differences.
            //            For example, given the following spreadsheet:
            //            5 1 9 5
            //7 5 3
            //2 4 6 8
            //    The first row's largest and smallest values are 9 and 1, and their difference is 8.
            //    The second row's largest and smallest values are 7 and 3, and their difference is 4.
            //    The third row's difference is 6.
            //In this example, the spreadsheet's checksum would be 8 + 4 + 6 = 18.
            #endregion

            Checksum checksum = new Checksum();
            checksum.FileOpen();
            Console.WriteLine(checksum.Suma);

            
            //string[] Foo = Numbers.Split(new char[] { ' ' });

            
            Console.Read();
        }
    }
}
