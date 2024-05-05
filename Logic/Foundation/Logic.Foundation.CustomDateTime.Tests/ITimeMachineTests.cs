using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;
using FluentAssertions;
using NUnit.Framework;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Tests
{
    [TestFixture]
    internal class ITimeMachineTests
    {
        private readonly ITimeMachine _machine;

        public ITimeMachineTests()
        {
            _machine = new TimeMachine();
        }

        [Test]
        public void CurrentTime_EmptyMachine_ShouldBeZero()
        {
            _machine.CurrentTime.TimePart1.Should().Be(0);
            _machine.CurrentTime.TimePart1.Should().Be(0);
        }

        [Test]
        public void CurrentTime_SetTime_ShouldBeSetTime()
        {
            var timeToSetTo = new DateTimeStamp(4, 2);


            _machine.SetTime(timeToSetTo);


            _machine.CurrentTime.TimePart1.Should().Be(timeToSetTo.TimePart1);
            _machine.CurrentTime.TimePart2.Should().Be(timeToSetTo.TimePart2);
        }


    }
}
