using Stalagtite.Parsing;
using Stalagtite.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stalagtite.Actions
{
    public interface IAction
    {
        ActionType ActionType { get; }
        ActionContext Context { get; }
        void Execute();
    }
}
