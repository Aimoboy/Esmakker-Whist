using System;
using System.Collections.Generic;
using System.Text;

namespace Game.GameModel {
    public class PileData {
        public Card[] Pile { get; set; }
        public int CardNumber { get; set; }
        public int Turn { get; set; }
        public CardType FirstCardType { get; set; }

        public void Reset(int startPlayer) {
            Pile = new Card[Constants.PlayerNumber];
            CardNumber = 0;
            Turn = startPlayer;
            FirstCardType = CardType.None;
        }
    }
}
