namespace Fateblade.Components.Logic.Foundation.FeatureTrackingComponent.Contract.Messages
{
    public class RegisterFeatureUsageMessage
    {
        public RegisterFeatureUsageMessage(string featureName)
        {
            FeatureName = featureName;
        }

        public string FeatureName { get; }
    }
}
