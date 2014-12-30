using System;
namespace Stalagtite.IO
{
    public interface IClientListener
    {
        IClientComponent AcceptClient();
        bool HasNewClients { get; }
    }
}
