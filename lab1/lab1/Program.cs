using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                Console.WriteLine("Hello " + String.Join(", ", args));
            }
            else
            {
                Console.WriteLine("Please enter an argument after lab1");
            }
        }
    }
}
