using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Tests
{
    internal partial class TimeMachineTests
    {
        [Test]
        public void Move_StandardUnit_TimePart1ShouldBe1Larger()
        {
            using (new TimeMachineTestScope(_machine))
            {
                _machine.SetTime(new DateTimeStamp(0, 0));


                _machine.Move(_standardUnit, 1);


                _machine.CurrentTime.TimePart1.Should().Be(1);
                _machine.CurrentTime.TimePart2.Should().Be(0);

            }
        }

        [Test]
        public void Move_NegativeStandardUnit_ShouldDecreaseTimePart1()
        {
            using (new TimeMachineTestScope(_machine))
            {
                _machine.SetTime(new DateTimeStamp(1, 0));


                _machine.Move(_standardUnit, -1);


                _machine.CurrentTime.TimePart1.Should().Be(0);
                _machine.CurrentTime.TimePart2.Should().Be(0);
            }
        }

        [Test]
        public void Move_StandardUnitOnMaxTimePart1_TimePart2ShouldIncreaseAndTimePart1Reset()
        {
            using (new TimeMachineTestScope(_machine))
            {
                _machine.SetTime(new DateTimeStamp(ulong.MaxValue, 0));


                _machine.Move(_standardUnit, 1);


                _machine.CurrentTime.TimePart1.Should().Be(0);
                _machine.CurrentTime.TimePart2.Should().Be(1);
            }
        }

        [Test]
        public void Move_NegativeStandardUnitOnMinTimePart1WithTimePart2_TimePart2ShouldDecreaseAndTimePartIncreaseToMax()
        {
            using (new TimeMachineTestScope(_machine))
            {
                _machine.SetTime(new DateTimeStamp(0, 1));


                _machine.Move(_standardUnit, -1);


                _machine.CurrentTime.TimePart1.Should().Be(ulong.MaxValue);
                _machine.CurrentTime.TimePart2.Should().Be(0);
            }
        }

        [Test]
        public void Move_StandardUnitOnMaxTimePart1AndMaxTimePart2_ShouldThrowException()
        {
            using (new TimeMachineTestScope(_machine))
            {
                _machine.SetTime(new DateTimeStamp(ulong.MaxValue, ulong.MaxValue));


                Action increase = () => _machine.Move(_standardUnit, 1);


                increase.Should().Throw<DateTimeOverflowException>();
            }
        }


        [Test]
        public void Move_NegativeAmountStandardUnitOnMinValue_ShouldThrowException()
        {
            using (new TimeMachineTestScope(_machine))
            {
                _machine.SetTime(new DateTimeStamp(0, 0));


                Action negativeIncrease = ()=>_machine.Move(_standardUnit, -1);


                negativeIncrease.Should().Throw<DateTimeUnderflowException>();
            }
        }

        [Test]
        public void Move_LargeAmountOfExtremelyLargeUnit_Throws()
        {
            using (new TimeMachineTestScope(_machine))
            {
                _machine.SetTime(new DateTimeStamp(0, 0));


                Action negativeIncrease = () => _machine.Move(_extremelyLargeUnit, int.MaxValue);


                negativeIncrease.Should().Throw<DateTimeOverflowException>();
            }
        }
    }
}
