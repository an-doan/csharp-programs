using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            //get user input and put into string var
            Console.Write("Enter <pyramid slot number>,<block letter>,<whether or not the block should be lit>: ");
            string userInput = Console.ReadLine();

            //find index of first comma and use it to get pyramid slot number
            int firstCommaInstance = userInput.IndexOf(',');

            //no need to make length because the startIndex is 0
            int pyramidSlotNumber = int.Parse(userInput.Substring(0, firstCommaInstance));

            //find the block letter; length will always be one
            char blockLetter = userInput.Substring(firstCommaInstance+1, 1)[0];

            //find second comma by making a substring using the first comma instance
            string userInputSubstring = userInput.Substring(firstCommaInstance + 1);
            int secondCommaInstance = userInputSubstring.IndexOf(',');

            //find whether or not block should be lit
            bool blockLit = bool.Parse(userInputSubstring.Substring(secondCommaInstance+1));

            //print out the slot number, block letter, and block lit boolean
            Console.WriteLine();
            Console.WriteLine("Pyramid Slot Number: " + pyramidSlotNumber);
            Console.WriteLine("Block Letter: " + blockLetter);
            Console.WriteLine("Block Lit: " + blockLit);
        }
    }
}
