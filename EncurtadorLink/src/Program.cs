using System.Text;
using FluentValidation;
using EncurtadorLink.Rotas;
using EncurtadorLink.Services;
using EncurtadorLink.Endpoints;
using EncurtadorLink.Entidades;
using Microsoft.EntityFrameworkCore;
using EncurtadorLink.Infra.Extensoes;
using Microsoft.IdentityModel.Tokens;
using EncurtadorLink.Infra.BancoDados;
using EncurtadorLink.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var strConexao = builder.Configuration.GetConnectionString("DataBaseMysql");

// Add services to the container.
builder.Services.AddDbContext<ContextoDb>(options => options.UseMySql(strConexao, ServerVersion.AutoDetect(strConexao)));
builder.Services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<ContextoDb>();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped(typeof(IUrlEncurtadorService), typeof(UrlEncurtadorService));
builder.Services.AddScoped(typeof(ITokenService), typeof(TokenService));
builder.Services.AddScoped(typeof(IUsuarioService), typeof(UsuarioService));
builder.Services.AddScoped(typeof(IClienteService), typeof(ClienteService));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(option => 
    {
        option.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidIssuers = configuration.GetSection("TokenJWT:Issuers").Get<List<string>>(),
            ValidateAudience = true,
            ValidAudiences = configuration.GetSection("TokenJWT:Audiences").Get<List<string>>(),
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(configuration["TokenJWT:ChaveInterna"]!.ToString())
        ),
            ValidateLifetime = true
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.AplicarMigrations();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Rotas
app.Raiz("api");
app.Teste("api/teste");
app.Token("api/token");
app.Usuario("api/usuario");
app.Cliente("api/cliente");

app.Run();

