using Stalagtite.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stalagtite.Actions
{
    public class ExitAction : BaseAction
    {
        public ExitAction(ActionContext ctx) : base(ctx) { }

        public override ActionType ActionType
        {
            get { return Actions.ActionType.AllClientAction; }
        }

        public override void Execute()
        {
            var player = (Player)Context.InvokedBy;
            player.Client.WriteLine("Goodbye!");
            player.Client.Disconnect();
            player.Destroy();
        }
    }
}
