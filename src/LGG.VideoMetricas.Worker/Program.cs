using LGG.Aplicacao.Library.Interfaces.IServices;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Infraestrutura.Library.Database;
using LGG.Infraestrutura.Library.Services;
using LGG.VideoMetricas.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IUnidTrabalho, UnidTrabalho>();
builder.Services.AddSingleton<IVideoMetricasService, VideoMetricasService>();
builder.Services.AddSingleton<IYoutubeService, YoutubeService>();
builder.Services.AddHttpClient<IHttpService, HttpService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
