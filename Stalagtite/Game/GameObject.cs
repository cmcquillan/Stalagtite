using Stalagtite.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stalagtite.Actions;

namespace Stalagtite.Game
{
    public abstract class GameObject
    {
        private readonly ActionQueue _actionQueue;
        private readonly List<GameObject> _containedObjects;
        private GameObject _parent;
        private GameState _state;

        protected GameObject(GameState state)
        {
            _state = state;
            _actionQueue = new ActionQueue(ExecuteableActions);
            _containedObjects = new List<GameObject>();
            GameState.Objects.Add(this);
        }

        protected GameState GameState { get { return _state; } }

        public string Name { get; set; }

        public virtual ActionType ExecuteableActions { get { return ActionType.None; } }

        public bool CanExecute(ActionType type)
        {
            return ActionQueue.CanExecute(type);
        }

        public ActionQueue ActionQueue { get { return _actionQueue; } }

        public GameObject Parent { get { return _parent; } set { _parent = value; } }

        public void AddChild(GameObject go)
        {
            if (go.Parent != null)
                go.Parent.RemoveChild(go);
            
            go.Parent = this;
            _containedObjects.Add(go);
        }

        public void RemoveChild(GameObject go)
        {
            _containedObjects.Remove(go);
        }

        public IEnumerable<GameObject> Children { get { return _containedObjects; } }

        public void Destroy()
        {
            OnDestroy();
            if (_parent != null)
                this.Parent.RemoveChild(this);

            GameState.Objects.Remove(this);
        }

        protected virtual void OnDestroy() { }

        public virtual void Tick()
        {
            if (!ActionQueue.HasNext())
                return;

            var action = ActionQueue.GetNext();
            action.Execute();
        }
    }
}