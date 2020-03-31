using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Game.GameModel;

namespace GameUnitTest.ModelTest {
    [TestFixture]
    public class PlaceCardTest {
        Model model;

        [SetUp]
        public void SetUp() {
            model = new Model();
            model.InitializeAll(new Random(0), 10000);
        }

        [Test]
        public void PlaceCard() {
            model.MakeCall(new Call(CallType.Pass), 0);
            model.MakeCall(new Call(CallType.Pass), 1);
            model.MakeCall(new Call(CallType.Pass), 2);
            model.MakeCall(new Call(CallType.Vip, 7), 3);

            model.ChoosePartner(CardType.Hearts, 3);

            model.ChooseTrumpVip(true, 3);

            Assert.AreEqual(0, model.PlaceCard(model.Players[0].Hand[0], 0));
        }

        [Test]
        public void WrongState() {
            Assert.AreEqual(-1, model.PlaceCard(new Card(CardType.Joker), 0));
        }

        [Test]
        public void WrongPlayer() {
            model.MakeCall(new Call(CallType.Pass), 0);
            model.MakeCall(new Call(CallType.Pass), 1);
            model.MakeCall(new Call(CallType.Pass), 2);
            model.MakeCall(new Call(CallType.Vip, 7), 3);

            model.ChoosePartner(CardType.Hearts, 3);

            model.ChooseTrumpVip(true, 3);

            Assert.AreEqual(-2, model.PlaceCard(model.Players[0].Hand[0], 1));
        }
    }
}
