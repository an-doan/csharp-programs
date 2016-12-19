using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    class Program
    {   
        /// <summary>
        /// Converts temperature in Fahrenheit to Celsius and back to Fahrenheit
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //prompts user for temperature in Fahrenheit
            Console.Write("Enter temperature in Fahrenheit: ");
            //puts input into float variable called originalFahrenheit
            int originalFahrenheit = int.Parse(Console.ReadLine());
            //converts to Celsius
            float celsius = ((float)originalFahrenheit - 32) / 9 * 5;
            //writes line to tell user the temperature in Celsius
            Console.WriteLine("The temperature in Celsius is " + (int)celsius + " degrees Celsius.");
            //converts back to Fahrenheit
            float newFahrenheit = celsius * 9 / 5 + 32;
            //writes on console the temperature converted back to Fahrenheit
            Console.WriteLine("The temperature converted back to Fahrenheit is " + (int)newFahrenheit + " degrees Fahrenheit.");
        }
    }
}
