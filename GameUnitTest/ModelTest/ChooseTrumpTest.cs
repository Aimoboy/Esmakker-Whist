using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Game.GameModel;

namespace GameUnitTest.ModelTest {
    [TestFixture]
    public class ChooseTrumpTest {
        Model model;

        [SetUp]
        public void SetUp() {
            model = new Model();
            model.InitializeAll();
        }

        [Test]
        public void ChooseTrumpNone() {
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 0));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 1));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 2));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.None, 7), 3));

            Assert.AreEqual(0, model.ChoosePartner(CardType.Spades, 3));
            Assert.AreEqual(0, model.ChooseTrumpNone(CardType.Clubs, 3));
        }

        [Test]
        public void ChooseTrumpVip() {
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 0));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 1));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 2));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Vip, 7), 3));

            Assert.AreEqual(0, model.ChoosePartner(CardType.Spades, 3));
            Assert.AreEqual(0, model.ChooseTrumpVip(false, 3));
            Assert.AreEqual(0, model.ChooseTrumpVip(false, 3));

            Assert.AreEqual(ModelState.Play, model.State);
        }

        [Test]
        public void ChooseTrumpHalve() {
            model = new Model();
            model.InitializePlayers();
            model.InitializeVariables();
            model.Deck.GenerateCards();
            model.DistributeCards();

            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 0));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 1));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 2));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Halve, 7), 3));

            Assert.AreEqual(0, model.ChoosePartner(CardType.Spades, 3));
            Assert.AreEqual(0, model.ChooseTrumpHalve(CardType.Clubs, 0));
        }

        [Test]
        public void ChooseTrumpWrongCall() {
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 0));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 1));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 2));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Vip, 7), 3));

            Assert.AreEqual(0, model.ChoosePartner(CardType.Spades, 3));

            model.CallData.BestCall.Type = CallType.Sol;

            Assert.AreEqual(-2, model.ChooseTrumpNone(CardType.Clubs, 0));
            Assert.AreEqual(-2, model.ChooseTrumpVip(true, 0));
            Assert.AreEqual(-2, model.ChooseTrumpHalve(CardType.Clubs, 0));
        }
    }
}
