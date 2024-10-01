using System;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.Exceptions
{
    public class DateTimeLimitException : CustomDateTimeException
    {
        public DateTimeLimitException(string message) : base(message)
        {
        }

        public DateTimeLimitException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public class DateTimeUnderflowException : DateTimeLimitException
    {
        public DateTimeUnderflowException() : base("Action resulted in an underflow of the custom date time")
        {
        }

        public DateTimeUnderflowException(string message) : base(message)
        {
        }

        public DateTimeUnderflowException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public class DateTimeOverflowException : DateTimeLimitException
    {
        public DateTimeOverflowException() : base("Action resulted in an overflow of the custom date time")
        {
        }

        public DateTimeOverflowException(string message) : base(message)
        {
        }

        public DateTimeOverflowException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    
}
