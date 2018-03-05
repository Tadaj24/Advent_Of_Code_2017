using System;
using System.Collections.Generic;
using System.IO;

namespace day_7
{
    class Disk
    {
        public string Name;
        public int Weight;
        public string[] Slaves;
    }
    class Tower
    {
        List<string[]> lista = new List<string[]>();
        List<string[]> lista1 = new List<string[]>();
        List<string[]> lista2 = new List<string[]>();
        int p = 0,p1=0,p2=0;

        public void FileOpen()
        {
            try
            {
                using (StreamReader sr = new StreamReader("E:\\Nauka\\Kurs C#\\Advent of Code 2017\\Puzzle\\day_7test.txt"))
                {
                    while (sr.EndOfStream == false)
                    {
                        string Line = sr.ReadLine();
                        SplitLine(Line);
                        p++;
                    }
                    CreateTable1();
                    CreateTable2();
                    SprawdzCzyWazaTyleSamo();
                    
                    Console.WriteLine("razem:"+p+"\tMaster:"+p1+"\tSlave:"+p2+(p1+p2));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);                
            }
        }

        public void SplitLine(string line)
        {
            String[] Foo = line.Split(new char[] { ' ' });
            int len;
            if (Foo.Length < 3)
            {
                len = Foo.Length + 3;
            }
            else len = Foo.Length + 2;
            string[] tablica = new string[len];
            // 0-nazwa 1-waga 2-waga calosci 3-czy_jest_pelny 4-Czy jest masterem 5-lista slavów
            //Console.WriteLine("kurwa");
            tablica[0] = Foo[0];
            tablica[1] = Foo[1].Trim(new char[] { '(', ')' });
            tablica[2] = "brak";
            
            if (Foo.Length>2) //jest masterem
            {
                tablica[3] = "false";
                tablica[4] = "true";
                for (int i = 3; i < Foo.Length; i++)
                {
                    tablica[i+2]=Foo[i].TrimEnd(new char[] {  ',' });
                }
            }
            else //nie ma zadnych dyskow nad soba
            {
                tablica[3] = "true";
                tablica[4] = "false";
            }
            string slowo = "";

            for (int i = 0; i < tablica.Length; i++)
            {
                slowo += tablica[i] + "\t";
            }

            Console.WriteLine(slowo);
            lista.Add(tablica);
        }

        public void SprawdzanieWagi(List<string[]> pomocnicza, string Waga)
        {
            int waga = Convert.ToInt32(Waga);
            Console.WriteLine(waga);
            foreach(string[] item in pomocnicza)
            {
                waga += Convert.ToInt32(item[1]);
                Console.WriteLine(waga);
                
            }
        }

        public void SprawdzCzyWazaTyleSamo()
        {
            List<int> pomiary = new List<int>();
            List<string[]> pomocniczaLista = new List<string[]>();
            bool DidHeFind = false;
            int z = 0;
            while (DidHeFind == false && z < 2)
            {
                foreach (string[] tablica in lista) //sprawdzapokolei wszystkie elementy
                {
                    if (tablica[3] == "false") //jeśli nie jest obliczona
                    {
                        int k = 0; 
                        for (int i = 5; i < tablica.Length; i++) //iteruje po slavach
                        {
                            string nazwa = tablica[i]; //nazwa szukanego slava

                            foreach (string[] item in lista) //pobiera każdy element znowu i porownuje nazwy
                            {
                                //Console.WriteLine(item[k]);
                                if (nazwa == item[0] && item[3] == "true")
                                {
                                    pomocniczaLista.Add(item);
                                    break;
                                }
                            }
                        }

                        if (k == tablica.Length - 5)
                        {
                            SprawdzanieWagi(pomocniczaLista,tablica[1]);     
                        }                        
                        tablica[3] = "true";
                        tablica[4] = "false";
                        pomiary.Clear();
                        z++;

                    }

                }
            }

            foreach (string[] item in lista)
            {
                Console.WriteLine(item[0] + " " + item[1]);
            }
        }
        //lista z dyskami zawieracymi inne dyski na sobie
        public void CreateTable1() 
        {
            foreach (string[] item in lista)
            {
                if (item.Length > 5)
                {
                    lista1.Add(item);
                    p1++;
                }                
            }

            bool DidHeFind = false,x=false;
            int indeks = 0;
            while (DidHeFind == false)
            {
                string[] linijka1 = lista1[indeks];
                string word1 = linijka1[0];
                //Console.WriteLine(word1);
                foreach (var item in lista1)
                {
                    for (int i = 5; i < item.Length; i++)
                    {
                        if (word1 == item[i])
                        {
                            x = true;
                            //Console.WriteLine(word1);
                            break;
                        }
                    }
                    if (x == true)
                    {
                        break;
                    }

                }

                if (x == false)
                {
                    Console.WriteLine("Głowyny dysk: " + word1);
                    DidHeFind = true;
                    break;
                }
                indeks++;
                x = false;
            }

        }

        public void CreateTable2()
        {
            foreach (string[] item in lista)
            {
                if (item.Length < 6)
                {
                    lista1.Add(item);
                    p2++;
                }
            }
        }

        
    }



    class Program
    {
        static void Main(string[] args)
        {
            Tower tower = new Tower();
            tower.FileOpen();
        }
    }
}
