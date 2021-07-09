using Fateblade.Components.Data.GenericDataStoring.Contract;
using Fateblade.Components.Logic.Foundation.FeatureTrackingComponent.Contract;
using Fateblade.Components.Logic.Foundation.FeatureTrackingComponent.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fateblade.Components.Logic.Foundation.FeatureTrackingComponent
{
    internal class ManualFeatureTracker : IFeatureTracker
    {
        private readonly IGenericRepository<IdentifiableFeatureCallAmountEntry> _dataRepository;

        public ManualFeatureTracker(IGenericRepository<IdentifiableFeatureCallAmountEntry> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public void RegisterFeatureUsage(string featureName)
        {
            var dataEntry = _dataRepository.Query.FirstOrDefault(entry => entry.FeatureName == featureName) ??
                            new IdentifiableFeatureCallAmountEntry() {Id = Guid.NewGuid()};

            dataEntry.CallAmount++;

            _dataRepository.Update(dataEntry);
        }

        public IReadOnlyDictionary<string, int> GetUsageList()
        {
            Dictionary<string, int> retVal = new Dictionary<string, int>();

            foreach (var entry in _dataRepository.Query)
            {
                retVal[entry.FeatureName] = entry.CallAmount;
            }

            return retVal;
        }
    }
}
