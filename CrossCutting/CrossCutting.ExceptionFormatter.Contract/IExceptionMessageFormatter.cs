using System;

namespace Fateblade.Components.CrossCutting.ExceptionFormatter.Contract
{
    public interface IExceptionMessageFormatter
    {
        string FormatAllMessagesToString(Exception toFormat);
        string FormatAllStackTracesToString(Exception toFormat);
        string FormatMessagesAndStackTracesToString(Exception toFormat);
    }
}
