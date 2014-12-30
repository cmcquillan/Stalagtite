using Stalagtite.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stalagtite.Game
{
    public class MobileObject : GameObject
    {
        public MobileObject(GameState state)
            : base(state)
        {
            GameState.Mobiles.Add(this);
        }

        public override ActionType ExecuteableActions
        {
            get
            {
                return base.ExecuteableActions | ActionType.MobileAction;
            }
        }
    }
}
