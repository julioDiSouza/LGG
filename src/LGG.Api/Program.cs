using System.Text;
using FluentValidation;
using LGG.Aplicacao.Library.Interfaces.IRabbitMq;
using LGG.Aplicacao.Library.Interfaces.IServices;
using LGG.Aplicacao.Library.Mediator.CampeonaMensal.Commands;
using LGG.Aplicacao.Library.Mediator.Usuarios.Commands;
using LGG.Aplicacao.Library.Mediator.Videos.Commands;
using LGG.Aplicacao.Library.Validadores;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Infraestrutura.Library.Database;
using LGG.Infraestrutura.Library.RabbitMq;
using LGG.Infraestrutura.Library.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // sempre obrigat�rio
  //.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true) // opcional
    .AddEnvironmentVariables(); // ainda pode sobrescrever via vari�veis de ambiente

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IUnidTrabalho, UnidTrabalho>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IYoutubeService, YoutubeService>();
builder.Services.AddScoped<IBrevoService, BrevoService>();
builder.Services.AddHttpClient<IHttpService, HttpService>();

#region Configuracao JWT

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

#endregion Fim - Configuracao JWT

#region Swagger

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LGG Api", Version = "v1" });

    // Configura��o do esquema de seguran�a
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "LGG Api"
    });

    // Exige o token nos endpoints protegidos
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

#endregion Fim - Swagger

#region MediatR

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CadastrarUsuarioCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(AtualizarUsuarioCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CadastrarCampeonatoMensalCommand).Assembly);
});

#endregion Fim - MediatR

#region Validadores (FluentValidation)

builder.Services.AddValidatorsFromAssemblyContaining<UsuarioCadastroDtoValidador>();
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioAtualizarDtoValidador>();
builder.Services.AddValidatorsFromAssemblyContaining<CampeonatoMensalCadastroDtoValidador>();
builder.Services.AddValidatorsFromAssemblyContaining<CampeonatoMensalAtualizarDtoValidador>();
builder.Services.AddValidatorsFromAssemblyContaining<VideoCadastroDtoValidador>();
builder.Services.AddValidatorsFromAssemblyContaining<VideoAtualizarDtoValidador>();

#endregion Fim - Validadores (FluentValidation)

#region RabbitMq

builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));
builder.Services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
builder.Services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();

#endregion Fim - RabbitMq

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

