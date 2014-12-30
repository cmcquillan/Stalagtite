using Stalagtite.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stalagtite
{
    public class TheGame : GameObject
    {
        public TheGame(GameState state)
            :base(state)
        { }

        public override Actions.ActionType ExecuteableActions
        {
            get
            {
                return base.ExecuteableActions | Actions.ActionType.GameAction;
            }
        }
    }
}
