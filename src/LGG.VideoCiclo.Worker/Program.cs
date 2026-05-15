using LGG.VideoCiclo.Worker;
using LGG.Infraestrutura.Library.Database;
using LGG.Infraestrutura.Library.VideoCiclo_;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Interfaces.IVideoCiclo;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IUnidTrabalho, UnidTrabalho>();
builder.Services.AddSingleton<IVideoCicloService, VideoCicloService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
