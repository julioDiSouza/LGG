using System.Text;
using FluentValidation;
using EncurtadorLink.Rotas;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.UseCases.Library.Services;
using EncurtadorLink.Database.Library.Contextos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using EncurtadorLink.Database.Library.UnidadeTrabalho;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;
using EncurtadorLink.Shared.Library.Services.Interfaces;
using EncurtadorLink.Shared.Library.Services;

#region ..:: Configuracoes ::..

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
#else
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
});

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

#endif

var strConexaoMysql = builder.Configuration.GetConnectionString("DataBaseMysql");

builder.Services.AddDbContext<MysqlContext>(options => 
options.UseMySql(strConexaoMysql, ServerVersion.AutoDetect(strConexaoMysql)));

builder.Services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<MysqlContext>();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped(typeof(MongoDbContext));
builder.Services.AddScoped(typeof(IUrlService), typeof(UrlService));
builder.Services.AddScoped(typeof(ITokenService), typeof(TokenService));
builder.Services.AddScoped(typeof(IUsuarioService), typeof(UsuarioService));
builder.Services.AddScoped(typeof(IClienteService), typeof(ClienteService));
builder.Services.AddScoped(typeof(IUrlMongoDbService), typeof(UrlMongoDbService));
builder.Services.AddScoped(typeof(IUnidadeTrabalho), typeof(UnidadeTrabalho));
builder.Services.AddScoped(typeof(IIEmailService), typeof(EmailService));
builder.Services.AddSingleton<IUrlMongoDbLazyService, UrlMongoDbLazyService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API - EncurtadorLink", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
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
            new string[] { }
        }
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(option => 
    {
        option.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidIssuers = builder.Configuration.GetSection("TokenJWT:Issuers").Get<List<string>>(),
            ValidateAudience = true,
            ValidAudiences = builder.Configuration.GetSection("TokenJWT:Audiences").Get<List<string>>(),
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenJWT:ChaveInterna"]!.ToString())),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

var ativarSwaggerPROD = true;
if (app.Environment.IsDevelopment() || ativarSwaggerPROD)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API - EncurtadorLink v1");
    });
}

var ativarExcecaoGenericaDEV = true;
if (!app.Environment.IsDevelopment() || ativarExcecaoGenericaDEV)
{
    app.UseExceptionHandler("/erro");
}

app.UseCors("CorsPolicy");
app.UseAuthorization();

#endregion ..:: Fim - Configuracoes ::..

// Rotas
app.RecuperaUrl("/");
app.EncurtaUrl("api", builder.Configuration);
app.Login("api", builder.Configuration);
app.Token("api/token");
app.Usuario("api/usuario", builder.Configuration);
app.Cliente("api/cliente", builder.Configuration);
app.Teste("api/teste");
app.ExcecaoGlobal("/erro");

app.Run();

