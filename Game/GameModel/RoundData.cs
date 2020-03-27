using System;
using System.Collections.Generic;
using System.Text;

namespace Game.GameModel {
    public class RoundData {
        public int Partner { get; set; }
        public CardType PartnerType { get; set; }
        public CardType Trump { get; set; }
        public int[] PileWins { get; set; }

        public void Reset() {
            Partner = -1;
            PartnerType = CardType.None;
            Trump = CardType.None;
            PileWins = new int[Constants.PlayerNumber];
        }
    }
}
