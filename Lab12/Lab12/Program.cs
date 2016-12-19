using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleCards;

namespace Lab12
{
    class Program
    {
        static void Main(string[] args)
        {
            //create deck and array of 5 cards
            Deck deck = new Deck();
            Card[] cards = new Card[5];

            //shuffle decks
            deck.Shuffle();

            //get the 1st and 2nd card of the deck and add to array; flip cards and print them
            cards[0] = deck.TakeTopCard();
            cards[0].FlipOver();
            cards[0].Print();

            cards[1] = deck.TakeTopCard();
            cards[1].FlipOver();
            cards[1].Print();

            Console.WriteLine();


            //use for loop to print cards
            for (int i = 0, n = cards.Length; i < n; i++)
            {
                cards[i] = deck.TakeTopCard();
                cards[i].FlipOver();
                cards[i].Print();
            }
        }
    }
}
