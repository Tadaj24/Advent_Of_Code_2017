using System;
using System.IO;

namespace day_9
{
    class StreamProcessing
    {

        public string Line;
        public void FileOpen()
        {
            try
            {
                using (StreamReader sr = new StreamReader("E:\\Nauka\\Kurs C#\\Advent of Code 2017\\Puzzle\\day_9.txt"))
                {
                    Console.WriteLine("Udało mi się otworzyć plik.");
                    while (sr.EndOfStream == false)
                    {
                         Line = sr.ReadToEnd();
                    }
                    //Console.WriteLine(Line + "\n Liczba znakow: " + Line.Length);
                    AnalizeStream();
                    //AnalizeStream();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //metoda usuwajaca wykrzykniki
        public string TableWithoutWykrzyknik(string line)
        {
            string NewLine = "";
            int LineLength = line.Length;
            string ActualCharacter = "";
            bool ItIsGarbage = false; //czy są to śmieci
            int k = 0;

            for (int index = 0; index < LineLength; index++)
            {
                ActualCharacter = Convert.ToString(line[index]); //pobranie aktualnej wartosci litery

                switch (ActualCharacter)
                {

                    case "<":
                        {
                            ItIsGarbage = true;
                            NewLine += ActualCharacter;
                            break;
                        }

                    case "!":
                        {
                            k++;
                            if (ItIsGarbage == true)
                            {
                                index++;
                            }
                            break;
                        }
                    case ">":
                        {
                            ItIsGarbage = false;
                            NewLine += ActualCharacter;
                            break;
                        }

                    default:
                        {
                            NewLine += ActualCharacter;
                            break;
                        }
                }
            }
            //Console.WriteLine(NewLine + "\n Liczba znakow: " + NewLine.Length);
            //Console.WriteLine(k);
            return NewLine;
        }

        //metoda usuwajaca garbage
        public string TableWithoutGarbage(string line)
        {
            int NumberOfGarbage=0;
            string NewLine = "";
            int LineLength = line.Length;
            string ActualCharacter = "";
            bool ItIsGarbage = false; //czy są to śmieci
            int k = 0;

            for (int index = 0; index < LineLength; index++)
            {
                ActualCharacter = Convert.ToString(line[index]); //pobranie aktualnej wartosci litery

                switch (ActualCharacter)
                {

                    case "<":
                        {
                            if (ItIsGarbage==true)
                            {
                                NumberOfGarbage++;
                            }
                            ItIsGarbage = true;
                            break;
                        }                       
                        
                    case ">":
                        {
                            ItIsGarbage = false;
                            break;
                        }

                    default:
                        {
                            if (ItIsGarbage == false)
                            {
                                NewLine += ActualCharacter;
                            }
                            else NumberOfGarbage++;
                            break;
                        }
                }
            }

            Console.WriteLine("Ilosc smieci: " + NumberOfGarbage);

            //Console.WriteLine(NewLine + "\n Dlugosc nowej lini: " + NewLine.Length);
            return NewLine;

        }

        //metoda obliczajaca wartosc grup
        public void ValueOfGroups(string line)
        {
            int IndexOfOpenedGroup = 0; //która zkolei grupa zostala otwarta
            int ValueOfGroups = 0; //aktualna wartosc grup
            string ActualCharacter = ""; //aktualna wartosc znaku
            int LineLength = line.Length;
            int IloscPrzecinkow = 0;
            int otwarcie = 0, zamkniecie = 0;

            for (int index = 0; index < LineLength; index++)
            {
                ActualCharacter = Convert.ToString(line[index]); //pobranie aktualnej wartosci litery

                switch (ActualCharacter)
                {
                    case "{":
                        {
                            otwarcie++;
                            IndexOfOpenedGroup++;
                            break;
                        }

                    case "}":
                        {
                            zamkniecie++;
                            ValueOfGroups += IndexOfOpenedGroup;
                            IndexOfOpenedGroup--;
                            break;
                        }

                    default:
                        {
                            IloscPrzecinkow++;
                            break;
                        }
                }

            }

            Console.WriteLine("Ilość przecinkow: " + IloscPrzecinkow + " Wartosc grup: " + ValueOfGroups);
            Console.WriteLine("Otwarcia: " + otwarcie + " Zamkniecia: " + zamkniecie);

        }

        public void AnalizeStream()
        {
            string NewLine = TableWithoutWykrzyknik(Line); //wyczyszczenie z wykrzykników
            NewLine=TableWithoutGarbage(NewLine);
            //Console.WriteLine(NewLine);
            ValueOfGroups(NewLine);


            //1 etapobrobki: usuniecie ! i znakow im odpowiadajacych
            


        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            StreamProcessing streamProcessing = new StreamProcessing();
            streamProcessing.FileOpen();
        }
    }
}
