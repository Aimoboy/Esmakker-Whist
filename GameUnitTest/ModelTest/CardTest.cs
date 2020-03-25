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
    }
}
