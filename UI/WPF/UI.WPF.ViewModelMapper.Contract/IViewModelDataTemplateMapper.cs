using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Aspects;
using Fateblade.Components.UI.WPF.ViewModelMapper.Contract.Exceptions;
using System;
using System.Windows;

namespace Fateblade.Components.UI.WPF.ViewModelMapper.Contract
{
    [MapException(typeof(ViewModelMapperException))]
    public interface IViewModelDataTemplateMapper
    {
        bool HasMapping<TType>();
        bool HasMapping(Type type);

        DataTemplate GetMapping(Type viewModelType);
        DataTemplate GetMapping<TType>();
        DataTemplate GetMapping(Type viewModelType, bool allowBaseTypeFallback);
        DataTemplate GetMapping<TType>(bool allowBaseTypeFallback);

        void RegisterMapping<TType>(DataTemplate template);
        void RegisterMapping(Type viewModelType, DataTemplate template);
        void RegisterMapping<TType>(DataTemplate template, bool overwrite);
        void RegisterMapping(Type viewModelType, DataTemplate template, bool overwrite);

        void RemoveMapping<TType>();
        void RemoveMapping(Type vmType);
    }
}
