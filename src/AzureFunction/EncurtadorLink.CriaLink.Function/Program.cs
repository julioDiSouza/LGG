using EncurtadorLink.Database.Library.Contextos;
using EncurtadorLink.Database.Library.Contextos.Interfaces;
using EncurtadorLink.Database.Library.UnidadeTrabalho;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.UseCases.Library.Services;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        
        services.AddScoped(typeof(IUrlService), typeof(UrlService));
        services.AddScoped(typeof(IUnidadeTrabalho), typeof(UnidadeTrabalho));
        services.AddScoped(typeof(IMongoDbContext), typeof(MongoDbContext));

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
