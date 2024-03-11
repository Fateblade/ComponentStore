namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses
{
    public struct DateTimeStamp
    {
        public ulong TimePart1 { get;  }
        public ulong TimePart2 { get; }

        public DateTimeStamp(ulong timePart1, ulong timePart2)
        {
            TimePart1 = timePart1;
            TimePart2 = timePart2;
        }
    }
}