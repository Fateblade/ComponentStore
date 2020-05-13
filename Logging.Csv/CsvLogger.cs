using System;
using System.IO;
using CommonMessages.Contract.Messages;
using Fateblade.Components.CrossCutting.Logging.Contract;
using Fateblade.Components.CrossCutting.Logging.Contract.DataClasses;

namespace Fateblade.Components.CrossCutting.Logging.Csv
{
    internal class CsvLogger:ILogger
    {
        private MessageBufferElement[] _buffer;
        private int _bufferPosition;
        private readonly string _fullPath;
        

        public CsvLogger(LoggingCsvConfiguration configuration)
        {
            _buffer = new MessageBufferElement[configuration.MessageBufferCount];
            _fullPath = configuration.FullPathToLogFile;
            _bufferPosition = 0;
        }

        public void Log(LoggingPriority priority, LoggingType type, string message)
        {
            MessageBufferElement newBufferElement = new MessageBufferElement(priority, type, message, DateTime.Now);

            if (_bufferPosition == _buffer.Length)
            {
                forceWriteBuffer();
                _bufferPosition = 0;
            }
            
            _buffer[_bufferPosition++] = newBufferElement;
        }

        internal void ResizeBuffer(int newBufferLength)
        {
            var newBuffer = new MessageBufferElement[newBufferLength];
            
            for (int i = 0; i < _bufferPosition; ++i)
            {
                newBuffer[i] = _buffer[i];
            }

            _buffer = newBuffer;
        }

        internal void HandleShutdownNotize(ShutdownIssuedMessage message)
        {
            Log(LoggingPriority.Low, LoggingType.Information, $"Reason: {message.Reason}");
            forceWriteBuffer();
        }

        private void forceWriteBuffer()
        {
            if (_bufferPosition == 0)
            {
                return;

            }

            bool writeHeader = !File.Exists(_fullPath);
            using (Stream stream = File.Open(_fullPath, FileMode.Append))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    if (writeHeader)
                    {
                        writer.WriteLine("");
                    }

                    //if any performance problems occur, maybe use a stringbuilder and write a single time to file
                    for (int i = 0; i < _bufferPosition; ++i)
                    {
                        writer.WriteLine($"{_buffer[i].Timestamp}{_buffer[i].Priority};{_buffer[i].Type};{_buffer[i].Message}");
                    }
                }
            }
            
        }


    }
}
