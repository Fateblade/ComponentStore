using System;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Tests
{
    [TestFixture]
    internal class ITimeMachineTests
    {
        private readonly ITimeMachine _machine;
        private readonly DateTimeUnit _standardUnit;

        public ITimeMachineTests()
        {
            _standardUnit = new DateTimeUnit();
            _machine = new TimeMachine(_standardUnit);
            
        }

        [Test]
        public void Initialisation_NoRoot_ThrowsException()
        {
            Action initialisation = () => new TimeMachine();


            initialisation.Should().Throw<DateTimeFormatException>();
        }

        [Test]
        public void Initialisation_TooManyRoot_ThrowsException()
        {
            Action initialisation = () => new TimeMachine(new DateTimeUnit(), new DateTimeUnit());


            initialisation.Should().Throw<DateTimeFormatException>();
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

        [Test]
        public void MoveForward_1StandardUnit_TimePart1ShouldBe1Larger()
        {
            var previousTime = _machine.CurrentTime;


            _machine.MoveForward(_standardUnit, 1);


            _machine.CurrentTime.TimePart1.Should().Be(previousTime.TimePart1 + 1);
            _machine.CurrentTime.TimePart2.Should().Be(previousTime.TimePart2);
        }

        [Test]
        public void MoveBackward_1StandardUnit_TimePart1ShouldBe1Smaller()
        {
            var previousTime = _machine.CurrentTime;


            _machine.MoveBackward(_standardUnit, 1);


            _machine.CurrentTime.TimePart1.Should().Be(previousTime.TimePart1 - 1);
            _machine.CurrentTime.TimePart2.Should().Be(previousTime.TimePart2);
        }
    }
}
