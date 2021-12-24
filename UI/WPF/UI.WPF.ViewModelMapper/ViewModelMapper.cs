using Fateblade.Components.UI.WPF.ViewModelMapper.Contract;
using Fateblade.Components.UI.WPF.ViewModelMapper.Contract.Exceptions;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Fateblade.Components.UI.WPF.ViewModelMapper
{
    class ViewModelMapper : IViewModelMapper
    {
        private readonly Dictionary<Type, DataTemplate> _dataTemplateDictionary;

        public ViewModelMapper()
        {
            _dataTemplateDictionary = new Dictionary<Type, DataTemplate>();
        }

        public DataTemplate GetMapping(Type viewModelType)
        {
            throwIfMappingDoesNotExist(viewModelType);

            return _dataTemplateDictionary[viewModelType];
        }

        public void RemoveMapping(Type viewModelType)
        {
            throwIfMappingDoesNotExist(viewModelType);

            _dataTemplateDictionary.Remove(viewModelType);
        }

        public void RegisterMapping(Type viewModelType, DataTemplate template)
        {
            if (_dataTemplateDictionary.ContainsKey(viewModelType))
            {
                throw new ViewModelMappingAlreadyExistsException(
                    $"Mapping already exists for view model of type '{viewModelType}'");
            }

            _dataTemplateDictionary.Add(viewModelType, template);
        }

        private void throwIfMappingDoesNotExist(Type viewModelType)
        {
            if (!_dataTemplateDictionary.ContainsKey(viewModelType))
            {
                throw new ViewModelMappingDoesNotExistException(
                    $"No known mapping for view model of type '{viewModelType}'");
            }
        }
    }
}
