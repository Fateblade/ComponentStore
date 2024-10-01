using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;
using FluentAssertions;
using NUnit.Framework;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Tests
{
    internal partial class TimeMachineTests
    {
        [Test]
        public void CurrentTime_EmptyMachine_ShouldBeZero()
        {
            var emptyMachine = new TimeMachine(_standardUnit, _extremelyLargeUnit, _largeUnit);
            emptyMachine.CurrentTime.TimePart1.Should().Be(0);
            emptyMachine.CurrentTime.TimePart1.Should().Be(0);
        }

        [Test]
        public void CurrentTime_SetTime_ShouldBeSetTime()
        {
            using (new TimeMachineTestScope(_machine))
            {
                var timeToSetTo = new DateTimeStamp(4, 2);


                _machine.SetTime(timeToSetTo);


                _machine.CurrentTime.TimePart1.Should().Be(timeToSetTo.TimePart1);
                _machine.CurrentTime.TimePart2.Should().Be(timeToSetTo.TimePart2);
            }
        }
    }
}