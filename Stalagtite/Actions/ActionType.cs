using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stalagtite.Actions
{
    [Flags]
    public enum ActionType
    {
        None = 0,
        EmptyClientAction = 1 << 0,
        GameAction = 1 << 1,
        WorldAction = 1 << 2,
        RoomAction =  1 << 3,
        ObjectAction = 1 << 4,
        PlayerAction = 1 << 5,
        MobileAction = 1 << 6,

        AreaAction = WorldAction | RoomAction,
        EnvironmentAction = AreaAction | GameAction,
        AllClientAction = EmptyClientAction | PlayerAction,
        
        AllAction = Int32.MaxValue,
    }
}
