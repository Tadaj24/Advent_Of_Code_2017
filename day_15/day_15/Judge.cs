using System;
using System.Collections.Generic;

namespace day_15
{
    class Judge
    {
        //wartości początkowe
        public int Counter = 0; //licznik powtorzen
        //public long ValueA = 65;
        //public long ValueB = 8921;

        public long ValueA = 873;
        public long ValueB = 583;

        public long FactorA = 16807;
        public long FactorB = 48271;
        public long Remainder = 2147483647;

        public List<long> GoodValueA = new List<long>();
        public List<long> GoodValueB = new List<long>();

        public void GenerateNewValues() //tworzy nową wartość
        {
            ValueA = (ValueA * FactorA) % Remainder;
            ValueB = (ValueB * FactorB) % Remainder;
           // Console.WriteLine(ValueA + "  " + ValueB);
        }

        public void MakeOperations1()//obsluguje cale zadanie 1
        {
            for (int i = 0; i < 40000000; i++) //40 miliony razy
            {
                GenerateNewValues();
                if (ConvertToBinary(ValueA)==ConvertToBinary(ValueB))
                {
                    Counter++;
                }

                if (i % 1000000 == 0)
                {
                    Console.WriteLine(i);
                }
            }
            Console.WriteLine("Licznik wystapień pierwszego: " + Counter);
        }

        public void MakeOperations2()
        {
            //for (int i = 0; i < 40000000; i++) //40 miliony razy
            int j = 0;
            while(GoodValueA.Count<=5000000 || GoodValueB.Count<=5000000)
            {
                j++;
                GenerateNewValues();
                if (ValueA % 4 ==0)
                {
                    GoodValueA.Add(ValueA);                    
                }

                if (ValueB % 8 == 0)
                {
                    GoodValueB.Add(ValueB);
                }

                //if (GoodValueA.Count>0 && GoodValueB.Count>0)
                //{
                //    if (GoodValueA[0]==GoodValueB[0])
                //    {
                //        Counter++;
                //        GoodValueA.RemoveAt(0);
                //        GoodValueB.RemoveAt(0);
                //    }
                //}

                if (j % 1000000 == 0)
                {
                    Console.WriteLine(j);
                }
            }
            Console.WriteLine(j);
            for (int i = 0; i < GoodValueB.Count; i++)
            {
                if (ConvertToBinary(GoodValueA[i]) == ConvertToBinary(GoodValueB[i]))
                {
                    Counter++;
                }
            }
            Console.WriteLine(Counter);
            Console.WriteLine(GoodValueA.Count);
            Console.WriteLine(GoodValueB.Count);
        }


        public string ConvertToBinary(long value)
        {
            string BinaryValue = Convert.ToString(value, 2);
            while (BinaryValue.Length<16) //dopisuje 0 na poczatku //jeśli wyraz jest zbyt ktorki dodaje 0
            {
                BinaryValue = "0" + BinaryValue;
            }
            string word = ""; //puste slowo do utworzenia ciagu binarnego
            for (int i = BinaryValue.Length-16; i < BinaryValue.Length; i++) //tworzy 16 znakowy wyraz
            {
                word += BinaryValue[i];
            }
            //Console.WriteLine(word);
            //Console.WriteLine(Value.ToString("00000000"));
            return word;
        }
    }
}
