using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Aspects;
using Fateblade.Components.Logic.Foundation.Translation.Contract.Exceptions;

namespace Fateblade.Components.Logic.Foundation.Translation.Contract
{
    [MapException(typeof(TranslationException))]
    public interface ITranslationStringProvider
    {
        void ChangeDefaultLanguage(string newDefaultLanguageKey);
        
        string GetString(string key);
        string GetString(string key, string languageKey);

        void LoadStringResourcesForDefaultLanguage(string resourceFileName);
        void LoadStringResourcesForLanguage(string resourceFileName, string languageKeyForContainedStrings);
    }
}
