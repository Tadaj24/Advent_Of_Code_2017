using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace day_5
{
    class FileOpen
    {
        public List<int> list = new List<int>();
        public FileOpen(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (sr.EndOfStream == false)
                {
                    sr.ReadLine();
                    list.Add(Convert.ToInt32(sr.ReadLine()));
                }
            }
        } 
        
        public List<int> ReturnDate()
        {
            return list;
        }
    }
}
