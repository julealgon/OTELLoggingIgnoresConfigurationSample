using OpenTelemetry.Logs;
using OTELLoggingIgnoresConfigurationSample;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.AddOpenTelemetry(c => c.AddOtlpExporter());
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
