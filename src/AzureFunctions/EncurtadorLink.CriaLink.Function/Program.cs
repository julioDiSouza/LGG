using FluentValidation;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.UseCases.Library.Services;
using Microsoft.Extensions.DependencyInjection;
using EncurtadorLink.Database.Library.Contextos;
using EncurtadorLink.Database.Library.UnidadeTrabalho;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration(config =>
    {
#if DEBUG
        config.AddJsonFile("appsettings.Development.json", optional: false);
#else
        config.AddJsonFile("appsettings.json", optional: false);
#endif
    })
    .ConfigureServices((ctx, services) =>
    {
        var strConexaoMysql = ctx.Configuration.GetConnectionString("DataBaseMysql");
        // Add services to the container.
        services.AddDbContext<MysqlContext>(options => options.UseMySql(strConexaoMysql, ServerVersion.AutoDetect(strConexaoMysql)));
        services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<MysqlContext>();
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        services.AddScoped(typeof(IUnidadeTrabalho), typeof(UnidadeTrabalho));
        services.AddScoped(typeof(MongoDbContext));
        services.AddScoped(typeof(IUrlService), typeof(UrlService));

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
