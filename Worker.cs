using Microsoft.Extensions.Options;
using OpenTelemetry.Exporter;

namespace OTELLoggingIgnoresConfigurationSample
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly IConfiguration configuration;
        private readonly OtlpExporterOptions otplExporterOptions;

        public Worker(
            ILogger<Worker> logger, 
            IOptions<OtlpExporterOptions> otplExporterOptions, 
            IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.otplExporterOptions = otplExporterOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("""
                    Worker running at: {time}.
                        'OTEL_EXPORTER_OTLP_ENDPOINT' from env var: {envvar}
                        'OTEL_EXPORTER_OTLP_ENDPOINT' from configuration: {configuration}
                        Otpl endpoint from OtlpExporterOptions: {endpoint}                        
                    """,
                    DateTimeOffset.Now, 
                    Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT"),
                    this.configuration["OTEL_EXPORTER_OTLP_ENDPOINT"],
                    this.otplExporterOptions.Endpoint);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}