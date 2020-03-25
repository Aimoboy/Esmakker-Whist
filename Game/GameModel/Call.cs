using System;
using System.Collections.Generic;
using System.Text;

namespace Game.GameModel {
    public class Call {
        public CallType Type { get; set; }
        public int Number { get; set; }

        /// <summary>
        /// Call constructor for types: None, Vip, Gode, Halve, Sang
        /// </summary>
        /// <param name="type">The type of call</param>
        /// <param name="number">The call number</param>
        public Call(CallType type, int number) {
            switch (type) {
                case CallType.Pass:
                case CallType.Sol:
                case CallType.RenSol:
                case CallType.Bordlægger:
                case CallType.SuperBordlægger:
                    throw new ArgumentException();
            }

            if (number < 7 || number > 13) {
                throw new ArgumentOutOfRangeException();
            }

            Type = type;
            Number = number;
        }

        /// <summary>
        /// Call constructor for types: Pass, Sol, RenSol, Bordlægger, SuperBordlægger
        /// </summary>
        /// <param name="type">The type of call</param>
        public Call(CallType type) {
            Type = type;

            Number = type switch
            {
                CallType.Pass => 7,
                CallType.Sol => 9,
                CallType.RenSol => 10,
                CallType.Bordlægger => 11,
                CallType.SuperBordlægger => 12,
                _ => throw new ArgumentException();
            };
        }
    }
}
