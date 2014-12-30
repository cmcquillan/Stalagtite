using Stalagtite;
using Stalagtite.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var listener = new TcpClientListener("localhost", 25025))
            {
                var server = new StagServer();
                server.ClientManager.RegisterClientListener(listener);
                server.Run();
            }
        }
    }
}
