using System;
using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;

namespace Fateblade.Components.Logic.Foundation.ConsentManager.DataClasses
{
    internal class ConsentEntry : IIdentifiableGuidEntity
    {
        public Guid Id { get; set; }
        public string ConsentKey { get; set; }
        public bool UserHasGivenConsent { get; set; }
    }
}
