using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    /// <summary>
    /// Implements Lab 4 functionality
    /// </summary>
    class Program
    {
        /// <summary>
        /// Implements Lab 4 functionality
        /// </summary>
        /// <param name="args">command-line args</param>
        static void Main(string[] args)
        {
            // create a new deck and print the contents of the deck
            Deck deck = new Deck();
            Console.WriteLine("========================================================");
            Console.WriteLine("=============Deck before shuffle...======================");
            Console.WriteLine("========================================================");
            deck.Print();

            // shuffle the deck and print the contents of the deck
            deck.Shuffle();
            Console.WriteLine("========================================================");
            Console.WriteLine("=============Deck after shuffle...======================");
            Console.WriteLine("========================================================");
            deck.Print();

            Console.WriteLine();

            // take the top card from the deck and print the card rank and suit
            Card topCard = deck.TakeTopCard();
            Console.WriteLine("The first top card is a " + topCard);
            Console.WriteLine();

            // take the top card from the deck and print the card rank and suit
            topCard = deck.TakeTopCard();
            Console.WriteLine("The second top card is a " + topCard);
            Console.WriteLine();

        }
    }
}
