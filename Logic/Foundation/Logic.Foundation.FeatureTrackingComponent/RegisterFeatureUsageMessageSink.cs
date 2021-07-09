using Fateblade.Components.Logic.Foundation.FeatureTrackingComponent.Contract;
using Fateblade.Components.Logic.Foundation.FeatureTrackingComponent.Contract.Messages;

namespace Fateblade.Components.Logic.Foundation.FeatureTrackingComponent
{
    internal class RegisterFeatureUsageMessageSink
    {
        private readonly IFeatureTracker _tracker;

        public RegisterFeatureUsageMessageSink(IFeatureTracker tracker)
        {
            _tracker = tracker;
        }

        public void HandleMessage(RegisterFeatureUsageMessage message)
        {
            _tracker.RegisterFeatureUsage(message.FeatureName);
        }
    }
}
