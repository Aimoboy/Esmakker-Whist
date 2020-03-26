using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Game.GameModel {
    public class Card {
        public CardType Type { get; set; }
        public int Number { get; set; }

        /// <summary>
        /// Card constructor for: Spades, Clubs, Hearts, Diamonds
        /// </summary>
        /// <param name="type">The card type</param>
        /// <param name="number">The card number</param>
        public Card(CardType type, int number) {
            if (type == CardType.Joker) {
                throw new ArgumentException();
            }

            if (number < 2 || number > 14) {
                throw new ArgumentOutOfRangeException();
            }

            if (type == CardType.Joker) {
                throw new ArgumentException();
            }

            Type = type;
            Number = number;
        }

        /// <summary>
        /// Card constructor for: Joker
        /// </summary>
        /// <param name="type">The card type</param>
        public Card(CardType type) {
            switch (type) {
                case CardType.Spades:
                case CardType.Clubs:
                case CardType.Hearts:
                case CardType.Diamonds:
                    throw new ArgumentException();
            }

            Type = type;
            Number = 0;
        }

        public static bool operator ==(Card left, Card right) {
            return left.Type == right.Type && left.Number == right.Number;
        }

        public static bool operator !=(Card left, Card right) {
            return !(left == right);
        }

        public override bool Equals(object obj) {
            if (obj is null) {
                return false;
            }

            return this == (Card)obj;
        }
    }
}
