using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Game.GameModel;

namespace GameUnitTest.ModelTest {
    [TestFixture]
    public class CardTest {
        [TestCase(CardType.Spades)]
        [TestCase(CardType.Clubs)]
        [TestCase(CardType.Hearts)]
        [TestCase(CardType.Diamonds)]
        public void CardCreationBorderCase(CardType type) {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Card(type, 1));
            Assert.DoesNotThrow(() => new Card(type, 2));
            Assert.DoesNotThrow(() => new Card(type, 14));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Card(type, 15));
        }

        [TestCase(CardType.Spades)]
        [TestCase(CardType.Clubs)]
        [TestCase(CardType.Hearts)]
        [TestCase(CardType.Diamonds)]
        public void CardCreation(CardType type) {
            for (int i = 2; i < 15; i++) {
                Assert.DoesNotThrow(() => new Card(type, i));
            }
        }

        [Test]
        public void CardCreationJoker() {
            Assert.DoesNotThrow(() => new Card(CardType.Joker));
        }

        [TestCase(CardType.Spades)]
        [TestCase(CardType.Clubs)]
        [TestCase(CardType.Hearts)]
        [TestCase(CardType.Diamonds)]
        public void CardCreationWrongConstructorNormal(CardType type) {
            Assert.Throws<ArgumentException>(() => new Card(type));
        }

        [Test]
        public void CardCreationWrongConstructorJoker() {
            Assert.Throws<ArgumentException>(() => new Card(CardType.Joker, 0));
        }

        [Test]
        public void CardComparisonEquals() {
            Card card1 = new Card(CardType.Spades, 2);
            Card card2 = new Card(CardType.Spades, 2);

            Assert.IsTrue(card1 == card2);
            Assert.AreEqual(card1, card2);
        }

        [Test]
        public void CardComparisonNotEqual() {
            Card card1 = new Card(CardType.Spades, 2);
            Card card2 = new Card(CardType.Spades, 3);

            Assert.IsTrue(card1 != card2);
            Assert.AreNotEqual(card1, card2);
        }
    }
}
