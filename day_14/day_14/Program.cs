using System;
using System.Collections.Generic;
using System.Linq;

namespace day_14
{
    class HardDisk
    {
        #region dysku
        int IleJedynek = 0;
        //public string Input = "flqrgnkx-";
        public string Input = "vbqugkhl-";

        public int GroupNumber = 2;
        public List<int[]> ListaReady = new List<int[]>();
        public List<int[]> ListaNotReady = new List<int[]>();
        public List<int[]> Wiersze = new List<int[]>();

        public void AddElementToNotReadyList(int numerWiersza, int numerKolumny) //wrzuca do stosu numer wiersza i kolumny
        {
            ListaNotReady.Add(new int[2] { numerWiersza, numerKolumny });
        }
        public bool IsHeInList(int numerWiersza, int numerKolumny) //sprawdza czy dana komorka jest juz na liscie
        {
            //oznacza który mamy wybrany stos
            foreach (var element in ListaNotReady) //srpawdza czy dany element jest już na liście
            {
                if (element[0] == numerWiersza && element[1] == numerKolumny)
                {
                    return true;
                }
            }

            foreach (var element in ListaReady) //srpawdza czy dany element jest już na liście
            {
                if (element[0] == numerWiersza && element[1] == numerKolumny)
                {
                    return true;
                }
            }
            return false;
        }
        public void SprawdzWokol(int numerWiersza, int numerKolumny) //sprawdza na około 
        {
            bool DidHeAdd = false;

            //dol
            int W = numerWiersza - 1, K = numerKolumny;
            if (W>=0)
            {
                if (Wiersze[W][K] == 1 && IsHeInList(W, K) == false) //GORA
                {
                    AddElementToNotReadyList(W, K);
                    DidHeAdd = true;
                }
            }            

            //gora
            W = numerWiersza + 1; K = numerKolumny;
            if (W<128)
            {
                if (Wiersze[W][K] == 1 && IsHeInList(W, K) == false) //dol
                {
                    AddElementToNotReadyList(W, K);
                    DidHeAdd = true;
                }
            }            

            //lewo
            W = numerWiersza; K = numerKolumny - 1;
            if(K>=0)
            {
                if (Wiersze[W][K] == 1 && IsHeInList(W, K) == false) //dol
                {
                    AddElementToNotReadyList(W, K);
                    DidHeAdd = true;
                }
            }            

            //prawo
            W = numerWiersza; K = numerKolumny + 1;
            if (K<128)
            {
                if (Wiersze[W][K] == 1 && IsHeInList(W, K) == false) 
                {
                    AddElementToNotReadyList(W, K);
                    DidHeAdd = true;
                }
            }
            

            ListaReady.Add(new int[2] { numerWiersza, numerKolumny });
            //return DidHeAdd;
        }
        public void CreateGroup(int numerGrupy,int wPocz, int kPocz) //tworzy całą grupę
        {
            //Console.WriteLine("Ilosc w ready: " + ListaReady.Count + "Ilosc w notready: " + ListaNotReady.Count);
            ListaNotReady.Clear();
            ListaReady.Clear();

            if (Wiersze[wPocz][kPocz]==1)
            {
                SprawdzWokol(wPocz, kPocz); //tworzy bazowa listę
                GroupNumber++;
            }

            while (ListaNotReady.Count!=0) //dopoki sa tam jakiekolwiek elementy
            {
                int Wie = ListaNotReady[0][0]; //pobiera wartosci z pierwszego elementu
                int Kol = ListaNotReady[0][1];
                SprawdzWokol(Wie, Kol);
                ListaNotReady.RemoveAt(0);
            }

            while(ListaReady.Count!=0) //zmiana wartości grupy
            {
                int Wie = ListaReady[0][0]; //pobiera wartosci z pierwszego elementu
                int Kol = ListaReady[0][1];

                Wiersze[Wie][Kol] = numerGrupy;
                ListaReady.RemoveAt(0);
            }
        }

        public void CountGroups()
        {
            int NumerWiersza = 0;
            foreach (var item in Wiersze) //iteruje po każdym wierszu
            {
                Console.WriteLine("Wiersz " + NumerWiersza);
                for (int k = 0; k < item.Length; k++)
                {
                    CreateGroup(GroupNumber, NumerWiersza, k);
                }
                NumerWiersza++;
            }
            Console.WriteLine(GroupNumber-2);
        }

        #endregion

        #region Region part2

        public List<int> InputList2 = new List<int>(); //lista z wejsciami
        public List<int> NumbersList = new List<int>();//lista numberow
        public List<int> DenseHashList = new List<int>();//lista numberow
        public List<int> DenseHashListInBinary = new List<int>();

        //public Stack<int> StosWierszReady = new Stack<int>(); //stos do trzymania wierszy gotowych, sprawdzonych na około
        //public Stack<int> StosKolumnaReady = new Stack<int>(); //stos do trzymania kolumn, sprwawdzonych na około

        //public Stack<int> StosWierszNotReady = new Stack<int>(); //stos do trzymania wierszy
        //public Stack<int> StosKolumnaNotReady = new Stack<int>(); //stos do trzymania kolumn

        public int Step = 0;     

        //dzieli wejscie na kody ASCII
        public void ToAsciiDecimal(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                InputList2.Add(Convert.ToInt16(input[i]));
            }
            //string word = "";

            InputList2.Add(17);
            InputList2.Add(31);
            InputList2.Add(73);
            InputList2.Add(47);
            InputList2.Add(23);
            //foreach (var item in InputList2)
            //{
            //    word += item + " ";
            //}
            //Console.WriteLine(word);
        }


        //tworzy listę bazowa
        public void CreateNumberList()
        {
            NumbersList.Clear();
            int NumberAmount = 256;
            for (int i = 0; i < NumberAmount; i++)
            {
                NumbersList.Add(i);
            }
        }        

        public void MakeOperations2()
        {
            for (int g = 0; g < 128; g++)
            {
                ToAsciiDecimal(Input + g);
                CreateNumberList();
                int StartIndex = 0;
                Step = 0;
                for (int j = 0; j < 64; j++)
                {
                    foreach (var WartoscInput in InputList2)
                    {
                        KnottBind(StartIndex, WartoscInput);

                        StartIndex += Step + WartoscInput;
                        if (NumbersList.Count() - 1 < StartIndex)
                        {
                            StartIndex = StartIndex % NumbersList.Count;

                        } //powoduje zapetlenie

                        Step++;
                    }
                    //Console.WriteLine(++d);
                }
                DenseHash();
                ConvertToHexadecimal();

                DenseHashList.Clear();
                DenseHashListInBinary.Clear();
                InputList2.Clear();
            }
            Console.WriteLine(IleJedynek);
            //Console.WriteLine(Wiersze.Count);
        }

        

        //wykonuje operacje zamiany; Input-ile ma liczb zamienic
        public void KnottBind(int startIndex, int Input)
        {
            List<int> PomocniczaList = new List<int>(); //będą w niej zapisywane chwilowe wartości
            string word = "";

            for (int i = startIndex; i < startIndex + Input; i++)
            {
                int ActualIndex = i;
                if (NumbersList.Count() - 1 < ActualIndex)
                {
                    ActualIndex -= NumbersList.Count;
                } //powoduje zapetlenie
                //Console.WriteLine(ActualIndex);

                PomocniczaList.Add(NumbersList[ActualIndex]);
                word += NumbersList[ActualIndex] + " ";
            }
            //Console.WriteLine("Pomocnicza: " + word);

            int PomocniczaIndex = PomocniczaList.Count - 1;
            for (int i = startIndex; i < startIndex + Input; i++)
            {
                int ActualIndex = i;
                if (NumbersList.Count() - 1 < ActualIndex)
                {
                    ActualIndex = ActualIndex % NumbersList.Count;
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
            int suma = 0;
            foreach (var item in NumbersList)
            {
                word1 += item + "\t";
                word2 += k++ + "\t";
                suma += item;
            }

            Console.WriteLine("Suma: " + Step + " " + suma);
            //Console.WriteLine("Podejście numer " + (Step+1) + ". Aktualna wartosc inputa: " + InputList[Step]);
            //Console.WriteLine(word2);
            //Console.WriteLine(word1);
            Console.WriteLine();
        }

        public void DenseHash()
        {
            for (int i = 0; i < NumbersList.Count; i += 16)
            {
                int DenseHash = NumbersList[i];
                for (int j = i + 1; j < i + 16; j++)
                {
                    DenseHash = DenseHash ^ NumbersList[j]; //xor
                }
                DenseHashList.Add(DenseHash);
                //Console.WriteLine(DenseHash);

            }
        }

        public void ConvertToHexadecimal()
        {
            string Word = "";
            foreach (int Value in DenseHashList)
            {
                Word += String.Format("{0:D2}", Value.ToString("x2"));
            }
            //Console.WriteLine(Word);
            //Console.WriteLine("Dlugosc hexa: " + Word.Length);
            ConvertToBinary(Word);
        }

        public int HexValue(char letter)
        {
            int value;

            switch (letter)
            {
                case 'a':
                    {
                        value = 10;
                        break;
                    }
                case 'b':
                    {
                        value = 11;
                        break;
                    }
                case 'c':
                    {
                        value = 12;
                        break;
                    }
                case 'd':
                    {
                        value = 13;
                        break;
                    }
                case 'e':
                    {
                        value = 14;
                        break;
                    }
                case 'f':
                    {
                        value = 15;
                        break;
                    }
                default:
                    {
                        value = Convert.ToInt32(letter-48);
                        break;
                    }

            }
            //Console.WriteLine(value);
            return value;
            
        }

        public void ConvertToBinary(string HexWord)
        {
            string BinaryWord = "";

            for (int i = 0; i < HexWord.Length; i++)
            {   
                string ciag = Convert.ToString(HexValue(HexWord[i]), 2);
                int Value = Convert.ToInt32(ciag);
                BinaryWord += Value.ToString("0000");
            }
            //Console.WriteLine(BinaryWord);

            int[] JedenWiersz = new int[128];
            for (int i = 0; i < BinaryWord.Length; i++)
            {
                if (BinaryWord[i]=='1')
                {
                    JedenWiersz[i] = Convert.ToInt32(BinaryWord[i]) - 48;
                    IleJedynek++;
                }

            }

            Wiersze.Add(JedenWiersz);

        }

        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            HardDisk hard = new HardDisk();
            hard.MakeOperations2();
            hard.CountGroups();
        }
    }
}
