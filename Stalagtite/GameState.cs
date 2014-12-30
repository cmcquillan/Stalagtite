using Stalagtite.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stalagtite
{
    public class GameState
    {
        public GameState()
        {
            Players = new List<Player>();
            Objects = new List<GameObject>();
            Mobiles = new List<MobileObject>();
            Worlds = new List<World>();
            Game = new TheGame(this);
        }

        public TheGame Game { get; private set; }
        public List<GameObject> Objects { get; private set; }
        public List<Player> Players { get; private set; }
        public List<MobileObject> Mobiles { get; private set; }
        public List<World> Worlds { get; private set; }
    }
}
