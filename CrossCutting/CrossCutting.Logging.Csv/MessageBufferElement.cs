using System;
using System.Collections.Generic;
using System.Text;
using Fateblade.Components.CrossCutting.Logging.Contract.DataClasses;

namespace Fateblade.Components.CrossCutting.Logging.Csv
{
    internal struct MessageBufferElement
    {
        public MessageBufferElement(LoggingPriority priority, LoggingType type, string message, DateTime timestamp)
        {
            Priority = priority;
            Type = type;
            Message = message;
            Timestamp = timestamp;
        }

        public DateTime Timestamp { get; }
        public LoggingPriority Priority { get; }
        public LoggingType Type { get; }
        public string Message { get; }
    }
}
