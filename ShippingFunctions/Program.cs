using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var host = new HostBuilder()
    .ConfigureAppConfiguration(c =>
    {
        c.AddCommandLine(args);
    })
    .ConfigureFunctionsWorkerDefaults(app =>
    {
    })
    .ConfigureServices(s =>
    {
        s.AddHttpClient();
    })
    .Build();

await host.RunAsync();