using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Game.GameModel {
    public class CallData {
        public Call BestCall { get; set; }
        public int CallWinner { get; set; }
        public Call[] Calls { get; set; }
        public int VipCounter { get; set; }

        /// <summary>
        /// Reset the call data
        /// </summary>
        public void Reset() {
            BestCall = new Call(CallType.Pass);
            CallWinner = -1;
            Calls = new Call[Constants.PlayerNumber];
            VipCounter = 0;
        }
    }
}
