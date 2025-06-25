using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RagAssistant.Job.Jobs;
using RagAssistant.Share.AppStarts;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        #region Logging
        LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}");

        // create logger before add Serilog logging with dispose: true
        Log.Logger = loggerConfiguration.CreateLogger();

        // by adding logging here will automatically added as a singleton scoped
        services.AddLogging(i => i.AddSerilog(dispose: true));
        #endregion

        // add jobs
        services.AddTransient<SyncFileJob>();

        // add collections
        services.AddFileCollection();
        services.AddMsGraphCollection(context.Configuration);
        services.AddOllamaCollection(context.Configuration);
        services.AddQdrantCollection(context.Configuration);
    })
    .Build();

// run manually
using (IServiceScope scope = host.Services.CreateScope())
{
    IServiceProvider serviceProvider = scope.ServiceProvider;
    SyncFileJob job = serviceProvider.GetRequiredService<SyncFileJob>();

    await job.RunAsync();
}
