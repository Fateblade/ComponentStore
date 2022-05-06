namespace Fateblade.Components.Logic.Foundation.ConsentManager.Contract.Messages
{
    public class ReceiveUserConsentMessage
    {
        public string ConsentKey { get; set; }
        public bool UserHasGivenConsent { get; set; }
    }
}
