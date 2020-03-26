using System;
using System.Collections.Generic;
using System.Text;

namespace Game.GameModel {
    public class Deck {
        public Card[] Cards { get; set; } = new Card[55];

        /// <summary>
        /// Generate 55 cards for the card array in the deck
        /// </summary>
        public void GenerateCards() {
            CardType[] types = { CardType.Spades, CardType.Clubs, CardType.Hearts, CardType.Diamonds };

            for (int i = 0; i < types.Length; i++) {
                for (int j = 0; j < 13; j++) {
                    Cards[j + i * 13] = new Card(types[i], j + 2);
                }
            }

            for (int i = 52; i < 55; i++) {
                Cards[i] = new Card(CardType.Joker);
            }
        }

        /// <summary>
        /// Shuffle the cards
        /// </summary>
        /// <param name="iterations">The number of times to switch two random cards</param>
        public void Shuffle(int iterations) {
            Random rand = new Random();

            for (int i = 0; i < iterations; i++) {
                int i1 = rand.Next(Cards.Length);
                int i2 = rand.Next(Cards.Length);

                Card tmp = Cards[i1];
                Cards[i1] = Cards[i2];
                Cards[i2] = tmp;
            }
        }
    }
}
