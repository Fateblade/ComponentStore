using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime
{
    class TimeMachine : ITimeMachine
    {
        private readonly Dictionary<Guid, ulong> _relativeToRootValues;


        public IReadOnlyCollection<DateTimeUnit> TimeFormat { get; }
        public DateTimeUnit RootUnit { get; }
        public DateTimeStamp CurrentTime { get; private set; }


        public TimeMachine(params DateTimeUnit[] timeFormat)
            : this(new DateTimeStamp(0, 0), timeFormat) { }

        public TimeMachine(DateTimeStamp startingTime, params DateTimeUnit[] timeFormat)
        {
            validateDateTimeFormat(timeFormat);

            CurrentTime = startingTime;
            TimeFormat = timeFormat;
            RootUnit = timeFormat.First(unit => !unit.LeavingRelations.Any());
            _relativeToRootValues = buildTimeFormatValueDictionary(timeFormat);
        }


        public DateTimeStamp MoveForward(DateTimeUnit unit, int amount)
        {
            if (!_relativeToRootValues.TryGetValue(unit.Id, out var rootUnitsPerUnit)) throw new UnitNotInFormatException(unit);
            var amountInRootUnit = (ulong)amount * rootUnitsPerUnit;

            moveForward(amountInRootUnit);

            return CurrentTime;
        }

        public DateTimeStamp MoveBackward(DateTimeUnit unit, int amount)
        {
            if (!_relativeToRootValues.TryGetValue(unit.Id, out var rootUnitsPerUnit)) throw new UnitNotInFormatException(unit);
            var amountInRootUnit = (ulong)amount * rootUnitsPerUnit;

            moveBackward(amountInRootUnit);

            return CurrentTime;
        }

        public void SetTime(DateTimeStamp timeStamp)
        {
            CurrentTime = timeStamp;
        }


        private void validateDateTimeFormat(DateTimeUnit[] timeFormat)
        {
            if (timeFormat.Count(unit => !unit.LeavingRelations.Any()) == 0)
                throw new DateTimeFormatException($"No root unit found");

            if (timeFormat.Count(unit => !unit.LeavingRelations.Any()) > 1)
                throw new DateTimeFormatException($"More than one root unit found");
        }

        private Dictionary<Guid, ulong> buildTimeFormatValueDictionary(DateTimeUnit[] timeFormat)
        {
            var dictionary = new Dictionary<Guid, ulong>();
            var queue = new Queue<DateTimeUnitRelation>();

            dictionary[RootUnit.Id] = 1;
            RootUnit.LeavingRelations.ForEach(queue.Enqueue);

            while (queue.Count>0)
            {
                var relation = queue.Dequeue();

                if (!dictionary.TryGetValue(relation.Source.Id, out var sourceValue))
                    throw new DateTimeFormatException($"Relation '{relation.Name}' ({relation.Id}) was inspected before its source '{relation.Source.FullName}' ({relation.Source.Id})");
                
                
                var target = timeFormat.FirstOrDefault(t => t.Id == relation.Target.Id);
                if (target == null) throw new UnitNotFoundInFormatException(relation.Target);

                var value = sourceValue * relation.Value;
                if (dictionary.TryGetValue(target.Id, out var existingTargetValue) && existingTargetValue!=value)
                    throw new DateTimeFormatInconclusiveException(target, relation, value, existingTargetValue);

                dictionary[target.Id] = value;
                target.LeavingRelations.ForEach(queue.Enqueue);
            }

            return dictionary;
        }

        private void moveForward(ulong amount)
        {
            CurrentTime = new DateTimeStamp(CurrentTime.TimePart1 + amount, CurrentTime.TimePart2);
        }


        private void moveBackward(ulong amount)
        {
            CurrentTime = new DateTimeStamp(CurrentTime.TimePart1 - amount, CurrentTime.TimePart2);
        }
    }
}