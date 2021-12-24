using System;
using System.Runtime.Serialization;

namespace Fateblade.Components.Logic.Foundation.Translation.Contract.Exceptions
{
    [Serializable]
    public class MissingTranslationException : TranslationException
    {
        public MissingTranslationException() { }
        public MissingTranslationException(string key) 
            : base($"Missing translation for key '{key}'")
        { }
        public MissingTranslationException(string language, string key) 
            : base($"Missing key '"+key+"' for language '"+language+"'")
        { }
        public MissingTranslationException(string message, string language, string key) 
            : base(message + $";{Environment.NewLine} Language: "+ language +$"{Environment.NewLine}: Key" + key)
        { }
        public MissingTranslationException(string key, Exception inner) 
            : base($"Missing translation for key '{key}'", inner)
        { }
        public MissingTranslationException(string language, string key, Exception inner) 
            : base($"Missing key '" + key + "' for language '" + language + "'", inner)
        { }
        public MissingTranslationException(string message, string language, string key, Exception inner) 
            : base(message + $";{Environment.NewLine} Language: " + language + $"{Environment.NewLine}: Key" + key, inner)
        { }

        protected MissingTranslationException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        { }
    }
}
