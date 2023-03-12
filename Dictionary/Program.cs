using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary dict = new Dictionary(true);
            dict.Add("word", "слово");
            dict.Add("robber", "вор");
            dict.Add("robber", "бандит");

            dict.Print();
            Console.Read();
        }
    }
}
