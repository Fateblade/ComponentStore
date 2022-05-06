using System.Collections.Generic;

namespace Fateblade.Components.Logic.Foundation.ConsentManager.Contract
{
    public interface IConsentManager
    {
        void AskForConsent(string key);
        bool? CheckForConsent(string key);
        void ChangeConsent(string key, bool value);
        IReadOnlyDictionary<string, bool> GetGivenConsents();
    }
}
