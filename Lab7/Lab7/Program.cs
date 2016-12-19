using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            //ask the user for their birth month and store user input into a string variable
            Console.Write("What month were you born in? ");
            string birthMonth = Console.ReadLine();

            //ask the user for their birth day and store user input into int variable
            Console.Write("What day were you born on? ");
            int birthDay = int.Parse(Console.ReadLine());

            Console.WriteLine();

            //display their full birthday
            Console.WriteLine("Your birthday is " + birthMonth + " " + birthDay);

            //display the email reminder
            Console.WriteLine("You will recieve an email reminder on " + birthMonth + " " + (birthDay-1));

            Console.WriteLine();
        }
    }
}
