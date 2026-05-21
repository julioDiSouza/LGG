using LGG.VideoMetricas.Worker;
using LGG.Infraestrutura.Library.Database;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Infraestrutura.Library.VideoMetricas;
using LGG.Aplicacao.Library.Interfaces.IVideoMetricas;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IUnidTrabalho, UnidTrabalho>();
builder.Services.AddSingleton<IVideoMetricasService, VideoMetricasService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
