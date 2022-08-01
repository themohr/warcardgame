// See https://aka.ms/new-console-template for more information
using System;
using System.Collections;
using System.Linq;
using Utilities;

namespace WarCardGame
{
    class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {
            // Instantiate/Initialize variables
            List<string> p1Hand = new List<string>();
            List<string> p2Hand = new List<string>();
            List<string> discard = new List<string>();
            string[] deck = orderedDeck();
            string[] shuffled = shuffleDeck(deck);
            int player = 1;
            int total = deck.Length;
            int count = 0;
            int p1 = 0;
            int p2 = 0;
            int n = 0;
            string p1Card = "";
            string p2Card = "";
            string p1CardVal = "";
            string p2CardVal = "";

            // Deal Cards from 'shuffled' 'deck'
            while(count < total) {
                // Card to player 1...
                if(player == 2) {
                    p2Hand.Add(shuffled[count]);
                    //p2Hand[p2] = shuffled[count];
                    p2++;
                    player = 1;
                } 
                else // Card to player 2...
                {
                    p1Hand.Add(shuffled[count]);
                    //p1Hand[p1] = shuffled[count];
                    p1++;
                    player = 2;
                }
                count++;

            }

            // Play the game!
            // While both players have cards, continue...
            while(p1Hand.Count > 0 && p2Hand.Count > 0) {
                p1Card = p1Hand.ElementAt(0);
                p2Card = p2Hand.ElementAt(0);
                p1CardVal = p1Card.Substring(p1Card.Length - 1);
                p2CardVal = p2Card.Substring(p2Card.Length - 1);
                
                // Start with no discards
                discard.Clear();
                Console.WriteLine("p1Hand: " + p1Hand + " p2Hand: " + p2Hand);
                Console.WriteLine("Discard: " + discard.Count());
                // Place a card in the discard pile
                discard.Add(p1Card);
                discard.Add(p2Card);

                // Remove one card from each players hand
                p1Hand.Remove(p1Card);
                p2Hand.Remove(p2Card);

                // Delay for 1 second
                Thread.Sleep(1000);
                // Display the player card
                Console.WriteLine("Player 1: " + p1Hand.Count + " Player2: " + p2Hand.Count);
                Console.WriteLine("p1CardVal - " + p1CardVal + " || p2CardVal - " + p2CardVal + " : " + String.Compare(p1CardVal,p2CardVal));
                
                // Compare the last character of each players current card
                if(String.Compare(p1Card.Substring(p1Card.Length - 1),p2Card.Substring(p2Card.Length - 1)) > 0) {
                    Console.WriteLine("Add to p1 and remove from p2: " + p2Card);
                    // Add the cards to player 1 hand
                    foreach(string card in discard) {
                        p1Hand.Add(card);
                    }
                    
                }
                else if(String.Compare(p1Card.Substring(p1Card.Length - 1),p2Card.Substring(p2Card.Length - 1)) < 0)
                {
                    Console.WriteLine("Add to p2 and remove from p1: " + p1Card);
                    // Add the cards to player 2 hand
                    foreach(string card in discard) {
                        p2Hand.Add(card);
                    }
                }
                else // Go to war...
                {
                    bool win = false;
                    
                    Console.WriteLine("Cards in Hand: " + p1Hand.Count + " " + p2Hand.Count);
                    Console.WriteLine("THIS IS WAR!!! p1Card: " + p1Card + "| p2Card: " + p2Card);
                    Console.WriteLine("Next Card Face Down: " + p1Hand.ElementAt(0) + " " + p2Hand.ElementAt(0));
                    Console.WriteLine("Next Card Face Up (wins): " + p1Hand.ElementAt(1) + " " + p2Hand.ElementAt(1));
                    
                    // Continue laying down cards until round is won
                    while(!win)
                    {
                        // Each player lays the next card face down
                        // and the following card face up
                        // Add cards to discard pile
                        discard.Add(p1Hand.ElementAt(0));
                        discard.Add(p1Hand.ElementAt(1));
                        discard.Add(p2Hand.ElementAt(0));
                        discard.Add(p2Hand.ElementAt(1));
                        // Remove the next 2 cards from each player
                        p1Hand.Remove(p1Hand.ElementAt(0));
                        p1Hand.Remove(p1Hand.ElementAt(1));
                        p2Hand.Remove(p2Hand.ElementAt(0));
                        p2Hand.Remove(p2Hand.ElementAt(1));

                        // Delay for 1/2 a second
                        Thread.Sleep(500);

                        // Compare cards
                        if(String.Compare(p1Hand.ElementAt(1),p2Hand.ElementAt(1)) > 0) 
                        {
                            // add to p1Hand
                            foreach(string card in discard) {
                                p1Hand.Add(card);
                            }
                            
                            win = true;
                        }
                        else if(String.Compare(p1Hand.ElementAt(1),p2Hand.ElementAt(1)) < 0) 
                        {
                            // add to p2Hand
                            foreach(string card in discard) {
                                p2Hand.Add(card);
                            }
                            win = true;
                        }
                        else
                        {
                            // War must continue
                            win = false;
                        }
                        
                    }
                    
                    //break;
                }
                n++;
            }
            // The winner is
            Console.WriteLine("Player 1: " + p1Hand.Count + " Player2: " + p2Hand.Count);
            
        }

        // Shuffle the deck of cards
        static string[] shuffleDeck(string[] deck) {

            int total = deck.Length;
            string[] shuffled = new string[total];
            int s = 0;

            shuffled = deck.OrderBy(x => rand.Next()).ToArray();

            return shuffled;

        }

        // Stack the cards in order
        static string[] orderedDeck() {

            string[] suits = Constants.SUITS;
            string[] cards = Constants.CARDS;
            string[] deck = new string[56];
            string suit = suits[0];
            string card = cards[0];
            string sc = "";
            int count = 0;

            for(int i = 0; i < suits.Length; i++) {

                suit = suits[i];
                for(int n = 0; n < cards.Length; n++) {
                    card = cards[n];
                    sc = suit + card;
                    deck[count] = sc;
                    count++;
                }
            }

            return deck;
        }
    }
}

