using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    class Program
    {
        static void Main(string[] args)
        {
            //welcome message
            Console.WriteLine("Welcome, this program will calculate the maximum and average of the values that inputted into the console.");

            //create list for values
            List<int> values = new List<int>();
            
            Console.WriteLine();
            
            //ask for user input for value
            Console.Write("Enter a positive number to add to the list of values. Enter -1 to stop: ");
            int value = int.Parse(Console.ReadLine());
            
            //check if user hasn't entered -1 yet
            while (value != -1)
            {
                //check if user entered valid input
                while (value > 0)
                {
                    //add to list and ask for more values
                    values.Add(value);
                    Console.Write("Add another value: ");
                    value = int.Parse(Console.ReadLine());
                }
                //if user didn't enter valid input
                while (!(value > 0) && value != -1)
                {
                    //ask to try again
                    Console.Write("You didn't enter a positive integer. Try again: ");
                    value = int.Parse(Console.ReadLine());
                }
            }

            Console.WriteLine();

            //find maximum value and average
            int maximum = 0;
            int averageSum = 0;
            foreach (int number in values)
            {
                averageSum += number;
                if (number > maximum)
                {
                    maximum = number;
                }
            }
            Console.WriteLine("Maximum: " + maximum);
            Console.WriteLine("Average: " + (double)(averageSum/values.Count));

            Console.WriteLine();
        }
    }
}
