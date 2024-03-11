using System;
using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses
{
    public class DateTimeUnitRelation : IIdentifiableGuidEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTimeUnit Source { get; set; }
        public DateTimeUnit Target { get; set; }
        public uint Value { get; set; }
    }
}