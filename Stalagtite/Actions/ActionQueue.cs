using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stalagtite.Actions
{
    public class ActionQueue
    {
        private Queue<IAction> _actions = new Queue<IAction>();
        private readonly ActionType _acceptedActions;

        public ActionQueue(ActionType acceptedActions)
        {
            _acceptedActions = acceptedActions;
        }

        public bool CanExecute(IAction action)
        {
            if ((action.ActionType & _acceptedActions) == 0)
                return false;
            return true;
        }

        public bool CanExecute(ActionType type)
        {
            if ((type & _acceptedActions) == 0)
                return false;
            return true;
        }

        public void Enqueue(IAction action)
        {
            if (!CanExecute(action))
                return;

            _actions.Enqueue(action);
        }

        public bool HasNext()
        {
            return _actions.Count > 0;
        }

        public IAction GetNext()
        {
            return _actions.Dequeue();
        }
    }
}
