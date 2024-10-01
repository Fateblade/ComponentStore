using System;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Tests
{
    [TestFixture]
    internal partial class TimeMachineTests
    {
        private readonly ITimeMachine _machine;
        private readonly DateTimeUnit _standardUnit;
        private readonly DateTimeUnit _largeUnit;
        private readonly DateTimeUnit _extremelyLargeUnit;


        public TimeMachineTests()
        {
            _standardUnit = new DateTimeUnit { Id = Guid.NewGuid(), FullName = "Standard Unit"};
            _largeUnit = new DateTimeUnit { Id = Guid.NewGuid(), FullName = "Large Unit" };
            _extremelyLargeUnit = new DateTimeUnit { Id = Guid.NewGuid(), FullName = "Extreme Unit" };

            var standardToLarge = new DateTimeUnitRelation
            {
                Source = _standardUnit,
                Target = _largeUnit,
                Value = uint.MaxValue,
                Name = "Standard->Large"
            };

            var largeToExtreme = new DateTimeUnitRelation
            {
                Source = _largeUnit,
                Target = _extremelyLargeUnit,
                Value = uint.MaxValue,
                Name = "Large->Extreme"
            };
            _standardUnit.LeavingRelations.Add(standardToLarge);
            
            _largeUnit.IncomingRelations.Add(standardToLarge);
            _largeUnit.LeavingRelations.Add(largeToExtreme);

            _extremelyLargeUnit.IncomingRelations.Add(largeToExtreme);

            _machine = new TimeMachine(_standardUnit, _largeUnit, _extremelyLargeUnit);
            
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

    }
}

//TODO:
/*  Complex types works
 *  Inconclusive types throw exceptions
 *
 *  extremely large date time units (one amount enough to increase datetimepart 2 directly)
 *
 * ´CanMove abdeckung
 */
