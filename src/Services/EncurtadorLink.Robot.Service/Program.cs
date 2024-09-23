using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Robot.Service.Hosted;
using EncurtadorLink.Robot.Service.Extensao;
using EncurtadorLink.Robot.Service.Services;
using EncurtadorLink.Shared.Library.Services;
using Microsoft.Extensions.DependencyInjection;
using EncurtadorLink.Robot.Service.Services.Interfaces;
using EncurtadorLink.Shared.Library.Services.Interfaces;
using EncurtadorLink.Database.Library.Contextos;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using EncurtadorLink.UseCases.Library.Services;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;
using EncurtadorLink.Database.Library.UnidadeTrabalho;
using Microsoft.EntityFrameworkCore;

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

        var strConexaoMysql = ctx.Configuration.GetConnectionString("DataBaseMysql");

        services.AddDbContext<MysqlContext>(options =>
        options.UseMySql(strConexaoMysql, ServerVersion.AutoDetect(strConexaoMysql)));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddSingleton<IProvedorTokenAutenticacao, ProvedorTokenAutenticacao>();
        services.AddHttpClient<IMonitoramento, Monitoramento>().AddPolicyHandler(services.ConfiguracaoTokenPolicy());
        services.AddScoped<IFilaEmailService, FilaEmailService>();
        services.AddScoped<IUnidadeTrabalho, UnidadeTrabalho>();
        services.AddScoped<IIEmailService, EmailService>();
        services.AddScoped(typeof(MongoDbContext));

        services.AddHostedService<RobotHosted>();
    })
    .Build();

await host.RunAsync();

