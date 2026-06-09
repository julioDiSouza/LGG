using LGG.VideoCiclo.Worker;
using LGG.Infraestrutura.Library.Database;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Infraestrutura.Library.Services;
using LGG.Aplicacao.Library.Interfaces.IServices;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IUnidTrabalho, UnidTrabalho>();
builder.Services.AddSingleton<IVideoCicloService, VideoCicloService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
