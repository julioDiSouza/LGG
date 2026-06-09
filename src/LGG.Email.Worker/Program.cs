using LGG.Email.Worker;
using LGG.Infraestrutura.Library.RabbitMq;
using LGG.Aplicacao.Library.Interfaces.IRabbitMq;
using LGG.Infraestrutura.Library.Email;
using LGG.Aplicacao.Library.Interfaces.IServices;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));

builder.Services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
builder.Services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer>();
builder.Services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
builder.Services.AddSingleton<IEmailService, EmailService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
