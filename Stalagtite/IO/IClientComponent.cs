using Stalagtite.Parsing;
using System;

namespace Stalagtite.IO
{
    public interface IClientComponent : IDisposable
    {
        Command NextCommand();
        Command NextCommand(bool blocking);
        int PendingCommands { get; }
        void Disconnect();
        void Write(string message);
        void WriteLine(string message);
        void WriteAndPrompt(string message, string prompt);
        bool Disconnected { get; }
    }
}
