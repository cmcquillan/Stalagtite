using Stalagtite.IO;
using Stalagtite.Actions;
using Stalagtite.Parsing;
using System;
using System.Collections.Generic;

namespace Stalagtite.Game
{
    public class Player : MobileObject
    {
        private Dictionary<Command, Type> _playerActionMap = new Dictionary<Command, Type>();

        public Player(GameState state, IClientComponent client)
            : base(state)
        {
            Client = client;
            State = default(PlayerState);
            GameState.Players.Add(this);
            _playerActionMap.Add(new Command() { Verb = "say" }, typeof(BroadcastAction));
            _playerActionMap.Add(new Command() { Verb = "quit" }, typeof(ExitAction));
        }

        public IClientComponent Client { get; private set; }

        public PlayerState State { get; private set; }

        public override ActionType ExecuteableActions
        {
            get
            {
                return base.ExecuteableActions | ActionType.PlayerAction;
            }
        }

        public override void Tick()
        {
            bool needsPrompt = false;

            while (Client.PendingCommands > 0)
            {
                Command cmd = Client.NextCommand();

                if (_playerActionMap.ContainsKey(cmd))
                {
                    ActionContext actx = ActionContext.Create(GameState, this, cmd);
                    IAction act = (IAction)Activator.CreateInstance(_playerActionMap[cmd], actx);
                    ActionQueue.Enqueue(act);
                }
                else { Client.WriteAndPrompt("Unknown command: " + cmd.Verb, ">>> "); }
            }

            if (ActionQueue.HasNext())
                needsPrompt = true;

            base.Tick();

            if (needsPrompt)
                Client.WriteAndPrompt(String.Empty, ">>> ");
        }

        protected override void OnDestroy()
        {
            Client.Dispose();
        }
    }
}
