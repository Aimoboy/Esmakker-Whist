using Game.GameModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameUnitTest.ModelTest {
    [TestFixture]
    public class ChoosePartnerTest {
        Model model;

        [SetUp]
        public void SetUp() {
            model = new Model();
            model.InitializeAll();
        }

        [Test]
        public void TestSuccessOne() {
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Vip, 7), 0));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 1));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 2));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 3));

            Assert.AreEqual(0, model.ChoosePartner(CardType.Spades, 0));
        }

        [Test]
        public void TestSuccessTwo() {
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 0));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Vip, 7), 1));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 2));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 3));

            Assert.AreEqual(0, model.ChoosePartner(CardType.Spades, 1));
        }

        [Test]
        public void TestSuccessThree() {
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 0));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 1));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Vip, 7), 2));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 3));

            Assert.AreEqual(0, model.ChoosePartner(CardType.Spades, 2));
        }

        [Test]
        public void TestSuccessFour() {
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 0));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 1));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), 2));
            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Vip, 7), 3));

            Assert.AreEqual(0, model.ChoosePartner(CardType.Spades, 3));
        }

        [Test]
        public void WrongState() {
            for (int i = 0; i < Constants.PlayerNumber; i++) {
                Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), i));
            }

            Assert.AreEqual(-1, model.ChoosePartner(CardType.Spades, 3));
        }

        [Test]
        public void WrongPlayer() {
            for (int i = 0; i < Constants.PlayerNumber - 1; i++) {
                Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), i));
            }

            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Vip, 7), 3));

            Assert.AreEqual(-2, model.ChoosePartner(CardType.Spades, 0));
        }

        [Test]
        public void SelectClubsWhenGode() {
            for (int i = 0; i < Constants.PlayerNumber - 1; i++) {
                Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), i));
            }

            Assert.AreEqual(0, model.MakeCall(new Call(CallType.Gode, 7), 3));

            Assert.AreEqual(-3, model.ChoosePartner(CardType.Clubs, 3));
        }
    }
}
