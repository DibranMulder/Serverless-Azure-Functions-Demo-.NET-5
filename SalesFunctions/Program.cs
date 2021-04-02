using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SalesFunctions;

var host = new HostBuilder()
    .ConfigureAppConfiguration(c =>
    {
        c.AddCommandLine(args);
    })
    .ConfigureFunctionsWorkerDefaults(app =>
    {
        //app.UseMiddleware<ExampleMiddleware>();
    })
    .ConfigureServices(s =>
    {
        s.AddHttpClient();
        s.AddScoped<PlaceOrderHandler>();
    })
    .Build();

await host.RunAsync();