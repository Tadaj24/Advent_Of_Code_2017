using System;
using System.Collections.Generic;
using System.IO;

namespace day_12
{
    class Pipe
    {
        public int PipeNumber; //numer rury
        public List<int> ConectedPipes = new List<int>(); //lista podlaczonych rur    
        public bool DidHeWasChecked = false; //sprawdza czy został już dodany
        
    }

    class PipesNet
    {
        public List<Pipe> PipesList = new List<Pipe>(); //tworzy liste rur
        public List<int> ActualPipes = new List<int>(); //tworzy listę rur tworzacych dana strukture
        public List<int> Pomocnicza = new List<int>();

        public int licznik = 0;
        public bool DidHeFindAll = false; //sprawdza czy znalazl jakąś nową rurę
        public int NumberOfGroups = 0;

        public void FileOpen()
        
        {

            try
            {
                using (StreamReader sr = new StreamReader("E:\\Nauka\\Kurs C#\\Advent of Code 2017\\Puzzle\\day_12.txt"))
                {
                    Console.WriteLine("Udalo się otworzyć plik");
                    while (sr.EndOfStream == false)
                    {
                        string Line = sr.ReadLine();
                        string[] PodzielonaLinia = SplitLine(Line);
                        CreateObject(PodzielonaLinia); //tworzy listę obiektów
                    }
                    
                    //MakeOperations();
                    //CountConectedPipes(0);
                    //Console.WriteLine(PipesConnectecTo0.Count);

                                       
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        } //otwiera plik
        public string[] SplitLine(string line) //dzieli linie na czesci
        {
            String[] Foo = line.Split(new char[] { ' ' });
            return Foo;
        }
        public void CreateObject(string[] podzielonaLinia)
        {
            Pipe NewPipe = new Pipe();
            NewPipe.PipeNumber = Convert.ToInt32(podzielonaLinia[0]);

            for (int i = 2; i < podzielonaLinia.Length; i++)
            {
                NewPipe.ConectedPipes.Add(Convert.ToInt32(podzielonaLinia[i].TrimEnd(new char[] { ',' })));
                //Console.WriteLine(podzielonaLinia[i].TrimEnd(new char[] { ',' }));
            }

            PipesList.Add(NewPipe);
            Console.WriteLine();
        } //tworzy obiekt

        public int GetNumberOfGroup()
        {
            int value = 0;
            foreach (var rura in PipesList)
            {
                if (rura.DidHeWasChecked == false)
                {
                    NumberOfGroups++;
                    return rura.PipeNumber;
                }
            }
            DidHeFindAll = true;
            return value;
        }

        public void MakeOperations()
        {
            FileOpen();

            do
            {
                CreateBaseList(GetNumberOfGroup());
                if (DidHeFindAll==true)
                {
                    break;
                }
                do
                {
                    AddToList();
                } while (licznik != 0);

                Console.WriteLine("Grupa " + (NumberOfGroups) + ". Liczba elementów w tablicy: " + ActualPipes.Count);
                WyswietlListe(ActualPipes);
            } while (DidHeFindAll == false);
            
        }       

        public void CreateBaseList(int numerTworzacej) //tworzy podstawową listę
        {
            ActualPipes.Clear(); //najpierw czyści listę
            foreach (var item in PipesList[numerTworzacej].ConectedPipes)
            {
                ActualPipes.Add(item);
            }            
        }

        public bool CheckInTheList(int numerSlave) //sprawdza czy dany element jest juz na liscie
        {
            foreach (var porownywanaRura in Pomocnicza)
            {
                if (porownywanaRura == numerSlave) //sprawdza czy jest już na liście
                {
                    return true; 
                }
            }
            return false;
        }

        public void WyswietlListe(List<int> lista)
        {
            string word = "";
            foreach (var item in lista)
            {
                word += item + " ";
            }
            Console.WriteLine(word);
        }

        public void AddToList() //metoda dodajaca wartosci do listy
        {
            licznik = 0; //mówi ile zostalo dodanych nowych rur do listy
            foreach (var item in ActualPipes)
            {
                Pomocnicza.Add(item);
            }

            //WyswietlListe(Pomocnicza);
            //WyswietlListe(ActualPipes);
            foreach (var numerRury in ActualPipes) //iteruje po obecnych rurach w liście
            {
                if (PipesList[numerRury].DidHeWasChecked==false)
                {
                    foreach (var numerSlave in PipesList[numerRury].ConectedPipes) //iteruje po każdej podlaczonej rurze
                    {
                        if (CheckInTheList(numerSlave) == false) //jeśli nie jest na liście
                        {
                            Pomocnicza.Add(numerSlave);
                            licznik++;

                        }
                    }
                    PipesList[numerRury].DidHeWasChecked = true;
                }                
            }

            ActualPipes.Clear();
            foreach (var item in Pomocnicza)
            {
                ActualPipes.Add(item);
            }
            Pomocnicza.Clear();
        }

    }

    

    class Program
    {
        static void Main(string[] args)
        {
            PipesNet pipesNet = new PipesNet();
            pipesNet.MakeOperations();
            //pipesNet.MakeOperations();
        }
    }
}
