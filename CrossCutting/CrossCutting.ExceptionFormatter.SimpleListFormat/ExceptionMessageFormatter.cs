using System;
using System.Text;
using Fateblade.Components.CrossCutting.ExceptionFormatter.Contract;

namespace Fateblade.Components.CrossCutting.ExceptionFormatter.SimpleListFormat
{
    internal class ExceptionMessageFormatter : IExceptionMessageFormatter
    {
        public string FormatAllMessagesToString(Exception toFormat)
        {
            StringBuilder sb = new StringBuilder(toFormat.Message);

            while (toFormat.InnerException != null)
            {
                sb.Append(Environment.NewLine).Append(Environment.NewLine);
                sb.Append(toFormat.InnerException.Message);

                toFormat = toFormat.InnerException;
            }

            return sb.ToString();
        }

        public string FormatAllStackTracesToString(Exception toFormat)
        {
            StringBuilder sb = new StringBuilder(toFormat.StackTrace);

            while (toFormat.InnerException != null)
            {
                sb.Append(Environment.NewLine).Append(Environment.NewLine);
                sb.Append(toFormat.InnerException.StackTrace);

                toFormat = toFormat.InnerException;
            }

            return sb.ToString();
        }

        public string FormatMessagesAndStackTracesToString(Exception toFormat)
        {
            StringBuilder sb = new StringBuilder();

            do
            {
                sb.Append("[Message]: ")
                  .Append(Environment.NewLine)
                  .Append(toFormat.Message)
                  .Append(Environment.NewLine)
                  .Append("[StackTrace]: ")
                  .Append(Environment.NewLine)
                  .Append(toFormat.StackTrace)
                  .Append(Environment.NewLine)
                  .Append(Environment.NewLine);

                toFormat = toFormat.InnerException;

            } while (toFormat != null);

            return sb.ToString();
        }
    }
}
