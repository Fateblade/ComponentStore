using Fateblade.Components.UI.WPF.ViewModelMapper.Contract;
using Fateblade.Components.UI.WPF.ViewModelMapper.Contract.Exceptions;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Fateblade.Components.UI.WPF.ViewModelMapper
{
    internal class ViewModelDataTemplateMapper : IViewModelDataTemplateMapper
    {
        private readonly Dictionary<Type, DataTemplate> _dataTemplateDictionary;
        private bool _allowBaseTypeFallback;



        public ViewModelDataTemplateMapper()
        {
            _dataTemplateDictionary = new Dictionary<Type, DataTemplate>();
        }



        public bool HasMapping<TType>()
        {
            return HasMapping(typeof(TType));
        }

        public bool HasMapping(Type viewModelType)
        {
            return _dataTemplateDictionary.ContainsKey(viewModelType);
        }


        public void RegisterMapping<TType>(DataTemplate template)
        {
            RegisterMapping(typeof(TType), template, false);
        }

        public void RegisterMapping(Type viewModelType, DataTemplate template)
        {
            RegisterMapping(viewModelType, template, false);
        }

        public void RegisterMapping<TType>(DataTemplate template, bool overwrite)
        {
            RegisterMapping(typeof(TType), template, overwrite);
        }

        public void RegisterMapping(Type viewModelType, DataTemplate template, bool overwrite)
        {
            if (_dataTemplateDictionary.ContainsKey(viewModelType))
            {
                if(!overwrite) throw new ViewModelMappingAlreadyExistsException(viewModelType);

                _dataTemplateDictionary.Remove(viewModelType);
            }

            _dataTemplateDictionary.Add(viewModelType, template);
        }


        public DataTemplate GetMapping<TType>()
        {
            return GetMapping(typeof(TType), _allowBaseTypeFallback);
        }

        public DataTemplate GetMapping(Type viewModelType)
        {
            return GetMapping(viewModelType, _allowBaseTypeFallback);
        }

        public DataTemplate GetMapping<TType>(bool allowBaseTypeFallback)
        {
            return GetMapping(typeof(TType), allowBaseTypeFallback);
        }

        public DataTemplate GetMapping(Type viewModelType, bool allowBaseTypeFallback)
        {
            if (_dataTemplateDictionary.ContainsKey(viewModelType))
                return _dataTemplateDictionary[viewModelType];

            if (allowBaseTypeFallback && viewModelType.BaseType != typeof(object))
                return GetMapping(viewModelType.BaseType, true);

            throw new ViewModelMappingDoesNotExistException(viewModelType);
        }


        public void RemoveMapping<TType>()
        {
            RemoveMapping(typeof(TType));
        }

        public void RemoveMapping(Type viewModelType)
        {
            _dataTemplateDictionary.Remove(viewModelType);
        }

        public void SetDefaultBaseTypeFallback(bool allowBaseTypeFallback)
        {
            _allowBaseTypeFallback = allowBaseTypeFallback;
        }
    }
}
