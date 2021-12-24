using System;
using Newtonsoft.Json;

namespace Fateblade.Components.CrossCutting.CoCo.Core.Configuration.NewtonsoftJson
{
    class JsonDeserializer
    {
        public static object ToObject(object elementValue, Type valueType)
        {
            return JsonConvert.DeserializeObject(elementValue.ToString(), valueType);
        }

        public static object ToRecursiveObject(object elementValue, Type valueType)
        {
            return null;
        }
    }
}
