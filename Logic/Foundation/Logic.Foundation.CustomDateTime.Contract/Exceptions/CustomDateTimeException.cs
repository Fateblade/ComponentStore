using System;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.Exceptions
{
    public class CustomDateTimeException : Exception
    {
        public CustomDateTimeException()
        {
        }

        public CustomDateTimeException(string message) : base(message)
        {
        }

        public CustomDateTimeException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
