using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Robot.Service.Hosted;
using EncurtadorLink.Robot.Service.Extensao;
using EncurtadorLink.Robot.Service.Services;
using EncurtadorLink.Shared.Library.Services;
using Microsoft.Extensions.DependencyInjection;
using EncurtadorLink.Robot.Service.Services.Interfaces;
using EncurtadorLink.Shared.Library.Services.Interfaces;

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
        services.AddHttpClient();

        services.AddSingleton<IProvedorTokenAutenticacao, ProvedorTokenAutenticacao>();
        services.AddHttpClient<IMonitoramento, Monitoramento>().AddPolicyHandler(services.ConfiguracaoTokenPolicy());

        services.AddHostedService<RobotHosted>();
    })
    .Build();

await host.RunAsync();

