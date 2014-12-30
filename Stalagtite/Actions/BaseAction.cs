using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stalagtite.Actions
{
    public abstract class BaseAction : IAction
    {
        protected BaseAction(ActionContext ctx) { Context = ctx; }

        public abstract ActionType ActionType { get; }

        public abstract void Execute();

        public ActionContext Context { get; private set; }
    }
}
