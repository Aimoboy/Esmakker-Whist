using System;
using System.Collections.Generic;
using System.Text;

namespace Game.GameModel {
    public class Model {

        public Deck Deck { get; set; } = new Deck();
        public Player[] Players { get; set; } = new Player[Constants.PlayerNumber];
        public Card[] Cat { get; set; } = new Card[3];
        public ModelState State { get; set; } = ModelState.SelectCall;
        public int StartPlayer { get; set; } = 0;

        public CallData CallData { get; set; } = new CallData();
        public RoundData RoundData { get; set; } = new RoundData();
        public PileData PileData { get; set; } = new PileData();

        public void InitializeAll(Random rand, int iterations) {
            InitializeVariables();
            InitializePlayers();
            GenerateAndShuffleCards(rand, iterations);
            DistributeCards();
        }

        /// <summary>
        /// Initialize the game model
        /// </summary>
        public void InitializeVariables() {
            CallData.Reset();
            RoundData.Reset();
            PileData.Reset(0);
        }

        /// <summary>
        /// Initialize player array
        /// </summary>
        public void InitializePlayers() {
            for (int i = 0; i < Constants.PlayerNumber; i++) {
                Players[i] = new Player();
            }
        }

        public void GenerateAndShuffleCards(Random rand, int iterations) {
            Deck.GenerateCards();
            Deck.Shuffle(rand, iterations);
        }

        public void DistributeCards() {
            for (int i = 0; i < Constants.PlayerNumber; i++) {
                Players[i].GiveCards(Deck.Cards, i * 13, 13 + i * 13);
            }

            for (int i = 0; i < 3; i++) {
                Cat[i] = Deck.Cards[i + 52];
            }
        }

        /// <summary>
        /// Make a call
        /// </summary>
        /// <param name="newCall">The call</param>
        /// <param name="playerNum">The player making the call</param>
        /// <returns>
        /// A code:
        /// -1 for wrong state
        /// -2 for wrong player
        /// -3 for invalid call
        /// </returns>
        public int MakeCall(Call newCall, int playerNum) {
            if (State != ModelState.SelectCall) {
                return -1;
            }

            if (playerNum != GetNextCallPlayer()) {
                return -2;
            }

            if (newCall.Type != CallType.Pass && newCall <= CallData.BestCall) {
                return -3;
            }

            CallData.Calls[playerNum] = newCall;

            if (CallData.BestCall < newCall) {
                CallData.BestCall = newCall;
                CallData.CallWinner = playerNum;
            }

            CheckCallSelectionDone();

            return 0;
        }

        /// <summary>
        /// Check if a player has won the call
        /// </summary>
        private void CheckCallSelectionDone() {
            int nullCount = 0;
            int passCount = 0;
            
            foreach (Call call in CallData.Calls) {
                if (call is null) {
                    nullCount++;
                }
                else if (call.Type == CallType.Pass) {
                    passCount++;
                }
            }

            if (nullCount > 0) {
                return;
            }

            if (passCount == 4) {
                CallData.Reset();
            }
            else if (passCount == 3) {
                switch (CallData.BestCall.Type) {
                    case CallType.None:
                    case CallType.Vip:
                    case CallType.Gode:
                    case CallType.Halve:
                    case CallType.Sang:
                        State = ModelState.SelectPartner;
                        break;
                    case CallType.Sol:
                    case CallType.RenSol:
                    case CallType.Bordlægger:
                    case CallType.SuperBordlægger:
                        State = ModelState.Play;
                        break;
                }
            }
        }

        /// <summary>
        /// Find the next player to make a call
        /// </summary>
        /// <returns>The index of the player that has to make a call</returns>
        private int GetNextCallPlayer() {
            int i = StartPlayer;

            while (CallData.Calls[i]?.Type == CallType.Pass || i == CallData.CallWinner) {
                i = GetNextPlayer(i);
            }

            return i;
        }

        /// <summary>
        /// Get the next player
        /// </summary>
        /// <param name="playerNum">The index of the current player</param>
        /// <returns>The index of the player after the given one</returns>
        private int GetNextPlayer(int playerNum) {
            playerNum++;

            if (playerNum == Constants.PlayerNumber) {
                playerNum = 0;
            }

            return playerNum;
        }

        /// <summary>
        /// Choose a partner by giving a card type
        /// </summary>
        /// <param name="type">The card type</param>
        /// <param name="playerNum">The index of the player calling</param>
        /// <returns>
        /// A code:
        /// 0 for succes
        /// -1 wrong model state
        /// -2 the player choosing partner is wrong
        /// -3 the call is gode and the player tries to select clubs as partner
        /// </returns>
        public int ChoosePartner(CardType type, int playerNum) {
            if (State != ModelState.SelectPartner) {
                return -1;
            }

            if (playerNum != CallData.CallWinner) {
                return -2;
            }

            if (type == CardType.Clubs && CallData.BestCall.Type == CallType.Gode) {
                return -3;
            }

            RoundData.PartnerType = type;

            // Set partner to the person that has the ace of that type
            for (int i = 0; i < Players.Length; i++) {
                foreach (Card card in Players[i].Hand) {
                    if (card.Type == type && card.Number == 14) {
                        RoundData.Partner = i;
                        break;
                    }
                }

                if (RoundData.Partner != -1) {
                    break;
                }
            }

            switch (CallData.BestCall.Type) {
                case CallType.None:
                case CallType.Vip:
                case CallType.Halve:
                    State = ModelState.SelectTrump;
                    break;
                case CallType.Gode:
                    RoundData.Trump = CardType.Clubs;
                    State = ModelState.Play;
                    break;
                case CallType.Sang:
                    State = ModelState.Play;
                    break;
            }

            return 0;
        }

        public int ChooseTrumpNone(CardType cardType, int playerNum) {
            if (State != ModelState.SelectTrump) {
                return -1;
            }

            if (CallData.BestCall.Type != CallType.None) {
                return -2;
            }

            if (CallData.CallWinner != playerNum) {
                return -3;
            }

            RoundData.Trump = cardType;
            State = ModelState.Play;

            return 0;
        }

        public int ChooseTrumpVip(bool take, int playerNum) {
            if (State != ModelState.SelectTrump) {
                return -1;
            }

            if (CallData.BestCall.Type != CallType.Vip) {
                return -2;
            }

            if (CallData.CallWinner != playerNum) {
                return -3;
            }

            if (take) {
                RoundData.Trump = Cat[CallData.VipCounter].Type;
                if (RoundData.Trump == CardType.Joker) {
                    RoundData.Trump = CardType.None;
                }
                State = ModelState.Play;
            }
            else {
                if (CallData.VipCounter == 1) {
                    RoundData.Trump = Cat[CallData.VipCounter].Type;
                    if (RoundData.Trump == CardType.Joker) {
                        RoundData.Trump = CardType.None;
                    }
                    State = ModelState.Play;
                }
                else {
                    CallData.VipCounter++;
                }
            }

            return 0;
        }

        public int ChooseTrumpHalve(CardType cardType, int playerNum) {
            if (State != ModelState.SelectTrump) {
                return -1;
            }

            if (CallData.BestCall.Type != CallType.Halve) {
                return -2;
            }

            if (RoundData.Partner != playerNum) {
                return -3;
            }

            RoundData.Trump = cardType;
            State = ModelState.Play;

            return 0;
        }

        public int PlaceCard(Card newCard, int playerNum) {
            if (State != ModelState.Play) {
                return -1;
            }

            if (PileData.Turn != playerNum) {
                return -2;
            }

            if (PileData.CardNumber > 0) {
                PileData.FirstCardType = PileData.Pile[0].Type;
            }

            switch (PileData.FirstCardType) {
                case CardType.None:
                case CardType.Joker:
                    break;
                default:
                    if (PlayerHandContains(PileData.FirstCardType, playerNum) && newCard.Type != PileData.FirstCardType) {
                        return -3;
                    }
                    break;
            }

            PileData.Pile[PileData.CardNumber] = newCard;
            PileData.CardNumber++;
            PileData.Turn = GetNextPlayer(PileData.Turn);

            Players[playerNum].Hand.Remove(newCard);

            // If pile is full
            if (PileData.CardNumber == Constants.PlayerNumber) {
                int winner = DeterminePileWinner();
                RoundData.PileWins[winner]++;
                PileData.Reset(winner);

                if (RoundOver()) {
                    GivePoints(); // Not implemented
                    CallData.Reset();
                    RoundData.Reset();
                    StartPlayer = GetNextPlayer(StartPlayer);
                    PileData.Reset(StartPlayer);
                }
            }

            return 0;
        }

        private bool PlayerHandContains(CardType cardType, int playerNum) {
            foreach (Card card in Players[playerNum].Hand) {
                if (card.Type == cardType) {
                    return true;
                }
            }

            return false;
        }

        private int DeterminePileWinner() {
            // Find trump index, -1 if there is none
            int containsTrump = -1;
            for (int i = 0; i < PileData.Pile.Length; i++) {
                if (PileData.Pile[i].Type == RoundData.Trump) {
                    containsTrump = i;
                    break;
                }
            }

            // If the first card is a joker and there is no trump
            if (containsTrump == -1 && PileData.Pile[0].Type == CardType.Joker) {
                return StartPlayer;
            }

            // Find index of winning card
            int winner = containsTrump != -1 ? containsTrump : 0;

            for (int i = 0; i < PileData.Pile.Length; i++) {
                Card card = PileData.Pile[i];

                if (containsTrump != -1) {
                    if (card.Type == RoundData.Trump && PileData.Pile[winner].Number < card.Number) {
                        winner = i;
                    }
                }
                else {
                    if (card.Type == PileData.FirstCardType && PileData.Pile[winner].Number < card.Number) {
                        winner = i;
                    }
                }
            }

            return winner;
        }

        private bool RoundOver() {
            if (Players[0].Hand.Count == 0) {
                return true;

            }

            switch (CallData.BestCall.Type) {
                case CallType.Sol:
                case CallType.Bordlægger:
                    if (Players[CallData.CallWinner].Hand.Count > 1) {
                        return true;
                    }
                    break;
                case CallType.RenSol:
                case CallType.SuperBordlægger:
                    if (Players[CallData.CallWinner].Hand.Count > 0) {
                        return true;
                    }
                    break;
            }

            return false;
        }

        private void GivePoints() {

        }
    }
}
