using Stalagtite.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Stalagtite.IO
{
    public class TcpClientListener : IClientListener, IDisposable
    {
        private readonly int _portNumber;
        private readonly string _hostName;
        private readonly TcpListener _listener;

        public TcpClientListener(string hostName, int portNumber)
        {
            _hostName = hostName;
            _portNumber = portNumber;
            _listener = TcpListener.Create(_portNumber);
            _listener.Start();
        }

        public bool HasNewClients { get { return _listener.Pending(); } }

        public IClientComponent AcceptClient()
        {
            var socket = _listener.AcceptSocket();
            socket.ReceiveTimeout = 50;
            var ns = new NetworkStream(socket, FileAccess.ReadWrite, true);

            var reader = new StreamReader(ns);
            var writer = new StreamWriter(ns);
            
            var client = new TextReaderClient(reader, writer, new TextCommandParser());
            client.Write("\u001B[2J");
            return client;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (_listener != null)
            {
                _listener.Stop();
            }
        }
    }
}
