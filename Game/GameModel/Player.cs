using System;
using System.Collections.Generic;
using System.Text;

namespace Game.GameModel {
    public class Player {
        public List<Card> Hand { get; set; } = new List<Card>();

        /// <summary>
        /// Give cards to the player
        /// </summary>
        /// <param name="cards">The card array to take cards from</param>
        /// <param name="start">The start index (included)</param>
        /// <param name="end">The end index (excluded)</param>
        public void GiveCards(Card[] cards, int start, int end) {
            for (int i = start; i < end; i++) {
                Hand.Add(cards[i]);
            }
        }
    }
}
