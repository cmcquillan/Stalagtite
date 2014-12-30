using Stalagtite.Parsing;
using Stalagtite.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stalagtite.Actions
{
    public class ActionContext
    {
        public GameState GameState { get; private set; }
        public GameObject InvokedBy { get; private set; }
        public Command Command { get; private set; }

        public static ActionContext Create(
            GameState gameState,
            GameObject invoked,
            Command cmd)
        {
            return new ActionContext
            {
                GameState = gameState,
                InvokedBy = invoked,
                Command = cmd,
            };
        }
    }
}
