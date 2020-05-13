using System;

namespace Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses
{
    public interface IIdentifiableGuidEntity
    {
        Guid Id { get; set; }
    }
}
