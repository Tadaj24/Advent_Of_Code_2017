using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace day_18
{
    class Command
    {
        public string OperationType;
        public string VariableName;
        public string Value;

        public Command(string operationType, string variableName)
        {
            OperationType = operationType;
            VariableName = variableName;
        }
    }

    class Variable //classa zmiennych
    {
        public string Name;
        public long Value;
        public long LastSound = 0;
    }

    class Duet
    {
        public long LastFreqSound = 0;
        public int NumberOfSends = 0;
        public List<Command> CommandList = new List<Command>(); //lista komend
        public List<Variable> VariableList = new List<Variable>(); //lista komend
        public List<Variable> VariableList1 = new List<Variable>(); //lista komend
        public Queue<long> Program0List = new Queue<long>(); //lista wartosci wyslanych do programu 0
        public Queue<long> Program1List = new Queue<long>(); //lista wartosci wsyalnych do programu 1
        public int ActualProgram = 0; //zmienna przechowujaca aktualny program
        public int index0 = 0, index1 = 0;

        #region zadanie 1
        public void MakeOperations1()
        {
            FileOpen();
            AddVariables();
            DoCommands();        
        }

        public void FileOpen()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"E:\Nauka\Kurs C#\Advent of Code 2017\Puzzle\day_18.txt"))
                {
                    while (sr.EndOfStream == false)
                    {
                        string Line = sr.ReadLine();
                        SplitLine(Line);
                        //Console.WriteLine(Line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void SplitLine(string Line)
        {
            string[] Foo = Line.Split(new char[] { ' ' });
            
            Command command = new Command(Foo[0],Foo[1]);
            if (Foo.Length==3)
            {
                command.Value = Foo[2];
            }

            CommandList.Add(command);            

        }
        public void AddVariables()
        {
            foreach (var item in CommandList)
            {
                try
                {
                    int k = Convert.ToInt32(item.VariableName);
                    //Console.WriteLine(k);
                }
                catch (Exception)
                {
                    if (CheckVariableList(item.VariableName) == true)
                    {
                        Variable variable = new Variable();
                        variable.Name = item.VariableName;
                        variable.Value = 0;
                        Variable variable1 = new Variable();
                        variable1.Name = item.VariableName;
                        if (variable1.Name=="p")
                        {
                            variable1.Value = 1;
                        }
                        else
                        variable1.Value = 0;
                        VariableList.Add(variable);
                        VariableList1.Add(variable1);
                    }
                }
               
            }
            VariableList1[0].Value = 1;
            //Console.WriteLine(VariableList.Count);
        }
        public bool CheckVariableList(string name)
        {
            foreach (var item in VariableList)
            {
                if (item.Name==name)
                {
                    return false;
                }
            }

            return true;
        }
        public bool CheckVariableList1(string name)
        {
            foreach (var item in VariableList1)
            {
                if (item.Name == name)
                {
                    return false;
                }
            }

            return true;
        }

        //pierwsze
        public void DoCommands()
        {
            for (int i = 0; i < CommandList.Count; i++)
            {
                Command command = CommandList[i];
                Console.WriteLine(i+". "+CommandList[i].OperationType + "\t " + CommandList[i].VariableName + "\t" + CommandList[i].Value + "\t"+ GetValue(CommandList[i].VariableName));
                switch (command.OperationType)
                {
                    case "set":
                        {
                            SetOperation(command);
                            break;
                        }
                    case "add":
                        {
                            AddOperation(command);
                            break;
                        }
                    case "mul":
                        {
                            MulOperation(command);
                            break;
                        }
                    case "mod":
                        {
                            ModOperation(command);
                            break;
                        }
                    case "snd":
                        {
                            SndOperation(command);
                            break;
                        }
                    case "rcv":
                        {
                            //Console.WriteLine("sprawdzam");
                            if (RcvOperation(command)==true)
                            {                                
                               // Console.WriteLine(LastFreqSound);
                                //Console.ReadKey();
                                break;
                            }
                            break;
                        }
                    case "jgz":
                        {
                            i += Convert.ToInt32(JgzOperation(command));
                            //Console.WriteLine(JgzOperation(command));
                            break;
                        }
                }
                
                //Console.WriteLine(VariableList[0].Value);
            }
        }
        public void SetOperation(Command command)
        {
            long Value = GetValue(command.Value);
            foreach (var item in VariableList)
            {
                if (item.Name==command.VariableName)
                {
                    item.Value = Value;
                    break;
                }
            }
        }
        public void AddOperation(Command command)
        {
            long Value = GetValue(command.Value);
            foreach (var item in VariableList)
            {
                if (item.Name == command.VariableName)
                {
                    item.Value +=Value;
                    break;
                }
            }
        }
        public void MulOperation(Command command)
        {
            long Value = GetValue(command.Value);
            foreach (var item in VariableList)
            {
                if (item.Name == command.VariableName)
                {
                    item.Value = item.Value*Value;
                    break;
                }
            }
        }
        public void ModOperation(Command command)
        {
            long Value = GetValue(command.Value);
            foreach (var item in VariableList)
            {
                if (item.Name == command.VariableName)
                {
                    item.Value = item.Value % Value;
                    break;
                }
            }
        }
        public void SndOperation(Command command)
        {
            long Value = GetValue(command.VariableName);
            LastFreqSound = Value;
            Console.WriteLine("dzwiek: " + Value);
            foreach (var item in VariableList)
            {
                if (item.Name==command.VariableName)
                {
                    item.LastSound = Value;
                    break;
                }
            }
        }
        public bool RcvOperation(Command command)
        {
            long Value = GetValue(command.VariableName);
            //Console.WriteLine();
            if (Value!=0)
            {
                foreach (var item in VariableList) //iteruje po zmiennych, szuka odpowiedniej
                {
                    if (item.Name == command.VariableName)
                    {
                        item.Value = item.LastSound;
                        Console.WriteLine("Ostatnia czestotliowsc: " + LastFreqSound);
                        return true;
                    }
                }

            }
            return false;
        }
        public long JgzOperation(Command command)
        {
            long Value = GetValue(command.Value); //wartosc skoku
            long VariableValue = GetValue(command.VariableName); //wartosc zmiennej
            if (VariableValue > 0)
            {
                return Value-1;
            }
            return 0;
        }
        #endregion


        public void MakeOperations2()
        {
            FileOpen();
            AddVariables();               
            DoCommands2();           
        }
        public void DoCommands2()
        {
            //int k = 0;
            do
            {
                //Console.WriteLine(NumberOfSends);
                //Console.WriteLine(Program0List.Count + "\t" + Program1List.Count);
                //Console.WriteLine("aaa");
                if (ActualProgram == 0)
                {
                    //Console.WriteLine("Teraz 1");
                    Operation0();
                }
                else
                {
                    //Console.WriteLine("Teraz 2");
                    //Console.WriteLine(NumberOfSends);
                    Operation1();
                }
                //Console.WriteLine(VariableList[0].Value);

            }
            while (Program0List.Count != 0 || Program1List.Count != 0);
            

            Console.WriteLine("Ostateczna liczba operacji: " + NumberOfSends);
        }



        public void Operation0()  //wykonuje operacje zaczynajac od indexu aktualnego
        {
            int k = 0;
            //Console.WriteLine("Operacje 0, index: " + index0);
           // Console.WriteLine(index0);
            //Console.ReadKey();

            for (int i = index0; i < CommandList.Count(); i++)
            {
                k++;
                Command command = CommandList[i];
                //Console.WriteLine(i + ". " + CommandList[i].OperationType + "\t " + CommandList[i].VariableName + "\t" + CommandList[i].Value + "\t" + GetValue(CommandList[i].VariableName));
                switch (command.OperationType)
                {
                    case "set":
                        {
                            SetOperation(command);
                            break;
                        }
                    case "add":
                        {
                            AddOperation(command);
                            break;
                        }
                    case "mul":
                        {
                            MulOperation(command);
                            break;
                        }
                    case "mod":
                        {
                            ModOperation(command);
                            break;
                        }
                    case "snd":
                        {
                            SndOperation0(command);
                            index0 = i+1;
                            break;
                        }
                    case "rcv":
                        {
                            if (Program0List.Count != 0) //jeśli lista jest pusta
                            {
                                RcvOperation0(command);
                            }
                            else
                            {
                                index0 = i;
                                ActualProgram = 1;
                            }
                            break;
                        }
                    case "jgz":
                        {
                            i += Convert.ToInt32(JgzOperation(command));
                            //Console.WriteLine(JgzOperation(command));
                            break;
                        }
                }
                
                if (ActualProgram==1)
                {
                    //Console.WriteLine(i);
                    //index0 = i;
                    break;
                }
                
            }
        }
        public void SndOperation0(Command command) //do zadania 2
        {
            long Value = GetValue(command.VariableName);
            Program1List.Enqueue(Value);
            //ActualProgram = 1;
            //NumberOfSends++;
        }
        public void RcvOperation0(Command command)
        {
            long Value = Program0List.Dequeue(); //pobiera wartość z kolejki

            foreach (var item in VariableList) //iteruje po zmiennych, szuka odpowiedniej
            {
                if (item.Name == command.VariableName)
                {
                    item.Value = Value;                
                }
            }
        }

        public void Operation1()  //wykonuje operacje zaczynajac od indexu aktualnego
        {
            //int k = 0;
            for (int i = index1; i < CommandList.Count(); i++)
            {                
                Command command = CommandList[i];
                //Console.WriteLine(i + ". " + CommandList[i].OperationType + "\t " + CommandList[i].VariableName + "\t" + CommandList[i].Value + "\t" + GetValue(CommandList[i].VariableName));
                switch (command.OperationType)
                {
                    case "set":
                        {
                            SetOperation1(command);
                            break;
                        }
                    case "add":
                        {
                            AddOperation1(command);
                            break;
                        }
                    case "mul":
                        {
                            MulOperation1(command);
                            break;
                        }
                    case "mod":
                        {
                            ModOperation1(command);
                            break;
                        }
                    case "snd":
                        {
                            SndOperation1(command);
                            index1 = i+1;
                            break;
                        }
                    case "rcv":
                        {
                            if (Program1List.Count != 0) //jeśli lista jest pusta
                            {
                                RcvOperation1(command);
                            }
                            else
                            {
                                index1 = i;
                                ActualProgram = 0;
                            }
                            break;
                        }
                    case "jgz":
                        {
                            i += Convert.ToInt32(JgzOperation1(command));
                            //Console.WriteLine(JgzOperation(command));
                            break;
                        }
                }
                if (ActualProgram == 0)
                {
                    //index1 = i;
                    break;
                }
            }

        }
        public void SetOperation1(Command command)
        {
            long Value = GetValue1(command.Value);
            foreach (var item in VariableList1)
            {
                if (item.Name == command.VariableName)
                {
                    item.Value = Value;
                    break;
                }
            }
        }
        public void AddOperation1(Command command)
        {
            long Value = GetValue1(command.Value);
            foreach (var item in VariableList1)
            {
                if (item.Name == command.VariableName)
                {
                    item.Value += Value;
                    break;
                }
            }
        }
        public void MulOperation1(Command command)
        {
            long Value = GetValue1(command.Value);
            foreach (var item in VariableList1)
            {
                if (item.Name == command.VariableName)
                {
                    item.Value = item.Value * Value;
                    break;
                }
            }
        }
        public void ModOperation1(Command command)
        {
            long Value = GetValue1(command.Value);
            foreach (var item in VariableList1)
            {
                if (item.Name == command.VariableName)
                {
                    item.Value = item.Value % Value;
                    break;
                }
            }
        }
        public void SndOperation1(Command command) //do zadania 2
        {
            long Value = GetValue1(command.VariableName);
            Program0List.Enqueue(Value);
           // ActualProgram = 0;
            NumberOfSends++;
        }
        public void RcvOperation1(Command command)
        {
            long Value = Program1List.Dequeue(); //pobiera wartość z kolejki

            foreach (var item in VariableList1) //iteruje po zmiennych, szuka odpowiedniej
            {
                if (item.Name == command.VariableName)
                {
                    item.Value = Value;
                }
            }
        }
        public long JgzOperation1(Command command)
        {
            long Value = GetValue1(command.Value); //wartosc skoku
            long VariableValue = GetValue1(command.VariableName); //wartosc zmiennej
            if (VariableValue > 0)
            {
                return Value - 1;
            }
            return 0;
        }
        public long GetValue1(string value) // dzieki tej metodzie albo odczytujemy liczbe albo wartosc ze zmiennej pobieramy
        {
            try
            {
                return Convert.ToInt64(value);
            }
            catch (Exception)
            {
                foreach (var item in VariableList1)
                {
                    if (item.Name == value)
                    {
                        return item.Value;
                    }
                }
            }
            return 0;
        }





        public long GetValue(string value) // dzieki tej metodzie albo odczytujemy liczbe albo wartosc ze zmiennej pobieramy
        {
            try
            {
                return Convert.ToInt64(value);
            }
            catch (Exception)
            {
                foreach (var item in VariableList)
                {
                    if (item.Name==value)
                    {
                        return item.Value;
                    }
                }                
            }
            return 0;
        }
    }
}
