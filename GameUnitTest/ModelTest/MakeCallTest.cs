using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Game.GameModel;

namespace GameUnitTest.ModelTest {
    [TestFixture]
    public class MakeCallTest {
        Model model;

        [SetUp]
        public void SetUp() {
            model = new Model();
            model.InitializeAll(new Random(0), 10000);
        }

        [Test]
        public void AllPass() {
            for (int i = 0; i < Constants.PlayerNumber; i++) {
                Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), i));
            }

            Assert.AreEqual(model.State, ModelState.SelectCall);
        }

        [Test]
        public void WrongState() {
            model.State = ModelState.Play;

            Assert.AreEqual(-1, model.MakeCall(new Call(CallType.Pass), 0));
        }

        [Test]
        public void WrongPlayer() {
            Assert.AreEqual(-2, model.MakeCall(new Call(CallType.Pass), 1));
        }

        [Test]
        public void InvalidCall() {
            model.CallData.BestCall = new Call(CallType.Vip, 10);

            Assert.AreEqual(-3, model.MakeCall(new Call(CallType.Vip, 7), 0));
        }

        [TestCase(CallType.None)]
        [TestCase(CallType.Vip)]
        [TestCase(CallType.Gode)]
        [TestCase(CallType.Halve)]
        [TestCase(CallType.Sang)]
        public void SwitchToPartnerSelection(CallType type) {
            for (int i = 0; i < Constants.PlayerNumber - 1; i++) {
                Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), i));
            }

            Assert.AreEqual(0, model.MakeCall(new Call(type, 7), 3));

            Assert.AreEqual(ModelState.SelectPartner, model.State);
        }

        [TestCase(CallType.Sol)]
        [TestCase(CallType.RenSol)]
        [TestCase(CallType.Bordlægger)]
        [TestCase(CallType.SuperBordlægger)]
        public void SwitchToPlay(CallType type) {
            for (int i = 0; i < Constants.PlayerNumber - 1; i++) {
                Assert.AreEqual(0, model.MakeCall(new Call(CallType.Pass), i));
            }

            Assert.AreEqual(0, model.MakeCall(new Call(type), 3));

            Assert.AreEqual(ModelState.Play, model.State);
        }
    }
}
