using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace day_10
{
    class KnottHash
    {
        public List<int> InputList = new List<int>(); //lista z wejsciami
        public List<int> NumbersList = new List<int>();//lista numberow
        public int Step = 0;

        //otwiera plik
        public void FileOpen()
        {
            try
            {
                using (StreamReader sr = new StreamReader("E:\\Nauka\\Kurs C#\\Advent of Code 2017\\Puzzle\\day_10.txt"))
                {
                    Console.WriteLine("Udało mi się otworzyć plik.\n");
                    string Input = sr.ReadToEnd();
                    SplitInput(Input);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //dzieli wejscie na liste
        public void SplitInput(string input)
        {
            String[] Foo = input.Split(new char[] { ',' });
            foreach (string item in Foo)
            {
                InputList.Add(Convert.ToInt32(item));
            }

        } 

        //tworzy listę bazowa
        public void CreateNumberList()
        {
            int NumberAmount = 256;
            for (int i = 0; i < NumberAmount; i++)
            {
                NumbersList.Add(i);
            }
        }

        public void MakeOperations()
        {
            FileOpen();
            CreateNumberList();
            //ShowList();

            int StartIndex = 0;

            foreach (var WartoscInput in InputList)
            {
                KnottBind(StartIndex, WartoscInput);
                Console.WriteLine("Aktualny indeks: " + StartIndex);

                StartIndex += Step + WartoscInput;
                if (NumbersList.Count() - 1 < StartIndex)
                {
                    StartIndex -= NumbersList.Count;
                    
                } //powoduje zapetlenie

                //ShowList();
                Step++;               
                
            }

            Console.WriteLine(NumbersList[0]*NumbersList[1]);
            //KnottBind(3, 3);
            //ShowList();
        }

        //wykonuje operacje zamiany; Input-ile ma liczb zamienic
        public void KnottBind(int startIndex, int Input)
        {
            List<int> PomocniczaList = new List<int>(); //będą w niej zapisywane chwilowe wartości
            string word = "";

            for (int i = startIndex; i < startIndex+Input; i++)
            {
                int ActualIndex = i;
                if (NumbersList.Count()-1<ActualIndex)
                {
                    ActualIndex -= NumbersList.Count;
                } //powoduje zapetlenie
                //Console.WriteLine(ActualIndex);

                PomocniczaList.Add(NumbersList[ActualIndex]);
                word += NumbersList[ActualIndex] + " ";
            }
            Console.WriteLine("Pomocnicza: " + word);

            int PomocniczaIndex = PomocniczaList.Count-1;
            for (int i = startIndex; i < startIndex + Input; i++)
            {
                int ActualIndex = i;
                if (NumbersList.Count() - 1 < ActualIndex)
                {
                    ActualIndex -= NumbersList.Count;
                } //powoduje zapetlenie
                NumbersList[ActualIndex] = PomocniczaList[PomocniczaIndex];
                PomocniczaIndex--;
            }
            PomocniczaList.Clear();
        }

        public void ShowList()
        {
            string word1 = "Aktualny ciąg znaków \t", word2 = "Indeks: \t \t";
            int k = 0;
            foreach (var item in NumbersList)
            {
                word1 += item + "\t";
                word2 += k++ + "\t";
            }
            Console.WriteLine("Podejście numer " + (Step+1) + ". Aktualna wartosc inputa: " + InputList[Step]);
            Console.WriteLine(word2);
            Console.WriteLine(word1);
            Console.WriteLine();
        }

        
    }


    class Program
    {
        static void Main(string[] args)
        {
            KnottHash knottHash = new KnottHash();
            string zmienna = "1";
            Encoding asciiEncoding = Encoding.ASCII;

            Console.WriteLine(Convert.ToInt32("1"));

            string text = "AaBbcde1234";
            for (int i = 0; i < text.Length; i++)
            {
                Console.WriteLine((Int16)text[i]);
                Console.WriteLine(Convert.ToInt16(text[i]));
            }

            //knottHash.MakeOperations();

        }


    }
}
