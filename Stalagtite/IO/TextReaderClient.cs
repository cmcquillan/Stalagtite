using Stalagtite.Parsing;
using Stalagtite.Utility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stalagtite.IO
{
    public class TextReaderClient : IClientComponent, IDisposable
    {
        private bool _isDisconnected = false;
        private bool _isDisposed = false;
        private const int BUFFER_SIZE = 1024;
        private char[] _buffer = new char[BUFFER_SIZE];
        private readonly char[] _newLine = Environment.NewLine.ToCharArray();
        private int _bufferPosition;
        private TextReader _textReader;
        private TextWriter _textWriter;
        private readonly ICommandParser _parser;
        private readonly ConcurrentQueue<string> _pendingCommands = new ConcurrentQueue<string>();
        private readonly object _readTask;
        private readonly object _writeTask;
        private readonly CancellationTokenSource _token;
        private readonly ConcurrentQueue<string> _pendingMessages = new ConcurrentQueue<string>();

        public TextReaderClient(TextReader textReader, TextWriter textWriter, ICommandParser parser)
        {
            this._textReader = textReader;
            this._textWriter = textWriter;
            this._parser = parser;

            _token = new CancellationTokenSource();
            _token.Token.Register(() => _isDisconnected = true);
            _readTask = HandleInputAsync(_token.Token).ConfigureAwait(false);
            _writeTask = HandleOutputAsync(_token.Token).ConfigureAwait(false);
        }

        public int PendingCommands { get { return _pendingCommands.Count; } }

        public Command NextCommand(bool blocking)
        {
            ThrowIfDisposed();

            if (blocking)
                WaitForCommand();

            string cmd = String.Empty;
            _pendingCommands.TryDequeue(out cmd);
            return _parser.GetCommand(cmd);
        }

        public Command NextCommand()
        {
            return NextCommand(true);
        }

        private void WaitForCommand()
        {
            while(true)
            {
                if (_pendingCommands.Count > 0)
                    return;
                Thread.Sleep(50);
            }
        }

        private async Task HandleInputAsync(CancellationToken token)
        {
            while(!token.IsCancellationRequested)
            {
                await ReadInputAsync();
            }
        }

        private async Task HandleOutputAsync(CancellationToken token)
        {
            while(!token.IsCancellationRequested)
            {
                var writeTask = WriteOutputAsync();
                await Task.Delay(50);
                await writeTask;
            }
        }

        private async Task ReadInputAsync()
        {
            int read = 0;

            try
            {
                read = await _textReader.ReadAsync(_buffer, _bufferPosition, _buffer.Length - _bufferPosition);
            }
            catch (IOException) { }
            catch (Exception) { _token.Cancel(); }

            _bufferPosition += read;

            if (read == 0)
                await Task.Delay(100);

            //Scan for newlines
            while (ExtractLine()) { };
        }

        private async Task WriteOutputAsync()
        {
            try
            {
                while (_pendingMessages.Count > 0)
                {
                    string msg = null;
                    _pendingMessages.TryDequeue(out msg);
                    _textWriter.Write(msg);
                }
                    
                await _textWriter.FlushAsync();
            }
            catch (IOException e)
            {
                _token.Cancel();
            }
        }

        private bool ExtractLine()
        {
            int newLinePosition = CharacterSearch.FindPosition(_buffer, _newLine);

            if (newLinePosition != CharacterSearch.POSITION_NOT_FOUND)
            {
                int copyAmount = newLinePosition + _newLine.Length;
                char[] newBuf = new char[BUFFER_SIZE];
                char[] cmdBuf = new char[copyAmount];
                Array.Copy(_buffer, cmdBuf, copyAmount);
                Array.Copy(_buffer, copyAmount, newBuf, 0, _buffer.Length - copyAmount);
                _buffer = newBuf;
                _bufferPosition = _bufferPosition - copyAmount;
                _pendingCommands.Enqueue(new string(cmdBuf).TrimEnd(_newLine));
                return true;
            }

            return false;
        }

        #region IDisposable
        public void ThrowIfDisposed()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(this.GetType().Name);
        }

        public void Dispose()
        {
            _isDisposed = true;
            _isDisconnected = true;
            Dispose(true);
        }

        public virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                _token.Cancel();

                if (_textWriter != null)
                {
                    if (_pendingMessages.Count > 0)
                        WriteOutputAsync().Wait();

                    _textWriter.Close();
                    _textWriter = null;
                }

                if(_textReader != null)
                {
                    _textReader.Close();
                    _textReader = null;
                }
            }
        }
        #endregion

        public void Write(string message)
        {
            _pendingMessages.Enqueue(message);
        }           
        
        public void WriteLine(string message)
        {
            Write(String.Format("{0}{1}", message, Environment.NewLine));
        }

        public bool Disconnected { get { return _isDisconnected; } }

        public void WriteAndPrompt(string message, string prompt)
        {
            Write(String.Format("{0}{1}{2}",
                message, Environment.NewLine, prompt));
        }

        public void Disconnect()
        {
            _token.Cancel();
        }
    }
}
