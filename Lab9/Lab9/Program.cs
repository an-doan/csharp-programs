using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    class Program
    {
        static void Main(string[] args)
        {
            //write user options
            Console.WriteLine("**************");
            Console.WriteLine("Menu:");
            Console.WriteLine("N – New Game");
            Console.WriteLine("L – Load Game");
            Console.WriteLine("O – Options");
            Console.WriteLine("Q – Quit");
            Console.WriteLine("**************");

            //prompt user for option they want and store user input
            Console.Write("Enter a menu option: ");
            char option = Console.ReadLine()[0];

            Console.WriteLine();
            Console.Write("Message created by if statements: ");

            //use if conditions to print messages for each option
            if (option == 'n' || option == 'N')
            {
                Console.WriteLine("New game created!");
            }
            else if (option == 'l' || option == 'L')
            {
                Console.WriteLine("Game is loaded!");
            }
            else if (option == 'o' || option == 'O')
            {
                Console.WriteLine("Options menu opened!");
            }
            else if (option == 'q' || option == 'Q')
            {
                Console.WriteLine("You have quit the game!");
            }
            else
            {
                Console.WriteLine("You didn't enter a valid option :(");
            }

            Console.Write("Message created by switch statements: ");

            //print messages with switch statements
            switch(option)
            {
                case 'n':
                case 'N':
                    Console.WriteLine("New game created!");
                    break;
                case 'l':
                case 'L':
                    Console.WriteLine("Game is loaded!");
                    break;
                case 'o':
                case 'O':
                    Console.WriteLine("Options menu opened!");
                    break;
                case 'q':
                case 'Q':
                    Console.WriteLine("You have quit the game!");
                    break;
                default:
                    Console.WriteLine("You didn't enter a valid option :(");
                    break;
            }
        }
    }
}
