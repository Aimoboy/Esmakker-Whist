using System;
using System.Collections.Generic;
using System.Text;

namespace Game.GameModel {
    public class Card {
        public CardType Type { get; set; }
        public int Number { get; set; }

        public Card(CardType type, int number) {
            if (number < 2 || number > 14) {
                throw new ArgumentOutOfRangeException();
            }

            Type = type;
            Number = number;
        }
    }
}
