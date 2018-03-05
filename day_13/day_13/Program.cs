using System;
using System.Collections.Generic;
using System.IO;

namespace day_13
{

    class Layer
    {
        public int LayerDepth = 0; //szerokosc warstwy 1000-deklarujemy tyle by nie bylo szns ze wroci
        public int ActualPosition = -1; // 
        public int Direction = 1; //1 znaczy ze dodajemy indeks a - ze odejmujemy
        public bool DidHeCatch = false;



        public void Move() //rozkazuje sie przesunąc 
        {

            //Console.WriteLine(ActualPosition);

            if (LayerDepth > 0) //jesli jest wieksze od 0 to zanczy ze ta wartswa istnieje
            {
                if (LayerDepth == 1)
                {
                    Direction = 0;
                }
                else if (ActualPosition + Direction >= LayerDepth) //
                {
                    Direction = -1;
                }
                else if (ActualPosition + Direction < 0) //ponizej zera
                {
                    Direction = 1;
                }

                ActualPosition += Direction;
            }
        }
    }

    class Firewalll
    {
        public List<Layer> Lista = new List<Layer>();
        public List<Layer> ListaBazwoa = new List<Layer>();
        public List<Layer> ListaAktualna = new List<Layer>();
        public int Severity = 0;

        public void FileOpen() //otwarcie pliku
        {
            try
            {
                int NumberElements = 0;
                string Path = @"E:\Nauka\Kurs C#\Advent of Code 2017\Puzzle\day_13.txt";
                using (StreamReader sr = new StreamReader(Path))
                {
                    while (sr.EndOfStream == false)
                    {
                        string Line = sr.ReadLine();
                        string[] Podzielona = SplitLine(Line);
                        NumberElements = Convert.ToInt32(Podzielona[0]);
                    }
                }

                AddEmptyElements(NumberElements + 1);
                //Console.WriteLine(Lista.Count);
                using (StreamReader sr = new StreamReader(Path)) //modyfikuje glebokosci
                {
                    while (sr.EndOfStream == false)
                    {
                        string Line = sr.ReadLine();
                        ChangeList(SplitLine(Line));
                    }
                }


                Console.WriteLine("Bazowa konfiguracja: ");
                EachElement(0);
                // Console.WriteLine();
                //GoThrowThePath1();
                //Console.WriteLine();
                //Console.WriteLine("Severity: " + Severity);
                Console.WriteLine();
                GoThroughThePath2();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ChangeList(string[] foo) //modyfikuje liste przy pomocy wartosci z pliku
        {
            int indeks = Convert.ToInt32(foo[0]);
            int value = Convert.ToInt32(foo[1]);

            Lista[indeks].LayerDepth = value;
            Lista[indeks].ActualPosition = 0;//przypisuje danej wartswie jej glebokosc

            ListaAktualna[indeks].LayerDepth = value;
            ListaAktualna[indeks].ActualPosition = 0;//przypisuje danej wartswie jej glebokosc

            ListaBazwoa[indeks].LayerDepth = value;
            ListaBazwoa[indeks].ActualPosition = 0;//przypisuje danej wartswie jej glebokosc
        }
        public string[] SplitLine(string line) //dzieli linie
        {
            string[] Foo = line.Split(new char[] { ' ' });

            Foo[0] = Foo[0].TrimEnd(new char[] { ':' });
            return Foo;
        }
        public void AddEmptyElements(int numberOfLayers) //tworzy  pusta liste
        {
            for (int i = 0; i < numberOfLayers; i++)
            {
                Layer layer = new Layer();
                Lista.Add(layer);
                Layer layer1 = new Layer();
                ListaBazwoa.Add(layer1);
                Layer layer2 = new Layer();
                ListaAktualna.Add(layer2);
            }
        }
        public void EachElement(int akctualPosition) //metoda np do wypisywania
        {
            string word = "";
            foreach (var item in Lista)
            {
                word += item.ActualPosition + " ";
            }
            Console.WriteLine("Aktualna pozycja: " + akctualPosition + " " + "Ustawienie firewalla: " + word);
        }

        public void RestoreBaseList(List<Layer> rodzajListy) //przywraca liste bazowa
        {
            Lista.Clear();
            foreach (var item in rodzajListy)
            {
                Layer layer = new Layer();
                layer.ActualPosition = item.ActualPosition;
                layer.LayerDepth = item.LayerDepth;
                layer.DidHeCatch = item.DidHeCatch;
                layer.Direction = item.Direction;

                Lista.Add(layer);
                //Console.WriteLine(item.ActualPosition);
            }

            //foreach (var item in Lista)
            //{
            //    if (item.LayerDepth > 0)
            //    {
            //        item.ActualPosition = 0;
            //    }
            //}
        }

        public bool CheckSituation(int actualPosition) //sprawdza czy złapał
        {
            if (Lista[actualPosition].ActualPosition == 0)
            {
                Lista[actualPosition].DidHeCatch = true;
                Severity += actualPosition * Lista[actualPosition].LayerDepth;
                //Console.WriteLine(actualPosition);
                return true;
            }
            return false;
        }
        public void MakeMove(List<Layer> lista)
        {
            foreach (var item in lista)
            {
                item.Move();
            }
        }
        public void GoThrowThePath1() //zadanie 1
        {
            int ActualPosition = 0;

            for (int i = 0; i < Lista.Count; i++)
            {
                CheckSituation(ActualPosition); //sprawdza poczatkowa sytuacje; tak jkaby skoczyl do zera
                MakeMove(Lista); //rusza się
                ActualPosition++;
                EachElement(ActualPosition);
            }
        }

        public void GoThroughThePath2()
        {
            int Delay = 0;
            bool DidHeDidIt = false; //czy udalo mu sie przejsc niezauwazonym
            while (DidHeDidIt == false)
            {
                DidHeDidIt = true; //zakladamy zemu sie udalo

                RestoreBaseList(ListaAktualna); //przywracam aktualna liste

                int ActualPosition = 0;
                for (int i = 0; i < Lista.Count; i++)
                {
                    if (CheckSituation(ActualPosition) == true)
                    {
                        DidHeDidIt = false;
                        break;
                    }
                    MakeMove(Lista); //rusza się
                    ActualPosition++;
                    //EachElement(ActualPosition);
                }

                MakeMove(ListaAktualna);
                

                //EachElement(0);
                Delay++;
                if (Delay % 10000 == 0)
                {
                    Console.WriteLine(Delay);
                }
            }
            Console.WriteLine("Porządany delay: " + (Delay - 1));

        }
    }
    class Program
    {


        static void Main(string[] args)
        {
            Firewalll firewalll = new Firewalll();
            firewalll.FileOpen();


        }
    }
}
