using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;
using System;

namespace Fateblade.Components.Logic.Foundation.FeatureTrackingComponent.DataContract
{
    public class IdentifiableFeatureCallAmountEntry : IIdentifiableGuidEntity
    {
        public Guid Id { get; set; }
        public string FeatureName { get; set; }
        public int CallAmount { get; set; }
    }
}
