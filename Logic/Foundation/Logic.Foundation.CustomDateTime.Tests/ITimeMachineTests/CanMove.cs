using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;
using FluentAssertions;
using NUnit.Framework;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Tests
{
    internal partial class TimeMachineTests
    {
        [Test]
        public void CanMove_StandardUnit_ReturnsTrue()
        {
            using (new TimeMachineTestScope(_machine))
            {
                _machine.SetTime(new DateTimeStamp(0, 0));


                _machine.CanMove(_standardUnit, 1).Should().BeTrue();
            }
        }

        [Test]
        public void CanMoveForward_NegativeStandardUnit_ReturnsTrue()
        {
            using (new TimeMachineTestScope(_machine))
            {
                _machine.SetTime(new DateTimeStamp(1, 0));


                _machine.CanMove(_standardUnit, -1).Should().BeTrue();
            }
        }

        [Test]
        public void CanMove_StandardUnitOnMaxTimePart1_ReturnsTrue() 
        {
            using (new TimeMachineTestScope(_machine))
            {
                _machine.SetTime(new DateTimeStamp(ulong.MaxValue, 0));


                _machine.CanMove(_standardUnit, 1).Should().BeTrue();
            }
        }

        [Test]
        public void CanMove_NegativeStandardUnitOnMinTimePart1WithTimePart2_ReturnsTrue()
        {
            using (new TimeMachineTestScope(_machine))
            {
                _machine.SetTime(new DateTimeStamp(0, 1));


                _machine.CanMove(_standardUnit, -1).Should().BeTrue();
            }
        }

        [Test]
        public void CanMove_StandardUnitOnMaxTimePart1AndMaxTimePart2_ReturnsFalse()
        {
            using (new TimeMachineTestScope(_machine))
            {
                _machine.SetTime(new DateTimeStamp(ulong.MaxValue, ulong.MaxValue));


                _machine.CanMove(_standardUnit, 1).Should().BeFalse();
            }
        }


        [Test]
        public void CanMoveForward_NegativeStandardUnitOnMinValue_ReturnsFalse()
        {
            using (new TimeMachineTestScope(_machine))
            {
                _machine.SetTime(new DateTimeStamp(0, 0));


                _machine.CanMove(_standardUnit, -1).Should().BeFalse();
            }
        }
    }
}
