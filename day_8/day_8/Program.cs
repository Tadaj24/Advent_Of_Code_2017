using System;
using System.Collections.Generic;
using System.IO;

namespace day_8
{
    class Zmienna
    {
        public string Name;
        public int Value;
    }

    class Registers
    {
        List<Zmienna> lista = new List<Zmienna>();
        int IloscWykoananychOperacji = 0;
        int MaksymalnaWartosc = 0;
        int MaksymalnaWartoscEver = 0;
        string NazwaMaksymalnejWartosci;

        string NazwaMaksymalnejWartosciEver;
        public void FileOpen()
        {
            
            try
            {
                using (StreamReader sr = new StreamReader("E:\\Nauka\\Kurs C#\\Advent of Code 2017\\Puzzle\\day_8.txt"))
                {
                    while (sr.EndOfStream == false)
                    {
                        string Line = sr.ReadLine();
                        string[] PodzielonaLinia=SplitLine(Line);
                        CreateObject(PodzielonaLinia);
                        AnalizeLine(PodzielonaLinia);
                    }

                    foreach (Zmienna item in lista)
                    {
                        if (item.Value > MaksymalnaWartosc)
                        {
                            MaksymalnaWartosc = item.Value;
                            NazwaMaksymalnejWartosci = item.Name;
                        }
                        Console.WriteLine(item.Name + " ma wartość " + item.Value);
                    }
                    Console.WriteLine("\nSa " + lista.Count + " zmienne w pliku");
                    Console.WriteLine("Wykonanych zostało " + IloscWykoananychOperacji + " operacji");
                    Console.WriteLine("Maksymalna wartosc zmiennej: " + NazwaMaksymalnejWartosci + " " + MaksymalnaWartosc  );

                    Console.WriteLine("Maksymalna wartosc zmiennej ever: " + NazwaMaksymalnejWartosciEver + " " + MaksymalnaWartoscEver);
                    //onsole.WriteLine("razem:" + p + "\tMaster:" + p1 + "\tSlave:" + p2 + (p1 + p2));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public string[] SplitLine(string line)
        {
            String[] Foo = line.Split(new char[] { ' ' });
            
            return Foo;
        }

        //tworzenie obiektu nowego jeśli nie istnieje
        public void CreateObject(string[] podzielonaLinia)
        {
            bool IsHeInTheList = false;
            foreach (Zmienna element in lista)
            {
                if (element.Name == podzielonaLinia[0] )
                {
                    IsHeInTheList = true;
                    break;
                }
            }

            if (IsHeInTheList==false)
            {
                Zmienna zmienna = new Zmienna();
                zmienna.Name = podzielonaLinia[0];
                zmienna.Value = 0;
                lista.Add(zmienna);
            }
        }

        //metoda poddająca obrobce cala operacje
        public void AnalizeLine(string[] podzielonaLinia)
        {
            // string znak, Operacja, ZmiennaPorownywana;
            //int WartoscPrzyrownania, WartoscInkrementacji;
            string NazwaZmiennaOperacyjna = podzielonaLinia[0]; //nazwa zmiennej opercyjnej
            string Operacja = podzielonaLinia[1]; // czy inc czy desc
            int WartoscInkrementacji = Convert.ToInt32(podzielonaLinia[2]); //o ile ma zwiekszyc/zmniejszyc dana zmienna
            string ZmiennaPorownywana = podzielonaLinia[4]; //zmienna która bezie sprawdzana pod katem czy jest warunek spelnipny
            string Znak = podzielonaLinia[5];   // znak operacji porownywania
            int WartoscPrzyrownania = Convert.ToInt32(podzielonaLinia[6]); //wartosc zmiennej proownywanej

            if (CheckRequirement(ZmiennaPorownywana, WartoscPrzyrownania, Znak) == true)
            {
                ChangeActualValue(NazwaZmiennaOperacyjna, Operacja, WartoscInkrementacji);
                IloscWykoananychOperacji++;
            }     
        }

        //zwraca aktualna wartosc podanej zmiennej
        public int FindActualValue(string name)
        {
            foreach (Zmienna element1 in lista)
            {
                if (name==element1.Name)
                {
                    return element1.Value;
                }
            }
            return 0;
        }

        //sprawdza czy warunek jest spełniony; zwraca prawde jeśli warunek jest prwadziwy
        public bool CheckRequirement(string zmiennaPorownywana, int wartoscPorownana, string znak) 
        {
            int ActualValue=FindActualValue(zmiennaPorownywana);

            switch (znak)
            {
                case "==":
                    {
                        if (ActualValue==wartoscPorownana)
                        {
                            return true;
                        }
                        break;
                    }

                case ">":
                    {
                        if (ActualValue > wartoscPorownana)
                        {
                            return true;
                        }
                        break;
                    }

                case "<":
                    {
                        if (ActualValue < wartoscPorownana)
                        {
                            return true;
                        }
                        break;
                    }

                case ">=":
                    {
                        if (ActualValue >= wartoscPorownana)
                        {
                            return true;
                        }
                        break;
                    }

                case "<=":
                    {
                        if (ActualValue <= wartoscPorownana)
                        {
                            return true;
                        }
                        break;
                    }
                case "!=":
                    {
                        if (ActualValue != wartoscPorownana)
                        {
                            return true;
                        }
                        break;
                    }

            }

            return false;
        }

        //metoda zmieniajaca aktualna wartosc zmiennej
        public void ChangeActualValue(string nazwaZmiennaOperacyjna, string operacja, int wartoscInkrementacji)
        {
            if (operacja == "inc")
            {
                foreach (Zmienna item in lista)
                {
                    if (nazwaZmiennaOperacyjna==item.Name)
                    {
                        item.Value += wartoscInkrementacji;
                        if (item.Value>MaksymalnaWartoscEver)
                        {
                            MaksymalnaWartoscEver = item.Value;
                            NazwaMaksymalnejWartosciEver = item.Name;
                        }
                        break;
                    }
                }
            }
            else if (operacja=="dec")
            {
                foreach (Zmienna item in lista)
                {
                    if (nazwaZmiennaOperacyjna == item.Name)
                    {
                        item.Value -= wartoscInkrementacji;
                        if ((item.Value) > (MaksymalnaWartoscEver))
                        {
                           MaksymalnaWartoscEver = item.Value;

                           NazwaMaksymalnejWartosciEver = item.Name;
                        }
                        break;
                    }
                }
            }
        }
        
}

    class Program
    {

        static void Main(string[] args)
        {
            Registers registers = new Registers();
            registers.FileOpen();
        }
    }
}
