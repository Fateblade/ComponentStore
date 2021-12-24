using System;
using System.Windows;

namespace Fateblade.Components.UI.WPF.ViewModelMapper.Contract
{
    public interface IViewModelMapper
    {
        DataTemplate GetMapping(Type viewModelType);
        void RegisterMapping(Type viewModelType, DataTemplate template);
        void RemoveMapping(Type viewModelType);
    }
}
