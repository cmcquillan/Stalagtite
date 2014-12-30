using Stalagtite.Actions;
using Stalagtite.Parsing;
using Stalagtite.Game;
using Stalagtite.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stalagtite
{
    public class StagServer
    {
        private readonly ClientManager _clientManager;
        private int _ticksPerMinute = 50;
        private long _liveTickCount = 0;
        private GameState _state;
        

        public StagServer()
        {
            _state = new GameState();
            _clientManager = new ClientManager(_state);
        }

        /// <summary>
        /// Adjusts game speed in terms of ticks per minute.
        /// </summary>
        public int Speed { get { return _ticksPerMinute; } set { _ticksPerMinute = value; } }

        public ClientManager ClientManager { get { return _clientManager; } }

        public GameState State { get { return _state; } }

        public void Run(long ticks)
        {
            while (ticks-- > 0)
            {
                int sleepTime = (60 / _ticksPerMinute) * 1000;
                Tick();
                _liveTickCount++;
                Task.Delay(sleepTime);
                //Thread.Sleep(sleepTime);
            }
        }

        [ExcludeFromCodeCoverage]
        public void Run()
        {
            while(true)
            {
                Run(long.MaxValue);
            }
        }

        protected void Tick()
        {
            _clientManager.AcceptNewClients();
            _clientManager.CleanupDeadClients();

            for (int i = _state.Objects.Count - 1; i >= 0;i--)
            {
                var o = _state.Objects[i];
                o.Tick();
            }

            for (int i = _state.Objects.Count - 1; i >= 0; i--)
            {
            }
        }

        public long LiveTicks { get { return _liveTickCount; } }
    }
}
