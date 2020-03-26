using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Game.GameModel;

namespace GameUnitTest.ModelTest {
    [TestFixture]
    public class DeckTest {
        Deck deck;

        [SetUp]
        public void SetUp() {
            deck = new Deck();
            deck.GenerateCards();
        }

        [Test]
        public void TestCardGenerationNoNull() {
            foreach (Card card in deck.Cards) {
                Assert.IsNotNull(card);
            }
        }

        [Test]
        public void TestCardGenerationOneOfEach() {
            Assert.IsTrue(OneOfEach());
        }

        [Test]
        public void TestShuffleOneOfEach() {
            deck.Shuffle(100);
            Assert.IsTrue(OneOfEach());
        }

        
        public bool OneOfEach() {
            CardType[] types = { CardType.Spades, CardType.Clubs, CardType.Hearts, CardType.Diamonds };

            bool result = true;
            for (int i = 0; i < types.Length; i++) {
                for (int j = 0; j < 13; j++) {

                    bool found = false;
                    Card c = new Card(types[i], j + 2);
                    foreach (Card card in deck.Cards) {
                        if (card == c) {
                            found = true;
                            break;
                        }
                    }

                    if (!found) {
                        result = false;
                    }

                }
            }

            int jokerCount = 0;
            foreach (Card card in deck.Cards) {
                if (card.Type == CardType.Joker) {
                    jokerCount++;
                }
            }

            return result && jokerCount == 3;
        }
    }
}
