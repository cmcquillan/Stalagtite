using Stalagtite.Parsing;
using Stalagtite.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stalagtite.Actions
{
    [Verb("say", AllowPartial = true)]
    public class BroadcastAction : BaseAction
    {
        public BroadcastAction(ActionContext ctx) : base(ctx) { }

        public override ActionType ActionType
        {
            get { return Actions.ActionType.AllAction; }
        }

        public override void Execute()
        {
           string msg = String.Format("{0} says, \"{1}\"{2}", Context.InvokedBy.Name, Context.Command.ArgText, Environment.NewLine);

            foreach (var p in Context.GameState.Players)
            {
                p.Client.Write(msg);
            }
        }
    }
}
