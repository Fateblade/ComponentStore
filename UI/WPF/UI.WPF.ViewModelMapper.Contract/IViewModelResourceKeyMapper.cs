using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Aspects;
using Fateblade.Components.UI.WPF.ViewModelMapper.Contract.Exceptions;
using System;

namespace Fateblade.Components.UI.WPF.ViewModelMapper.Contract
{
    [MapException(typeof(ViewModelMapperException))]
    public interface IViewModelResourceKeyMapper
    {
        bool HasMapping<TType>();
        bool HasMapping(Type type);

        string GetMapping<TType>();
        string GetMapping(Type type);
        string GetMapping<TType>(bool allowBaseTypeFallback);
        string GetMapping(Type type, bool allowBaseTypeFallback);
        
        void RegisterMapping<TType>(string templateKey);
        void RegisterMapping(Type viewModelType, string templateKey);
        void RegisterMapping<TType>(string templateKey, bool overwrite);
        void RegisterMapping(Type viewModelType, string templateKey, bool overwrite);
        
        void RemoveMapping<TType>();
        void RemoveMapping(Type vmType);

        void SetDefaultBaseTypeFallback(bool allowBaseTypeFallback);
    }
}
