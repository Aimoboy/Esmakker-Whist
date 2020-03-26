using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Game.GameModel;

namespace GameUnitTest.ModelTest {
    [TestFixture]
    public class CallTest {
        [Test]
        public void CreateCallNormalType([Range(7, 14)] int number) {
            CallType[] types = { CallType.None, CallType.Vip, CallType.Gode, CallType.Halve, CallType.Sang };

            foreach (CallType type in types) {
                Assert.DoesNotThrow(() => new Call(type, number));
            }
        }

        [Test]
        public void CreateCallNormalTypeBorderCase() {
            CallType[] types = { CallType.None, CallType.Vip, CallType.Gode, CallType.Halve, CallType.Sang };

            foreach (CallType type in types) {
                Assert.Throws<ArgumentOutOfRangeException>(() => new Call(type, 6));
                Assert.Throws<ArgumentOutOfRangeException>(() => new Call(type, 15));
            }
        }

        [Test]
        public void CreateCallSpecialType() {
            CallType[] types = { CallType.Pass, CallType.Sol, CallType.RenSol, CallType.Bordlægger, CallType.SuperBordlægger };

            foreach (CallType type in types) {
                Assert.DoesNotThrow(() => new Call(type)); 
            }
        }

        [Test]
        public void CreateCallWrongConstructorNormalType() {
            CallType[] types = { CallType.None, CallType.Vip, CallType.Gode, CallType.Halve, CallType.Sang };

            foreach (CallType type in types) {
                Assert.Throws<ArgumentException>(() => new Call(type));
            }
        }

        [Test]
        public void CreateCallWrongConstructorSpecialType([Range(7, 14)] int number) {
            CallType[] types = { CallType.Pass, CallType.Sol, CallType.RenSol, CallType.Bordlægger, CallType.SuperBordlægger };

            foreach (CallType type in types) {
                Assert.Throws<ArgumentException>(() => new Call(type, number));
            }
        }

        [Test]
        public void CallComparisonEquals() {
            Call call1 = new Call(CallType.Sol);
            Call call2 = new Call(CallType.Sol);

            Assert.AreEqual(call1, call2);
            Assert.IsTrue(call1 == call2);
        }

        [Test]
        public void CallComparisonNotEquals() {
            Call call1 = new Call(CallType.Sol);
            Call call2 = new Call(CallType.RenSol);

            Assert.AreNotEqual(call1, call2);
            Assert.IsFalse(call1 == call2);

            call2 = null;

            Assert.AreNotEqual(call1, call2);
            Assert.IsFalse(call1 == call2);

            call1 = new Call(CallType.Vip, 7);
            call2 = new Call(CallType.Vip, 8);

            Assert.AreNotEqual(call1, call2);
            Assert.IsFalse(call1 == call2);
        }

        [Test]
        public void CallComparisonSmallerGreater([Range(7, 13)] int number) {
            CallType[] types = { CallType.None, CallType.Vip, CallType.Gode, CallType.Halve, CallType.Sang };

            for (int i = 1; i < types.Length; i++) {
                for (int j = 0; j < i; j++) {
                    Assert.IsTrue(new Call(types[j], number) < new Call(types[i], number));
                }
            }
        }
    }
}
