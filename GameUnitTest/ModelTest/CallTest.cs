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
    }
}
