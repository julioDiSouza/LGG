using LGG.Email.Worker;
using LGG.Infraestrutura.Library.Database;
using LGG.Infraestrutura.Library.Services;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Interfaces.IServices;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IUnidTrabalho, UnidTrabalho>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IBrevoService, BrevoService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
