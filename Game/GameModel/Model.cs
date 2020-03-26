using System;
using System.Collections.Generic;
using System.Text;

namespace Game.GameModel {
    public class Model {

        public Player[] Players { get; set; } = new Player[Constants.PlayerNumber];
        public ModelState State { get; set; }
    }
}
