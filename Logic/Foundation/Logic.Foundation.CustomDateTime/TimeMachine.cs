using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime
{
    class TimeMachine : ITimeMachine
    {
        private readonly object _lock = new object();
        private readonly Dictionary<Guid, ulong> _relativeToRootValues;
        private DateTimeStamp _currentTime;


        public IReadOnlyCollection<DateTimeUnit> TimeFormat { get; }
        public DateTimeUnit RootUnit { get; }

        public DateTimeStamp CurrentTime
        {
            get
            {
                lock (_lock)
                {
                    return _currentTime;
                }
            }
            private set => _currentTime = value;
        }


        public TimeMachine(params DateTimeUnit[] timeFormat)
            : this(new DateTimeStamp(0, 0), timeFormat) { }

        public TimeMachine(DateTimeStamp startingTime, params DateTimeUnit[] timeFormat)
        {
            validateDateTimeFormat(timeFormat);

            CurrentTime = startingTime;
            TimeFormat = timeFormat;
            RootUnit = timeFormat.First(unit => !unit.IncomingRelations.Any() && unit.LeavingRelations.Any());
            _relativeToRootValues = buildTimeFormatValueDictionary(timeFormat);
        }


        public bool CanMove(DateTimeUnit unit, int amount)
        {
            lock (_lock)
            {
                if (!_relativeToRootValues.TryGetValue(unit.Id, out var rootUnitsPerUnit)) return false;
                if (amount == 0) return true;
                
                var amountInRootUnit = (ulong)Math.Abs(amount) * rootUnitsPerUnit;

                return amount < 0 
                    ? canMoveBackward(amountInRootUnit) 
                    : canMoveForward(amountInRootUnit);
            }
        }

        public DateTimeStamp Move(DateTimeUnit unit, int amount)
        {
            lock (_lock)
            {
                if (!_relativeToRootValues.TryGetValue(unit.Id, out var rootUnitsPerUnit))
                    throw new UnitNotInFormatException(unit);
                if (amount == 0) return CurrentTime;
                

                var maxAmountPerMove = ulong.MaxValue / rootUnitsPerUnit;

                if (maxAmountPerMove <= (ulong)Math.Abs(amount))
                {
                    throw new ArgumentOutOfRangeException(nameof(amount), "Amount exceeds maximum move range");
                }

                var amountInRootUnit = (ulong)Math.Abs(amount) * rootUnitsPerUnit;

                if (amount < 0)
                {
                    moveBackward(amountInRootUnit);
                }
                else
                {
                    moveForward(amountInRootUnit);
                }

                return CurrentTime;
            }
        }

        public void SetTime(DateTimeStamp timeStamp)
        {
            lock(_lock)
            {
                CurrentTime = timeStamp;
            }
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
            var overflow = CurrentTime.TimePart1 + amount;

            if (CurrentTime.TimePart2 == ulong.MaxValue && overflow < CurrentTime.TimePart1)
                throw new DateTimeOverflowException();

            CurrentTime = new DateTimeStamp(
                overflow,
                overflow < CurrentTime.TimePart1
                    ? CurrentTime.TimePart2 + 1
                    : CurrentTime.TimePart2);
        }


        private void moveBackward(ulong amount)
        {
            var underflow = CurrentTime.TimePart1 - amount;

            if (CurrentTime.TimePart2 == 0 && underflow > CurrentTime.TimePart1)
                throw new DateTimeUnderflowException();

            CurrentTime = new DateTimeStamp(
                underflow,
                underflow > CurrentTime.TimePart1 
                    ? CurrentTime.TimePart2 - 1 
                    : CurrentTime.TimePart2);
        }

        private bool canMoveForward(ulong amount)
        {
            var overflow = CurrentTime.TimePart1 + amount;
            
            return CurrentTime.TimePart2 != ulong.MaxValue || overflow >= CurrentTime.TimePart1;
        }

        private bool canMoveBackward(ulong amount)
        {
            var underflow = CurrentTime.TimePart1 - amount;

            return CurrentTime.TimePart2 != 0 || underflow <= CurrentTime.TimePart1;
        }

    }
}