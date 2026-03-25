using LGG.Canal.Worker;
using LGG.Infraestrutura.Library.Http;
using LGG.Infraestrutura.Library.Youtube;
using LGG.Infraestrutura.Library.Database;
using LGG.Infraestrutura.Library.RabbitMq;
using LGG.Aplicacao.Library.Interfaces.IHttp;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Interfaces.IYoutube;
using LGG.Aplicacao.Library.Interfaces.IRabbitMq;

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
