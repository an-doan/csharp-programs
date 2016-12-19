using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleCards;

namespace Lab13
{
    class Program
    {
        static void Main(string[] args)
        {
            //welcome statement
            Console.WriteLine("Welcome, this program will print out all numbers from a given range.");

            //ask for number for lowest range and parse to int
            Console.Write("Enter a number for the lowest range: ");
            int lowRange;
            bool lowParse = int.TryParse(Console.ReadLine(), out lowRange);

            //if not an int, try again
            while (!lowParse)
            {
                Console.Write("Not a number. Try again: ");
                lowParse = int.TryParse(Console.ReadLine(), out lowRange);
            }

            //ask for number for highest range and parse to int
            Console.Write("Enter a number for the highest range: ");
            int highRange;
            bool highParse = int.TryParse(Console.ReadLine(), out highRange);

            //if not an int or lower than lowest range, try again
            while (!highParse || highRange < lowRange)
            {
                Console.Write("Not a number or too low. Try again: ");
                lowParse = int.TryParse(Console.ReadLine(), out highRange);
            }

            Console.WriteLine();

            //use for loop to print numbers between range
            for (int i = lowRange; i <= highRange; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
            Console.WriteLine();

            //new program, welcome message
            Console.WriteLine("This program will create a hand of cards using a deck and it will flip the cards over. Then, the cards in the hand will be printed out.");

            Console.WriteLine();

            //create new deck and shuffle
            Deck deck = new Deck();
            deck.Shuffle();

            //create list for hand of cards
            List<Card> hand = new List<Card>();

            //add 5 cards to hand
            for (int i = 0; i < 5; i++)
            {
                hand.Add(deck.TakeTopCard());
            }

            //use for loop to flip cards over
            for (int i = 0; i < hand.Count(); i++)
            {
                hand.ElementAt(i).FlipOver();
            }

            //use foreach loop to print cards
            foreach (Card card in hand)
            {
                card.Print();
            }

            Console.WriteLine();

        }
    }
}
