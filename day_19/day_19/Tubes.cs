using System;
using System.Collections.Generic;
using System.IO;

namespace day_19
{
    class Tubes
    {
        public List<string> LineList = new List<string>();
        public string Word = "";
        public int X=0, Y=0; //aktualna wspolrzedna
        public int PreviousDirection = 2;
        public int Steps = 1;

        public int HorizontalDirection = 0; //right 1, left -1
        public int VerticalDirection = 1; //down 1, up -1

        public void FileOpen()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"E:\Nauka\Kurs C#\Advent of Code 2017\Puzzle\day_19.txt"))
                {
                    while (sr.EndOfStream == false)
                    {
                        string Line = sr.ReadLine();
                        LineList.Add(Line);
                        Console.WriteLine(Line.Length + "\t" + Line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void MakeOperations1()
        {
            FileOpen();
            FindStart();
           

            //Console.WriteLine(X + " " + Y);

            while (1<2)
            {
                //bool End = false;
                int xx = X + VerticalDirection;
                int yy = Y + HorizontalDirection;
                
                
                if (LineList[xx][yy]=='+' ) //sprawdza czy nastepny znak nie jest plusem
                {
                    ChangeCordinates();
                    if (FindDirection()==false)
                    {
                        break;
                    }
                    //FindDirection();
                }
                else
                {
                    ChangeCordinates();
                }
               // Console.WriteLine(X+" " + Y);
                GetLetter();
                Steps++;
                if (FindEnd() == false)
                {
                    break;
                }
            }
            Console.WriteLine(Word);
            Console.WriteLine("Ilosc krokow: " + Steps);
        }

        public void FindStart()
        {
            for (int i = 0; i < LineList[0].Length; i++)
            {
                if (LineList[0][i]=='|')
                {
                    Y = i;
                    break;
                }
            }
        }
        public void ChangeCordinates()//przemieszcza nas
        {
            Y = Y + HorizontalDirection;
            X = X + VerticalDirection;
        }
        public bool FindEnd()
        {
            //prawo
            int x = X, y = Y + 1;
            if (y < LineList[0].Length && PreviousDirection != 1)
            {
                //Console.WriteLine(x+" " + y);
                if (LineList[x][y] != ' ')
                {
                    return true;
                }
            }

            //lewo
            x = X; y = Y - 1;
            if (y >= 0)
            {
                if (LineList[x][y] != ' ' && PreviousDirection != 3)
                {
                    return true;
                }
            }

            //dol
            x = X + 1; y = Y;
            if (x < LineList.Count)
            {
                if (LineList[x][y] != ' ' && PreviousDirection != 2)
                {
                    return true;
                }
            }

            //gora
            x = X - 1; y = Y;
            if (x >= 0)
            {
                if (LineList[x][y] != ' ' && PreviousDirection != 0)
                {
                    return true;
                }
            }

            return false;
        }
        public bool FindDirection()
        {
            //prawo
            int x = X, y = Y + 1;
            VerticalDirection = 0;
            HorizontalDirection = 0;
            if (y < LineList[0].Length && PreviousDirection!=1)
            {
                //Console.WriteLine(x+" " + y);
                if (LineList[x][y] != ' ')
                {
                    HorizontalDirection = 1;
                    PreviousDirection = 3;
                    return true;
                }
            }

            //lewo
            x = X; y = Y - 1;
            if (y >= 0)
            {
                if (LineList[x][y] != ' ' && PreviousDirection!=3)
                {
                    HorizontalDirection = -1;

                    PreviousDirection = 1;
                    return true;
                }
            }

            //dol
            x = X + 1; y = Y;
            if (x < LineList.Count)
            {
                if (LineList[x][y] != ' ' && PreviousDirection!=2)
                {
                    VerticalDirection = 1;

                    PreviousDirection = 0;
                    return true;
                }
            }

            //gora
            x = X -1; y = Y;
            if (x >=0)
            {
                if (LineList[x][y] != ' ' && PreviousDirection!=0)
                {
                    VerticalDirection = -1;

                    PreviousDirection = 2;
                    return true;
                }
            }

            return false;
        }

        public void GetLetter()
        {
            int CharValue = Convert.ToInt16(LineList[X][Y]);

            if (CharValue>64 && CharValue <123) //to znaczy że jest to litera
            {
                Word += Convert.ToString(LineList[X][Y]);
            }
        }
    }
}
