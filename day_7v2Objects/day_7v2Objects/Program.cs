using System;
using System.Collections.Generic;
using System.IO;

namespace day_7v2Objects
{

    class Tower
    {
        List<object> lista = new List<object>();
        List<object> listaBazowa = new List<object>();
        //List<object> lista2 = new List<object>();
        public bool DidHeFindResult = false;
        int p = 0, p1 = 0, p2 = 0;
        public int OperationCounter = 0;

        public void FileOpen() //otwiera plik dodaje obiekty do listy
        {
            try
            {
                using (StreamReader sr = new StreamReader("E:\\Nauka\\Kurs C#\\Advent of Code 2017\\Puzzle\\day_7.txt"))
                {
                    int l = 0;
                    while (sr.EndOfStream == false)
                    {
                        string Line = sr.ReadLine();
                        CreateObject(Line); //tworzy nowe obiekty
                    }
                    AddBaseSlaveWeigth();
                    listaBazowa = lista;

                    int j = 1;
                    ShowObjects();
                    //Console.WriteLine(p1);
                    Console.WriteLine();
                    while (j<10)      
                    {
                        Console.WriteLine("Numer iteracji: " + j);
                        AddSlavesDoLObjectList();
                        ValidationCheck();
                        ShowObjects();
                        Console.WriteLine("Ilosc dodanych slavow: " + OperationCounter);
                        Console.WriteLine();
                        j++;
                    }
                    
                    ValidationCheck();

                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



        //wypisywanie wszystkich obiektow
        public void ShowObjects()
        {
           // Console.WriteLine();
            int p = 0;
            foreach (Disk item in lista)
            {

                if (item.IsHeMaster == true)
                {
                    //Console.WriteLine("Nazwa: " + item.Name + " Masa: " + item.Weight + " Czy jest masterem: " + item.IsHeMaster);
                    p++;
                }
                //Console.WriteLine("Nazwa: " + item.Name + " Masa: " + item.Weight + " Czy jest masterem: " + item.IsHeMaster);

            }
            Console.WriteLine("Jest masterow: " + p);
            //Console.WriteLine();
        }

        //dodaje obiekty do listy 
        public void CreateObject(string line)
        {
            String[] Foo = line.Split(new char[] { ' ' }); //podzielenie lini na tablicę stringów

            Disk disk = new Disk(); //utworzenie obiektu dysk
            disk.Name = Foo[0]; //wyciagniecie nazwy
            disk.Weight = Convert.ToInt32(Foo[1].Trim(new char[] { '(', ')' })); //wyciągniecie jego masy
            
            if (Foo.Length > 2) //sprawdzenie czy ma slavy
            {
                disk.CanBeCompare = false; //nie może być bo ma nad sobą kogoś
                disk.IsHeMaster = true; 
                for (int i = 3; i < Foo.Length; i++)
                {
                    disk.SlaveName.Add(Foo[i].TrimEnd(new char[] { ',' })); //dodanie nazwy slave do listy
                    disk.SlaveWeigth.Add(0);
                    p1++;
                }
            }
            else
            {
                disk.CanBeCompare = true;
                disk.IsHeMaster = false;
                p2++;
            }

            lista.Add(disk);  //dodaje obiekt do listy
        }

        public void AddBaseSlaveWeigth()
        {
            foreach (Disk item in lista)
            {
                foreach (string nazwa in item.SlaveName)
                {
                    foreach (Disk porownywana in lista)
                    {
                        if(nazwa == porownywana.Name)
                        {
                            item.SlaveBazowa.Add(porownywana.Weight);
                            break;
                        }
                    }
                }
            }
        }
        //dodaje slavy do listy dyskow
        public void AddSlavesDoLObjectList()
        {
            OperationCounter = 0;
            foreach (Disk ObiektGlowny in lista)
            {
                if (ObiektGlowny.IsHeMaster == true) //jeśli ma nad sobą jakieś dyski
                {
                    int m = 0;
                    foreach (string NazwaSlave in ObiektGlowny.SlaveName) //foreach iterujacy po kazdym slavename
                    {
                        //Console.WriteLine("Petla po nazwach slava");
                        if (ObiektGlowny.SlaveWeigth[m]==0) // to zanczy ze nie zostala dodana jeszcze masa
                        {
                            foreach (Disk ObiektPorownywany in lista)
                            {
                                //Console.WriteLine("x");
                                if (ObiektPorownywany.IsHeMaster == false && NazwaSlave == ObiektPorownywany.Name) //szuka takiej samej nazwy i sprawdz czy jest nie jest masterem
                                {
                                    //Console.WriteLine("Kurwa");
                                    ObiektGlowny.SlaveWeigth[m]=(ObiektPorownywany.Weight);
                                    OperationCounter++;
                                    break;
                                }
                            }
                        }
                        m++;
                        
                    }
                }
                
            }
            
        }

        //sprawdzaczy wagi sa rowne i oblicza ich amsę
        public void ValidationCheck()
        {
            foreach (Disk ObiektGlowny in lista)
            {
                ObiektGlowny.SumWeigths();
            }
        }
    }
        
        class Disk
    {
        public int Location; //lokacja danego obietku na liście
        public string Name; //nazwa dysku
        public int Weight; //masa dysku
        public bool IsHeMaster; //sprawdza czy ma nad sobą cokolwiek
        public bool CanBeCompare; //zmienna mowiaca o tym czy dany element ma nad soba kompletne dyski
        public int WagaSrednia = 0;

        //public bool CzyMajaBycBadaneSlave;
        public bool DidHeCorrect=true; //zmienna mowica o tym czy dany dysk jest spooko 

        public List<string> SlaveName = new List<string>(); //
        public List<int> SlaveWeigth = new List<int>(); //
        public List<int> SlaveBazowa = new List<int>(); //

        //metoda sprawdzajaca czy wszystkie dyski maja taką samą wage
        

        public bool TheyAreSameWeights()
        {
            bool Kurwa = true;
            foreach (int item in SlaveWeigth)
            {
                if (item == 0)
                {
                    Kurwa = false;
                    break;
                }
            }

            if (Kurwa==true ) //sprawdza czy wszystkie slavy maja wpisaną mase do zmiennej
            {
                for (int z = 0; z < SlaveName.Count - 1; z++)
                {
                    if (SlaveWeigth[z] != SlaveWeigth[z + 1]) //jesli masy sa rozne wypisz wszystkie skladniki
                    {
                        Console.WriteLine("Zly dysk: " + Name);
                        int zz = 0;
                        DidHeCorrect = false;
                        foreach (string item66 in SlaveName)
                        {
                            Console.WriteLine(zz + ".Nazwa slava blednego:\t" + item66 + "\t" + SlaveWeigth[zz] + "\tWartość bazowa tego slave:\t" + SlaveBazowa[zz]); //wypisze wszystkie bledne dyski
                            WagaSrednia += SlaveWeigth[zz];
                            zz++;
                        }

                        WagaSrednia = WagaSrednia / zz;
                        Console.ReadKey();
                        Kurwa = false;
                        //Console.ReadKey();
                        return Kurwa;
                    }
                }
            }
            else
            {
                //Console.WriteLine();
                Kurwa = false;
               // Console.ReadKey();
                return Kurwa;
            }

            return true;
        }

        public void SumWeigths() //najpierw oblicza 
        {
            if(TheyAreSameWeights()==true) //wtedy staje sie slavem a jego masa jest sumowana
            {
                foreach (int item12 in SlaveWeigth)
                {
                    Weight += item12;
                }
                CanBeCompare = true;
                IsHeMaster = false;
                SlaveName.Clear();
                //SlaveWeigth.Clear();
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
