using System;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.Exceptions
{
    public class DateTimeFormatException : CustomDateTimeException
    {
        public DateTimeFormatException()
        {
        }

        public DateTimeFormatException(string message) : base(message)
        {
        }

        public DateTimeFormatException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
