using System.Collections.Generic;

namespace Fateblade.Components.Logic.Foundation.FeatureTrackingComponent.Contract
{
    public interface IFeatureTracker
    {
        void RegisterFeatureUsage(string featureName);
        IReadOnlyDictionary<string, int> GetUsageList();
    }
}
