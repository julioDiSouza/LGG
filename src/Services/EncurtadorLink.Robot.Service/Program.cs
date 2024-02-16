using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Robot.Service.Hosted;
using Microsoft.Extensions.DependencyInjection;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(config => 
    { 
        config.SetBasePath(Directory.GetCurrentDirectory());
    })
    .ConfigureAppConfiguration((ctx, config) =>
    {
#if DEBUG
        config.AddJsonFile("appsettings.Development.json", optional: false);
#else
        config.AddJsonFile("appsettings.json", optional: false);
#endif
    })
    .ConfigureServices((ctx, services) =>
    {
        services.AddHostedService<RobotHosted>();
    })
    .Build();

await host.RunAsync();