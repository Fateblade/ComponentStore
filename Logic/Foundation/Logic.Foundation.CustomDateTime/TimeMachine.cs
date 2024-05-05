using System.Collections.Generic;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime
{
    class TimeMachine : ITimeMachine
    {
        public IReadOnlyCollection<DateTimeUnit> TimeFormat { get; }
        public DateTimeStamp CurrentTime { get; private set; }


        public TimeMachine(params DateTimeUnit[] timeFormat)
        {
            CurrentTime = new DateTimeStamp(0, 0);
            TimeFormat = timeFormat;
        }


        public DateTimeStamp MoveForward(DateTimeUnit unit, int amount)
        {
            throw new System.NotImplementedException();
        }

        public DateTimeStamp MoveBackward(DateTimeUnit unit, int amount)
        {
            throw new System.NotImplementedException();
        }

        public void SetTime(DateTimeStamp timeStamp)
        {
            CurrentTime = timeStamp;
        }
    }
}