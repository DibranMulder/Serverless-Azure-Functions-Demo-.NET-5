using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.DependencyInjection;

var host = new HostBuilder()
    .ConfigureAppConfiguration(c =>
    {
        c.AddCommandLine(args);
        //c.AddEnvironmentVariables();
    })
    .ConfigureFunctionsWorkerDefaults(app =>
    {
        //app.UseMiddleware<ExampleMiddleware>();
    })
    .ConfigureServices(s =>
    {
        //s.AddSuffHere();
    })
    .Build();

await host.RunAsync();