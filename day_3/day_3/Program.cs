using System;
namespace day_3
{
    class Square 

    {
        //        //task1
        //        You come across an experimental new kind of memory stored on an infinite two-dimensional grid.

        //Each square on the grid is allocated in a spiral pattern starting at a location marked 1 and then counting up while spiraling outward. For example, the first few squares are allocated like this:

        //17  16  15  14  13
        //18   5   4   3  12
        //19   6   1   2  11
        //20   7   8   9  10
        //21  22  23---> ...

        //While this is very space-efficient (no squares are skipped), requested data must be carried back to square 1 (the location of the only access port for this memory system) by programs that can only move up, down, left, or right.They always take the shortest path: the Manhattan Distance between the location of the data and square 1.

        //For example:

        //    Data from square 1 is carried 0 steps, since it's at the access port.
        //    Data from square 12 is carried 3 steps, such as: down, left, left.
        //    Data from square 23 is carried only 2 steps: up twice.
        //    Data from square 1024 must be carried 31 steps.

        //How many steps are required to carry the data from the square identified in your puzzle input all the way to the access port?

        //int SquareSize = 0;//rozmiar

        //task 2
        //        As a stress test on the system, the programs here clear the grid and then store the value 1 in square 1. Then, in the same allocation order as shown above, they store the sum of the values in all adjacent squares, including diagonals.

        //So, the first few squares' values are chosen as follows:

        //    Square 1 starts with the value 1.
        //    Square 2 has only one adjacent filled square (with value 1), so it also stores 1.
        //    Square 3 has both of the above squares as neighbors and stores the sum of their values, 2.
        //    Square 4 has all three of the aforementioned squares as neighbors and stores the sum of their values, 4.
        //    Square 5 only has the first and fourth squares as neighbors, so it gets the value 5.

        //Once a square is written, its value does not change.Therefore, the first few squares would receive the following values:

        //147  142  133  122   59
        //304    5    4    2   57
        //330   10    1    1   54
        //351   11   23   25   26
        //362  747  806--->   ...

        //What is the first value written that is larger than your puzzle input?


        int PuzzleInput = 361527;
        //int PuzzleInput = 277678; Damiana

        public int SizeOfSquare() //metoda do policzenia rozmiaru kwadratu
        {
            int SquareSize = 1; //wymiar kwadratu
            int Number = 1;
            while (Number < PuzzleInput)
            {
                SquareSize = SquareSize + 2;
                Number = SquareSize * SquareSize;
                //Console.WriteLine(SquareSize + "   " + Number);
            }
            
            return SquareSize;
        }

        public void Distance(int indeksX, int indeksY, int squareSize)
        {
            int distance = Math.Abs(indeksX-squareSize/2)+Math.Abs(indeksY-squareSize/2);
            Console.WriteLine("Odpwoiedź do zadania 1: " +distance + " kroków.");
        }

        public int SumOfSquare(int[,] squareWithNumbers, int indeksX, int indeksY)
        {
            int Number = squareWithNumbers[indeksX + 1, indeksY - 1] + squareWithNumbers[indeksX + 1, indeksY] + squareWithNumbers[indeksX + 1, indeksY+1];
            Number = Number + squareWithNumbers[indeksX, indeksY - 1] + squareWithNumbers[indeksX, indeksY + 1];
            Number=Number + squareWithNumbers[indeksX - 1, indeksY - 1] + squareWithNumbers[indeksX - 1, indeksY] + squareWithNumbers[indeksX - 1, indeksY + 1];
            return Number;
        }

        public void CreateTable1(int squareSize)
        {
            //Console.WriteLine(squareSize);

            int[,] SquareWithNumbers;
            SquareWithNumbers = new int[squareSize, squareSize];         //tworzenie tabeli

            int CurrentNumber = 1, step = 1; //deklaracja warunków początkowych
            int IndeksX = (squareSize-1) / 2, IndeksY = (squareSize-1) / 2; //deklaracja punktu poczatkowego
            
            SquareWithNumbers[IndeksX, IndeksY] = CurrentNumber;        //przypisanie aktualnej wartości
            
            //petla wpisujaca numery do tabeli
            while (CurrentNumber<PuzzleInput)
            {
                //right
                if (CurrentNumber <= PuzzleInput)
                    for (int i = 0; i < step; i++)
                    {                        
                        CurrentNumber++;
                        if (CurrentNumber > PuzzleInput) break;

                        IndeksY++;
                        //Console.WriteLine(CurrentNumber);
                        SquareWithNumbers[IndeksX, IndeksY] = CurrentNumber;
                        
                        //Console.WriteLine("X" + IndeksX + "Y" + " " + IndeksY);
                    }
                else break;

                //up
                if (CurrentNumber <= PuzzleInput)
                {
                    for (int i = 0; i < step; i++)
                    {                        
                        CurrentNumber++;                        
                        if (CurrentNumber > PuzzleInput) break;

                        IndeksX--;
                        //Console.WriteLine("X"+IndeksX +"Y" + " " + IndeksY);
                        SquareWithNumbers[IndeksX, IndeksY] = CurrentNumber;


                    }
                    step++;
                }
                else break;
                
                //left
                if (CurrentNumber <= PuzzleInput)
                    for (int i = 0; i < step; i++)
                    {                        
                        CurrentNumber++;                        
                        if (CurrentNumber > PuzzleInput) break;

                        IndeksY--;
                        //Console.WriteLine("X" + IndeksX + "Y" + " " + IndeksY);
                        SquareWithNumbers[IndeksX, IndeksY] = CurrentNumber;

                    }
                else break;

                if (CurrentNumber <= PuzzleInput)
                //down
                {
                    for (int i = 0; i < step; i++)
                    {                        
                        CurrentNumber++;
                        if (CurrentNumber > PuzzleInput) break;

                        IndeksX++;
                        //Console.WriteLine("X" + IndeksX + "Y" + " " + IndeksY);
                        SquareWithNumbers[IndeksX, IndeksY] = CurrentNumber;
                        //Console.WriteLine(CurrentNumber);
                    }
                    step++;
                }
                else break;

                
            }

            //DisplayTable(SquareWithNumbers, squareSize);
            Distance(IndeksX, IndeksY, squareSize - 1);            
        }

        public void CreateTable2(int squareSize)
        {
            //Console.WriteLine(squareSize);

            int[,] SquareWithNumbers;
            SquareWithNumbers = new int[squareSize, squareSize];         //tworzenie tabeli

            int CurrentNumber = 1, step = 1; //deklaracja warunków początkowych
            int IndeksX = (squareSize - 1) / 2, IndeksY = (squareSize - 1) / 2; //deklaracja punktu poczatkowego

            SquareWithNumbers[IndeksX, IndeksY] = CurrentNumber;        //przypisanie aktualnej wartości
            //Console.WriteLine("X" + IndeksX + "Y" + " " + IndeksY + " wartosc pocz");

            //petla wpisujaca numery do tabeli
            while (CurrentNumber < PuzzleInput)
            {
                //right
                if (CurrentNumber < PuzzleInput)
                    for (int i = 0; i < step; i++)
                    {     
                        IndeksY++;
                        CurrentNumber = SumOfSquare(SquareWithNumbers, IndeksX, IndeksY);  
                        SquareWithNumbers[IndeksX, IndeksY] = CurrentNumber;

                        //Console.WriteLine("X" + IndeksX + "Y" + " " + IndeksY);
                        if (CurrentNumber > PuzzleInput) break;
                    }
                else break;

                //up
                if (CurrentNumber < PuzzleInput)
                {
                    for (int i = 0; i < step; i++)
                    {
                        IndeksX--;
                        CurrentNumber = SumOfSquare(SquareWithNumbers, IndeksX, IndeksY);
                        SquareWithNumbers[IndeksX, IndeksY] = CurrentNumber;
                        
                       // Console.WriteLine("X" + IndeksX + "Y" + " " + IndeksY);
                        if (CurrentNumber > PuzzleInput) break;
                    }
                    step++;
                }
                else break;

                //left
                if (CurrentNumber < PuzzleInput)
                    for (int i = 0; i < step; i++)
                    {
                        IndeksY--;
                        CurrentNumber = SumOfSquare(SquareWithNumbers, IndeksX, IndeksY);
                        SquareWithNumbers[IndeksX, IndeksY] = CurrentNumber;

                        //Console.WriteLine("X" + IndeksX + "Y" + " " + IndeksY);
                        if (CurrentNumber > PuzzleInput) break;
                    }
                else break;

                if (CurrentNumber < PuzzleInput)
                //down
                {
                    for (int i = 0; i < step; i++)
                    {
                        IndeksX++;
                        CurrentNumber = SumOfSquare(SquareWithNumbers, IndeksX, IndeksY);
                        SquareWithNumbers[IndeksX, IndeksY] = CurrentNumber;

                        //Console.WriteLine("X" + IndeksX + "Y" + " " + IndeksY);
                        if (CurrentNumber > PuzzleInput) break;
                    }
                    step++;
                }
                else break;


            }
            Console.WriteLine("Odpowiedź do zadania 2: " + CurrentNumber + ".\n");
            Console.WriteLine("Tabela z wynikami dla zadania 2:  ");
            DisplayTable(SquareWithNumbers, squareSize);
            
            //Distance(IndeksX, IndeksY, squareSize - 1);
        }

        public void DisplayTable(int[,] squareWithNumbers, int squareSize)
        {
            string table="";
            for (int i = 0; i < squareSize; i++)
            {
                for (int j = 0; j < squareSize; j++)
                {
                    table = table + squareWithNumbers[i, j] + "\t";
                }
                table = table + "\n";
            }

            Console.WriteLine(table);
        }

    }

    class Program
    {
        

        static void Main(string[] args)
        {
            Square square = new Square();

            int Size = square.SizeOfSquare();
            square.CreateTable1(Size);

            square.CreateTable2(11); //12 przyjęta trochę z dupy

        }
    }
}
