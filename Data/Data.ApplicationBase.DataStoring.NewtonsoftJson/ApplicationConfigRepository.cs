using Fateblade.Components.CrossCutting.ApplicationBase;
using Fateblade.Components.CrossCutting.Logging.Contract;
using Fateblade.Components.CrossCutting.Logging.Contract.DataClasses;
using Fateblade.Components.Data.ApplicationBase.DataStoring.Contract;
using Newtonsoft.Json;
using System.IO;

namespace Fateblade.Components.Data.ApplicationBase.DataStoring.NewtonsoftJson
{
    internal class ApplicationConfigRepository : IApplicationConfigRepository
    {
        private readonly ILogger _logger;
        internal readonly string _completePath;

        public ApplicationConfigRepository(ILogger logger)
        {
            _logger = logger;
            
            string rootPath = Directory.GetCurrentDirectory();
            string fileName = "ApplicationConfig.json";


            _completePath = Path.Combine(rootPath, fileName);
            _logger.Log(LoggingPriority.None, LoggingType.Debug, $"Newtonsoft ApplicationConfigRepository initialized (Path: '{_completePath}')");
        }

        public ApplicationConfig Get()
        {
            if (!File.Exists(_completePath))
                return new ApplicationConfig();

            using (var sr = new StreamReader(File.Open(_completePath, FileMode.OpenOrCreate)))
            {
                return JsonConvert.DeserializeObject<ApplicationConfig>(sr.ReadToEnd());
            }
        }

        public void Save(ApplicationConfig config)
        {
            using (var sw = new StreamWriter(File.Open(_completePath, FileMode.Create)))
            {
                sw.Write(JsonConvert.SerializeObject(config));
            }
        }


    }
}
