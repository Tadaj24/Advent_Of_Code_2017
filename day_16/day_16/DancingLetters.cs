using System;
using System.Collections.Generic;
using System.IO;

namespace day_16
{
    class DancingLetters
    {
        public int LettersCount = 16; //ilosc liter ktorych uzyway do tanca
        public char[] Letters; //tworzę tablicę znaków od a do p
        public char[] LettersBase; //tworzę tablicę znaków od a do p
        public List<string> DanceMoves = new List<string>(); //tworze nowa liste przetrzymującą


        public void PrintLetters()
        {
            string word = "";
            foreach (var item in Letters)
            {
                word += item;
            }
            Console.WriteLine(word);
        }
        public void MakeOperation1()
        {
            Letters = new char[LettersCount];
            LettersBase = new char[LettersCount];
            FIleOpen();
            FillLettersTable();

            if (TheyAreSame()==true)
            {
                
            }
            int z = 0;
            for (int l = 0; l < 1000000000 % 60; l++)
            {
                foreach (var Move in DanceMoves)
                {
                    DanceMove(Move);
                    //PrintLetters();
                    //Console.WriteLine(++z);
                }

                if (TheyAreSame() == true)
                {
                    Console.WriteLine(l);
                }

                
            }
            PrintLetters();
            
            
        }
        public bool TheyAreSame()
        {
            //int ka = 0;
            for (int i = 0; i < Letters.Length; i++)
            {
                if (Letters[i]!=LettersBase[i])
                {
                    return false;
                }
            }
            return true;
        }

        public void MakeStep(string step)//przesuwanie wszystkich indeksów
        {
           char[] ExchangeTabel= new char[LettersCount]; //tworzy sie pomocnicza tabela           

            int k = 0;
            foreach (var item in Letters)
            {
                char znak = item;
                ExchangeTabel[k] = znak;
            }

           step=step.TrimStart('s'); //ucinam ske

           int stepSize = Convert.ToInt32(step); //pobieram dlguosc kroku
            
            for (int i = 0; i < Letters.Length; i++)
            {
                int index = i+stepSize;
                //Console.WriteLine(index);
                if (index>=Letters.Length)
                {
                    index = index % Letters.Length;
                }
                //Console.WriteLine(index);
                ExchangeTabel[index] = Letters[i];
            }
            k = 0;
            foreach (var item in Letters)
            {
                Letters[k] = ExchangeTabel[k]; 
                k++;
            }
            
        }
        public void MakeExchange(string step)
        {
            step = step.TrimStart('x');
            string[] exchange = step.Split(new char[] { '/' });

            int firstIndex = Convert.ToInt32(exchange[0]);
            int secondIndex = Convert.ToInt32(exchange[1]);
            char Memory = Letters[firstIndex];
            Letters[firstIndex] = Letters[secondIndex];
            Letters[secondIndex] = Memory;
        }
        public void MakePartner(string step)
        {
            //step = step.TrimStart('p');
            string[] partner = step.Split(new char[] { '/' });
            
            int firstIndex=-1;
            int secondIndex=-1;
            int k = 0;
            foreach (var lettter in Letters)
            {
                if (Convert.ToString(lettter)==Convert.ToString(partner[0][1]))
                {
                    firstIndex = k;
                }
                else if (Convert.ToString(lettter) == Convert.ToString(partner[1]))
                {
                    secondIndex = k;
                }
                if (firstIndex>=0 && secondIndex>=0)
                {
                    break;
                }
                k++;
            }
            char Memory = Letters[firstIndex];
            Letters[firstIndex] = Letters[secondIndex];
            Letters[secondIndex] = Memory;
        }

        public void DanceMove(string move) //analizuja poszczegolne kroki
        {
            if (move[0]=='s') //step
            {
                MakeStep(move);
            }
            else if (move[0]=='x')
            {
                MakeExchange(move);
            }
            else if (move[0] == 'p')
            {
                MakePartner(move);
            }
            else Console.WriteLine("Coś jest nie tak z krokami...");


        }

        public void FIleOpen()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"E:\Nauka\Kurs C#\Advent of Code 2017\Puzzle\day_16.txt"))
                {
                    string Input = sr.ReadLine();
                    //Console.WriteLine(Input);
                    SplitLine(Input);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SplitLine(string input)//dzieli wejscie na konkretne komendy
        {
            string[] Foo = input.Split(new char[] { ',' });

            for (int i = 0; i < Foo.Length; i++)
            {
                DanceMoves.Add(Foo[i]);                
                //Console.WriteLine(Foo[i]);
            }
            Console.WriteLine("Ilosc krokow: " + DanceMoves.Count);

        }

        public void FillLettersTable() //tworzy tabele znaków tancerzy
        {
            int tableIndex = 0;
            for (int i = 0; i < LettersCount; i++)
            {
                Letters[i]=Convert.ToChar(i+97);
                LettersBase[i]=Convert.ToChar(i+97);

                tableIndex++;
            }
        }
    }
}
