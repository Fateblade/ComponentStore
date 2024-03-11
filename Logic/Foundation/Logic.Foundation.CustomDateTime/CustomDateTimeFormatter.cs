using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime
{
    class CustomDateTimeFormatter : ICustomDateTimeFormatter
    {
        public string FormatCurrentTime(ITimeMachine timeMachine, string format)
        {
            throw new System.NotImplementedException();
        }

        public string Format(DateTimeStamp timeStamp, string format, params DateTimeUnit[] units)
        {
            throw new System.NotImplementedException();
        }
    }
}