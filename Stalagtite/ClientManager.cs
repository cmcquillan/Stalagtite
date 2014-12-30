using Stalagtite.Game;
using Stalagtite.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stalagtite
{
    public class ClientManager
    {
                private readonly GameState _state;
                private List<IClientListener> _clientListeners = new List<IClientListener>();

        public ClientManager(GameState state)
        {
            _state = state;
        }


        public void RegisterClientListener(IClientListener clientListener)
        {
            if (clientListener == null)
                throw new ArgumentNullException("clientListener", "Listener cannot be null.");
            _clientListeners.Add(clientListener);
        }

        public void AcceptNewClients()
        {
            //Accept new clients.
            foreach (var listener in _clientListeners)
            {
                while (listener.HasNewClients)
                {
                    var player = new Player(_state, listener.AcceptClient());
                }
            }
        }

        public void CleanupDeadClients()
        {
            //Remove any Disconnected clients.
            foreach (var p in _state.Players.Where(o => o.Client.Disconnected))
                p.Client.Dispose();

            _state.Players.RemoveAll(p => p.Client.Disconnected);
        }
    }
}
