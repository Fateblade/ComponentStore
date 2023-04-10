using Fateblade.Components.UI.WPF.ViewModelMapper.Contract;
using Fateblade.Components.UI.WPF.ViewModelMapper.Contract.Exceptions;
using System;
using System.Collections.Generic;

namespace Fateblade.Components.UI.WPF.ViewModelMapper
{
    internal class ViewModelResourceKeyMapper : IViewModelResourceKeyMapper
    {
        private readonly Dictionary<Type, string> _dictionary;
        private bool _allowBaseTypeFallback;



        public ViewModelResourceKeyMapper()
        {
            _dictionary = new Dictionary<Type, string>();
        }



        public bool HasMapping<TType>()
        {
            return HasMapping(typeof(TType));
        }

        public bool HasMapping(Type type)
        {
            if (_dictionary.ContainsKey(type))
            {
                return true;
            }

            if (type.BaseType != typeof(object))
            {
                return HasMapping(type.BaseType);
            }

            return false;
        }


        public void RegisterMapping<TType>(string templateKey)
        {
            RegisterMapping<TType>(templateKey, false);
        }

        public void RegisterMapping(Type viewModelType, string templateKey)
        {
            RegisterMapping(viewModelType, templateKey, false);
        }

        public void RegisterMapping<TType>(string templateKey, bool overwrite)
        {
            RegisterMapping(typeof(TType), templateKey, overwrite);
        }

        public void RegisterMapping(Type viewModelType, string templateKey, bool overwrite)
        {
            if (_dictionary.ContainsKey(viewModelType))
            {
                if (!overwrite) throw new ViewModelMappingAlreadyExistsException(viewModelType);
                _dictionary.Remove(viewModelType);
            }

            _dictionary.Add(viewModelType, templateKey);
        }


        public string GetMapping<TType>()
        {
            return GetMapping(typeof(TType), _allowBaseTypeFallback);
        }

        public string GetMapping(Type type)
        {
            return GetMapping(type, _allowBaseTypeFallback);
        }

        public string GetMapping<TType>(bool allowBaseTypeFallback)
        {
            return GetMapping(typeof(TType), allowBaseTypeFallback);
        }

        public string GetMapping(Type type, bool allowBaseTypeFallback)
        {
            if (_dictionary.ContainsKey(type))
                return _dictionary[type];

            if (allowBaseTypeFallback && type.BaseType != typeof(object))
                return GetMapping(type.BaseType);

            throw new ViewModelMappingDoesNotExistException(type);
        }


        public void RemoveMapping<TType>()
        {
            RemoveMapping(typeof(TType));
        }

        public void RemoveMapping(Type vmType)
        {
            _dictionary.Remove(vmType);
        }

        public void SetDefaultBaseTypeFallback(bool allowBaseTypeFallback)
        {
            _allowBaseTypeFallback = allowBaseTypeFallback;
        }
    }
}
