using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stalagtite.Game
{
    public class World : GameObject
    {
        public World(GameState state)
            :base(state)
        {
            GameState.Worlds.Add(this);
        }

        public override Actions.ActionType ExecuteableActions
        {
            get
            {
                return base.ExecuteableActions | Actions.ActionType.WorldAction;
            }
        }
    }
}
