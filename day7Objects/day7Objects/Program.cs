using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace day7Objects
{
    class Disk
    {
        
        public string Name;
        public int Weight;
        //public List<string> SlavesList = new List<string>();
        public bool IsHeMaster, IsHeFull,CanBeSum;
        public string[] SlavesList;

        public Disk(int length)
        {
            //Console.WriteLine( "cos" );
             SlavesList= new string[length];
        }
        
    }

    class Tower
    {
        List<object> lista = new List<object>();
        List<object> lista1 = new List<object>();
        List<object> lista2 = new List<object>();
        int p = 0, p1 = 0, p2 = 0;

        public void FileOpen()
        {
            try
            {
                using (StreamReader sr = new StreamReader("E:\\Nauka\\Kurs C#\\Advent of Code 2017\\Puzzle\\day_7.txt"))
                {
                    while (sr.EndOfStream == false)
                    {
                        string Line = sr.ReadLine();
                        SplitLine(Line);
                        Console.WriteLine(  ++p);
                    }
                    
                    
                    Console.WriteLine(  lista.Count());
                    //Console.WriteLine("razem:"+p+"\tMaster:"+p1+"\tSlave:"+p2+(p1+p2));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SprawdzCzyJestPelny(string name)
        {

        }


        public void ObliczSume()
        {
            foreach (Disk item in lista)
            {
                if (item.IsHeMaster == true) //sprawdza czy nad nim sa inne dyski
                {
                    if (item.IsHeFull == false) //sprawdza czy jest już sprawdzony ile wazy
                    {
                        int CzyWszystkieSaPelne = 0;
                        bool koniec = false;
                        for (int i = 0; i < item.SlavesList.Length; i++) //petla iterujaca po slavach
                        {                            
                            string name1 = item.SlavesList[i];
                            foreach (Disk element in lista)
                            {
                                if (name1 == element.Name && element.IsHeFull == true)
                                {
                                    CzyWszystkieSaPelne++;
                                }                                                               
                            }

                            if (item.SlavesList.Length == CzyWszystkieSaPelne)
                            {
                                int masaNowa = 0;
                                int masaStara = 10;
                                foreach (Disk element in lista)
                                {
                                    
                                    if (name1 == element.Name)
                                    {
                                        masaStara = masaNowa;
                                        masa1 = element.Weight;
                                        item.Weight += element.Weight;
                                    }
                                }
                            }


                            if (koniec == true) break;
                        }
                    }
                }
            }
        }

        public void SplitLine(string line)
        {
            String[] Foo = line.Split(new char[] { ' ' });
            
            Disk disk = new Disk(Foo.Length - 2);
            disk.Name = Foo[0];
            disk.Weight = Convert.ToInt32(Foo[1].Trim(new char[] { '(', ')' }));


            if (Foo.Length > 2)
            {
                disk.IsHeFull = false;
                disk.IsHeMaster = true;
                for (int i = 3; i < Foo.Length; i++)
                {
                    disk.SlavesList[i - 3] = Foo[i].TrimEnd(new char[] { ',' });
                }
            }
            else
            {
                disk.IsHeFull = true;
                disk.IsHeMaster = false;
            }

            lista.Add(disk);
            if (disk.IsHeMaster == true)
            {
                lista1.Add(disk);
            }
            else lista1.Add(disk);

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
