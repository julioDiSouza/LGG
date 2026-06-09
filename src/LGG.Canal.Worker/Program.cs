using LGG.Canal.Worker;
using LGG.Infraestrutura.Library.Database;
using LGG.Infraestrutura.Library.RabbitMq;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Interfaces.IRabbitMq;
using LGG.Infraestrutura.Library.Services;
using LGG.Aplicacao.Library.Interfaces.IServices;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));

builder.Services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
builder.Services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer>();
builder.Services.AddSingleton<IUnidTrabalho, UnidTrabalho>();
builder.Services.AddSingleton<IYoutubeService, YoutubeService>();
builder.Services.AddHttpClient<IHttpService, HttpService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
