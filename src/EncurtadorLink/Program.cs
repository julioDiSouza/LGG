using System.Text;
using FluentValidation;
using EncurtadorLink.Rotas;
using EncurtadorLink.Extensoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.UseCases.Library.Services;
using EncurtadorLink.Database.Library.Contextos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using EncurtadorLink.Database.Library.UnidadeTrabalho;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using EncurtadorLink.Database.Library.Contextos.Interfaces;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var strConexaoMysql = builder.Configuration.GetConnectionString("DataBaseMysql");

// Add services to the container.
builder.Services.AddDbContext<MysqlContext>(options => options.UseMySql(strConexaoMysql, ServerVersion.AutoDetect(strConexaoMysql)));
builder.Services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<MysqlContext>();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped(typeof(IUrlService), typeof(UrlService));
builder.Services.AddScoped(typeof(ITokenService), typeof(TokenService));
builder.Services.AddScoped(typeof(IUsuarioService), typeof(UsuarioService));
builder.Services.AddScoped(typeof(IClienteService), typeof(ClienteService));
builder.Services.AddScoped(typeof(IUrlMongoDbService), typeof(UrlMongoDbService));
builder.Services.AddScoped(typeof(IUnidadeTrabalho), typeof(UnidadeTrabalho));
builder.Services.AddScoped(typeof(IMongoDbContext), typeof(MongoDbContext));

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

