using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Aspects;
using Fateblade.Components.Logic.Foundation.Translation.Contract.Exceptions;

namespace Fateblade.Components.Logic.Foundation.Translation.Contract
{
    [MapException(typeof(TranslationException))]
    public interface ITranslationStringProvider
    {
        string GetString(string key);
        string GetString(string key, string languageKey);
    }
}
