using System;
using System.Collections.Generic;
using System.Linq;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.Data.GenericDataStoring.Contract;
using Fateblade.Components.Logic.Foundation.ConsentManager.Contract;
using Fateblade.Components.Logic.Foundation.ConsentManager.Contract.Messages;
using Fateblade.Components.Logic.Foundation.ConsentManager.DataClasses;

namespace Fateblade.Components.Logic.Foundation.ConsentManager
{
    internal class ConsentManager : IConsentManager
    {
        private readonly IGenericRepository<ConsentEntry> _consentRepository;
        private readonly IEventBroker _eventBroker;

        public ConsentManager(IGenericRepository<ConsentEntry> consentRepository, IEventBroker eventBroker)
        {
            _consentRepository = consentRepository ?? throw new ArgumentException(nameof(consentRepository));
            _eventBroker = eventBroker ?? throw new ArgumentNullException(nameof(eventBroker));

            _eventBroker.Subscribe<ReceiveUserConsentMessage>(handleReceiveUserConsentMessage);
        }

        public bool? CheckForConsent(string key)
        {
            return getConsentEntryFromRepository(key)?.UserHasGivenConsent;
        }

        public void AskForConsent(string key)
        {
            _eventBroker.Raise(new RequestUserConsentMessage() {ConsentKey = key});
        }

        public void ChangeConsent(string key, bool value)
        {
            var entry = getConsentEntryFromRepository(key) ?? new ConsentEntry() {ConsentKey = key};
            entry.UserHasGivenConsent = value;

            _consentRepository.Update(entry);
        }

        public IReadOnlyDictionary<string, bool> GetGivenConsents()
        {
            return _consentRepository
                .Query
                .ToDictionary(
                    (entry) => entry.ConsentKey, 
                    (entry) => entry.UserHasGivenConsent);
        }

        private void handleReceiveUserConsentMessage(ReceiveUserConsentMessage consentMessage)
        {
            if (string.IsNullOrWhiteSpace(consentMessage.ConsentKey))
            {
                throw new ArgumentException("Consent key of '{nameof(ReceiveUserConsentMessage)}' may not be null or empty!");
            }

            ChangeConsent(consentMessage.ConsentKey, consentMessage.UserHasGivenConsent);
        }

        private ConsentEntry getConsentEntryFromRepository(string key)
        {
            return _consentRepository
                .Query
                .FirstOrDefault(entry => entry.ConsentKey == key);
        }
    }
}
