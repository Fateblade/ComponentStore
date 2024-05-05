using System.ComponentModel;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Bootstrapping;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Configuration.ConfigObjects;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Configuration;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Bootstrapping;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.DependencyInjection;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.EventBrokerage;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.NinjectAdapter;
using Fateblade.Components.CrossCutting.CoCo.Core.Configuration.NewtonsoftJson;
using Fateblade.Components.CrossCutting.ExceptionFormatter.SimpleListFormat;
using Fateblade.Components.CrossCutting.Logging.Csv;
using Fateblade.Components.Logic.Foundation.OCR.Contract;
using Fateblade.Components.Logic.Foundation.OCR.Contract.DataClasses;
using Fateblade.Components.Logic.Foundation.OCR.Tesseract;
using Spectre.Console;

AnsiConsole.WriteLine("Hello, World!");

var pathToConfig = Path.Combine(Directory.GetCurrentDirectory(), "MergedConfig.json");

AnsiConsole.WriteLine("Initializing Kernel");
IKernelContainer kernelContainer = new KernelContainer();
var kernel = kernelContainer.Kernel;
kernel.Register<IBootstrapper, Bootstrapper>();
kernel.Register<IEventBroker, EventBroker>();
kernel.RegisterUnique<IConfigurationRepository, DatabaseConfigurationRepository>(new DatabaseConfigurationRepository(pathToConfig));
kernel.Register<IConfigurator, Configurator>();
kernel.Register<IConfigObjectProvider, ConfigObjectProvider>();
kernel.RegisterComponent<SimpleListExceptionFormatterComponentActivator>();
kernel.RegisterComponent<LoggingCsvComponentActivator>();

kernel.RegisterComponent<TesseractOcrScannerComponentActivator>();

AnsiConsole.WriteLine("Bootstrap components");

var bootstrapper = kernel.Get<IBootstrapper>();
bootstrapper.RegisterAllMappings(kernel);
bootstrapper.ActivatingAll();
bootstrapper.ActivatedAll();




var scanner = kernel.Get<IOcrScanner>();


var jpgTask = AnsiConsole.Progress().StartAsync(async (context)=>
{
    var task = context.AddTask("Scanning via jpg transformation");
    task.MaxValue = 100;
    task.StartTask();
    context.Refresh();

    var scanInfosJpg = new List<ScanInfo>();

    BackgroundWorker bgw = new BackgroundWorker();
    bgw.DoWork += (o, e) =>
    {
        var x = ((ProgressTask pt, ProgressContext pc, List<ScanInfo> ls)?)e.Argument;
        if (!x.HasValue) { return; }

        for (var i = 0; i < 100; i++)
        {
            x.Value.ls.Add(scanner.ScanPdfUsingJpgConversion(@".\samples\2024-02-17_11.18.31.pdf", 600));
            x.Value.pt.Increment(1);
            x.Value.pc.Refresh();
        }
    };
    bgw.RunWorkerAsync((task, context, scanInfosJpg));

    //task.Increment(1);
    //context.Refresh();
    while (bgw.IsBusy)
    {
        Thread.Sleep(100);
    }

    AnsiConsole.WriteLine($"Max Confidence: {scanInfosJpg.Max((t) => t.Confidence)}");

    return Task.FromResult(scanInfosJpg);
});


var pngTask = AnsiConsole.Progress().StartAsync((context) =>
{
    var task = context.AddTask("Scanning via png transformation");
    task.MaxValue = 100;
    task.StartTask();
    context.Refresh();

    var scanInfosPng = new List<ScanInfo>();


    BackgroundWorker bgw = new BackgroundWorker();
    bgw.DoWork += (o, e) =>
    {
        var x = ((ProgressTask pt, ProgressContext pc, List<ScanInfo> ls)?)e.Argument;
        if (!x.HasValue) { return; }

        for (var i = 0; i < 100; i++)
        {
            x.Value.ls.Add(scanner.ScanPdfUsingPngConversion(@".\samples\2024-02-17_11.18.31.pdf", 600));
            x.Value.pt.Increment(1);
            x.Value.pc.Refresh();
        }
    };
    bgw.RunWorkerAsync((task, context, scanInfosPng));

    while (bgw.IsBusy)
    {
        Thread.Sleep(100);
    }

    AnsiConsole.WriteLine($"Max Confidence: {scanInfosPng.Max((t) => t.Confidence)}");

    return Task.FromResult(scanInfosPng);
});


while (!pngTask.IsCompleted && !jpgTask.IsCompleted)
{
    await Task.Delay(100);
}






